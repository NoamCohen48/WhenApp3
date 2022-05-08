using System.ComponentModel.DataAnnotations;

namespace whenAppModel.Models
{
    public class Message
    {

        public Message(int id, User from, User to, DateTime date, Types type, string data)
        {
            this.Id = id;
            this.From = from;
            this.To = to;
            this.Date = date;
            this.Type = type;
            this.Data = data;
        }

        public enum Types
        {
            Text,
            Image,
            Video,
            Record
        }

        [Key]
        public int Id { get; set; }

        public virtual User From { get; set; }
        
        public virtual User To { get; set; }
        
        public DateTime Date { get; set; }
        
        public Types Type { get; set; }    
        
        public string Data { get; set; }    

    }
}
