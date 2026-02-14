using Compliance.Domain.Aggregates.BankComplinceAggregate;
using Shared.Application.Interfaces;

namespace Compliance.Application.Queries
{
    public class GetComplianceExistsQueryHandler : IQueryHandler<GetComplianceExistsQuery, bool>
    {
        private readonly IComplianceRepository _complianceRepository;

        public GetComplianceExistsQueryHandler(IComplianceRepository complianceRepository)
        {
            _complianceRepository = complianceRepository;
        }

        public async Task<bool> Handle(GetComplianceExistsQuery request, CancellationToken cancellationToken)
        {
            return await _complianceRepository.ExistsByNationalCodeAsync(request.NationalCode, cancellationToken);
        }
    }
}
