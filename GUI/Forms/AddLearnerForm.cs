using BLL.Services;
using DAL;
using GUI.Validators;
using Guna.UI2.WinForms;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace GUI
{
    public partial class AddLearnerForm : Form
    {
        public AddLearnerForm()
        {
            InitializeComponent();
            FormHelper.ApplyRoundedCorners(this, 20);
        }

        private void AddLearnerForm_Load(object sender, EventArgs e)
        {
            shadowAddLearnerForm.SetShadowForm(this);
            
            FormHelper.FocusControl(txtName);
            if (cboCourses.SelectedIndex == -1)
            {
                this.DisplayOrHideCourseDetail(false);
                return;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!this.ValidateFields()) return;

            Learner learner = this.GetLearner();
            Course course = this.GetCourse();
            var result = LearnerService.AddLearner(learner, course); // Tạo learner mới

            FormHelper.ShowActionResult(result, "Learner added successfully.", "Failed to add learner.");
            if (result) // Nếu thêm thành công thì reset controls
                this.Close();
        }

        private Course GetCourse()
        {
            return new Course
            {
                CourseID = Convert.ToInt32(cboCourses.SelectedValue.ToString()),
                DurationInHours = Convert.ToInt32(lblDurationHours.Text),
                Fee = Convert.ToInt32(lblFee.Text.Replace(",", ""))
            };
        }

        private bool ValidateFields()
        {
            string license = cboLicenses.Text;
            string courseLicense = cboCourses.Text.Split('-')[0];

            if (!LearnerValidator.ValidateFullName(txtName, toolTip)) return false;

            if (!LearnerValidator.ValidateCitizenID(txtCitizenId, toolTip)) return false;

            if (!LearnerValidator.ValidateEmail(txtEmail, toolTip)) return false;

            if (!LearnerValidator.ValidatePhoneNumber(txtPhone, toolTip)) return false;

            if (!LearnerValidator.IsLearnerEligible(dtpDOB, toolTip)) return false;

            if (!LearnerValidator.ValidateAddress(txtAddress, toolTip)) return false;

            if (license != "None")
            {
                if (!TeacherValidator.ValidateLicenseNumber(txtLicenseNumber, toolTip)) return false;

                if (!LearnerValidator.IsBeginningDateValid(dtpDOB, dtpBeginningDate, toolTip)) return false;
            }

            if (!LearnerValidator.ValidateSelectedCourse(cboCourses, toolTip)) return false;

            if (!LearnerValidator.ValidateEligibleCourse(dtpDOB, lblLicenseName.Text, cboCourses, toolTip)) return false;

            if (license != "None")
            {
                if (!LearnerValidator.CheckIfLearnerCanUpgradeLicense(license, courseLicense, dtpBeginningDate, toolTip)) return false;
            }

            return true;
        }

        private Learner GetLearner()
        {
            return new Learner()
            {
                FullName = txtName.Text,
                DateOfBirth = DateTime.Parse(dtpDOB.Text),
                Gender = cboGender.Text,
                PhoneNumber = txtPhone.Text,
                Email = txtEmail.Text,
                Address = txtAddress.Text,
                CitizenID = txtCitizenId.Text,
                Nationality = cboNationality.Text,
                StatusID = Constant.StatusID_Active,
                LicenseID = Convert.ToInt32(cboLicenses.SelectedValue),
                LicenseNumber = Convert.ToInt32(cboLicenses.SelectedValue) == 5 ? "" : (txtLicenseNumber.Text ?? ""),
                BeginningDate = Convert.ToInt32(cboLicenses.SelectedValue) == 5 ? (DateTime?)null : dtpBeginningDate.Value,
                IsPass = false,
                Created_At = DateTime.Now
            };
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void numeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormHelper.CheckNumericKeyPress(e);
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormHelper.CheckLetterKeyPress(e, txtName);
        }

        private void cboCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!FormHelper.HasSelectedItem(cboCourses))
            {
                this.DisplayOrHideCourseDetail(false);
                return;
            }
            this.DisplayOrHideCourseDetail(true);
            this.AssignCourseToDetailLabels();

            string licenseName = cboCourses.Text.Split('-')[0];
        }

        private void AssignCourseToDetailLabels()
        {
            int courseID = Convert.ToInt32(cboCourses.SelectedValue.ToString());
            var course = CourseService.GetCourse(courseID);
            if (course == null) return;
            lblLicenseName.Text = course.License.LicenseName;
            lblStartDate.Text = course.StartDate.Value.ToString("dd/MM/yyyy");
            lblEndDate.Text = course.EndDate.Value.ToString("dd/MM/yyyy");

            var currLicense = cboLicenses.Text;
            var courseLicense = course.License.LicenseName;

            if (currLicense == "None")
            {
                lblDurationHours.Text = course.DurationInHours.ToString();
                if (courseLicense == "B") lblFee.Text = Constant.Tuition_B.ToString("N0");
                else if (courseLicense == "C") lblFee.Text = Constant.Tuition_C.ToString("N0");
            }

            else
            {
                if (Constant.UpgradeHours.TryGetValue($"{currLicense}-{courseLicense}", out int durationHours))
                    lblDurationHours.Text = durationHours.ToString();
                if (Constant.UpgradeFee.TryGetValue($"{currLicense}-{courseLicense}", out int fee))
                    lblFee.Text = fee.ToString("N0");
            }
        }

        private void DisplayOrHideCourseDetail(bool showDetails)
        {
            pnlCourseDetails.Visible = showDetails;
            this.Height = showDetails ? 580 : 465;
            FormHelper.ApplyRoundedCorners(this, 20);
        }

        private void dtpBeginningDate_ValueChanged(object sender, EventArgs e)
        {
            // Lấy hàm có sẵn ở TeachersForm
            TeachersForm.Instance.SetBeginningYears(dtpBeginningDate.Value, txtBeginningYears);
        }

        private void cboLicenses_SelectedIndexChanged(object sender, EventArgs e)
        {
            var licenseName = cboLicenses.Text;

            this.EnableLicenseNumberAndBeginningDate(licenseName);
           
            if (licenseName == "None" || licenseName == "B" || licenseName == "C" || licenseName == "D")
                ComboboxService.AssignAvailableCourseToCombobox(cboCourses, licenseName);
        }

        private void EnableLicenseNumberAndBeginningDate(string licenseName)
        {
            if (licenseName == "None" || cboLicenses.SelectedIndex == 0)
            {
                txtLicenseNumber.Clear();
                dtpBeginningDate.Value = DateTime.Now;   
                txtLicenseNumber.Enabled = false;
                dtpBeginningDate.Enabled = false;
            }
            else if (licenseName == "B" || licenseName == "C" || licenseName == "D")
            {
                txtLicenseNumber.Enabled = true;
                dtpBeginningDate.Enabled = true;
            }
        }

        private void dtpDOB_ValueChanged(object sender, EventArgs e)
        {
            var age = this.GetAge();

            if (age > 18)
            {
                txtAge.Text = age.ToString();
                ComboboxService.AssignLicensesToCombobox_ForLearner(cboLicenses, age);
            }
            else
                txtAge.Clear();
        }

        private int GetAge()
        {
            var age = DateTime.Now.Year - dtpDOB.Value.Year;
            if (dtpDOB.Value.AddYears(age) > DateTime.Now.Date)
                age--;
            return age;
        }
    }
}
