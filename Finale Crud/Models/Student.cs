using Microsoft.AspNetCore.Mvc.TagHelpers;
using System.ComponentModel.DataAnnotations;

namespace Finale_Crud.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public int Age { get; set; }
        public DateTime? Birthdate { get; set; }
        public int cid { get; set; } 
        public string? cname { get; set; } 

        public int sid { get; set; }
    }
}
