namespace CakeStore.Models.Request.Factory
{
    public class UpdateFactoryRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int EIK { get; set; }
        public string Address { get; set; }
    }
}
