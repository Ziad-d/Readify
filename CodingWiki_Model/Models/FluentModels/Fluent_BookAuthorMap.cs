using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingWiki_Model.Models
{
    public class Fluent_BookAuthorMap
    {
        // Foreign Keys
        public int BookId { get; set; }
        public int AuthorId { get; set; }

        // Navigation Properties
        public Fluent_Book Book { get; set; }
        public Fluent_Author Author { get; set; }
    }
}
