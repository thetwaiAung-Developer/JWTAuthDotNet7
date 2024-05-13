
//namespace JWTAuthDotNet7.Middleware
//{
//    public class AuthenticationMiddleWare : IMiddleware
//    {
//        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
//        {
//            var path = context.Request.Path;

//            var ignorePathList = new[]
//            {
//                "/iapi/",
//                "/iapi/Login"
//            };

//            if (ignorePathList.Any(x => x.Equals(path, StringComparison.OrdinalIgnoreCase)))
//            {
//                await next.Invoke(context);
//                return;
//            }

//            context.Response.Redirect("/");
//            return;
//        }
//    }
//}
