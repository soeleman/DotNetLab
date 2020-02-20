namespace Soeleman
{
    public sealed class DataRepository
    {
        public DataRepository(
            IDataRepository repository)
        {
            this.Repo = repository;
        }

        public IDataRepository Repo { get; }

        public DataResult GetData(
            int pageSize,
            int currentPage)
        {
            return this.Repo.GetData(
                pageSize, 
                currentPage);
        }

        public void AddNewData()
        {
            this.Repo.AddNewData();
        }
    }
}
