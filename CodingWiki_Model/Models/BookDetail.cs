using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingWiki_Model.Models
{
    public class BookDetail
    {
        [Key]
        public int BookDetail_Id { get; set; }
        public int NumberOfChapters { get; set; }
        public int NumberOfPages { get; set; }
        public int Weight { get; set; }

        // Foreign Key
        [ForeignKey("Book")]
        public int BookId { get; set; }

        // Navigation Property
        public Book Book { get; set; }
    }
}
