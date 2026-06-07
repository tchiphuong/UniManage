using Hangfire.Dashboard;

namespace UniManage.Worker.Filters
{
    public class AllowAllAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            // WARNING: This allows access from ANYWHERE. 
            // In Production, you MUST check context.GetHttpContext().User.Identity.IsAuthenticated
            // or verify specific claims/roles.
            return true;
        }
    }
}
