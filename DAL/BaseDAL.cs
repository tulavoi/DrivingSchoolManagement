using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public abstract class BaseDAL<T> where T : class
    {
        protected abstract IEnumerable<dynamic> QueryAllData();

        protected abstract IEnumerable<dynamic> QueryDataByKeyword(string keyword);

        protected abstract IEnumerable<dynamic> QueryDataByFilter(string filterString);

        protected bool AddData(T data)
        {
            try
            {
                using (var db = DataAccess.GetDataContext())
                {
                    db.GetTable<T>().InsertOnSubmit(data);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        protected virtual IEnumerable<dynamic> QueryLearnerByCourseID(int courseID)
        {
            return Enumerable.Empty<dynamic>();
        }

        protected List<T> MapToList(IEnumerable<dynamic> data, Func<dynamic, T> mapFunction)
        {
            if (data == null) return new List<T>();

            return data.Select(mapFunction).ToList();
        }

        protected List<T> ExecuteQuery(Func<IEnumerable<dynamic>> queryFunction, Func<dynamic, T> mapFunction)
        {
            var data = queryFunction();
            return MapToList(data, mapFunction);
        }

        protected List<T> GetAll(Func<dynamic, T> mapFunction)
        {
            return this.ExecuteQuery(QueryAllData, mapFunction);
        }

        protected List<T> SearchData(string keyword, Func<dynamic, T> mapFunction)
        {
            var data = QueryDataByKeyword(keyword);
            return this.ExecuteQuery(() => QueryDataByKeyword(keyword), mapFunction);
        }

        protected List<T> FilterData(string filterString, Func<dynamic, T> mapFunction)
        {
            var data = QueryDataByFilter(filterString);
            return this.ExecuteQuery(() => QueryDataByFilter(filterString), mapFunction);
        }

        protected List<T> GetLearnerByCourseID(int courseID, Func<dynamic, T> mapFunction)
        {
            var data = QueryLearnerByCourseID(courseID);
            return this.ExecuteQuery(() => QueryLearnerByCourseID(courseID), mapFunction);
        }
    }
}
