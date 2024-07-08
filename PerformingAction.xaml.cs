using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Windows.Storage.Pickers;

namespace WyyMusicConvertGui
{
    /// <summary>
    /// PerformingAction.xaml 的交互逻辑
    /// </summary>
    public partial class PerformingAction : Page
    {
        public ObservableCollection<MusicDescriptor> MusicListItems { get; set; } = new();
        private List<string> Files;
        private bool isFinishWork = false;


        public PerformingAction(List<string> files)
        {
            InitializeComponent();
            this.DataContext = this;
            Files = files;
        }

        private void ProcessButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("请选择保存目录，开始转换啦啦啦~~~");
            ProcessFiles();


        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateFileList();
        }

        private Task UpdateFileList()
        {
            MusicListItems.Clear();
            return Task.Run(() =>
            {
                GC.Collect();
                Files.ForEach(f =>
                {
                    if (NeteaseCryptoMusic.CheckFile(f) == false)
                        return;
                    MusicListItems.Add(new MusicDescriptor(f));
                });

            }
            );
        }

        private async void ProcessFiles()
        {
            var fpicker = new FolderPicker();
            var hwnd = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
            WinRT.Interop.InitializeWithWindow.Initialize(fpicker, hwnd);

            fpicker.SuggestedStartLocation = PickerLocationId.MusicLibrary;
            fpicker.FileTypeFilter.Add("*");

            var result = await fpicker.PickSingleFolderAsync();
            string outdir;
            if (result != null) { outdir = result.Path; }
            else
            {
                MessageBox.Show("提示","请选择保存目录");
                return;
            }

            int totalItems = MusicListItems.Count;
            int processedItems = 0;

            await Task.Run(() =>
            {
                foreach (var item in MusicListItems)
                {
                    item.Status = "Processing";

                    if (item.CryptoMusic.WriteDecryptMusic(outdir))
                    {
                        item.Status = "OK";
                        MessageBox.Show("文件转换完成！！！");
                    }
                    else
                    {
                        item.Status = "Fail";
                    }
                    processedItems++;
                    double progress = (double)processedItems / totalItems;

                    //怕调用太快被服务器拉黑了
                    if ((GlobalVars.Configs.DownloadCoverImage && item.CryptoMusic.IsEnabedImages) || GlobalVars.Configs.DownloadLyric)
                    {
                        Thread.Sleep(1000);
                    }
                }
            });

            isFinishWork = true;
        }



        private string FileSizeToString(long len)
        {
            long fileSizeInBytes = len;
            double fileSizeInKB = fileSizeInBytes / 1024.0; // 字节转换为千字节（KB）
            double fileSizeInMB = fileSizeInKB / 1024.0; // 千字节转换为兆字节（MB）
            string FileSize = $"{Math.Round(fileSizeInMB, 2)} MB";
            return FileSize;
        }
    }
}
