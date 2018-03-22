using CMS.Web.Domain;
using CMS.Web.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Web.Utils
{
    public class CustomerUtil
    {
        public CustomerRepository CustomerRepository { get; set; }

        public CustomerUtil()
        {
            CustomerRepository = new CustomerRepository();
        }

        public void Dispose()
        {
            if (CustomerRepository != null)
            {
                CustomerRepository.Dispose();
                CustomerRepository = null;
            }
        }
    }
}