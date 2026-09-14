using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HostelManagementSystem
{
    public partial class Reports : Form
    {
        string connectionString = "Server=localhost;Database=hostel_db;Uid=root;Pwd=MilesHero010.;";
        DataGridView dgvReport = new DataGridView();
        Label lblReportTitle = new Label();

        public Reports()
        {
            InitializeComponent();
            this.Text = "Reports";
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.FromArgb(245, 247, 250);
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
                Text = "📋  Reports & Analytics",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(25, 15),
                BackColor = Color.Transparent
            };

            Label lblSub = new Label
            {
                Text = "Generate and view hostel management reports",
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

            // ── REPORT BUTTONS (TOP ROW) ──────────────────────────
            Panel pnlButtons = new Panel
            {
                Location = new Point(20, 20),
                Size = new Size(1400, 110),
                BackColor = Color.Transparent,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            string[] reportNames = {
                "All Students",
                "All Hostels",
                "Room Occupancy",
                "Approved Allocations",
                "Pending Applications",
                "Rejected Applications"
            };

            string[] reportIcons = { "👤", "🏠", "🛏", "✅", "⏳", "❌" };

            Color[] btnColors = {
                Color.FromArgb(26, 86, 219),
                Color.FromArgb(5, 122, 85),
                Color.FromArgb(109, 40, 217),
                Color.FromArgb(5, 122, 85),
                Color.FromArgb(180, 83, 9),
                Color.FromArgb(224, 36, 36)
            };

            int bx = 0;
            for (int i = 0; i < reportNames.Length; i++)
            {
                string reportName = reportNames[i];
                string icon = reportIcons[i];
                Color btnColor = btnColors[i];

                Panel btnCard = new Panel
                {
                    Size = new Size(195, 95),
                    Location = new Point(bx, 0),
                    BackColor = Color.White,
                    Cursor = Cursors.Hand
                };
                btnCard.Paint += (s, e) =>
                {
                    ControlPaint.DrawBorder(e.Graphics, btnCard.ClientRectangle,
                        Color.FromArgb(229, 231, 235), ButtonBorderStyle.Solid);
                };

                Label lblIcon = new Label
                {
                    Text = icon,
                    Font = new Font("Segoe UI", 20),
                    Location = new Point(15, 12),
                    Size = new Size(40, 40),
                    BackColor = Color.Transparent,
                    TextAlign = ContentAlignment.MiddleCenter,
                    AutoSize = false
                };

                Label lblBtnName = new Label
                {
                    Text = reportName,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    ForeColor = Color.FromArgb(17, 24, 39),
                    AutoSize = true,
                    Location = new Point(58, 20)
                };

                Label lblBtnSub = new Label
                {
                    Text = "Click to generate",
                    Font = new Font("Segoe UI", 7),
                    ForeColor = Color.FromArgb(107, 114, 128),
                    AutoSize = true,
                    Location = new Point(58, 45)
                };

                // Color bar on left
                Panel colorBar = new Panel
                {
                    Size = new Size(5, 95),
                    Location = new Point(0, 0),
                    BackColor = btnColor
                };

                btnCard.Controls.Add(colorBar);
                btnCard.Controls.Add(lblIcon);
                btnCard.Controls.Add(lblBtnName);
                btnCard.Controls.Add(lblBtnSub);

                // Click event
                btnCard.Click += (s, e) => GenerateReport(reportName);
                lblIcon.Click += (s, e) => GenerateReport(reportName);
                lblBtnName.Click += (s, e) => GenerateReport(reportName);
                lblBtnSub.Click += (s, e) => GenerateReport(reportName);

                pnlButtons.Controls.Add(btnCard);
                bx += 205;
            }

            pnlMain.Resize += (s, e) =>
            {
                pnlButtons.Width = pnlMain.Width - 40;
                int cardW = (pnlButtons.Width - (6 * 10)) / 6;
                for (int i = 0; i < pnlButtons.Controls.Count; i++)
                {
                    pnlButtons.Controls[i].Width = cardW;
                    pnlButtons.Controls[i].Location = new Point(i * (cardW + 10), 0);
                }
            };

            // ── REPORT DISPLAY PANEL ─────────────────────────────
            Panel pnlReport = new Panel
            {
                Location = new Point(20, 145),
                BackColor = Color.White,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };
            pnlReport.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, pnlReport.ClientRectangle,
                    Color.FromArgb(229, 231, 235), ButtonBorderStyle.Solid);
            };

            // Report header inside panel
            Panel pnlReportHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 55,
                BackColor = Color.White
            };
            pnlReportHeader.Paint += (s, e) =>
            {
                e.Graphics.DrawLine(new Pen(Color.FromArgb(229, 231, 235), 1),
                    0, pnlReportHeader.Height - 1, pnlReportHeader.Width, pnlReportHeader.Height - 1);
            };

            lblReportTitle = new Label
            {
                Text = "Select a report above to generate",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(17, 24, 39),
                AutoSize = true,
                Location = new Point(20, 15)
            };

            // Print button
            Button btnPrint = new Button
            {
                Text = "🖨  Print Report",
                Size = new Size(150, 35),
                Location = new Point(900, 10),
                BackColor = Color.FromArgb(26, 86, 219),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            btnPrint.FlatAppearance.BorderSize = 0;
            btnPrint.Click += BtnPrint_Click;

            pnlReportHeader.Controls.Add(lblReportTitle);
            pnlReportHeader.Controls.Add(btnPrint);

            pnlReportHeader.Resize += (s, e) =>
            {
                btnPrint.Location = new Point(pnlReportHeader.Width - 170, 10);
            };

            // DataGridView
            dgvReport = new DataGridView
            {
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
                RowTemplate = { Height = 40 },
                MultiSelect = false
            };
            dgvReport.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(26, 86, 219),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Padding = new Padding(8, 0, 0, 0),
                SelectionBackColor = Color.FromArgb(26, 86, 219),
                SelectionForeColor = Color.White
            };
            dgvReport.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvReport.ColumnHeadersHeight = 42;
            dgvReport.EnableHeadersVisualStyles = false;
            dgvReport.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(249, 250, 251)
            };
            dgvReport.DefaultCellStyle = new DataGridViewCellStyle
            {
                Padding = new Padding(8, 0, 0, 0),
                SelectionBackColor = Color.FromArgb(219, 234, 254),
                SelectionForeColor = Color.FromArgb(17, 24, 39)
            };

            pnlReport.Controls.Add(dgvReport);
            pnlReport.Controls.Add(pnlReportHeader);

            pnlMain.Resize += (s, e) =>
            {
                pnlReport.Size = new Size(pnlMain.Width - 40, pnlMain.Height - 165);
            };

            pnlMain.Controls.Add(pnlButtons);
            pnlMain.Controls.Add(pnlReport);

            this.Controls.Add(pnlMain);
            this.Controls.Add(pnlHeader);
        }

        private void GenerateReport(string reportName)
        {
            lblReportTitle.Text = "📊  " + reportName + " Report";
            string query = "";

            switch (reportName)
            {
                case "All Students":
                    query = @"SELECT 
                                CONCAT(FirstName, ' ', LastName) AS 'Full Name',
                                MatricNumber AS 'Matric No',
                                Department AS 'Department',
                                Level AS 'Level',
                                Gender AS 'Gender',
                                PhoneNumber AS 'Phone',
                                Email AS 'Email'
                              FROM Student ORDER BY LastName";
                    break;

                case "All Hostels":
                    query = @"SELECT 
                                HostelName AS 'Hostel Name',
                                Location AS 'Location',
                                Capacity AS 'Capacity',
                                Gender AS 'Gender',
                                (SELECT COUNT(*) FROM Rooms WHERE HostelID = Hostel.HostelID) AS 'Total Rooms',
                                (SELECT COUNT(*) FROM Rooms WHERE HostelID = Hostel.HostelID AND Status='Available') AS 'Available Rooms',
                                (SELECT COUNT(*) FROM Rooms WHERE HostelID = Hostel.HostelID AND Status='Occupied') AS 'Occupied Rooms'
                              FROM Hostel ORDER BY HostelName";
                    break;

                case "Room Occupancy":
                    query = @"SELECT 
                                h.HostelName AS 'Hostel',
                                r.RoomNumber AS 'Room No',
                                r.RoomType AS 'Room Type',
                                r.Status AS 'Status'
                              FROM Rooms r
                              JOIN Hostel h ON r.HostelID = h.HostelID
                              ORDER BY h.HostelName, r.RoomNumber";
                    break;

                case "Approved Allocations":
                    query = @"SELECT 
                                CONCAT(s.FirstName, ' ', s.LastName) AS 'Student Name',
                                s.MatricNumber AS 'Matric No',
                                s.Department AS 'Department',
                                h.HostelName AS 'Hostel',
                                r.RoomNumber AS 'Room No',
                                r.RoomType AS 'Room Type',
                                a.AllocationDate AS 'Date Approved'
                              FROM Allocation a
                              JOIN Student s ON a.StudentID = s.StudentID
                              JOIN Rooms r ON a.RoomID = r.RoomID
                              JOIN Hostel h ON r.HostelID = h.HostelID
                              WHERE a.Status = 'Approved'
                              ORDER BY a.AllocationDate DESC";
                    break;

                case "Pending Applications":
                    query = @"SELECT 
                                CONCAT(s.FirstName, ' ', s.LastName) AS 'Student Name',
                                s.MatricNumber AS 'Matric No',
                                s.Department AS 'Department',
                                h.HostelName AS 'Hostel',
                                r.RoomNumber AS 'Room No',
                                r.RoomType AS 'Room Type',
                                a.AllocationDate AS 'Date Applied'
                              FROM Allocation a
                              JOIN Student s ON a.StudentID = s.StudentID
                              JOIN Rooms r ON a.RoomID = r.RoomID
                              JOIN Hostel h ON r.HostelID = h.HostelID
                              WHERE a.Status = 'Pending'
                              ORDER BY a.AllocationDate DESC";
                    break;

                case "Rejected Applications":
                    query = @"SELECT 
                                CONCAT(s.FirstName, ' ', s.LastName) AS 'Student Name',
                                s.MatricNumber AS 'Matric No',
                                s.Department AS 'Department',
                                h.HostelName AS 'Hostel',
                                r.RoomNumber AS 'Room No',
                                r.RoomType AS 'Room Type',
                                a.AllocationDate AS 'Date Applied'
                              FROM Allocation a
                              JOIN Student s ON a.StudentID = s.StudentID
                              JOIN Rooms r ON a.RoomID = r.RoomID
                              JOIN Hostel h ON r.HostelID = h.HostelID
                              WHERE a.Status = 'Rejected'
                              ORDER BY a.AllocationDate DESC";
                    break;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvReport.DataSource = dt;

                    // Color status column if exists
                    dgvReport.CellFormatting += (s, e) =>
                    {
                        if (e.ColumnIndex >= 0 && dgvReport.Columns[e.ColumnIndex].Name == "Status" && e.Value != null)
                        {
                            string status = e.Value.ToString() ?? "";
                            if (status == "Available" || status == "Approved")
                            { e.CellStyle.ForeColor = Color.FromArgb(5, 122, 85); e.CellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold); }
                            else if (status == "Occupied" || status == "Rejected")
                            { e.CellStyle.ForeColor = Color.FromArgb(224, 36, 36); e.CellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold); }
                            else if (status == "Maintenance" || status == "Pending")
                            { e.CellStyle.ForeColor = Color.FromArgb(180, 83, 9); e.CellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold); }
                        }
                    };

                    // Show record count
                    lblReportTitle.Text = $"📊  {reportName} Report  —  {dt.Rows.Count} records found";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating report: " + ex.Message);
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            if (dgvReport.DataSource == null || dgvReport.Rows.Count == 0)
            {
                MessageBox.Show("Please generate a report first.", "No Data",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            MessageBox.Show("Print functionality will be available in the next update.\n\nFor now, you can take a screenshot or export the data.",
                "Print Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}