namespace CakeStore.Models.Request.Cake
{
    public class UpdateCakeRequest
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid FactoryId { get; set; }
    }
}
