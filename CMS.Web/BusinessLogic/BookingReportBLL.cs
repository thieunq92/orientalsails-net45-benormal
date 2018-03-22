using CMS.Web.Domain;
using CMS.Web.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Web.BusinessLogic
{
    public class BookingReportBLL
    {
        public BookingRepository BookingRepository { get; set; }
        public CruiseRepository CruiseRepository { get; set; }

        public BookingReportBLL() {
            BookingRepository = new BookingRepository();
            CruiseRepository = new CruiseRepository();
        }

        public IList<Booking> BookingReportBLL_BookingSearchBy(DateTime startDate, int cruiseId, int bookingStatus)
        {
           return BookingRepository.BookingReportBLL_BookingSearchBy(startDate, cruiseId, bookingStatus);
        }

        public Cruise CruiseGetById(int cruiseId)
        {
            return CruiseRepository.CruiseGeById(cruiseId);
        }
    }
}