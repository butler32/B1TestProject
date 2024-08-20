namespace B1TestProject.Utilities.Interfaces
{
    public interface ICommand : System.Windows.Input.ICommand
    {
        void System.Windows.Input.ICommand.Execute(object? parameter) => ExecuteAsync().ConfigureAwait(false);
        bool System.Windows.Input.ICommand.CanExecute(object? parameter) => true;

        Task ExecuteAsync();

        public static RelayCommand From(Action action) => new(action);
        public static RelayCommand From(Func<Task> action) => new(action);
    }
}
