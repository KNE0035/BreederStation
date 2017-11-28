namespace BreederStationDesktopView
{
    partial class EmployeeForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.EmployeeDataGrid = new System.Windows.Forms.DataGridView();
            this.Login = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FirstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Phone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BirthDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Role = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AddNewEmployeeButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.EmployeeDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // EmployeeDataGrid
            // 
            this.EmployeeDataGrid.AllowUserToAddRows = false;
            this.EmployeeDataGrid.AllowUserToDeleteRows = false;
            this.EmployeeDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.EmployeeDataGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.EmployeeDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.EmployeeDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Login,
            this.FirstName,
            this.LastName,
            this.Phone,
            this.BirthDate,
            this.Role});
            this.EmployeeDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EmployeeDataGrid.Location = new System.Drawing.Point(0, 0);
            this.EmployeeDataGrid.MultiSelect = false;
            this.EmployeeDataGrid.Name = "EmployeeDataGrid";
            this.EmployeeDataGrid.ReadOnly = true;
            this.EmployeeDataGrid.RowHeadersVisible = false;
            this.EmployeeDataGrid.ShowCellErrors = false;
            this.EmployeeDataGrid.ShowEditingIcon = false;
            this.EmployeeDataGrid.ShowRowErrors = false;
            this.EmployeeDataGrid.Size = new System.Drawing.Size(676, 324);
            this.EmployeeDataGrid.TabIndex = 0;
            this.EmployeeDataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.EmployeeDataGrid_CellContentClick);
            // 
            // Login
            // 
            this.Login.DataPropertyName = "Login";
            this.Login.HeaderText = "Login";
            this.Login.Name = "Login";
            this.Login.ReadOnly = true;
            // 
            // FirstName
            // 
            this.FirstName.DataPropertyName = "FirstName";
            this.FirstName.HeaderText = "Jméno";
            this.FirstName.Name = "FirstName";
            this.FirstName.ReadOnly = true;
            // 
            // LastName
            // 
            this.LastName.DataPropertyName = "LastName";
            this.LastName.HeaderText = "Příjmení";
            this.LastName.Name = "LastName";
            this.LastName.ReadOnly = true;
            // 
            // Phone
            // 
            this.Phone.DataPropertyName = "Phone";
            this.Phone.HeaderText = "Tel. číslo";
            this.Phone.Name = "Phone";
            this.Phone.ReadOnly = true;
            // 
            // BirthDate
            // 
            this.BirthDate.DataPropertyName = "BirthDate";
            this.BirthDate.HeaderText = "Datum narození";
            this.BirthDate.Name = "BirthDate";
            this.BirthDate.ReadOnly = true;
            // 
            // Role
            // 
            this.Role.DataPropertyName = "RoleType";
            this.Role.HeaderText = "Role";
            this.Role.Name = "Role";
            this.Role.ReadOnly = true;
            // 
            // AddNewEmployeeButton
            // 
            this.AddNewEmployeeButton.AutoSize = true;
            this.AddNewEmployeeButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.AddNewEmployeeButton.Location = new System.Drawing.Point(0, 291);
            this.AddNewEmployeeButton.Name = "AddNewEmployeeButton";
            this.AddNewEmployeeButton.Size = new System.Drawing.Size(676, 33);
            this.AddNewEmployeeButton.TabIndex = 1;
            this.AddNewEmployeeButton.Text = "Přidat zaměstnance";
            this.AddNewEmployeeButton.UseVisualStyleBackColor = true;
            this.AddNewEmployeeButton.Click += new System.EventHandler(this.AddNewEmployeeButton_Click);
            // 
            // EmployeeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(676, 324);
            this.Controls.Add(this.AddNewEmployeeButton);
            this.Controls.Add(this.EmployeeDataGrid);
            this.Name = "EmployeeForm";
            this.Text = "EmployeeForm";
            ((System.ComponentModel.ISupportInitialize)(this.EmployeeDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView EmployeeDataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Login;
        private System.Windows.Forms.DataGridViewTextBoxColumn FirstName;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Phone;
        private System.Windows.Forms.DataGridViewTextBoxColumn BirthDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Role;
        private System.Windows.Forms.Button AddNewEmployeeButton;
    }
}