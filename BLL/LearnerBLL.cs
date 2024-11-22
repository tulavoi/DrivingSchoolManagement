using DAL;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Data;

namespace BLL
{
    public class LearnerBLL
    {
        #region Properties
        private static LearnerBLL instance;

        public static LearnerBLL Instance
        {
            get
            {
                if (instance == null) instance = new LearnerBLL();
                return instance;
            }
        }
        #endregion

        public void AssignLearnersToCombobox(Guna2ComboBox cbo)
        {
            List<Learner> learners = LearnerDAL.Instance.GetAllLearners();
            this.AddLearnersToCombobox(cbo, learners);
        }

        public void AssignLearnersToCombobox(Guna2ComboBox cbo, string status)
        {
            List<Learner> learners = LearnerDAL.Instance.FilterLearnersByStatus(status);
            this.AddLearnersToCombobox(cbo, learners);
        }

        private void AddLearnersToCombobox(Guna2ComboBox cbo, List<Learner> learners)
        {
            Learner learner = new Learner();
            learner.FullName = "Select Learner";
            learners.Insert(0, learner);

            cbo.DataSource = learners;
            cbo.ValueMember = "LearnerID";
            cbo.DisplayMember = "FullName";
        }

        public void LoadAllLearners(Guna2DataGridView dgv)
        {
            List<Learner> learners = LearnerDAL.Instance.GetAllLearners();
            this.AddLearnersToDataGridView(dgv, learners);
        }

        public List<Learner> GetAllLearners()
        {
            return LearnerDAL.Instance.GetAllLearners();
        }

        public void SearchLearners(Guna2DataGridView dgv, string keyword)
        {
            List<Learner> learners = LearnerDAL.Instance.SearchLearners(keyword);
            this.AddLearnersToDataGridView(dgv, learners);
        }

        public void SearchLearners(Guna2ComboBox cbo, string keyword)
        {
            List<Learner> learners = LearnerDAL.Instance.SearchLearners(keyword);
            this.AddLearnersToCombobox(cbo, learners);
        }

        public void FilterLearnersByStatus(Guna2DataGridView dgv, string status)
        {
            List<Learner> learners = LearnerDAL.Instance.FilterLearnersByStatus(status);
            this.AddLearnersToDataGridView(dgv, learners);
        }

        private void AddLearnersToDataGridView(Guna2DataGridView dgv, List<Learner> learners)
        {
            dgv.Rows.Clear();
            foreach (var learner in learners)
            {
                int rowIndex = dgv.Rows.Add();

                if (rowIndex != -1 && rowIndex < dgv.Rows.Count)
                {
                    dgv.Rows[rowIndex].Tag = learner;
                    dgv.Rows[rowIndex].Cells["FullName"].Value = learner.FullName;
                    dgv.Rows[rowIndex].Cells["CitizenID"].Value = learner.CitizenID;
                    dgv.Rows[rowIndex].Cells["Status"].Value = learner.Status.StatusName;
                }
            }
        }

        public bool AddLearner(Learner learner, int courseID)
        {
            return LearnerDAL.Instance.AddLearner(learner, courseID);
        }

        public bool EditLearner(Learner learner, int courseID)
        {
            return LearnerDAL.Instance.EditLearner(learner, courseID);
        }

        public bool DeleteLearner(int learnerID)
        {
            return LearnerDAL.Instance.DeleteLearner(learnerID);
        }

        public Learner GetLearner(int learnerID)
        {
            return LearnerDAL.Instance.GetLearner(learnerID);
        }

        public DataTable GetAllLearnersData()
        {
            return LearnerDAL.Instance.GetAllLearnersData();
        }

        public bool ConfirmPass(int learnerID)
        {
            return LearnerDAL.Instance.ConfirmPass(learnerID);
        }

        public DataTable GetEligibleLearnersData()
        {
            return LearnerDAL.Instance.GetEligibleLearnersData();
        }
    }
}
