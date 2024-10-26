using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

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

        protected bool EditData(Func<T, bool> predicate, Action<T> update)
        {
            try
            {
                using (var db = DataAccess.GetDataContext())
                {
                    var entity = db.GetTable<T>().FirstOrDefault(predicate); // Tìm đối tượng theo điều kiện
                    if (entity == null) return false;
                    update(entity); // Cập nhật các thuộc tính
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

        protected bool AddData(T data, out string errorMessage)
        {
            errorMessage = "";
            try
            {
                using (var db = DataAccess.GetDataContext())
                {
                    db.GetTable<T>().InsertOnSubmit(data);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601) // Lỗi trùng khóa UNIQUE
                {
                    if (ex.Message.Contains("UQ_Schedule_Teacher"))
                        errorMessage = "The teacher already has a schedule for this session on this date.";

                    else if (ex.Message.Contains("UQ_Schedule_Learner"))
                        errorMessage = "The learner already has a schedule for this session on this date.";

                    else if (ex.Message.Contains("UQ_Schedule_Vehicle"))
                        errorMessage = "This vehicle is already being used for this session on this date.";
                }
                else
                {
                    errorMessage = ex.Message; // Lỗi khác
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        protected bool EditData(Func<T, bool> predicate, Action<T> update, out string errorMessage)
        {
            errorMessage = ""; // Khởi tạo thông báo lỗi
            try
            {
                using (var db = DataAccess.GetDataContext())
                {
                    var entity = db.GetTable<T>().FirstOrDefault(predicate); // Tìm đối tượng theo điều kiện
                    if (entity == null)
                    {
                        errorMessage = "Entity not found."; // Thông báo nếu không tìm thấy đối tượng
                        return false;
                    }
                    update(entity); // Cập nhật các thuộc tính
                    db.SubmitChanges(); // Lưu thay đổi
                    return true;
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601) // Lỗi trùng khóa UNIQUE
                {
                    if (ex.Message.Contains("UQ_Schedule_Teacher"))
                        errorMessage = "The teacher already has a schedule for this session on this date.";

                    else if (ex.Message.Contains("UQ_Schedule_Learner"))
                        errorMessage = "The learner already has a schedule for this session on this date.";

                    else if (ex.Message.Contains("UQ_Schedule_Vehicle"))
                        errorMessage = "This vehicle is already being used for this session on this date.";
                }
                else
                {
                    errorMessage = ex.Message; // Lỗi khác
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        protected bool DeleteData(Func<T, bool> predicate)
        {
            try
            {
                using (var db = DataAccess.GetDataContext())
                {
                    var entity = db.GetTable<T>().FirstOrDefault(predicate);
                    if (entity == null) return false;
                    db.GetTable<T>().DeleteOnSubmit(entity);
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

        protected bool UpdateStatus(Func<T, bool> predicate, int statusID)
        {
            try
            {
                using (var db = DataAccess.GetDataContext())
                {
                    var entity = db.GetTable<T>().FirstOrDefault(predicate);
                    if (entity == null) return false;

                    // Cập nhật StatusID của đối tượng thay vì xóa
                    var statusProperty = entity.GetType().GetProperty("StatusID");
                    if (statusProperty != null && statusProperty.CanWrite)
                    {
                        statusProperty.SetValue(entity, statusID);
                    }
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
    }
}
