namespace HostelManagementSystem
{
    partial class ManageStudents
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            txtFirstName = new TextBox();
            txtLastName = new TextBox();
            txtMatric = new TextBox();
            txtDepartment = new TextBox();
            txtPhone = new TextBox();
            txtEmail = new TextBox();
            txtPassword = new TextBox();
            cmbGender = new ComboBox();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            dgvStudents = new DataGridView();
            cmbLevel = new ComboBox();
            txtUsername = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dgvStudents).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(101, 25);
            label1.TabIndex = 0;
            label1.Text = "First Name:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 59);
            label2.Name = "label2";
            label2.Size = new Size(99, 25);
            label2.TabIndex = 1;
            label2.Text = "Last Name:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 111);
            label3.Name = "label3";
            label3.Size = new Size(135, 25);
            label3.TabIndex = 2;
            label3.Text = "Matric Number:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 166);
            label4.Name = "label4";
            label4.Size = new Size(73, 25);
            label4.TabIndex = 3;
            label4.Text = "Gender:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 222);
            label5.Name = "label5";
            label5.Size = new Size(111, 25);
            label5.TabIndex = 4;
            label5.Text = "Department:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 272);
            label6.Name = "label6";
            label6.Size = new Size(55, 25);
            label6.TabIndex = 5;
            label6.Text = "Level:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 325);
            label7.Name = "label7";
            label7.Size = new Size(136, 25);
            label7.TabIndex = 6;
            label7.Text = "Phone Number:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(12, 384);
            label8.Name = "label8";
            label8.Size = new Size(58, 25);
            label8.TabIndex = 7;
            label8.Text = "Email:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(12, 440);
            label9.Name = "label9";
            label9.Size = new Size(95, 25);
            label9.TabIndex = 8;
            label9.Text = "Username:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(12, 500);
            label10.Name = "label10";
            label10.Size = new Size(91, 25);
            label10.TabIndex = 9;
            label10.Text = "Password:";
            // 
            // txtFirstName
            // 
            txtFirstName.Location = new Point(205, 6);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Size = new Size(172, 31);
            txtFirstName.TabIndex = 10;
            // 
            // txtLastName
            // 
            txtLastName.Location = new Point(205, 56);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(172, 31);
            txtLastName.TabIndex = 11;
            // 
            // txtMatric
            // 
            txtMatric.Location = new Point(205, 108);
            txtMatric.Name = "txtMatric";
            txtMatric.Size = new Size(172, 31);
            txtMatric.TabIndex = 12;
            // 
            // txtDepartment
            // 
            txtDepartment.Location = new Point(205, 219);
            txtDepartment.Name = "txtDepartment";
            txtDepartment.Size = new Size(172, 31);
            txtDepartment.TabIndex = 13;
            // 
            // txtPhone
            // 
            txtPhone.Location = new Point(205, 322);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(172, 31);
            txtPhone.TabIndex = 14;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(205, 381);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(172, 31);
            txtEmail.TabIndex = 15;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(205, 497);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(172, 31);
            txtPassword.TabIndex = 17;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // cmbGender
            // 
            cmbGender.FormattingEnabled = true;
            cmbGender.Items.AddRange(new object[] { "Male", "Female" });
            cmbGender.Location = new Point(205, 163);
            cmbGender.Name = "cmbGender";
            cmbGender.Size = new Size(172, 33);
            cmbGender.TabIndex = 18;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.FromArgb(26, 86, 219);
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(1, 627);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(112, 62);
            btnAdd.TabIndex = 20;
            btnAdd.Text = "Add Student";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.FromArgb(5, 122, 85);
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnUpdate.ForeColor = Color.White;
            btnUpdate.Location = new Point(131, 627);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(112, 62);
            btnUpdate.TabIndex = 21;
            btnUpdate.Text = "Update Student";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(224, 36, 36);
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnDelete.ForeColor = Color.White;
            btnDelete.Location = new Point(265, 627);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(112, 62);
            btnDelete.TabIndex = 22;
            btnDelete.Text = "Delete Student";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // dgvStudents
            // 
            dgvStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvStudents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvStudents.Dock = DockStyle.Right;
            dgvStudents.Location = new Point(383, 0);
            dgvStudents.Name = "dgvStudents";
            dgvStudents.ReadOnly = true;
            dgvStudents.RowHeadersWidth = 62;
            dgvStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStudents.Size = new Size(749, 701);
            dgvStudents.TabIndex = 23;
            // 
            // cmbLevel
            // 
            cmbLevel.FormattingEnabled = true;
            cmbLevel.Items.AddRange(new object[] { "100", "200", "300", "400", "500" });
            cmbLevel.Location = new Point(205, 272);
            cmbLevel.Name = "cmbLevel";
            cmbLevel.Size = new Size(172, 33);
            cmbLevel.TabIndex = 24;
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(205, 440);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(172, 31);
            txtUsername.TabIndex = 25;
            // 
            // ManageStudents
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1132, 701);
            Controls.Add(txtUsername);
            Controls.Add(cmbLevel);
            Controls.Add(dgvStudents);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnAdd);
            Controls.Add(cmbGender);
            Controls.Add(txtPassword);
            Controls.Add(txtEmail);
            Controls.Add(txtPhone);
            Controls.Add(txtDepartment);
            Controls.Add(txtMatric);
            Controls.Add(txtLastName);
            Controls.Add(txtFirstName);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "ManageStudents";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Manage Students";
            ((System.ComponentModel.ISupportInitialize)dgvStudents).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private TextBox txtFirstName;
        private TextBox txtLastName;
        private TextBox txtMatric;
        private TextBox txtDepartment;
        private TextBox txtPhone;
        private TextBox txtEmail;
        private TextBox txtPassword;
        private ComboBox cmbGender;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private DataGridView dgvStudents;
        private ComboBox cmbLevel;
        private TextBox txtUsername;
    }
}