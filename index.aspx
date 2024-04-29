<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="FinalProject.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pizzeria del meneo</title>
    <style type="text/css">
        body {
            cursor: url('/cursor/cursor.cur'), auto;
        }

        body:active {
            cursor: url('/cursor/active.cur'), auto;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Search Client</h2>
            <asp:Label ID="lblPhone" runat="server" Text="Phone Number:"></asp:Label>
            <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="SearchClient_Click" />
            <br />
            <br />
            <table id="clientInfo" runat="server">
                <tr>
                    <th>Name:</th>
                    <td id="clientName" runat="server"></td>
                </tr>
                <tr>
                    <th>Delivery Address:</th>
                    <td id="deliveryAddress" runat="server"></td>
                </tr>
            </table>

            <h2>Manage Database</h2>
            <p>Select Table:</p>
            <asp:DropDownList ID="entityTypeDropdown" runat="server" OnSelectedIndexChanged="EntityType_Changed" AutoPostBack="true">
                <asp:ListItem Value="pizzas">Pizzas</asp:ListItem>
                <asp:ListItem Value="sizes">Sizes</asp:ListItem>
                <asp:ListItem Value="ingredients">Ingredients</asp:ListItem>
                <asp:ListItem Value="crusts">Crusts</asp:ListItem>
                <asp:ListItem Value="clients">Clients</asp:ListItem>
            </asp:DropDownList>
            <br />
            <div id="entityForm" runat="server">
                <asp:ListBox ID="lstInfo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="lstInfo_SelectedIndexChanged"></asp:ListBox>
                <br />
            </div>
            <br />
            <asp:Label ID="lblName" runat="server" Text="Name"></asp:Label>
            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
            <asp:Button ID="btnAdd" runat="server" Text="Add" />
            <asp:Button ID="btnUpdate" runat="server" Text="Update" />
            <asp:Button ID="btnDelete" runat="server" Text="Delete" />
            <br />

            <h2>Create Order</h2>
            <br />
            <table id="orderTable" runat="server">
                <tr>
                    <th>Client:</th>
                    <td id="orderClient" runat="server"></td>
                </tr>
                <tr>
                    <th>Pizza:</th>
                    <td id="orderPizza" runat="server"></td>
                </tr>
                <tr>
                    <th>Size:</th>
                    <td id="orderSize" runat="server"></td>
                </tr>
                <tr>
                    <th>Crust:</th>
                    <td id="orderCrust" runat="server"></td>
                </tr>
                <tr>
                    <th>Ingredients:</th>
                    <td id="orderIngredients" runat="server"></td>
                </tr>
            </table>
            <asp:Button ID="btnSaveOrder" runat="server" Text="Save Order" OnClick="SaveOrder_Click" />
        </div>
    </form>
</body>
</html>
