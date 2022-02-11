namespace FileTracker.Common.Interfaces
{
    public interface IFilterManager : IFilter
    {
        IFilterManager AddFilter(IFilter filter);

        IFilterManager ClearFilters();
    }
}