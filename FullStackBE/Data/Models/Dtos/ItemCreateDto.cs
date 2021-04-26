using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FullStackBE.Models.Dtos
{
    public class ItemCreateDto
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
