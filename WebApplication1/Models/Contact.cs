using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace whenAppModel.Models
{
    public class Contact
    {
        public Contact()
        {
            Id = string.Empty;
            Name = string.Empty;
            Server = string.Empty;
            Last = string.Empty;
            LastDate = string.Empty;
            User = new User();
        }

        public Contact(string id, string name, string server, string last, string lastdate, User user)
        {
            Id=id;
            Name=name;
            Server=server;
            Last=last;
            LastDate=lastdate;
            User=user;
        }

        [Required]
        [Key]
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [Required]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [Required]
        [JsonPropertyName("server")]
        public string Server { get; set; }

        [JsonPropertyName("last")]
        public string Last { get; set; }

        [JsonPropertyName("lastdate")]
        public string LastDate { get; set; }

        [Required]
        [JsonPropertyName("user")]
        public User User { get; set; }
    }
}
