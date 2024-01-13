using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingWiki_Model.Models
{
    public class Fluent_Publisher
    {
        public int PublisherId { get; set; }
        [Required]
        public string PublisherName { get; set;}
        public string Location { get; set; }

        // Navigation Property
        public List<Fluent_Book> Books { get; set; }
    }
}
