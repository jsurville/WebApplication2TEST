﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication2TEST.Models
{
	public class Client : Personne
	{
        //[Required]
        //[Index(IsUnique = true)]
        //[StringLength(60, MinimumLength = 5, ErrorMessage = "L'adresse mail doit avoir de 5 a 60 caracteres")]
        //public string Email { get; set; }
        public  string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }

        public ICollection<DossierReservation> DossiersReservations { get; set; }
	}
}