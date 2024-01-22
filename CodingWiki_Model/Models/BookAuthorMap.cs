using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingWiki_Model.Models
{
    public class BookAuthorMap
    {
        // Foreign Keys
        [ForeignKey("Book")]
        public int BookId { get; set; }
        [ForeignKey("Author")]
        public int AuthorId { get; set; }

        // Navigation Properties
        public virtual Book Book { get; set; }
        public virtual Author Author { get; set; }
    }
}
