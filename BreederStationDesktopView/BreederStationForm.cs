﻿using BreederStationBussinessLayer.Domain.Enums;
using System;
using System.Windows.Forms;

namespace BreederStationDesktopView
{
    public partial class BreederStationForm : Form
    {
        public BreederStationForm()
        {
            InitializeComponent();
            SetUserRoleRights();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void EmployeeMenuItem_Click(object sender, EventArgs e)
        {
            EmployeeForm form = new EmployeeForm();
            form.MdiParent = this;
            form.Show();
            form.WindowState = FormWindowState.Maximized;
        }

        private void AnimalEventMenuItem_Click(object sender, EventArgs e)
        {
            AnimalEventForm form = new AnimalEventForm();
            form.MdiParent = this;
            form.Show();
            form.WindowState = FormWindowState.Maximized;
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();
            this.Close();
        }

        private void SetUserRoleRights()
        {
            UserSession userSession = UserSession.getInstance();
            if (userSession.Role.Type == RoleEnum.UKLIZEC || userSession.Role.Type == RoleEnum.CHOVATEL)
            {
                EmployeeMenuItem.Visible = false;
                EmployeeMenuItem.Enabled = false;
            }

            if (userSession.Role.Type == RoleEnum.UKLIZEC) {
                AnimalEventMenuItem.Visible = false;
                AnimalEventMenuItem.Enabled = false;
            }
        }
    }
}