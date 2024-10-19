using DAL;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;

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

        public void SearchLearners(Guna2DataGridView dgv, string keyword)
        {
            List<Learner> learners = LearnerDAL.Instance.SearchLearners(keyword);
            this.AddLearnersToDataGridView(dgv, learners);
        }

        public void FilterLearnersByStatus(Guna2DataGridView dgv, string licenseType)
        {
            List<Learner> learners = LearnerDAL.Instance.FilterLearnersByStatus(licenseType);
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
                    dgv.Rows[rowIndex].Cells["Created_At"].Value = learner.Created_At;
                }
            }
        }

        public bool AddLearner(Learner learner)
        {
            return LearnerDAL.Instance.AddLearner(learner);
        }

        public bool EditLearner(Learner learner)
        {
            return LearnerDAL.Instance.EditLearner(learner);
        }

        public bool DeleteLearner(int learnerID)
        {
            return LearnerDAL.Instance.DeleteLearner(learnerID);
        }
    }
}
