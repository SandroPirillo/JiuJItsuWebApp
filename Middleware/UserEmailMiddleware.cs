public class UserEmailMiddleware
{
	private readonly RequestDelegate _next;

	public UserEmailMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task InvokeAsync(HttpContext context)
	{

		context.Items["UserEmail"] = context.Session.GetString("UserEmail");


		await _next(context);
	}
}

public static class UserEmailMiddlewareExtensions
{
	public static IApplicationBuilder UseUserEmailMiddleware(this IApplicationBuilder builder)
	{
		return builder.UseMiddleware<UserEmailMiddleware>();
	}
}

