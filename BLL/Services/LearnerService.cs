﻿using BLL;
using DAL;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLL.Services
{
    public class LearnerService
    {
        public static bool AddLearner(Learner learner, int courseID)
        {
            return LearnerBLL.Instance.AddLearner(learner, courseID);
        }

        public static bool EditLearner(Learner learner, int courseID)
        {
            return LearnerBLL.Instance.EditLearner(learner, courseID);
        }

        public static bool DeleteLearner(int learnerID)
        {
            return LearnerBLL.Instance.DeleteLearner(learnerID);
        }

        public static void UpdateLicense(int learnerID, int courseID)
        {
            LearnerBLL.Instance.UpdateLicense(learnerID, courseID);
        }

        public static Learner GetLearner(int learnerID)
        {
            return LearnerBLL.Instance.GetLearner(learnerID);
        }

        public static void LoadAllLearners(Guna2DataGridView dgv)
        {
            LearnerBLL.Instance.LoadAllLearners(dgv);
        }

        public static void SearchLearners(Guna2DataGridView dgv, string keyword)
        {
            LearnerBLL.Instance.SearchLearners(dgv, keyword);
        }

        public static void SearchLearners(Guna2ComboBox cbo, string keyword)
        {
            LearnerBLL.Instance.SearchLearners(cbo, keyword);
        }

        public static void FilterLearnersByStatus(Guna2DataGridView dgv, string license)
        {
            LearnerBLL.Instance.FilterLearnersByStatus(dgv, license);
        }
    }
}
