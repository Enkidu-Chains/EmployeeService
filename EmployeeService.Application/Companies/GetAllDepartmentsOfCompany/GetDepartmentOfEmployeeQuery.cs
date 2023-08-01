﻿using EmployeeService.Common.Application.Queries;
using EmployeeService.Domain.Model.Companies;
using EmployeeService.Domain.Model.Companies.Departments;
using EmployeeService.Domain.Model.Employees;

namespace EmployeeService.Application.Companies.GetAllDepartmentsOfCompany;

public class GetDepartmentOfEmployeeQuery : IQuery<Department>
{
    public GetDepartmentOfEmployeeQuery(EmployeeId employeeId)
    {
        EmployeeId = employeeId;
    }
    
    public EmployeeId EmployeeId { get; set; }
}