namespace Domain
{
    public class IgnoredTransaction : Transaction
    {
        public string TextLine { get; set; }

        public IgnoredTransaction(string textLine)
        {
            TextLine = textLine;
        }
    }
}