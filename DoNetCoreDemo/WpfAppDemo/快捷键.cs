using System.Windows;

namespace WpfAppDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region CommandBinding对象

#if false

        #region F1
        /// <summary>
        /// 新建CommandBinding对象 F1
        /// 将命令绑定到程序中给定的时间处理程序
        /// 大多数CommandBinding对象都要实现CanExecute和Executed
        /// </summary>
        private void SetFlcommandBinding()
        {
            //配置CommandBinding对象,使其操作ApplicationCommands.Help命令
            //该命令自动响应F1键
            CommandBinding helpBinding = new CommandBinding(ApplicationCommands.Help);
            helpBinding.CanExecute += CanHelpExecute;
            helpBinding.Executed += HelpExecuted;
            CommandBindings.Add(helpBinding);
        }

        /// <summary>
        /// 响应事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Look,it is not that difficult.Just type something!", "Help!");
        }

        /// <summary>
        /// 是否响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanHelpExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //阻止命令执行CanExecute = false;
            e.CanExecute = true;
        }
        #endregion

        #region

        /// <summary>
        /// 响应事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenCmdExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            //创建打开文件的对话框,并只显示TXT文件
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files |*.txt";

            //是否单击OK
            if (true == openFileDialog.ShowDialog())
            {
                //加载所选文件
                string dataFromFile = File.ReadAllText(openFileDialog.FileName);

                //在TextBox中显示字符串
                textData.Text = dataFromFile;
            }
        }

        /// <summary>
        /// 是否响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenCmdCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //阻止命令执行CanExecute = false;
            e.CanExecute = true;
        }


        /// <summary>
        /// 响应事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveCmdExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            //创建打开文件的对话框,并只显示TXT文件
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files |*.txt";

            //是否单击OK
            if (true == saveFileDialog.ShowDialog())
            {
                //将TextBox中的数据保存到命名的文件(另存为)
                File.WriteAllText(saveFileDialog.FileName, textData.Text);
            }
        }

        /// <summary>
        /// 是否响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveCmdCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //阻止命令执行CanExecute = false;
            e.CanExecute = true;
        }

        #endregion

#endif

        #endregion
    }
}
