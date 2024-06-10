namespace ITT.CRM.App
{
    public class DatabaseManagement
    {
        private readonly DataContext _context;
        public DatabaseManagement(DataContext context)
        {
            _context = context;
        }

        async public Task InitDatabase()
        {
            await _context.Database.EnsureCreatedAsync();
        }
    }
}
