using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;

namespace CrewManagerDemo
{
    /// <summary>
    /// The class CSVHelper read the data from CSV file and store it in a DataTable class,
    /// and allows to return a column with index or column name.
    /// </summary>
    class CSVHelper
    {
        private string path;

        public CSVHelper(string filename)
        {
            path = filename;
        }

        public DataTable Read()
        {
            FileInfo fi = new FileInfo(path);
            if (fi == null || !fi.Exists) return null;

            StreamReader reader = new StreamReader(path);

            string line = string.Empty;
            int lineNumber = 0;

            DataTable dataTable = new DataTable();

            while ((line = reader.ReadLine()) != null)
            {
                if (lineNumber == 0)
                {
                    dataTable = CreateDataTable(line);
                    if (dataTable.Columns.Count == 0) return null;
                }
                else
                {
                    bool isSuccess = CreateDataRow(ref dataTable, line);
                    if (!isSuccess)
                    {
                        throw new FileLoadException("There are some data unconsistent in your file.");
                    }
                }
                lineNumber++;
            }

            return dataTable;
        }

        public List<double> GetColumnWithName(string columnName, DataTable dataTable)
        {
            List<double> list = new List<double>();

            var index = dataTable.Columns.IndexOf(columnName);
            list = GetColumnWithIndex(index, dataTable);

            return list;
        }

        public List<double> GetColumnWithIndex(int index, DataTable dt)
        {
            List<double> list = new List<double>();

            foreach (DataRow dr in dt.Rows)
            {
                var s = dr[index].ToString();
                if (string.Compare(s, "") == 0)
                {
                    break;
                }
                double value = Convert.ToDouble(s);
                list.Add(value);
            }

            return list;
        }

        private char[] FormatSplit
        {
            get { return new char[] { ',' }; }
        }

        private DataTable CreateDataTable(string line)
        {
            DataTable dataTable = new DataTable();
            foreach (string field in
                line.Split(FormatSplit, StringSplitOptions.None))
            {
                dataTable.Columns.Add(field);
            }
            return dataTable;
        }

        private bool CreateDataRow(ref DataTable dt, string line)
        {
            DataRow dr = dt.NewRow();

            string[] fileds = line.Split(FormatSplit, StringSplitOptions.None);

            if (fileds.Length == 0 || fileds.Length > dt.Columns.Count)
            {
                return false;
            }

            for (int i = 0; i < fileds.Length; i++)
            {
                dr[i] = fileds[i];
            }

            dt.Rows.Add(dr);
            return true;
        }
    }
}

