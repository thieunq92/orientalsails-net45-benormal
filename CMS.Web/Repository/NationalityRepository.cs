using NHibernate;
using CMS.Web.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Web.Repository
{
    public class NationalityRepository : RepositoryBase<Nationality>
    {
        public NationalityRepository() : base() { }

        public NationalityRepository(ISession session) : base(session) { }

        public IList<Nationality> NationalityGetAll()
        {
            return _session.QueryOver<Nationality>().Where(x=>x.Deleted == false).Future<Nationality>().ToList();
        }
    }
}