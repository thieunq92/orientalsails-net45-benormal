using System;
using System.Web;
using System.Web.Security;
using log4net;
using CMS.Core;
using CMS.Core.Service;
using CMS.Core.Domain;
using CMS.Core.Util;
using CMS.Core.Security;
using CMS.ServerControls;
using CMS.Web.Repository;

namespace CMS.Web.HttpModules
{
	public sealed class CookieHelper
	{
		private HttpRequestBase _request;
		private HttpResponseBase _response;

		public CookieHelper(HttpRequestBase request,
		HttpResponseBase response)
		{
			_request = request;
			_response = response;
		}

		//[DebuggerStepThrough()]
		public void SetLoginCookie(string userId, string password, bool isPermanentCookie)
		{
			if (_response != null)
			{
				if (isPermanentCookie)
				{
					FormsAuthenticationTicket userAuthTicket =
						new FormsAuthenticationTicket(1, userId, DateTime.Now,
						DateTime.MaxValue, true, password, FormsAuthentication.FormsCookiePath);
					string encUserAuthTicket = FormsAuthentication.Encrypt(userAuthTicket);
					HttpCookie userAuthCookie = new HttpCookie
						(FormsAuthentication.FormsCookieName, encUserAuthTicket);
					if (userAuthTicket.IsPersistent) userAuthCookie.Expires =
							userAuthTicket.Expiration;
					userAuthCookie.Path = FormsAuthentication.FormsCookiePath;
					_response.Cookies.Add(userAuthCookie);
				}
				else
				{
					FormsAuthentication.SetAuthCookie(userId, isPermanentCookie);
				}
			}
		}
	}

	/// <summary>
	/// HttpModule to extend Forms Authentication.
	/// </summary>
	public class AuthenticationModule : IHttpModule
	{
		private const int AUTHENTICATION_TIMEOUT = 20;
		private static readonly ILog log = LogManager.GetLogger(typeof(AuthenticationModule));

		public AuthenticationModule()
		{
		}

		public void Init(HttpApplication context)
		{
			context.AuthenticateRequest += new EventHandler(Context_AuthenticateRequest);
		}

		public void Dispose()
		{
			// Nothing here	
		}

		/// <summary>
		/// Try to authenticate the user.
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		public bool AuthenticateUser(string username, string password, bool persistLogin)
		{
			CoreRepository cr = (CoreRepository)HttpContext.Current.Items["CoreRepository"];
            UserRepository userRepository = new UserRepository ();
			string hashedPassword = Encryption.StringToMD5Hash(password);
			try
			{
                User user = userRepository.UserGetByUsernameAndPassword(username, hashedPassword);
				if (user != null)
				{
					if (! user.IsActive)
					{
						log.Warn(String.Format("Inactive user {0} tried to login.", user.UserName));
						throw new AccessForbiddenException("The account is disabled.");
					}
					user.IsAuthenticated = true;
					string currentIp = HttpContext.Current.Request.UserHostAddress;
					user.LastLogin = DateTime.Now;
					user.LastIp = currentIp;
					// Save login date and IP
					userRepository.SaveOrUpdate(user);
					// Create the authentication ticket
					HttpContext.Current.User = new BitPortalPrincipal(user);
					CookieHelper newCookieHelper = new CookieHelper(new HttpRequestWrapper(HttpContext.Current.Request), new HttpResponseWrapper(HttpContext.Current.Response));
					newCookieHelper.SetLoginCookie(user.Id.ToString(), password, true);
					log.Info(string.Format("User {0} login at {1}",user.UserName,DateTime.Now));

					return true;
				}
				log.Warn(String.Format("Invalid username-password combination: {0}:{1}.", username, password));
				return false;
			}
			catch (Exception ex)
			{
				log.Error(String.Format("An error occured while logging in user {0}.", username));
				throw new Exception(String.Format("Unable to log in user '{0}': " + ex.Message, username), ex);
			}
		}

		/// <summary>
		/// Log out the current user.
		/// </summary>
		public void Logout()
		{
			if (HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated)
			{
				FormsAuthentication.SignOut();
			}
		}

		private void Context_AuthenticateRequest(object sender, EventArgs e)
		{
			HttpApplication app = (HttpApplication)sender;
			if (app.Context.User != null && app.Context.User.Identity.IsAuthenticated)
			{
				CoreRepository cr = (CoreRepository)HttpContext.Current.Items["CoreRepository"];
				// There is a logged-in user with a standard Forms Identity. Replace it with
				// the cached Cuyahoga identity (the User class implements IIdentity). 				
                int userId = Int32.Parse(app.Context.User.Identity.Name);
                User cuyahogaUser = (User)cr.GetObjectById(typeof(User), userId);
				cuyahogaUser.IsAuthenticated = true;
				app.Context.User = new BitPortalPrincipal(cuyahogaUser);
			}
		}
	}
}
