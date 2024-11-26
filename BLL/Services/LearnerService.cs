using BLL;
using DAL;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLL.Services
{
    public class LearnerService
    {
        public static bool AddLearner(Learner learner, Course course)
        {
            return LearnerBLL.Instance.AddLearner(learner, course);
        }

        public static bool EditLearner(Learner learner, int courseID)
        {
            return LearnerBLL.Instance.EditLearner(learner, courseID);
        }

        public static bool DeleteLearner(int learnerID)
        {
            return LearnerBLL.Instance.DeleteLearner(learnerID);
        }

        public static Learner GetLearner(int learnerID)
        {
            return LearnerBLL.Instance.GetLearner(learnerID);
        }

        public static void LoadAllLearners(Guna2DataGridView dgv)
        {
            LearnerBLL.Instance.LoadAllLearners(dgv);
        }

        public static List<Learner> GetAllLearners()
        {
            return LearnerBLL.Instance.GetAllLearners();
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

        public static DataTable GetAllLearnersData()
        {
            return LearnerBLL.Instance.GetAllLearnersData();
        }

        public static bool ConfirmPass(int learnerID)
        {
            return LearnerBLL.Instance.ConfirmPass(learnerID);
        }

        public static DataTable GetEligibleLearnersData()
        {
            return LearnerBLL.Instance.GetEligibleLearnersData();
        }

        public static DataTable GetLearnerDetailData(int learnerID)
        {
            return LearnerBLL.Instance.GetLearnerDetailData(learnerID);
        }
    }
}
