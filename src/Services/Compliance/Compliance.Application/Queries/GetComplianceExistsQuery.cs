using Shared.Application.Interfaces;

namespace Compliance.Application.Queries
{
    public class GetComplianceExistsQuery : IQuery<bool>
    {
        public string NationalCode { get; set; }
    }
}
