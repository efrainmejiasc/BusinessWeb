using BusinessWebSite.Engine.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace BusinessWebSite.Engine
{
    public class EngineRead: IEngineRead
    {
        public  bool ReadXlsx  (string path)
        {
            bool result = false;
            try { 
                string excelConnectionString = @"Provider='Microsoft.ACE.OLEDB.12.0';Data Source='" + path + "';Extended Properties='Excel 12.0 Xml;IMEX=1'";
                OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                excelConnection.Open();
                string tableName = excelConnection.GetSchema("Tables").Rows[0]["TABLE_NAME"].ToString();
                excelConnection.Close();
                DataTable dataTable = new DataTable();
                OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM  [" + tableName + "]", excelConnection);
                adapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                string err = ex.ToString();
            }
            return result;
        }
    }
}