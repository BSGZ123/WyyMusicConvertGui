using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //设定为单一文件来源模式(全局)
        //若未选择，则默认为单一文件模式
        public string SelectModeString { get; set; } = "Single";



        public MainWindow()
        {
            InitializeComponent();
        }


        private void SelectMode(object sender, RoutedEventArgs e)
        {
           RadioButton radioButton = (RadioButton)sender;
            if (radioButton.IsChecked == true )
            {
                SelectModeString=radioButton.Name;
                MessageBox.Show("选择了"+radioButton.Name+"按钮   识别字符串"+SelectModeString);
            }
        }

        private  async void Button_Click(object sender, RoutedEventArgs e)
        {
            var picker = new FileOpenPicker();

            /*Process.GetCurrentProcess().MainWindowHandle
             * 在一个非 UWP 或 WinUI 的桌面应用程序中，当你需要从当前进程的主窗口（必须是唯一的且应用程序的 MainWindow 已经初始化）获取窗口句柄时使用。
             * 
             * WindowNative.GetWindowHandle(this)
             * 明确知道 this的对象是一个有效的窗口或视图，并希望从其直接获取窗口句柄时使用。
             */
            //var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);//失效...只能在WinUI/UWP中使用
            var hwnd = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hwnd);

            picker.ViewMode = PickerViewMode.Thumbnail;//缩略图
            picker.SuggestedStartLocation = PickerLocationId.MusicLibrary;//默认个人文档音乐文件夹
            picker.FileTypeFilter.Add("*");

            if (SelectModeString.Equals("Single"))
            {
                var result = await picker.PickSingleFileAsync();
                if (result != null) {
                    //List<string> files = [result.Path];
                    MessageBox.Show("+++++" + result.Path + "  ++++");
                }
            }
        }
    }
}