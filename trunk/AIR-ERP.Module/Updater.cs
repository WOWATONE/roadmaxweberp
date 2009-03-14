using System;

using DevExpress.ExpressApp.Updating;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Security;
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
                Role administratorRole;
                administratorRole = new Role(Session);
                administratorRole.Name = "Administrator role";
                administratorRole.AddPermission(new EditModelPermission());
                administratorRole.AddPermission(new ObjectAccessPermission(typeof(object), ObjectAccess.AllAccess));
                administratorRole.Save();

                Role userRole;
                userRole = new Role(Session);
                userRole.Name = "User";
                userRole.AddPermission(new ObjectAccessPermission(typeof(object), ObjectAccess.Navigate));
                userRole.AddPermission(new ObjectAccessPermission(typeof(object), ObjectAccess.Read));
                userRole.Save();

                userAdmin = new Employee(Session);
                userAdmin.UserName = "Admin";
                userAdmin.FirstName = "Admin";
                userAdmin.LastName = "";
                userAdmin.Roles.Add(administratorRole);
                userAdmin.Save();

                Branch branch1 = new Branch(Session);
                branch1.Acronym = "WTPR";
                branch1.Branchname = "Roadmax Head Office";

                Company company1 = new Company(Session);
                company1.CompanyName = "Roadmax Marketing Inc.";
                company1.Acronym = "WTPR";
                company1.Employees.Add(userAdmin);
                company1.Branches.Add(branch1);
                company1.Save();

                ImportData(company1);

                Currency curr = new Currency(Session);
                curr.CurrencyName = "Peso";
                curr.CurrencyRate = 1;
                curr.Save();

            }
        }

        private void ImportData(Company co)
        {
            //Create a new ExcelReader 
            utilities.ExcelReader exr = new utilities.ExcelReader();
            exr.ExcelFilename = @"Maintenance.xls";
            exr.Headers = true;
            exr.MixedData = true;
            exr.SheetName = "AccountType";
            exr.KeepConnectionOpen = true;

            //Create a datable to get the data
            DataTable accountTypeDTable = new DataTable("AccountType");
            accountTypeDTable = exr.GetTable();

            //Iterate the data and create the objects
            foreach (DataRow accountTypeRow in accountTypeDTable.Rows)
            {
                AccountType accountType = new AccountType(Session);
                accountType.Acronym = accountTypeRow["Acronym"].ToString().Trim();
                accountType.Description = accountTypeRow["Description"].ToString().Trim();
                accountType.IsActive = true;
                accountType.Save();
            }

            //Create a datable to get the data
            DataTable bankDTable = new DataTable("Bank");
            exr.SheetName = "Bank";
            bankDTable = exr.GetTable();

            //Iterate the data and create the objects
            foreach (DataRow bankRow in bankDTable.Rows)
            {
                Bank bank = new Bank(Session);
                bank.Acronym = bankRow["Acronym"].ToString().Trim();
                bank.BankName = bankRow["BankName"].ToString().Trim();
                bank.IsActive = true;
                bank.Save();
            }

            DataTable bpartnerDTable = new DataTable("BusinessPartnerType");
            exr.SheetName = "BusinessPartnerType";
            bpartnerDTable = exr.GetTable();

            //Iterate the data and create the objects
            foreach (DataRow bpTypeRow in bpartnerDTable.Rows)
            {
                BusinessPartnerType bpType = new BusinessPartnerType(Session);
                bpType.Description = bpTypeRow["Description"].ToString().Trim();
                bpType.IsActive = true;
                bpType.Save();
            }

            DataTable chartOfAcctTable = new DataTable("ChartOfAccounts");
            exr.SheetName = "ChartOfAccounts";
            chartOfAcctTable = exr.GetTable();

            //Iterate the data and create the objects
            foreach (DataRow cohRow in chartOfAcctTable.Rows)
            {
                ChartOfAccounts chartOfAccounts = new ChartOfAccounts(Session);
                chartOfAccounts.Description = cohRow["Description"].ToString().Trim();
                chartOfAccounts.AccountId = cohRow["AccountId"].ToString().Trim();
                if ( "Y" == cohRow["IsParent"].ToString().Trim() )
                {
                    chartOfAccounts.IsParent = true;
                }
                else
                {
                    chartOfAccounts.IsParent = false;
                }
                chartOfAccounts.IsActive = true;
                chartOfAccounts.Company = co;
                chartOfAccounts.Save();
            }

            //Create a datable to get the data
            DataTable termDTable = new DataTable("Term");
            exr.SheetName = "Term";
            termDTable = exr.GetTable();

            //Iterate the data and create the objects
            foreach (DataRow termRow in termDTable.Rows)
            {
                Term term = new Term(Session);
                term.Acronym = termRow["NumOfDays"].ToString().Trim();
                term.TermDescription = termRow["Description"].ToString().Trim();
                term.NumOfDays = Convert.ToDecimal(termRow["NumOfDays"]);
                term.IsActive = true;
                term.Save();
            }

            //Create a datable to get the data
            DataTable uomDTable = new DataTable("UOM");
            exr.SheetName = "UOM";
            uomDTable = exr.GetTable();

            //Iterate the data and create the objects
            foreach (DataRow uomRow in uomDTable.Rows)
            {
                UnitOfMeasure unitOfMeasure = new UnitOfMeasure(Session);
                unitOfMeasure.Acronym = uomRow["Acronym"].ToString().Trim();
                unitOfMeasure.Description = uomRow["Description"].ToString().Trim();
                unitOfMeasure.IsActive = true;
                unitOfMeasure.Save();
            }
        }
    }
}
