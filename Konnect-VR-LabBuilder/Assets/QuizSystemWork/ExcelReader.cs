using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

namespace ExcelReadWrite
{
    public class ExcelReader
    {
        [SerializeField] private string filePath = "";

        public bool chooseFile(string sentPath)
        {
            filePath = sentPath;
            return (!string.IsNullOrWhiteSpace(filePath));
        }

        public List<DataTable> ReadCSV(string[] tableSplit)
        {
            List<DataTable> dataTable = ConvertCSVToDataTable(tableSplit);
            return dataTable;
        }

        private List<DataTable> ConvertCSVToDataTable(string[] tableSplit)
        {
            StreamReader sr = new StreamReader(filePath);
            List<DataTable> tables = new List<DataTable>();
            foreach (string strSplit in tableSplit)
            {
                tables.Add(CreateTable(sr, strSplit));
            }
            tables.Add(CreateTable(sr, null));

            return tables;
        }

        private DataTable CreateTable(StreamReader sr, string strSplit)
        {
            bool splitDetected = false;

            string[] headers = sr.ReadLine().Split(',');
            DataTable dt = new DataTable();
            foreach (string header in headers)
            {
                dt.Columns.Add(header);
            }
            while (!sr.EndOfStream && !splitDetected)
            {
                string[] rows = Regex.Split(sr.ReadLine(), ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                if (strSplit == null || !rows[0].Equals(strSplit))
                { 
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i];
                    }
                    dt.Rows.Add(dr);
                } else
                {
                    splitDetected = true;
                }
            }
            return dt;
        }
    }
}
