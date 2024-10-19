﻿using DAL;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void AssignLeanersToCombobox(Guna2ComboBox cbo)
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
    }
}