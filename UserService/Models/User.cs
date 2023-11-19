using Microsoft.AspNetCore.Http.HttpResults;

namespace UserService.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        //public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
        /*public UserCreatedEvent ToUserCreatedEvent()
        {
            return new UserCreatedEvent
            {
                Id = Id,
                Name = Name,
                CreatedAt = CreatedAt
            };
        }*/
    }

    /*public record UserCreatedEvent
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public DateTime CreatedAt { get; init; }
    }*/
}
