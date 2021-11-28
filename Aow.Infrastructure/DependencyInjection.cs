using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aow.Infrastructure
{
    public static class DependencyInjection
    {
        //    public class PasswordlessLoginTotpTokenProvider<TUser> : TotpSecurityStampBasedTokenProvider<TUser>
        //where TUser : class
        //    {
        //        public override Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<TUser> manager, TUser user)
        //        {
        //            return Task.FromResult(false);
        //        }

        //        public override async Task<string> GetUserModifierAsync(string purpose, UserManager<TUser> manager, TUser user)
        //        {
        //            var email = await manager.GetEmailAsync(user);
        //            return "PasswordlessLogin:" + purpose + ":" + email;
        //        }
        //    }
        //    public static IdentityBuilder AddPasswordlessLoginTokenProvider(this IdentityBuilder builder)
        //    {
        //        var userType = builder.UserType;
        //        var provider = typeof(PasswordlessLoginTotpTokenProvider<>).MakeGenericType(userType);
        //        return builder.AddTokenProvider("PasswordlessLoginProvider", provider);
        //    }

        //public static IdentityBuilder AddDefaultTokenProvider(this IdentityBuilder builder)
        //{
        //    var userType = builder.UserType;
        //    // var dataProtectionProviderType = typeof(DefaultPersonalDataProtector<>).MakeGenericType(userType);
        //    var phoneNumberProviderType = typeof(PhoneNumberTokenProvider<>).MakeGenericType(userType);
        //    var emailTokenProviderType = typeof(EmailTokenProvider<>).MakeGenericType(userType);
        //    return builder.AddTokenProvider(TokenOptions.DefaultEmailProvider, emailTokenProviderType)
        //        .AddTokenProvider(TokenOptions.DefaultPhoneProvider, phoneNumberProviderType);
        //}

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<AowContext>(options =>
            //    options.UseSqlServer(
            //        Configuration.GetConnectionString("DefaultConnection"),
            //        b => b.MigrationsAssembly(typeof(AowContext).Assembly.FullName)));


            services.AddDbContextPool<AowContext>(options =>
              options.UseMySQL(
                  configuration.GetConnectionString("DefaultConnection"),
                  b => b.MigrationsAssembly(typeof(AowContext).Assembly.FullName)));         

            //services.AddAuthentication()
            //    .AddIdentityServerJwt();

            return services;
        }
    }
}
