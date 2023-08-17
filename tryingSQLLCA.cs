using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI.Selection;
using System.Data.SQLite;
using System.Data.SqlClient;

namespace TestWorks
{
    //This is the Transaction mode set to Manual
    [Transaction(TransactionMode.Manual)]

    //This is the Setting LCA Parameter value public class IExternalCommand
    public class tryingSQLLCA : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            SqlLcaDbConnect2 sqlConnection = new SqlLcaDbConnect2();
            sqlConnection.ConnecDB();

            SampleCollector sc = new SampleCollector();
            List<Wall> ListWalls_Class = sc.GetWalls_Class(doc);
            foreach (Wall wall in ListWalls_Class)
            {
                using (Transaction t = new Transaction(doc, "setting LCA Value"))
                {
                    t.Start();
                    try
                    {
                        string getQuery =
                            $"SELECT GWP FROM OkoData WHERE Name_en LIKE '%Concrete C20/25%' AND Konformität = 'DIN EN 15804' AND Modul = 'A1-A3'";

                        SqlCommand command = sqlConnection.Query(getQuery);
                        SqlDataReader reader = command.ExecuteReader();
                        
                        while (reader.Read())
                        {
                            var gwpIndicator = reader["GWP"];
                            //double gwp = Convert.ToDouble(gwpIndicator);
                            var gwp = gwpIndicator;
                            var lcaParameter = wall.GetParameters("ClimateChangePerUnit");
                            if (lcaParameter != null)
                            {
                                foreach (Parameter parameter in lcaParameter)
                                {
                                    parameter.Set((double)gwp);
                                }

                            }
                            else
                            {

                                string message2 = string.Format("NULL");
                                TaskDialog.Show("There is no value", message2);
                            }
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        TaskDialog.Show("Error", ex.ToString());
                    }

                    t.Commit();
                }
            }
            return Result.Succeeded;
        }

    }
}