using System;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HostelManagementSystem
{
    public partial class RegisterForm : Form
    {
        string connectionString = "Server=localhost;Database=hostel_db;Uid=root;Pwd=MilesHero010.;";

        TextBox txtFirstName = new TextBox();
        TextBox txtLastName = new TextBox();
        TextBox txtMatric = new TextBox();
        TextBox txtDepartment = new TextBox();
        TextBox txtPhone = new TextBox();
        TextBox txtEmail = new TextBox();
        TextBox txtPassword = new TextBox();
        TextBox txtConfirmPassword = new TextBox();
        ComboBox cmbGender = new ComboBox();
        ComboBox cmbLevel = new ComboBox();
        ComboBox cmbSession = new ComboBox();
        Button btnTogglePass = new Button();
        Button btnToggleConfirm = new Button();
        bool passVisible = false;
        bool confirmVisible = false;

        public RegisterForm()
        {
            InitializeComponent();
            this.Text = "SDU Hostel Management System - Student Registration";
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.FromArgb(241, 245, 249);
            this.MinimumSize = new Size(800, 600);
            BuildUI();
            this.Resize += (s, e) => CenterCard();
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

            Label lblAppName = new Label
            {
                Text = "SDU Campus Hostel Management System",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(25, 18),
                BackColor = Color.Transparent
            };

            Label lblUniversity = new Label
            {
                Text = "Southern Delta University",
                ForeColor = Color.FromArgb(200, 220, 255),
                Font = new Font("Segoe UI", 9),
                AutoSize = true,
                Location = new Point(27, 50),
                BackColor = Color.Transparent
            };

            pnlHeader.Controls.Add(lblAppName);
            pnlHeader.Controls.Add(lblUniversity);

            // ── FOOTER ───────────────────────────────────────────
            Panel pnlFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 45,
                BackColor = Color.FromArgb(30, 41, 59)
            };

            Label lblFooter = new Label
            {
                Text = "© 2026 Southern Delta University. All rights reserved.",
                ForeColor = Color.FromArgb(148, 163, 184),
                Font = new Font("Segoe UI", 8),
                AutoSize = true,
                Location = new Point(25, 14)
            };
            pnlFooter.Controls.Add(lblFooter);

            // ── CARD ─────────────────────────────────────────────
            Panel pnlCard = new Panel
            {
                Name = "pnlCard",
                Size = new Size(780, 650),
                BackColor = Color.White
            };
            pnlCard.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, pnlCard.ClientRectangle,
                    Color.FromArgb(229, 231, 235), ButtonBorderStyle.Solid);
            };

            Label lblTitle = new Label
            {
                Text = "Student Registration",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42),
                AutoSize = true,
                Location = new Point(30, 25)
            };

            Label lblSub = new Label
            {
                Text = "Create your account to apply for hostel accommodation",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(107, 114, 128),
                AutoSize = true,
                Location = new Point(30, 68)
            };

            // ── ROW 1: First Name + Last Name ─────────────────────
            Label lbl1 = MakeLbl("First Name: *", 30, 100);
            txtFirstName = MakeTxt(30, 125, 340);

            Label lbl2 = MakeLbl("Last Name: *", 410, 100);
            txtLastName = MakeTxt(410, 125, 340);

            // ── ROW 2: Matric + Gender ────────────────────────────
            Label lbl3 = MakeLbl("Matriculation Number: *", 30, 180);
            txtMatric = MakeTxt(30, 205, 340);

            Label lbl4 = MakeLbl("Gender: *", 410, 180);
            cmbGender = MakeCmb(410, 205, 340);
            cmbGender.Items.AddRange(new string[] { "Male", "Female" });

            // ── ROW 3: Department + Level ─────────────────────────
            Label lbl5 = MakeLbl("Department: *", 30, 260);
            txtDepartment = MakeTxt(30, 285, 340);

            Label lbl6 = MakeLbl("Level: *", 410, 260);
            cmbLevel = MakeCmb(410, 285, 340);
            cmbLevel.Items.AddRange(new string[] { "100", "200", "300", "400", "500" });

            // ── ROW 4: Session + Phone ────────────────────────────
            Label lbl7 = MakeLbl("Academic Session: *", 30, 340);
            cmbSession = MakeCmb(30, 365, 340);
            cmbSession.Items.AddRange(new string[] {
                "2020/2021", "2021/2022", "2022/2023",
                "2023/2024", "2024/2025", "2025/2026"
            });

            Label lbl8 = MakeLbl("Phone Number:", 410, 340);
            txtPhone = MakeTxt(410, 365, 340);

            // ── ROW 5: Password + Confirm ─────────────────────────
            Label lbl9 = MakeLbl("Password: *", 40, 420);
            Panel pnlPass = MakePassBox(30, 445, 340, ref txtPassword, ref btnTogglePass, false);
            btnTogglePass.Click += (s, e) =>
            {
                passVisible = !passVisible;
                txtPassword.UseSystemPasswordChar = !passVisible;
                btnTogglePass.Text = passVisible ? "🙈" : "👁";
            };

            Label lbl10 = MakeLbl("Confirm Password: *", 410, 420);
            Panel pnlConfirm = MakePassBox(410, 445, 340, ref txtConfirmPassword, ref btnToggleConfirm, false);
            btnToggleConfirm.Click += (s, e) =>
            {
                confirmVisible = !confirmVisible;
                txtConfirmPassword.UseSystemPasswordChar = !confirmVisible;
                btnToggleConfirm.Text = confirmVisible ? "🙈" : "👁";
            };

            // ── BUTTONS ───────────────────────────────────────────
            Button btnRegister = new Button
            {
                Text = "✔  Create Account",
                Size = new Size(340, 48),
                Location = new Point(30, 510),
                BackColor = Color.FromArgb(26, 86, 219),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnRegister.FlatAppearance.BorderSize = 0;
            btnRegister.Click += BtnRegister_Click;

            Button btnBack = new Button
            {
                Text = "← Back to Login",
                Size = new Size(340, 48),
                Location = new Point(410, 510),
                BackColor = Color.FromArgb(243, 244, 246),
                ForeColor = Color.FromArgb(55, 65, 81),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnBack.FlatAppearance.BorderSize = 1;
            btnBack.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
            btnBack.Click += (s, e) =>
            {
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
                this.Close();
            };

            Label lblRequired = new Label
            {
                Text = "* Required fields",
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.FromArgb(148, 163, 184),
                AutoSize = true,
                Location = new Point(30, 568)
            };

            pnlCard.Controls.Add(lblTitle);
            pnlCard.Controls.Add(lblSub);
            pnlCard.Controls.Add(lbl1); pnlCard.Controls.Add(txtFirstName);
            pnlCard.Controls.Add(lbl2); pnlCard.Controls.Add(txtLastName);
            pnlCard.Controls.Add(lbl3); pnlCard.Controls.Add(txtMatric);
            pnlCard.Controls.Add(lbl4); pnlCard.Controls.Add(cmbGender);
            pnlCard.Controls.Add(lbl5); pnlCard.Controls.Add(txtDepartment);
            pnlCard.Controls.Add(lbl6); pnlCard.Controls.Add(cmbLevel);
            pnlCard.Controls.Add(lbl7); pnlCard.Controls.Add(cmbSession);
            pnlCard.Controls.Add(lbl8); pnlCard.Controls.Add(txtPhone);
            pnlCard.Controls.Add(lbl9); pnlCard.Controls.Add(pnlPass);
            pnlCard.Controls.Add(lbl10); pnlCard.Controls.Add(pnlConfirm);
            pnlCard.Controls.Add(btnRegister);
            pnlCard.Controls.Add(btnBack);
            pnlCard.Controls.Add(lblRequired);

            this.Controls.Add(pnlCard);
            this.Controls.Add(pnlFooter);
            this.Controls.Add(pnlHeader);

            CenterCard();
        }

        private void CenterCard()
        {
            var card = this.Controls.Find("pnlCard", false);
            if (card.Length > 0)
            {
                int headerH = 80;
                int footerH = 45;
                int available = this.ClientSize.Height - headerH - footerH;
                card[0].Left = (this.ClientSize.Width - card[0].Width) / 2;
                card[0].Top = headerH + (available - card[0].Height) / 2;
            }
        }

        // ── Helper methods ────────────────────────────────────────
        private Label MakeLbl(string text, int x, int y)
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

        private TextBox MakeTxt(int x, int y, int width)
        {
            return new TextBox
            {
                Font = new Font("Segoe UI", 10),
                Location = new Point(x, y),
                Size = new Size(width, 32),
                BorderStyle = BorderStyle.FixedSingle
            };
        }

        private ComboBox MakeCmb(int x, int y, int width)
        {
            return new ComboBox
            {
                Font = new Font("Segoe UI", 10),
                Location = new Point(x, y),
                Size = new Size(width, 32),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
        }

        private Panel MakePassBox(int x, int y, int width, ref TextBox txt, ref Button btn, bool visible)
        {
            Panel pnl = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(width, 36),
                BackColor = Color.White
            };
            pnl.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, pnl.ClientRectangle,
                    Color.FromArgb(180, 180, 180), ButtonBorderStyle.Solid);
            };

            txt = new TextBox
            {
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.None,
                Location = new Point(8, 7),
                Size = new Size(width - 50, 22),
                UseSystemPasswordChar = true,
                BackColor = Color.White
            };

            btn = new Button
            {
                Text = "👁",
                Font = new Font("Segoe UI", 11),
                Size = new Size(36, 32),
                Location = new Point(width - 38, 2),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(107, 114, 128),
                Cursor = Cursors.Hand,
                TabStop = false
            };
            btn.FlatAppearance.BorderSize = 0;

            pnl.Controls.Add(txt);
            pnl.Controls.Add(btn);
            return pnl;
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            // Validate required fields
            if (string.IsNullOrEmpty(txtFirstName.Text) ||
                string.IsNullOrEmpty(txtLastName.Text) ||
                string.IsNullOrEmpty(txtMatric.Text) ||
                string.IsNullOrEmpty(cmbGender.Text) ||
                string.IsNullOrEmpty(txtDepartment.Text) ||
                string.IsNullOrEmpty(cmbLevel.Text) ||
                string.IsNullOrEmpty(cmbSession.Text) ||
                string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Please fill in all required fields marked with *", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Passwords do not match. Please try again.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtPassword.Text.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters long.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Check if matric number already exists
                    string checkQuery = "SELECT COUNT(*) FROM Student WHERE MatricNumber=@matric";
                    MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@matric", txtMatric.Text.Trim());
                    int existing = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (existing > 0)
                    {
                        MessageBox.Show("A student with this matriculation number already exists.\nPlease check and try again.", "Already Registered",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Register student
                    string query = @"INSERT INTO Student 
                                    (FirstName, LastName, MatricNumber, Gender, Department, 
                                     Level, PhoneNumber, Email, Username, Password, Session)
                                    VALUES 
                                    (@first, @last, @matric, @gender, @dept,
                                     @level, @phone, @email, @username, @password, @session)";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@first", txtFirstName.Text.Trim());
                    cmd.Parameters.AddWithValue("@last", txtLastName.Text.Trim());
                    cmd.Parameters.AddWithValue("@matric", txtMatric.Text.Trim());
                    cmd.Parameters.AddWithValue("@gender", cmbGender.Text);
                    cmd.Parameters.AddWithValue("@dept", txtDepartment.Text.Trim());
                    cmd.Parameters.AddWithValue("@level", cmbLevel.Text);
                    cmd.Parameters.AddWithValue("@phone", txtPhone.Text.Trim());
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@username", txtMatric.Text.Trim());
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                    cmd.Parameters.AddWithValue("@session", cmbSession.Text);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show(
                        "Registration successful!\n\n" +
                        "You can now log in using:\n" +
                        "• Matriculation Number: " + txtMatric.Text.Trim() + "\n" +
                        "• Your chosen password",
                        "Registration Complete",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoginForm loginForm = new LoginForm();
                    loginForm.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error registering: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
