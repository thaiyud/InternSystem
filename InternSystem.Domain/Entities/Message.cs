using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Domain.Entities
{
    [Table("Message")]
    public class Message
    {
        [Key]
        [StringLength(36)] 
        public string Id { get; set; }
        [Required]
        public string IdSender { get; set; }
        [ForeignKey("IdSender")]
        public virtual AspNetUser Sender { get; set; }
        [Required]
        public string IdReceiver { get; set; }
        [ForeignKey("IdReceiver")]
        public virtual AspNetUser Receiver { get; set; }
        public DateTime Timestamp { get; set; }
        [Required]
        public string MessageText { get; set; }
        
    }
}
