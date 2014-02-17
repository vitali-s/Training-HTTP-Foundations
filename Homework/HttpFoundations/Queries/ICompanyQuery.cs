using System.Collections.Generic;
using HttpFoundations.Models;

namespace HttpFoundations.Queries
{
    public interface ICompanyQuery
    {
        CompanyInfoModel GetCompanyInfo(string name);

        IEnumerable<OfficeModel> GetOffices(string name);

        IEnumerable<EmployeeModel> GetEmployees(string name);
    }
}