<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="FinalProject.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pizzeria del meneo</title>
    <style>
        body {
            background-color: #fdf6e3;
            color: #333;
            padding: 20px;
            line-height: 1.6;
        }

        body {
            cursor: url('/cursor/cursor.cur'), auto;
        }

        body:active {
            cursor: url('/cursor/active.cur'), auto;
        }

        h2 {
            color: #d35400;
            padding: 10px 0;
        }

        table {
            width: 100%;
            border-collapse: collapse;
            margin: 20px 0;
        }

        th, td {
            padding: 10px;
            border: 1px solid #ccc;
            text-align: left;
        }

        th {
            background-color: #e74c3c;
            color: #fff;
        }

        .button {
            background-color: #e74c3c;
            color: #fff;
            border: none;
            padding: 10px 20px;
            text-transform: uppercase;
            margin: 10px 0;
            border-radius: 10px;
            cursor: url('/cursor/cursor.cur'), auto;
        }

        .button:active {
            cursor: url('/cursor/active.cur'), auto;
        }

        .button:hover {
            background-color: #c0392b;
        }

        input[type="text"], select {
            width: 100%;
            padding: 8px;
            margin: 8px 0;
            display: inline-block;
            border: 1px solid #ccc;
            border-radius: 4px;
            box-sizing: border-box;
            cursor: url('/cursor/cursor.cur'), auto;
        }

        input[type="text"]:active, select:active {
            cursor: url('/cursor/active.cur'), auto;
        }

        .form-control {
            margin-bottom: 20px;
        }

        .label {
            font-weight: bold;
            color: #333;
        }
    </style>

</head>
<body>
    <h1>La pizzeria de Ivan 🙀</h1>
    <form id="form1" runat="server">
        <div>
            <h2>Search Client</h2>
            <asp:Label ID="lblPhone" runat="server" Text="Phone Number:"></asp:Label>
            <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
            <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" OnClick="SearchClient_Click" />
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
            <asp:Button ID="btnAdd" CssClass="button" runat="server" Text="Add" OnClick="btnAdd_Click" />
            <asp:Button ID="btnUpdate" CssClass="button" runat="server" Text="Update" OnClick="btnUpdate_Click" />
            <asp:Button ID="btnDelete" CssClass="button" runat="server" Text="Delete" OnClick="btnDelete_Click" />
            <br />

            <h2>Create Order</h2>
            <br />
            <table id="orderTable" runat="server">
                <tr>
                    <th>Client:</th>
                    <td id="orderClient" runat="server">
                        <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
                    </td>
                    
                </tr>
                <tr>
                    <th>Pizza:</th>
                    <td id="orderPizza" runat="server">
                        <asp:DropDownList ID="DropDownList2" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>Size:</th>
                    <td id="orderSize" runat="server">
                        <asp:DropDownList ID="DropDownList3" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>Crust:</th>
                    <td id="orderCrust" runat="server">
                        <asp:DropDownList ID="DropDownList4" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>Ingredients:</th>
                    <td id="orderIngredients" runat="server">
                        <asp:DropDownList ID="DropDownList5" runat="server"></asp:DropDownList></td>
                </tr>
            </table>
            <asp:Button ID="btnSaveOrder" CssClass="button" runat="server" Text="Save Order" OnClick="SaveOrder_Click" />
        </div>
    </form>
</body>
</html>
