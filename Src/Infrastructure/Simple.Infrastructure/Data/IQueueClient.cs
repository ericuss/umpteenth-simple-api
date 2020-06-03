namespace Simple.Infrastructure.Data
{
    using System.Threading.Tasks;

    public interface IQueueClient<TClient>
    {
        IQueueClient<TClient> CreateClient();

        Task PublishAsync(string queue, string message);
    }
}
