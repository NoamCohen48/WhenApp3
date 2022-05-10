using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace whenAppModel.Models
{
    public class User
    {
        public User()
        {
            Username = string.Empty;
            Password = string.Empty;
            Nickname = string.Empty;
            Avatar = string.Empty;
        }

        public User(string username, string password, string nickname, string avatar)
        {
            Username = username;
            Password = password;
            Nickname = nickname;
            Avatar = avatar;
        }

        [Required] 
        [Key]
        [JsonPropertyName("id")]
        public string Username { get; set; }

        [Required]
        [JsonPropertyName("name")]
        public string Nickname { get; set; }

        [RegularExpression("^(? !.* )(?=.*'\'d)(?=.*[A - Z]).{8,}$")]
        [Required]
        [JsonIgnore]
        public string Password { get; set; }

        [Required]
        [JsonIgnore]
        public string Avatar { get; set; }

        [JsonPropertyName("server")]
        public string Server { get; set; } = "thisServer";

    }
}
