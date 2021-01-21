using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;
using System.ComponentModel.DataAnnotations;


namespace Vidly.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public MembershipTypeDto MembershipType { get; set; }

          //     [Min18YerasIfAMember]
        public DateTime? BirthDate { get; set; }

        public bool IsSubscriebedToNewsLetter { get; set; }

        public byte MembershipTypeID { get; set; }
    }
}