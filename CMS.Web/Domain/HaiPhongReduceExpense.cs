using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Web.Domain
{
    public class HaiPhongReduceExpense
    {
        public virtual int HaiPhongReduceExpenseId { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<HaiPhongExpense> HaiPhongExpenses { get; set; }
        public virtual HaiPhongExpenseCustomerType HaiPhongExpenseCustomerType { get; set; }
        public virtual HaiPhongExpenseTypeHaiPhongExpenseCustomerType HaiPhongExpenseTypeHaiPhongExpenseCustomerType { get; set; }
    }
}