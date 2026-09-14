using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HostelManagementSystem
{
    public partial class ManageAllocations : Form
    {
        string connectionString = "Server=localhost;Database=hostel_db;Uid=root;Pwd=MilesHero010.;";
        int selectedAllocationID = 0;
        int selectedRoomID = 0;

        public ManageAllocations()
        {
            InitializeComponent();
            this.Text = "Manage Allocations";
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.Load += ManageAllocations_Load;
            BuildUI();
        }

        private void BuildUI()
        {
            this.Controls.Clear();

            // ── HEADER ───────────────────────────────────────────
            Panel pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.FromArgb(26, 86, 219)
            };

            Label lblTitle = new Label
            {
                Text = "📂  Manage Allocations",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(25, 18),
                BackColor = Color.Transparent
            };

            Label lblSub = new Label
            {
                Text = "Review, approve or reject student hostel applications",
                ForeColor = Color.FromArgb(200, 220, 255),
                Font = new Font("Segoe UI", 9),
                AutoSize = true,
                Location = new Point(27, 52),
                BackColor = Color.Transparent
            };

            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(lblSub);

            // ── TOOLBAR ──────────────────────────────────────────
            Panel pnlToolbar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.White,
                Padding = new Padding(20, 10, 20, 10)
            };

            pnlToolbar.Paint += (s, e) =>
            {
                e.Graphics.DrawLine(new Pen(Color.FromArgb(229, 231, 235), 1),
                    0, pnlToolbar.Height - 1, pnlToolbar.Width, pnlToolbar.Height - 1);
            };

            Button btnApprove = new Button
            {
                Text = "✔  Approve",
                Size = new Size(140, 38),
                Location = new Point(25, 11),
                BackColor = Color.FromArgb(5, 122, 85),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Name = "btnApprove"
            };
            btnApprove.FlatAppearance.BorderSize = 0;
            btnApprove.Click += btnApprove_Click;

            Button btnReject = new Button
            {
                Text = "✘  Reject",
                Size = new Size(140, 38),
                Location = new Point(175, 11),
                BackColor = Color.FromArgb(224, 36, 36),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Name = "btnReject"
            };
            btnReject.FlatAppearance.BorderSize = 0;
            btnReject.Click += btnReject_Click;

            Button btnRefresh = new Button
            {
                Text = "↻  Refresh",
                Size = new Size(140, 38),
                Location = new Point(325, 11),
                BackColor = Color.FromArgb(26, 86, 219),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Name = "btnRefresh"
            };
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.Click += btnRefresh_Click;

            // Status filter label
            Label lblFilter = new Label
            {
                Text = "Click a row to select, then Approve or Reject",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(107, 114, 128),
                AutoSize = true,
                Location = new Point(490, 20)
            };

            pnlToolbar.Controls.Add(btnApprove);
            pnlToolbar.Controls.Add(btnReject);
            pnlToolbar.Controls.Add(btnRefresh);
            pnlToolbar.Controls.Add(lblFilter);

            // ── MAIN CONTENT ─────────────────────────────────────
            Panel pnlContent = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(245, 247, 250),
                Padding = new Padding(20)
            };

            // ── DATA GRID ────────────────────────────────────────
            DataGridView dgv = new DataGridView
            {
                Name = "dgvAllocations",
                Dock = DockStyle.Fill,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                GridColor = Color.FromArgb(229, 231, 235),
                Font = new Font("Segoe UI", 9),
                RowTemplate = { Height = 45 },
                MultiSelect = false
            };

            // Style the header
            dgv.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(26, 86, 219),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Padding = new Padding(8, 0, 0, 0),
                SelectionBackColor = Color.FromArgb(26, 86, 219),
                SelectionForeColor = Color.White
            };
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgv.ColumnHeadersHeight = 45;
            dgv.EnableHeadersVisualStyles = false;

            // Alternating row colors
            dgv.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(249, 250, 251)
            };
            dgv.DefaultCellStyle = new DataGridViewCellStyle
            {
                Padding = new Padding(8, 0, 0, 0),
                SelectionBackColor = Color.FromArgb(219, 234, 254),
                SelectionForeColor = Color.FromArgb(17, 24, 39)
            };

            dgv.CellClick += DgvAllocations_CellClick;

            // Store reference
            dgvAllocations = dgv;

            pnlContent.Controls.Add(dgv);

            // Add to form in correct order
            this.Controls.Add(pnlContent);
            this.Controls.Add(pnlToolbar);
            this.Controls.Add(pnlHeader);
        }

        DataGridView dgvAllocations = new DataGridView();

        private void ManageAllocations_Load(object sender, EventArgs e)
        {
            LoadAllocations();
        }

        private void LoadAllocations()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT a.AllocationID,
                                    CONCAT(s.FirstName, ' ', s.LastName) AS 'Student Name',
                                    s.MatricNumber AS 'Matric No',
                                    s.Department AS 'Department',
                                    s.Level AS 'Level',
                                    h.HostelName AS 'Hostel',
                                    r.RoomNumber AS 'Room No',
                                    r.RoomType AS 'Room Type',
                                    a.AllocationDate AS 'Date Applied',
                                    a.Status AS 'Status'
                                    FROM Allocation a
                                    JOIN Student s ON a.StudentID = s.StudentID
                                    JOIN Rooms r ON a.RoomID = r.RoomID
                                    JOIN Hostel h ON r.HostelID = h.HostelID
                                    ORDER BY a.AllocationDate DESC";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvAllocations.DataSource = dt;

                    // Hide AllocationID column
                    if (dgvAllocations.Columns["AllocationID"] != null)
                        dgvAllocations.Columns["AllocationID"].Visible = false;

                    // Color the Status column based on value
                    dgvAllocations.CellFormatting += (s, e) =>
                    {
                        if (dgvAllocations.Columns[e.ColumnIndex].Name == "Status" && e.Value != null)
                        {
                            string status = e.Value.ToString() ?? "";
                            if (status == "Approved")
                            {
                                e.CellStyle.ForeColor = Color.FromArgb(5, 122, 85);
                                e.CellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                            }
                            else if (status == "Rejected")
                            {
                                e.CellStyle.ForeColor = Color.FromArgb(224, 36, 36);
                                e.CellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                            }
                            else if (status == "Pending")
                            {
                                e.CellStyle.ForeColor = Color.FromArgb(180, 83, 9);
                                e.CellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                            }
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading allocations: " + ex.Message);
            }
        }

        private void DgvAllocations_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvAllocations.Rows[e.RowIndex];
                if (row.Cells["AllocationID"].Value == null ||
                    row.Cells["AllocationID"].Value == DBNull.Value)
                    return;
                selectedAllocationID = Convert.ToInt32(row.Cells["AllocationID"].Value);
                selectedRoomID = 0;
            }
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            if (selectedAllocationID == 0)
            {
                MessageBox.Show("Please select an application to approve.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string currentStatus = GetCurrentStatus();
            if (currentStatus == "Approved")
            {
                MessageBox.Show("This application is already approved.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (currentStatus == "Rejected")
            {
                MessageBox.Show("This application was rejected and cannot be approved.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Approve this hostel application?",
                "Confirm Approval", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();

                        string getRoomQuery = "SELECT RoomID FROM Allocation WHERE AllocationID=@id";
                        MySqlCommand getRoomCmd = new MySqlCommand(getRoomQuery, conn);
                        getRoomCmd.Parameters.AddWithValue("@id", selectedAllocationID);
                        selectedRoomID = Convert.ToInt32(getRoomCmd.ExecuteScalar());

                        string approveQuery = "UPDATE Allocation SET Status='Approved' WHERE AllocationID=@id";
                        MySqlCommand approveCmd = new MySqlCommand(approveQuery, conn);
                        approveCmd.Parameters.AddWithValue("@id", selectedAllocationID);
                        approveCmd.ExecuteNonQuery();

                        string roomQuery = "UPDATE Rooms SET Status='Occupied' WHERE RoomID=@roomID";
                        MySqlCommand roomCmd = new MySqlCommand(roomQuery, conn);
                        roomCmd.Parameters.AddWithValue("@roomID", selectedRoomID);
                        roomCmd.ExecuteNonQuery();

                        MessageBox.Show("Application approved! Room has been assigned.", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        selectedAllocationID = 0;
                        LoadAllocations();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error approving: " + ex.Message);
                }
            }
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            if (selectedAllocationID == 0)
            {
                MessageBox.Show("Please select an application to reject.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string currentStatus = GetCurrentStatus();
            if (currentStatus == "Approved")
            {
                MessageBox.Show("This application is already approved.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (currentStatus == "Rejected")
            {
                MessageBox.Show("This application is already rejected.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show("Reject this hostel application?",
                "Confirm Rejection", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "UPDATE Allocation SET Status='Rejected' WHERE AllocationID=@id";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@id", selectedAllocationID);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Application rejected.", "Rejected",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        selectedAllocationID = 0;
                        LoadAllocations();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error rejecting: " + ex.Message);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            selectedAllocationID = 0;
            LoadAllocations();
        }

        private string GetCurrentStatus()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT Status FROM Allocation WHERE AllocationID=@id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", selectedAllocationID);
                    return cmd.ExecuteScalar()?.ToString() ?? "";
                }
            }
            catch { return ""; }
        }
    }
}