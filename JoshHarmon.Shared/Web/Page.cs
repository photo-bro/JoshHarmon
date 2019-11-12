using System;
namespace JoshHarmon.Shared.Web
{
    public class Page
    {
        public Page(DateTime from, DateTime to, int limit, int count)
        {
            From = from;
            To = to;
            Limit = limit;
            Count = count;
        }

        public DateTime From { get; }

        public DateTime To { get; }

        public int Limit { get; }

        public int Count { get; }
    }
}
