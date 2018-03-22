using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CMS.Core.Domain;

namespace CMS.Web.Domain
{
    public class AnswerGroup
    {
        public virtual int Id { get; set; }
        public virtual QuestionGroup Group { get; set; }
        public virtual AnswerSheet AnswerSheet { get; set; }
        public virtual string Comment { get; set; }
    }
}
