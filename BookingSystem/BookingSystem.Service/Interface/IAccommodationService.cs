using BookingSystem.Domain.DomainModels;
using BookingSystem.Domain.DomainModels.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Service.Interface
{
    public interface IAccommodationService
    {
        List<Accommodation> GetAll();
        Accommodation? GetById(Guid id);
        Accommodation Insert(Accommodation accommodation);
        Accommodation Update(Accommodation accommodation);
        Accommodation DeleteById(Guid id);
        PaginatedList<Accommodation> GetPaginated(int pageIndex, int pageSize, Guid? cityId = null);
    }
}
