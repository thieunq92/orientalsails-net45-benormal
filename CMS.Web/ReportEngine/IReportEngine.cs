using System;
using System.Collections;
using System.Web;
using CMS.Web.Domain;
using CMS.Web.Web.UI;

namespace CMS.Web.ReportEngine
{
    public interface IReportEngine
    {
        /// <summary>
        /// Xuất dữ liệu thông tin chi tiết về khách check-in theo ngày
        /// </summary>
        void CustomerDetails(IList customers, DateTime startDate, string tplPath, HttpResponse response);

        /// <summary>
        /// Xuất dữ liệu thông tin về một booking
        /// </summary>
        void BookingConfirmation(Booking booking, SailsAdminBasePage Page, HttpResponse Response, string templatePath);

        /// <summary>
        /// Xuất ra doanh thu theo ngày
        /// </summary>
        void IncomeByDate(IList list, SailsAdminBasePage Page, HttpResponse Response, string templatePath);

        /// <summary>
        /// Xuất ra bảng thông tin xuất nhập cảnh
        /// </summary>
        void Provisional(IList list, DateTime date, Purpose defaultPurpose, SailsAdminBasePage Page,
                         HttpResponse Response, string templatePath);
    }
}