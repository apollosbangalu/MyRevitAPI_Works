using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using OfficeOpenXml;


namespace TestWorks
{
    [Transaction(TransactionMode.Manual)]
    public class CommandMaterialExportExcel : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            // Check if any element is selected
            if (uidoc.Selection.GetElementIds().Count == 0)
            {
                TaskDialog.Show("Error", "Please select a wall element.");
                return Result.Failed;
            }

            // Get the selected element (assumed to be a compound wall)
            ElementId selectedElementId = uidoc.Selection.GetElementIds().FirstOrDefault();
            Element selectedElement = doc.GetElement(selectedElementId);

            // Check if the selected element is a wall
            //if (!(selectedElement is Wall selectedWall))
            //{
            //    TaskDialog.Show("Error", "The selected element is not a wall.");
            //    return Result.Failed;
            //}

            // Check if the selected element is a wall
            if (selectedElement is Wall selectedWall)
            {
                // Get the compound structure of the wall type
                CompoundStructure compoundStructure = selectedWall.WallType.GetCompoundStructure();

                // Check if the compound structure is valid
                if (compoundStructure == null)
                {
                    TaskDialog.Show("Error", "The selected wall does not have a compound structure.");
                    return Result.Failed;
                }

                // Get the layers of the compound structure
                IList<CompoundStructureLayer> layers = compoundStructure.GetLayers();

                // Create a list to hold the data for each layer
                List<string[]> data = new List<string[]>();

                // Loop through the layers in the compound structure
                for (int i = 0; i < layers.Count; i++)
                {
                    // Get the material IDs of each layer
                    CompoundStructureLayer layer = layers[i];
                    Material material = doc.GetElement(layer.MaterialId) as Material;
                    
                    // Check if the material is valid
                    if (material == null)
                    {
                        TaskDialog.Show("Error", $"Material in layer {i + 1} is not found.");
                        continue;
                    }

                    // Get the property set element to get the structural asset ID
                    PropertySetElement propertySetElement = doc.GetElement(material.StructuralAssetId) as PropertySetElement;
                    
                    //Check if prppertset is valid
                    if (propertySetElement == null)
                    {
                        TaskDialog.Show("Error", $"Property set element for material in layer {i + 1} is not found.");
                        continue;
                    }
                    string assetlayerName = material.Name;
                    string assetUniqueId = material.UniqueId;
                    string assetTrueName = propertySetElement.Name;

                    IList<Parameter> parameters = propertySetElement.GetOrderedParameters();
                    for (int x = 0; x < parameters.Count; x++)
                    {
                        Parameter parameter = parameters[x];
                        InternalDefinition internalDefinition = parameter.Definition as InternalDefinition;
                        if (internalDefinition != null)
                        {
                            string parameterName = internalDefinition.Name;
                            string parameterValue = parameter.AsValueString();
                            //Add the layer data to the list
                            data.Add(new string[] {
                            $"Layer {i + 1}",
                            assetUniqueId,
                            assetlayerName,
                            assetTrueName,
                            parameterName,
                            parameterValue
                        });
                        }
                        
                    }
                }

                // Export the data to an Excel file
                using (ExcelPackage package = new ExcelPackage())
                {
                    // Add a new worksheet to the workbook
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Data");

                    // Set the headers for the worksheet
                    worksheet.Cells[1, 1].Value = "Layer";
                    worksheet.Cells[1, 2].Value = "Unique ID";
                    worksheet.Cells[1, 3].Value = "Material Name";
                    worksheet.Cells[1, 4].Value = "Material True Name";
                    worksheet.Cells[1, 5].Value = "Parameter Name";
                    worksheet.Cells[1, 6].Value = "Parameter Value";

                    // Set the data for the worksheet
                    for (int row = 2; row <= data.Count + 1; row++)
                    {
                        for (int col = 1; col <= data[row - 2].Length; col++)
                        {
                            worksheet.Cells[row, col].Value = data[row - 2][col - 1];
                        }
                    }

                    // Save the Excel file
                    string filePath = Path.Combine(Path.GetTempPath(), "Data.xlsx");
                    File.WriteAllBytes(filePath, package.GetAsByteArray());
                    System.Diagnostics.Process.Start(filePath);

                }
                TaskDialog.Show("Completed", $"Layer Detail ok");
            }

            return Result.Succeeded;
        }
    }
}
