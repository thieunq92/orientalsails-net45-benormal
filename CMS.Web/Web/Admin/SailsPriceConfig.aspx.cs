using System;
using System.Collections;
using System.Globalization;
using System.Web.UI.WebControls;
using log4net;
using CMS.Web.Domain;
using CMS.Web.Web.UI;
using CMS.Web.Web.Util;

namespace CMS.Web.Web.Admin
{
    public partial class SailsPriceConfig : SailsAdminBasePage
    {
        #region -- Private Member --

        private readonly ILog _logger = LogManager.GetLogger(typeof(SailsPriceConfig));
        private SailsTrip _trip;

        private int TripId
        {
            get
            {
                int id;
                if (Request.QueryString["TripId"] != null && Int32.TryParse(Request.QueryString["TripId"], out id))
                {
                    return id;
                }
                return -1;
            }
        }

        private int _tableId
        {
            get
            {
                if (ViewState["TableId"] != null)
                {
                    return Convert.ToInt32(ViewState["TableId"]);
                }
                return -1;
            }
            set
            {
                ViewState["TableId"] = value;
            }
        }

        private SailsPriceTable _table;

        private TripOption Option
        {
            get
            {
                if (Request.QueryString["Option"] != null)
                {
                    switch (Request.QueryString["Option"])
                    {
                        case "2":
                            return TripOption.Option2;
                        case "3":
                            return TripOption.Option3;
                        default:
                            return TripOption.Option1;
                    }
                }
                return TripOption.Option1;
            }
        }

        public SailsPriceTable Table
        {
            get
            {
                if (_table == null)
                {
                    _table = Module.PriceTableGetById(_tableId);
                }
                return _table;
            }
        }

        private Cruise _activeCruise;
        private Cruise ActiveCruise
        {
            get
            {
                if (_activeCruise == null && Request.QueryString["cruiseid"] != null)
                {
                    _activeCruise = Module.CruiseGetById(Convert.ToInt32(Request.QueryString["cruiseid"]));
                }
                return _activeCruise;
            }
        }

        private Agency _agency;
        private Agency ActiveAgency
        {
            get
            {
                if (_agency == null && Request.QueryString["agencyid"] != null)
                {
                    _agency = Module.AgencyGetById(Convert.ToInt32(Request.QueryString["agencyid"]));
                }
                return _agency;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = Resources.titleSailsPriceConfig;
            if (TripId <= 0)
            {
                panelContent.Visible = false;
                ShowError(Resources.stringAccessDenied);
                return;
            }
            _trip = Module.TripGetById(TripId);

            int option = 0;
            if (Request.QueryString["option"]!=null)
            {
                option = Convert.ToInt32(Request.QueryString["option"]);
            }
            if (option == 0)
            {
                option = 1;
            }
            if (ActiveAgency != null)
            {
                titleSailsPriceConfig.Text = string.Format("Price config for {0}, option {1} on {2} of {3}", _trip.Name, option, ActiveCruise.Name, ActiveAgency.Name);
            }
            else
            {
                titleSailsPriceConfig.Text = string.Format("Price config for {0}, option {1} on {2}", _trip.Name, option, ActiveCruise.Name);
            }
            if (!IsPostBack)
            {
                if (ActiveCruise != null)
                {
                    ddlCruises.Visible = false;
                }
                else
                {
                    ddlCruises.DataSource = Module.CruiseGetAll();
                    ddlCruises.DataTextField = "Name";
                    ddlCruises.DataValueField = "Id";
                    ddlCruises.DataBind();
                    ddlCruises.Items.Insert(0, "-- Choose cruise --");
                }

                rptPriceTables.DataSource = Module.PriceTableGetAll(_trip, Option, ActiveCruise, ActiveAgency);
                rptPriceTables.DataBind();
            }
        }

        protected void rptRoomClass_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            {
                RoomClass roomClass = (RoomClass)e.Item.DataItem;

                #region -- Header --

                // Đối với header, thêm danh sách roomType thông thường
                using (Repeater rpt = e.Item.FindControl("rptRoomTypeHeader") as Repeater)
                {
                    if (rpt != null)
                    {
                        rpt.DataSource = Module.RoomTypexGetAll();
                        rpt.DataBind();
                    }
                }

                #endregion

                #region -- Item --

                #region RoomClass Id

                using (Label labelRoomClassId = e.Item.FindControl("labelRoomClassId") as Label)
                {
                    if (labelRoomClassId != null)
                    {
                        labelRoomClassId.Text = roomClass.Id.ToString();
                    }
                }

                #endregion

                //Đối với từng dòng
                using (Repeater rpt = e.Item.FindControl("rptRoomTypeCell") as Repeater)
                {
                    if (rpt != null)
                    {
                        // Gán sự kiện ItemDataBound (vì control trong Repeater không tự nhận hàm này)
                        rpt.ItemDataBound += RptRoomTypeItemDataBound;

                        IList roomTypeList = Module.RoomTypexGetAll();

                        rpt.DataSource = roomTypeList;
                        rpt.DataBind();
                    }
                }

                #endregion
            }
        }

        protected void RptRoomTypeItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RoomTypex rtype = e.Item.DataItem as RoomTypex;
            RoomClass father = (RoomClass)(((RepeaterItem)e.Item.Parent.Parent).DataItem);
            TextBox txtSingle = (e.Item.Parent.Parent).FindControl("txtSingle") as TextBox;
            if (rtype != null)
            {
                #region RoomType Id

                using (Label labelRoomTypeId = e.Item.FindControl("labelRoomTypeId") as Label)
                {
                    if (labelRoomTypeId != null)
                    {
                        labelRoomTypeId.Text = rtype.Id.ToString();
                    }
                }

                #endregion

                TextBox textBoxPrice = (TextBox)e.Item.FindControl("textBoxPrice");
                Label labelSailsPriceConfigId = (Label)e.Item.FindControl("labelSailsPriceConfigId");

                //Kiểm tra xem có tồn tại room nào mà class và type là rtype và father ko?
                IList room = Module.RoomGetBy_ClassType(ActiveCruise, father, rtype);
                //Nếu có thì hiện giá
                if (room.Count > 0)
                {
                    Domain.SailsPriceConfig priceConfig = Module.SailsPriceConfigGet(Table, rtype, father);
                    //Module.SailsPriceConfigGetBy_RoomType_RoomClass_Trip(_trip,rtype,father,Option);
                    //Nếu có giá thì hiện
                    if (priceConfig != null)
                    {
                        labelSailsPriceConfigId.Text = priceConfig.Id.ToString();
                        textBoxPrice.Text = priceConfig.NetPrice.ToString("####");
                        if (txtSingle != null)
                        {
                            txtSingle.Text = priceConfig.SpecialPrice.ToString("#,0.#");
                        }
                    }
                }
                //Nếu không tồn tại room thì để N/A
                else
                {
                    textBoxPrice.Enabled = false;
                    textBoxPrice.Text = "N/A";
                }
            }
        }

        protected void buttonSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                bool isvalid = false;

                #region -- Lấy thông tin bảng giá, độc lập với lưu giá --
                SailsPriceTable table;
                if (_tableId <= 0)
                {
                    table = new SailsPriceTable();
                }
                else
                {
                    table = Table;
                }
                table.StartDate = DateTime.ParseExact(textBoxStartDate.Text, "dd/MM/yyyy",
                                                      CultureInfo.InvariantCulture);
                table.EndDate = DateTime.ParseExact(textBoxEndDate.Text, "dd/MM/yyyy",
                                                      CultureInfo.InvariantCulture);
                table.Trip = _trip;
                table.TripOption = Option;
                table.Note = string.Empty;
                table.Agency = ActiveAgency;
                if (ActiveCruise != null)
                {
                    table.Cruise = ActiveCruise;
                }
                else
                {
                    if (ddlCruises.SelectedIndex > 0)
                    {
                        table.Cruise = Module.CruiseGetById(Convert.ToInt32(ddlCruises.SelectedValue));
                    }
                    else
                    {
                        table.Cruise = null;
                    }
                }

                #endregion

                foreach (RepeaterItem rptClassItem in rptRoomClass.Items)
                {
                    Repeater rptRoomTypeCell = rptClassItem.FindControl("rptRoomTypeCell") as Repeater;
                    Label labelRoomClassId = rptClassItem.FindControl("labelRoomClassId") as Label;
                    if (labelRoomClassId != null && labelRoomClassId.Text != string.Empty && rptRoomTypeCell != null)
                    {
                        RoomClass roomClass = Module.RoomClassGetById(Convert.ToInt32(labelRoomClassId.Text));

                        #region -- Kiểm tra tính hợp lệ của bảng giá --

                        foreach (RepeaterItem priceItem in rptRoomTypeCell.Items)
                        {
                            TextBox txtCellPrice = priceItem.FindControl("textBoxPrice") as TextBox;
                            //Kiểm tra xem textboxPrice có enable ko ( không nghĩa là o tồn tại giá kiểu class và type đó)
                            if (txtCellPrice != null && txtCellPrice.Enabled)
                            {
                                double price;
                                //kiểm tra xem price có hợp lệ ko
                                isvalid = double.TryParse(txtCellPrice.Text, out price);
                                if (!isvalid) break;
                            }
                        }

                        #endregion

                        //Nếu bảng giá hợp lệ thì lưu
                        if (isvalid)
                        {
                            Module.SaveOrUpdate(table);

                            TextBox txtSingle = rptClassItem.FindControl("txtSingle") as TextBox;
                            double single = 0;
                            if (txtSingle != null && !string.IsNullOrEmpty(txtSingle.Text))
                            {
                                single = Convert.ToDouble(txtSingle.Text);
                            }

                            foreach (RepeaterItem priceItem in rptRoomTypeCell.Items)
                            {
                                Label labelRoomTypeId = priceItem.FindControl("labelRoomTypeId") as Label;
                                Label labelSailsPriceConfigId =
                                    priceItem.FindControl("labelSailsPriceConfigId") as Label;
                                TextBox textBoxPrice = priceItem.FindControl("textBoxPrice") as TextBox;
                                RoomTypex roomType = null;

                                #region Lấy về RoomType tương ứng để chuẩn bị lưu

                                if (labelRoomTypeId != null && labelRoomTypeId.Text != string.Empty)
                                {
                                    if (Convert.ToInt32(labelRoomTypeId.Text) > 0)
                                    {
                                        roomType = Module.RoomTypexGetById(Convert.ToInt32(labelRoomTypeId.Text));
                                    }
                                }

                                #endregion

                                if (textBoxPrice != null && textBoxPrice.Enabled &&
                                    textBoxPrice.Text != string.Empty)
                                {
                                    double price;
                                    //Check giá người dùng nhập vào
                                    if (!double.TryParse(textBoxPrice.Text, out price))
                                    {
                                        isvalid = false;
                                        break;
                                    }
                                    Domain.SailsPriceConfig rPrice;
                                    if (labelSailsPriceConfigId != null &&
                                        !string.IsNullOrEmpty(labelSailsPriceConfigId.Text) &&
                                        Convert.ToInt32(labelSailsPriceConfigId.Text) > 0)
                                    {
                                        //update
                                        rPrice =
                                            Module.SailsPriceConfigGetById(Convert.ToInt32(labelSailsPriceConfigId.Text));
                                    }
                                    else
                                    {
                                        //insert
                                        rPrice = new Domain.SailsPriceConfig();
                                        rPrice.RoomType = roomType;
                                        rPrice.RoomClass = roomClass;
                                        rPrice.TripOption = Option;
                                        rPrice.Trip = _trip;
                                    }

                                    // Giá single supplement
                                    rPrice.SpecialPrice = single;
                                    rPrice.NetPrice = price;
                                    rPrice.Table = table;
                                    Module.SaveOrUpdate(rPrice);
                                }
                            }
                        }
                    }
                }

                PageRedirect(Request.RawUrl);
            }
            catch (Exception ex)
            {
                _logger.Error("Error when buttonSubmit_Click in SailsPriceConfig", ex);
                ShowError(ex.Message);
            }
        }

        protected void buttonCancel_Click(object sender, EventArgs e)
        {
            PageRedirect(string.Format("SailsTripList.aspx?NodeId={0}&SectionId={1}", Node.Id, Section.Id));
        }

        protected void rptPriceTables_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is SailsPriceTable)
            {
                SailsPriceTable table = (SailsPriceTable)e.Item.DataItem;
                Label labelCruise = e.Item.FindControl("labelCruise") as Label;
                if (labelCruise != null)
                {
                    if (table.Cruise != null)
                    {
                        labelCruise.Text = table.Cruise.Name;
                    }
                }

                Label labelValidFrom = e.Item.FindControl("labelValidFrom") as Label;
                if (labelValidFrom != null)
                {
                    if (table.StartDate != null) labelValidFrom.Text = table.StartDate.Value.ToString("dd/MM/yyyy");
                }

                Label labelValidTo = e.Item.FindControl("labelValidTo") as Label;
                if (labelValidTo != null)
                {
                    if (table.EndDate != null) labelValidTo.Text = table.EndDate.Value.ToString("dd/MM/yyyy");
                }

                HyperLink hplEdit = e.Item.FindControl("hplEdit") as HyperLink;
                if (hplEdit != null)
                {
                    hplEdit.NavigateUrl = string.Format("SailsPriceConfig.aspx?NodeId={0}&SectionId={1}&TableId={2}",
                                                        Node.Id, Section.Id, table.Id);
                }
            }
        }

        protected void rptPriceTables_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            _tableId = Convert.ToInt32(e.CommandArgument);
            #region -- Load data --

            if (Table.StartDate != null) textBoxStartDate.Text = Table.StartDate.Value.ToString("dd/MM/yyyy");
            if (Table.EndDate != null) textBoxEndDate.Text = Table.EndDate.Value.ToString("dd/MM/yyyy");
            if (ActiveCruise == null)
            {
                if (Table.Cruise != null)
                {
                    ddlCruises.SelectedValue = Table.Cruise.Id.ToString();
                }
                else
                {
                    ddlCruises.SelectedIndex = 0;
                }
            }

            rptRoomClass.DataSource = Module.RoomClassGetAll();
            rptRoomClass.DataBind();
            #endregion
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            textBoxStartDate.Text = string.Empty;
            textBoxEndDate.Text = string.Empty;

            rptRoomClass.DataSource = Module.RoomClassGetAll();
            rptRoomClass.DataBind();
        }
    }
}