namespace WyyMusicConvertGui
{
    public class GlobalVars : ObservableObject
    {
        public static GlobalVars Configs = new();

        public bool DownloadCoverImage
        {
            get;
            set;
        } = false;

        public bool DownloadLyric
        {
            get;
            set;
        } = false;

        public bool OverwriteFile
        {
            get;
            set;
        } = false;

        public bool DeleteOldFile
        {
            get;
            set;
        } = false;

        private GlobalVars() { }

    }
}