namespace Domain
{
    public class IgnoredTransactionDto : TransactionDto
    {
        public string TextLine { get; set; }

        public IgnoredTransactionDto(string textLine)
        {
            TextLine = textLine;
        }
    }
}