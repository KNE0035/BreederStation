using BreederStationBussinessLayer.Domain;
using BreederStationBussinessLayer.Service;
using System;
using System.Windows.Forms;

namespace BreederStationDesktopView
{
    public partial class LoginForm : Form
    {
        private PersonService personService = ServiceRegister.getInstance().Get<PersonService>();

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            ErrorProvider.Clear();
            if (!ValidateLoginInputs())
            {
                return;
            }

            if (!AuthorizeUser())
            {
                ErrorProvider.SetError(LoginText, "Špatné údaje");
                ErrorProvider.SetError(PasswordText, "Špatné údaje");
                return;
            }

            this.Hide();
            BreederStationForm breederStationForm = new BreederStationForm();
            breederStationForm.ShowDialog();
            this.Close();
        }

        private bool ValidateLoginInputs()
        {
            bool valid = true;
            if (String.IsNullOrEmpty(LoginText.Text))
            {
                ErrorProvider.SetError(LoginText, "Vyplnte login");
                valid = false;
            }

            if (String.IsNullOrEmpty(PasswordText.Text))
            {
                ErrorProvider.SetError(PasswordText, "Vyplnte heslo");
                valid = false;
            }
            return valid;
        }

        private bool AuthorizeUser()
        {
            bool authorized = true;

            Person loggedUser = personService.Authorize(LoginText.Text, PasswordText.Text);
            if (loggedUser == null)
            {
                authorized = false;
            } else
            {
                UserSession userSession = UserSession.getInstance();
                userSession.Id = loggedUser.Id;
                userSession.Login = loggedUser.Login;
                userSession.Role = loggedUser.Role;
            }
            return authorized;
        }

    }
}
