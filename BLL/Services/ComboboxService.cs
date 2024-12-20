﻿using DAL;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BLL.Services
{
	public class ComboboxService
	{
		public static void AssignLearnersToCombobox(Guna2ComboBox cbo)
		{
			LearnerBLL.Instance.AssignLearnersToCombobox(cbo);
		}

		public static void AssignCoursesToCombobox(Guna2ComboBox cbo)
        {
            CourseBLL.Instance.AssignCoursesToCombobox(cbo);
        }

		public static void AssignCoursesToCombobox(Guna2ComboBox cbo, string status)
		{
            CourseBLL.Instance.AssignCoursesToCombobox(cbo, status);
        }

        public static void AssignCoursesToCombobox(Guna2ComboBox cbo, string status, DateTime curDate)
		{
			CourseBLL.Instance.AssignCoursesToCombobox(cbo, status, curDate);
		}

		public static void AssignAvailableCourseToCombobox(Guna2ComboBox cbo, string licenseName)
		{
			CourseBLL.Instance.AssignAvailableCourseToCombobox(cbo, licenseName);
		}

		public static void GetAvailableAndLearnerCourses(Guna2ComboBox cbo, int learnerID)
		{
			CourseBLL.Instance.GetAvailableAndLearnerCourses(cbo, learnerID);
		}

		public static void AssignTeachersToCombobox(Guna2ComboBox cbo)
		{
			TeacherBLL.Instance.AssignTeachersToCombobox(cbo);
		}

		public static void AssignTeachersToCombobox(Guna2ComboBox cbo, int courseID, int sessionID, DateTime curDate)
		{
			TeacherBLL.Instance.AssignTeachersToCombobox(cbo, courseID, sessionID, curDate);
		}

		public static void AssignTeacherInCourseToCombobox(Guna2ComboBox cbo, int courseID, int sessionID, DateTime curDate)
		{
			TeacherBLL.Instance.AssignTeacherInCourseToCombobox(cbo, courseID, sessionID, curDate);
		}

		public static void AssignSessionsToCombobox(Guna2ComboBox cbo)
		{
			SessionBLL.Instance.AssignSessionsToCombobox(cbo);
		}

		public static void AssignSessionsToCombobox(Guna2ComboBox cboSessions, int courseID, DateTime curDate)
		{
			SessionBLL.Instance.AssignSessionsToCombobox(cboSessions, courseID, curDate);
		}

		public static void AssignSessionsToCombobox(Guna2ComboBox cboSessions, int courseID, DateTime curDate, int sessionID)
		{
			SessionBLL.Instance.AssignSessionsToCombobox(cboSessions, courseID, curDate, sessionID);
		}

		public static void AssignVehiclesToCombobox(Guna2ComboBox cbo)
		{
			VehicleBLL.Instance.AssignVehiclesToCombobox(cbo);
		}

		public static void AssignVehiclesToCombobox(Guna2ComboBox cbo, int courseID, int sessionID, DateTime curDate)
		{
			VehicleBLL.Instance.AssignVehiclesToCombobox(cbo, courseID, sessionID, curDate);
		}

		public static void AssignLicensesToCombobox(Guna2ComboBox cbo)
		{
			LicenseBLL.Instance.AssignLicensesToCombobox(cbo);
		}

        public static void AssignLicensesToCombobox_ForLearner(Guna2ComboBox cbo, int age)
        {
            LicenseBLL.Instance.AssignLicensesToCombobox_ForLearner(cbo, age);
        }

        public static void AssignStatesToCombobox(Guna2ComboBox cbo)
		{
			StatusBLL.Instance.AssignCoursesToCombobox(cbo);
		}
    
		public static void AssignInvoicesToCombobox(Guna2ComboBox cbo)
		{
			InvoiceBLL.Instance.AssignInvoicesToCombobox(cbo);
		}
    
		public static void AssignInvoicesToCombobox(Guna2ComboBox cbo, string status)
		{
			InvoiceBLL.Instance.AssignInvoicesToCombobox(cbo, status);
		}
    
		public static void AssignPaymentsToCombobox(Guna2ComboBox cbo)
		{
			PaymentBLL.Instance.AssignPaymentsToCombobox(cbo);
    }
    
		public static void AssignVehicleInCourseToCombobox(Guna2ComboBox cbo, int courseID, int sessionID, DateTime curDate)
		{
			VehicleBLL.Instance.AssignVehicleInCourseToCombobox(cbo, courseID, sessionID, curDate);
		}
	}
}
