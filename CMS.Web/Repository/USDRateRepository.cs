using NHibernate;
using CMS.Web.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Web.Repository
{
    public class USDRateRepository : RepositoryBase<USDRate>
    {
        public USDRateRepository() : base() { }

        public USDRateRepository(ISession session) : base(session) { }

        public USDRate USDRateGetByDate(DateTime date)
        {
            return _session.QueryOver<USDRate>()
                .Where(x => x.ValidFrom <= date)
                .OrderBy(x => x.ValidFrom).Desc
                .Take(1)
                .FutureValue().Value;
        }
    }
}