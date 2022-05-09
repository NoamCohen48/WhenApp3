using System.ComponentModel.DataAnnotations;
using whenAppModel.Models;

namespace WebApplication1.Models
{
    public class Chat
    {
        [Key]
        public User Person1 { get; set; }

        [Key]
        public User Person2 { get; set; }

        public ICollection<Message> Messages { get; set; } = new List<Message>();

        public bool Compare(string p1, string p2)
        {
            if(p1 == Person1.Username && p2 == Person2.Username)
            {
                return true;
            }

            if(p1 == Person2.Username && p2 == Person1.Username)
            {
                return true;
            }

            return false;
        }
    }
}
