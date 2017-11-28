using BreederStationBussinessLayer.Domain;
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
    public partial class AnimalEventForm : Form
    {

        private EventService animalEventService = ServiceRegister.getInstance().Get<EventService>();
        public AnimalEventForm()
        {
            InitializeComponent();
            RefreshData();
        }

        private void RefreshData()
        {
            IList<Event> animalEvents = animalEventService.GetAllAnimalEvents();
            BindingList<Event> bindingList = new BindingList<Event>(animalEvents);
            AnimalEventDataGrid.AutoGenerateColumns = false;
            AnimalEventDataGrid.DataSource = bindingList;
        }

        private void EmployeeDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AddNewAnimalEventButton_Click(object sender, EventArgs e)
        {
            AddAnimalEventForm form = new AddAnimalEventForm();
            form.ShowDialog();
            RefreshData();
        }
    }
}
