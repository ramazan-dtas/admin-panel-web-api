using System;
using System.ComponentModel.DataAnnotations;

namespace skolesystem.DTOs.Classe.Request
{
	public class UpdateClasse
	{
        [Required]
        [StringLength(255, ErrorMessage = "Max string length is 255")]
        [MinLength(1, ErrorMessage = "Min string length is 1")]
        public string className { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Max string length is 255")]
        [MinLength(1, ErrorMessage = "Min string length is 1")]
        public string location { get; set; }
    }
}

