using System;
using System.IO;
using DevExpress.ExpressApp.Updating;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Reports;
using System.Data;

namespace AIR_ERP.Module
{
    public class Updater : ModuleUpdater
    {
        public Updater(Session session, Version currentDBVersion) : base(session, currentDBVersion) { }
        public override void UpdateDatabaseAfterUpdateSchema()
        {
            base.UpdateDatabaseAfterUpdateSchema();
            Employee userAdmin = Session.FindObject<Employee>(new BinaryOperator("UserName", "Admin"));
            if (new XPCollection<Company>(Session).Count == 0)
            {
                using (Role administratorRole = new Role(Session) { Name = "Administrator role" })
                {
                    administratorRole.AddPermission(new EditModelPermission());
                    administratorRole.AddPermission(new ObjectAccessPermission(typeof(object), ObjectAccess.AllAccess));
                    administratorRole.Save();
                    using (Role userRole = new Role(Session) { Name = "User" })
                    {
                        userRole.AddPermission(new ObjectAccessPermission(typeof(object), ObjectAccess.Navigate));
                        userRole.AddPermission(new ObjectAccessPermission(typeof(object), ObjectAccess.Read));
                        userRole.Save();
                    }
                    userAdmin = new Employee(Session);
                    userAdmin.UserName = "Admin";
                    userAdmin.FirstName = "Admin";
                    userAdmin.LastName = "";
                    userAdmin.Roles.Add(administratorRole);
                    userAdmin.Save();
                    using (Branch branch1 = new Branch(Session) { Acronym = "WTPR", Branchname = "Roadmax Head Office" })
                    {
                        using (Company company1 = new Company(Session) { CompanyName = "Roadmax Marketing Inc.", Acronym = "WTPR" })
                        {
                            company1.Employees.Add(userAdmin);
                            company1.Branches.Add(branch1);
                            company1.Save();
                            ImportData(company1);
                        }
                    }
                }

                using (Currency curr = new Currency(Session) { CurrencyName = "Peso", CurrencyRate = 1 })
                {
                    curr.Save();
                }

                using (VatCategory vat = new VatCategory(Session) { Acronym = "VAT", Description = "Vatable", Rate = 12 })
                {
                    vat.Save();
                }


            }
            //importReport("SOHeaderPrintOut");
        }

        private void ImportData(Company co)
        {
            //Create a new ExcelReader 
            using (utilities.ExcelReader exr = new utilities.ExcelReader { ExcelFilename = @"Maintenance.xls", Headers = true, MixedData = true, SheetName = "AccountType", KeepConnectionOpen = true })
            {
                //Create a datable to get the data
                DataTable accountTypeDTable = new DataTable("AccountType");
                accountTypeDTable = exr.GetTable();
                //Iterate the data and create the objects
                foreach (DataRow accountTypeRow in accountTypeDTable.Rows)
                {
                    using (AccountType accountType = new AccountType(Session) { Acronym = accountTypeRow["Acronym"].ToString().Trim(), Description = accountTypeRow["Description"].ToString().Trim(), IsActive = true })
                    {
                        accountType.Save();
                    }
                }
                //Create a datable to get the data
                DataTable bankDTable = new DataTable("Bank");
                exr.SheetName = "Bank";
                bankDTable = exr.GetTable();
                //Iterate the data and create the objects
                foreach (DataRow bankRow in bankDTable.Rows)
                {
                    using (Bank bank = new Bank(Session) { Acronym = bankRow["Acronym"].ToString().Trim(), BankName = bankRow["BankName"].ToString().Trim(), IsActive = true })
                    {
                        bank.Save();
                    }
                }
                DataTable bpartnerDTable = new DataTable("BusinessPartnerType");
                exr.SheetName = "BusinessPartnerType";
                bpartnerDTable = exr.GetTable();
                //Iterate the data and create the objects
                foreach (DataRow bpTypeRow in bpartnerDTable.Rows)
                {
                    using (BusinessPartnerType bpType = new BusinessPartnerType(Session) { Description = bpTypeRow["Description"].ToString().Trim(), IsActive = true })
                    {
                        bpType.Save();
                    }
                }
                DataTable chartOfAcctTable = new DataTable("ChartOfAccounts");
                exr.SheetName = "ChartOfAccounts";
                chartOfAcctTable = exr.GetTable();
                //Iterate the data and create the objects
                foreach (DataRow cohRow in chartOfAcctTable.Rows)
                {
                    using (ChartOfAccounts chartOfAccounts = new ChartOfAccounts(Session) { Description = cohRow["Description"].ToString().Trim(), AccountId = cohRow["AccountId"].ToString().Trim() })
                    {
                        if ("Y" == cohRow["IsParent"].ToString().Trim())
                            chartOfAccounts.IsParent = true;
                        else
                            chartOfAccounts.IsParent = false;
                        chartOfAccounts.IsActive = true;
                        chartOfAccounts.Company = co;
                        chartOfAccounts.Save();
                    }
                }
                //Create a datable to get the data
                DataTable termDTable = new DataTable("Term");
                exr.SheetName = "Term";
                termDTable = exr.GetTable();
                //Iterate the data and create the objects
                foreach (DataRow termRow in termDTable.Rows)
                {
                    using (Term term = new Term(Session) { Acronym = termRow["NumOfDays"].ToString().Trim(), TermDescription = termRow["Description"].ToString().Trim(), NumOfDays = Convert.ToDecimal(termRow["NumOfDays"]), IsActive = true })
                    {
                        term.Save();
                    }
                }
                //Create a datable to get the data
                DataTable uomDTable = new DataTable("UOM");
                exr.SheetName = "UOM";
                uomDTable = exr.GetTable();
                //Iterate the data and create the objects
                foreach (DataRow uomRow in uomDTable.Rows)
                {
                    using (UnitOfMeasure unitOfMeasure = new UnitOfMeasure(Session) { Acronym = uomRow["Acronym"].ToString().Trim(), Description = uomRow["Description"].ToString().Trim(), IsActive = true })
                    {
                        unitOfMeasure.Save();
                    }
                }
            }
        }

        private void importReport(String reportName)
        {
            ReportData reportdata = Session.FindObject<ReportData>(new BinaryOperator("Name", reportName));
            if (reportdata == null)
            {
                reportdata = new ReportData(Session);
            }
            reportdata.IsInplaceReport = false;
            using (XafReport rep = new XafReport { ReportName = reportName, ObjectSpace = new ObjectSpace(new UnitOfWork(Session.DataLayer), XafTypesInfo.Instance) })
            {
                rep.LoadLayout(GetType().Assembly.GetManifestResourceStream(String.Format("AIR_ERP.Module.AR.Reports.{0}.repx", reportName)));
                reportdata.SaveXtraReport(rep);
            }
            reportdata.Save();
            //XafReport rep = new XafReport();
            //rep.ReportName = reportName;
            //rep.ObjectSpace = new ObjectSpace(new UnitOfWork(Session.DataLayer), XafTypesInfo.Instance);
            //rep.LoadLayout(GetType().Assembly.GetManifestResourceStream("AIR_ERP.Module.AR.Reports." + reportName + ".repx"));
            //reportdata.SaveXtraReport(rep);
            //reportdata.Save();
        }
    }
}
