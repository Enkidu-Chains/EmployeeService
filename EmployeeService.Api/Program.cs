using EmployeeService.Application.Companies.CreateCompany;
using EmployeeService.Application.Companies.GetDepartmentOfEmployee;
using EmployeeService.Application.Companies.GetNewCompanyId;
using EmployeeService.Application.Companies.GetNewDepartmentId;
using EmployeeService.Application.Companies.IsThereCompany;
using EmployeeService.Application.Companies.IsThereDepartment;
using EmployeeService.Application.Companies.IsThereDepartmentInCompany;
using EmployeeService.Application.Employees.CreateEmployee;
using EmployeeService.Application.Employees.DeleteEmployee;
using EmployeeService.Application.Employees.GetAllEmployeeOfCompany;
using EmployeeService.Application.Employees.GetNewEmployeeId;
using EmployeeService.Application.Employees.IsThereEmployee;
using EmployeeService.Common.Application.Data;
using EmployeeService.Common.Domain.Model;
using EmployeeService.Domain.Model.Companies;
using EmployeeService.Domain.Model.Employees;
using EmployeeService.Infrastructure.DataAccess;
using EmployeeService.Infrastructure.Domain.Companies;
using EmployeeService.Infrastructure.Domain.Employees;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<GetDepartmentOfEmployeeQueryHandler>();
builder.Services.AddScoped<GetAllEmployeeOfCompanyQueryHandler>();
builder.Services.AddScoped<GetNewCompanyIdQueryHandler>();
builder.Services.AddScoped<GetNewDepartmentIdQueryHandler>();
builder.Services.AddScoped<GetNewEmployeeIdQueryHandler>();
builder.Services.AddScoped<IsThereDepartmentInCompanyQueryHandler>();
builder.Services.AddScoped<IsThereDepartmentQueryHandler>();
builder.Services.AddScoped<IsThereCompanyQueryHandler>();
builder.Services.AddScoped<IsThereEmployeeQueryHandler>();

builder.Services.AddScoped<CreateCompanyCommandHandler>();
builder.Services.AddScoped<CreateEmployeeCommandHandler>();
builder.Services.AddScoped<DeleteEmployeeCommandHandler>();

builder.Services.AddScoped<ISqlConnectionFactory, SqliteConnectionFactory>(provider =>
{
	string? connectionString = provider.GetRequiredService<IConfiguration>()
		.GetConnectionString("Database");

	return new SqliteConnectionFactory(connectionString);
});
builder.Services.AddDbContext<EmployeeServiceDbContext>(optionsBuilder =>
{
	optionsBuilder.UseSqlite(
		builder.Configuration.GetConnectionString("Database")
	).UseSnakeCaseNamingConvention();
});
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

builder.Services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
builder.Services.AddScoped<DomainEventSource>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();