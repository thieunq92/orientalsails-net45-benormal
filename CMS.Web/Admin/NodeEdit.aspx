﻿<%@ Page language="c#" Codebehind="NodeEdit.aspx.cs" AutoEventWireup="false" Inherits="CMS.Web.Admin.NodeEdit" %>
<%@ Import Namespace="CMS.Web.Util"%>
<%@ Import Namespace="CMS.Core.Util"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 TRANSITIONAL//EN" >
<html>
	<head>
		<title>Hệ thống quản trị website</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<meta content="C#" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		<link href="Css/Admin.css" type="text/css" rel="stylesheet"/>
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<script type="text/javascript"> <!--
				function confirmDeleteNode()
				{
					return confirm("Bạn thực sự muốn xóa nút này?");
				}
				// -->
			</script>
			<p>Quản lý thuộc tính của các nút (trang). Sử dụng các nút nằm ở phía dưới để lưu hoặc xóa
			trang hay thêm một nút con vào nút hiện tại.</p>
			<div class="group">
				<h4>General</h4>
				<table>
					<tr>
						<td style="WIDTH: 100px">Tiêu đề nút</td>
						<td><asp:textbox id="txtTitle" runat="server" width="300px"></asp:textbox><asp:requiredfieldvalidator id="rfvTitle" runat="server" errormessage="Phải có tiêu đề" display="Dynamic"
								cssclass="validator" controltovalidate="txtTitle" enableclientscript="False"></asp:requiredfieldvalidator></td>
					</tr>
					<tr>
						<td>Friendly url</td>
						<td><asp:textbox id="txtShortDescription" runat="server" width="300px" tooltip="Bạn có thể dùng chức năng này để tạo thành đường dẫn dễ nhớ ([shortdescription].aspx). Lưu ý nó phải là duy nhất trên site của bạn!"></asp:textbox><asp:Label ID="labelExtension" runat="server"></asp:Label>&nbsp;<asp:regularexpressionvalidator id="revShortDescription" runat="server" errormessage="Không được có khoảng trắng" display="Dynamic"
								cssclass="validator" controltovalidate="txtShortDescription" enableclientscript="False" validationexpression="\S+"></asp:regularexpressionvalidator><asp:requiredfieldvalidator id="rfvShortDescription" runat="server" errormessage="Phải có tên rút gọn"
								display="Dynamic" controltovalidate="txtShortDescription" enableclientscript="False"></asp:requiredfieldvalidator></td>
					</tr>
					<tr>
						<td>Nút cha</td>
						<td><asp:label id="lblParentNode" runat="server"></asp:label></td>
					</tr>
					<tr>
						<td>Vị trí</td>
						<td><asp:imagebutton id="btnUp" runat="server" imageurl="~/Admin/Images/upred.gif" causesvalidation="False"
								alternatetext="Di chuyển lên"></asp:imagebutton><asp:imagebutton id="btnDown" runat="server" imageurl="~/Admin/Images/downred.gif" causesvalidation="False"
								alternatetext="Di chuyển xuống"></asp:imagebutton><asp:imagebutton id="btnLeft" runat="server" imageurl="~/Admin/Images/leftred.gif" causesvalidation="False"
								alternatetext="Di chuyển sang trái"></asp:imagebutton><asp:imagebutton id="btnRight" runat="server" imageurl="~/Admin/Images/rightred.gif" causesvalidation="False"
								alternatetext="Di chuyển sang phải"></asp:imagebutton></td>
					</tr>
					<tr>
						<td>Vùng ngôn ngữ</td>
						<td><asp:dropdownlist id="ddlCultures" runat="server"></asp:dropdownlist></td>
					</tr>
					<tr>
						<td>Trên thanh định hướng</td>
						<td><asp:checkbox id="chkShowInNavigation" runat="server"></asp:checkbox></td>
					</tr>
					<tr>
						<td>Thông tin meta</td>
						<td>
							<asp:textbox id="txtMetaDescription" runat="server" maxlength="500" textmode="MultiLine"
								width="400px" height="35px"></asp:textbox></td>
					</tr>
					<tr>
						<td>Từ khóa</td>
						<td>
							<asp:textbox id="txtMetaKeywords" runat="server" maxlength="500" textmode="MultiLine"
								width="400px" height="35px"></asp:textbox></td>
					</tr>
					<tr>
						<td></td>
						<td><asp:checkbox id="chkLink" runat="server" autopostback="True" text="Nút này trỏ đến một URL ngoài"></asp:checkbox><asp:panel id="pnlLink" runat="server" visible="False">
								<table>
									<tr>
										<td style="width:60px">Url</td>
										<td>
											<asp:textbox id="txtLinkUrl" runat="server" width="400px"></asp:textbox></td>
									</tr>
									<tr>
										<td>Khi kích chuột vào Url này</td>
										<td>
											<asp:dropdownlist id="ddlLinkTarget" runat="server">
												<asp:listitem value="Self">Vẫn ở cửa sổ cũ</asp:listitem>
												<asp:listitem value="New">Mở cửa sổ mới</asp:listitem>
											</asp:dropdownlist></td>
									</tr>
								</table>
							</asp:panel></td>
					</tr>
				</table>
			</div>
			<asp:panel id="pnlTemplate" runat="server" cssclass="group">
				<h4>Giao diện</h4>
				<table>
					<tr>
						<td style="WIDTH: 100px">&nbsp;</td>
						<td>
							<asp:dropdownlist id="ddlTemplates" runat="server" autopostback="True"></asp:dropdownlist></td>
					</tr>
				</table>
			</asp:panel><asp:panel id="pnlMenus" runat="server" cssclass="group" visible="False">
				<h4>Thực đơn</h4>
				<em>Bạn đang sửa nút gốc nên bạn có thể thêm vào các menu tùy biến</em>
				<table class="tbl">
					<asp:repeater id="rptMenus" runat="server">
						<headertemplate>
							<tr>
								<th>
									Thực đơn</th>
								<th>
									Vị trí</th>
								<th>
									Thao tác</th>
							</tr>
						</headertemplate>
						<itemtemplate>
							<tr>
								<td><%# DataBinder.Eval(Container.DataItem, "Name") %></td>
								<td><%# DataBinder.Eval(Container.DataItem, "Placeholder") %></td>
								<td>
									<asp:hyperlink id="hplEditMenu" runat="server">Edit</asp:hyperlink>
								</td>
							</tr>
						</itemtemplate>
					</asp:repeater></table>
				<asp:hyperlink id="hplNewMenu" runat="server">Thêm thực đơn</asp:hyperlink>
			</asp:panel><asp:panel id="pnlSections" runat="server" cssclass="group">
				<h4>Vùng phân hệ</h4>
				<table class="tbl">
					<asp:repeater id="rptSections" runat="server">
						<headertemplate>
							<tr>
								<th>
									Tiêu đề vùng</th>
								<th>
									Tên phân hệ</th>
								<th>
									Vị trí trên site</th>
								<th>
									Thời gian cache</th>
								<th>
									Thứ tự</th>
								<th>
									Thao tác</th>
							</tr>
						</headertemplate>
						<itemtemplate>
							<tr>
								<td><%# DataBinder.Eval(Container.DataItem, "Title") %></td>
								<td><%# DataBinder.Eval(Container.DataItem, "ModuleType.FullName") %></td>
								<td><%# DataBinder.Eval(Container.DataItem, "PlaceholderId") %>
									<asp:label id="lblNotFound" cssclass="validator" visible="False" runat="server">(không tìm thấy trên mẫu!)</asp:label></td>
								<td style="text-align:right"><%# DataBinder.Eval(Container.DataItem, "CacheDuration") %></td>
								<td>
									<asp:hyperlink id="hplSectionUp" imageurl="~/Admin/Images/upred.gif" visible="False" enableviewstate="False"
										runat="server">Di chuyển lên</asp:hyperlink>
									<asp:hyperlink id="hplSectionDown" imageurl="~/Admin/Images/downred.gif" visible="False" enableviewstate="False"
										runat="server">Di chuyển xuống</asp:hyperlink>
								</td>
								<td>
									<asp:hyperlink id="hplEdit" runat="server">Sửa</asp:hyperlink>
									<asp:linkbutton id="lbtDetach" runat="server" causesvalidation="False" commandname="Detach" commandargument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'>Gỡ ra</asp:linkbutton>
									<asp:linkbutton id="lbtDelete" runat="server" causesvalidation="False" commandname="Delete" commandargument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'>Xóa đi</asp:linkbutton>
								</td>
							</tr>
						</itemtemplate>
					</asp:repeater></table>
				<asp:hyperlink id="hplNewSection" runat="server" visible="False">Thêm vùng phân hệ</asp:hyperlink>
			</asp:panel>
			<div class="group">
				<h4>Quản lý quyền</h4>
				<table class="tbl">
					<asp:repeater id="rptRoles" runat="server">
						<headertemplate>
							<tr>
								<th>
									Vai trò</th>
								<th>
									Quyền xem</th>
								<th>
									Quyền quản trị</th>
							</tr>
						</headertemplate>
						<itemtemplate>
							<tr>
								<td><%# DataBinder.Eval(Container.DataItem, "Name") %></td>
								<td style="text-align:center">
									<asp:checkbox id="chkViewAllowed" runat="server"></asp:checkbox></td>
								<td style="text-align:center">
									<asp:checkbox id="chkEditAllowed" runat="server"></asp:checkbox></td>
							</tr>
						</itemtemplate>
					</asp:repeater></table>
				<br/>
				<asp:checkbox id="chkPropagateToSections" runat="server" text="Thừa kế phân quyền nút sang các vùng phân hệ trong nó"></asp:checkbox><br/>
				<asp:checkbox id="chkPropagateToChildNodes" runat="server" text="Thừa kế phân quyền nút sang các nút con"></asp:checkbox></div>
			<div><asp:button id="btnSave" runat="server" text="Lưu"></asp:button><asp:button id="btnCancel" runat="server" causesvalidation="False" text="Hoàn tác"></asp:button><asp:button id="btnNew" runat="server" text="Thêm nút con"></asp:button><asp:button id="btnDelete" runat="server" causesvalidation="False" text="Xóa"></asp:button></div>
		</form>
	</body>
</html>
