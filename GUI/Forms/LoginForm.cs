using BLL;
using Guna.UI2.WinForms;
using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;

namespace GUI
{
    public partial class LoginForm : Form
    {
        #region Properties
        private const string ConfigFilePath = "configFilePath";
        #endregion

        public LoginForm()
        {
            InitializeComponent();
            FormHelper.ApplyRoundedCorners(this, 20);
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            this.shadowLoginForm.SetShadowForm(this);

            this.LoadLoginInfo();
        }

        private void LoadLoginInfo()
        {
            if (!File.Exists(ConfigFilePath)) return;

            try
            {
                XDocument doc = XDocument.Load(ConfigFilePath);
                XElement root = doc.Root;

                if (root == null) return;

                this.LoadElementsToTextBoxes(root);

                toggleSwitchRemember.Checked = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void LoadElementsToTextBoxes(XElement root)
        {
            txtEmail.Text = root.Element("Email")?.Value ?? string.Empty;
            txtPassword.Text = root.Element("Password")?.Value ?? string.Empty;
            txtServerName.Text = root.Element("ServerName")?.Value ?? string.Empty;
            txtDBName.Text = root.Element("DBName")?.Value ?? string.Empty;
        }

        private void ClearLoginInfo()
        {
            if (File.Exists(ConfigFilePath))
                File.Delete(ConfigFilePath);
        }

        private void SaveLoginInfo(params string[] values)
        {
            XDocument doc = new XDocument(
                new XElement("LoginInfo",
                    new XElement("Email", values[0]),
                    new XElement("Password", values[1]),
                    new XElement("ServerName", values[2]),
                    new XElement("DBName", values[3])
                )
            );
            doc.Save(ConfigFilePath);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.HandleLogin();
        }

        private void HandleLogin()
        {
            string serverName = txtServerName.Text;
            string dbName = txtDBName.Text;
            string email = txtEmail.Text;
            string pass = txtPassword.Text;

            if (!this.ValidateInputFields(serverName, dbName, email, pass)) return;

            if (!this.ConnectToDatabase(serverName, dbName)) return;

            if (!this.ValidateLoginFields(email, pass)) return;

            if (AccountBLL.Instance.CheckLogin(email, pass))
            {
                this.CheckSaveLoginInfo(email, pass, serverName, dbName);
                OpenMainForm();
            }

            else
                FormHelper.ShowError("Login failed!");
        }

        private bool ConnectToDatabase(string serverName, string dbName)
        {
            if (!DataAccessBLL.Instance.SetupConnection(serverName, dbName))
            {
                FormHelper.ShowError("Connection failed!");
                return false;
            }
            return true;
        }

        private void CheckSaveLoginInfo(params string[] values)
        {
            if (toggleSwitchRemember.Checked)
                this.SaveLoginInfo(values);
            else
                this.ClearLoginInfo();
        }

        private void ShowError(string message)
        {
            MessageBox.Show($"{message}",
                            "Notify",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
        }

        private bool ValidateInputFields(string serverName, string dbName, string email, string pass)
        {
            return this.ValidateFields(
                                    (txtServerName, serverName, "Please enter server name."),
                                    (txtDBName, dbName, "Please enter database name."),
                                    (txtEmail, email, "Please enter your email."),
                                    (txtPassword, pass, "Please enter your password."));
        }

        private bool ValidateLoginFields(string email, string pass)
        {
            if (!this.IsValidEmail(email))
            {
                FormHelper.ShowToolTip(txtEmail, toolTip, "Invalid email.");
                return false;
            }
            if (pass.Length < 6)
            {
                FormHelper.ShowToolTip(txtPassword, toolTip, "Password must be at least 6 characters.");
                return false;
            }
            return true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var address = new System.Net.Mail.MailAddress(email);
                return address.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool ValidateFields(params (Guna2TextBox textBox, string value, string errorMessage)[] fields)
        {
            foreach (var field in fields)
            {
                if (string.IsNullOrEmpty(field.value))
                {
                    FormHelper.ShowToolTip(field.textBox, toolTip, field.errorMessage);
                    return false;
                }
            }
            return true;
        }

        private void OpenMainForm()
        {
            MainForm form = new MainForm();
            this.Hide();
            form.Show();
        }

        private void btnCloseForm_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void TextBoxes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;

                btnLogin_Click(sender, e);
            }
        }
    }
}
