<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true"
    Codebehind="DefaultSetting.aspx.cs" Inherits="CMS.Web.Web.Admin.DefaultSetting"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <legend>
            <img alt="Room" src="../Images/sails.gif" align="absMiddle" />
            <%= base.GetText("titleSettings") %>
        </legend>
        <div class="basicinfo">
            <table>
                <asp:PlaceHolder ID="plhSettings" runat="server" Visible="false">
                    <tr>
                        <td>
                           ></td>
                        <td>
                            <asp:DropDownList ID="ddlTicketSuppliers" runat="server">
                            </asp:DropDownList></td>
                        <td>
                            Meal agency</td>
                        <td>
                            <asp:DropDownList ID="ddlMealSuppliers" runat="server">
                            </asp:DropDownList></td>
                        <td>
                            (Hỗ trợ dịch vụ)</td>
                        <td>
                            <asp:DropDownList ID="ddlServicesSuppliers" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>
                            Kayaking agency</td>
                        <td>
                            <asp:DropDownList ID="ddlKayakingSuppliers" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                            Rock climbing</td>
                        <td>
                            <asp:DropDownList ID="ddlRockClimbing" runat="server">
                            </asp:DropDownList></td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </asp:PlaceHolder>
                <tr>
                    <td>
                        <%= base.GetText("textChildPercentage") %></td>
                    <td>
                        <asp:TextBox ID="txtChildPrice" runat="server"></asp:TextBox></td>
                    <td>
                         <%= base.GetText("textUseCustomBookingId") %></td>
                    <td>
                        <asp:CheckBox ID="chkCustomBookingId" runat="server" /></td>
                    <td><%= base.GetText("textBookingCodeFormat") %>
                    </td>
                    <td><asp:TextBox ID="txtBookingFormat" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td><%= base.GetText("textShowCustomerName") %></td>
                    <td><asp:CheckBox ID="chkShowCustomerName" runat="server" /></td>
                    <td><%= base.GetText("textUseCustomPriceWhenAddBooking") %></td>
                    <td><asp:CheckBox ID="chkCustomPriceAddBooking" runat="server" /></td>
                    <td><%= base.GetText("textUseCustomPriceForEachRoom") %></td>
                    <td><asp:CheckBox ID="chkCustomPriceForRoom" runat="server" /></td>
                </tr>
                <tr>
                    <td><%= base.GetText("textExpensePartnershipManager") %></td>
                    <td><asp:CheckBox ID="chkPartnership" runat="server" /></td>
                    <td>Check account status</td>
                    <td><asp:CheckBox ID="chkAccountStatus" runat="server" /></td>
                    <td>Check charter</td>
                    <td><asp:CheckBox ID="chkCharter" runat="server" /></td>
                </tr>
                <tr>
                    <td>Show expense in booking by date</td>
                    <td><asp:CheckBox ID="chkShowExpenseByDate" runat="server" /></td>
                    <td>Bar revenue</td>
                    <td><asp:CheckBox ID="chkBarRevenue" runat="server" /></td>
                    <td>Allow no agency booking</td>
                    <td><asp:CheckBox ID="chkNoAgency" runat="server" /></td>
                </tr>
                <tr>
                    <td>Use detail service</td>
                    <td><asp:CheckBox ID="chkDetail" runat="server" /></td>
                    <td>Show overall daily expense in Bk by date</td>
                    <td><asp:CheckBox ID="chkOverall" runat="server" /></td>
                    <td>Use default status is approved when add booking</td>
                    <td><asp:CheckBox ID="chkApprovedDefault" runat="server" /></td>
                </tr>
                <tr>
                    <td>P/u address required</td>
                    <td><asp:CheckBox ID="chkPuRequired" runat="server" /></td>
                    <td>Period expense avg.</td>
                    <td><asp:CheckBox ID="chkPeriodExpenseAvg" runat="server" /></td>
                    <td>Lock booking after approved</td>
                    <td><asp:CheckBox ID="chkApprovedLock" runat="server" /></td>
                </tr>
                <tr>
                    <td>Price by each customer</td>
                    <td><asp:CheckBox ID="chkCustomerPrice" runat="server" /></td>
                    <td>Use VND expense</td>
                    <td><asp:CheckBox ID="chkVND" runat="server" /></td>
                    <td></td>
                    <td></td>
                </tr>
            </table>
        </div>
        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="button" />
    </fieldset>
</asp:Content>
