using System;

namespace Domain
{
    public class Transaction : Base
    {
        public DateTime Date { get; set; }
        public Package Package { get; set; } = new Package();
    }
}