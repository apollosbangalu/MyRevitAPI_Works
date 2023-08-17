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
    //This is the Transaction mode set to Manual
    [Transaction(TransactionMode.Manual)]

    //This is the MaterialExample public class IExternalCommand
    public class CommandGetMaterialsLinq : IExternalCommand
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
                using (Transaction t = new Transaction(doc))
                {
                    t.Start("Get Material Details with GetElementIds");
                    try
                    {
                        //get the selected wall from Revit UI
                        //var wall = uidoc.Selection.GetElementIds().Select(x => doc.GetElement(x)).First();

                        //get all materialIds of the wall

                        var materialIds = wall.GetMaterialIds(false).ToList();

                        //we have a method for each element called GetMaterialArea & GetMaterialVolume
                        //each element in Revit knows what materials it consist of and how much area / volume
                        //NOTE there is need to create a method that would convert units
                        //(because the value of material areas/volume is in Feet)

                        var materialAreas = materialIds.Select(x => wall.GetMaterialArea(x, false)).ToList();
                        var materialVolumes = materialIds.Select(x => wall.GetMaterialVolume(x)).ToList();
                        var materialNames = materialIds.Select(x => doc.GetElement(x).Name).ToList();

                        double materialArea1 = UnitUtils.ConvertFromInternalUnits(materialAreas[0], UnitTypeId.SquareMeters);
                        double materialVolume1 = UnitUtils.ConvertFromInternalUnits(materialVolumes[0], UnitTypeId.CubicMeters);
                        
                        //double materialArea2 = UnitUtils.ConvertFromInternalUnits(materialAreas[1], UnitTypeId.SquareMeters);
                        //double materialVolume2 = UnitUtils.ConvertFromInternalUnits(materialVolumes[1], UnitTypeId.CubicMeters);



                        TaskDialog.Show("Values", "The Element Id is----" + wall.Id + "\n"
                                + "---Element Name is \n" + wall.Name + "\n"
                                + "---The Element Material Id is \n" + materialIds[0] + "\n"
                                + "---The Element Material Name is \n" + materialNames[0] + "\n"
                                + "---The Element Material Area is \n" + materialAreas[0] + "\n"
                                + "---The Element Material Area is \n" + materialArea1 + "\n"
                                +"---The Element Material Volume is \n" + materialVolumes[0] + "\n"
                                + "---The Element Material Volume is \n" + materialVolume1 + "\n"
                                //+ "---The Element Material Ids is \n" + materialIds[1] + "\n"
                                //+ "---The Element Material Name is \n" + materialNames[1] + "\n"
                                //+ "---The Element Material Area is \n" + materialAreas[0] + "\n"
                                //+ "---The Element Material Area is \n" + materialArea2 + "\n"
                                //+"---The Element Material Volume is \n" + materialVolumes[0] + "\n"
                                //+ "---The Element Material Volume is \n" + materialVolume2 + "\n"
                                );
                    }
                    catch (Exception ex)
                    {
                        message = ex.Message;
                        return Result.Failed;
                    }
                    t.Commit();
                }
            }

            //// I tried PickObject method
            //using (Transaction t = new Transaction(doc))
            //{
            //    t.Start("Get Material Details with PickObject");
            //    try
            //    {
            //        //I used Pick Object but it does not give me the Material Name.
            //        Reference reference = uidoc.Selection.PickObject(ObjectType.Element);
            //        Element element = uidoc.Document.GetElement(reference);
            //        var elementMaterials = element.GetMaterialIds(false).Select(x => doc.GetElement(x)).Cast<Material>();

            //        //the get_Parameter method and BuiltInParameter is only in relation to the wall as a Whole.
            //        var elementArea = element.get_Parameter(BuiltInParameter.HOST_AREA_COMPUTED).AsValueString();
            //        var elementVolume = element.get_Parameter(BuiltInParameter.HOST_VOLUME_COMPUTED).AsValueString();

            //        TaskDialog.Show("Values", "---Element Name is \n" + element.Name + "\n"
            //            + "---The Elemeent Area is \n" + elementArea + "\n"
            //            + "---The Element Volume is \n" + elementVolume + "\n"
            //            + "---The Element material is \n" + elementMaterials + "\n");
            //    }
            //    catch (Exception ex)
            //    {
            //        message = ex.Message;
            //        return Result.Failed;
            //    }
            //    t.Commit();
            //}
            return Result.Succeeded;
        }
    }
}
