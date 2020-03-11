using System.IO;
using DynamicData.Binding;

namespace TailBlazer.Infrastucture
{
    public class FileHeader: AbstractNotifyPropertyChanged
    {
        private readonly FileInfo _info;
        private  string _displayName;

        public string FullName => _info.FullName;

        public FileHeader(FileInfo info)
            : this(info, info.Name)
        {
        }

        public FileHeader(FileInfo info, string displayName)
        {
            _info = info;
            _displayName = displayName;
        }

        public string DisplayName
        {
            get => _displayName;
            set => SetAndRaise(ref _displayName,value);
        }


    }
}