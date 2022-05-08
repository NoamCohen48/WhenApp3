using System.ComponentModel.DataAnnotations;
namespace whenAppModel.Models
{
    public class User
    {
        private ICollection<User> users;

        public User(string username, string password, string nickname, string avatar)
        {
            Username = username;
            Password = password;
            Nickname = nickname;
            Avatar = avatar;

            users = new List<User>();
        }

        [Required] 
        [Key]
        public string Username { get; set; }

        [Required]
        public string Nickname { get; set; }

        [RegularExpression("^(? !.* )(?=.*'\'d)(?=.*[A - Z]).{8,}$")]
        [Required]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Url)]
        public string Avatar { get; set; }

        public virtual ICollection<User>? Contacts { get; set; }


    }
}
