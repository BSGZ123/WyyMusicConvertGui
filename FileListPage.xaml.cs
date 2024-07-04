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
        public MusicItemList MusicItems { get; set; }=new MusicItemList();
        private int SelectedFileCount = 0;

        public FileListPage(List<string> files)
        {
            InitializeComponent();
            Files=files;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void MusicItemListView_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
