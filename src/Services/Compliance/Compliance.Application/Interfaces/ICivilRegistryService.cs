
using Compliance.Application.DTOs;

namespace Compliance.Application.Interfaces
{
    public interface ICivilRegistryService
    {
        Task<CivilRegistryResponseDto?> GetPersonInfoAsync(string nationalCode);
    }
}
