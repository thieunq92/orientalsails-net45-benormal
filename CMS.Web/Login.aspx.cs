using System;
using System.Web.Security;
using CMS.Web.HttpModules;
// using System.Data;

namespace CMS.Web
{
	/// <summary>
	/// Summary description for Login.
	/// </summary>
	public class Login : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox txtUsername;
		protected System.Web.UI.WebControls.TextBox txtPassword;
		protected System.Web.UI.WebControls.Label lblError;
		protected System.Web.UI.WebControls.Button btnLogin;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnLogin_Click(object sender, System.EventArgs e)
		{
			AuthenticationModule am = (AuthenticationModule)this.Context.ApplicationInstance.Modules["AuthenticationModule"];
			if (am.AuthenticateUser(txtUsername.Text, txtPassword.Text, false))
			{
				Context.Response.Redirect(FormsAuthentication.GetRedirectUrl(this.User.Identity.Name, false));
			}
			else
			{
				this.lblError.Text = "Invalid username or password.";
				this.lblError.Visible = true;
			}		
		}
	}
}
