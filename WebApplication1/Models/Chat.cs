using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using whenAppModel.Models;

namespace WebApplication1.Models
{
    public class Chat
    {
        public Chat()
        {
            this.Person1 = string.Empty;
            this.Person2 = string.Empty;
            ICollection<Message> Messages = new List<Message>();
            Last = string.Empty;
            LastDate = string.Empty;
        }
        public Chat(string p1, string p2)
        {
            this.Person1 = p1;
            this.Person2 = p2;
            ICollection<Message> Messages = new List<Message>();
            Last =string.Empty;
            LastDate = string.Empty;    
        }
        [Key]
        [ForeignKey("User")]
        [Column(Order = 0)]
        public string Person1 { get; set; }

        [Key]
        [ForeignKey("User")]
        [Column(Order = 1)]
        public string Person2 { get; set; }

        public ICollection<Message> Messages { get; set; } = new List<Message>();

        public string Last { get; set; }

        public string LastDate { get; set; }

        public bool Compare(string p1, string p2)
        {
            if(p1 == Person1 && p2 == Person2)
            {
                return true;
            }

            if(p1 == Person2 && p2 == Person1)
            {
                return true;
            }

            return false;
        }
    }
}
