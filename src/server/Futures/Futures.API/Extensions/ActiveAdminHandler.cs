// using System.Security.Claims;
// using Futures.Domain.Enums;
// using Futures.Domain.Extensions;
// using Microsoft.AspNetCore.Authorization;
//
// namespace Futures.API.Extensions;
//
// public class ActiveAdminHandler : AuthorizationHandler<ActiveAdminRequirement>
// {
// 	protected override Task HandleRequirementAsync(
// 		AuthorizationHandlerContext context,
// 		ActiveAdminRequirement requirement)
// 	{
// 		// Проверяем, есть ли claim с идентификатором пользователя
// 		var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);
// 		var userRole = context.User.FindFirst(ClaimTypes.Role);
//
// 		if (userIdClaim == null || userRole == null)
// 		{
// 			context.Fail();
//
// 			return Task.CompletedTask;
// 		}
//
// 		if (!context.User.IsInRole(Role.Admin.GetDescription()) &&
// 			!context.User.IsInRole(Role.User.GetDescription()))
// 		{
// 			context.Fail();
//
// 			return Task.CompletedTask;
// 		}
//
// 		context.Succeed(requirement);
//
// 		return Task.CompletedTask;
// 	}
// }
//
// public class ActiveAdminRequirement : IAuthorizationRequirement;