using BreederStationBussinessLayer.Domain;
using BreederStationBussinessLayer.Domain.Enums;
using BreederStationBussinessLayer.SelectCriteria;
using BreederStationBussinessLayer.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BreederStationDesktopView
{
    public partial class EmployeeForm : Form
    {
        private PersonService personService = ServiceRegister.getInstance().Get<PersonService>();
        public EmployeeForm()
        {
            InitializeComponent();
            RefreshData();
            SetUserRoleRights();
        }

        private void RefreshData()
        {
            IList<Person> persons = personService.GetAllUsers(new PersonCriteria());
            BindingList<Person> bindingList = new BindingList<Person>(persons);
            EmployeeDataGrid.AutoGenerateColumns = false;
            EmployeeDataGrid.DataSource = bindingList;
        }

        private void AddNewEmployeeButton_Click(object sender, EventArgs e)
        {
            AddEmployeeForm form = new AddEmployeeForm();
            form.ShowDialog();
            RefreshData();
        }

        private void EmployeeDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void SetUserRoleRights()
        {
            UserSession userSession = UserSession.getInstance();
            if (userSession.Role.Type == RoleEnum.UKLIZEC || userSession.Role.Type == RoleEnum.CHOVATEL)
            {
                AddNewEmployeeButton.Visible = false;
                AddNewEmployeeButton.Enabled = false;
            }

        }
    }
}
