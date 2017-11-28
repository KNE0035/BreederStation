using BreederStationBussinessLayer.Domain;
using BreederStationBussinessLayer.Domain.Enums;
using BreederStationBussinessLayer.Service;
using BreederStationBussinessLayer.ValidationObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BreederStationDesktopView
{
    public partial class AddEmployeeForm : Form
    {
        Person person = new Person();

        private const int MAX_LOGIN_LENGTH = 20;
        private const int MAX_FIRST_LAST_NAME_LENGTH = 30;
        private const int MAX_GROUP_INFO_HEIGHT = 200;

        private PersonService personService = ServiceRegister.getInstance().Get<PersonService>();
        private AnimalGroupService animalgGroupService = ServiceRegister.getInstance().Get<AnimalGroupService>();
        private IList<AnimalGroup> animalGroups;
        private Size implicitSize;
        private const int FORM_BOTTOM_OFFSET = 85;
        public AddEmployeeForm()
        {
            InitializeComponent();
            implicitSize = this.Size;
            LoadAnimalGroupSelect();
        }

        private void LoadAnimalGroupSelect()
        {
            animalGroups = animalgGroupService.GetAllAnimalGroups(false);
            ComboAnimalGroup.DisplayMember = "Description";
            ComboAnimalGroup.ValueMember = "Id";
            ComboAnimalGroup.DataSource = animalGroups;
        }

        private void RoleGroupBox_Enter(object sender, EventArgs e)
        {
        }

        private void Admin_CheckedChanged(object sender, EventArgs e)
        {
            ChemicalQualification.Visible = false;
            ChemicalQualification.Enabled = false;
            CleanerLabel.Visible = false;

            BreederGroupBox.Visible = false;
            BreederGroupBox.Enabled = false;
            this.Size = this.implicitSize;
            AddEmployeeButton.Top = this.Height - FORM_BOTTOM_OFFSET;
        }

        private void Director_CheckedChanged(object sender, EventArgs e)
        {
            ChemicalQualification.Visible = false;
            ChemicalQualification.Enabled = false;
            CleanerLabel.Visible = false;

            BreederGroupBox.Visible = false;
            BreederGroupBox.Enabled = false;
            this.Size = this.implicitSize;
            AddEmployeeButton.Top = this.Height - FORM_BOTTOM_OFFSET;
        }

        private void Breeder_CheckedChanged(object sender, EventArgs e)
        {
            const int OFFSET = 50;
            ChemicalQualification.Visible = false;
            ChemicalQualification.Enabled = false;
            CleanerLabel.Visible = false;

            BreederGroupBox.Visible = true;
            BreederGroupBox.Enabled = true;
            this.Size = this.implicitSize;
            this.Height = this.Size.Height + BreederGroupBox.Size.Height + OFFSET;
            AddEmployeeButton.Top = this.Height - FORM_BOTTOM_OFFSET;
        }

        private void Cleaner_CheckedChanged(object sender, EventArgs e)
        {
            ChemicalQualification.Visible = true;
            ChemicalQualification.Enabled = true;
            CleanerLabel.Visible = true;

            BreederGroupBox.Visible = false;
            BreederGroupBox.Enabled = false;

            this.Size = this.implicitSize;
            this.Height = this.Size.Height + 100;
            AddEmployeeButton.Top = this.Height - FORM_BOTTOM_OFFSET;
        }

        private void ComboAnimalGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            GroupId.Text = (ComboAnimalGroup.SelectedValue).ToString();
        }

        private void groupInfoButton_Click(object sender, EventArgs e)
        {
            if (AnimalGroupDataGrid.Visible)
            {
                AnimalGroupDataGrid.Visible = false;
                BreederGroupBox.Height = BreederGroupBox.Size.Height - AnimalGroupDataGrid.Height;
                this.Height = this.Size.Height - AnimalGroupDataGrid.Height;
                AddEmployeeButton.Top = this.Height - FORM_BOTTOM_OFFSET;
                groupInfoButton.Text = "Zobraz informace o skupinách";
            }
            else
            {
                IList<AnimalGroup> animalGroups = animalgGroupService.GetAllAnimalGroups(true); // true - selects detail info about animal groups
                BindingList<AnimalGroup> bindingList = new BindingList<AnimalGroup>(animalGroups);
                AnimalGroupDataGrid.AutoGenerateColumns = false;
                AnimalGroupDataGrid.DataSource = bindingList;
                int datagridHeight = GetDataGridViewHeight(AnimalGroupDataGrid);

                if (datagridHeight > MAX_GROUP_INFO_HEIGHT) {
                    datagridHeight = MAX_GROUP_INFO_HEIGHT;
                }

                AnimalGroupDataGrid.Visible = true;
                AnimalGroupDataGrid.Height = datagridHeight;
                BreederGroupBox.Height = BreederGroupBox.Size.Height + datagridHeight;
                this.Height = this.Size.Height + datagridHeight;
                AddEmployeeButton.Top = this.Height - FORM_BOTTOM_OFFSET;
                groupInfoButton.Text = "Schovej informace o skupinách";
            }
        }

        private int GetDataGridViewHeight(DataGridView dataGridView)
        {
            const int heightOffset = 24;
            int sum = dataGridView.ColumnHeadersHeight + 
                      dataGridView.Rows.OfType<DataGridViewRow>().Where(r => r.Visible).Sum(r => r.Height) + heightOffset;
            return sum;
        }

        private void AddEmployeeButton_click(object sender, EventArgs e)
        {
            if (GetData())
            {
                PersonValidationObject personValidationObject = personService.RegisterUser(this.person);

                if (personValidationObject.DirectorDuplicity)
                {
                    ErrorProvider.SetError(Director, "V databázi již jeden ředitel je");
                    return;

                }

                if (personValidationObject.LoginDuplicity)
                {
                    ErrorProvider.SetError(Login, "Duplicitní login");
                    return;
                }

                Close();
            }
        }

        private bool GetData()
        {
            bool ret = true;
            person.Role = new Role();
            ErrorProvider.Clear();

            if (Login.Text != "")
            {
                if (Login.Text.Length > MAX_LOGIN_LENGTH)
                {
                    ErrorProvider.SetError(Login, "Délka loginu musí být menší než " + MAX_LOGIN_LENGTH + " znaků");
                    ret = false;
                }
                else
                {
                    person.Login = Login.Text;
                }
            }
            else
            {
                ErrorProvider.SetError(Login, "Vyplnte login prosím");
                ret = false;
            }

            if (FirstName.Text != "")
            {
                if (FirstName.Text.Length > MAX_FIRST_LAST_NAME_LENGTH)
                {
                    ErrorProvider.SetError(FirstName, "Délka jména musí být menší než " + MAX_FIRST_LAST_NAME_LENGTH + " znaků");
                    ret = false;
                }
                else
                {
                    person.FirstName = FirstName.Text;
                }
            }
            else
            {
                ErrorProvider.SetError(FirstName, "Vyplnte jméno prosím");
                ret = false;
            }

            if (LastName.Text != "")
            {
                if (LastName.Text.Length > MAX_FIRST_LAST_NAME_LENGTH)
                {
                    ErrorProvider.SetError(LastName, "Délka příjmení musí být menší než " + MAX_FIRST_LAST_NAME_LENGTH + " znaků");
                    ret = false;
                }
                else
                {
                    person.LastName = LastName.Text;
                } 
            }
            else
            {
                ErrorProvider.SetError(LastName, "Vyplnte příjmení prosím");
                ret = false;
            }

            if (Regex.IsMatch(Phone.Text, @"^\d{3} \d{3} \d{3} \d{3}$"))
            {
                person.Phone = Phone.Text;
            }
            else
            {
                ErrorProvider.SetError(Phone, "Telefonní číslo musí být ve tvaru: 'ddd ddd ddd ddd' d je číslo od 0 do 9");
                ret = false;
            }

            person.BirthDate = BirthDate.Value;

            if (ChemicalQualification.Enabled)
            {
                Cleaner cleaner = new Cleaner(person);
                cleaner.ChemicalQualification = ChemicalQualification.Checked;
                person = cleaner;
                person.Role.Type = RoleEnum.UKLIZEC;
            }

            if (BreederGroupBox.Enabled)
            {
                Breeder breeder = new Breeder(person);

                breeder.AnimalGroup = new AnimalGroup();
                breeder.AnimalGroup.Id = Int32.Parse(ComboAnimalGroup.SelectedValue.ToString());
                person = breeder;
                person.Role.Type = RoleEnum.CHOVATEL;
            }

            if (Admin.Checked)
            {
                person.Role.Type = RoleEnum.ADMIN;
            }

            else if (Director.Checked)
            {
                person.Role.Type = RoleEnum.REDITEL;
            }

            return ret;
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void GroupDescription_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
