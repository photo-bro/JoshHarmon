using System;
namespace JoshHarmon.Shared.Web
{
    public class Page
    {
        public Page(DateTime from, DateTime to, int offset, int total)
        {
            From = from;
            To = to;
            Offset = offset;
            Total = total;
        }

        public DateTime From { get; }

        public DateTime To { get; }

        public int Offset { get; }

        public int Total { get; }
    }
}
