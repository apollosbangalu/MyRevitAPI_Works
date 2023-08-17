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
    public class CommandMaterial : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

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

                // Create a list to store the data to be exported to Excel
                List<List<string>> data = new List<List<string>>(); //this is new part

                // Loop through the layers in the compound structure
                for (int i = 0; i < layers.Count; i++)
                {
                    using (Transaction t = new Transaction(doc))
                    {
                        t.Start("Get Material Layer");

                        // Create a list to store the data for each layer
                        List<string> layerData = new List<string>(); //this is new part

                        // Get the material IDs of each 
                        CompoundStructureLayer layer = layers[i];
                        Material material = doc.GetElement(layer.MaterialId) as Material;

                        //Get the property set element to get the structural asset ID
                        PropertySetElement propertySetElement = doc.GetElement(material.StructuralAssetId) as PropertySetElement;
                        string assetlayerName = material.Name;
                        string assetUniqueId = material.UniqueId;
                        string assetTrueName = propertySetElement.Name;

                        // Add the layer data to the list
                        layerData.Add((i + 1).ToString());
                        layerData.Add(assetUniqueId);
                        layerData.Add(assetlayerName);
                        layerData.Add(assetTrueName);//this is new part

                        IList<Parameter> parameters = propertySetElement.GetOrderedParameters();
                        for (int x = 0; x < parameters.Count; x++)
                        {
                            Parameter parameter = parameters[x];
                            
                            InternalDefinition internalDefinition = parameter.Definition as InternalDefinition;
                            if (internalDefinition != null)
                            {
                                string parameterName = internalDefinition.Name;
                                string parameterValue = parameter.AsValueString();
                                // Add the parameter data to the list
                                layerData.Add((x + 1).ToString());
                                layerData.Add(parameterName);
                                layerData.Add(parameterValue);//this is new part
                            }
                        }

                        // Add the layer data to the overall data list
                        data.Add(layerData); //this is new part
                        
                        t.Commit();
                    }  
                }
                //this is new part
                // Export the data to an Excel file
                using (ExcelPackage excelPackage = new ExcelPackage())
                {
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Compound Wall Layers");
                    for (int i = 0; i < data.Count; i++)
                    {
                        List<string> rowData = data[i];
                        for (int j = 0; j < rowData.Count; j++)
                        {
                            worksheet.Cells[i + 1, j + 1].Value = rowData[j];
                        }
                    }

                    // Save the Excel file to the desktop
                    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string filePath = Path.Combine(desktopPath, "Compound Wall Layers.xlsx");
                    FileInfo fileInfo = new FileInfo(filePath);
                    excelPackage.SaveAs(fileInfo);

                }//new part ends here
                TaskDialog.Show("Completed", $"Layer Detail ok");
            }
            return Result.Succeeded;
        }
    }
}
