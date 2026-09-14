using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HostelManagementSystem
{
    public partial class ManageStudents : Form
    {
        string connectionString = "Server=localhost;Database=hostel_db;Uid=root;Pwd=MilesHero010.;";
        int selectedStudentID = 0;

        public ManageStudents()
        {
            InitializeComponent();
            this.Load += ManageStudents_Load;
        }

        private void ManageStudents_Load(object sender, EventArgs e)
        {
            LoadStudents();
            dgvStudents.CellClick += DgvStudents_CellClick;
        }

        private void LoadStudents()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT StudentID, FirstName, LastName, MatricNumber, " +
                                   "Gender, Department, Level, PhoneNumber, Email, Username " +
                                   "FROM Student";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvStudents.DataSource = dt;

                    if (dgvStudents.Columns["StudentID"] != null)
                        dgvStudents.Columns["StudentID"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading students: " + ex.Message);
            }
        }

        private void DgvStudents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvStudents.Rows[e.RowIndex];

                if (row.Cells["StudentID"].Value == null ||
                    row.Cells["StudentID"].Value == DBNull.Value)
                    return;

                selectedStudentID = Convert.ToInt32(row.Cells["StudentID"].Value);
                txtFirstName.Text = row.Cells["FirstName"].Value?.ToString() ?? "";
                txtLastName.Text = row.Cells["LastName"].Value?.ToString() ?? "";
                txtMatric.Text = row.Cells["MatricNumber"].Value?.ToString() ?? "";
                cmbGender.Text = row.Cells["Gender"].Value?.ToString() ?? "";
                txtDepartment.Text = row.Cells["Department"].Value?.ToString() ?? "";
                cmbLevel.Text = row.Cells["Level"].Value?.ToString() ?? "";
                txtPhone.Text = row.Cells["PhoneNumber"].Value?.ToString() ?? "";
                txtEmail.Text = row.Cells["Email"].Value?.ToString() ?? "";
                txtUsername.Text = row.Cells["Username"].Value?.ToString() ?? "";
                txtPassword.Text = "";
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFirstName.Text) ||
                string.IsNullOrEmpty(txtLastName.Text) ||
                string.IsNullOrEmpty(txtMatric.Text) ||
                string.IsNullOrEmpty(txtUsername.Text) ||
                string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Please fill in all required fields.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO Student (FirstName, LastName, MatricNumber, " +
                                   "Gender, Department, Level, PhoneNumber, Email, Username, Password) " +
                                   "VALUES (@first, @last, @matric, @gender, @dept, @level, " +
                                   "@phone, @email, @username, @password)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@first", txtFirstName.Text);
                    cmd.Parameters.AddWithValue("@last", txtLastName.Text);
                    cmd.Parameters.AddWithValue("@matric", txtMatric.Text);
                    cmd.Parameters.AddWithValue("@gender", cmbGender.Text);
                    cmd.Parameters.AddWithValue("@dept", txtDepartment.Text);
                    cmd.Parameters.AddWithValue("@level", cmbLevel.Text);
                    cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Student added successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields();
                    LoadStudents();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding student: " + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedStudentID == 0)
            {
                MessageBox.Show("Please select a student to update.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE Student SET FirstName=@first, LastName=@last, " +
                                   "MatricNumber=@matric, Gender=@gender, Department=@dept, " +
                                   "Level=@level, PhoneNumber=@phone, Email=@email, " +
                                   "Username=@username WHERE StudentID=@id";

                    // Only update password if a new one is entered
                    if (!string.IsNullOrEmpty(txtPassword.Text))
                        query = "UPDATE Student SET FirstName=@first, LastName=@last, " +
                                "MatricNumber=@matric, Gender=@gender, Department=@dept, " +
                                "Level=@level, PhoneNumber=@phone, Email=@email, " +
                                "Username=@username, Password=@password WHERE StudentID=@id";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@first", txtFirstName.Text);
                    cmd.Parameters.AddWithValue("@last", txtLastName.Text);
                    cmd.Parameters.AddWithValue("@matric", txtMatric.Text);
                    cmd.Parameters.AddWithValue("@gender", cmbGender.Text);
                    cmd.Parameters.AddWithValue("@dept", txtDepartment.Text);
                    cmd.Parameters.AddWithValue("@level", cmbLevel.Text);
                    cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@id", selectedStudentID);

                    if (!string.IsNullOrEmpty(txtPassword.Text))
                        cmd.Parameters.AddWithValue("@password", txtPassword.Text);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Student updated successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields();
                    LoadStudents();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating student: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedStudentID == 0)
            {
                MessageBox.Show("Please select a student to delete.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete this student?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "DELETE FROM Student WHERE StudentID=@id";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@id", selectedStudentID);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Student deleted successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFields();
                        LoadStudents();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting student: " + ex.Message);
                }
            }
        }

        private void ClearFields()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtMatric.Text = "";
            cmbGender.SelectedIndex = -1;
            txtDepartment.Text = "";
            cmbLevel.SelectedIndex = -1;
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            selectedStudentID = 0;
        }

        
    }
}