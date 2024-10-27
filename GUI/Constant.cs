using System.Drawing;

namespace GUI
{
    public class Constant
    {
        public static int DayOfWeek = 7;
        public static int DayOfColumn = 6;

        public static int dayButtonWidth = 45;
        public static int dayButtonHeight = 25;

        public static Color BrightBlue = Color.FromArgb(50, 100, 230);
        public static Color BrightBlack = Color.FromArgb(49, 50, 52);
        public static Color OffWhite = Color.FromArgb(247, 247, 247);

        public static string EDIT_MODE = "Edit";
        public static string SAVE_MODE = "Save";

        public static int Tuition_B = 11000000;
        public static int Tuition_C = 12000000;
        public static int Tuition_D = 15000000;
        public static int Tuition_E = 20000000;
        public static bool DefaultInvoiceStatus = false; // Mặc định là pending

        public static string EmailValidationPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        public static string PhoneNumberValidationPattern = @"^0\d{9,10}$";

        public static int MinExperienceYear_LicenseB = 3;
        public static int MinExperienceYear_LicenseCDE = 5;
        public static int MinAge = 18;

        public static int DurationHours_B = 588;
        public static int DurationHours_C = 920;
        public static int DurationHours_D = 192;
        public static int DurationHours_E = 192;

        public static int LicenseID_None = 1001;
        public static int LicenseID_B = 1002;
        public static int LicenseID_C = 1003;
        public static int LicenseID_D = 1004;
        public static int LicenseID_E = 1005;

        public static int StatusID_Active = 1001;
    }
}
