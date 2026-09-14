using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HostelManagementSystem
{
    public partial class ManageRooms : Form
    {
        string connectionString = "Server=localhost;Database=hostel_db;Uid=root;Pwd=MilesHero010.;";
        int selectedRoomID = 0;

        TextBox txtRoomNumber = new TextBox();
        ComboBox cmbHostel = new ComboBox();
        ComboBox cmbRoomType = new ComboBox();
        ComboBox cmbStatus = new ComboBox();
        DataGridView dgvRooms = new DataGridView();

        public ManageRooms()
        {
            InitializeComponent();
            this.Text = "Manage Rooms";
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.Load += ManageRooms_Load;
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
                Text = "🏢  Manage Rooms",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(25, 15),
                BackColor = Color.Transparent
            };

            Label lblSub = new Label
            {
                Text = "Add, update or delete hostel rooms",
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
                Size = new Size(350, 540),
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
                Text = "Room Details",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(17, 24, 39),
                AutoSize = true,
                Location = new Point(20, 20)
            };

            // Hostel
            Label lblHos = new Label { Text = "Hostel:", Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.FromArgb(51, 65, 85), AutoSize = true, Location = new Point(20, 65) };
            cmbHostel = new ComboBox { Font = new Font("Segoe UI", 10), Location = new Point(20, 88), Size = new Size(310, 30), DropDownStyle = ComboBoxStyle.DropDownList };

            // Room Number
            Label lblNum = new Label { Text = "Room Number:", Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.FromArgb(51, 65, 85), AutoSize = true, Location = new Point(20, 135) };
            txtRoomNumber = new TextBox { Font = new Font("Segoe UI", 10), Location = new Point(20, 158), Size = new Size(310, 30), BorderStyle = BorderStyle.FixedSingle };

            // Room Type
            Label lblType = new Label { Text = "Room Type:", Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.FromArgb(51, 65, 85), AutoSize = true, Location = new Point(20, 205) };
            cmbRoomType = new ComboBox { Font = new Font("Segoe UI", 10), Location = new Point(20, 228), Size = new Size(310, 30), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbRoomType.Items.AddRange(new string[] { "Single", "Double Occupancy", "Triple Occupancy" });

            // Status
            Label lblStat = new Label { Text = "Status:", Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.FromArgb(51, 65, 85), AutoSize = true, Location = new Point(20, 275) };
            cmbStatus = new ComboBox { Font = new Font("Segoe UI", 10), Location = new Point(20, 298), Size = new Size(310, 30), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbStatus.Items.AddRange(new string[] { "Available", "Occupied", "Maintenance" });

            // Buttons
            Button btnAdd = new Button
            {
                Text = "➕  Add Room",
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
            pnlForm.Controls.Add(lblHos);
            pnlForm.Controls.Add(cmbHostel);
            pnlForm.Controls.Add(lblNum);
            pnlForm.Controls.Add(txtRoomNumber);
            pnlForm.Controls.Add(lblType);
            pnlForm.Controls.Add(cmbRoomType);
            pnlForm.Controls.Add(lblStat);
            pnlForm.Controls.Add(cmbStatus);
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
                Text = "All Rooms",
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

            dgvRooms = new DataGridView
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
            dgvRooms.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(26, 86, 219),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Padding = new Padding(8, 0, 0, 0),
                SelectionBackColor = Color.FromArgb(26, 86, 219),
                SelectionForeColor = Color.White
            };
            dgvRooms.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvRooms.ColumnHeadersHeight = 42;
            dgvRooms.EnableHeadersVisualStyles = false;
            dgvRooms.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(249, 250, 251) };
            dgvRooms.DefaultCellStyle = new DataGridViewCellStyle
            {
                Padding = new Padding(8, 0, 0, 0),
                SelectionBackColor = Color.FromArgb(219, 234, 254),
                SelectionForeColor = Color.FromArgb(17, 24, 39)
            };
            dgvRooms.CellClick += DgvRooms_CellClick;

            // Color Status column
            dgvRooms.CellFormatting += (s, e) =>
            {
                if (dgvRooms.Columns.Count > e.ColumnIndex && dgvRooms.Columns[e.ColumnIndex].Name == "Status" && e.Value != null)
                {
                    string status = e.Value.ToString() ?? "";
                    if (status == "Available") { e.CellStyle.ForeColor = Color.FromArgb(5, 122, 85); e.CellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold); }
                    else if (status == "Occupied") { e.CellStyle.ForeColor = Color.FromArgb(224, 36, 36); e.CellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold); }
                    else if (status == "Maintenance") { e.CellStyle.ForeColor = Color.FromArgb(180, 83, 9); e.CellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold); }
                }
            };

            pnlGrid.Controls.Add(lblGridTitle);
            pnlGrid.Controls.Add(lblHint);
            pnlGrid.Controls.Add(dgvRooms);

            pnlMain.Resize += (s, e) =>
            {
                pnlGrid.Size = new Size(pnlMain.Width - 410, pnlMain.Height - 40);
                dgvRooms.Size = new Size(pnlGrid.Width, pnlGrid.Height - 65);
            };

            pnlMain.Controls.Add(pnlForm);
            pnlMain.Controls.Add(pnlGrid);

            this.Controls.Add(pnlMain);
            this.Controls.Add(pnlHeader);
        }

        private void ManageRooms_Load(object sender, EventArgs e)
        {
            LoadHostelsIntoComboBox();
            LoadRooms();
            dgvRooms.CellClick += DgvRooms_CellClick;
        }

        private void LoadHostelsIntoComboBox()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT HostelID, HostelName FROM Hostel", conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    cmbHostel.DataSource = dt;
                    cmbHostel.DisplayMember = "HostelName";
                    cmbHostel.ValueMember = "HostelID";
                    cmbHostel.SelectedIndex = -1;
                }
            }
            catch (Exception ex) { MessageBox.Show("Error loading hostels: " + ex.Message); }
        }

        private void LoadRooms()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT r.RoomID, h.HostelName, r.RoomNumber, r.RoomType, r.Status
                                    FROM Rooms r JOIN Hostel h ON r.HostelID = h.HostelID";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvRooms.DataSource = dt;
                    if (dgvRooms.Columns["RoomID"] != null)
                        dgvRooms.Columns["RoomID"].Visible = false;
                }
            }
            catch (Exception ex) { MessageBox.Show("Error loading rooms: " + ex.Message); }
        }

        private void DgvRooms_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvRooms.Rows[e.RowIndex];
                if (row.Cells["RoomID"].Value == null || row.Cells["RoomID"].Value == DBNull.Value) return;
                selectedRoomID = Convert.ToInt32(row.Cells["RoomID"].Value);
                txtRoomNumber.Text = row.Cells["RoomNumber"].Value?.ToString() ?? "";
                cmbRoomType.Text = row.Cells["RoomType"].Value?.ToString() ?? "";
                cmbStatus.Text = row.Cells["Status"].Value?.ToString() ?? "";
                string hostelName = row.Cells["HostelName"].Value?.ToString() ?? "";
                foreach (DataRowView item in cmbHostel.Items)
                {
                    if (item["HostelName"].ToString() == hostelName)
                    {
                        cmbHostel.SelectedItem = item;
                        break;
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbHostel.SelectedIndex == -1 || string.IsNullOrEmpty(txtRoomNumber.Text) || string.IsNullOrEmpty(cmbRoomType.Text) || string.IsNullOrEmpty(cmbStatus.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO Rooms (HostelID, RoomNumber, RoomType, Status) VALUES (@hostelID, @roomNumber, @roomType, @status)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@hostelID", cmbHostel.SelectedValue);
                    cmd.Parameters.AddWithValue("@roomNumber", txtRoomNumber.Text);
                    cmd.Parameters.AddWithValue("@roomType", cmbRoomType.Text);
                    cmd.Parameters.AddWithValue("@status", cmbStatus.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Room added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields();
                    LoadRooms();
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedRoomID == 0) { MessageBox.Show("Please select a room to update.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE Rooms SET HostelID=@hostelID, RoomNumber=@roomNumber, RoomType=@roomType, Status=@status WHERE RoomID=@id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@hostelID", cmbHostel.SelectedValue);
                    cmd.Parameters.AddWithValue("@roomNumber", txtRoomNumber.Text);
                    cmd.Parameters.AddWithValue("@roomType", cmbRoomType.Text);
                    cmd.Parameters.AddWithValue("@status", cmbStatus.Text);
                    cmd.Parameters.AddWithValue("@id", selectedRoomID);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Room updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields();
                    LoadRooms();
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedRoomID == 0) { MessageBox.Show("Please select a room to delete.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (MessageBox.Show("Are you sure you want to delete this room?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("DELETE FROM Rooms WHERE RoomID=@id", conn);
                        cmd.Parameters.AddWithValue("@id", selectedRoomID);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Room deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFields();
                        LoadRooms();
                    }
                }
                catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            }
        }

        private void ClearFields()
        {
            cmbHostel.SelectedIndex = -1;
            txtRoomNumber.Text = "";
            cmbRoomType.SelectedIndex = -1;
            cmbStatus.SelectedIndex = -1;
            selectedRoomID = 0;
        }
    }
}