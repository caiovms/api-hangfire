using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Hangfire.Mongo;
using Hangfire;
using api.hangfire.domain;

namespace api_hangfire
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var migrationOptions = new MongoMigrationOptions
            {
                Strategy = MongoMigrationStrategy.Migrate,
                BackupStrategy = MongoBackupStrategy.Collections
            };
            var storageOptions = new MongoStorageOptions
            {
                MigrationOptions = migrationOptions
            };

            services.AddHangfire(configuration => configuration.UseMongoStorage("mongodb://localhost:27017", "Hangfire", storageOptions));

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHangfireDashboard();
            app.UseHangfireServer();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Exemplo Hangfire - github.com/caiovms");
            });

            InitProcess();
        }

        private void InitProcess()
        {
            var division = new Division();
            RecurringJob.AddOrUpdate(() => division.DivisionRandom(), Cron.Minutely());
        }
    }
}
