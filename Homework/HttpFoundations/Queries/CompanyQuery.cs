using System;
using System.Collections.Generic;
using System.Linq;
using HttpFoundations.Models;
using Ploeh.AutoFixture;

namespace HttpFoundations.Queries
{
    public class CompanyQuery : ICompanyQuery
    {
        private static readonly object LockObject = new object();

        private readonly IFixture _fixture;

        private ICollection<EmployeeModel> _employeesStorage;
        private DateTime _employeesUpdateDateTime;

        public CompanyQuery()
        {
            _fixture = new Fixture();
            _employeesStorage = new List<EmployeeModel>();
            _employeesUpdateDateTime = DateTime.Now;
        }

        public CompanyInfoModel GetCompanyInfo(string name)
        {
            var companyInfo = _fixture.Build<CompanyInfoModel>()
                .With(p => p.Name, name)
                .With(p => p.Description, string.Concat(Enumerable.Repeat(name, 65536)))
                .Create();

            return companyInfo;
        }

        public IEnumerable<OfficeModel> GetOffices(string name)
        {
            var offices = _fixture.Build<OfficeModel>()
                .With(p => p.CompanyName, name)
                .With(p => p.CurrentDate, DateTime.Now)
                .CreateMany(5);

            return offices;
        }

        public IEnumerable<EmployeeModel> GetEmployees(string name)
        {
            if (DateTime.Now.Subtract(_employeesUpdateDateTime) > TimeSpan.FromSeconds(30) || _employeesStorage.Count == 0)
            {
                lock (LockObject)
                {
                    _employeesStorage = _fixture.Build<EmployeeModel>()
                        .With(p => p.CompanyName, name)
                        .With(p => p.CurrentDate, DateTime.Now)
                        .CreateMany(5)
                        .ToList();

                    _employeesUpdateDateTime = DateTime.Now;
                }
            }

            return _employeesStorage;
        }
    }
}
