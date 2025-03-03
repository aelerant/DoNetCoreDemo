using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfAppDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //SetFlcommandBinding();
        }

        /// <summary>
        /// 双击组件创建方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Click!");
        }

#if false
        private void MouseEnterEixtArea(object sender, RoutedEventArgs e)
        {
            statusBarText.Text = "Exit the Application";
        }

        private void MouseLeaveEixArea(object sender, RoutedEventArgs e)
        {
            statusBarText.Text = "Ready";
        }

        private void FileExit_Click(object sender, RoutedEventArgs e)
        {
            //关闭窗口
            this.Close();
        }

        private void MouseEnterToolsHintsArea(object sender, RoutedEventArgs e)
        {
            statusBarText.Text = "Show Spelling Suggestions";
        }

        private void MouseLeaveToolsHintsArea(object sender, RoutedEventArgs e)
        {
            statusBarText.Text = "Ready";
        }

        private void ToolsSpellingHints_Click(object sender, RoutedEventArgs e)
        {
            string spellingHints = string.Empty;

            //获取当前位置的拼音错误
            SpellingError error = textData.GetSpellingError(textData.CaretIndex);
            if (error != null)
            {
                //构建更改建议的字符串
                foreach (string s in error.Suggestions)
                {
                    spellingHints += string.Format("{0}\n", s);
                }
                //显示建议和Expander
                labExpanderSpelling.Content = spellingHints;
                expanderSpelling.IsExpanded = true;
            }
            else
            {
                labExpanderSpelling.Content = "Success";
                expanderSpelling.IsExpanded = true;
            }
        }
#endif

        private void myRect_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //单击时改变矩形颜色
            myRect.Fill = Brushes.Pink;
        }

    }
}