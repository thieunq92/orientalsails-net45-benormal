using System;
using CMS.Core.Domain;

namespace CMS.Web.Domain
{
    public class BookingSale
    {
        public virtual int Id { get; set; }
        public virtual User Sale { get; set; }
    }
}
