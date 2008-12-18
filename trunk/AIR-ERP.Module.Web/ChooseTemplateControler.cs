using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;
using System.Web;

namespace AIR_ERP.Module
{
    public partial class ChooseTemplateController : ViewController
    {
        public ChooseTemplateController()
        {
            InitializeComponent();
            CreateActionItems();
            RegisterActions(components);
        }
        private void CreateActionItems()
        {
            ChoiceActionItem defaultTemplateItem = new ChoiceActionItem("Horizontal navigation", "Default.aspx");
            ChooseTemplateAction.Items.Add(defaultTemplateItem);
            ChoiceActionItem defaultVerticalTemplateItem = new ChoiceActionItem("Vertical navigation", "DefaultVertical.aspx");
            ChooseTemplateAction.Items.Add(defaultVerticalTemplateItem);
        }
        private void ChooseTemplateAction_Execute(object sender, DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventArgs e)
        {
            string appPath = HttpContext.Current.Request.ApplicationPath;
            HttpContext.Current.Response.Redirect(appPath.EndsWith("\\") ?
                appPath : appPath + "\\"
                + e.SelectedChoiceActionItem.Data.ToString() + HttpContext.Current.Request.Url.Query);
        }
        private void ClearFieldsAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            foreach (PropertyEditor item in ((DetailView)View).GetItems<PropertyEditor>())
            {
                if (!item.ReadOnly)
                {
                    try
                    {
                        item.PropertyValue = null;
                    }
                    catch (IntermediateMemberIsNullException)
                    {
                        item.Refresh();
                    }
                }
            }
        }
        private void ChooseTemplateController_Activated(object sender, System.EventArgs e)
        {
            Frame.TemplateChanged += new EventHandler(Frame_TemplateChanged);
        }

        void Frame_TemplateChanged(object sender, EventArgs e)
        {
            UpdateSelectedItem();
        }
        private void ChooseTemplateController_Deactivating(object sender, System.EventArgs e)
        {
            Frame.TemplateChanged -= new EventHandler(Frame_TemplateChanged);
        }
        private void UpdateSelectedItem()
        {
            foreach (ChoiceActionItem item in ChooseTemplateAction.Items)
            {
                if (HttpContext.Current.Request.Url.AbsolutePath.ToLower().Contains(item.Data.ToString().ToLower()))
                {
                    ChooseTemplateAction.SelectedItem = item;
                    break;
                }
            }
        }
    }
}
