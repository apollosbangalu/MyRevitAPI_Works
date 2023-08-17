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
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB.Architecture;
using System.Data.SqlClient;
using Form = System.Windows.Forms.Form;


namespace TestWorks
{
    
    public partial class SQLDoorsForm : System.Windows.Forms.Form
    {
        Document Doc;
        SQLDBconnect sqlConnection = new SQLDBconnect();
        public SQLDoorsForm(Document doc)
        {
            InitializeComponent();
            Doc = doc;
        }

        private void SQLDoorsForm_Load(object sender, EventArgs e)
        {
            sqlConnection.ConnecDB();

        }

        private void btn_TableCreate_Click(object sender, EventArgs e)
        {
            bool doesExist = TableExists("RevitSQLTrail", "Doors");
            if (doesExist)
            {
                
                TaskDialog.Show("SQL Table Error", "Table already exists" + "\n" + "Do you want to Delete/Drop the Table?" );
                TaskDialog newDialog = new TaskDialog("Delete Table?");
                newDialog.MainContent = "Are you sure you want to delete table?";
                newDialog.CommonButtons = TaskDialogCommonButtons.Ok | TaskDialogCommonButtons.Cancel;
                if (newDialog.Show() == TaskDialogResult.Ok)
                {
                    try
                    {
                        string tableQuery = "DROP TABLE Doors";

                        SqlCommand command = sqlConnection.Query(tableQuery);
                        command.ExecuteNonQuery();
                        TaskDialog.Show("Delete/Drop Table", "Doors table Deleted/Dropped");

                    }
                    catch (Exception ex)
                    {
                        TaskDialog.Show("SQL Error", ex.ToString());
                    }
                }
                else
                {
                    TaskDialog.Show("Delete", "The Table is not deleted");
                }

            }
            else
            {
                try
                {
                    string tableQuery = "CREATE TABLE Doors" + "(UniqueId varchar(255) NOT NULL PRIMARY KEY, FamilyType varchar(255), Mark varchar(255), DoorFinish varchar(255))";

                    SqlCommand command = sqlConnection.Query(tableQuery);
                    command.ExecuteNonQuery();
                    TaskDialog.Show("Create SQL Table", "Doors table created");
                 
                }
                catch (Exception ex)
                {
                    TaskDialog.Show("SQL Error", ex.ToString());
                }
            }

        }

        private bool TableExists(string database, string name)
        {
            try
            {
                string existsQuery = "select case when exists((SELECT * FROM [" + database + "].sys.tables WHERE name = '" + name + "')) then 1 else 0 end";

                SqlCommand command = sqlConnection.Query(existsQuery);
                return(int)command.ExecuteScalar() == 1;
            }
            catch(Exception ex)
            {
                TaskDialog.Show("Error", ex.ToString());
                return true;
            }
        }

        private void btn_ExportDoorData_Click(object sender, EventArgs e)
        {
            IList<Element> doors = new FilteredElementCollector(Doc).OfCategory(BuiltInCategory.
                OST_Doors).WhereElementIsNotElementType().ToElements();

            string setQuery = "INSERT INTO Doors (UniqueId, FamilyType, Mark, DoorFinish) VALUES(@param1, @param2, @param3, @param4)";

            foreach(Element ele in doors)
            {
                Parameter doorFinish = ele.get_Parameter(BuiltInParameter.DOOR_FINISH);
                string dFinish;
                if(doorFinish.HasValue == true)
                {
                    dFinish = doorFinish.AsString();
                }
                else
                {
                    dFinish = " ";
                }
                Parameter doorMark = ele.LookupParameter("Mark");
                string dMark = doorMark.AsString();

                using (SqlCommand command = sqlConnection.Query(setQuery))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@param1", ele.UniqueId);
                        command.Parameters.AddWithValue("@param2", ele.Name);
                        command.Parameters.AddWithValue("@param3", dMark);
                        command.Parameters.AddWithValue("@param4", dFinish);
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        TaskDialog.Show("SQL Insert Error", ex.ToString());
                    }
                }
            }
            TaskDialog.Show("Door Values Export", "Door values added!");
        }

        private void btn_ImportDoorData_Click(object sender, EventArgs e)
        {
            string getQuery = "SELECT * FROM Doors";

            SqlCommand command = sqlConnection.Query(getQuery);
            SqlDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                string uID = reader["UniqueId"].ToString();
                string dFinish = reader["DoorFinish"].ToString();

                Element ele = Doc.GetElement(uID);
                Parameter doorFinish = ele.get_Parameter(BuiltInParameter.DOOR_FINISH);

                using (Transaction t = new Transaction(Doc, "Set Data"))
                {
                    t.Start();
                    doorFinish.Set(dFinish);
                    t.Commit();
                }
            }
            reader.Close();
            TaskDialog.Show("Update Door Finish Values", "Door Finish Values updated");
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
