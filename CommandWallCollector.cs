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

namespace TestWorks
{
    [Transaction(TransactionMode.Manual)]
    public class CommandWallCollector : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            #region SAMPLE COLLECTORS FOR WALLS
            SampleCollector sc = new SampleCollector();

            List<Wall> ListWalls_Class = sc.GetWalls_Class(doc);
            List<Wall> ListWalls_ClassActiveView = sc.GetWalls_ClassActiveView(doc);
            List<Wall> ListWalls_Category = sc.GetWalls_Category(doc);
            Element Wall_ByNameLINQ = sc.GetWallByNameLINQ(doc, "ApollosWall200");
            Element Wall_ByNameLambda = sc.GetWallByNameLambda(doc, "ApollosWall200");

            TaskDialog.Show("Values", "---Walls using Class \n" + SB(ListWalls_Class).ToString()
                + "---Walls using Class ActiveView \n" + SB(ListWalls_ClassActiveView).ToString()
                + "---Walls using Category \n" + SB(ListWalls_Category).ToString()
                + "\n ---Walls using LINQ \n" + Wall_ByNameLINQ.Name + " " + Wall_ByNameLINQ.Id
                + "\n ---Walls using Lambda \n" + Wall_ByNameLambda.Name + " " + Wall_ByNameLambda.Id);
            #endregion


            #region SET TYPE PARAMETER ISSUES

            //SampleParameters_Type typeParameter = new SampleParameters_Type();
            //typeParameter.SetTypeParameter(doc);

            #endregion


            #region SHARED PARAMETER ISSUES

            //SampleCreateSharedParameter scsp = new SampleCreateSharedParameter();
            //scsp.CreateSampleSharedParameters(doc, app);

            #endregion


            #region DELETING ELEMENTS
            //SampleDeleteElements d = new SampleDeleteElements();
            //d.DeleteElement(doc);
            //d.DeleteElements(doc);
            #endregion

            return Result.Succeeded;
        }

        public StringBuilder SB(List<Wall> Walls)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Wall w in Walls)
                sb.AppendLine(w.Name + " " + w.Id + "\n");
            return sb;
        }
    }
}
