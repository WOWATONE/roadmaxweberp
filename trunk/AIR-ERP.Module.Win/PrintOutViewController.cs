using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Reports;
using DevExpress.Data.Filtering;
using DevExpress.XtraReports.UI;

namespace AIR_ERP.Module.Win
{
    public partial class PrintOutViewController : ViewController
    {
        public PrintOutViewController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void printOut_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ReportData reportdata = ObjectSpace.FindObject<ReportData>(new BinaryOperator("Name", View.ObjectType.Name + "PrintOut"));
            XtraReport xr = reportdata.LoadXtraReport(ObjectSpace);
            xr.DataSource = View.SelectedObjects;
            xr.ShowPreviewDialog();
        }
    }
}
