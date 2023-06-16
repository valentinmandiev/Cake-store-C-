namespace CakeStore.Models.Request.Cake
{
    public class AddCakeRequest
    {
        public string Name { get; set; }

        public Guid FactoryId { get; set; }
    }
}
