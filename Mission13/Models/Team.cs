using System;
using System.ComponentModel.DataAnnotations;

namespace Mission13.Models
{
    public class Team
    {
        [Key]
        [Required]
        public int TeamID { get; set; }
        [Required]
        public string TeamName { get; set; }
        [Required]
        public int CaptainID { get; set; }
    }
}
