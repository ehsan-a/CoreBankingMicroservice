using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;

namespace Identity.API.Models
{
    public class UserApplication : IdentityUser<Guid>
    {
        public UserApplication(Guid customerId)
        {
            Guard.Against.NullOrEmpty(customerId, nameof(customerId));

            CustomerId = customerId;
        }

#pragma warning disable CS8618 // Required by Entity Framework
        private UserApplication() { }

        public Guid CustomerId { get; private set; }
    }
}
