namespace CakeStore.Models.Requests.Cake
{
    public class CakeResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid FactoryId { get; set; }
    }
}
