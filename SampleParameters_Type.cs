 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;


namespace TestWorks
{
    class SampleParameters_Type
    {
        public void SetTypeParameter(Document doc)
        {
            Element e = FindElementByName(doc, typeof(WallType), "ApollosWall200");
            WallType wallType = doc.GetElement(e.Id) as WallType;

            try
            {
                using(Transaction t = new Transaction(doc, "Set Type"))
                {
                    t.Start();
                    Parameter p = wallType.get_Parameter(BuiltInParameter.ALL_MODEL_COST);
                    p.Set(500);
                    t.Commit();
                }
            }
            catch(Exception ex)
            {
                TaskDialog.Show("error", ex.Message);
            }
        }
        public Element FindElementByName(Document doc, Type targetType, string targetName)
        {
            return new FilteredElementCollector(doc)
                .OfClass(targetType)
                .FirstOrDefault<Element>(e => e.Name.Equals(targetName));
        }
    }
}
