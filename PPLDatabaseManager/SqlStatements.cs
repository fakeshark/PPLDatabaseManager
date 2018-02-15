using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PPLDatabaseManager
{
    class SqlStatements
    {
        //ALTER TABLE tablename AUTO_INCREMENT = 1
        public string[] PplDbRows = { "PCCN", "PLISN", "INDC", "CAGE", "PN", "RNCC", "RNVC", "DAC", "PPSL", "EC", "NAME", "SL", "SLAC", "COG", "MCC", "FSC", "NIIN", "NSNSUFF", "UM", "UMPRICE", "UI", "UIPRICE", "CONV", "QUP", "SMR", "DMIL", "PLT", "HCI", "PSPC", "PMIC", "ADPEC", "NHA", "ORR", "QPA", "QPE", "MRRI", "MRRII", "MRRMOD", "TQR", "SAPLISN", "PRPLISN", "MAOT", "MAC", "NRTS", "UOC", "REFDES", "RDOC", "RDC", "SMCC", "PLCC", "SMIC", "AIC", "AICQTY", "MRU", "RMSS", "RISS", "RTLLQTY", "RSR", "OMTD", "FMTD", "HMTD", "SRAMTD", "DMTD", "CEDMTD", "CADMTD", "ORCT", "FRCT", "HRCT", "SRARCT", "DRCT", "CONRCT", "ORTD", "FRTD", "HRTD", "SRARTD", "DRTD", "DOP1", "DOP2", "CTIC", "AMC", "AMSC", "IMC", "RIP", "CHANGEAUTHORITY1", "IC", "SNFROM", "SNTO", "TIC", "RSPLISN", "QTYSHIPPED", "QTYPROCURED", "DCNUOC", "PRORATEDELIN", "PRORATEDQTY", "LCN", "ALTLCN", "REMARKS", "TMCODE", "FIGNUM", "ITEMNUM", "TMCHG", "TMIND", "QTYFIG", "WUCTMFGC", "BASISOFISSUE1", "BASISOFISSUE2", "CC", "INC", "LRU", "PROVNOM", "ALTCAGE1", "ALTPN1", "ALTRNCC1", "ALTRNVC1", "ALTDAC1", "ALTPPSL1", "ALTCAGE2", "ALTPN2", "ALTRNCC2", "ALTRNVC2", "ALTDAC2", "ALTPPSL2", "ALTCAGE3", "ALTPN3", "ALTRNCC3", "ALTRNVC3", "ALTDAC3", "ALTPPSL3", "ALTCAGE4", "ALTPN4", "ALTRNCC4", "ALTRNVC4", "ALTDAC4", "ALTPPSL4", "ALTCAGE5", "ALTPN5", "ALTRNCC5", "ALTRNVC5", "ALTDAC5", "ALTPPSL5", "ALTCAGE6", "ALTPN6", "ALTRNCC6", "ALTRNVC6", "ALTDAC6", "ALTPPSL6", "MATERIAL1", "MATERIAL2", "MATERIAL3", "MATERIAL4", "RBD", "SUPPNOMEN1", "AELA", "AELB", "AELC", "AELD", "AELE", "AELF", "AELG", "AELH", "SUPPNOMEN2", "AFC2", "AFCQTY2", "ANC2", "AOC2", "AOCQTY2", "LLTIL1", "PPL1", "SFPPL1", "CBIL1", "RIL1", "ISIL1", "PCL1", "TTEL1", "SCPL1", "DCN1", "ARF", "LLTIL2", "PPL2", "SFPPL2", "CBIL2", "RIL2", "ISIL2", "PCL2", "TTEL2", "SCPL2", "DCN2", "ACCCODE", "ALTNIINREL", "ALTNIIN", "ALTNIINREL2", "ALTNIIN2", "REFDES2", "RDOC2", "CHANGEAUTHORITY2", "IC2", "SNFROM2", "SNTO2", "TIC2", "RSPLISN2", "QTYSHIPPED2", "QTYPROCURED2", "DCNUOC2", "PRORATEDELIN2", "PRORATEDQTY2", "LCN2", "ALTLCN2", "LENGTH", "WIDTH", "HEIGHT", "WEIGHT", "temp1", "PARTID" };
        string appos = "'";
        string quote = "\"";
        string ConnectionString = "server=localhost; database=partsdb; user=root";

        public void AddPplDataToDatabase(List<string[]> rowArray)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                string insertPplRow = buildRowInsertQuery(rowArray);
                try
                {
                    connection.Open();
                    using (MySqlCommand myCmd = new MySqlCommand(insertPplRow, connection))
                    {
                        myCmd.CommandType = CommandType.Text;
                        int rowsInserted = myCmd.ExecuteNonQuery();
                        if (rowsInserted > 0)
                        {
                            MessageBox.Show("Import successful.\n\n" + rowsInserted + " rows have been added to the database.", "Import Results");
                        }
                        else
                        {
                            MessageBox.Show("No rows have been imported.\n\nThis may be that the data already exists in the database or is improperly formatted.", "Import Results");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error!\n\n" + ex.Message);
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
                connection.Close();
            }
        }

        private string buildRowInsertQuery(List<string[]> rowArray)
        {
            List<string> insertList = new List<string>();

            string insertStatement = "INSERT IGNORE INTO `ppl` ( ";
            StringBuilder sb = new StringBuilder(insertStatement);

            // append the database columns to the INSERT statement
            for (int i = 0; i < PplDbRows.Length; i++)
            {
                if (i != (PplDbRows.Length - 1))
                {
                    sb.Append(PplDbRows[i] + ", ");
                }
                else
                {
                    sb.Append(PplDbRows[i] + " ) VALUES ");
                }
            }

            // append the ppl data to the INSERT statement
            foreach (string[] row in rowArray)
            {
                sb.Append("( ");
                for (int i = 0; i < row.Length; i++)
                {
                    if (i != row.Length - 1)
                    {
                        if (row[i].Contains(appos) || row[i].Contains(quote))
                        {
                            char[] tempCellData = row[i].Trim().ToCharArray();
                            string formattedCellData = string.Empty;

                            for (int j = 0; j < tempCellData.Length; j++)
                            {
                                if (tempCellData[j] == '\'')
                                {
                                    formattedCellData += "\'";
                                }
                                else if (tempCellData[j] == '"')
                                {
                                    formattedCellData += "\\\"";
                                }
                                else
                                {
                                    formattedCellData += tempCellData[j].ToString();
                                }
                            }
                            sb.Append("\"" + formattedCellData + "\", ");
                        }
                        else
                        {
                            sb.Append("\"" + row[i].Trim() + "\", ");
                        }
                    }
                    else
                    {
                        if (row[i].Contains(appos) || row[i].Contains(quote))
                        {
                            char[] tempCellData = row[i].Trim().ToCharArray();
                            string formattedCellData = string.Empty;

                            for (int j = 0; j < tempCellData.Length; j++)
                            {
                                if (tempCellData[j] == '\'')
                                {
                                    formattedCellData += "\'";
                                }
                                else if (tempCellData[j] == '"')
                                {
                                    formattedCellData += "\\\"";
                                }
                                else
                                {
                                    formattedCellData += tempCellData[j].ToString();
                                }
                            }
                            sb.Append("\"" + formattedCellData + "\" ");
                        }
                        else
                        {
                            sb.Append("\"" + row[i].Trim() + "\" ");
                        }
                    }
                }
                sb.Append(" ), \n");
            }

            insertStatement = sb.ToString();
            insertStatement = insertStatement.Remove(insertStatement.Length - 3);
            insertStatement = insertStatement + ";";

            return insertStatement;
        }

        public void ValidatePPLData(List<string[]> data, string[] columnType)
        {           

            #region Size Validation
            foreach (string[] dataRow in data)
            {
                string errorMsg = string.Empty;

                for (int i = 0; i < dataRow.Length; i++)
                {
                    if (columnType[i] == "PCCN" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "PCCN is incorrect length.\n";
                    }
                    if (columnType[i] == "PLISN" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "PLISN is incorrect length.\n";
                    }
                    if (columnType[i] == "INDC" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "INDC is incorrect length.\n";
                    }
                    if (columnType[i] == "CAGE" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "CAGE is incorrect length.\n";
                    }

                    if (columnType[i] == "PN" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "PN is incorrect length.\n";
                    }

                    if (columnType[i] == "RNCC" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "RNCC is incorrect length.\n";
                    }

                    if (columnType[i] == "RNVC" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "RNVC is incorrect length.\n";
                    }

                    if (columnType[i] == "DAC" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "DAC is incorrect length.\n";
                    }

                    if (columnType[i] == "PPSL" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "PPSL is incorrect length.\n";
                    }

                    if (columnType[i] == "EC" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "EC is incorrect length.\n";
                    }

                    if (columnType[i] == "NAME" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "NAME is incorrect length.\n";
                    }

                    if (columnType[i] == "SL" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "SL is incorrect length.\n";
                    }

                    if (columnType[i] == "SLAC" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "SLAC is incorrect length.\n";
                    }

                    if (columnType[i] == "COG" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "COG is incorrect length.\n";
                    }

                    if (columnType[i] == "MCC" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "MCC is incorrect length.\n";
                    }

                    if (columnType[i] == "FSC" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "FSC is incorrect length.\n";
                    }

                    if (columnType[i] == "NIIN" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "NIIN is incorrect length.\n";
                    }

                    if (columnType[i] == "NSNSUFF" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "NSNSUFF is incorrect length.\n";
                    }

                    if (columnType[i] == "UM" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "UM is incorrect length.\n";
                    }

                    if (columnType[i] == "UMPRICE" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "UMPRICE is incorrect length.\n";
                    }

                    if (columnType[i] == "UI" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "UI is incorrect length.\n";
                    }

                    if (columnType[i] == "UIPRICE" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "UIPRICE is incorrect length.\n";
                    }

                    if (columnType[i] == "CONV" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "CONV is incorrect length.\n";
                    }

                    if (columnType[i] == "QUP" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "QUP is incorrect length.\n";
                    }

                    if (columnType[i] == "SMR" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "SMR is incorrect length.\n";
                    }

                    if (columnType[i] == "DMIL" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "DMIL is incorrect length.\n";
                    }

                    if (columnType[i] == "PLT" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "PLT is incorrect length.\n";
                    }

                    if (columnType[i] == "HCI" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "HCI is incorrect length.\n";
                    }

                    if (columnType[i] == "PSPC" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "PSPC is incorrect length.\n";
                    }

                    if (columnType[i] == "PMIC" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "PMIC is incorrect length.\n";
                    }

                    if (columnType[i] == "ADPEC" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ADPEC is incorrect length.\n";
                    }

                    if (columnType[i] == "NHA" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "NHA is incorrect length.\n";
                    }

                    if (columnType[i] == "ORR" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ORR is incorrect length.\n";
                    }

                    if (columnType[i] == "QPA" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "QPA is incorrect length.\n";
                    }

                    if (columnType[i] == "QPE" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "QPE is incorrect length.\n";
                    }

                    if (columnType[i] == "MRRI" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "MRRI is incorrect length.\n";
                    }

                    if (columnType[i] == "MRRII" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "MRRII is incorrect length.\n";
                    }

                    if (columnType[i] == "MRRMOD" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "MRRMOD is incorrect length.\n";
                    }
                    if (columnType[i] == "TQR" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "TQR is incorrect length.\n";
                    }
                    if (columnType[i] == "SAPLISN" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "SAPLISN is incorrect length.\n";
                    }
                    if (columnType[i] == "PRPLISN" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "PRPLISN is incorrect length.\n";
                    }
                    if (columnType[i] == "MAOT" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "MAOT is incorrect length.\n";
                    }
                    if (columnType[i] == "MAC" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "MAC is incorrect length.\n";
                    }
                    if (columnType[i] == "NRTS" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "NRTS is incorrect length.\n";
                    }
                    if (columnType[i] == "UOC" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "UOC is incorrect length.\n";
                    }
                    if (columnType[i] == "REFDES" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "REFDES is incorrect length.\n";
                    }
                    if (columnType[i] == "RDOC" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "RDOC is incorrect length.\n";
                    }
                    if (columnType[i] == "RDC" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "RDC is incorrect length.\n";
                    }
                    if (columnType[i] == "SMCC" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "SMCC is incorrect length.\n";
                    }
                    if (columnType[i] == "PLCC" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "PLCC is incorrect length.\n";
                    }
                    if (columnType[i] == "SMIC" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "SMIC is incorrect length.\n";
                    }
                    if (columnType[i] == "AIC" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "AIC is incorrect length.\n";
                    }
                    if (columnType[i] == "AICQTY" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "AICQTY is incorrect length.\n";
                    }
                    if (columnType[i] == "MRU" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "MRU is incorrect length.\n";
                    }
                    if (columnType[i] == "RMSS" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "RMSS is incorrect length.\n";
                    }
                    if (columnType[i] == "RISS" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "RISS is incorrect length.\n";
                    }
                    if (columnType[i] == "RTLLQTY" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "RTLLQTY is incorrect length.\n";
                    }
                    if (columnType[i] == "RSR" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "RSR is incorrect length.\n";
                    }
                    if (columnType[i] == "OMTD" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "OMTD is incorrect length.\n";
                    }
                    if (columnType[i] == "FMTD" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "FMTD is incorrect length.\n";
                    }
                    if (columnType[i] == "HMTD" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "HMTD is incorrect length.\n";
                    }
                    if (columnType[i] == "SRAMTD" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "SRAMTD is incorrect length.\n";
                    }
                    if (columnType[i] == "DMTD" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "DMTD is incorrect length.\n";
                    }
                    if (columnType[i] == "CEDMTD" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "CEDMTD is incorrect length.\n";
                    }
                    if (columnType[i] == "CADMTD" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "CADMTD is incorrect length.\n";
                    }
                    if (columnType[i] == "ORCT" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ORCT is incorrect length.\n";
                    }
                    if (columnType[i] == "FRCT" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "FRCT is incorrect length.\n";
                    }
                    if (columnType[i] == "HRCT" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "HRCT is incorrect length.\n";
                    }
                    if (columnType[i] == "SRARCT" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "SRARCT is incorrect length.\n";
                    }
                    if (columnType[i] == "DRCT" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "DRCT is incorrect length.\n";
                    }
                    if (columnType[i] == "CONRCT" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "CONRCT is incorrect length.\n";
                    }
                    if (columnType[i] == "ORTD" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ORTD is incorrect length.\n";
                    }
                    if (columnType[i] == "FRTD" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "FRTD is incorrect length.\n";
                    }
                    if (columnType[i] == "HRTD" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "HRTD is incorrect length.\n";
                    }
                    if (columnType[i] == "SRARTD" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "SRARTD is incorrect length.\n";
                    }
                    if (columnType[i] == "DRTD" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "DRTD is incorrect length.\n";
                    }
                    if (columnType[i] == "DOP1" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "DOP1 is incorrect length.\n";
                    }
                    if (columnType[i] == "DOP2" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "DOP2 is incorrect length.\n";
                    }
                    if (columnType[i] == "CTIC" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "CTIC is incorrect length.\n";
                    }
                    if (columnType[i] == "AMC" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "AMC is incorrect length.\n";
                    }
                    if (columnType[i] == "AMSC" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "AMSC is incorrect length.\n";
                    }
                    if (columnType[i] == "IMC" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "IMC is incorrect length.\n";
                    }
                    if (columnType[i] == "RIP" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "RIP is incorrect length.\n";
                    }
                    if (columnType[i] == "CHANGEAUTHORITY1" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "CHANGEAUTHORITY1 is incorrect length.\n";
                    }
                    if (columnType[i] == "IC" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "IC is incorrect length.\n";
                    }
                    if (columnType[i] == "SNFROM" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "SNFROM is incorrect length.\n";
                    }
                    if (columnType[i] == "SNTO" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "SNTO is incorrect length.\n";
                    }
                    if (columnType[i] == "TIC" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "TIC is incorrect length.\n";
                    }
                    if (columnType[i] == "RSPLISN" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "RSPLISN is incorrect length.\n";
                    }
                    if (columnType[i] == "QTYSHIPPED" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "QTYSHIPPED is incorrect length.\n";
                    }
                    if (columnType[i] == "QTYPROCURED" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "QTYPROCURED is incorrect length.\n";
                    }
                    if (columnType[i] == "DCNUOC" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "DCNUOC is incorrect length.\n";
                    }
                    if (columnType[i] == "PRORATEDELIN" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "PRORATEDELIN is incorrect length.\n";
                    }
                    if (columnType[i] == "PRORATEDQTY" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "PRORATEDQTY is incorrect length.\n";
                    }
                    if (columnType[i] == "LCN" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "LCN is incorrect length.\n";
                    }
                    if (columnType[i] == "ALTLCN" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ALTLCN is incorrect length.\n";
                    }
                    if (columnType[i] == "REMARKS" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "REMARKS is incorrect length.\n";
                    }
                    if (columnType[i] == "TMCODE" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "TMCODE is incorrect length.\n";
                    }
                    if (columnType[i] == "FIGNUM" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "FIGNUM is incorrect length.\n";
                    }
                    if (columnType[i] == "ITEMNUM" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ITEMNUM is incorrect length.\n";
                    }
                    if (columnType[i] == "TMCHG" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "TMCHG is incorrect length.\n";
                    }
                    if (columnType[i] == "TMIND" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "TMIND is incorrect length.\n";
                    }
                    if (columnType[i] == "QTYFIG" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "QTYFIG is incorrect length.\n";
                    }
                    if (columnType[i] == "WUCTMFGC" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "WUCTMFGC is incorrect length.\n";
                    }
                    if (columnType[i] == "BASISOFISSUE1" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "BASISOFISSUE1 is incorrect length.\n";
                    }
                    if (columnType[i] == "BASISOFISSUE2" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "BASISOFISSUE2 is incorrect length.\n";
                    }
                    if (columnType[i] == "CC" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "CC is incorrect length.\n";
                    }
                    if (columnType[i] == "INC" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "INC is incorrect length.\n";
                    }
                    if (columnType[i] == "LRU" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "LRU is incorrect length.\n";
                    }
                    if (columnType[i] == "PROVNOM" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 1000))
                    {
                        errorMsg += "PROVNOM is incorrect length.\n";
                    }
                    if (columnType[i] == "ALTCAGE1" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ALTCAGE1 is incorrect length.\n";
                    }
                    if (columnType[i] == "ALTPN1" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ALTPN1 is incorrect length.\n";
                    }
                    if (columnType[i] == "ALTRNCC1" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ALTRNCC1 is incorrect length.\n";
                    }
                    if (columnType[i] == "ALTRNVC1" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ALTRNVC1 is incorrect length.\n";
                    }
                    if (columnType[i] == "ALTDAC1" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ALTDAC1 is incorrect length.\n";
                    }
                    if (columnType[i] == "ALTPPSL1" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ALTPPSL1 is incorrect length.\n";
                    }
                    if (columnType[i] == "ALTCAGE2" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ALTCAGE2 is incorrect length.\n";
                    }
                    if (columnType[i] == "ALTPN2" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ALTPN2 is incorrect length.\n";
                    }
                    if (columnType[i] == "ALTRNCC2" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ALTRNCC2 is incorrect length.\n";
                    }
                    if (columnType[i] == "ALTRNVC2" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ALTRNVC2 is incorrect length.\n";
                    }
                    if (columnType[i] == "ALTDAC2" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ALTDAC2 is incorrect length.\n";
                    }
                    if (columnType[i] == "ALTPPSL2" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ALTPPSL2 is incorrect length.\n";
                    }
                    if (columnType[i] == "ALTCAGE3" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ALTCAGE3 is incorrect length.\n";
                    }
                    if (columnType[i] == "ALTPN3" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ALTPN3 is incorrect length.\n";
                    }
                    if (columnType[i] == "ALTRNCC3" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ALTRNCC3 is incorrect length.\n";
                    }
                    if (columnType[i] == "ALTRNVC3" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ALTRNVC3 is incorrect length.\n";
                    }
                    if (columnType[i] == "ALTDAC3" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ALTDAC3 is incorrect length.\n";
                    }
                    if (columnType[i] == "ALTPPSL3" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ALTPPSL3 is incorrect length.\n";
                    }
                    if (columnType[i] == "ALTCAGE4" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ALTCAGE4 is incorrect length.\n";
                    }
                    if (columnType[i] == "ALTPN4" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ALTPN4 is incorrect length.\n";
                    }
                    if (columnType[i] == "ALTRNCC4" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ALTRNCC4 is incorrect length.\n";
                    }
                    if (columnType[i] == "ALTRNVC4" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ALTRNVC4 is incorrect length.\n";
                    }
                    if (columnType[i] == "ALTDAC4" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ALTDAC4 is incorrect length.\n";
                    }
                    if (columnType[i] == "ALTPPSL4" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ALTPPSL4 is incorrect length.\n";
                    }
                    if (columnType[i] == "ALTCAGE5" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ALTCAGE5 is incorrect length.\n";
                    }
                    if (columnType[i] == "ALTPN5" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ALTPN5 is incorrect length.\n";
                    }
                    if (columnType[i] == "ALTRNCC5" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ALTRNCC5 is incorrect length.\n";
                    }
                    if (columnType[i] == "ALTRNVC5" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ALTRNVC5 is incorrect length.\n";
                    }
                    if (columnType[i] == "ALTDAC5" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ALTDAC5 is incorrect length.\n";
                    }
                    if (columnType[i] == "ALTPPSL5" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ALTPPSL5 is incorrect length.\n";
                    }
                    if (columnType[i] == "ALTCAGE6" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ALTCAGE6 is incorrect length.\n";
                    }
                    if (columnType[i] == "ALTPN6" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ALTPN6 is incorrect length.\n";
                    }
                    if (columnType[i] == "ALTRNCC6" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ALTRNCC6 is incorrect length.\n";
                    }
                    if (columnType[i] == "ALTRNVC6" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ALTRNVC6 is incorrect length.\n";
                    }
                    if (columnType[i] == "ALTDAC6" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ALTDAC6 is incorrect length.\n";
                    }
                    if (columnType[i] == "ALTPPSL6" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ALTPPSL6 is incorrect length.\n";
                    }
                    if (columnType[i] == "MATERIAL1" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "MATERIAL1 is incorrect length.\n";
                    }
                    if (columnType[i] == "MATERIAL2" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "MATERIAL2 is incorrect length.\n";
                    }
                    if (columnType[i] == "MATERIAL3" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "MATERIAL3 is incorrect length.\n";
                    }
                    if (columnType[i] == "MATERIAL4" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "MATERIAL4 is incorrect length.\n";
                    }
                    if (columnType[i] == "RBD" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "RBD is incorrect length.\n";
                    }
                    if (columnType[i] == "SUPPNOMEN1" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "SUPPNOMEN1 is incorrect length.\n";
                    }
                    if (columnType[i] == "AELA" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "AELA is incorrect length.\n";
                    }
                    if (columnType[i] == "AELB" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "AELB is incorrect length.\n";
                    }
                    if (columnType[i] == "AELC" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "AELC is incorrect length.\n";
                    }
                    if (columnType[i] == "AELD" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "AELD is incorrect length.\n";
                    }
                    if (columnType[i] == "AELE" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "AELE is incorrect length.\n";
                    }
                    if (columnType[i] == "AELF" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "AELF is incorrect length.\n";
                    }
                    if (columnType[i] == "AELG" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "AELG is incorrect length.\n";
                    }
                    if (columnType[i] == "AELH" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "AELH is incorrect length.\n";
                    }
                    if (columnType[i] == "SUPPNOMEN2" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "SUPPNOMEN2 is incorrect length.\n";
                    }
                    if (columnType[i] == "AFC2" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "AFC2 is incorrect length.\n";
                    }
                    if (columnType[i] == "AFCQTY2" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "AFCQTY2 is incorrect length.\n";
                    }
                    if (columnType[i] == "ANC2" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ANC2 is incorrect length.\n";
                    }
                    if (columnType[i] == "AOC2" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "AOC2 is incorrect length.\n";
                    }
                    if (columnType[i] == "AOCQTY2" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "AOCQTY2 is incorrect length.\n";
                    }
                    if (columnType[i] == "LLTIL1" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "LLTIL1 is incorrect length.\n";
                    }
                    if (columnType[i] == "PPL1" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "PPL1 is incorrect length.\n";
                    }
                    if (columnType[i] == "SFPPL1" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "SFPPL1 is incorrect length.\n";
                    }
                    if (columnType[i] == "CBIL1" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "CBIL1 is incorrect length.\n";
                    }
                    if (columnType[i] == "RIL1" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "RIL1 is incorrect length.\n";
                    }
                    if (columnType[i] == "ISIL1" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ISIL1 is incorrect length.\n";
                    }
                    if (columnType[i] == "PCL1" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "PCL1 is incorrect length.\n";
                    }
                    if (columnType[i] == "TTEL1" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "TTEL1 is incorrect length.\n";
                    }
                    if (columnType[i] == "SCPL1" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "SCPL1 is incorrect length.\n";
                    }
                    if (columnType[i] == "DCN1" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "DCN1 is incorrect length.\n";
                    }
                    if (columnType[i] == "ARF" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ARF is incorrect length.\n";
                    }
                    if (columnType[i] == "LLTIL2" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "LLTIL2 is incorrect length.\n";
                    }
                    if (columnType[i] == "PPL2" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "PPL2 is incorrect length.\n";
                    }
                    if (columnType[i] == "SFPPL2" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "SFPPL2 is incorrect length.\n";
                    }
                    if (columnType[i] == "CBIL2" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "CBIL2 is incorrect length.\n";
                    }
                    if (columnType[i] == "RIL2" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "RIL2 is incorrect length.\n";
                    }
                    if (columnType[i] == "ISIL2" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ISIL2 is incorrect length.\n";
                    }
                    if (columnType[i] == "PCL2" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "PCL2 is incorrect length.\n";
                    }
                    if (columnType[i] == "TTEL2" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "TTEL2 is incorrect length.\n";
                    }
                    if (columnType[i] == "SCPL2" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "SCPL2 is incorrect length.\n";
                    }
                    if (columnType[i] == "DCN2" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "DCN2 is incorrect length.\n";
                    }
                    if (columnType[i] == "ACCCODE" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ACCCODE is incorrect length.\n";
                    }
                    if (columnType[i] == "ALTNIINREL" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ALTNIINREL is incorrect length.\n";
                    }
                    if (columnType[i] == "ALTNIIN" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ALTNIIN is incorrect length.\n";
                    }
                    if (columnType[i] == "ALTNIINREL2" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ALTNIINREL2 is incorrect length.\n";
                    }
                    if (columnType[i] == "ALTNIIN2" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ALTNIIN2 is incorrect length.\n";
                    }
                    if (columnType[i] == "REFDES2" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "REFDES2 is incorrect length.\n";
                    }
                    if (columnType[i] == "RDOC2" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "RDOC2 is incorrect length.\n";
                    }
                    if (columnType[i] == "CHANGEAUTHORITY2" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "CHANGEAUTHORITY2 is incorrect length.\n";
                    }
                    if (columnType[i] == "IC2" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "IC2 is incorrect length.\n";
                    }
                    if (columnType[i] == "SNFROM2" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "SNFROM2 is incorrect length.\n";
                    }
                    if (columnType[i] == "SNTO2" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "SNTO2 is incorrect length.\n";
                    }
                    if (columnType[i] == "TIC2" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "TIC2 is incorrect length.\n";
                    }
                    if (columnType[i] == "RSPLISN2" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "RSPLISN2 is incorrect length.\n";
                    }
                    if (columnType[i] == "QTYSHIPPED2" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "QTYSHIPPED2 is incorrect length.\n";
                    }
                    if (columnType[i] == "QTYPROCURED2" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "QTYPROCURED2 is incorrect length.\n";
                    }
                    if (columnType[i] == "DCNUOC2" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "DCNUOC2 is incorrect length.\n";
                    }
                    if (columnType[i] == "PRORATEDELIN2" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "PRORATEDELIN2 is incorrect length.\n";
                    }
                    if (columnType[i] == "PRORATEDQTY2" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "PRORATEDQTY2 is incorrect length.\n";
                    }
                    if (columnType[i] == "LCN2" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "LCN2 is incorrect length.\n";
                    }
                    if (columnType[i] == "ALTLCN2" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "ALTLCN2 is incorrect length.\n";
                    }
                    if (columnType[i] == "LENGTH" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "LENGTH is incorrect length.\n";
                    }
                    if (columnType[i] == "WIDTH" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "WIDTH is incorrect length.\n";
                    }
                    if (columnType[i] == "HEIGHT" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "HEIGHT is incorrect length.\n";
                    }
                    if (columnType[i] == "WEIGHT" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "WEIGHT is incorrect length.\n";
                    }
                    if (columnType[i] == "temp1" && !CheckMinMaxLength(dataRow[i].Trim(), 0, 100))
                    {
                        errorMsg += "temp1 is incorrect length.\n";
                    }
                }
                if (errorMsg != "")
                {
                    MessageBox.Show("The following error(s) were found in your data: \n\n" + errorMsg, "Data Formating Error");
                }
            }
            #endregion

            #region Data Type Validation

            #endregion
        }

        private bool CheckMinMaxLength(string data, int min, int max)
        {
            bool correctSize = false;
            if (min <= max && max >= 1)
            {
                if (data.Length >= min && data.Length <= max)
                {
                    correctSize = true;
                }
            }
            return correctSize;
        }
    }
}
