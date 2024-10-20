using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TeacherDAL : BaseDAL<Teacher>
    {
        protected override IEnumerable<dynamic> QueryAllData()
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<dynamic> QueryDataByFilter(string filterString)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<dynamic> QueryDataByKeyword(string keyword)
        {
            throw new NotImplementedException();
        }
    }
}
