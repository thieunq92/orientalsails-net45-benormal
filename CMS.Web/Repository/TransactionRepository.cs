using NHibernate;
using CMS.Web.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Web.Repository
{
    public class TransactionRepository : RepositoryBase<Transaction>
    {
        public TransactionRepository() : base() { }

        public TransactionRepository(ISession session) : base() { }
    }
}