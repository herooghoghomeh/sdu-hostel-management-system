using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HostelManagementSystem
{
    public partial class ManageHostels : Form
    {
        string connectionString = "Server=localhost;Database=hostel_db;Uid=root;Pwd=MilesHero010.;";
        int selectedHostelID = 0;

        // Input controls
        TextBox txtHostelName = new TextBox();
        TextBox txtLocation = new TextBox();
        TextBox txtCapacity = new TextBox();
        ComboBox cmbGender = new ComboBox();
        DataGridView dgvHostels = new DataGridView();

        public ManageHostels()
        {
            InitializeComponent();
            this.Text = "Manage Hostels";
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.Load += ManageHostels_Load;
            BuildUI();
        }

        private void BuildUI()
        {
            this.Controls.Clear();

            // ── HEADER ───────────────────────────────────────────
            Panel pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 85,
                BackColor = Color.FromArgb(26, 86, 219)
            };

            Label lblTitle = new Label
            {
                Text = "🏠  Manage Hostels",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(25, 15),
                BackColor = Color.Transparent
            };

            Label lblSub = new Label
            {
                Text = "Add, update or delete hostel facilities",
                ForeColor = Color.FromArgb(200, 220, 255),
                Font = new Font("Segoe UI", 9),
                AutoSize = true,
                Location = new Point(27, 50),
                BackColor = Color.Transparent
            };

            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(lblSub);

            // ── MAIN PANEL ───────────────────────────────────────
            Panel pnlMain = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(245, 247, 250),
                Padding = new Padding(20)
            };

            // ── LEFT PANEL (Input Form) ───────────────────────────
            Panel pnlForm = new Panel
            {
                Size = new Size(350, 500),
                Location = new Point(20, 20),
                BackColor = Color.White,
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };
            pnlForm.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, pnlForm.ClientRectangle,
                    Color.FromArgb(229, 231, 235), ButtonBorderStyle.Solid);
            };

            Label lblFormTitle = new Label
            {
                Text = "Hostel Details",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(17, 24, 39),
                AutoSize = true,
                Location = new Point(20, 20)
            };

            // Hostel Name
            Label lblName = new Label { Text = "Hostel Name:", Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.FromArgb(51, 65, 85), AutoSize = true, Location = new Point(20, 65) };
            txtHostelName = new TextBox { Font = new Font("Segoe UI", 10), Location = new Point(20, 88), Size = new Size(310, 30), BorderStyle = BorderStyle.FixedSingle };

            // Location
            Label lblLoc = new Label { Text = "Location:", Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.FromArgb(51, 65, 85), AutoSize = true, Location = new Point(20, 135) };
            txtLocation = new TextBox { Font = new Font("Segoe UI", 10), Location = new Point(20, 158), Size = new Size(310, 30), BorderStyle = BorderStyle.FixedSingle };

            // Capacity
            Label lblCap = new Label { Text = "Capacity:", Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.FromArgb(51, 65, 85), AutoSize = true, Location = new Point(20, 205) };
            txtCapacity = new TextBox { Font = new Font("Segoe UI", 10), Location = new Point(20, 228), Size = new Size(310, 30), BorderStyle = BorderStyle.FixedSingle };

            // Gender
            Label lblGen = new Label { Text = "Gender:", Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.FromArgb(51, 65, 85), AutoSize = true, Location = new Point(20, 275) };
            cmbGender = new ComboBox { Font = new Font("Segoe UI", 10), Location = new Point(20, 298), Size = new Size(310, 30), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbGender.Items.AddRange(new string[] { "Male", "Female" });

            // Buttons
            Button btnAdd = new Button
            {
                Text = "➕  Add Hostel",
                Size = new Size(145, 42),
                Location = new Point(20, 360),
                BackColor = Color.FromArgb(26, 86, 219),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.Click += btnAdd_Click;

            Button btnUpdate = new Button
            {
                Text = "✏  Update",
                Size = new Size(145, 42),
                Location = new Point(185, 360),
                BackColor = Color.FromArgb(5, 122, 85),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnUpdate.FlatAppearance.BorderSize = 0;
            btnUpdate.Click += btnUpdate_Click;

            Button btnDelete = new Button
            {
                Text = "🗑  Delete",
                Size = new Size(145, 42),
                Location = new Point(20, 415),
                BackColor = Color.FromArgb(224, 36, 36),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.Click += btnDelete_Click;

            Button btnClear = new Button
            {
                Text = "✖  Clear",
                Size = new Size(145, 42),
                Location = new Point(185, 415),
                BackColor = Color.FromArgb(107, 114, 128),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.Click += (s, e) => ClearFields();

            pnlForm.Controls.Add(lblFormTitle);
            pnlForm.Controls.Add(lblName);
            pnlForm.Controls.Add(txtHostelName);
            pnlForm.Controls.Add(lblLoc);
            pnlForm.Controls.Add(txtLocation);
            pnlForm.Controls.Add(lblCap);
            pnlForm.Controls.Add(txtCapacity);
            pnlForm.Controls.Add(lblGen);
            pnlForm.Controls.Add(cmbGender);
            pnlForm.Controls.Add(btnAdd);
            pnlForm.Controls.Add(btnUpdate);
            pnlForm.Controls.Add(btnDelete);
            pnlForm.Controls.Add(btnClear);

            // ── RIGHT PANEL (DataGridView) ────────────────────────
            Panel pnlGrid = new Panel
            {
                Location = new Point(390, 20),
                BackColor = Color.White,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };
            pnlGrid.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, pnlGrid.ClientRectangle,
                    Color.FromArgb(229, 231, 235), ButtonBorderStyle.Solid);
            };

            Label lblGridTitle = new Label
            {
                Text = "All Hostels",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(17, 24, 39),
                AutoSize = true,
                Location = new Point(15, 15)
            };

            Label lblHint = new Label
            {
                Text = "Click a row to edit",
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.FromArgb(107, 114, 128),
                AutoSize = true,
                Location = new Point(15, 40)
            };

            dgvHostels = new DataGridView
            {
                Location = new Point(0, 65),
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                GridColor = Color.FromArgb(229, 231, 235),
                Font = new Font("Segoe UI", 9),
                RowTemplate = { Height = 40 },
                MultiSelect = false
            };
            dgvHostels.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(26, 86, 219),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Padding = new Padding(8, 0, 0, 0),
                SelectionBackColor = Color.FromArgb(26, 86, 219),
                SelectionForeColor = Color.White
            };
            dgvHostels.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvHostels.ColumnHeadersHeight = 42;
            dgvHostels.EnableHeadersVisualStyles = false;
            dgvHostels.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(249, 250, 251) };
            dgvHostels.DefaultCellStyle = new DataGridViewCellStyle
            {
                Padding = new Padding(8, 0, 0, 0),
                SelectionBackColor = Color.FromArgb(219, 234, 254),
                SelectionForeColor = Color.FromArgb(17, 24, 39)
            };
            dgvHostels.CellClick += DgvHostels_CellClick;

            pnlGrid.Controls.Add(lblGridTitle);
            pnlGrid.Controls.Add(lblHint);
            pnlGrid.Controls.Add(dgvHostels);

            // Resize grid with form
            pnlMain.Resize += (s, e) =>
            {
                pnlGrid.Size = new Size(pnlMain.Width - 410, pnlMain.Height - 40);
                dgvHostels.Size = new Size(pnlGrid.Width, pnlGrid.Height - 65);
            };

            pnlMain.Controls.Add(pnlForm);
            pnlMain.Controls.Add(pnlGrid);

            this.Controls.Add(pnlMain);
            this.Controls.Add(pnlHeader);
        }

        private void ManageHostels_Load(object sender, EventArgs e)
        {
            LoadHostels();
        }

        private void LoadHostels()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT HostelID, HostelName, Location, Capacity, Gender FROM Hostel";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvHostels.DataSource = dt;
                    if (dgvHostels.Columns["HostelID"] != null)
                        dgvHostels.Columns["HostelID"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading hostels: " + ex.Message);
            }
        }

        private void DgvHostels_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvHostels.Rows[e.RowIndex];
                if (row.Cells["HostelID"].Value == null || row.Cells["HostelID"].Value == DBNull.Value) return;
                selectedHostelID = Convert.ToInt32(row.Cells["HostelID"].Value);
                txtHostelName.Text = row.Cells["HostelName"].Value?.ToString() ?? "";
                txtLocation.Text = row.Cells["Location"].Value?.ToString() ?? "";
                txtCapacity.Text = row.Cells["Capacity"].Value?.ToString() ?? "";
                cmbGender.Text = row.Cells["Gender"].Value?.ToString() ?? "";
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtHostelName.Text) || string.IsNullOrEmpty(txtCapacity.Text) || string.IsNullOrEmpty(cmbGender.Text))
            {
                MessageBox.Show("Please fill in all required fields.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO Hostel (HostelName, Location, Capacity, Gender) VALUES (@name, @location, @capacity, @gender)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", txtHostelName.Text);
                    cmd.Parameters.AddWithValue("@location", txtLocation.Text);
                    cmd.Parameters.AddWithValue("@capacity", Convert.ToInt32(txtCapacity.Text));
                    cmd.Parameters.AddWithValue("@gender", cmbGender.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Hostel added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields();
                    LoadHostels();
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedHostelID == 0) { MessageBox.Show("Please select a hostel to update.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE Hostel SET HostelName=@name, Location=@location, Capacity=@capacity, Gender=@gender WHERE HostelID=@id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", txtHostelName.Text);
                    cmd.Parameters.AddWithValue("@location", txtLocation.Text);
                    cmd.Parameters.AddWithValue("@capacity", Convert.ToInt32(txtCapacity.Text));
                    cmd.Parameters.AddWithValue("@gender", cmbGender.Text);
                    cmd.Parameters.AddWithValue("@id", selectedHostelID);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Hostel updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields();
                    LoadHostels();
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedHostelID == 0) { MessageBox.Show("Please select a hostel to delete.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (MessageBox.Show("Are you sure you want to delete this hostel?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("DELETE FROM Hostel WHERE HostelID=@id", conn);
                        cmd.Parameters.AddWithValue("@id", selectedHostelID);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Hostel deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFields();
                        LoadHostels();
                    }
                }
                catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            }
        }

        private void ClearFields()
        {
            txtHostelName.Text = "";
            txtLocation.Text = "";
            txtCapacity.Text = "";
            cmbGender.SelectedIndex = -1;
            selectedHostelID = 0;
        }
    }
}
