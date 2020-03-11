using System.Collections.Generic;
using TailBlazer.Views.Searching;

namespace TailBlazer.Views.Tail
{
    public sealed class TailViewState
    {
        public static readonly TailViewState Empty = new TailViewState();

        public string FileName { get; }
        public string DisplayName { get; }
        public bool AutoTail { get; }
        public string SelectedSearch { get; }

        public IEnumerable<SearchState> SearchItems { get; }

        public TailViewState(string fileName, string displayName, bool autoTail, string selectedSearch, IEnumerable<SearchState> searchItems)
        {
            FileName = fileName;
            DisplayName = displayName;
            AutoTail = autoTail;
            SelectedSearch = selectedSearch;
            SearchItems = searchItems;
        }

        private TailViewState()
        {
            
        }
    }
}