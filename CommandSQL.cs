using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI.Selection;

namespace TestWorks
{
    [Transaction(TransactionMode.Manual)]
    public class CommandSQL : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            DatabaseSample databaseObject = new DatabaseSample();
            ////reading from the database

            string query = "SELECT * FROM OkoData3 WHERE Name_en LIKE '%concrete% C20/25' AND Konformität == 'DIN EN 15804' AND Modul == 'A1-A3'";
            //string query2 = "SELECT GWP FROM OkoData3 WHERE Name_en LIKE '%concrete% C20/25' AND Konformität == 'DIN EN 15804' AND Modul == 'A1-A3'";
            SQLiteCommand myCommand = new SQLiteCommand(query, databaseObject.myConnection);
            //SQLiteCommand myCommand2 = new SQLiteCommand(query2, databaseObject.myConnection);

            databaseObject.OpenConnection();
            SQLiteDataReader result = myCommand.ExecuteReader();
            //SQLiteDataReader result2 = myCommand2.ExecuteReader();

            if (result.HasRows)
            {
                while (result.Read())
                {
                    Console.WriteLine(result.GetInt32(0));
                    //var queryValue = result.GetInt32(0);
                    //TaskDialog.Show("Query response", queryValue.ToString());
                }
            }
            //if (result2.HasRows)
            //{
            //    while (result2.Read())
            //    {
            //        Console.WriteLine(result2.GetFloat(0));

            //    }
            //}

            databaseObject.CloseConnection();
            Console.ReadKey();

            return Result.Succeeded;
        }

    }
}
