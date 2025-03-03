namespace WpfAppDemo
{
    public interface ICommand
    {
        //当影响命令能否执行的变化发生时,触发该事件
        event EventHandler CanExecuteChanged;
    }
}
