using DAL;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;

namespace BLL.Services
{
	public class ComboboxService
	{
		public static void AssignLearnersToCombobox(Guna2ComboBox cbo)
		{
			LearnerBLL.Instance.AssignLearnersToCombobox(cbo);
		}

		public static void AssignLearnersToCombobox(Guna2ComboBox cbo, string status)
		{
			LearnerBLL.Instance.AssignLearnersToCombobox(cbo, status);
		}

		public static void AssignCoursesToCombobox(Guna2ComboBox cbo)
		{
			CourseBLL.Instance.AssignCoursesToCombobox(cbo);
		}

		public static void AssignCoursesToCombobox(Guna2ComboBox cbo, string status)
		{
			CourseBLL.Instance.AssignCoursesToCombobox(cbo, status);
		}

		public static void AssignAvailableToCombobox(Guna2ComboBox cbo)
		{
			CourseBLL.Instance.AssignAvailableToCombobox(cbo);
		}

		public static void GetAvailableAndLearnerCourses(Guna2ComboBox cbo, int learnerID)
		{
			CourseBLL.Instance.GetAvailableAndLearnerCourses(cbo, learnerID);
		}

		public static void AssignCoursesToCombobox(Guna2ComboBox cbo, int learnerID)
		{
			CourseBLL.Instance.AssignCoursesToCombobox(cbo, learnerID);
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

		public static void AssignStatesToCombobox(Guna2ComboBox cbo)
		{
			StatusBLL.Instance.AssignCoursesToCombobox(cbo);
		}

		public static void AssignVehicleInCourseToCombobox(Guna2ComboBox cbo, int courseID, int sessionID, DateTime curDate)
		{
			VehicleBLL.Instance.AssignVehicleInCourseToCombobox(cbo, courseID, sessionID, curDate);
		}
	}
}
