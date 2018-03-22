using System;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.Web.UI
{
    /// <summary>
    /// Summary description for LocalizedUserControl.
    /// </summary>
    public class LocalizedUserControl : UserControl
    {
        private readonly CultureInfo _currentUICulture;
        private readonly ResourceManager _resMan;

        public LocalizedUserControl()
        {
            // Base name of the resources consists of Namespace.Resources.Strings
            string baseName = GetType().BaseType.Namespace + ".Resources.Strings";
            _resMan = new ResourceManager(baseName, GetType().BaseType.Assembly);
            try
            {
                _resMan.GetString("Test");
            }
            catch (MissingManifestResourceException ex)
            {
                baseName = GetType().BaseType.Namespace + ".Resources";
                _resMan = new ResourceManager(baseName, GetType().BaseType.Assembly);
            }
            _currentUICulture = Thread.CurrentThread.CurrentUICulture;
        }

        /// <summary>
        /// Get a localized text string for a given key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected virtual string GetText(string key)
        {
            return _resMan.GetString(key, _currentUICulture);
        }

        /// <summary>
        /// Try to localize the controls. 
        /// The resource keys should consist of the name of the User Control class
        /// with the ID of the control added. For example "Articles.lblTitle".
        /// </summary>
        protected virtual void LocalizeControls()
        {
            LocalizeControls(this);
        }

        /// <summary>
        /// Try to localize the controls. 
        /// The resource keys should be the same as the ID of the control that should be translated.
        /// You can override this behavior by adding a prefix with the UserControl name and a 
        /// semicolon, for example: "Articles:lblTitle".
        /// </summary>
        /// <param name="control"></param>
        protected virtual void LocalizeControls(Control control)
        {
            foreach (Control childControl in control.Controls)
            {
                // First try to find a string for this specific user control.
                // Use the name of BaseType because we need the code-behind class.
                string resourceKey = GetType().BaseType.Name + ":" + childControl.ID;
                string localizedText = GetText(resourceKey);

                if (localizedText == null)
                {
                    // No translation found with the user control prefix. Try to find a translation that
                    // only has the control ID as key
                    if (childControl.ID != null)
                    {
                        localizedText = GetText(childControl.ID);
                    }
                }

                if (! String.IsNullOrEmpty(localizedText))
                {
                    if ((childControl is Label) && ! (childControl is BaseValidator))
                    {
                        Label label = (Label) childControl;
                        label.Text = localizedText;
                    }
                    if (childControl is Button)
                    {
                        Button button = (Button) childControl;
                        button.Text = localizedText;
                    }
                    if (childControl is LinkButton)
                    {
                        LinkButton linkButton = (LinkButton) childControl;
                        linkButton.Text = localizedText;
                    }
                    if (childControl is HyperLink)
                    {
                        HyperLink hyperLink = (HyperLink) childControl;
                        hyperLink.Text = localizedText;
                    }
                    if (childControl is RadioButton)
                    {
                        RadioButton radioButton = (RadioButton) childControl;
                        radioButton.Text = localizedText;
                    }
                    else if (childControl is BaseValidator)
                    {
                        BaseValidator validator = (BaseValidator) childControl;
                        validator.ErrorMessage = localizedText;
                    }
                    else if (childControl is CheckBox)
                    {
                        CheckBox checkBox = (CheckBox)childControl;
                        checkBox.Text = localizedText;
                    }
                }
                // Recursive translate childcontrols
                LocalizeControls(childControl);
            }
        }

        /// <summary>
        /// Recursively databind controls that might have localized texts.
        /// </summary>
        protected virtual void BindResources()
        {
            BindResources(this);
        }

        private void BindResources(Control control)
        {
            foreach (Control childControl in control.Controls)
            {
                if (childControl is Label || childControl is Button || childControl is BaseValidator)
                {
                    childControl.DataBind();
                }
                BindResources(childControl);
            }
        }
    }
}