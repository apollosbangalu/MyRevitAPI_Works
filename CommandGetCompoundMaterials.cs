using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;

namespace TestWorks
{
    [Transaction(TransactionMode.Manual)]
    public class CommandGetCompoundMaterials : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;




            #region GET MATERIAL ISSUES

            //SampleGetMaterial sampleGetMaterial = new SampleGetMaterial();
            //sampleGetMaterial.GetWallLayerMaterial(doc, wall);
            //SampleParameters_Type typeParameter = new SampleParameters_Type();
            //typeParameter.SetTypeParameter(doc);

            #endregion

            return Result.Succeeded;
        }

    }
}
