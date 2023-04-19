using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infraestructure.Data;

public class SecurityContextSeed
{
    public static async Task SeedRolsAsync(SecurityContext context, ILoggerFactory loggerFactory)
    {
        try
        {
            if (!context.Roles.Any())
            {
                var roles = new List<Rol>()
                        {
                            new Rol{Id=1, Name="Administrador"},
                            new Rol{Id=2, Name="Empleado"},
                        };
                context.Roles.AddRange(roles);
                await context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<SecurityContextSeed>();
            logger.LogError(ex.Message);
        }
    }
}
