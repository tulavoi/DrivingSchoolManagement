﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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

		public static decimal Tuition_B = 11000000;
		public static decimal Tuition_C = 12000000;
		public static decimal Tuition_D = 15000000;
		public static decimal Tuition_E = 20000000;
		public static string DefaultInvoiceStatus = "Pending";

		public static string EmailValidationPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
		public static string PhoneNumberValidationPattern = @"^0\d{9,10}$";

		public static int MinExperienceYear_LicenseB = 3;
		public static int MinExperienceYear_LicenseCDE = 5;
		public static int MinAge = 18;
    }
}
