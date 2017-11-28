namespace BreederStationDesktopView
{
    partial class AnimalEventForm
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
            this.AddNewAnimalEventButton = new System.Windows.Forms.Button();
            this.AnimalEventDataGrid = new System.Windows.Forms.DataGridView();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BreederLogin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AnimalNames = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.AnimalEventDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // AddNewAnimalEventButton
            // 
            this.AddNewAnimalEventButton.AutoSize = true;
            this.AddNewAnimalEventButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.AddNewAnimalEventButton.Location = new System.Drawing.Point(0, 228);
            this.AddNewAnimalEventButton.Name = "AddNewAnimalEventButton";
            this.AddNewAnimalEventButton.Size = new System.Drawing.Size(544, 33);
            this.AddNewAnimalEventButton.TabIndex = 3;
            this.AddNewAnimalEventButton.Text = "Přidat zvířecí událost";
            this.AddNewAnimalEventButton.UseVisualStyleBackColor = true;
            this.AddNewAnimalEventButton.Click += new System.EventHandler(this.AddNewAnimalEventButton_Click);
            // 
            // AnimalEventDataGrid
            // 
            this.AnimalEventDataGrid.AllowUserToAddRows = false;
            this.AnimalEventDataGrid.AllowUserToDeleteRows = false;
            this.AnimalEventDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.AnimalEventDataGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.AnimalEventDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AnimalEventDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Description,
            this.StartDate,
            this.EndDate,
            this.BreederLogin,
            this.AnimalNames});
            this.AnimalEventDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AnimalEventDataGrid.Location = new System.Drawing.Point(0, 0);
            this.AnimalEventDataGrid.MultiSelect = false;
            this.AnimalEventDataGrid.Name = "AnimalEventDataGrid";
            this.AnimalEventDataGrid.ReadOnly = true;
            this.AnimalEventDataGrid.RowHeadersVisible = false;
            this.AnimalEventDataGrid.ShowCellErrors = false;
            this.AnimalEventDataGrid.ShowEditingIcon = false;
            this.AnimalEventDataGrid.ShowRowErrors = false;
            this.AnimalEventDataGrid.Size = new System.Drawing.Size(544, 261);
            this.AnimalEventDataGrid.TabIndex = 2;
            this.AnimalEventDataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.EmployeeDataGrid_CellContentClick);
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "Popis události";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            // 
            // StartDate
            // 
            this.StartDate.DataPropertyName = "StartDate";
            this.StartDate.HeaderText = "Začátek události";
            this.StartDate.Name = "StartDate";
            this.StartDate.ReadOnly = true;
            // 
            // EndDate
            // 
            this.EndDate.DataPropertyName = "EndDate";
            this.EndDate.HeaderText = "Konec události";
            this.EndDate.Name = "EndDate";
            this.EndDate.ReadOnly = true;
            // 
            // BreederLogin
            // 
            this.BreederLogin.DataPropertyName = "BreederLogin";
            this.BreederLogin.HeaderText = "Login chovatele";
            this.BreederLogin.Name = "BreederLogin";
            this.BreederLogin.ReadOnly = true;
            // 
            // AnimalNames
            // 
            this.AnimalNames.DataPropertyName = "AnimalsNamesString";
            this.AnimalNames.HeaderText = "Jména zůčastněných zvířat";
            this.AnimalNames.Name = "AnimalNames";
            this.AnimalNames.ReadOnly = true;
            // 
            // AnimalEventForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 261);
            this.Controls.Add(this.AddNewAnimalEventButton);
            this.Controls.Add(this.AnimalEventDataGrid);
            this.Name = "AnimalEventForm";
            this.Text = "AnimalEventForm";
            ((System.ComponentModel.ISupportInitialize)(this.AnimalEventDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AddNewAnimalEventButton;
        private System.Windows.Forms.DataGridView AnimalEventDataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn EndDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn BreederLogin;
        private System.Windows.Forms.DataGridViewTextBoxColumn AnimalNames;
    }
}