using PlatformService.Data;

namespace PlatformService.PrepDb;

public static class PrepDb
{
    public static void PrepPopulation(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
        SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
    }

    private static void SeedData(AppDbContext context)
    {
        if(!context.Platforms.Any())
        {
            Console.WriteLine("..Seeting up the database");
            context.Platforms.AddRange(
                new Models.Platform(){Name = "Dot Net",Publisher= "Microsoft",Cost="Free"},
                new Models.Platform(){Name = "SQL Servier Express",Publisher= "Microsoft",Cost="Free"},
                new Models.Platform(){Name = "KuberNates",Publisher= "Cloud Native Computer Foundation", Cost="Free"}
            );

            context.SaveChanges();
        }
        else
        {
            Console.WriteLine("we already have data");
        }
    }
}