namespace Simple.Infrastructure.Data
{
    using System.Threading.Tasks;

    public interface IBlobClient<TClient>
    {
        IBlobClient<TClient> CreateClient();

        Task<string> GetAsync(string documentPath);
    }
}
