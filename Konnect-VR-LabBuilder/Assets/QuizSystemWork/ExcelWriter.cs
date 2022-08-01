using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.IO;
using UnityEngine;

namespace ExcelReadWrite
{

    public class ExcelWriter
    {
        private string filePath = "";

        public bool chooseFile(string sentPath)
        {
            filePath = sentPath;
            return (!string.IsNullOrWhiteSpace(filePath));
        }

        public void writeCSV(DataTable[] dataTables, string[] headers)
        {
            StreamWriter sw = new StreamWriter(filePath, false);
            //write first table
            WriteTableToCSV(sw, dataTables[0]);

            //write additional tables for each header
            int i = 0;
            foreach (string header in headers)
            {
                sw.Write(header);
                sw.Write(sw.NewLine);
                WriteTableToCSV(sw, dataTables[i + 1]);
                i++;
            }

            sw.Close();
        }

        private void WriteTableToCSV(StreamWriter sw, DataTable dataTable)
        {
            //insert the headers
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                sw.Write(dataTable.Columns[i]);
                if (i < dataTable.Columns.Count - 1)
                {
                    sw.Write(", ");
                }
            }
            sw.Write(sw.NewLine);

            //write the data
            foreach (DataRow dr in dataTable.Rows)
            {
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(","))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
                    }
                    if (i < dataTable.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
        }
    }
}
