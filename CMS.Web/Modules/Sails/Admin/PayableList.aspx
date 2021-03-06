<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true"
    CodeBehind="PayableList.aspx.cs" Inherits="CMS.Web.Web.Admin.PayableList"
    Title="Untitled Page" %>

<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <script>
        $(function () {
   $("#tabs").tabs({ 
       activate: function() {
          var selectedTab = $('#tabs').tabs('option', 'active');
          $("#<%= hdnSelectedTab.ClientID %>").val(selectedTab);
          },
       active: <%= hdnSelectedTab.Value %>
   });
 });
    </script>
    <style>
        .ajax__calendar_container
        {
            z-index : 1;
        }
    </style>
    <fieldset>
        <legend>
            <img alt="Room" src="../Images/sails.gif" align="absMiddle" />
            Payable report </legend>
        <div class="search_panel">
            <table>
                <tr>
                    <td>
                        From
                    </td>
                    <td>
                        <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox>
                        <ajax:CalendarExtender ID="calendarFrom" runat="server" TargetControlID="txtFrom"
                            Format="dd/MM/yyyy">
                        </ajax:CalendarExtender>
                    </td>
                    <td>
                        To
                    </td>
                    <td>
                        <asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
                        <ajax:CalendarExtender ID="calendarTo" runat="server" TargetControlID="txtTo" Format="dd/MM/yyyy">
                        </ajax:CalendarExtender>
                    </td>
                    <td>
                        Supplier
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSupplier" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
        <asp:Button ID="btnDisplay" runat="server" Text="Display" OnClick="btnDisplay_Click"
            CssClass="button" />
        <div id="tabs">
            <ul>
                <asp:Repeater ID="rptCostTypeTabHeader" runat="server">
                    <ItemTemplate>
                        <li><a href="#tabs-<%# DataBinder.Eval(Container.DataItem, "Id") %>">
                            <%# DataBinder.Eval(Container.DataItem, "Name") %></a></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
            <asp:Repeater ID="rptPanel" runat="server" OnItemDataBound="rptPanel_ItemDataBound">
                <ItemTemplate>
                    <div id="tabs-<%# DataBinder.Eval(Container.DataItem, "Id") %>">
                        <div class="data_table">
                            <div class="data_grid">
                                <table>
                                    <tr>
                                        <th>
                                            Date
                                        </th>
                                        <th>
                                            Partner
                                        </th>
                                        <th>
                                            Service
                                        </th>
                                        <th>
                                            Total
                                        </th>
                                        <th>
                                            Paid
                                        </th>
                                        <th>
                                            Payable
                                        </th>
                                        <th style="width: 160px;">
                                        </th>
                                    </tr>
                                    <asp:Repeater ID="rptExpenseServices" runat="server" OnItemDataBound="rptExpenseServices_ItemDataBound"
                                        OnItemCommand="rptExpenseServices_ItemCommand">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:HyperLink ID="hplDate" runat="server"></asp:HyperLink>
                                                </td>
                                                <td>
                                                    <asp:HyperLink ID="hplPartner" runat="server"></asp:HyperLink>
                                                </td>
                                                <td>
                                                    <asp:HyperLink ID="hplService" runat="server"></asp:HyperLink>
                                                </td>
                                                <td>
                                                    <asp:Literal ID="litTotal" runat="server"></asp:Literal>
                                                </td>
                                                <td>
                                                    <asp:Literal ID="litPaid" runat="server"></asp:Literal>
                                                </td>
                                                <td>
                                                    <asp:Literal ID="litPayable" runat="server"></asp:Literal>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPay" runat="server" Style="width: 132px"></asp:TextBox>
                                                    <asp:Button ID="btnPay" runat="server" Text="Pay" CssClass="button" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <tr>
                                                <td colspan="3">
                                                    GRAND TOTAL
                                                </td>
                                                <td>
                                                    <asp:Literal ID="litTotal" runat="server"></asp:Literal>
                                                </td>
                                                <td>
                                                    <asp:Literal ID="litPaid" runat="server"></asp:Literal>
                                                </td>
                                                <td>
                                                    <asp:Literal ID="litPayable" runat="server"></asp:Literal>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnPay" runat="server" Text="Pay all" CssClass="button"/>
                                                    <ajax:ConfirmButtonExtender ID="ConfirmButtonExtenderDelete" runat="server" TargetControlID="btnPay"
                                                        ConfirmText='Are you sure you have pay all payable in the list? This action can not be undone.'>
                                                    </ajax:ConfirmButtonExtender>
                                                </td>
                                            </tr>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </table>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <asp:Button ID="btnExport" runat="server" Text="Export" CssClass="button" OnClick="btnExport_Click" />
    </fieldset>
    <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0" />
</asp:Content>
