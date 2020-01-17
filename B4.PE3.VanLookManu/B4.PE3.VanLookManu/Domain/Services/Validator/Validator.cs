using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using B4.PE3.VanLookManu.Domain.Models;

namespace B4.PE3.VanLookManu.Domain.Services.Validator
{
    public class Validator
    {
        public List<ValidationResult> ValidatorLocation(LocationUser loc)
        {
            List<ValidationResult> results = new List<ValidationResult>();

            if (loc.Name == string.Empty || loc.Name == "") { results.Add(new ValidationResult("name is empty please fill in a name")); }

            return results;
        }

        public List<ValidationResult> ValidatorUser(User user)
        {
            List<ValidationResult> results = new List<ValidationResult>();

            if (user.Name == string.Empty || user.Name == "") { results.Add(new ValidationResult("name is empty please fill in a name")); }


            return results;
        }

    }
}
