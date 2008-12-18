using System;

using DevExpress.Xpo;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using System.Collections.ObjectModel;
using DevExpress.Data.Filtering;

namespace AIR_ERP.Module
{
    [NonPersistent]
    public class MyLogonParameters : INotifyPropertyChanged
    {
        private ObjectSpace objectSpace;
        private ReadOnlyCollection<Company> availableCompanies;
        private XPCollection<Employee> availableUsers;

        private Company company;
        private Employee employee;
        private string password;

        private void RefreshAvailableUsers()
        {
            if (availableUsers == null)
            {
                return;
            }

            availableUsers.Criteria = new BinaryOperator("Company", Company);

            if (employee != null && availableUsers.IndexOf(employee) == -1)
            {
                Employee = null;
            }
        }

        [Browsable(false)]
        public ObjectSpace ObjectSpace
        {
            get { return objectSpace; }
            set { objectSpace = value; }
        }
        [Browsable(false)]
        public ReadOnlyCollection<Company> AvailableCompanies
        {
            get
            {
                if (objectSpace == null)
                {
                    throw new InvalidOperationException("objectSpace is null");
                }
                if (availableCompanies == null)
                {
                    availableCompanies = new ReadOnlyCollection<Company>(ObjectSpace.CreateCollection<Company>());
                }
                return availableCompanies;
            }
        }
        [Browsable(false)]
        public XPCollection<Employee> AvailableUsers
        {
            get
            {
                if (availableUsers == null)
                {
                    availableUsers = new XPCollection<Employee>(ObjectSpace.Session);
                    availableUsers.BindingBehavior = CollectionBindingBehavior.AllowNone;
                    RefreshAvailableUsers();
                }
                return availableUsers;
            }
        }

        [DataSourceProperty("AvailableCompanies"), ImmediatePostData]
        public Company Company
        {
            get { return company; }
            set
            {
                company = value;
                RefreshAvailableUsers();
            }
        }

        [DataSourceProperty("AvailableUsers"), ImmediatePostData]
        public Employee Employee
        {
            get { return employee; }
            set
            {
                employee = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Employee"));
                }
            }
        }

        [PasswordPropertyText(true)]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

}
