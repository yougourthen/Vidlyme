using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Min18YerasIfAMember : ValidationAttribute 
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer) validationContext.ObjectInstance;

            if (customer.MembershipTypeID == MembershipType.Unknown || 
                customer.MembershipTypeID == MembershipType.PayAsYouGo)
                return ValidationResult.Success;

            if (customer.BirthDate == null)
                return new ValidationResult("BirthDate is Requiered");

             var age = DateTime.Today.Year - customer.BirthDate.Value.Year;

           return (age >= 18)? ValidationResult.Success: 
                new ValidationResult("Age Must Be Greater than 18 Years Old To Be A Member");
                

        }
    }
}