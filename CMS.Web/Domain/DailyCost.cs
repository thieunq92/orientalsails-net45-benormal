using System.Collections;
using CMS.Core.Domain;

namespace CMS.Web.Domain
{
    public class DailyCost
    {
        public virtual int Id { get; set; }
        public virtual CostType Type { get; set; }
        public virtual DailyCostTable Table { get; set; }
        public virtual double Cost { get; set; }

    }
}
