using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Threading;
using System.IO;
using ExcelDataReader;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Security.Cryptography;

namespace PPLDatabaseManager
{
    public partial class frmPPLDatabaseForm1 : Form
    {
        string ConnectionString = "server=localhost; database=partsdb; user=root";
        SqlStatements SqlSt = new SqlStatements();
        List<string[]> pplData = new List<string[]>();

        public frmPPLDatabaseForm1()
        {
            InitializeComponent();
            CheckDbConnection(false);
        }

        private void CheckDbConnection(bool msgBox)
        {
            string msg;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();
                    DateTime currentTime = new DateTime();
                    currentTime = DateTime.Now;
                    msg = "Database Status: " + connection.State.ToString() + " (@ " + currentTime.ToLocalTime() + ")";
                    lblDbStatus.Text = msg;
                    if (msgBox)
                    {
                        MessageBox.Show(msg);
                    }
                    connection.Close();
                }
            }
            catch (Exception)
            {
                DateTime currentTime = new DateTime();
                currentTime = DateTime.Now;
                msg = "Database Status: Unable to connect" + " (@ " + currentTime.ToLocalTime() + ")";
                lblDbStatus.Text = msg;
                if (msgBox)
                {
                    MessageBox.Show(msg);
                }
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CheckDbConnection(true);
        }

        private void btnImportPplData_Click(object sender, EventArgs e)
        {
            btnImportPplData.Enabled = false;

            importPPLData();
        }

        private void importPPLData()
        {
            {
                try
                {
                    //user browses to folder and selects ppl spreadsheet
                    OpenFileDialog pplFileSelected = new OpenFileDialog
                    {
                        Filter = "Excel Files (*.xlsx)|*.xlsx",
                        Multiselect = false
                    };

                    if (pplFileSelected.ShowDialog() == DialogResult.OK)
                    {
                        string pplFileName = pplFileSelected.FileName;
                        string pplFilePath = Path.GetFullPath(pplFileName);
                        pplFilePath = Directory.GetParent(pplFilePath).FullName;

                        //pull data from excel file
                        using (var stream = File.Open(pplFileName, FileMode.Open, FileAccess.Read))
                        {
                            using (var reader = ExcelReaderFactory.CreateReader(stream))
                            {
                                var ds = reader.AsDataSet();
                                pplData.Clear();

                                List<string> pplRow = new List<string>();

                                foreach (DataRow dataRow in ds.Tables[0].Rows)
                                {
                                    for (int i = 0; i < dataRow.ItemArray.Length; i++)
                                    {
                                        string cell = dataRow.ItemArray[i].ToString();
                                        pplRow.Add(cell);
                                    }

                                    // set the unique PARTID, a concatenation of the PLISN, INDC, CAGE, PN, and NHA fields
                                    string[] pplRowArr = pplRow.ToArray();
                                    string PartId = pplRowArr[1].Trim() + pplRowArr[2].Trim() + pplRowArr[3].Trim() + pplRowArr[4].Trim() + pplRowArr[31].Trim();
                                    string hashPart = string.Empty;

                                    // one way hash for making PARTID a fixed length (28) for unique column
                                    using (SHA1 sha1 = SHA1.Create())
                                    {
                                        byte[] prehash = sha1.ComputeHash(Encoding.UTF32.GetBytes(PartId));
                                        byte[] hash = sha1.ComputeHash(prehash);
                                        hashPart = Convert.ToBase64String(hash);
                                    }
                                    pplRow.Add(hashPart);
                                    pplRowArr = pplRow.ToArray();

                                    // prevent excel header row from being added to the data set
                                    if (pplRowArr[0] != "PCCN")
                                    {
                                        pplData.Add(pplRowArr);
                                    }
                                    pplRow.Clear();
                                }

                                lblActiveDoc.Text = "Active Document: " + pplFileSelected.SafeFileName + "\nParts Count: " + pplData.Count.ToString();
                                if (pplData.Count > 0)
                                {
                                    lbxActivePPLparts.Items.Clear();
                                    foreach (string[] row in pplData)
                                    {
                                        lbxActivePPLparts.Items.Add(row[4]);
                                    }
                                    btnImportPplData.Enabled = true;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void importPPLDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            importPPLData();
        }

        private void btnShowAllRecords_Click(object sender, EventArgs e)
        {
            DisplayAllDbRecords();
        }

        private void DisplayAllDbRecords()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection();
                connection.ConnectionString = ConnectionString;
                string selectAllQuery = "SELECT * FROM `ppl`";
                MySqlCommand selectcmd = new MySqlCommand(selectAllQuery, connection);

                //Clear out DataGridView control
                dgvDbOutput.DataSource = null;

                System.Data.DataTable dt = new System.Data.DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(selectcmd);
                adapter.Fill(dt);
                dgvDbOutput.DataSource = dt;
                //dgvDbOutput.Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:  " + ex.Message);
            }
        }

        private void btnImportPplData_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (pplData.Count > 0)
                {
                    SqlSt.ValidatePPLData(pplData, SqlSt.PplDbRows);
                    SqlSt.AddPplDataToDatabase(pplData);
                    DisplayAllDbRecords();
                }
                lbxActivePPLparts.Items.Clear();
                lblActiveDoc.Text = "Active Document: None";
                btnImportPplData.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("There has been an error!\n\n" + ex.Message, "Error!");
            }
        }
    }
}
