<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SeriesView.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.SeriesView" MasterPageFile="MO.Master" Title ="Series View" %>

<asp:Content ID="AdminContent" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="table-panel">
        <div class="row">
            <div class="col-xs-12">
                <table class="table table-bordered table-hover">
                    <tr class="active">
                        <th>Series code</th>
                        <th>Booking code</th>
                        <th>Date</th>
                        <th>Trip</th>
                        <th>Cruise</th>
                        <th>Pax</th>
                        <th>Cabins</th>
                    </tr>
                    <asp:Repeater runat="server" ID="rptListBooking">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("Series.SeriesCode")%></td>
                                <td>OS<%# Eval("Id") %></td>
                                <td><%# DataBinder.Eval(Container.DataItem, "StartDate", "{0:dd/MM/yyyy}")%></td>
                                <td><%# Eval("Trip.Name") %></td>
                                <td><%# Eval("Cruise.name")%></td>
                                <td>Adults : <%# Eval("Adult")%></br> 
                                    Childs : <%# Eval("Child")%></br>
                                    Baby : <%# Eval("Baby")%></td>
                                <td><%# Eval("RoomName") %>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
