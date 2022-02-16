using SondageSoiree.Models.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SondageSoiree.Models.Entity
{
    [MetadataType(typeof(EleveMetadata))]
    public partial class Eleve : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var valid = new List<ValidationResult>();
            if (string.IsNullOrWhiteSpace(Password))
                valid.Add(new ValidationResult("Vous devez remplir le mot de passe"));
            return valid;
        }
    }
}