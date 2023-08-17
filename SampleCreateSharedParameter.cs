using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI.Selection;

namespace TestWorks
{
    internal class SampleCreateSharedParameter
    {
        public void CreateSampleSharedParameters(Document doc, Application app)
        {
            Category category = doc.Settings.Categories.get_Item(BuiltInCategory.OST_Walls);
            CategorySet categorySet = app.Create.NewCategorySet();
            categorySet.Insert(category);

            string originalFile = app.SharedParametersFilename;
            
            //********ABSOLUTE FILE PATHS*************
            //string tempFile = @"C:\Program Files\Autodesk\Revit 2022\IFC Shared Parameters-RevitIFCBuiltIn_ALL Copy.txt";
            //string tempFile = @"C:\Users\MILA\source\repos\TestWorks\Resources\IFC Shared Parameters-RevitIFCBuiltIn_ALL Copy.txt";
            
            //***********EXAMPLE FORMAT FOR RELATIVE FILE PATH*********
            //var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            //var iconPath = Path.Combine(outPutDirectory, "Folder\\Img.jpg");
            //string icon_path = new Uri(iconPath).LocalPath;
            
            //*******SETTING A RELETIVE FILE PATH**********
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            var tempFilePath = Path.Combine(outPutDirectory, "Resources\\IFC Shared Parameters-RevitIFCBuiltIn_ALL Copy.txt");
            string tempFile = new Uri(tempFilePath).LocalPath;


            try
            {
                app.SharedParametersFilename = tempFile;

                DefinitionFile sharedParameterFile = app.OpenSharedParameterFile();

                foreach (DefinitionGroup dg in sharedParameterFile.Groups)
                {
                    if (dg.Name == "IFC Properties")
                    {
                        
                        ExternalDefinition externalDefinition1 = dg.Definitions.get_Item(
                            "FunctionalUnitReference") as ExternalDefinition;

                        using (Transaction t = new Transaction(doc))
                        {
                            t.Start("Add Shared Parameter");
                            //parameter binding
                            InstanceBinding newIB = app.Create.NewInstanceBinding(categorySet);

                            //parameter group to IFC properties
                            doc.ParameterBindings.Insert(externalDefinition1, newIB, BuiltInParameterGroup.PG_IFC);

                            t.Commit();
                        }

                        ExternalDefinition externalDefinition2 = dg.Definitions.get_Item(
                            "LifeCyclePhase") as ExternalDefinition;

                        using (Transaction t = new Transaction(doc))
                        {
                            t.Start("Add Shared Parameter");
                            //parameter binding
                            InstanceBinding newIB = app.Create.NewInstanceBinding(categorySet);

                            //parameter group to IFC properties
                            doc.ParameterBindings.Insert(externalDefinition2, newIB, BuiltInParameterGroup.PG_IFC);

                            t.Commit();
                        }

                        ExternalDefinition externalDefinition3 = dg.Definitions.get_Item(
                            "ExpectedServiceLife") as ExternalDefinition;

                        using (Transaction t = new Transaction(doc))
                        {
                            t.Start("Add Shared Parameter");
                            //parameter binding
                            InstanceBinding newIB = app.Create.NewInstanceBinding(categorySet);

                            //parameter group to IFC properties
                            doc.ParameterBindings.Insert(externalDefinition3, newIB, BuiltInParameterGroup.PG_IFC);

                            t.Commit();
                        }

                        ExternalDefinition externalDefinition4 = dg.Definitions.get_Item("ClimateChangePerUnit") as ExternalDefinition;

                        using (Transaction t = new Transaction(doc))
                        {
                            t.Start("Add Shared Parameter");
                            //parameter binding
                            InstanceBinding newIB = app.Create.NewInstanceBinding(categorySet);

                            //parameter group to IFC properties
                            doc.ParameterBindings.Insert(externalDefinition4, newIB, BuiltInParameterGroup.PG_IFC);

                            t.Commit(); 
                        }

                        ExternalDefinition externalDefinition5 = dg.Definitions.get_Item(
                            "AtmosphericAcidificationPerUnit") as ExternalDefinition;

                        using (Transaction t = new Transaction(doc))
                        {
                            t.Start("Add Shared Parameter");
                            //parameter binding
                            InstanceBinding newIB = app.Create.NewInstanceBinding(categorySet);

                            //parameter group to IFC properties
                            doc.ParameterBindings.Insert(externalDefinition5, newIB, BuiltInParameterGroup.PG_IFC);

                            t.Commit();
                        }

                        ExternalDefinition externalDefinition6 = dg.Definitions.get_Item(
                            "ResourceDepletionPerUnit") as ExternalDefinition;

                        using (Transaction t = new Transaction(doc))
                        {
                            t.Start("Add Shared Parameter");
                            //parameter binding
                            InstanceBinding newIB = app.Create.NewInstanceBinding(categorySet);

                            //parameter group to IFC properties
                            doc.ParameterBindings.Insert(externalDefinition6, newIB, BuiltInParameterGroup.PG_IFC);

                            t.Commit();
                        }
                        ExternalDefinition externalDefinition7 = dg.Definitions.get_Item(
                            "StratosphericOzoneLayerDestructionPerUnit") as ExternalDefinition;

                        using (Transaction t = new Transaction(doc))
                        {
                            t.Start("Add Shared Parameter");
                            //parameter binding
                            InstanceBinding newIB = app.Create.NewInstanceBinding(categorySet);

                            //parameter group to IFC properties
                            doc.ParameterBindings.Insert(externalDefinition7, newIB, BuiltInParameterGroup.PG_IFC);

                            t.Commit();
                        }
                        ExternalDefinition externalDefinition8 = dg.Definitions.get_Item(
                            "PhotochemicalOzoneFormationPerUnit") as ExternalDefinition;

                        using (Transaction t = new Transaction(doc))
                        {
                            t.Start("Add Shared Parameter");
                            //parameter binding
                            InstanceBinding newIB = app.Create.NewInstanceBinding(categorySet);

                            //parameter group to IFC properties
                            doc.ParameterBindings.Insert(externalDefinition8, newIB, BuiltInParameterGroup.PG_IFC);

                            t.Commit();
                        }
                        ExternalDefinition externalDefinition9 = dg.Definitions.get_Item(
                            "EutrophicationPerUnit") as ExternalDefinition;

                        using (Transaction t = new Transaction(doc))
                        {
                            t.Start("Add Shared Parameter");
                            //parameter binding
                            InstanceBinding newIB = app.Create.NewInstanceBinding(categorySet);

                            //parameter group to IFC properties
                            doc.ParameterBindings.Insert(externalDefinition9, newIB, BuiltInParameterGroup.PG_IFC);

                            t.Commit();
                        }
                    }
                }
            }
            catch { }
            finally
            {
                //reset to original file
                app.SharedParametersFilename = originalFile;
            }
        }
    }
}

