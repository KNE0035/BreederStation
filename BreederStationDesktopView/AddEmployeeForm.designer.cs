namespace BreederStationDesktopView
{
    partial class AddEmployeeForm
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
            this.components = new System.ComponentModel.Container();
            this.LastName = new System.Windows.Forms.TextBox();
            this.FirstName = new System.Windows.Forms.TextBox();
            this.Phone = new System.Windows.Forms.TextBox();
            this.Login = new System.Windows.Forms.TextBox();
            this.RoleGroupBox = new System.Windows.Forms.GroupBox();
            this.Cleaner = new System.Windows.Forms.RadioButton();
            this.Breeder = new System.Windows.Forms.RadioButton();
            this.Director = new System.Windows.Forms.RadioButton();
            this.Admin = new System.Windows.Forms.RadioButton();
            this.RoleLabel = new System.Windows.Forms.Label();
            this.BirthDate = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CleanerLabel = new System.Windows.Forms.Label();
            this.ChemicalQualification = new System.Windows.Forms.CheckBox();
            this.BreederGroupBox = new System.Windows.Forms.GroupBox();
            this.AnimalGroupDataGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupInfoButton = new System.Windows.Forms.Button();
            this.GroupId = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.ComboAnimalGroup = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.AddEmployeeButton = new System.Windows.Forms.Button();
            this.ErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.RoleGroupBox.SuspendLayout();
            this.BreederGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AnimalGroupDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // LastName
            // 
            this.LastName.Location = new System.Drawing.Point(689, 89);
            this.LastName.Name = "LastName";
            this.LastName.Size = new System.Drawing.Size(183, 20);
            this.LastName.TabIndex = 41;
            // 
            // FirstName
            // 
            this.FirstName.Location = new System.Drawing.Point(422, 89);
            this.FirstName.Name = "FirstName";
            this.FirstName.Size = new System.Drawing.Size(184, 20);
            this.FirstName.TabIndex = 40;
            // 
            // Phone
            // 
            this.Phone.Location = new System.Drawing.Point(160, 134);
            this.Phone.Name = "Phone";
            this.Phone.Size = new System.Drawing.Size(183, 20);
            this.Phone.TabIndex = 39;
            // 
            // Login
            // 
            this.Login.Location = new System.Drawing.Point(160, 89);
            this.Login.Name = "Login";
            this.Login.Size = new System.Drawing.Size(183, 20);
            this.Login.TabIndex = 38;
            // 
            // RoleGroupBox
            // 
            this.RoleGroupBox.BackColor = System.Drawing.SystemColors.Control;
            this.RoleGroupBox.Controls.Add(this.Cleaner);
            this.RoleGroupBox.Controls.Add(this.Breeder);
            this.RoleGroupBox.Controls.Add(this.Director);
            this.RoleGroupBox.Controls.Add(this.Admin);
            this.RoleGroupBox.Location = new System.Drawing.Point(13, 251);
            this.RoleGroupBox.Name = "RoleGroupBox";
            this.RoleGroupBox.Size = new System.Drawing.Size(336, 33);
            this.RoleGroupBox.TabIndex = 37;
            this.RoleGroupBox.TabStop = false;
            this.RoleGroupBox.Enter += new System.EventHandler(this.RoleGroupBox_Enter);
            // 
            // Cleaner
            // 
            this.Cleaner.AutoSize = true;
            this.Cleaner.Location = new System.Drawing.Point(268, 9);
            this.Cleaner.Name = "Cleaner";
            this.Cleaner.Size = new System.Drawing.Size(62, 17);
            this.Cleaner.TabIndex = 18;
            this.Cleaner.Text = "Uklízeč";
            this.Cleaner.UseVisualStyleBackColor = true;
            this.Cleaner.CheckedChanged += new System.EventHandler(this.Cleaner_CheckedChanged);
            // 
            // Breeder
            // 
            this.Breeder.AutoSize = true;
            this.Breeder.Location = new System.Drawing.Point(178, 9);
            this.Breeder.Name = "Breeder";
            this.Breeder.Size = new System.Drawing.Size(67, 17);
            this.Breeder.TabIndex = 17;
            this.Breeder.Text = "Chovatel";
            this.Breeder.UseVisualStyleBackColor = true;
            this.Breeder.CheckedChanged += new System.EventHandler(this.Breeder_CheckedChanged);
            // 
            // Director
            // 
            this.Director.AutoSize = true;
            this.Director.Checked = true;
            this.Director.Location = new System.Drawing.Point(90, 9);
            this.Director.Name = "Director";
            this.Director.Size = new System.Drawing.Size(58, 17);
            this.Director.TabIndex = 1;
            this.Director.TabStop = true;
            this.Director.Text = "Ředitel";
            this.Director.UseVisualStyleBackColor = true;
            this.Director.CheckedChanged += new System.EventHandler(this.Director_CheckedChanged);
            // 
            // Admin
            // 
            this.Admin.AutoSize = true;
            this.Admin.Location = new System.Drawing.Point(6, 9);
            this.Admin.Name = "Admin";
            this.Admin.Size = new System.Drawing.Size(54, 17);
            this.Admin.TabIndex = 0;
            this.Admin.Text = "Admin";
            this.Admin.UseVisualStyleBackColor = true;
            this.Admin.CheckedChanged += new System.EventHandler(this.Admin_CheckedChanged);
            // 
            // RoleLabel
            // 
            this.RoleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RoleLabel.Location = new System.Drawing.Point(10, 218);
            this.RoleLabel.Name = "RoleLabel";
            this.RoleLabel.Size = new System.Drawing.Size(182, 30);
            this.RoleLabel.TabIndex = 36;
            this.RoleLabel.Text = " Uživatelská role: *";
            this.RoleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BirthDate
            // 
            this.BirthDate.Location = new System.Drawing.Point(160, 174);
            this.BirthDate.Name = "BirthDate";
            this.BirthDate.Size = new System.Drawing.Size(183, 20);
            this.BirthDate.TabIndex = 35;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(612, 83);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 30);
            this.label7.TabIndex = 33;
            this.label7.Text = "Příjmení: *";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(360, 86);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 30);
            this.label6.TabIndex = 32;
            this.label6.Text = "Jméno: *";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(11, 171);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(143, 30);
            this.label5.TabIndex = 31;
            this.label5.Text = "Datum narození: *";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 30);
            this.label4.TabIndex = 30;
            this.label4.Text = "Telefon *";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(11, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 30);
            this.label3.TabIndex = 29;
            this.label3.Text = "Login: *";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(251, 30);
            this.label2.TabIndex = 28;
            this.label2.Text = "Vložení zaměstnance";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 30);
            this.label1.TabIndex = 27;
            this.label1.Text = "Základní informace: *";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CleanerLabel
            // 
            this.CleanerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CleanerLabel.Location = new System.Drawing.Point(9, 315);
            this.CleanerLabel.Name = "CleanerLabel";
            this.CleanerLabel.Size = new System.Drawing.Size(182, 30);
            this.CleanerLabel.TabIndex = 42;
            this.CleanerLabel.Text = "Sekce uklízeče: *";
            this.CleanerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CleanerLabel.Visible = false;
            // 
            // ChemicalQualification
            // 
            this.ChemicalQualification.AutoSize = true;
            this.ChemicalQualification.Enabled = false;
            this.ChemicalQualification.Location = new System.Drawing.Point(13, 348);
            this.ChemicalQualification.Name = "ChemicalQualification";
            this.ChemicalQualification.Size = new System.Drawing.Size(127, 17);
            this.ChemicalQualification.TabIndex = 43;
            this.ChemicalQualification.Text = "Chemická kvalifikace";
            this.ChemicalQualification.UseVisualStyleBackColor = true;
            this.ChemicalQualification.Visible = false;
            // 
            // BreederGroupBox
            // 
            this.BreederGroupBox.Controls.Add(this.AnimalGroupDataGrid);
            this.BreederGroupBox.Controls.Add(this.groupInfoButton);
            this.BreederGroupBox.Controls.Add(this.GroupId);
            this.BreederGroupBox.Controls.Add(this.label11);
            this.BreederGroupBox.Controls.Add(this.ComboAnimalGroup);
            this.BreederGroupBox.Controls.Add(this.label10);
            this.BreederGroupBox.Enabled = false;
            this.BreederGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BreederGroupBox.Location = new System.Drawing.Point(11, 315);
            this.BreederGroupBox.Name = "BreederGroupBox";
            this.BreederGroupBox.Size = new System.Drawing.Size(861, 147);
            this.BreederGroupBox.TabIndex = 44;
            this.BreederGroupBox.TabStop = false;
            this.BreederGroupBox.Text = "Sekce chovatele";
            this.BreederGroupBox.Visible = false;
            // 
            // AnimalGroupDataGrid
            // 
            this.AnimalGroupDataGrid.AllowUserToAddRows = false;
            this.AnimalGroupDataGrid.AllowUserToDeleteRows = false;
            this.AnimalGroupDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.AnimalGroupDataGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.AnimalGroupDataGrid.BackgroundColor = System.Drawing.SystemColors.Control;
            this.AnimalGroupDataGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.AnimalGroupDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AnimalGroupDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.AnimalGroupDataGrid.Location = new System.Drawing.Point(9, 119);
            this.AnimalGroupDataGrid.MultiSelect = false;
            this.AnimalGroupDataGrid.Name = "AnimalGroupDataGrid";
            this.AnimalGroupDataGrid.ReadOnly = true;
            this.AnimalGroupDataGrid.RowHeadersVisible = false;
            this.AnimalGroupDataGrid.RowTemplate.Height = 24;
            this.AnimalGroupDataGrid.ShowCellErrors = false;
            this.AnimalGroupDataGrid.ShowEditingIcon = false;
            this.AnimalGroupDataGrid.ShowRowErrors = false;
            this.AnimalGroupDataGrid.Size = new System.Drawing.Size(846, 28);
            this.AnimalGroupDataGrid.TabIndex = 46;
            this.AnimalGroupDataGrid.Visible = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Id";
            this.dataGridViewTextBoxColumn1.HeaderText = "Id skupiny";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 105;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Description";
            this.dataGridViewTextBoxColumn2.HeaderText = "Popis skupiny";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 130;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "BreedersInfo";
            this.dataGridViewTextBoxColumn3.HeaderText = "Chovatelé";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 105;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn4.DataPropertyName = "AnimalsInfo";
            this.dataGridViewTextBoxColumn4.HeaderText = "Zvířata";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 82;
            // 
            // groupInfoButton
            // 
            this.groupInfoButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupInfoButton.Location = new System.Drawing.Point(573, 78);
            this.groupInfoButton.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.groupInfoButton.Name = "groupInfoButton";
            this.groupInfoButton.Size = new System.Drawing.Size(282, 28);
            this.groupInfoButton.TabIndex = 48;
            this.groupInfoButton.Text = "Zozbraz informace o skupinách";
            this.groupInfoButton.UseVisualStyleBackColor = true;
            this.groupInfoButton.Click += new System.EventHandler(this.groupInfoButton_Click);
            // 
            // GroupId
            // 
            this.GroupId.Enabled = false;
            this.GroupId.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupId.Location = new System.Drawing.Point(718, 32);
            this.GroupId.Name = "GroupId";
            this.GroupId.Size = new System.Drawing.Size(137, 24);
            this.GroupId.TabIndex = 47;
            this.GroupId.TextChanged += new System.EventHandler(this.GroupDescription_TextChanged);
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(6, 30);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(105, 28);
            this.label11.TabIndex = 46;
            this.label11.Text = "Popis skupiny:*";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ComboAnimalGroup
            // 
            this.ComboAnimalGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboAnimalGroup.FormattingEnabled = true;
            this.ComboAnimalGroup.Location = new System.Drawing.Point(123, 34);
            this.ComboAnimalGroup.MaxDropDownItems = 50;
            this.ComboAnimalGroup.Name = "ComboAnimalGroup";
            this.ComboAnimalGroup.Size = new System.Drawing.Size(459, 24);
            this.ComboAnimalGroup.TabIndex = 45;
            this.ComboAnimalGroup.SelectedIndexChanged += new System.EventHandler(this.ComboAnimalGroup_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(628, 32);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(84, 24);
            this.label10.TabIndex = 30;
            this.label10.Text = "Id skupiny:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // AddEmployeeButton
            // 
            this.AddEmployeeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddEmployeeButton.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.AddEmployeeButton.Location = new System.Drawing.Point(689, 291);
            this.AddEmployeeButton.Name = "AddEmployeeButton";
            this.AddEmployeeButton.Size = new System.Drawing.Size(183, 27);
            this.AddEmployeeButton.TabIndex = 46;
            this.AddEmployeeButton.Text = "Vložit zaměstnance";
            this.AddEmployeeButton.UseVisualStyleBackColor = true;
            this.AddEmployeeButton.Click += new System.EventHandler(this.AddEmployeeButton_click);
            // 
            // ErrorProvider
            // 
            this.ErrorProvider.ContainerControl = this;
            // 
            // AddEmployeeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(924, 330);
            this.Controls.Add(this.AddEmployeeButton);
            this.Controls.Add(this.BreederGroupBox);
            this.Controls.Add(this.ChemicalQualification);
            this.Controls.Add(this.CleanerLabel);
            this.Controls.Add(this.LastName);
            this.Controls.Add(this.FirstName);
            this.Controls.Add(this.Phone);
            this.Controls.Add(this.Login);
            this.Controls.Add(this.RoleGroupBox);
            this.Controls.Add(this.RoleLabel);
            this.Controls.Add(this.BirthDate);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "AddEmployeeForm";
            this.Text = "AddEmployeeForm";
            this.RoleGroupBox.ResumeLayout(false);
            this.RoleGroupBox.PerformLayout();
            this.BreederGroupBox.ResumeLayout(false);
            this.BreederGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AnimalGroupDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox LastName;
        private System.Windows.Forms.TextBox FirstName;
        private System.Windows.Forms.TextBox Phone;
        private System.Windows.Forms.TextBox Login;
        private System.Windows.Forms.GroupBox RoleGroupBox;
        private System.Windows.Forms.RadioButton Cleaner;
        private System.Windows.Forms.RadioButton Breeder;
        private System.Windows.Forms.RadioButton Director;
        private System.Windows.Forms.RadioButton Admin;
        private System.Windows.Forms.Label RoleLabel;
        private System.Windows.Forms.DateTimePicker BirthDate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label CleanerLabel;
        private System.Windows.Forms.CheckBox ChemicalQualification;
        private System.Windows.Forms.GroupBox BreederGroupBox;
        private System.Windows.Forms.TextBox GroupId;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox ComboAnimalGroup;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button groupInfoButton;
        private System.Windows.Forms.DataGridView AnimalGroupDataGrid;
        private System.Windows.Forms.Button AddEmployeeButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.ErrorProvider ErrorProvider;
    }
}