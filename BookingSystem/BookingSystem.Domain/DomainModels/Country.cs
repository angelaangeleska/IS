using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Domain.DomainModels
{
    public class Country : BaseEntity
    {
        public string? Name { get; set; }
        public string? OfficialName { get; set; }
        public string? Capital { get; set; }
        public string? FlagUrl { get; set; }
        public string? CurrencyName { get; set; }
        public string? CurrencySymbol { get; set; }
        public string? Language { get; set; }
        public virtual ICollection<City>? Cities { get; set; }
    }
}
