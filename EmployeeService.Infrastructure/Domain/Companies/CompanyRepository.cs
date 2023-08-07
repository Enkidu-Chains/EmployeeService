﻿using EmployeeService.Domain.Model.Companies;
using EmployeeService.Infrastructure.DataAccess;

namespace EmployeeService.Infrastructure.Domain.Companies;

public class CompanyRepository : ICompanyRepository
{
	private readonly EmployeeServiceDbContext _dbContext;

	public CompanyRepository(EmployeeServiceDbContext dbContext) => _dbContext = dbContext;

	public void CreateCompany(Company company)
	{
		var companyModel = new CompanyModel { Id = company.Identity.Deconvert(), Name = company.Name };

		_dbContext.Companies.Add(companyModel);

		IEnumerable<DepartmentModel> departmentModels = company.Departments.Select(department =>
			new DepartmentModel
			{
				Id = department.Identity.Deconvert(),
				Name = department.Name,
				Phone = department.PhoneNumber.Number,
				CompanyId = department.CompanyId.Deconvert()
			}
		);

		_dbContext.Departments.AddRange(departmentModels);
	}

	public void DeleteCompanyById(CompanyId companyId)
	{
		int id = companyId.Deconvert();
		
		var companyModelToRemove = new CompanyModel{ Id = id };

		_dbContext.Companies.Attach(companyModelToRemove);

		_dbContext.Companies.Remove(companyModelToRemove);
	}

	public void Save() => _dbContext.SaveChanges();
}