using CMS.Web.Domain;
using CMS.Web.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Web.BusinessLogic
{
    public class SeriesViewBLL
    {
        public SeriesRepository SeriesRepository { get; set; }

        public BookingRepository BookingRepository { get; set; }

        public SeriesViewBLL()
        {
            SeriesRepository = new SeriesRepository();
            BookingRepository = new BookingRepository();
        }

        public void Dispose()
        {
            if (SeriesRepository != null)
            {
                SeriesRepository.Dispose();
                SeriesRepository = null;
            }

            if (BookingRepository != null)
            {
                BookingRepository.Dispose();
                BookingRepository = null;
            }
        }

        public IList<Booking> BookingGetBySeries(int seriesId)
        {
            return BookingRepository.BookingGetBySeries(seriesId);
        }
    }
}