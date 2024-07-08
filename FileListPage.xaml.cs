using System;
using System.Collections.Generic;
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

namespace WyyMusicConvertGui
{
    /// <summary>
    /// FileListPage.xaml 的交互逻辑
    /// </summary>
    public partial class FileListPage : Page
    {

        private List<string> Files;
        public MusicItemList MusicItems { get; set; }=new ();
        private int SelectedFileCount = 0;

        public FileListPage(List<string> files)
        {
            InitializeComponent();
            this.DataContext = this;
            Files =files;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateFileList();
            MessageBox.Show("选中文件数量为"+Files.Count+" 份");
        }

        private Task UpdateFileList()
        {
           MusicItems.MusicDescriptorList.Clear();
            return Task.Run(() =>
            {
                GC.Collect();
                Files.ForEach(f =>
                {
                    if (NeteaseCryptoMusic.CheckFile(f) == false)
                        return;
                    MusicItems.MusicDescriptorList.Add(new MusicDescriptor(f));
                });

            }
            );
        }

        private void MusicItemListView_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("暂时没用哦");
        }

        private void NextStepButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationWindow navigationWindow = new NavigationWindow();
            //这是被选中转换的ncm文件
            var newFileList = MusicItems.MusicDescriptorList.Where(f => f.IsItemChecked).Select( f=> f.FileName).ToList();
            //foreach (var file in newFileList) { MessageBox.Show(file); }
            navigationWindow.Content = new PerformingAction(newFileList);
            navigationWindow.Show();


        }

        private void MusicItemListView_MouseMove(object sender, MouseEventArgs e)
        {
            //var grid = sender as Grid;
            //var descriptor = grid.DataContext as MusicDescriptor;
            //descriptor.IsItemChecked = !descriptor.IsItemChecked;

        }

    }
}
