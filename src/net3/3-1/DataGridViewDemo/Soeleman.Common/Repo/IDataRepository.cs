namespace Soeleman
{
    public interface IDataRepository
    {
        DataResult GetData(int pageSize, int currentPage);

        void AddNewData();
    }
}