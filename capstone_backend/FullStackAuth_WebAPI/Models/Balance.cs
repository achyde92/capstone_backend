using System;
using System.ComponentModel.DataAnnotations;

namespace FullStackAuth_WebAPI.Models
{
	public class Balance
	{
        [Key]
        public string UserId { get; set; }

        public decimal Amount { get; set; }
    }
} 

