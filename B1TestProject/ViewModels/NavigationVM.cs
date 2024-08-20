using B1TestProject.Utilities.Interfaces;
using B1TestProject.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace B1TestProject.ViewModels
{
    public class NavigationVM : ViewModelBase
    {
        IServiceProvider _serviceProvider;

        private UserControl _currentView;
        public UserControl CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public ICommand TextFileCommand { get; }
        public ICommand ExcelFileCommand { get; }

        private void SetTextFileView()
        {
            CurrentView = _serviceProvider.GetRequiredService<TextFilesTaskView>();
            var a = 0;
        }

        private void SetExcelFileView()
        {
            CurrentView = _serviceProvider.GetRequiredService<ExcelFilesTaskView>();
        }

        public NavigationVM(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            TextFileCommand = ICommand.From(SetTextFileView);
            ExcelFileCommand = ICommand.From(SetExcelFileView);

            CurrentView = _serviceProvider.GetRequiredService<ExcelFilesTaskView>();
        }
    }
}
