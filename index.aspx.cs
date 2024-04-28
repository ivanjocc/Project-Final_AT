using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace FinalProject
{
    public partial class index : System.Web.UI.Page
    {
        static DataSet dbNapolitana;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dbNapolitana = CreateNapolitanaDataSet();
            }
        }

        private DataSet CreateNapolitanaDataSet()
        {
            DataSet dataSet = new DataSet("NapolitanaDB");

            // clients table
            DataTable clients = new DataTable("Clients");
            clients.Columns.Add("ClientID", typeof(int)).AutoIncrement = true;
            clients.Columns.Add("Name", typeof(string));
            clients.Columns.Add("PhoneNumber", typeof(string));
            clients.Columns.Add("DeliveryAddress", typeof(string));
            clients.PrimaryKey = new DataColumn[] { clients.Columns["ClientID"] };
            dataSet.Tables.Add(clients);

            // add clients
            AddClient(clients, "ivan", "123", "123 asdasdd");
            AddClient(clients, "jose", "321", "423 colombia");

            // pizzas table
            DataTable pizzas = new DataTable("Pizzas");
            pizzas.Columns.Add("PizzaID", typeof(int)).AutoIncrement = true;
            pizzas.Columns.Add("Name", typeof(string));
            pizzas.PrimaryKey = new DataColumn[] { pizzas.Columns["PizzaID"] };
            dataSet.Tables.Add(pizzas);

            // add pizzas
            AddPizza(pizzas, "margarita");
            AddPizza(pizzas, "peperoni");

            // sizes table
            DataTable sizes = new DataTable("Sizes");
            sizes.Columns.Add("SizeID", typeof(int)).AutoIncrement = true;
            sizes.Columns.Add("Description", typeof(string));
            sizes.PrimaryKey = new DataColumn[] { sizes.Columns["SizeID"] };
            dataSet.Tables.Add(sizes);

            // add sizes
            AddSize(sizes, "small");
            AddSize(sizes, "medium");
            AddSize(sizes, "large");

            // ingredients table
            DataTable ingredients = new DataTable("Ingredients");
            ingredients.Columns.Add("IngredientID", typeof(int)).AutoIncrement = true;
            ingredients.Columns.Add("Name", typeof(string));
            ingredients.PrimaryKey = new DataColumn[] { ingredients.Columns["IngredientID"] };
            dataSet.Tables.Add(ingredients);

            // add ingredients
            AddIngredient(ingredients, "tomatoes");
            AddIngredient(ingredients, "cheese");
            AddIngredient(ingredients, "garlic");

            // crusts table
            DataTable crusts = new DataTable("Crusts");
            crusts.Columns.Add("CrustID", typeof(int)).AutoIncrement = true;
            crusts.Columns.Add("Type", typeof(string));
            crusts.PrimaryKey = new DataColumn[] { crusts.Columns["CrustID"] };
            dataSet.Tables.Add(crusts);

            // add crust
            AddCrust(crusts, "thin");
            AddCrust(crusts, "thick");

            // orders table
            DataTable orders = new DataTable("Orders");
            orders.Columns.Add("OrderID", typeof(int)).AutoIncrement = true;
            orders.Columns.Add("ClientID", typeof(int));
            orders.Columns.Add("PizzaID", typeof(int));
            orders.Columns.Add("SizeID", typeof(int));
            orders.Columns.Add("CrustID", typeof(int));
            orders.PrimaryKey = new DataColumn[] { orders.Columns["OrderID"] };
            dataSet.Tables.Add(orders);

            // foreign keys
            dataSet.Relations.Add("ClientOrders", clients.Columns["ClientID"], orders.Columns["ClientID"]);
            dataSet.Relations.Add("PizzaOrders", pizzas.Columns["PizzaID"], orders.Columns["PizzaID"]);
            dataSet.Relations.Add("SizeOrders", sizes.Columns["SizeID"], orders.Columns["SizeID"]);
            dataSet.Relations.Add("CrustOrders", crusts.Columns["CrustID"], orders.Columns["CrustID"]);

            return dataSet;
        }

        private void AddCrust(DataTable table, string type)
        {
            DataRow row = table.NewRow();
            row["Type"] = type;
            table.Rows.Add(row);
        }

        private void AddIngredient(DataTable table, string name)
        {
            DataRow row = table.NewRow();
            row["Name"] = name;
            table.Rows.Add(row);
        }

        private void AddSize(DataTable table, string description)
        {
            DataRow row = table.NewRow();
            row["Description"] = description;
            table.Rows.Add(row);
        }

        void AddPizza(DataTable table, string name)
        {
            DataRow row = table.NewRow();
            row["Name"] = name;
            table.Rows.Add(row);
        }

        private void AddClient(DataTable table, string name, string phone, string address)
        {
            DataRow row = table.NewRow();
            row["Name"] = name;
            row["PhoneNumber"] = phone;
            row["DeliveryAddress"] = address;
            table.Rows.Add(row);
        }

        protected void SearchClient_Click(object sender, EventArgs e)
        {
            var phoneNumber = txtPhone.Text;
            var clientRow = dbNapolitana.Tables["Clients"].AsEnumerable()
                .FirstOrDefault(row => row.Field<string>("PhoneNumber") == phoneNumber);

            if (clientRow != null)
            {
                clientName.InnerText = clientRow["Name"].ToString();
                deliveryAddress.InnerText = clientRow["DeliveryAddress"].ToString();
            }
            else
            {
                clientName.InnerText = "client not found";
                deliveryAddress.InnerText = "";
            }
        }
        
        protected void SaveEntity_Click(object sender, EventArgs e)
        {

        }
        protected void CancelEntity_Click(object sender, EventArgs e)
        {

        }

        protected void CreateOrder_Click(object sender, EventArgs e)
        {

        }

        protected void SaveOrder_Click(object sender, EventArgs e)
        {

        }

        protected void EntityType_Changed(object sender, EventArgs e)
        {
            // Get the selected value from the dropdown
            string selectedValue = entityTypeDropdown.SelectedValue;

            // Check which table to bind based on the selected value
            switch (selectedValue)
            {
                case "pizzas":
                    entityGrid.DataSource = dbNapolitana.Tables["Pizzas"];
                    ((BoundField)entityGrid.Columns[1]).DataField = "Name";
                    break;
                case "sizes":
                    entityGrid.DataSource = dbNapolitana.Tables["Sizes"];
                    ((BoundField)entityGrid.Columns[1]).DataField = "Description";
                    break;
                case "ingredients":
                    entityGrid.DataSource = dbNapolitana.Tables["Ingredients"];
                    ((BoundField)entityGrid.Columns[1]).DataField = "Name";
                    break;
                case "crusts":
                    entityGrid.DataSource = dbNapolitana.Tables["Crusts"];
                    ((BoundField)entityGrid.Columns[1]).DataField = "Type";
                    break;
                case "clients":
                    entityGrid.DataSource = dbNapolitana.Tables["Clients"];
                    ((BoundField)entityGrid.Columns[1]).DataField = "Name";
                    break;
                default:
                    break;
            }

            // Bind the selected table to the GridView
            entityGrid.DataBind();
        }



        protected void entityGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Set the row for editing
            entityGrid.EditIndex = e.NewEditIndex;

            // Rebind the GridView to show the editing row
            EntityType_Changed(entityTypeDropdown, EventArgs.Empty);
        }

        protected void entityGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // Get the new values from the GridView row
            GridViewRow row = entityGrid.Rows[e.RowIndex];
            int id = Convert.ToInt32(((TextBox)(row.Cells[1].Controls[0])).Text);
            string newName = ((TextBox)(row.Cells[2].Controls[0])).Text;

            // Find the entity in the appropriate table and update its details
            DataRow entityRow = dbNapolitana.Tables[entityTypeDropdown.SelectedValue].Rows.Find(id);
            entityRow["Name"] = newName;

            // Exit editing mode
            entityGrid.EditIndex = -1;

            // Rebind the GridView to show the updated row
            EntityType_Changed(entityTypeDropdown, EventArgs.Empty);
        }

        protected void entityGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            // Exit editing mode
            entityGrid.EditIndex = -1;

            // Rebind the GridView
            EntityType_Changed(entityTypeDropdown, EventArgs.Empty);
        }

    }
}