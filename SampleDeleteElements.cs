using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace TestWorks
{
    class SampleDeleteElements
    {
        //deleting only 1 element using the ID
        public void DeleteElement(Document doc)
        {
            Element element = FindElementByName(doc, typeof(Floor), "ApollosSlab150");

            using(Transaction t = new Transaction(doc, "Delete element"))
            {
                t.Start();
                doc.Delete(element.Id);
                TaskDialog tDialog = new TaskDialog("Delete Element");
                tDialog.MainContent = "Are you sure you want to delete?";
                tDialog.CommonButtons = TaskDialogCommonButtons.Ok | TaskDialogCommonButtons.Cancel;
                if(tDialog.Show() == TaskDialogResult.Ok)
                {
                    t.Commit();
                    TaskDialog.Show("Delete", element.Id.ToString() + "deleted");
                }
                else
                {
                    t.RollBack();
                    TaskDialog.Show("Delete", element.Id.ToString() + "not deleted");
                }
            }
        }

        //deleting multiple elements
        public void DeleteElements(Document doc)
        {
            List<Wall> walls = GetWalls(doc);
            List<ElementId> idSelection = new List<ElementId>();

            foreach(Wall w in walls)
            {
                Element e = w as Element;
                idSelection.Add(e.Id);
            }

            using(Transaction t = new Transaction(doc,"Delete elements"))
            {
                t.Start();
                doc.Delete(idSelection);
                TaskDialog tDialog = new TaskDialog("Delete Element");
                tDialog.MainContent = "Are you sure you want to delete?";
                tDialog.CommonButtons = TaskDialogCommonButtons.Ok | TaskDialogCommonButtons.Cancel;
                if (tDialog.Show() == TaskDialogResult.Ok)
                {
                    t.Commit();
                    TaskDialog.Show("Delete", idSelection.ToString() + "deleted");
                }
                else
                {
                    t.RollBack();
                    TaskDialog.Show("Delete", idSelection.ToString() + "not deleted");
                }
            }
        }

        //method to return a list of walls
        public List<Wall> GetWalls(Document doc)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            ICollection<Element> Walls = collector.OfClass(typeof(Wall)).ToElements();
            List<Wall> List_Walls = new List<Wall>();
            foreach (Wall w in Walls)
            {
                List_Walls.Add(w);
            }
                
            return List_Walls;
        }

        //a method to find a particular element
        public Element FindElementByName(Document doc, Type targetType, string targetName)
        {
            return new FilteredElementCollector(doc)
                .OfClass(targetType)
                .FirstOrDefault<Element>(e => e.Name.Equals(targetName));
        }

    }
}
