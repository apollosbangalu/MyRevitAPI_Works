using System;
using System.Reflection;
using System.IO;
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
    class DatabaseSample //: IExternalCommand
    {
        //public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        //{
        //    UIApplication uiapp = commandData.Application;
        //    UIDocument uidoc = uiapp.ActiveUIDocument;
        //    Application app = uiapp.Application;
        //    Document doc = uidoc.Document;

        //    return Result.Succeeded;
        //}



        public SQLiteConnection myConnection;

        //string dataFile = @"URI=file:C:\Users\MILA\source\repos\SQLiteLesson\Resources\OurDataBase4C.db";
        public DatabaseSample()
        {
            //myConnection = new SQLiteConnection("Data Source=C:/OurDataBase.db; Version=3;");
            //myConnection = new SQLiteConnection("Data Source=C:/Users/MILA/PycharmProjects/pythonProject2/OurDataBase.db; Version=3;");
            myConnection = new SQLiteConnection("Data Source=C:/Users/MILA/source/repos/TestWorks/Resources/OurDataBase4C.db; Version=3;");
            if (myConnection != null)
            {
                string information = "my connection is not NULL";
                TaskDialog.Show("Connection State", information);
            }
            else
            {
                string information2 = "my connection is NULL";
                TaskDialog.Show("Connection Error", information2);
            }
            //if (File.Exists("./OurDataBase.db"))
            //{
            //    Console.WriteLine("Database file found");
            //}
        }

        public void OpenConnection()
        {
            if (myConnection.State != System.Data.ConnectionState.Open)
            {
                myConnection.Open();
            }
        }

        public void CloseConnection()
        {
            if (myConnection.State != System.Data.ConnectionState.Closed)
            {
                myConnection.Close();
            }
        }
    }
}
