using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;

namespace TestWorks
{
    public class SampleGetMaterial
    {
        public void GetWallLayerMaterial(Document doc, Wall wall)
        {
            
            // get WallType of wall
            WallType aWallType = wall.WallType;
            // Only Basic Wall has compoundStructure
            if (WallKind.Basic == aWallType.Kind)
            {

                // Get CompoundStructure
                CompoundStructure comStruct = aWallType.GetCompoundStructure();
                Categories allCategories = doc.Settings.Categories;

                // Get the category OST_Walls default Material; 
                // use if that layer's default Material is 
                Category wallCategory = allCategories.get_Item(BuiltInCategory.OST_Walls);
                Material wallMaterial = wallCategory.Material;

                foreach (CompoundStructureLayer structLayer in comStruct.GetLayers())
                {
                    Material layerMaterial = doc.GetElement(structLayer.MaterialId) as Material;

                    // If CompoundStructureLayer's Material is specified, use default
                    // Material of its Category
                    if (null == layerMaterial)
                    {
                        switch (structLayer.Function)
                        {
                            case MaterialFunctionAssignment.Finish1:
                                layerMaterial =
                                        allCategories.get_Item(BuiltInCategory.OST_WallsFinish1).Material;
                                break;
                            case MaterialFunctionAssignment.Finish2:
                                layerMaterial =
                                        allCategories.get_Item(BuiltInCategory.OST_WallsFinish2).Material;
                                break;
                            case MaterialFunctionAssignment.Membrane:
                                layerMaterial =
                                        allCategories.get_Item(BuiltInCategory.OST_WallsMembrane).Material;
                                break;
                            case MaterialFunctionAssignment.Structure:
                                layerMaterial =
                                        allCategories.get_Item(BuiltInCategory.OST_WallsStructure).Material;
                                break;
                            case MaterialFunctionAssignment.Substrate:
                                layerMaterial =
                                        allCategories.get_Item(BuiltInCategory.OST_WallsSubstrate).Material;
                                break;
                            case MaterialFunctionAssignment.Insulation:
                                layerMaterial =
                                        allCategories.get_Item(BuiltInCategory.OST_WallsInsulation).Material;
                                break;
                            default:
                                // It is impossible to reach here
                                break;
                        }
                        if (null == layerMaterial)
                        {
                            // CompoundStructureLayer's default Material is its SubCategory
                            layerMaterial = wallMaterial;
                        }
                    }
                    TaskDialog.Show("Revit", "Layer Material: " + layerMaterial);
                }
            }
        }
    }
}
