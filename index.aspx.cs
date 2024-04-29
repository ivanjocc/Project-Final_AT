using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
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
            sizes.Columns.Add("Name", typeof(string));
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
            crusts.Columns.Add("Name", typeof(string));
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
            row["Name"] = type;
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
            row["Name"] = description;
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
                clientName.InnerText = "Client not found";
                deliveryAddress.InnerText = "";
            }
        }

        protected void SaveOrder_Click(object sender, EventArgs e)
        {

        }

        protected void EntityType_Changed(object sender, EventArgs e)
        {
            lstInfo.Items.Clear();

            string selectedEntityType = entityTypeDropdown.SelectedValue;
            DataTable selectedTable = dbNapolitana.Tables[selectedEntityType];

            foreach (DataRow row in selectedTable.Rows)
            {
                ListItem newItem = new ListItem(row["Name"].ToString());
                lstInfo.Items.Add(newItem);
            }

            LoadItemsForSelectedTable();
        }


        protected void lstInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstInfo.SelectedIndex != -1)
            {
                txtName.Text = lstInfo.SelectedItem.Text;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string selectedTable = entityTypeDropdown.SelectedValue;
            DataTable table = dbNapolitana.Tables[selectedTable];

            if (lstInfo.SelectedItem != null)
            {
                int itemId = Convert.ToInt32(lstInfo.SelectedValue);

                DataRow rowToDelete = table.Rows.Find(itemId);

                if (rowToDelete != null)
                {
                    table.Rows.Remove(rowToDelete);

                    LoadItemsForSelectedTable();
                }
            }
        }

        private void LoadItemsForSelectedTable()
        {
            string selectedTable = entityTypeDropdown.SelectedValue;
            DataTable table = dbNapolitana.Tables[selectedTable];

            lstInfo.Items.Clear();

            foreach (DataRow row in table.Rows)
            {
                ListItem listItem = new ListItem(row["Name"].ToString(), row[table.PrimaryKey[0].ColumnName].ToString());
                lstInfo.Items.Add(listItem);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string selectedTable = entityTypeDropdown.SelectedValue;
            DataTable table = dbNapolitana.Tables[selectedTable];

            DataRow newRow = table.NewRow();

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                newRow["Name"] = "Nuevo ítem";
            }
            else
            {
                newRow["Name"] = txtName.Text;
            }

            table.Rows.Add(newRow);

            LoadItemsForSelectedTable();

            txtName.Text = "";
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string selectedTable = entityTypeDropdown.SelectedValue;
            DataTable table = dbNapolitana.Tables[selectedTable];

            if (lstInfo.SelectedItem != null)
            {
                int itemId = Convert.ToInt32(lstInfo.SelectedValue);

                DataRow rowToUpdate = table.Rows.Find(itemId);

                if (rowToUpdate != null && !string.IsNullOrWhiteSpace(txtName.Text))
                {
                    rowToUpdate["Name"] = txtName.Text;

                    LoadItemsForSelectedTable();
                }
            }
        }
    }
}