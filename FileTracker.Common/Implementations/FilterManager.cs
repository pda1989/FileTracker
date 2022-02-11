using FileTracker.Common.Interfaces;
using System.Collections.Generic;

namespace FileTracker.Common.Implementations
{
    public class FilterManager : IFilterManager
    {
        private readonly HashSet<IFilter> _filters = new HashSet<IFilter>();
        private readonly ILogger _logger;

        public FilterManager(ILogger logger)
        {
            _logger = logger;
        }

        public IFilterManager AddFilter(IFilter filter)
        {
            if (_filters.Add(filter))
                _logger.Info($"The '{filter.GetType().Name}' has been added");
            return this;
        }

        public IFilterManager ClearFilters()
        {
            _filters.Clear();
            _logger.Info("All filteres have been removed");
            return this;
        }

        public bool IsMatch(string text)
        {
            bool result = true;
            foreach (IFilter filter in _filters)
            {
                result &= filter.IsMatch(text);
            }
            return result;
        }
    }
}