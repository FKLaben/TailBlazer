namespace TailBlazer.Views.WindowManagement
{
    using System.IO;

    public class FileInfoExtension
    {
        public FileInfoExtension(FileInfo fileInfo, string displayName)
        {
            FileInfo = fileInfo;
            DisplayName = displayName;
        }

        public FileInfo FileInfo { get; private set; }

        public string DisplayName { get; private set; }
    }
}
