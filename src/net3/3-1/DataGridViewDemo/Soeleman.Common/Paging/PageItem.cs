namespace Soeleman
{
    public sealed class PageItem
    {
        public readonly int PageSize;

        public readonly int CurrentPage;

        public PageItem(
            int limit,
            int offset)
        {
            this.PageSize = limit;
            this.CurrentPage = offset;
        }
    }
}