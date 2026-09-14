using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HostelManagementSystem
{
    public partial class StudentDashboard : Form
    {
        string connectionString = "Server=localhost;Database=hostel_db;Uid=root;Pwd=MilesHero010.;";
        int studentID = 0;
        string studentName = "";

        // Profile fields
        TextBox txtFirstName = new TextBox();
        TextBox txtLastName = new TextBox();
        TextBox txtMatric = new TextBox();
        TextBox txtDepartment = new TextBox();
        TextBox txtPhone = new TextBox();
        TextBox txtEmail = new TextBox();
        TextBox txtPassword = new TextBox();
        ComboBox cmbGender = new ComboBox();
        ComboBox cmbLevel = new ComboBox();
        ComboBox cmbSession = new ComboBox();

        // Application fields
        ComboBox cmbHostel = new ComboBox();
        ComboBox cmbRoomType = new ComboBox();

        // Display
        DataGridView dgvApplications = new DataGridView();
        Label lblAllocationStatus = new Label();
        Label lblHostelName = new Label();
        Label lblRoomNumber = new Label();
        Label lblRoomType = new Label();
        Label lblDateApplied = new Label();
        Label lblWelcome = new Label();

        // Tab panels
        Panel pnlProfileTab = new Panel();
        Panel pnlApplicationTab = new Panel();
        Button btnTabProfile = new Button();
        Button btnTabApplication = new Button();

        public StudentDashboard(int id, string name)
        {
            InitializeComponent();
            studentID = id;
            studentName = name;
            this.Text = "Student Dashboard";
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.Load += StudentDashboard_Load;
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

            Label lblLogo = new Label
            {
                Text = "🏛",
                Font = new Font("Segoe UI", 20),
                ForeColor = Color.White,
                Location = new Point(18, 20),
                Size = new Size(45, 45),
                BackColor = Color.Transparent
            };

            Label lblAppName = new Label
            {
                Text = "SDU Campus Hostel Management System",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(68, 18),
                BackColor = Color.Transparent
            };

            Label lblUniversity = new Label
            {
                Text = "Southern Delta University",
                ForeColor = Color.FromArgb(200, 220, 255),
                Font = new Font("Segoe UI", 9),
                AutoSize = true,
                Location = new Point(69, 50),
                BackColor = Color.Transparent
            };

            Button btnLogout = new Button
            {
                Text = "⬡ Logout",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(200, 30, 30),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(110, 36),
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.Click += BtnLogout_Click;
            pnlHeader.Resize += (s, e) => btnLogout.Location = new Point(pnlHeader.Width - 125, 25);

            pnlHeader.Controls.Add(lblLogo);
            pnlHeader.Controls.Add(lblAppName);
            pnlHeader.Controls.Add(lblUniversity);
            pnlHeader.Controls.Add(btnLogout);

            // ── WELCOME BANNER ───────────────────────────────────
            Panel pnlBanner = new Panel
            {
                Dock = DockStyle.Top,
                Height = 85,
                BackColor = Color.FromArgb(37, 99, 235)
            };

            lblWelcome = new Label
            {
                Text = "Welcome, " + studentName + "!",
                Font = new Font("Segoe UI", 17, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(25, 15),
                BackColor = Color.Transparent
            };

            Label lblWelcomeSub = new Label
            {
                Text = "Manage your profile, submit hostel applications and track your allocation status",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(200, 220, 255),
                AutoSize = true,
                Location = new Point(25, 55),
                BackColor = Color.Transparent
            };

            pnlBanner.Controls.Add(lblWelcome);
            pnlBanner.Controls.Add(lblWelcomeSub);

            // ── TAB BAR ──────────────────────────────────────────
            Panel pnlTabBar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = Color.White
            };
            pnlTabBar.Paint += (s, e) =>
            {
                e.Graphics.DrawLine(new Pen(Color.FromArgb(229, 231, 235), 1),
                    0, pnlTabBar.Height - 1, pnlTabBar.Width, pnlTabBar.Height - 1);
            };

            btnTabProfile = new Button
            {
                Text = "👤  My Profile",
                Size = new Size(160, 48),
                Location = new Point(20, 1),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(26, 86, 219),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnTabProfile.FlatAppearance.BorderSize = 0;
            btnTabProfile.FlatAppearance.BorderColor = Color.FromArgb(26, 86, 219);
            btnTabProfile.Paint += (s, e) =>
            {
                if (btnTabProfile.Tag?.ToString() == "active")
                    e.Graphics.DrawLine(new Pen(Color.FromArgb(26, 86, 219), 3),
                        0, btnTabProfile.Height - 3, btnTabProfile.Width, btnTabProfile.Height - 3);
            };
            btnTabProfile.Click += (s, e) => SwitchTab("profile");

            btnTabApplication = new Button
            {
                Text = "🏠  Hostel Application",
                Size = new Size(200, 48),
                Location = new Point(185, 1),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(107, 114, 128),
                Font = new Font("Segoe UI", 10),
                Cursor = Cursors.Hand
            };
            btnTabApplication.FlatAppearance.BorderSize = 0;
            btnTabApplication.Click += (s, e) => SwitchTab("application");

            pnlTabBar.Controls.Add(btnTabProfile);
            pnlTabBar.Controls.Add(btnTabApplication);

            // ── CONTENT AREA ──────────────────────────────────────
            Panel pnlContent = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(245, 247, 250),
                Padding = new Padding(20),
                AutoScroll = true
            };

            // ═══ PROFILE TAB ═════════════════════════════════════
            pnlProfileTab = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Visible = true
            };

            // Profile form card
            Panel pnlProfileCard = new Panel
            {
                Location = new Point(0, 5),
                Size = new Size(900, 560),
                BackColor = Color.White,
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };
            pnlProfileCard.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, pnlProfileCard.ClientRectangle,
                    Color.FromArgb(229, 231, 235), ButtonBorderStyle.Solid);
            };

            Label lblProfileTitle = new Label
            {
                Text = "Personal Information",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(17, 24, 39),
                AutoSize = true,
                Location = new Point(25, 22)
            };

            Label lblProfileSub = new Label
            {
                Text = "Update your personal details below",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(107, 114, 128),
                AutoSize = true,
                Location = new Point(25, 50)
            };

            // Row 1: First Name + Last Name
            Label lbl1 = MakeLabel("First Name:", 25, 90);
            txtFirstName = MakeTextBox(25, 115, 380);

            Label lbl2 = MakeLabel("Last Name:", 440, 90);
            txtLastName = MakeTextBox(440, 115, 380);

            // Row 2: Matric + Gender
            Label lbl3 = MakeLabel("Matriculation Number:", 25, 175);
            txtMatric = MakeTextBox(25, 200, 380);
            txtMatric.ReadOnly = true;
            txtMatric.BackColor = Color.FromArgb(243, 244, 246);

            Label lbl4 = MakeLabel("Gender:", 440, 175);
            cmbGender = MakeComboBox(440, 200, 380);
            cmbGender.Items.AddRange(new string[] { "Male", "Female" });

            // Row 3: Department + Level
            Label lbl5 = MakeLabel("Department:", 25, 260);
            txtDepartment = MakeTextBox(25, 285, 380);

            Label lbl6 = MakeLabel("Level:", 440, 260);
            cmbLevel = MakeComboBox(440, 285, 380);
            cmbLevel.Items.AddRange(new string[] { "100", "200", "300", "400", "500" });

            // Row 4: Session + Phone
            Label lbl7 = MakeLabel("Academic Session:", 25, 345);
            cmbSession = MakeComboBox(25, 370, 380);
            cmbSession.Items.AddRange(new string[] {
                "2020/2021", "2021/2022", "2022/2023",
                "2023/2024", "2024/2025", "2025/2026"
            });

            Label lbl8 = MakeLabel("Phone Number:", 440, 345);
            txtPhone = MakeTextBox(440, 370, 380);

            // Row 5: Email + Password
            Label lbl9 = MakeLabel("Email Address:", 25, 430);
            txtEmail = MakeTextBox(25, 455, 380);

            Label lbl10 = MakeLabel("New Password (leave blank to keep):", 440, 430);
            txtPassword = MakeTextBox(440, 455, 380);
            txtPassword.UseSystemPasswordChar = true;

            // Save Button
            Button btnSave = new Button
            {
                Text = "💾  Save Profile",
                Size = new Size(200, 45),
                Location = new Point(25, 510),
                BackColor = Color.FromArgb(26, 86, 219),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += BtnSaveProfile_Click;

            pnlProfileCard.Controls.Add(lblProfileTitle);
            pnlProfileCard.Controls.Add(lblProfileSub);
            pnlProfileCard.Controls.Add(lbl1); pnlProfileCard.Controls.Add(txtFirstName);
            pnlProfileCard.Controls.Add(lbl2); pnlProfileCard.Controls.Add(txtLastName);
            pnlProfileCard.Controls.Add(lbl3); pnlProfileCard.Controls.Add(txtMatric);
            pnlProfileCard.Controls.Add(lbl4); pnlProfileCard.Controls.Add(cmbGender);
            pnlProfileCard.Controls.Add(lbl5); pnlProfileCard.Controls.Add(txtDepartment);
            pnlProfileCard.Controls.Add(lbl6); pnlProfileCard.Controls.Add(cmbLevel);
            pnlProfileCard.Controls.Add(lbl7); pnlProfileCard.Controls.Add(cmbSession);
            pnlProfileCard.Controls.Add(lbl8); pnlProfileCard.Controls.Add(txtPhone);
            pnlProfileCard.Controls.Add(lbl9); pnlProfileCard.Controls.Add(txtEmail);
            pnlProfileCard.Controls.Add(lbl10); pnlProfileCard.Controls.Add(txtPassword);
            pnlProfileCard.Controls.Add(btnSave);

            pnlProfileTab.Controls.Add(pnlProfileCard);

            // ═══ APPLICATION TAB ══════════════════════════════════
            pnlApplicationTab = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Visible = false
            };

            // Status Card
            Panel pnlStatusCard = new Panel
            {
                Location = new Point(0, 5),
                Size = new Size(580, 210),
                BackColor = Color.White
            };
            pnlStatusCard.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, pnlStatusCard.ClientRectangle,
                    Color.FromArgb(229, 231, 235), ButtonBorderStyle.Solid);
            };

            Label lblStatusTitle = new Label
            {
                Text = "🏠  Current Allocation Status",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(17, 24, 39),
                AutoSize = true,
                Location = new Point(18, 18)
            };

            lblAllocationStatus = new Label
            {
                Text = "Loading...",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(107, 114, 128),
                Location = new Point(18, 55),
                Size = new Size(130, 30),
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = false
            };

            Label lbHLbl = new Label { Text = "Hostel:", Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.FromArgb(107, 114, 128), AutoSize = true, Location = new Point(18, 102) };
            lblHostelName = new Label { Text = "—", Font = new Font("Segoe UI", 9), ForeColor = Color.FromArgb(17, 24, 39), AutoSize = true, Location = new Point(95, 102) };

            Label lbRLbl = new Label { Text = "Room:", Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.FromArgb(107, 114, 128), AutoSize = true, Location = new Point(18, 132) };
            lblRoomNumber = new Label { Text = "—", Font = new Font("Segoe UI", 9), ForeColor = Color.FromArgb(17, 24, 39), AutoSize = true, Location = new Point(95, 132) };

            Label lbTLbl = new Label { Text = "Type:", Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.FromArgb(107, 114, 128), AutoSize = true, Location = new Point(18, 162) };
            lblRoomType = new Label { Text = "—", Font = new Font("Segoe UI", 9), ForeColor = Color.FromArgb(17, 24, 39), AutoSize = true, Location = new Point(95, 162) };

            Label lbDLbl = new Label { Text = "Applied:", Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.FromArgb(107, 114, 128), AutoSize = true, Location = new Point(310, 102) };
            lblDateApplied = new Label { Text = "—", Font = new Font("Segoe UI", 9), ForeColor = Color.FromArgb(17, 24, 39), AutoSize = true, Location = new Point(395, 102) };

            pnlStatusCard.Controls.Add(lblStatusTitle);
            pnlStatusCard.Controls.Add(lblAllocationStatus);
            pnlStatusCard.Controls.Add(lbHLbl); pnlStatusCard.Controls.Add(lblHostelName);
            pnlStatusCard.Controls.Add(lbRLbl); pnlStatusCard.Controls.Add(lblRoomNumber);
            pnlStatusCard.Controls.Add(lbTLbl); pnlStatusCard.Controls.Add(lblRoomType);
            pnlStatusCard.Controls.Add(lbDLbl); pnlStatusCard.Controls.Add(lblDateApplied);

            // Apply Card
            Panel pnlApplyCard = new Panel
            {
                Location = new Point(600, 5),
                Size = new Size(580, 210),
                BackColor = Color.White
            };
            pnlApplyCard.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, pnlApplyCard.ClientRectangle,
                    Color.FromArgb(229, 231, 235), ButtonBorderStyle.Solid);
            };

            Label lblApplyTitle = new Label
            {
                Text = "📝  Submit Hostel Application",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(17, 24, 39),
                AutoSize = true,
                Location = new Point(18, 18)
            };

            Label lblHosLbl = new Label { Text = "Select Hostel:", Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.FromArgb(51, 65, 85), AutoSize = true, Location = new Point(18, 62) };
            cmbHostel = MakeComboBox(18, 85, 258);

            Label lblRTLbl = new Label { Text = "Room Type:", Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.FromArgb(51, 65, 85), AutoSize = true, Location = new Point(295, 62) };
            cmbRoomType = MakeComboBox(295, 85, 258);
            cmbRoomType.Items.AddRange(new string[] { "Single", "Double Occupancy", "Triple Occupancy" });

            Button btnApply = new Button
            {
                Text = "📤  Submit Application",
                Size = new Size(535, 45),
                Location = new Point(18, 148),
                BackColor = Color.FromArgb(26, 86, 219),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnApply.FlatAppearance.BorderSize = 0;
            btnApply.Click += BtnApply_Click;

            pnlApplyCard.Controls.Add(lblApplyTitle);
            pnlApplyCard.Controls.Add(lblHosLbl); pnlApplyCard.Controls.Add(cmbHostel);
            pnlApplyCard.Controls.Add(lblRTLbl); pnlApplyCard.Controls.Add(cmbRoomType);
            pnlApplyCard.Controls.Add(btnApply);

            // History Table
            Panel pnlHistory = new Panel
            {
                Location = new Point(0, 235),
                Size = new Size(1180, 350),
                BackColor = Color.White
            };
            pnlHistory.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, pnlHistory.ClientRectangle,
                    Color.FromArgb(229, 231, 235), ButtonBorderStyle.Solid);
            };

            Panel pnlHistHdr = new Panel
            {
                Dock = DockStyle.Top,
                Height = 55,
                BackColor = Color.White
            };
            pnlHistHdr.Paint += (s, e) =>
            {
                e.Graphics.DrawLine(new Pen(Color.FromArgb(229, 231, 235), 1),
                    0, pnlHistHdr.Height - 1, pnlHistHdr.Width, pnlHistHdr.Height - 1);
            };

            Label lblHistTitle = new Label
            {
                Text = "📋  My Application History",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(17, 24, 39),
                AutoSize = true,
                Location = new Point(18, 16)
            };
            pnlHistHdr.Controls.Add(lblHistTitle);

            dgvApplications = new DataGridView
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
            dgvApplications.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(26, 86, 219),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Padding = new Padding(8, 0, 0, 0),
                SelectionBackColor = Color.FromArgb(26, 86, 219),
                SelectionForeColor = Color.White
            };
            dgvApplications.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvApplications.ColumnHeadersHeight = 42;
            dgvApplications.EnableHeadersVisualStyles = false;
            dgvApplications.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(249, 250, 251) };
            dgvApplications.DefaultCellStyle = new DataGridViewCellStyle
            {
                Padding = new Padding(8, 0, 0, 0),
                SelectionBackColor = Color.FromArgb(219, 234, 254),
                SelectionForeColor = Color.FromArgb(17, 24, 39)
            };
            dgvApplications.CellFormatting += (s, e) =>
            {
                if (e.ColumnIndex >= 0 && dgvApplications.Columns[e.ColumnIndex].Name == "Status" && e.Value != null)
                {
                    string status = e.Value.ToString() ?? "";
                    if (status == "Approved") { e.CellStyle.ForeColor = Color.FromArgb(5, 122, 85); e.CellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold); }
                    else if (status == "Rejected") { e.CellStyle.ForeColor = Color.FromArgb(224, 36, 36); e.CellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold); }
                    else { e.CellStyle.ForeColor = Color.FromArgb(180, 83, 9); e.CellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold); }
                }
            };

            pnlHistory.Controls.Add(dgvApplications);
            pnlHistory.Controls.Add(pnlHistHdr);

            pnlApplicationTab.Controls.Add(pnlStatusCard);
            pnlApplicationTab.Controls.Add(pnlApplyCard);
            pnlApplicationTab.Controls.Add(pnlHistory);

            pnlContent.Controls.Add(pnlProfileTab);
            pnlContent.Controls.Add(pnlApplicationTab);

            this.Controls.Add(pnlContent);
            this.Controls.Add(pnlTabBar);
            this.Controls.Add(pnlBanner);
            this.Controls.Add(pnlHeader);

            SwitchTab("profile");
        }

        private void SwitchTab(string tab)
        {
            if (tab == "profile")
            {
                pnlProfileTab.Visible = true;
                pnlApplicationTab.Visible = false;
                btnTabProfile.Tag = "active";
                btnTabApplication.Tag = "";
                btnTabProfile.ForeColor = Color.FromArgb(26, 86, 219);
                btnTabProfile.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                btnTabApplication.ForeColor = Color.FromArgb(107, 114, 128);
                btnTabApplication.Font = new Font("Segoe UI", 10);
                btnTabProfile.Invalidate();
                btnTabApplication.Invalidate();
            }
            else
            {
                pnlProfileTab.Visible = false;
                pnlApplicationTab.Visible = true;
                btnTabApplication.Tag = "active";
                btnTabProfile.Tag = "";
                btnTabApplication.ForeColor = Color.FromArgb(26, 86, 219);
                btnTabApplication.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                btnTabProfile.ForeColor = Color.FromArgb(107, 114, 128);
                btnTabProfile.Font = new Font("Segoe UI", 10);
                btnTabProfile.Invalidate();
                btnTabApplication.Invalidate();
                LoadAllocationStatus();
                LoadApplicationHistory();
            }
        }

        // ── Helper Methods ───────────────────────────────────────
        private Label MakeLabel(string text, int x, int y)
        {
            return new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 65, 85),
                AutoSize = true,
                Location = new Point(x, y)
            };
        }

        private TextBox MakeTextBox(int x, int y, int width)
        {
            return new TextBox
            {
                Font = new Font("Segoe UI", 10),
                Location = new Point(x, y),
                Size = new Size(width, 32),
                BorderStyle = BorderStyle.FixedSingle
            };
        }

        private ComboBox MakeComboBox(int x, int y, int width)
        {
            return new ComboBox
            {
                Font = new Font("Segoe UI", 10),
                Location = new Point(x, y),
                Size = new Size(width, 32),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
        }

        private void StudentDashboard_Load(object sender, EventArgs e)
        {
            LoadStudentProfile();
            LoadHostels();
        }

        private void LoadStudentProfile()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM Student WHERE StudentID=@id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", studentID);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        txtFirstName.Text = reader["FirstName"]?.ToString() ?? "";
                        txtLastName.Text = reader["LastName"]?.ToString() ?? "";
                        txtMatric.Text = reader["MatricNumber"]?.ToString() ?? "";
                        cmbGender.Text = reader["Gender"]?.ToString() ?? "";
                        txtDepartment.Text = reader["Department"]?.ToString() ?? "";
                        cmbLevel.Text = reader["Level"]?.ToString() ?? "";
                        txtPhone.Text = reader["PhoneNumber"]?.ToString() ?? "";
                        txtEmail.Text = reader["Email"]?.ToString() ?? "";
                        try { cmbSession.Text = reader["Session"]?.ToString() ?? ""; } catch { }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error loading profile: " + ex.Message); }
        }

        private void LoadHostels()
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

        private void LoadAllocationStatus()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT a.Status, h.HostelName, r.RoomNumber, r.RoomType, a.AllocationDate
                                    FROM Allocation a
                                    JOIN Rooms r ON a.RoomID = r.RoomID
                                    JOIN Hostel h ON r.HostelID = h.HostelID
                                    WHERE a.StudentID = @id
                                    ORDER BY a.AllocationDate DESC LIMIT 1";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", studentID);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        string status = reader["Status"].ToString() ?? "";
                        lblAllocationStatus.Text = status;
                        lblHostelName.Text = reader["HostelName"]?.ToString() ?? "—";
                        lblRoomNumber.Text = reader["RoomNumber"]?.ToString() ?? "—";
                        lblRoomType.Text = reader["RoomType"]?.ToString() ?? "—";
                        lblDateApplied.Text = Convert.ToDateTime(reader["AllocationDate"]).ToString("dd MMM yyyy");

                        if (status == "Approved") lblAllocationStatus.BackColor = Color.FromArgb(5, 122, 85);
                        else if (status == "Rejected") lblAllocationStatus.BackColor = Color.FromArgb(224, 36, 36);
                        else lblAllocationStatus.BackColor = Color.FromArgb(180, 83, 9);
                    }
                    else
                    {
                        lblAllocationStatus.Text = "No Application";
                        lblAllocationStatus.BackColor = Color.FromArgb(107, 114, 128);
                        lblHostelName.Text = "—";
                        lblRoomNumber.Text = "—";
                        lblRoomType.Text = "—";
                        lblDateApplied.Text = "—";
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error loading status: " + ex.Message); }
        }

        private void LoadApplicationHistory()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT h.HostelName AS 'Hostel', r.RoomNumber AS 'Room No',
                                    r.RoomType AS 'Room Type', a.AllocationDate AS 'Date Applied',
                                    a.Status AS 'Status'
                                    FROM Allocation a
                                    JOIN Rooms r ON a.RoomID = r.RoomID
                                    JOIN Hostel h ON r.HostelID = h.HostelID
                                    WHERE a.StudentID = @id
                                    ORDER BY a.AllocationDate DESC";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", studentID);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvApplications.DataSource = dt;
                }
            }
            catch (Exception ex) { MessageBox.Show("Error loading history: " + ex.Message); }
        }

        private void BtnSaveProfile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFirstName.Text) || string.IsNullOrEmpty(txtLastName.Text))
            {
                MessageBox.Show("First Name and Last Name are required.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"UPDATE Student SET 
                                    FirstName=@first, LastName=@last,
                                    Gender=@gender, Department=@dept,
                                    Level=@level, PhoneNumber=@phone,
                                    Email=@email, Session=@session
                                    WHERE StudentID=@id";

                    if (!string.IsNullOrEmpty(txtPassword.Text))
                        query = @"UPDATE Student SET 
                                    FirstName=@first, LastName=@last,
                                    Gender=@gender, Department=@dept,
                                    Level=@level, PhoneNumber=@phone,
                                    Email=@email, Session=@session,
                                    Password=@password
                                    WHERE StudentID=@id";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@first", txtFirstName.Text);
                    cmd.Parameters.AddWithValue("@last", txtLastName.Text);
                    cmd.Parameters.AddWithValue("@gender", cmbGender.Text);
                    cmd.Parameters.AddWithValue("@dept", txtDepartment.Text);
                    cmd.Parameters.AddWithValue("@level", cmbLevel.Text);
                    cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@session", cmbSession.Text);
                    cmd.Parameters.AddWithValue("@id", studentID);

                    if (!string.IsNullOrEmpty(txtPassword.Text))
                        cmd.Parameters.AddWithValue("@password", txtPassword.Text);

                    cmd.ExecuteNonQuery();

                    // Update welcome label
                    studentName = txtFirstName.Text + " " + txtLastName.Text;
                    lblWelcome.Text = "Welcome, " + studentName + "!";
                    txtPassword.Text = "";

                    MessageBox.Show("Profile saved successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex) { MessageBox.Show("Error saving profile: " + ex.Message); }
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            if (cmbHostel.SelectedIndex == -1 || string.IsNullOrEmpty(cmbRoomType.Text))
            {
                MessageBox.Show("Please select a hostel and room type.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string checkQuery = "SELECT COUNT(*) FROM Allocation WHERE StudentID=@id AND (Status='Pending' OR Status='Approved')";
                    MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@id", studentID);
                    int existing = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (existing > 0)
                    {
                        MessageBox.Show("You already have an active application.\nPlease wait for it to be processed.", "Warning",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string roomQuery = "SELECT RoomID FROM Rooms WHERE HostelID=@hostelID AND RoomType=@roomType AND Status='Available' LIMIT 1";
                    MySqlCommand roomCmd = new MySqlCommand(roomQuery, conn);
                    roomCmd.Parameters.AddWithValue("@hostelID", cmbHostel.SelectedValue);
                    roomCmd.Parameters.AddWithValue("@roomType", cmbRoomType.Text);
                    object roomResult = roomCmd.ExecuteScalar();

                    if (roomResult == null)
                    {
                        MessageBox.Show("No available rooms found for the selected hostel and room type.", "No Rooms Available",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    int roomID = Convert.ToInt32(roomResult);
                    string insertQuery = "INSERT INTO Allocation (StudentID, RoomID, AllocationDate, Status) VALUES (@studentID, @roomID, @date, 'Pending')";
                    MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn);
                    insertCmd.Parameters.AddWithValue("@studentID", studentID);
                    insertCmd.Parameters.AddWithValue("@roomID", roomID);
                    insertCmd.Parameters.AddWithValue("@date", DateTime.Now.Date);
                    insertCmd.ExecuteNonQuery();

                    MessageBox.Show("Application submitted successfully!\nPlease wait for admin approval.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    cmbHostel.SelectedIndex = -1;
                    cmbRoomType.SelectedIndex = -1;
                    LoadAllocationStatus();
                    LoadApplicationHistory();
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to logout?", "Logout",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
                this.Close();
            }
        }
    }
}