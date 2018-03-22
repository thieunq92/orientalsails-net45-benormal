using NHibernate;
using CMS.Web.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Web.Repository
{
    public class RoomClassRepository : RepositoryBase<RoomClass>
    {
        public RoomClassRepository() : base() { }

        public RoomClassRepository(ISession session) : base() { }

        public IList<RoomClass> RoomClassGetAll()
        {
            return _session.QueryOver<RoomClass>().Future().ToList();
        }

        public RoomClass RoomClassById(int roomClassId)
        {
            return _session.QueryOver<RoomClass>().Where(x => x.Id == roomClassId).FutureValue().Value;
        }
    }
}