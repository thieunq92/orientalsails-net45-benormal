using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Web.Domain
{
    public class HaiPhongExpenseTypeHaiPhongExpenseCustomerType
    {
        public virtual int HaiPhongExpenseTypeHaiPhongExpenseCustomerTypeId { get; set; }
        public virtual int OrderNumber { get; set; }
        public virtual HaiPhongExpenseType HaiPhongExpenseType { get; set; }
        public virtual HaiPhongExpenseCustomerType HaiPhongExpenseCustomerType { get; set; }
        public virtual IList<HaiPhongExpense> HaiPhongExpenses { get; set; }
        public virtual IList<HaiPhongReduceExpense> HaiPhongReduceExpenses { get; set; }
    }
}