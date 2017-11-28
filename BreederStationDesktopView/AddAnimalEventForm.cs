using BreederStationBussinessLayer.Domain;
using BreederStationBussinessLayer.Domain.Enums;
using BreederStationBussinessLayer.SelectCriteria;
using BreederStationBussinessLayer.Service;
using ChovnaStanice.Forms;
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
    public partial class AddAnimalEventForm : Form
    {
        private const int BREEDER_IS_ON_EVENT_ORA_CODE = -20001;
        private const int ANIMAL_IS_ON_EVENT_ORA_CODE = -20002;

        private Event animalEvent = new Event();


        private PersonService personService = ServiceRegister.getInstance().Get<PersonService>();
        private AnimalService animalService = ServiceRegister.getInstance().Get<AnimalService>();
        private EventService eventService = ServiceRegister.getInstance().Get<EventService>();

        private IList<Animal> animals;
        private IList<Person> employees;
        private IList<AnimalChoiceItem> animalChoices = new List<AnimalChoiceItem>();
        private Point nextAnimalChoiceItemPossition;
        private const int INPUT_WIDTH = 100;
        private const int INPUT_HEIGHT = 26;
        public AddAnimalEventForm()
        {
            InitializeComponent();
            ResetEndDateToNull();
            LoadEmployeeSelect();
            nextAnimalChoiceItemPossition = new Point() { X = 10, Y = 55};
            animals = animalService.GetAnimals(new AnimalCriteria());
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void EndDate_ValueChanged(object sender, EventArgs e)
        {
            EndDate.Format = DateTimePickerFormat.Long;
        }

        private void ResetEndDateToNull()
        {
            EndDate.Format = DateTimePickerFormat.Custom;
            EndDate.CustomFormat = " ";
        }

        private void ResetEndDateButton_Click(object sender, EventArgs e)
        {
            ResetEndDateToNull();
        }

        private void LoadEmployeeSelect()
        {
            employees = personService.GetAllUsers(new PersonCriteria { Role = RoleEnum.CHOVATEL });
            LoginCombo.DisplayMember = "Login";
            LoginCombo.ValueMember = "BreederId";
            LoginCombo.DataSource = employees;
            
            FirstNameCombo.DisplayMember = "FirstName";
            FirstNameCombo.ValueMember = "Login";
            FirstNameCombo.DataSource = employees;

            LastNameCombo.DisplayMember = "LastName";
            LastNameCombo.ValueMember = "Login";
            LastNameCombo.DataSource = employees;
        }

        private void LoadAnimalSelect(ComboBox animalNameComboBox)
        {
            IList<Animal> animals = animalService.GetAnimals(new AnimalCriteria());
            animalNameComboBox.DisplayMember = "Name";
            animalNameComboBox.ValueMember = "Id";
            animalNameComboBox.DataSource = animals;
            animalNameComboBox.SelectedIndex = -1;
        }

        private void AddAnimalButton_Click(object sender, EventArgs e)
        {
            const int INPUT_OFFSET = 30;
            int leftOffset = this.nextAnimalChoiceItemPossition.X;
            const int RACE_INPUT_WIDTH = 150;
            ComboBox animalNameComboBox = new ComboBox();
            TextBox animalRace = new TextBox();
            TextBox animalId = new TextBox();
            TextBox animalGroupId = new TextBox();
            TextBox textBox1 = new TextBox();
            Button minusButton = new Button();

            minusButton.Text = "Odebrat";
            minusButton.Click += new System.EventHandler(this.minusButton_Click);

            AnimalGroupBox.Controls.Add(animalNameComboBox);
            AnimalGroupBox.Controls.Add(animalRace);
            AnimalGroupBox.Controls.Add(animalId);
            AnimalGroupBox.Controls.Add(animalGroupId);
            AnimalGroupBox.Controls.Add(minusButton);

            animalNameComboBox.Location = this.nextAnimalChoiceItemPossition;
            leftOffset += INPUT_WIDTH + INPUT_OFFSET;
            animalRace.Location = new Point { X = leftOffset, Y = this.nextAnimalChoiceItemPossition.Y };
            leftOffset += RACE_INPUT_WIDTH + INPUT_OFFSET;
            animalId.Location = new Point { X = leftOffset, Y = this.nextAnimalChoiceItemPossition.Y };
            leftOffset += INPUT_WIDTH + INPUT_OFFSET;
            animalGroupId.Location = new Point { X = leftOffset, Y = this.nextAnimalChoiceItemPossition.Y };
            leftOffset += INPUT_WIDTH + INPUT_OFFSET;
            minusButton.Location = new Point { X = leftOffset, Y = this.nextAnimalChoiceItemPossition.Y };

            animalNameComboBox.Size = new Size { Height = INPUT_HEIGHT, Width = INPUT_WIDTH };
            animalRace.Size = new Size { Height = INPUT_HEIGHT, Width = RACE_INPUT_WIDTH };
            animalId.Size = new Size { Height = INPUT_HEIGHT, Width = INPUT_WIDTH };
            animalGroupId.Size = new Size { Height = INPUT_HEIGHT, Width = INPUT_WIDTH };
            minusButton.Size = new Size { Height = INPUT_HEIGHT, Width = INPUT_WIDTH - 10 };

            animalRace.Enabled = false;
            animalId.Enabled = false;
            animalGroupId.Enabled = false;

            animalChoices.Add(new AnimalChoiceItem { AnimalNameComboBox = animalNameComboBox, AnimalGroupId = animalGroupId, AnimalId = animalId, AnimalRace = animalRace, MinusButton = minusButton});

            this.LoadAnimalSelect(animalNameComboBox);
            animalNameComboBox.SelectedIndexChanged += new System.EventHandler(this.animalComboBox_Changed);

            AnimalGroupBox.Height += INPUT_HEIGHT + 10;

            AddAnimalButton.Top += INPUT_HEIGHT + 10;
            Description.Top += INPUT_HEIGHT + 10;
            DescrLabel.Top += INPUT_HEIGHT + 10;
            AddAnimalEventButton.Top += INPUT_HEIGHT + 10;
            this.Height += INPUT_HEIGHT + 10;
            this.nextAnimalChoiceItemPossition.Y = this.nextAnimalChoiceItemPossition.Y + INPUT_HEIGHT + 10;
        }

        private void minusButton_Click(object sender, EventArgs e)
        {
            int removedIndex = animalChoices.IndexOf(animalChoices.First(item => item.MinusButton == (Button)sender));
            AnimalChoiceItem deletedItem = animalChoices.First(item => item.MinusButton == (Button)sender);
            int removedIndexInGroupBox = AnimalGroupBox.Controls.IndexOf(deletedItem.MinusButton);
            for(int i = 0; i < 5; i++)
            {
                AnimalGroupBox.Controls.RemoveAt(removedIndexInGroupBox);
                removedIndexInGroupBox--;
            }
            animalChoices.Remove(deletedItem);
            this.nextAnimalChoiceItemPossition.Y -= (INPUT_HEIGHT + 10);

            if (animalChoices.Count != removedIndex)
            {
                for(int i = removedIndex; i < animalChoices.Count; i++)
                {
                    AnimalChoiceItem item = animalChoices[i];

                    item.AnimalGroupId.Top -= (INPUT_HEIGHT + 10);
                    item.AnimalId.Top -= (INPUT_HEIGHT + 10);
                    item.AnimalNameComboBox.Top -= (INPUT_HEIGHT + 10);
                    item.AnimalRace.Top -= (INPUT_HEIGHT + 10);
                    item.MinusButton.Top -= (INPUT_HEIGHT + 10);
                }
            }
            AnimalGroupBox.Height -= INPUT_HEIGHT + 10;
            AddAnimalButton.Top -= (INPUT_HEIGHT + 10);
            Description.Top -= INPUT_HEIGHT + 10;
            DescrLabel.Top -= INPUT_HEIGHT + 10;
            AddAnimalEventButton.Top -= INPUT_HEIGHT + 10;
            this.Height -= INPUT_HEIGHT + 10;
        }

        private void animalComboBox_Changed(object sender, EventArgs e)
        {
            AnimalChoiceItem selectedItem = animalChoices.First(item => item.AnimalNameComboBox == (ComboBox)sender);
            int animalId = Int32.Parse(selectedItem.AnimalNameComboBox.SelectedValue.ToString());

            foreach(Control item in AnimalGroupBox.Controls)
            {
                if(item.GetType() == typeof(ComboBox))
                {
                    if((ComboBox)item != (ComboBox)sender && ((ComboBox)(item)).SelectedValue != null)
                    {
                        if (animalId.ToString() == ((ComboBox)(item)).SelectedValue.ToString())
                        {
                            MessageBox.Show("Zvíře už je vybráno");
                            selectedItem.AnimalRace.Text = " ";
                            selectedItem.AnimalId.Text = " ";
                            selectedItem.AnimalGroupId.Text = " ";
                            ((ComboBox)sender).SelectedIndexChanged -= animalComboBox_Changed;
                            ((ComboBox)sender).SelectedIndex = -1;
                            ((ComboBox)sender).SelectedIndexChanged += animalComboBox_Changed;
                            return;
                        }
                    }
                }
            }

            Animal animal = animals.First(item => item.Id == animalId);

            selectedItem.AnimalRace.Text = animal.Race;
            selectedItem.AnimalId.Text = animal.Id.ToString();
            selectedItem.AnimalGroupId.Text = animal.AnimalGroup.Id.ToString();
        }

        private void AddAnimalEventButton_Click(object sender, EventArgs e)
        {
            if (GetData())
            {
                eventService.AddAnimalEvent(animalEvent);
                this.Close();
            }
        }

        private bool GetData()
        {
            bool ret = true;

            ErrorProvider.Clear();

            if (LoginCombo.SelectedText != " ")
            {
                animalEvent.Breeder = new Breeder { Id = ((Breeder)LoginCombo.SelectedValue).Id };
            }
            else
            {
                ErrorProvider.SetError(LoginCombo, "Zvolte chovatele");
                ErrorProvider.SetError(FirstNameCombo, "Zvolte chovatele");
                ErrorProvider.SetError(LastNameCombo, "Zvolte chovatele");
                ret = false;
            }
            animalEvent.StartDate = StartDate.Value;

            if (EndDate.Text != " ")
            {
                animalEvent.EndDate = EndDate.Value;
            }

            if (!GetAnimalsIds())
            {
                ErrorProvider.SetError(AnimalGroupBox, "Zvolte nějaké zvíře prosím");
                ret = false;
            }

            if(Description.Text != "")
            {
                animalEvent.Description = Description.Text;
            } else
            {
                ErrorProvider.SetError(Description, "Vypište popis prosím");
                ret = false;
            }
            return ret;
        }

        private bool GetAnimalsIds()
        {
            IList<Animal> animals = new List<Animal>();
            animalEvent.animals = animals;
            foreach (AnimalChoiceItem item in animalChoices)
            {
                if (item.AnimalId.Text != "" && item.AnimalId.Text != " ") {
                    animalEvent.animals.Add(new Animal { Id = Int32.Parse(item.AnimalId.Text) });
                }
            }
            return !(animalEvent.animals.Count == 0);
        }
    }
}
