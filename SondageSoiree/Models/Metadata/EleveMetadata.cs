using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SondageSoiree.Models.Metadata
{
    public class EleveMetadata
    {
        [Required]
        [StringLength(100)]
        public string Nom;

        [Required]
        [StringLength(150)]
        public string Prenom;

        [Required]
        [MinLength(6)]
        public string Password;
    }
}