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
    public class CommandSetLCAParameterValue2 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;


            SampleCollector sc = new SampleCollector();
            List<Wall> ListWalls_Class = sc.GetWalls_Class(doc);
            foreach (Wall wall in ListWalls_Class)
            {
                using (Transaction t = new Transaction(doc, "setting LCA Value"))
                {
                    t.Start();
                    try
                    {
                        var lcaParameter = wall.GetParameters("ClimateChangePerUnit");
                        if (lcaParameter != null)
                        {
                            foreach (Parameter parameter in lcaParameter)
                            {
                                parameter.Set(0.0099);
                            }
                        }
                        else
                        {
                            string message2 = string.Format("NULL");
                            TaskDialog.Show("There is no value", message2);
                        }
                    }
                    catch (Exception ex)
                    {
                        TaskDialog.Show(message, ex.Message);
                    }
                    t.Commit();
                }
            }
            return Result.Succeeded;
        }
    }
}
