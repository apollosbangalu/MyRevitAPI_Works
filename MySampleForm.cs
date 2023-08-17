using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace TestWorks
{
    public partial class MySampleForm : System.Windows.Forms.Form
    {
        Document Doc;
        
        public MySampleForm (Document doc)
        {
            InitializeComponent();
            Doc = doc;
        }

        private void Wall_Count_Click(object sender, EventArgs e)
        {
            ICollection<Element> walls = new FilteredElementCollector(Doc, Doc.ActiveView.Id)
                .OfCategory(BuiltInCategory.OST_Walls).ToElements();

            SampleCollector sc = new SampleCollector();
            List<Wall> ListWalls_Class = sc.GetWalls_Class(Doc);


            TaskDialog.Show("Wall Count", walls.Count.ToString() + "walls from Active View method" + "\n"
                + ListWalls_Class.Count.ToString() + "walls from Linq method of Sample collector");
        }

        private void MySampleForm_Load(object sender, EventArgs e)
        {

        }
    }
}
