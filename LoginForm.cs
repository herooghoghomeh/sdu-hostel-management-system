using System;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HostelManagementSystem
{
    public partial class LoginForm : Form
    {
        string connectionString = "Server=localhost;Database=hostel_db;Uid=root;Pwd=MilesHero010.;";

        Panel? pnlHeader, pnlFooter, pnlCard;
        Label? lblTitle, lblSubtitle, lblAppName, lblUniversity, lblDemo, lblUsernameTitle;
        TextBox? txtUsername, txtPassword;
        Button? btnStudent, btnAdministrator, btnSignIn, btnTogglePassword;
        PictureBox? picLogo;
        string selectedRole = "Student";
        bool passwordVisible = false;

        public LoginForm()
        {
            InitializeComponent();
            this.Text = "SDU Hostel Management System";
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.FromArgb(241, 245, 249);
            this.MinimumSize = new Size(800, 600);
            BuildUI();
            this.Resize += (s, e) => CenterCard();
        }

        private void BuildUI()
        {
            // ── HEADER ──────────────────────────────────────────
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 110,
                BackColor = Color.FromArgb(26, 86, 219)
            };

            picLogo = new PictureBox
            {
                Size = new Size(55, 55),
                Location = new Point(30, 27),
                BackColor = Color.Transparent,
                BorderStyle = BorderStyle.None
            };

            lblAppName = new Label
            {
                Text = "SDU Campus Hostel Management System",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(100, 25),
                BackColor = Color.Transparent
            };

            lblUniversity = new Label
            {
                Text = "Southern Delta University",
                ForeColor = Color.FromArgb(200, 220, 255),
                Font = new Font("Segoe UI", 10),
                AutoSize = true,
                Location = new Point(102, 62),
                BackColor = Color.Transparent
            };

            pnlHeader.Controls.Add(picLogo);
            pnlHeader.Controls.Add(lblAppName);
            pnlHeader.Controls.Add(lblUniversity);

            // ── FOOTER ──────────────────────────────────────────
            pnlFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 55,
                BackColor = Color.FromArgb(30, 41, 59)
            };

            Label lblFooterLeft = new Label
            {
                Text = "© 2026 Southern Delta University. All rights reserved.",
                ForeColor = Color.FromArgb(148, 163, 184),
                Font = new Font("Segoe UI", 9),
                AutoSize = true,
                Location = new Point(25, 18)
            };

            Label lblFooterRight = new Label
            {
                Text = "Campus Housing Management System v1.0",
                ForeColor = Color.FromArgb(148, 163, 184),
                Font = new Font("Segoe UI", 9),
                AutoSize = true,
                Location = new Point(750, 18)
            };

            pnlFooter.Controls.Add(lblFooterLeft);
            pnlFooter.Controls.Add(lblFooterRight);

            // ── CARD ────────────────────────────────────────────
            int cardWidth = 480;

            pnlCard = new Panel
            {
                Name = "pnlCard",
                Size = new Size(cardWidth, 760),
                BackColor = Color.White
            };

            // ── LOGO CIRCLE ──────────────────────────────────────
            int circleSize = 90;
            int circleX = (cardWidth - circleSize) / 2;

            Panel pnlLogoCircle = new Panel
            {
                Size = new Size(circleSize, circleSize),
                Location = new Point(circleX, 30),
                BackColor = Color.FromArgb(219, 234, 254)
            };
            pnlLogoCircle.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                e.Graphics.FillEllipse(
                    new SolidBrush(Color.FromArgb(219, 234, 254)),
                    0, 0, circleSize - 1, circleSize - 1);
            };

            PictureBox schoolLogo = new PictureBox
            {
                Size = new Size(circleSize, circleSize),
                Location = new Point(0, 0),
                BackColor = Color.Transparent,
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = Image.FromFile("logo.jpg")
            };
            pnlLogoCircle.Controls.Add(schoolLogo);

            // ── WELCOME BACK ─────────────────────────────────────
            lblTitle = new Label
            {
                Text = "Welcome Back",
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42),
                AutoSize = true,
                Location = new Point(115, 142)
            };

            // ── SUBTITLE ─────────────────────────────────────────
            lblSubtitle = new Label
            {
                Text = "Sign in to access your account",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(100, 116, 139),
                AutoSize = true,
                Location = new Point(130, 200)
            };

            // ── ROLE TOGGLE ──────────────────────────────────────
            Panel pnlToggle = new Panel
            {
                Size = new Size(340, 46),
                Location = new Point(70, 255),
                BackColor = Color.FromArgb(241, 245, 249)
            };

            btnStudent = new Button
            {
                Text = "Student",
                Size = new Size(168, 42),
                Location = new Point(1, 2),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(26, 86, 219),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnStudent.FlatAppearance.BorderSize = 0;
            btnStudent.Click += BtnStudent_Click;

            btnAdministrator = new Button
            {
                Text = "Administrator",
                Size = new Size(168, 42),
                Location = new Point(170, 2),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(241, 245, 249),
                ForeColor = Color.FromArgb(100, 116, 139),
                Font = new Font("Segoe UI", 10),
                Cursor = Cursors.Hand
            };
            btnAdministrator.FlatAppearance.BorderSize = 0;
            btnAdministrator.Click += BtnAdministrator_Click;

            pnlToggle.Controls.Add(btnStudent);
            pnlToggle.Controls.Add(btnAdministrator);

            // ── USERNAME / MATRIC LABEL ───────────────────────────
            lblUsernameTitle = new Label
            {
                Text = "Matriculation Number",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 65, 85),
                AutoSize = true,
                Location = new Point(70, 315)
            };

            Panel pnlUserBox = new Panel
            {
                Size = new Size(340, 50),
                Location = new Point(70, 342),
                BackColor = Color.White
            };
            pnlUserBox.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, pnlUserBox.ClientRectangle,
                    Color.FromArgb(203, 213, 225), ButtonBorderStyle.Solid);
            };

            Label lblUserIcon = new Label
            {
                Text = "👤",
                Font = new Font("Segoe UI", 12),
                Location = new Point(10, 10),
                Size = new Size(30, 30),
                BackColor = Color.Transparent
            };

            txtUsername = new TextBox
            {
                PlaceholderText = "Enter your matriculation number",
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.None,
                Location = new Point(45, 14),
                Size = new Size(283, 26),
                BackColor = Color.White
            };

            pnlUserBox.Controls.Add(lblUserIcon);
            pnlUserBox.Controls.Add(txtUsername);

            // ── PASSWORD ─────────────────────────────────────────
            Label lblPasswordTitle = new Label
            {
                Text = "Password",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 65, 85),
                AutoSize = true,
                Location = new Point(70, 418)
            };

            Panel pnlPassBox = new Panel
            {
                Size = new Size(340, 54),
                Location = new Point(70, 446),
                BackColor = Color.White
            };
            pnlPassBox.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, pnlPassBox.ClientRectangle,
                    Color.FromArgb(203, 213, 225), ButtonBorderStyle.Solid);
            };

            Label lblPassIcon = new Label
            {
                Text = "🔒",
                Font = new Font("Segoe UI", 12),
                Location = new Point(10, 12),
                Size = new Size(30, 30),
                BackColor = Color.Transparent
            };

            txtPassword = new TextBox
            {
                PlaceholderText = "Enter your password",
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.None,
                Location = new Point(45, 15),
                Size = new Size(240, 26),
                UseSystemPasswordChar = true,
                BackColor = Color.White
            };

            btnTogglePassword = new Button
            {
                Text = "👁",
                Font = new Font("Segoe UI", 13),
                Size = new Size(40, 40),
                Location = new Point(293, 7),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(100, 116, 139),
                Cursor = Cursors.Hand,
                TabStop = false
            };
            btnTogglePassword.FlatAppearance.BorderSize = 0;
            btnTogglePassword.Click += BtnTogglePassword_Click;

            pnlPassBox.Controls.Add(lblPassIcon);
            pnlPassBox.Controls.Add(txtPassword);
            pnlPassBox.Controls.Add(btnTogglePassword);

            // ── SIGN IN BUTTON ───────────────────────────────────
            btnSignIn = new Button
            {
                Text = "Sign In",
                Size = new Size(340, 52),
                Location = new Point(70, 528),
                BackColor = Color.FromArgb(26, 86, 219),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnSignIn.FlatAppearance.BorderSize = 0;
            btnSignIn.Click += BtnSignIn_Click;

            // ── REGISTER LINK (only for students) ────────────────
            Label lblRegisterLink = new Label
            {
                Text = "New student? Click here to register",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(26, 86, 219),
                AutoSize = true,
                Location = new Point(108, 596),
                Cursor = Cursors.Hand
            };
            lblRegisterLink.Click += (s, e) =>
            {
                RegisterForm registerForm = new RegisterForm();
                registerForm.Show();
                this.Hide();
            };

            // ── DEMO TEXT ────────────────────────────────────────
            lblDemo = new Label
            {
                Text = "Admin login: Use your username and password",
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.FromArgb(148, 163, 184),
                Location = new Point(70, 630),
                AutoSize = false,
                Size = new Size(340, 20)
            };

            // Add all to card
            pnlCard.Controls.Add(pnlLogoCircle);
            pnlCard.Controls.Add(lblTitle);
            pnlCard.Controls.Add(lblSubtitle);
            pnlCard.Controls.Add(pnlToggle);
            pnlCard.Controls.Add(lblUsernameTitle);
            pnlCard.Controls.Add(pnlUserBox);
            pnlCard.Controls.Add(lblPasswordTitle);
            pnlCard.Controls.Add(pnlPassBox);
            pnlCard.Controls.Add(btnSignIn);
            pnlCard.Controls.Add(lblRegisterLink);
            pnlCard.Controls.Add(lblDemo);

            // Add to form
            this.Controls.Add(pnlHeader);
            this.Controls.Add(pnlFooter);
            this.Controls.Add(pnlCard);

            CenterCard();
        }

        private void CenterCard()
        {
            int headerHeight = pnlHeader?.Height ?? 110;
            int footerHeight = pnlFooter?.Height ?? 55;
            int availableHeight = this.ClientSize.Height - headerHeight - footerHeight;
            if (pnlCard != null)
            {
                pnlCard.Left = (this.ClientSize.Width - pnlCard.Width) / 2;
                pnlCard.Top = headerHeight + (availableHeight - pnlCard.Height) / 2;
            }
        }

        private void BtnTogglePassword_Click(object sender, EventArgs e)
        {
            passwordVisible = !passwordVisible;
            if (txtPassword != null)
                txtPassword.UseSystemPasswordChar = !passwordVisible;
            if (btnTogglePassword != null)
                btnTogglePassword.Text = passwordVisible ? "🙈" : "👁";
        }

        private void BtnStudent_Click(object sender, EventArgs e)
        {
            selectedRole = "Student";
            if (lblUsernameTitle != null)
                lblUsernameTitle.Text = "Matriculation Number";
            if (txtUsername != null)
            {
                txtUsername.PlaceholderText = "Enter your matriculation number";
                txtUsername.Text = "";
            }
            if (txtPassword != null)
                txtPassword.Text = "";
            if (btnStudent != null)
            {
                btnStudent.BackColor = Color.FromArgb(26, 86, 219);
                btnStudent.ForeColor = Color.White;
                btnStudent.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            }
            if (btnAdministrator != null)
            {
                btnAdministrator.BackColor = Color.FromArgb(241, 245, 249);
                btnAdministrator.ForeColor = Color.FromArgb(100, 116, 139);
                btnAdministrator.Font = new Font("Segoe UI", 10);
            }
        }

        private void BtnAdministrator_Click(object sender, EventArgs e)
        {
            selectedRole = "Administrator";
            if (lblUsernameTitle != null)
                lblUsernameTitle.Text = "Username";
            if (txtUsername != null)
            {
                txtUsername.PlaceholderText = "Enter your username";
                txtUsername.Text = "";
            }
            if (txtPassword != null)
                txtPassword.Text = "";
            if (btnAdministrator != null)
            {
                btnAdministrator.BackColor = Color.FromArgb(26, 86, 219);
                btnAdministrator.ForeColor = Color.White;
                btnAdministrator.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            }
            if (btnStudent != null)
            {
                btnStudent.BackColor = Color.FromArgb(241, 245, 249);
                btnStudent.ForeColor = Color.FromArgb(100, 116, 139);
                btnStudent.Font = new Font("Segoe UI", 10);
            }
        }

        private void BtnSignIn_Click(object sender, EventArgs e)
        {
            string username = txtUsername?.Text.Trim() ?? "";
            string password = txtPassword?.Text.Trim() ?? "";

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter your credentials.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (selectedRole == "Administrator")
                LoginAsAdmin(username, password);
            else
                LoginAsStudent(username, password);
        }

        private void LoginAsAdmin(string username, string password)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM Administrator WHERE Username=@user AND Password=@pass";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@user", username);
                    cmd.Parameters.AddWithValue("@pass", password);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        AdminDashboard adminForm = new AdminDashboard();
                        adminForm.WindowState = FormWindowState.Maximized;
                        adminForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password.", "Login Failed",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoginAsStudent(string username, string password)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM Student WHERE MatricNumber=@user AND Password=@pass";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@user", username);
                    cmd.Parameters.AddWithValue("@pass", password);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        int id = Convert.ToInt32(reader["StudentID"]);
                        string name = reader["FirstName"].ToString() + " " +
                                      reader["LastName"].ToString();
                        reader.Close();

                        StudentDashboard studentForm = new StudentDashboard(id, name);
                        studentForm.WindowState = FormWindowState.Maximized;
                        studentForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Invalid matriculation number or password.", "Login Failed",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}