using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace TestWorks
{
    [Transaction(TransactionMode.Manual)]
    public class CommandMaterialExportCSV : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            // Get the current active document
            //Document doc = ActiveUIDocument.Document;

            // Get the selected element (assumed to be a compound wall)
            ElementId selectedElementId = uidoc.Selection.GetElementIds().FirstOrDefault();
            Element selectedElement = doc.GetElement(selectedElementId);

            // Check if the selected element is a wall
            if (selectedElement is Wall selectedWall)
            {
                // Get the compound structure of the wall type
                CompoundStructure compoundStructure = selectedWall.WallType.GetCompoundStructure();

                // Get the layers of the compound structure
                IList<CompoundStructureLayer> layers = compoundStructure.GetLayers();

                // Define the output CSV file path and create the file
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "MaterialLayers.csv");
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    // Write the header row to the CSV file
                    sw.WriteLine("Layer,Material,Property Name,Property Value");

                    // Loop through the layers in the compound structure
                    for (int i = 0; i < layers.Count; i++)
                    {
                        // Get the material IDs of each layer
                        CompoundStructureLayer layer = layers[i];
                        Material material = doc.GetElement(layer.MaterialId) as Material;

                        //Get the then get the property set element to get the structural asset ID
                        PropertySetElement propertySetElement = doc.GetElement(material.StructuralAssetId) as PropertySetElement;

                        // Loop through the parameters in the material's structural asset property set
                        IList<Parameter> parameters = propertySetElement.GetOrderedParameters();
                        for (int x = 0; x < parameters.Count; x++)
                        {
                            Parameter parameter = parameters[x];
                            InternalDefinition internalDefinition = parameter.Definition as InternalDefinition;
                            if (internalDefinition != null)
                            {
                                string parameterName = internalDefinition.Name;
                                string parameterValue = parameter.AsValueString();

                                // Write the data to the CSV file
                                sw.WriteLine($"{i + 1},{material.Name},{parameterName},{parameterValue}");
                            }
                            
                        }
                    }
                }

                // Show a message box when the export is complete
                TaskDialog.Show("Export Material Layers", $"The material layer data has been exported to {filePath}");
            }

            return Result.Succeeded;
        }
    }
}
