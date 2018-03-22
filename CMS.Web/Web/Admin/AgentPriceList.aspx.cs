using System;
using System.Collections;
using System.Web.UI.WebControls;
using CMS.Core.Domain;
using log4net;
using CMS.Web.Domain;
using CMS.Web.Web.UI;

namespace CMS.Web.Web.Admin
{
    public partial class AgentPriceList : SailsAdminBasePage
    {
        #region -- Private Member --

        private readonly ILog _logger = LogManager.GetLogger(typeof (AgentPriceList));
        private Role _role;

        #endregion

        #region -- Page Event --

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra quyền Admin
                if (!UserIdentity.HasPermission(AccessLevel.Administrator))
                {
                    panelContent.Visible = false;
                    ShowError(Resources.stringAccessDenied);
                    return;
                }

                _role = Module.RoleGetById(Convert.ToInt32(Request.QueryString["RoleId"]));
                Title = string.Format(Resources.titleAdminAgencyEdit, _role.Name);
                if (!IsPostBack)
                {
                    GetDataSource(_role);
                    rptPrices.DataBind();
                    lblRoleName.Text = _role.Name;
                    lblUserCount.Text = _role.Users.Count.ToString();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error when Page_Load in AgentPriceList", ex);
                ShowError(ex.Message);
            }
        }

        #endregion

        #region -- Private Method --

        private void GetDataSource(Role role)
        {
            lblError.Text = string.Empty;
            IList list = Module.AgencyPolicyGetByRole(role);
            double current = 0;
            foreach (AgencyPolicy agent in list)
            {
                if (current == -1)
                {
                    lblError.Text = string.Format(Resources.messageErrorTwoMaxValue);
                    break;
                }
                double next = agent.CostFrom;
                if (current > next)
                {
                    lblError.Text = string.Format(Resources.messageErrorOneValueBelongToTwoRange, current, next);
                }

                if (current + 1 < next)
                {
                    lblError.Text = string.Format(Resources.messageErrorDontHaveRangeFor, current + 1, next - 1);
                }

                if (agent.CostTo != null)
                {
                    current = agent.CostTo.Value;
                }
                else
                {
                    current = -1;
                }
            }
            rptPrices.DataSource = list;
        }

        #endregion

        #region -- Control Event --

        protected void btnAddPrice_Click(object sender, EventArgs e)
        {
            txtFrom.Text = string.Empty;
            txtTo.Text = string.Empty;
            txtPercentage.Text = string.Empty;
            txtHiddenId.Text = "-1";
            programmaticModalPopup.Show();
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                AgencyPolicy agent;
                if (txtHiddenId.Text == "-1")
                {
                    agent = new AgencyPolicy();
                }
                else
                {
                    agent = Module.AgencyPolicyGetById(Convert.ToInt32(txtHiddenId.Text));
                }
                Role role = Module.RoleGetById(Convert.ToInt32(Request.QueryString["RoleId"]));
                agent.Role = role;
                agent.CostFrom = Convert.ToDouble(txtFrom.Text);
                double db;
                if (Double.TryParse(txtTo.Text, out db))
                {
                    agent.CostTo = db;
                }
                else
                {
                    agent.CostTo = null;
                }
                agent.Percentage = Convert.ToDouble(txtPercentage.Text);
                agent.IsPercentage = ddlUnit.SelectedIndex == 0;

                agent.ModuleType = "ORIENTALSAILS";
                if (agent.Id > 0)
                {
                    Module.Update(agent);
                }
                else
                {
                    Module.Save(agent);
                }
                GetDataSource(role);
                rptPrices.DataBind();
                programmaticModalPopup.Hide();
            }
            catch (Exception ex)
            {
                _logger.Error("Error when btnOK_Click in AgenPriceList", ex);
                ShowError(ex.Message);
            }
        }

        protected void rptPrices_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                AgencyPolicy agent;
                switch (e.CommandName)
                {
                    case "edit":
                        agent = Module.AgencyPolicyGetById(Convert.ToInt32(e.CommandArgument));
                        txtHiddenId.Text = agent.Id.ToString();
                        txtFrom.Text = agent.CostFrom.ToString();
                        txtTo.Text = agent.CostTo.ToString();
                        txtPercentage.Text = agent.Percentage.ToString();
                        programmaticModalPopup.Show();
                        break;
                    case "delete":
                        agent = Module.AgencyPolicyGetById(Convert.ToInt32(e.CommandArgument));
                        Module.Delete(agent);
                        GetDataSource(_role);
                        rptPrices.DataBind();
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error when rptPrices_ItemCommand in AgenPrceList", ex);
                ShowError(ex.Message);
            }
        }

        protected void rptPrices_DataBound(object sender, RepeaterItemEventArgs e)
        {
            AgencyPolicy agent = e.Item.DataItem as AgencyPolicy;
            if (agent != null)
            {
                using (Label lbl = e.Item.FindControl("lblCostTo") as Label)
                {
                    if (lbl != null)
                    {
                        if (agent.CostTo.HasValue)
                        {
                            lbl.Text = ((AgencyPolicy) e.Item.DataItem).CostTo.Value.ToString();
                        }
                        else
                        {
                            lbl.Text = Resources.textAndUp;
                        }
                    }
                }
                using (Label lblApply = e.Item.FindControl("lblApply") as Label)
                {
                    if (lblApply != null)
                    {
                        if (agent.IsPercentage)
                        {
                            lblApply.Text = string.Format("{0}%", ((AgencyPolicy) e.Item.DataItem).Percentage);
                        }
                        else
                        {
                            lblApply.Text = string.Format("+{0}$", ((AgencyPolicy) e.Item.DataItem).Percentage);
                        }
                    }
                }
            }
        }

        #endregion
    }
}