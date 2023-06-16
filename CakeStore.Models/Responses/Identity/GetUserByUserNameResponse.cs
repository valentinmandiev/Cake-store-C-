namespace CakeStore.Models.Responses.Identity
{
    public class GetUserByUserNameResponse
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
