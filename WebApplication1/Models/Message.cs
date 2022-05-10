 using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;

namespace whenAppModel.Models
{
    public class Message
    {
        public enum Types
        {
            Text,
            Image,
            Video,
            Record
        }

        [Key]
        public int Id { get; set; }

        public Chat Chat { get; set; }

        public DateTime Date { get; set; }
        
        public Types Type { get; set; }    
        
        public string Data { get; set; }

        public Message()
        {
        }

        public Message(int id, Chat chat, DateTime date, Types type, string data)
        {
            this.Id = id;
            this.Chat = chat;
            this.Date = date;
            this.Type = type;
            this.Data = data;
        }
    }
}
