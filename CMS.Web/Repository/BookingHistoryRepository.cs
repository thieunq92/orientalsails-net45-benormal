using NHibernate;
using CMS.Web.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Web.Repository
{
    public class BookingHistoryRepository : RepositoryBase<BookingHistory>
    {
        public BookingHistoryRepository() : base() { }

        public BookingHistoryRepository(ISession session) : base(session) { }


        public IList<BookingHistory> BookingHistoryGetByBookingId(int bookingId)
        {
            return _session.QueryOver<BookingHistory>().Where(x => x.Booking.Id == bookingId).Future().ToList();
        }
    }
}