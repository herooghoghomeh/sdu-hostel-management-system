using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HostelManagementSystem
{
    public partial class AdminDashboard : Form
    {
        string connectionString = "Server=localhost;Database=hostel_db;Uid=root;Pwd=MilesHero010.;";

        Label lblTotalHostels = new Label();
        Label lblTotalRooms = new Label();
        Label lblAllocated = new Label();
        Label lblPending = new Label();
        Panel pnlRecentList = new Panel();
        Panel pnlNotificationList = new Panel();

        public AdminDashboard()
        {
            InitializeComponent();
            this.Text = "Admin Dashboard";
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.Load += AdminDashboard_Load;
            this.Activated += (s, e) => { LoadStats(); LoadRecentAllocations(); };
            BuildUI();
        }

        private void AdminDashboard_Load(object sender, EventArgs e)
        {
            LoadStats();
            LoadRecentAllocations();
        }

        private void BuildUI()
        {
            this.Controls.Clear();

            // ── TOP HEADER BAR ───────────────────────────────────
            Panel pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 65,
                BackColor = Color.FromArgb(26, 86, 219)
            };

            Label lblLogo = new Label
            {
                Text = "🏛",
                Font = new Font("Segoe UI", 18),
                ForeColor = Color.White,
                Location = new Point(18, 12),
                Size = new Size(40, 40),
                BackColor = Color.Transparent
            };

            Label lblAppName = new Label
            {
                Text = "SDU Campus Hostel Management System",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(62, 12),
                BackColor = Color.Transparent
            };

            Label lblUniversity = new Label
            {
                Text = "Southern Delta University",
                ForeColor = Color.FromArgb(200, 220, 255),
                Font = new Font("Segoe UI", 8),
                AutoSize = true,
                Location = new Point(63, 42),
                BackColor = Color.Transparent
            };

            Button btnLogout = new Button
            {
                Text = "⬡ Logout",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(200, 30, 30),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(100, 34),
                Location = new Point(1800, 15),
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.Click += BtnLogout_Click;

            pnlHeader.Controls.Add(lblLogo);
            pnlHeader.Controls.Add(lblAppName);
            pnlHeader.Controls.Add(lblUniversity);
            pnlHeader.Controls.Add(btnLogout);

            pnlHeader.Resize += (s, e) =>
            {
                btnLogout.Location = new Point(pnlHeader.Width - 120, 15);
            };

            // ── NAV BAR ──────────────────────────────────────────
            Panel pnlNav = new Panel
            {
                Dock = DockStyle.Top,
                Height = 55,
                BackColor = Color.White
            };

            pnlNav.Paint += (s, e) =>
            {
                e.Graphics.DrawLine(new Pen(Color.FromArgb(229, 231, 235), 1),
                    0, pnlNav.Height - 1, pnlNav.Width, pnlNav.Height - 1);
            };

            string[] navItems = { "🏠  Hostels", "🏢  Rooms", "📂  Allocations", "📋  Reports"};
            string[] navActions = { "hostels", "rooms", "allocations", "reports"};
            int navX = 20;

            for (int i = 0; i < navItems.Length; i++)
            {
                string action = navActions[i];
                Button navBtn = new Button
                {
                    Text = navItems[i],
                    Font = new Font("Segoe UI", 9),
                    ForeColor = Color.FromArgb(55, 65, 81),
                    BackColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Size = new Size(155, 40),
                    Location = new Point(navX, 7),
                    Cursor = Cursors.Hand,
                    Tag = action
                };
                navBtn.FlatAppearance.BorderSize = 0;
                navBtn.Click += NavBtn_Click;
                pnlNav.Controls.Add(navBtn);
                navX += 145;
            }

            // ── MAIN SCROLL PANEL ────────────────────────────────
            Panel pnlMain = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(245, 247, 250),
                AutoScroll = true,
                Padding = new Padding(25, 20, 25, 20)
            };

            // ── DASHBOARD HERO BANNER ────────────────────────────
            Panel pnlBanner = new Panel
            {
                Size = new Size(1200, 100),
                Location = new Point(25, 20),
                BackColor = Color.FromArgb(26, 86, 219),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            Label lblDashTitle = new Label
            {
                Text = "Administrator Dashboard",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(25, 18),
                BackColor = Color.Transparent
            };

            Label lblDashSubtitle = new Label
            {
                Text = "Manage housing units, rooms, and student allocations",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(200, 220, 255),
                AutoSize = true,
                Location = new Point(25, 58),
                BackColor = Color.Transparent
            };

            pnlBanner.Controls.Add(lblDashTitle);
            pnlBanner.Controls.Add(lblDashSubtitle);

            pnlMain.Resize += (s, e) =>
            {
                pnlBanner.Width = pnlMain.Width - 65;
            };

            // ── STAT CARDS ───────────────────────────────────────
            string[] statTitles = { "Total Housing Units", "Total Rooms", "Allocated Students", "Pending Applications" };
            string[] statIcons = { "🏠", "🏢", "👥", "📋" };
            Color[] iconBg = {
                Color.FromArgb(219, 234, 254),
                Color.FromArgb(209, 250, 229),
                Color.FromArgb(237, 233, 254),
                Color.FromArgb(255, 237, 213)
            };
            Label[] statLabels = { lblTotalHostels, lblTotalRooms, lblAllocated, lblPending };

            Panel pnlCards = new Panel
            {
                Location = new Point(25, 130),
                Size = new Size(1200, 110),
                BackColor = Color.Transparent,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            pnlMain.Resize += (s, e) =>
            {
                pnlCards.Width = pnlMain.Width - 65;
                int cardW = (pnlCards.Width - 60) / 4;
                for (int i = 0; i < pnlCards.Controls.Count; i++)
                {
                    pnlCards.Controls[i].Width = cardW;
                    pnlCards.Controls[i].Location = new Point(i * (cardW + 20), 0);
                }
            };

            Color[] iconFg = {
                Color.FromArgb(26, 86, 219),
                Color.FromArgb(5, 122, 85),
                Color.FromArgb(109, 40, 217),
                Color.FromArgb(180, 83, 9)
            };

            for (int i = 0; i < 4; i++)
            {
                int cardW = 270;
                Panel card = new Panel
                {
                    Size = new Size(cardW, 105),
                    Location = new Point(i * (cardW + 20), 0),
                    BackColor = Color.White
                };

                card.Paint += (s, e) =>
                {
                    ControlPaint.DrawBorder(e.Graphics, card.ClientRectangle,
                        Color.FromArgb(229, 231, 235), ButtonBorderStyle.Solid);
                };

                Label lblStatTitle = new Label
                {
                    Text = statTitles[i],
                    Font = new Font("Segoe UI", 9),
                    ForeColor = Color.FromArgb(107, 114, 128),
                    AutoSize = true,
                    Location = new Point(18, 18)
                };

                statLabels[i].Text = "0";
                statLabels[i].Font = new Font("Segoe UI", 22, FontStyle.Bold);
                statLabels[i].ForeColor = iconFg[i];
                statLabels[i].AutoSize = true;
                statLabels[i].Location = new Point(18, 38);

                Panel iconCircle = new Panel
                {
                    Size = new Size(44, 44),
                    Location = new Point(cardW - 62, 30),
                    BackColor = iconBg[i]
                };
                iconCircle.Paint += (s, e) =>
                {
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    e.Graphics.FillEllipse(new SolidBrush(iconCircle.BackColor),
                        0, 0, iconCircle.Width - 1, iconCircle.Height - 1);
                };

                Label iconLbl = new Label
                {
                    Text = statIcons[i],
                    Font = new Font("Segoe UI", 16),
                    Size = new Size(44, 44),
                    Location = new Point(0, 0),
                    BackColor = Color.Transparent,
                    TextAlign = ContentAlignment.MiddleCenter,
                    AutoSize = false
                };
                iconCircle.Controls.Add(iconLbl);

                Label lblTrend = new Label
                {
                    Text = "↑ +12% from last month",
                    Font = new Font("Segoe UI", 8),
                    ForeColor = Color.FromArgb(5, 122, 85),
                    AutoSize = true,
                    Location = new Point(18, 78)
                };

                card.Controls.Add(lblStatTitle);
                card.Controls.Add(statLabels[i]);
                card.Controls.Add(iconCircle);
                card.Controls.Add(lblTrend);
                pnlCards.Controls.Add(card);
            }

            // ── BOTTOM: Recent Allocations + Notifications ───────
            Panel pnlBottom = new Panel
            {
                Location = new Point(25, 260),
                Size = new Size(1200, 400),
                BackColor = Color.Transparent,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            pnlMain.Resize += (s, e) =>
            {
                pnlBottom.Width = pnlMain.Width - 65;
                int half = (pnlBottom.Width - 20) / 2;
                if (pnlBottom.Controls.Count >= 2)
                {
                    pnlBottom.Controls[0].Width = half;
                    pnlBottom.Controls[1].Width = half;
                    pnlBottom.Controls[1].Location = new Point(half + 20, 0);
                }
            };

            // Recent Allocations
            Panel pnlRecent = new Panel
            {
                Size = new Size(590, 380),
                Location = new Point(0, 0),
                BackColor = Color.White
            };
            pnlRecent.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, pnlRecent.ClientRectangle,
                    Color.FromArgb(229, 231, 235), ButtonBorderStyle.Solid);
            };

            Label lblRecentTitle = new Label
            {
                Text = "Recent Allocations",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(17, 24, 39),
                AutoSize = true,
                Location = new Point(18, 18)
            };

            pnlRecentList = new Panel
            {
                Location = new Point(0, 55),
                Size = new Size(590, 320),
                AutoScroll = true,
                BackColor = Color.White
            };

            pnlRecent.Controls.Add(lblRecentTitle);
            pnlRecent.Controls.Add(pnlRecentList);

            // Notifications
            Panel pnlNotif = new Panel
            {
                Size = new Size(590, 380),
                Location = new Point(610, 0),
                BackColor = Color.White
            };
            pnlNotif.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, pnlNotif.ClientRectangle,
                    Color.FromArgb(229, 231, 235), ButtonBorderStyle.Solid);
            };

            Label lblNotifIcon = new Label
            {
                Text = "🔔  Notifications",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(17, 24, 39),
                AutoSize = true,
                Location = new Point(18, 18)
            };

            pnlNotificationList = new Panel
            {
                Location = new Point(0, 55),
                Size = new Size(590, 320),
                AutoScroll = true,
                BackColor = Color.White
            };

            string[] notifTexts = {
                "North Campus Hostel Room 205 maintenance required",
                "15 new applications received today",
                "Payment verified for 8 students"
            };
            Color[] notifColors = {
                Color.FromArgb(255, 243, 205),
                Color.FromArgb(219, 234, 254),
                Color.FromArgb(209, 250, 229)
            };
            Color[] barColors = {
                Color.FromArgb(217, 119, 6),
                Color.FromArgb(26, 86, 219),
                Color.FromArgb(5, 122, 85)
            };
            string[] notifTimes = { "1 hour ago", "3 hours ago", "5 hours ago" };

            int ny = 10;
            for (int i = 0; i < notifTexts.Length; i++)
            {
                Panel notifItem = new Panel
                {
                    Size = new Size(555, 65),
                    Location = new Point(10, ny),
                    BackColor = notifColors[i]
                };

                Panel bar = new Panel
                {
                    Size = new Size(4, 65),
                    Location = new Point(0, 0),
                    BackColor = barColors[i]
                };

                Label notifText = new Label
                {
                    Text = notifTexts[i],
                    Font = new Font("Segoe UI", 9),
                    ForeColor = Color.FromArgb(31, 41, 55),
                    Location = new Point(14, 10),
                    Size = new Size(530, 30),
                    AutoSize = false
                };

                Label notifTime = new Label
                {
                    Text = notifTimes[i],
                    Font = new Font("Segoe UI", 8),
                    ForeColor = Color.FromArgb(107, 114, 128),
                    AutoSize = true,
                    Location = new Point(14, 42)
                };

                notifItem.Controls.Add(bar);
                notifItem.Controls.Add(notifText);
                notifItem.Controls.Add(notifTime);
                pnlNotificationList.Controls.Add(notifItem);
                ny += 75;
            }

            pnlNotif.Controls.Add(lblNotifIcon);
            pnlNotif.Controls.Add(pnlNotificationList);

            pnlBottom.Controls.Add(pnlRecent);
            pnlBottom.Controls.Add(pnlNotif);

            pnlMain.Controls.Add(pnlBanner);
            pnlMain.Controls.Add(pnlCards);
            pnlMain.Controls.Add(pnlBottom);

            this.Controls.Add(pnlMain);
            this.Controls.Add(pnlNav);
            this.Controls.Add(pnlHeader);
        }

        private void LoadStats()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd1 = new MySqlCommand("SELECT COUNT(*) FROM Hostel", conn);
                    lblTotalHostels.Text = cmd1.ExecuteScalar().ToString();

                    MySqlCommand cmd2 = new MySqlCommand("SELECT COUNT(*) FROM Rooms", conn);
                    lblTotalRooms.Text = cmd2.ExecuteScalar().ToString();

                    MySqlCommand cmd3 = new MySqlCommand("SELECT COUNT(*) FROM Allocation WHERE Status='Approved'", conn);
                    lblAllocated.Text = cmd3.ExecuteScalar().ToString();

                    MySqlCommand cmd4 = new MySqlCommand("SELECT COUNT(*) FROM Allocation WHERE Status='Pending'", conn);
                    lblPending.Text = cmd4.ExecuteScalar().ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading stats: " + ex.Message);
            }
        }

        private void LoadRecentAllocations()
        {
            try
            {
                pnlRecentList.Controls.Clear();
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT CONCAT(s.FirstName,' ',s.LastName) AS StudentName,
                                    s.MatricNumber, h.HostelName, r.RoomNumber
                                    FROM Allocation a
                                    JOIN Student s ON a.StudentID = s.StudentID
                                    JOIN Rooms r ON a.RoomID = r.RoomID
                                    JOIN Hostel h ON r.HostelID = h.HostelID
                                    ORDER BY a.AllocationDate DESC LIMIT 5";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    int ry = 10;
                    foreach (DataRow row in dt.Rows)
                    {
                        Panel item = new Panel
                        {
                            Size = new Size(560, 65),
                            Location = new Point(10, ry),
                            BackColor = Color.White
                        };

                        item.Paint += (s, e) =>
                        {
                            e.Graphics.DrawLine(new Pen(Color.FromArgb(243, 244, 246), 1),
                                0, item.Height - 1, item.Width, item.Height - 1);
                        };

                        Panel avatar = new Panel
                        {
                            Size = new Size(38, 38),
                            Location = new Point(10, 13),
                            BackColor = Color.FromArgb(219, 234, 254)
                        };
                        avatar.Paint += (s, e) =>
                        {
                            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                            e.Graphics.FillEllipse(new SolidBrush(Color.FromArgb(219, 234, 254)),
                                0, 0, 37, 37);
                        };

                        Label avatarIcon = new Label
                        {
                            Text = "👤",
                            Font = new Font("Segoe UI", 14),
                            Size = new Size(38, 38),
                            Location = new Point(0, 0),
                            BackColor = Color.Transparent,
                            TextAlign = ContentAlignment.MiddleCenter,
                            AutoSize = false
                        };
                        avatar.Controls.Add(avatarIcon);

                        Label lblName = new Label
                        {
                            Text = row["StudentName"].ToString(),
                            Font = new Font("Segoe UI", 9, FontStyle.Bold),
                            ForeColor = Color.FromArgb(17, 24, 39),
                            AutoSize = true,
                            Location = new Point(58, 12)
                        };

                        Label lblMatric = new Label
                        {
                            Text = row["MatricNumber"].ToString(),
                            Font = new Font("Segoe UI", 8),
                            ForeColor = Color.FromArgb(107, 114, 128),
                            AutoSize = true,
                            Location = new Point(58, 32)
                        };

                        Label lblHostel = new Label
                        {
                            Text = row["HostelName"].ToString(),
                            Font = new Font("Segoe UI", 9, FontStyle.Bold),
                            ForeColor = Color.FromArgb(17, 24, 39),
                            AutoSize = true,
                            Location = new Point(260, 12)
                        };

                        Label lblRoom = new Label
                        {
                            Text = "Room " + row["RoomNumber"].ToString(),
                            Font = new Font("Segoe UI", 8),
                            ForeColor = Color.FromArgb(107, 114, 128),
                            AutoSize = true,
                            Location = new Point(260, 32)
                        };

                        item.Controls.Add(avatar);
                        item.Controls.Add(lblName);
                        item.Controls.Add(lblMatric);
                        item.Controls.Add(lblHostel);
                        item.Controls.Add(lblRoom);
                        pnlRecentList.Controls.Add(item);
                        ry += 70;
                    }

                    if (dt.Rows.Count == 0)
                    {
                        Label lblNoData = new Label
                        {
                            Text = "No allocations yet.",
                            Font = new Font("Segoe UI", 9),
                            ForeColor = Color.FromArgb(107, 114, 128),
                            AutoSize = true,
                            Location = new Point(20, 20)
                        };
                        pnlRecentList.Controls.Add(lblNoData);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading recent allocations: " + ex.Message);
            }
        }

        private void NavBtn_Click(object sender, EventArgs e)
        {
            Button? btn = sender as Button;
            string action = btn?.Tag?.ToString() ?? "";

            switch (action)
            {
                case "hostels":
                    ManageHostels hostelsForm = new ManageHostels();
                    hostelsForm.WindowState = FormWindowState.Maximized;
                    hostelsForm.Show();
                    break;
                case "rooms":
                    ManageRooms roomsForm = new ManageRooms();
                    roomsForm.WindowState = FormWindowState.Maximized;
                    roomsForm.Show();
                    break;
                case "allocations":
                    ManageAllocations allocForm = new ManageAllocations();
                    allocForm.WindowState = FormWindowState.Maximized;
                    allocForm.Show();
                    break;
                case "reports":
                    Reports reportsForm = new Reports();
                    reportsForm.WindowState = FormWindowState.Maximized;
                    reportsForm.Show();
                    break;
            }
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?",
                "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
                this.Close();
            }
        }
    }
}
