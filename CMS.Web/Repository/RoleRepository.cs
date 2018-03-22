using CMS.Core.Domain;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Web.Repository
{
    public class RoleRepository : RepositoryBase<Role>
    {
        public RoleRepository() : base() { }

        public RoleRepository(ISession session) : base(session) { }

        public Role RoleGetById(int roleId)
        {
            return _session.QueryOver<Role>()
                .Where(x => x.Id == roleId).SingleOrDefault();
        }
    }
}