﻿using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace EmployeeService.Api.Controllers;

[Controller]
public abstract class ExtendedControllerBase : ControllerBase
{
	[NonAction]
	public Uri DisplayUrl() => new(HttpContext.Request.GetDisplayUrl());

	[NonAction]
	public CreatedResult Created([ActionResultObjectValue] object? value) => Created(DisplayUrl(), value);
}