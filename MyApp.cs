using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;
using Autodesk.Revit.UI;


namespace TestWorks
{
    public class MyApp : IExternalApplication
    {
        public Result OnStartup (UIControlledApplication App)
        {
           
            string tabName = "My Tools";
            string panelName = "Testing Codes";

            //creating BITIMAGE
            BitmapImage bt1image = new BitmapImage(new Uri("pack://application:,,,/TestWorks;component/Resources/dc_icon.ico"));
            
            BitmapImage bt2image = new BitmapImage(new Uri("pack://application:,,,/TestWorks;component/Resources/dc_icon2.ico"));
            
            BitmapImage bt3image = new BitmapImage(new Uri("pack://application:,,,/TestWorks;component/Resources/dc_icon3.ico"));
            
            BitmapImage bt4image = new BitmapImage(new Uri("pack://application:,,,/TestWorks;component/Resources/dc_icon4.ico"));
            
            BitmapImage bt5image = new BitmapImage(new Uri("pack://application:,,,/TestWorks;component/Resources/dc_icon5.ico"));
            
            BitmapImage bt6image = new BitmapImage(new Uri("pack://application:,,,/TestWorks;component/Resources/dc_icon6.ico"));
            
            BitmapImage bt7image = new BitmapImage(new Uri("pack://application:,,,/TestWorks;component/Resources/dc_icon7.ico"));
            
            BitmapImage bt8image = new BitmapImage(new Uri("pack://application:,,,/TestWorks;component/Resources/dc_icon8.ico"));
            
            BitmapImage bt9image = new BitmapImage(new Uri("pack://application:,,,/TestWorks;component/Resources/dc_icon9.ico"));
            
            BitmapImage bt10image = new BitmapImage(new Uri("pack://application:,,,/TestWorks;component/Resources/dc_icon10.ico"));
            
            BitmapImage bt11image = new BitmapImage(new Uri("pack://application:,,,/TestWorks;component/Resources/dc_icon11.ico"));

            BitmapImage bt12image = new BitmapImage(new Uri("pack://application:,,,/TestWorks;component/Resources/dc_icon12.ico"));





            //creating TAB
            App.CreateRibbonTab(tabName);

            //creating PANEL
            var myPanel = App.CreateRibbonPanel(tabName, panelName);

            //creating BUTTONS
            string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;

            var button1 = new PushButtonData("my button1", "Wall Collector", thisAssemblyPath, "TestWorks.CommandWallCollector");
            button1.ToolTip = "Collects Wall elements";
            button1.LongDescription = "This button collects all the wall elements by Class Type, Category, Id and LINQS";
            button1.LargeImage = bt1image;
            var btn1 = myPanel.AddItem(button1) as PushButton;

            var button2 = new PushButtonData("my button2", "SetType Param", thisAssemblyPath, "TestWorks.CommandSetTypeParameter");
            button2.ToolTip = "Type parameters of Elements";
            button2.LongDescription = "This button sets specified type parameters of Elements";
            button2.LargeImage = bt2image;
            var btn2 = myPanel.AddItem(button2) as PushButton;

            var button3 = new PushButtonData("my button3", "Add LCAParam", thisAssemblyPath, "TestWorks.CommandSetSharedLCAParam");
            button3.ToolTip = "IFC LCA Parameters";
            button3.LongDescription = "This adds IFC Parameters specific Building LCA information";
            button3.LargeImage = bt3image;
            var btn3 = myPanel.AddItem(button3) as PushButton;

            var button4 = new PushButtonData("my button4", "DeleteElements", thisAssemblyPath, "TestWorks.CommandDeletingElements");
            button4.ToolTip = "This Deletes Element(s)";
            button4.LongDescription = "This deletes specified element(s)";
            button4.LargeImage = bt4image;
            var btn4 = myPanel.AddItem(button4) as PushButton;

            var button5 = new PushButtonData("my button5", "SetViaLookUp", thisAssemblyPath, "TestWorks.CommandSetParamWithLookup");
            button5.ToolTip = "Change Parameter Value";
            button5.LongDescription = "This uses Revitlookup to change parameter value of a specified parameter";
            button5.LargeImage = bt5image;
            var btn5 = myPanel.AddItem(button5) as PushButton;

            var button6 = new PushButtonData("my button6", "CompoundMaterials", thisAssemblyPath, "TestWorks.CommandMaterialExportExcel"); //ExportCSV or //ExportExcel
            button6.ToolTip = "What is the Wall Material?";
            button6.LongDescription = "This uses Revit Compound Structure to get the Material of a Wall";
            button6.LargeImage = bt6image;
            var btn6 = myPanel.AddItem(button6) as PushButton;

            var button7 = new PushButtonData("my button7", "GetWallMaterials", thisAssemblyPath, "TestWorks.CommandGetMaterialsLinq");
            button7.ToolTip = "What are the Wall Material Details?";
            button7.LongDescription = "This uses LINQ to get the Material Details of a Wall";
            button7.LargeImage = bt7image;
            var btn7 = myPanel.AddItem(button7) as PushButton;

            var button8 = new PushButtonData("my button8", "LCAparamTRY", thisAssemblyPath, "TestWorks.CommandLCAparameter2");
            button8.ToolTip = "Set LCAParam Trail";
            button8.LongDescription = "This set one LCA Parameter for Trail purposes";
            button8.LargeImage = bt8image;
            var btn8 = myPanel.AddItem(button8) as PushButton;

            var button9 = new PushButtonData("my button9", "LCAparamValueTRY", thisAssemblyPath, "TestWorks.tryingSQLLCA");
            button9.ToolTip = "Set LCAParam Value Trail";
            button9.LongDescription = "This set one LCA Parameter Value for trail purpose";
            button9.LargeImage = bt9image;
            var btn9 = myPanel.AddItem(button9) as PushButton;

            var button10 = new PushButtonData("my button10", "Wall Count", thisAssemblyPath, "TestWorks.CommandFormWallCount");
            button10.ToolTip = "Wall Count TRAILS";
            button10.LongDescription = "This is an attempt to use Windows Form to simply Count walls by active view and total working document";
            button10.LargeImage = bt10image;
            var btn10 = myPanel.AddItem(button10) as PushButton;

            var button11 = new PushButtonData("my button11", "Database Work", thisAssemblyPath, "TestWorks.CommandSQLFormExportImport");
            button11.ToolTip = "Using SQL DB for TRAILS";
            button11.LongDescription = "This is an attempt to use Windows Form and SQL Database to export and import elements";
            button11.LargeImage = bt11image;
            var btn11 = myPanel.AddItem(button11) as PushButton;

            var button12 = new PushButtonData("my button12", "Database & LCA Work", thisAssemblyPath, "TestWorks.CommandSqlLcaForm");
            button12.ToolTip = "Using SQL DB for LCA TRAILS";
            button12.LongDescription = "This is an attempt to use Windows Form and SQL Database to export and import elements for LCA Work";
            button12.LargeImage = bt12image;
            var btn12 = myPanel.AddItem(button12) as PushButton;





            //RibbonPanel Panel = ribbonPanel(App);
            //PushButton button = Panel.AddItem(new PushButtonData("TestWorks Button", "TestWorks Button", thisAssemblyPath, "TestWorks.Command3")) as PushButton;
            //button.ToolTip = "Testing my codes";
            //var globePath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "dc_icon.ico");
            //Uri uriImage = new Uri(globePath);
            //BitmapImage LargeImage = new BitmapImage(uriImage);
            //button.LargeImage = LargeImage;

            App.ApplicationClosing += App_ApplicationClosing;

            //set application to idling
            App.Idling += App_Idling;

            return Result.Succeeded;
        }

        void App_Idling(object sender, Autodesk.Revit.UI.Events.IdlingEventArgs e)
        {

        }

        void App_ApplicationClosing(object sender, Autodesk.Revit.UI.Events.ApplicationClosingEventArgs e)
        {
            throw new NotImplementedException();
        }

        //public RibbonPanel ribbonPanel(UIControlledApplication App)
        //{
        //    string tab = "My TestWorks Tab";
        //    RibbonPanel ribbonPanel = null;

        //    try
        //    {
        //        //App.CreateRibbonPanel("My TestWorks Tools")
        //        App.CreateRibbonTab(tab);

        //    }
        //    catch { }
        //    try
        //    {
        //        RibbonPanel panel = App.CreateRibbonPanel(tab, "TestWorks");
        //    }
        //    catch { }

        //    List<RibbonPanel> panels = App.GetRibbonPanels(tab);
        //    foreach (RibbonPanel panel in panels)
        //    {
        //        if (panel.Name == "TestWorks")
        //        {
        //            ribbonPanel = panel;
        //        }
        //    }
        //    return ribbonPanel;
        //}

        public Result OnShutdown(UIControlledApplication App)

        {
            return Result.Succeeded;
        }
    }
}
