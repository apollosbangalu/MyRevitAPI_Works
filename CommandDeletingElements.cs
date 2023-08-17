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
    public class CommandDeletingElements : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            #region DELETING ELEMENTS
            SampleDeleteElements d = new SampleDeleteElements();
            d.DeleteElement(doc);
            d.DeleteElements(doc);
            #endregion

            return Result.Succeeded;
        }
    }
}
