using Ardalis.GuardClauses;
using Compliance.Domain.Events;
using Shared.Domain.Abstractions;
using Shared.Domain.Interfaces;

namespace Compliance.Domain.Aggregates.BankComplinceAggregate
{
    public class BankCompliance : BaseEntity, IAggregateRoot
    {
        private BankCompliance(string nationalCode,
                               bool centralBankCreditCheckPassed,
                               bool civilRegistryVerified,
                               bool policeClearancePassed)
        {
            Guard.Against.NullOrEmpty(nationalCode, nameof(nationalCode));

            NationalCode = nationalCode;
            CentralBankCreditCheckPassed = centralBankCreditCheckPassed;
            CivilRegistryVerified = civilRegistryVerified;
            PoliceClearancePassed = policeClearancePassed;
        }

#pragma warning disable CS8618 // Required by Entity Framework
        private BankCompliance() { }

        public string NationalCode { get; private set; }
        public bool CentralBankCreditCheckPassed { get; private set; }
        public bool CivilRegistryVerified { get; private set; }
        public bool PoliceClearancePassed { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.Now;

        public static BankCompliance Create(string nationalCode,
                                            bool centralBankCreditCheckPassed,
                                            bool civilRegistryVerified,
                                            bool policeClearancePassed,
                                            Guid userId)
        {
            var authentication = new BankCompliance(nationalCode,
                                                    centralBankCreditCheckPassed,
                                                    civilRegistryVerified,
                                                    policeClearancePassed);
            authentication.AddDomainEvent(new BankComplianceCreatedEvent(authentication, userId));
            return authentication;
        }
    }
}
