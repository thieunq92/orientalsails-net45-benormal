using CMS.Web.BusinessLogic;
using CMS.Web.Web.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.Web.Web.Admin
{
    public partial class SeriesView : SailsAdminBasePage
    {
        private SeriesViewBLL seriesViewBLL;
        public SeriesViewBLL SeriesViewBLL
        {
            get
            {
                if (seriesViewBLL == null)
                {
                    seriesViewBLL = new SeriesViewBLL();
                }
                return seriesViewBLL;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            BookingLoadData();
        }

        public void BookingLoadData()
        {
            var seriesId = -1;
            try
            {
                seriesId = Convert.ToInt32(Request.QueryString["si"]);
            }
            catch { }

            var listBooking = SeriesViewBLL.BookingGetBySeries(seriesId);
            rptListBooking.DataSource = listBooking;
            rptListBooking.DataBind();
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (seriesViewBLL != null)
            {
                seriesViewBLL.Dispose();
                seriesViewBLL = null;
            }
        }
    }
}