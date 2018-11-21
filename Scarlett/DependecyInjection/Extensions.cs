namespace Scarlett.DependecyInjection
{
    using Financial;
    using Microsoft.Extensions.DependencyInjection;

    public static class Extensions
    {
        public static IServiceCollection AddScarlett(this IServiceCollection service)
        {
            return service.AddScoped<AccountService>();
        }
    }
}