using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Extensions
{
    public static class LearnerExtension
    {
        public static Learner SelectLearner(this Learner learner)
        {
            return new Learner
            {
                LearnerID = learner.LearnerID,
                CurrentLicenseID = learner.CurrentLicenseID,
                FullName = learner.FullName,
                DateOfBirth = learner.DateOfBirth,
                Gender = learner.Gender,
                PhoneNumber = learner.PhoneNumber,
                Email = learner.Email,
                Address = learner.Address,
                CitizenID = learner.CitizenID,
                Status = new Status
                {
                    StatusID = learner.Status.StatusID,
                    StatusName = learner.Status.StatusName,
                },
                Created_At = learner.Created_At,
                Updated_At = learner.Updated_At
            };
        }
    }
}
