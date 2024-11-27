using Org.BouncyCastle.Bcpg.Sig;
using System.Collections.Generic;
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

        public static int LicenseID_B = 1;
        public static int LicenseID_C = 2;
        public static int LicenseID_D = 3;
        public static int LicenseID_E = 4;
        public static int LicenseID_None = 5;

        public static int StatusID_Active = 1;

        public static Dictionary<string, int> UpgradeHours = new Dictionary<string, int>()
        {
            { "B-C", 192 },
            { "B-D", 336 },
            { "C-D", 192 },
            { "C-E", 336 },
            { "D-E", 192 },
        };

        public static Dictionary<string, int> UpgradeFee = new Dictionary<string, int>()
        {
            { "B-C", 5000000 },
            { "B-D", 7000000 },
            { "C-D", 5500000 },
            { "C-E", 7500000 },
            { "D-E", 7000000 },
        };

        public static Dictionary<string, int> UpgradeLicenseRequirements = new Dictionary<string, int>()
        {
            { "B-D", 5 },
            { "C-D", 3 },
            { "C-E", 5 },
            { "D-E", 3 },
        };
    }
}
