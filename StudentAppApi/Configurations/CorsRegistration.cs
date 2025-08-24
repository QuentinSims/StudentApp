namespace StudentAppApi.Configurations
{
    public static class CorsRegistration
    {

        public static void ConfigureCORS(this WebApplicationBuilder builder)
        {
            var config = builder.Configuration.GetSection("CORSConfiguration:ValidAudiences");

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(CorsConfiguration.PolicyName, builder =>
                builder.WithOrigins(config.Get<List<string>>().ToArray())
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }
    }
    public class CorsConfiguration
    {
        public static string PolicyName = "corsPolicy";

        internal string[] ValidAudiences { get; set; }
    }
}
