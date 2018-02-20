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
        List<string> savedQueryList = new List<string>();

        public frmPPLDatabaseForm1()
        {
            InitializeComponent();
            CheckDbConnection(false);
            SetDbDrivenFormControls();
        }

        public class ComboboxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        private void SetDbDrivenFormControls()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection();
                connection.ConnectionString = ConnectionString;
                string selectAllQuery = "SELECT * FROM `storedquery`";

                MySqlCommand selectcmd = new MySqlCommand(selectAllQuery, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(selectcmd);

                System.Data.DataTable dt = new System.Data.DataTable();
                adapter.Fill(dt);
                int counter = 0;
                savedQueryList.Clear();
                cbxSavedCommands.Items.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    if (counter == 0)
                    {
                        btnRunActiveCommand.Text = (dr["QUERYNAME"].ToString());
                    }

                    ComboboxItem item = new ComboboxItem();


                    item.Text = dr["QUERYNAME"].ToString();
                    savedQueryList.Add(dr["QUERY"].ToString());

                    cbxSavedCommands.Items.Add(item);
                    counter++;
                }
                cbxSavedCommands.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                btnRunActiveCommand.Text = "...";
            }
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
                    SetDbDrivenFormControls();
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
            Button btn = sender as Button;
            if (btn.Text == "...")
            {
                    MessageBox.Show("There is a problem connecting to the database, make sure the database is running and try again.", "Error");
                CheckDbConnection(false);
            }
            else
            {
                string[] queryArray = savedQueryList.ToArray();
                int selectIndex = cbxSavedCommands.SelectedIndex;
                RunActiveCommand(queryArray[selectIndex]);
            }
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

        private void RunActiveCommand(string Query)
        {
            string message = string.Empty;
            try
            {
                MySqlConnection connection = new MySqlConnection();
                connection.ConnectionString = ConnectionString;
                string selectAllQuery = Query;
                MySqlCommand selectcmd = new MySqlCommand(selectAllQuery, connection);

                //Clear out DataGridView control
                dgvDbOutput.DataSource = null;

                System.Data.DataTable dt = new System.Data.DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(selectcmd);
                adapter.Fill(dt);
                dgvDbOutput.DataSource = dt;
                //dgvDbOutput.Update();
                message = "Your command has executed successfully.";
            }
            catch (Exception ex)
            {
                message = "Your command failed.\n\n" + ex.Message;
            }
            finally
            {
                MessageBox.Show(message, "Result:");
            }
        }

        private void btnImportPplData_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (pplData.Count > 0)
                {
                    List<List<string[]>> ValidatedRows = SqlSt.ValidatePPLData(pplData, SqlSt.PplDbRows);

                    SqlSt.AddPplDataToDatabase(ValidatedRows[0]);
                    DisplayAllDbRecords();
                    if (ValidatedRows[1].Count > 0)
                    {
                        MessageBox.Show("Errors present in the data prevented " + ValidatedRows[1].Count + " rows from being added to the database.\n\nWould you like to attempt to fix the data?");
                    }
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

        private void cbxSavedCommands_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] queryArray = savedQueryList.ToArray();
            int selectIndex = cbxSavedCommands.SelectedIndex;
            txtSqlTestString.Text = queryArray[selectIndex];
            btnRunActiveCommand.Text = cbxSavedCommands.SelectedItem.ToString();
        }

        private void btnRunTestCommand_Click(object sender, EventArgs e)
        {
            if (txtSqlTestString.Text != "")
            {
                RunActiveCommand(txtSqlTestString.Text);
            }
        }

        private void quitApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSaveCommand_Click(object sender, EventArgs e)
        {
            //do this
        }
    }
}
