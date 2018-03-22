using System;
using System.Reflection;
using System.Web;
using Castle.Core;
using Castle.Windsor;
using CMS.Core.Service;
using CMS.Core.Util;
using CMS.Web.Components;
using GemBox.Spreadsheet;
using log4net;
using log4net.Config;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using CMS.Core.Domain;


namespace CMS.Web
{
    public class Global : HttpApplication, IContainerAccessor
    {
        private static readonly string ERROR_PAGE_LOCATION = "~/Error.aspx";
        private static readonly ILog log = LogManager.GetLogger(typeof(Global));

        public Global()
        {
            InitializeComponent();
        }

        public IWindsorContainer Container
        {
            get { return IoC.Container; }
        }

        protected void Application_Start(Object sender, EventArgs e)
        {
            // Initialize Cuyahoga environment
            SpreadsheetInfo.SetLicense("EL6N-3AAA-AAAA-AAAB");

            // Set application level flags.
            HttpContext.Current.Application.Lock();
            HttpContext.Current.Application["ActiveUsers"] = 0;
            HttpContext.Current.Application["ModulesLoaded"] = false;
            HttpContext.Current.Application["IsModuleLoading"] = false;
            HttpContext.Current.Application["IsInstalling"] = false;
            HttpContext.Current.Application["IsUpgrading"] = false;
            HttpContext.Current.Application.UnLock();

            // Initialize log4net 
            XmlConfigurator.Configure();

            //HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();

            // Initialize Windsor
            IWindsorContainer container = new PortalContainer();
            container.Kernel.ComponentCreated += Kernel_ComponentCreated;
            container.Kernel.ComponentDestroyed += Kernel_ComponentDestroyed;

            // Inititialize the static Windsor helper class. 
            IoC.Initialize(container);

            // Check for any new versions
            CheckInstaller();

        }

        protected void Session_Start(Object sender, EventArgs e)
        {
        
        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
         
        }

        protected virtual void Application_EndRequest()
        {

        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            if (Context != null && Context.IsCustomErrorEnabled)
            {
                Server.Transfer(ERROR_PAGE_LOCATION, false);
            }
        }

        protected void Session_End(Object sender, EventArgs e)
        {
         
        }

        protected void Application_End(Object sender, EventArgs e)
        {
            IWindsorContainer container = IoC.Container;
            container.Kernel.ComponentCreated -= Kernel_ComponentCreated;
            container.Kernel.ComponentDestroyed -= Kernel_ComponentDestroyed;
            container.Dispose();
        }

        private void CheckInstaller()
        {
            // Check version and redirect to install pages if neccessary.
            DatabaseInstaller dbInstaller = new DatabaseInstaller(HttpContext.Current.Server.MapPath("~/Install/Core"),
                                                                  Assembly.Load("CMS.Core"));
            if (dbInstaller.TestDatabaseConnection())
            {
                if (dbInstaller.CanUpgrade)
                {
                    HttpContext.Current.Application.Lock();
                    HttpContext.Current.Application["IsUpgrading"] = true;
                    HttpContext.Current.Application.UnLock();

                    HttpContext.Current.Response.Redirect("~/Install/Upgrade.aspx");
                }
                else if (dbInstaller.CanInstall)
                {
                    HttpContext.Current.Application.Lock();
                    HttpContext.Current.Application["IsInstalling"] = true;
                    HttpContext.Current.Application.UnLock();
                    HttpContext.Current.Response.Redirect("~/Install/Install.aspx");
                }
            }
            else
            {
                throw new Exception("Cuyahoga can't connect to the database. Please check your application settings.");
            }
        }

        private void LoadModules()
        {
            if (log.IsDebugEnabled)
            {
                log.Debug("Entering module loading.");
            }
            // Load module types into the container.
            ModuleLoader loader = Container.Resolve<ModuleLoader>();
            loader.RegisterActivatedModules();

            if (log.IsDebugEnabled)
            {
                log.Debug("Finished module loading. Now redirecting to self.");
            }
            // Re-load the requested page (to avoid conflicts with first-time configured NHibernate modules )
            HttpContext.Current.Response.Redirect(HttpContext.Current.Request.RawUrl);
        }

        private void Kernel_ComponentCreated(ComponentModel model, object instance)
        {
            if (log.IsDebugEnabled)
            {
                log.Debug("Component created: " + instance);
            }
        }

        private void Kernel_ComponentDestroyed(ComponentModel model, object instance)
        {
            if (log.IsDebugEnabled)
            {
                log.Debug("Component destroyed: " + instance);
            }
        }

        #region Web Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        }

        #endregion
    }
}