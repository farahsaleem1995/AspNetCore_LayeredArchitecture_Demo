using LayeredArch.Core.Domain.Models;
using LayeredArch.Core.Domain.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayeredArch.Core.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<DomainUser>> GetUsersAsync(bool isAdmin = false);
        Task<DomainUser> FindByIdAsync(string userId, bool isAdmin = false);
        Task<DomainUser> FindByNameAsync(string userId, bool isAdmin = false);
        Task<DomainUser> FindByConfirmedPhoneNumberAsync(string userId);
        Task<DomainUser> FindByConfirmedEmailAsync(string userId);
    }
}
