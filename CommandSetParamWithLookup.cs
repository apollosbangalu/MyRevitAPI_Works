using System;
using System.Data;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.UI.Selection;

namespace TestWorks
{
    [Transaction(TransactionMode.Manual)]
    public class CommandSetParamWithLookup : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            Element e = SelectElement(uidoc, doc);
            Parameter parameter = e.LookupParameter("Comments");
            using(Transaction t = new Transaction (doc, "parameter"))
            {
                t.Start("param");
                try
                {
                    parameter.Set("test");
                }
                catch { }
                t.Commit();
            }
           

            return Result.Succeeded;
        }

        public Element SelectElement(UIDocument uidoc, Document doc)
        {
            Reference reference = uidoc.Selection.PickObject(ObjectType.Element);
            Element element = uidoc.Document.GetElement(reference);
            return element;
        }
    }
}
