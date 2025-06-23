using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Domain.DomainModels
{
    public class Country : BaseEntity
    {
        public string? Name { get; set; }
        public string? CurrencyName { get; set; }
        public string? Code { get; set; }
    }
}
