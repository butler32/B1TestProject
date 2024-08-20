using B1TestProject.ViewModels;
using System.Windows.Controls;

namespace B1TestProject.Views
{
    /// <summary>
    /// Interaction logic for TextFilesTaskView.xaml
    /// </summary>
    public partial class TextFilesTaskView : UserControl
    {
        public TextFilesTaskView(TextFilesTaskVM textFilesTaskViewModel)
        {
            InitializeComponent();
            DataContext = textFilesTaskViewModel;
        }
    }
}
