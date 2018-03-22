using NHibernate;
using CMS.Web.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Web.Repository
{
    public class BookingRoomRepository : RepositoryBase<BookingRoom>
    {
        public BookingRoomRepository() : base() { }

        public BookingRoomRepository(ISession session) : base(session) { }


        public BookingRoom BookingRoomGetById(int bookingRoomId)
        {
            return _session.QueryOver<BookingRoom>().Where(x => x.Id == bookingRoomId).FutureValue().Value;
        }
    }
}