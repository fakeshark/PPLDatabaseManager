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

namespace PPLDatabaseManager
{
    public partial class frmPPLDatabaseForm1 : Form
    {
        string ConnectionString = "server=localhost; database=partsdb; user=root";
        SqlStatements SqlSt = new SqlStatements();

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
                                List<string[]> pplData = new List<string[]>();
                                List<string> pplRow = new List<string>();

                                foreach (DataRow dataRow in ds.Tables[0].Rows)
                                {
                                    for (int i = 0; i < dataRow.ItemArray.Length; i++)
                                    {
                                        string cell = dataRow.ItemArray[i].ToString();
                                        pplRow.Add(cell);
                                    }
                                    string[] pplRowArr = pplRow.ToArray();
                                    pplRow.Clear();
                                    pplData.Add(pplRowArr);
                                }

                                SqlSt.AddPplDataToDatabase(pplData);
                                DisplayAllDbRecords();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There has been an error!\n\n" + ex.Message, "Error!");
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
    }
}
