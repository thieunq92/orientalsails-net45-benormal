
using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace CMS.Web.Domain
{
    public class BookingServicePrice
    {
        public virtual int Id { get; set; }
        public virtual Booking Booking { get; set; }
        public virtual ExtraOption ExtraOption { get; set; }
        public virtual double UnitPrice { get; set; }

    }
}
