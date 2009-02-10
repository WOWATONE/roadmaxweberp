namespace AIR_ERP.Module.Win
{
    partial class PrintOutViewController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.printOut = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // printOut
            // 
            this.printOut.Caption = "Print Out";
            this.printOut.Category = "RecordEdit";
            this.printOut.Id = "ShowPrintOut";
            this.printOut.ImageName = "MenuBar_Print";
            this.printOut.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.printOut.ToolTip = "Print this current transaction";
            this.printOut.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.printOut_Execute);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction printOut;
    }
}
