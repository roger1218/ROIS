﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;

namespace CrewManagerDemo
{
    class PairingListParser
    {
        private string path;

        public PairingListParser(string filename)
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
            dataTable.Columns.Add("Pairing_Number");
            dataTable.Columns.Add("Pairing_Base");
            dataTable.Columns.Add("Pairing_Leg");
            dataTable.Columns.Add("Pairing_Note1");
            dataTable.Columns.Add("Pairing_Start_Time");
            dataTable.Columns.Add("Pairing_End_Time");
            dataTable.Columns.Add("Pairing_Note2");
            dataTable.Columns.Add("Pairing_Note3");
            dataTable.Columns.Add("Pairing_Note4");

            while ((line = reader.ReadLine()) != null)
            {
                bool isSuccess = CreateDataRow(ref dataTable, line);
                if (!isSuccess)
                {
                    throw new FileLoadException("There are some data unconsistent in your file.");
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
            get { return new char[] { '\t' }; }
        }

        private bool CreateDataRow(ref DataTable dt, string line)
        {
            DataRow dr = dt.NewRow();

            string[] fileds = line.Split(FormatSplit, StringSplitOptions.RemoveEmptyEntries);

            if (fileds.Length == 0 || fileds.Length > dt.Columns.Count)
            {
                return false;
            }

            //StringBuilder startTime = new StringBuilder();
            //StringBuilder endTime = new StringBuilder();
            for (int i = 0; i < fileds.Length; i++)
            {
                //if (i < 4)
                //{
                //    dr[i] = fileds[i];
                //}
                //else if (i > 3 && i < 9)
                //{
                //    startTime.Append(fileds[i].ToString());
                //}
                //else if (i > 8  && i < 14)
                //{
                //    startTime.Append(fileds[i].ToString());
                //}
                //else
                //{
                //    dr[i - 9] = fileds[i];
                //}

                if (i == 4 || i == 5)
                {
                    string temp = fileds[i].Replace(" ", string.Empty);
                    dr[i] = DateTime.Parse(temp);
                }
                else
                    dr[i] = fileds[i];

            }

            dt.Rows.Add(dr);
            return true;
        }
    }
}

