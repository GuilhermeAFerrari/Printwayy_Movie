namespace PrintWayyMovie_API
{
    public class StartDatabase
    {
        public async Task VerificaExistenciaDatabase(IServiceProvider services, ILogger logger, string connectionString)
        {
            logger.LogInformation("Banco de dados existe na string de conexão : '{connectionString}'", connectionString);
            using var db = services.CreateScope().ServiceProvider.GetRequiredService<DataContext>();
            await db.Database.EnsureCreatedAsync();
            await db.Database.MigrateAsync();
        }
    }
}
