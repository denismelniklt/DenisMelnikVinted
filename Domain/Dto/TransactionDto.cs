namespace Domain
{
    public class TransactionDto
    {
        public string Id { get; set; }
        public string Date { get; set; }
        public PackageDto Package { get; set; }
    }
}
