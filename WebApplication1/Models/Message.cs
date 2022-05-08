using System.ComponentModel.DataAnnotations;

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

        public virtual User From { get; set; }
        
        public virtual User To { get; set; }
        
        public DateTime Date { get; set; }
        
        public Types Type { get; set; }    
        
        public string Data { get; set; }    

    }
}
