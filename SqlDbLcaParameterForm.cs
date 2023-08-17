using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Autodesk.Revit.ApplicationServices;
using Application = Autodesk.Revit.ApplicationServices.Application;

namespace TestWorks
{
    public partial class SqlDbLcaParameterForm : System.Windows.Forms.Form
    {
        private Application App;
        Document Doc;
        SqlLcaDbConnect2 sqlConnection = new SqlLcaDbConnect2();


        public SqlDbLcaParameterForm(Document doc, Application app)
        {
            InitializeComponent();
            App = app;
            Doc = doc;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void Lca_Plugin_Form_Load(object sender, EventArgs e)
        {
            sqlConnection.ConnecDB();

        }


        private void btn_CreateLca_Indicator_Click(object sender, EventArgs e)
        {
            //This sets the LCA Parameter -THE ENVIRONMENTAL IMPACT INDICATORS PLACE HOLDERS
            Category category = Doc.Settings.Categories.get_Item(BuiltInCategory.OST_Walls);
            CategorySet categorySet = App.Create.NewCategorySet();
            categorySet.Insert(category);

            string originalFile = App.SharedParametersFilename;

            //*******SETTING A RELETIVE FILE PATH**********
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            var tempFilePath = Path.Combine(outPutDirectory, "Resources\\IFC Shared Parameters-RevitIFCBuiltIn_ALL Copy.txt");
            string tempFile = new Uri(tempFilePath).LocalPath;

            try
            {
                App.SharedParametersFilename = tempFile;

                DefinitionFile sharedParameterFile = App.OpenSharedParameterFile();

                foreach (DefinitionGroup dg in sharedParameterFile.Groups)
                {
                    if (dg.Name == "IFC Properties")
                    {
                        ExternalDefinition externalDefinition4 =
                            dg.Definitions.get_Item("ClimateChangePerUnit") as ExternalDefinition;

                        using (Transaction t = new Transaction(Doc))
                        {
                            t.Start("Add Shared Parameter");
                            //parameter binding
                            InstanceBinding newIB = App.Create.NewInstanceBinding(categorySet);

                            //parameter group to IFC properties
                            Doc.ParameterBindings.Insert(externalDefinition4, newIB, BuiltInParameterGroup.PG_IFC);

                            t.Commit();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Error", ex.ToString());
            }
            finally
            {
                //reset to original file
                App.SharedParametersFilename = originalFile;
            }

        }


        private void btn_SetLcaIndicatorValue_Click(object sender, EventArgs e)
        {
            //This sets the LCA Parameter -THE ENVIRONMENTAL IMPACT INDICATORS values as in the Okobaudat Database
            FilteredElementCollector collector = new FilteredElementCollector(Doc);
            ICollection<Element> Walls = collector.OfClass(typeof(Wall)).ToElements();
            //List<Wall> List_Walls = new List<Wall>();
            foreach (var wall in Walls)
            {
                using (Transaction t = new Transaction(Doc))
                {
                    t.Start("Get Material Details with GetElementIds");
                    try
                    {
                        //get all the lcaParameter list of the wall with the specified name 
                        var lcaParameter = wall.GetParameters("ClimateChangePerUnit");
                        
                        //get all materialIds of the wall
                        var materialIds = wall.GetMaterialIds(false).ToList();

                        //get all material names
                        var materialNames = materialIds.Select(x => Doc.GetElement(x).Name).ToList();

                        //show all material details
                        TaskDialog newDialog = new TaskDialog("Wall Material Detail");
                        newDialog.MainContent = "The Element Name is ---" + wall.Name + "\n"
                                                + "The Element Material Id is ---" + materialIds[0] + "\n"
                                                + "The Element Material Name is ---" + materialNames[0] + "\n"
                                                + "The Element Count lcaParameter - ClimateChangePerUnit is ---" + lcaParameter.Count + "\n"
                                                + "The Element Material ClimateChangePerUnit AsDouble is ---" + lcaParameter[0].AsDouble() + "\n"
                                                + "The Element Material ClimateChangePerUnit AsValueString is ---" + lcaParameter[0].AsValueString() + "\n"
                                                + "Would you like to proceed?";

                        newDialog.CommonButtons = TaskDialogCommonButtons.Ok | TaskDialogCommonButtons.Cancel;

                        if (newDialog.Show() == TaskDialogResult.Ok)
                        {
                            var materialName = materialNames[0];
                            materialName = materialName.Replace(",", "");
                            string getQuery =
                                $"SELECT GWP FROM OkoData WHERE Name_en LIKE '%{materialName}%' AND Konformität = 'DIN EN 15804' AND Modul = 'A1-A3'";
                            SqlCommand command = sqlConnection.Query(getQuery);
                            SqlDataReader reader = command.ExecuteReader();

                            while (reader.Read())
                            {
                                var gwpIndicator = reader["GWP"];
                                var gwp = gwpIndicator;

                                //Iterate through the lcaParameter list
                                if (lcaParameter != null)
                                {
                                    foreach (Parameter parameter in lcaParameter)
                                    {
                                        parameter.Set((double)gwp);
                                    }
                                }
                                else
                                {
                                    string message2 = string.Format("NULL");
                                    TaskDialog.Show("There is no value", message2);
                                }
                            }
                            reader.Close();
                            DialogResult = DialogResult.OK;
                            //Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        TaskDialog.Show( "Error", ex.ToString());
                    }
                    t.Commit();
                }
            }
        }

        private void btn_CreateLcaParamValue2_Click(object sender, EventArgs e)
        {
            //This sets the LCA Parameter -THE ENVIRONMENTAL IMPACT VALUES PLACE HOLDERS
            Category category = Doc.Settings.Categories.get_Item(BuiltInCategory.OST_Walls);
            CategorySet categorySet = App.Create.NewCategorySet();
            categorySet.Insert(category);

            string originalFile = App.SharedParametersFilename;

            //*******SETTING A RELETIVE FILE PATH**********
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            var tempFilePath = Path.Combine(outPutDirectory, "Resources\\IFC Shared Parameters-RevitIFCBuiltIn_ALL Copy.txt");
            string tempFile = new Uri(tempFilePath).LocalPath;

            try
            {
                App.SharedParametersFilename = tempFile;

                DefinitionFile sharedParameterFile = App.OpenSharedParameterFile();

                foreach (DefinitionGroup dg in sharedParameterFile.Groups)
                {
                    if (dg.Name == "IFC Properties")
                    {
                        ExternalDefinition externalDefinition4 =
                            dg.Definitions.get_Item("ClimateChange") as ExternalDefinition;

                        using (Transaction t = new Transaction(Doc))
                        {
                            t.Start("Add Shared Parameter");
                            //parameter binding
                            InstanceBinding newIB = App.Create.NewInstanceBinding(categorySet);

                            //parameter group to IFC properties
                            Doc.ParameterBindings.Insert(externalDefinition4, newIB, BuiltInParameterGroup.PG_IFC);

                            t.Commit();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Error", ex.ToString());
            }
            finally
            {
                //reset to original file
                App.SharedParametersFilename = originalFile;
            }

        }

        private void btn_SetLcaParamValue2_Click(object sender, EventArgs e)
        {
            //This sets the LCA Parameter -THE ENVIRONMENTAL IMPACT INDICATORS values as in the Okobaudat Database
            FilteredElementCollector collector = new FilteredElementCollector(Doc);
            ICollection<Element> Walls = collector.OfClass(typeof(Wall)).ToElements();
            //List<Wall> List_Walls = new List<Wall>();
            foreach (var wall in Walls)
            {
                using (Transaction t = new Transaction(Doc))
                {
                    t.Start("Get Material Details with GetElementIds");
                    try
                    {
                        //Parameter gwParameter = wall.LookupParameter("ClimateChange");

                        var lcaParameter = wall.GetParameters("ClimateChangePerUnit");
                        var lcaParameter2 = wall.GetParameters("ClimateChange");

                        //get all materialIds of the wall
                        var materialIds = wall.GetMaterialIds(false).ToList();

                        //get all material names
                        var materialNames = materialIds.Select(x => Doc.GetElement(x).Name).ToList();

                        //get all material volume and area details
                        var materialAreas = materialIds.Select(x => wall.GetMaterialArea(x, false)).ToList();
                        var materialVolumes = materialIds.Select(x => wall.GetMaterialVolume(x)).ToList();

                        //convert all material volume and area to the required SI Units
                        double materialArea1 = UnitUtils.ConvertFromInternalUnits(materialAreas[0], UnitTypeId.SquareMeters);
                        double materialVolume1 = UnitUtils.ConvertFromInternalUnits(materialVolumes[0], UnitTypeId.CubicMeters);

                        //show material name
                        TaskDialog newDialog = new TaskDialog("Wall Material Detail");
                        newDialog.MainContent = "The Element Name is --- " + wall.Name + "\n"
                                                + "The Element Material Id is --- " + materialIds[0] + "\n"
                                                + "The Element Material Name is --- " + materialNames[0] + "\n"
                                                + "The Element Material Area is --- " + materialArea1 + "\n"
                                                + "The Element Material Volume is --- " + materialVolume1 + "\n"
                                                + "The Element Count of lcaParameter - ClimateChangePerUnit is --- " + lcaParameter.Count + "\n"
                                                + "The Element Material ClimateChangePerUnit AsDouble is --- " + lcaParameter[0].AsDouble() + "\n"
                                                + "The Element Material ClimateChangePerUnit AsValueString is --- " + lcaParameter[0].AsValueString() + "\n"
                                                + "Would you like to proceed?";
                        
                        newDialog.CommonButtons = TaskDialogCommonButtons.Ok | TaskDialogCommonButtons.Cancel;

                        //if (newDialog.Show() == TaskDialogResult.Ok)
                        //{
                        //    var materialName = materialNames[0];
                        //    materialName = materialName.Replace(",", "");
                        //    string getQuery =
                        //        $"SELECT GWP FROM OkoData WHERE Name_en LIKE '%{materialName}%' AND Konformität = 'DIN EN 15804' AND Modul = 'A1-A3'";
                        //    SqlCommand command = sqlConnection.Query(getQuery);
                        //    SqlDataReader reader = command.ExecuteReader();

                        //    while (reader.Read())
                        //    {
                        //        var gwpIndicator = reader["GWP"];
                        //        //double gwp = Convert.ToDouble(gwpIndicator);
                        //        var gwp = gwpIndicator;
                        //        //var lcaParameter = wall.GetParameters("ClimateChange");
                        //        if (lcaParameter != null)
                        //        {
                        //            foreach (Parameter parameter in lcaParameter)
                        //            {
                        //                parameter.Set((double)gwp * materialVolume1);
                        //            }
                        //        }
                        //        else
                        //        {
                        //            string message2 = string.Format("NULL");
                        //            TaskDialog.Show("There is no value", message2);
                        //        }
                        //    }
                        //    reader.Close();
                        //    DialogResult = DialogResult.OK;
                        //    Close();
                        //}

                        if (newDialog.Show() == TaskDialogResult.Ok)
                        {
                            foreach (Parameter parameter in lcaParameter2)
                            {
                                parameter.Set(lcaParameter[0].AsDouble() * materialVolume1);
                            }
                            DialogResult = DialogResult.OK;
                        }
                    }
                    catch (Exception ex)
                    {
                        TaskDialog.Show("Error", ex.ToString());
                    }
                    t.Commit();
                }
            }
        }
    }
}
