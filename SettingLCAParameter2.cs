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
    public class SettingLCAParameter2
    {
        public void CreateSampleSharedParameters2(Document doc, Application app)
        {
            Category category = doc.Settings.Categories.get_Item(BuiltInCategory.OST_Walls);
            CategorySet categorySet = app.Create.NewCategorySet();
            categorySet.Insert(category);

            string originalFile = app.SharedParametersFilename;

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
