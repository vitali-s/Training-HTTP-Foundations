using System.Collections.Generic;
using System.Web.Http;
using HttpFoundations.Models;
using HttpFoundations.Queries;

namespace HttpFoundations.Controllers
{
    public class CompanyController : ApiController
    {
        private readonly ICompanyQuery _companyQuery;

        public CompanyController(ICompanyQuery companyQuery)
        {
            _companyQuery = companyQuery;
        }

        [HttpGet, Route("company/{name}")]
        public CompanyInfoModel Get(string name)
        {
            return _companyQuery.GetCompanyInfo(name);
        }

        [HttpGet, Route("company/{name}/offices")]
        public IEnumerable<OfficeModel> GetOffices(string name)
        {
            return _companyQuery.GetOffices(name);
        }

        [HttpGet, Route("company/{name}/employees")]
        public IEnumerable<EmployeeModel> GetEmployees(string name)
        {
            return _companyQuery.GetEmployees(name);
        }
    }
}
