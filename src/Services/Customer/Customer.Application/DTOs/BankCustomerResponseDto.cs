namespace Customer.Application.DTOs
{
    public class BankCustomerResponseDto
    {
        public Guid Id { get; set; }
        public string NationalCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
