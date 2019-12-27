using System;
using System.Linq;
using models = Database.Models;
namespace Base.DTOs.USR
{
    public class UserListDTO
    {
        public Guid Id { get; set; }
        public string EmployeeNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string PhoneNo { get; set; }

        public static UserListDTO CreateFromQueryResult(UserQueryResult model)
        {
            if (model != null)
            {
                var result = new UserListDTO()
                {
                    Id = model.User.ID,
                    EmployeeNo = model.User.EmployeeNo,
                    FirstName = model.User.FirstName,
                    LastName = model.User.LastName,
                    DisplayName = model.User.DisplayName,
                    PhoneNo = model.User.PhoneNo
                };
                return result;
            }
            else
            {
                return null;
            }
        }

        public static UserListDTO CreateFromModel(models.USR.User model)
        {
            if (model != null)
            {
                var result = new UserListDTO()
                {
                    Id = model.ID,
                    EmployeeNo = model.EmployeeNo,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    DisplayName = model.DisplayName
                };
                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(UserListSortByParam sortByParam, ref IQueryable<UserQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case UserListSortBy.FirstName:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.User.FirstName);
                        else query = query.OrderByDescending(o => o.User.FirstName);
                        break;
                    case UserListSortBy.LastName:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.User.LastName);
                        else query = query.OrderByDescending(o => o.User.LastName);
                        break;
                    case UserListSortBy.EmployeeNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.User.EmployeeNo);
                        else query = query.OrderByDescending(o => o.User.EmployeeNo);
                        break;
                    case UserListSortBy.DisplayName:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.User.DisplayName);
                        else query = query.OrderByDescending(o => o.User.DisplayName);
                        break;
                    default:
                        query = query.OrderBy(o => o.User.EmployeeNo);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.User.EmployeeNo);
            }
        }
    }

    public class UserQueryResult
    {
        public models.USR.User User { get; set; }
    }
}
