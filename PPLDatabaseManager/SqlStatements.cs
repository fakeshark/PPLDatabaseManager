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
        string[] PplDbRows = { "PCCN", "PLISN", "INDC", "CAGE", "PN", "RNCC", "RNVC", "DAC", "PPSL", "EC", "NAME", "SL", "SLAC", "COG", "MCC", "FSC", "NIIN", "NSNSUFF", "UM", "UMPRICE", "UI", "UIPRICE", "CONV", "QUP", "SMR", "DMIL", "PLT", "HCI", "PSPC", "PMIC", "ADPEC", "NHA", "ORR", "QPA", "QPE", "MRRI", "MRRII", "MRRMOD", "TQR", "SAPLISN", "PRPLISN", "MAOT", "MAC", "NRTS", "UOC", "REFDES", "RDOC", "RDC", "SMCC", "PLCC", "SMIC", "AIC", "AICQTY", "MRU", "RMSS", "RISS", "RTLLQTY", "RSR", "OMTD", "FMTD", "HMTD", "SRAMTD", "DMTD", "CEDMTD", "CADMTD", "ORCT", "FRCT", "HRCT", "SRARCT", "DRCT", "CONRCT", "ORTD", "FRTD", "HRTD", "SRARTD", "DRTD", "DOP1", "DOP2", "CTIC", "AMC", "AMSC", "IMC", "RIP", "CHANGEAUTHORITY1", "IC", "SNFROM", "SNTO", "TIC", "RSPLISN", "QTYSHIPPED", "QTYPROCURED", "DCNUOC", "PRORATEDELIN", "PRORATEDQTY", "LCN", "ALTLCN", "REMARKS", "TMCODE", "FIGNUM", "ITEMNUM", "TMCHG", "TMIND", "QTYFIG", "WUCTMFGC", "BASISOFISSUE1", "BASISOFISSUE2", "CC", "INC", "LRU", "PROVNOM", "ALTCAGE1", "ALTPN1", "ALTRNCC1", "ALTRNVC1", "ALTDAC1", "ALTPPSL1", "ALTCAGE2", "ALTPN2", "ALTRNCC2", "ALTRNVC2", "ALTDAC2", "ALTPPSL2", "ALTCAGE3", "ALTPN3", "ALTRNCC3", "ALTRNVC3", "ALTDAC3", "ALTPPSL3", "ALTCAGE4", "ALTPN4", "ALTRNCC4", "ALTRNVC4", "ALTDAC4", "ALTPPSL4", "ALTCAGE5", "ALTPN5", "ALTRNCC5", "ALTRNVC5", "ALTDAC5", "ALTPPSL5", "ALTCAGE6", "ALTPN6", "ALTRNCC6", "ALTRNVC6", "ALTDAC6", "ALTPPSL6", "MATERIAL1", "MATERIAL2", "MATERIAL3", "MATERIAL4", "RBD", "SUPPNOMEN1", "AELA", "AELB", "AELC", "AELD", "AELE", "AELF", "AELG", "AELH", "SUPPNOMEN2", "AFC2", "AFCQTY2", "ANC2", "AOC2", "AOCQTY2", "LLTIL1", "PPL1", "SFPPL1", "CBIL1", "RIL1", "ISIL1", "PCL1", "TTEL1", "SCPL1", "DCN1", "ARF", "LLTIL2", "PPL2", "SFPPL2", "CBIL2", "RIL2", "ISIL2", "PCL2", "TTEL2", "SCPL2", "DCN2", "ACCCODE", "ALTNIINREL", "ALTNIIN", "ALTNIINREL2", "ALTNIIN2", "REFDES2", "RDOC2", "CHANGEAUTHORITY2", "IC2", "SNFROM2", "SNTO2", "TIC2", "RSPLISN2", "QTYSHIPPED2", "QTYPROCURED2", "DCNUOC2", "PRORATEDELIN2", "PRORATEDQTY2", "LCN2", "ALTLCN2", "LENGTH", "WIDTH", "HEIGHT", "WEIGHT", "temp1" };
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
                            MessageBox.Show("Import successful. " + rowsInserted + " rows have been added to the database.");
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

            string insertStatement = "INSERT INTO `ppl` ( ";
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

        private void ValidatePPLData(string[] data, string[] columnType)
        {
            string errorMsg = string.Empty;

            #region Size Validation

            for (int i = 0; i < data.Length; i++)
            {
                if (columnType[i] == "PCCN" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "PCCN is incorrect length.\n";
                }
                if (columnType[i] == "PLISN" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "PLISN is incorrect length.\n";
                }
                if (columnType[i] == "INDC" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "INDC is incorrect length.\n";
                }
                if (columnType[i] == "CAGE" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "CAGE is incorrect length.\n";
                }

                if (columnType[i] == "PN" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "PN is incorrect length.\n";
                }

                if (columnType[i] == "RNCC" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "RNCC is incorrect length.\n";
                }

                if (columnType[i] == "RNVC" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "RNVC is incorrect length.\n";
                }

                if (columnType[i] == "DAC" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "DAC is incorrect length.\n";
                }

                if (columnType[i] == "PPSL" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "PPSL is incorrect length.\n";
                }

                if (columnType[i] == "EC" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "EC is incorrect length.\n";
                }

                if (columnType[i] == "NAME" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "NAME is incorrect length.\n";
                }

                if (columnType[i] == "SL" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "SL is incorrect length.\n";
                }

                if (columnType[i] == "SLAC" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "SLAC is incorrect length.\n";
                }

                if (columnType[i] == "COG" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "COG is incorrect length.\n";
                }

                if (columnType[i] == "MCC" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "MCC is incorrect length.\n";
                }

                if (columnType[i] == "FSC" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "FSC is incorrect length.\n";
                }

                if (columnType[i] == "NIIN" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "NIIN is incorrect length.\n";
                }

                if (columnType[i] == "NSNSUFF" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "NSNSUFF is incorrect length.\n";
                }

                if (columnType[i] == "UM" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "UM is incorrect length.\n";
                }

                if (columnType[i] == "UMPRICE" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "UMPRICE is incorrect length.\n";
                }

                if (columnType[i] == "UI" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "UI is incorrect length.\n";
                }

                if (columnType[i] == "UIPRICE" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "UIPRICE is incorrect length.\n";
                }

                if (columnType[i] == "CONV" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "CONV is incorrect length.\n";
                }

                if (columnType[i] == "QUP" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "QUP is incorrect length.\n";
                }

                if (columnType[i] == "SMR" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "SMR is incorrect length.\n";
                }

                if (columnType[i] == "DMIL" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "DMIL is incorrect length.\n";
                }

                if (columnType[i] == "PLT" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "PLT is incorrect length.\n";
                }

                if (columnType[i] == "HCI" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "HCI is incorrect length.\n";
                }

                if (columnType[i] == "PSPC" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "PSPC is incorrect length.\n";
                }

                if (columnType[i] == "PMIC" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "PMIC is incorrect length.\n";
                }

                if (columnType[i] == "ADPEC" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ADPEC is incorrect length.\n";
                }

                if (columnType[i] == "NHA" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "NHA is incorrect length.\n";
                }

                if (columnType[i] == "ORR" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ORR is incorrect length.\n";
                }

                if (columnType[i] == "QPA" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "QPA is incorrect length.\n";
                }

                if (columnType[i] == "QPE" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "QPE is incorrect length.\n";
                }

                if (columnType[i] == "MRRI" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "MRRI is incorrect length.\n";
                }

                if (columnType[i] == "MRRII" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "MRRII is incorrect length.\n";
                }

                if (columnType[i] == "MRRMOD" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "MRRMOD is incorrect length.\n";
                }
                if (columnType[i] == "TQR" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "TQR is incorrect length.\n";
                }
                if (columnType[i] == "SAPLISN" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "SAPLISN is incorrect length.\n";
                }
                if (columnType[i] == "PRPLISN" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "PRPLISN is incorrect length.\n";
                }
                if (columnType[i] == "MAOT" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "MAOT is incorrect length.\n";
                }
                if (columnType[i] == "MAC" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "MAC is incorrect length.\n";
                }
                if (columnType[i] == "NRTS" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "NRTS is incorrect length.\n";
                }
                if (columnType[i] == "UOC" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "UOC is incorrect length.\n";
                }
                if (columnType[i] == "REFDES" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "REFDES is incorrect length.\n";
                }
                if (columnType[i] == "RDOC" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "RDOC is incorrect length.\n";
                }
                if (columnType[i] == "RDC" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "RDC is incorrect length.\n";
                }
                if (columnType[i] == "SMCC" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "SMCC is incorrect length.\n";
                }
                if (columnType[i] == "PLCC" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "PLCC is incorrect length.\n";
                }
                if (columnType[i] == "SMIC" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "SMIC is incorrect length.\n";
                }
                if (columnType[i] == "AIC" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "AIC is incorrect length.\n";
                }
                if (columnType[i] == "AICQTY" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "AICQTY is incorrect length.\n";
                }
                if (columnType[i] == "MRU" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "MRU is incorrect length.\n";
                }
                if (columnType[i] == "RMSS" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "RMSS is incorrect length.\n";
                }
                if (columnType[i] == "RISS" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "RISS is incorrect length.\n";
                }
                if (columnType[i] == "RTLLQTY" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "RTLLQTY is incorrect length.\n";
                }
                if (columnType[i] == "RSR" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "RSR is incorrect length.\n";
                }
                if (columnType[i] == "OMTD" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "OMTD is incorrect length.\n";
                }
                if (columnType[i] == "FMTD" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "FMTD is incorrect length.\n";
                }
                if (columnType[i] == "HMTD" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "HMTD is incorrect length.\n";
                }
                if (columnType[i] == "SRAMTD" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "SRAMTD is incorrect length.\n";
                }
                if (columnType[i] == "DMTD" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "DMTD is incorrect length.\n";
                }
                if (columnType[i] == "CEDMTD" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "CEDMTD is incorrect length.\n";
                }
                if (columnType[i] == "CADMTD" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "CADMTD is incorrect length.\n";
                }
                if (columnType[i] == "ORCT" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ORCT is incorrect length.\n";
                }
                if (columnType[i] == "FRCT" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "FRCT is incorrect length.\n";
                }
                if (columnType[i] == "HRCT" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "HRCT is incorrect length.\n";
                }
                if (columnType[i] == "SRARCT" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "SRARCT is incorrect length.\n";
                }
                if (columnType[i] == "DRCT" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "DRCT is incorrect length.\n";
                }
                if (columnType[i] == "CONRCT" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "CONRCT is incorrect length.\n";
                }
                if (columnType[i] == "ORTD" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ORTD is incorrect length.\n";
                }
                if (columnType[i] == "FRTD" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "FRTD is incorrect length.\n";
                }
                if (columnType[i] == "HRTD" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "HRTD is incorrect length.\n";
                }
                if (columnType[i] == "SRARTD" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "SRARTD is incorrect length.\n";
                }
                if (columnType[i] == "DRTD" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "DRTD is incorrect length.\n";
                }
                if (columnType[i] == "DOP1" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "DOP1 is incorrect length.\n";
                }
                if (columnType[i] == "DOP2" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "DOP2 is incorrect length.\n";
                }
                if (columnType[i] == "CTIC" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "CTIC is incorrect length.\n";
                }
                if (columnType[i] == "AMC" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "AMC is incorrect length.\n";
                }
                if (columnType[i] == "AMSC" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "AMSC is incorrect length.\n";
                }
                if (columnType[i] == "IMC" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "IMC is incorrect length.\n";
                }
                if (columnType[i] == "RIP" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "RIP is incorrect length.\n";
                }
                if (columnType[i] == "CHANGEAUTHORITY1" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "CHANGEAUTHORITY1 is incorrect length.\n";
                }
                if (columnType[i] == "IC" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "IC is incorrect length.\n";
                }
                if (columnType[i] == "SNFROM" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "SNFROM is incorrect length.\n";
                }
                if (columnType[i] == "SNTO" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "SNTO is incorrect length.\n";
                }
                if (columnType[i] == "TIC" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "TIC is incorrect length.\n";
                }
                if (columnType[i] == "RSPLISN" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "RSPLISN is incorrect length.\n";
                }
                if (columnType[i] == "QTYSHIPPED" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "QTYSHIPPED is incorrect length.\n";
                }
                if (columnType[i] == "QTYPROCURED" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "QTYPROCURED is incorrect length.\n";
                }
                if (columnType[i] == "DCNUOC" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "DCNUOC is incorrect length.\n";
                }
                if (columnType[i] == "PRORATEDELIN" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "PRORATEDELIN is incorrect length.\n";
                }
                if (columnType[i] == "PRORATEDQTY" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "PRORATEDQTY is incorrect length.\n";
                }
                if (columnType[i] == "LCN" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "LCN is incorrect length.\n";
                }
                if (columnType[i] == "ALTLCN" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ALTLCN is incorrect length.\n";
                }
                if (columnType[i] == "REMARKS" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "REMARKS is incorrect length.\n";
                }
                if (columnType[i] == "TMCODE" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "TMCODE is incorrect length.\n";
                }
                if (columnType[i] == "FIGNUM" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "FIGNUM is incorrect length.\n";
                }
                if (columnType[i] == "ITEMNUM" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ITEMNUM is incorrect length.\n";
                }
                if (columnType[i] == "TMCHG" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "TMCHG is incorrect length.\n";
                }
                if (columnType[i] == "TMIND" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "TMIND is incorrect length.\n";
                }
                if (columnType[i] == "QTYFIG" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "QTYFIG is incorrect length.\n";
                }
                if (columnType[i] == "WUCTMFGC" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "WUCTMFGC is incorrect length.\n";
                }
                if (columnType[i] == "BASISOFISSUE1" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "BASISOFISSUE1 is incorrect length.\n";
                }
                if (columnType[i] == "BASISOFISSUE2" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "BASISOFISSUE2 is incorrect length.\n";
                }
                if (columnType[i] == "CC" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "CC is incorrect length.\n";
                }
                if (columnType[i] == "INC" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "INC is incorrect length.\n";
                }
                if (columnType[i] == "LRU" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "LRU is incorrect length.\n";
                }
                if (columnType[i] == "PROVNOM" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "PROVNOM is incorrect length.\n";
                }
                if (columnType[i] == "ALTCAGE1" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ALTCAGE1 is incorrect length.\n";
                }
                if (columnType[i] == "ALTPN1" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ALTPN1 is incorrect length.\n";
                }
                if (columnType[i] == "ALTRNCC1" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ALTRNCC1 is incorrect length.\n";
                }
                if (columnType[i] == "ALTRNVC1" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ALTRNVC1 is incorrect length.\n";
                }
                if (columnType[i] == "ALTDAC1" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ALTDAC1 is incorrect length.\n";
                }
                if (columnType[i] == "ALTPPSL1" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ALTPPSL1 is incorrect length.\n";
                }
                if (columnType[i] == "ALTCAGE2" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ALTCAGE2 is incorrect length.\n";
                }
                if (columnType[i] == "ALTPN2" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ALTPN2 is incorrect length.\n";
                }
                if (columnType[i] == "ALTRNCC2" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ALTRNCC2 is incorrect length.\n";
                }
                if (columnType[i] == "ALTRNVC2" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ALTRNVC2 is incorrect length.\n";
                }
                if (columnType[i] == "ALTDAC2" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ALTDAC2 is incorrect length.\n";
                }
                if (columnType[i] == "ALTPPSL2" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ALTPPSL2 is incorrect length.\n";
                }
                if (columnType[i] == "ALTCAGE3" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ALTCAGE3 is incorrect length.\n";
                }
                if (columnType[i] == "ALTPN3" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ALTPN3 is incorrect length.\n";
                }
                if (columnType[i] == "ALTRNCC3" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ALTRNCC3 is incorrect length.\n";
                }
                if (columnType[i] == "ALTRNVC3" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ALTRNVC3 is incorrect length.\n";
                }
                if (columnType[i] == "ALTDAC3" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ALTDAC3 is incorrect length.\n";
                }
                if (columnType[i] == "ALTPPSL3" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ALTPPSL3 is incorrect length.\n";
                }
                if (columnType[i] == "ALTCAGE4" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ALTCAGE4 is incorrect length.\n";
                }
                if (columnType[i] == "ALTPN4" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ALTPN4 is incorrect length.\n";
                }
                if (columnType[i] == "ALTRNCC4" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ALTRNCC4 is incorrect length.\n";
                }
                if (columnType[i] == "ALTRNVC4" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ALTRNVC4 is incorrect length.\n";
                }
                if (columnType[i] == "ALTDAC4" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ALTDAC4 is incorrect length.\n";
                }
                if (columnType[i] == "ALTPPSL4" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ALTPPSL4 is incorrect length.\n";
                }
                if (columnType[i] == "ALTCAGE5" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ALTCAGE5 is incorrect length.\n";
                }
                if (columnType[i] == "ALTPN5" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ALTPN5 is incorrect length.\n";
                }
                if (columnType[i] == "ALTRNCC5" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ALTRNCC5 is incorrect length.\n";
                }
                if (columnType[i] == "ALTRNVC5" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ALTRNVC5 is incorrect length.\n";
                }
                if (columnType[i] == "ALTDAC5" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ALTDAC5 is incorrect length.\n";
                }
                if (columnType[i] == "ALTPPSL5" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ALTPPSL5 is incorrect length.\n";
                }
                if (columnType[i] == "ALTCAGE6" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ALTCAGE6 is incorrect length.\n";
                }
                if (columnType[i] == "ALTPN6" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ALTPN6 is incorrect length.\n";
                }
                if (columnType[i] == "ALTRNCC6" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ALTRNCC6 is incorrect length.\n";
                }
                if (columnType[i] == "ALTRNVC6" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ALTRNVC6 is incorrect length.\n";
                }
                if (columnType[i] == "ALTDAC6" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ALTDAC6 is incorrect length.\n";
                }
                if (columnType[i] == "ALTPPSL6" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ALTPPSL6 is incorrect length.\n";
                }
                if (columnType[i] == "MATERIAL1" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "MATERIAL1 is incorrect length.\n";
                }
                if (columnType[i] == "MATERIAL2" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "MATERIAL2 is incorrect length.\n";
                }
                if (columnType[i] == "MATERIAL3" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "MATERIAL3 is incorrect length.\n";
                }
                if (columnType[i] == "MATERIAL4" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "MATERIAL4 is incorrect length.\n";
                }
                if (columnType[i] == "RBD" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "RBD is incorrect length.\n";
                }
                if (columnType[i] == "SUPPNOMEN1" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "SUPPNOMEN1 is incorrect length.\n";
                }
                if (columnType[i] == "AELA" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "AELA is incorrect length.\n";
                }
                if (columnType[i] == "AELB" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "AELB is incorrect length.\n";
                }
                if (columnType[i] == "AELC" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "AELC is incorrect length.\n";
                }
                if (columnType[i] == "AELD" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "AELD is incorrect length.\n";
                }
                if (columnType[i] == "AELE" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "AELE is incorrect length.\n";
                }
                if (columnType[i] == "AELF" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "AELF is incorrect length.\n";
                }
                if (columnType[i] == "AELG" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "AELG is incorrect length.\n";
                }
                if (columnType[i] == "AELH" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "AELH is incorrect length.\n";
                }
                if (columnType[i] == "SUPPNOMEN2" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "SUPPNOMEN2 is incorrect length.\n";
                }
                if (columnType[i] == "AFC2" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "AFC2 is incorrect length.\n";
                }
                if (columnType[i] == "AFCQTY2" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "AFCQTY2 is incorrect length.\n";
                }
                if (columnType[i] == "ANC2" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ANC2 is incorrect length.\n";
                }
                if (columnType[i] == "AOC2" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "AOC2 is incorrect length.\n";
                }
                if (columnType[i] == "AOCQTY2" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "AOCQTY2 is incorrect length.\n";
                }
                if (columnType[i] == "LLTIL1" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "LLTIL1 is incorrect length.\n";
                }
                if (columnType[i] == "PPL1" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "PPL1 is incorrect length.\n";
                }
                if (columnType[i] == "SFPPL1" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "SFPPL1 is incorrect length.\n";
                }
                if (columnType[i] == "CBIL1" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "CBIL1 is incorrect length.\n";
                }
                if (columnType[i] == "RIL1" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "RIL1 is incorrect length.\n";
                }
                if (columnType[i] == "ISIL1" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ISIL1 is incorrect length.\n";
                }
                if (columnType[i] == "PCL1" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "PCL1 is incorrect length.\n";
                }
                if (columnType[i] == "TTEL1" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "TTEL1 is incorrect length.\n";
                }
                if (columnType[i] == "SCPL1" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "SCPL1 is incorrect length.\n";
                }
                if (columnType[i] == "DCN1" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "DCN1 is incorrect length.\n";
                }
                if (columnType[i] == "ARF" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ARF is incorrect length.\n";
                }
                if (columnType[i] == "LLTIL2" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "LLTIL2 is incorrect length.\n";
                }
                if (columnType[i] == "PPL2" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "PPL2 is incorrect length.\n";
                }
                if (columnType[i] == "SFPPL2" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "SFPPL2 is incorrect length.\n";
                }
                if (columnType[i] == "CBIL2" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "CBIL2 is incorrect length.\n";
                }
                if (columnType[i] == "RIL2" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "RIL2 is incorrect length.\n";
                }
                if (columnType[i] == "ISIL2" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ISIL2 is incorrect length.\n";
                }
                if (columnType[i] == "PCL2" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "PCL2 is incorrect length.\n";
                }
                if (columnType[i] == "TTEL2" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "TTEL2 is incorrect length.\n";
                }
                if (columnType[i] == "SCPL2" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "SCPL2 is incorrect length.\n";
                }
                if (columnType[i] == "DCN2" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "DCN2 is incorrect length.\n";
                }
                if (columnType[i] == "ACCCODE" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ACCCODE is incorrect length.\n";
                }
                if (columnType[i] == "ALTNIINREL" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ALTNIINREL is incorrect length.\n";
                }
                if (columnType[i] == "ALTNIIN" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ALTNIIN is incorrect length.\n";
                }
                if (columnType[i] == "ALTNIINREL2" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ALTNIINREL2 is incorrect length.\n";
                }
                if (columnType[i] == "ALTNIIN2" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ALTNIIN2 is incorrect length.\n";
                }
                if (columnType[i] == "REFDES2" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "REFDES2 is incorrect length.\n";
                }
                if (columnType[i] == "RDOC2" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "RDOC2 is incorrect length.\n";
                }
                if (columnType[i] == "CHANGEAUTHORITY2" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "CHANGEAUTHORITY2 is incorrect length.\n";
                }
                if (columnType[i] == "IC2" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "IC2 is incorrect length.\n";
                }
                if (columnType[i] == "SNFROM2" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "SNFROM2 is incorrect length.\n";
                }
                if (columnType[i] == "SNTO2" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "SNTO2 is incorrect length.\n";
                }
                if (columnType[i] == "TIC2" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "TIC2 is incorrect length.\n";
                }
                if (columnType[i] == "RSPLISN2" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "RSPLISN2 is incorrect length.\n";
                }
                if (columnType[i] == "QTYSHIPPED2" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "QTYSHIPPED2 is incorrect length.\n";
                }
                if (columnType[i] == "QTYPROCURED2" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "QTYPROCURED2 is incorrect length.\n";
                }
                if (columnType[i] == "DCNUOC2" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "DCNUOC2 is incorrect length.\n";
                }
                if (columnType[i] == "PRORATEDELIN2" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "PRORATEDELIN2 is incorrect length.\n";
                }
                if (columnType[i] == "PRORATEDQTY2" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "PRORATEDQTY2 is incorrect length.\n";
                }
                if (columnType[i] == "LCN2" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "LCN2 is incorrect length.\n";
                }
                if (columnType[i] == "ALTLCN2" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "ALTLCN2 is incorrect length.\n";
                }
                if (columnType[i] == "LENGTH" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "LENGTH is incorrect length.\n";
                }
                if (columnType[i] == "WIDTH" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "WIDTH is incorrect length.\n";
                }
                if (columnType[i] == "HEIGHT" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "HEIGHT is incorrect length.\n";
                }
                if (columnType[i] == "WEIGHT" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "WEIGHT is incorrect length.\n";
                }
                if (columnType[i] == "temp1" && !CheckMinMaxLength(data[i], 0, 6))
                {
                    errorMsg = "temp1 is incorrect length.\n";
                }
            }
            #endregion

            #region Data Type Validation
            
            #endregion

            if (errorMsg != "")
            {
                MessageBox.Show("The following error(s) were found in your data: \n\n" + errorMsg, "Data Formating Error");
            }

        }

        private bool CheckMinMaxLength(string data, int min, int max)
        {
            bool correctSize = false;
            if (min <= max)
            {
                if (data.Length >= min || data.Length <= max)
                {
                    correctSize = true;
                }
            }
            return correctSize;
        }
    }
}
