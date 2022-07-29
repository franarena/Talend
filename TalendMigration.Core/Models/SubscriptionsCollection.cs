namespace TalendMigration.Core.Models;
public class SubscriptionsCollection<T>
{
    public IEnumerable<T> Subscriptions { get; set; }
    public SubscriptionsCollection(IEnumerable<T> lista)
    {
        Subscriptions = lista;
    }
}