using B1TestProject.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace B1TestProject.Views
{
    /// <summary>
    /// Interaction logic for ExcelFilesTaskView.xaml
    /// </summary>
    public partial class ExcelFilesTaskView : UserControl
    {
        public ExcelFilesTaskView(ExcelFilesTaskVM excelFilesTaskVM)
        {
            InitializeComponent();
            DataContext = excelFilesTaskVM;
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0)
                {
                    foreach (string file in files)
                    {
                        if (file.Split('.').Last() == "xls" || file.Split('.').Last() == "xlsx" || file.Split('.').Last() == "xlsm")
                        {
                            (DataContext as ExcelFilesTaskVM)!.FilePath = file;
                            return;
                        }
                    }

                    MessageBox.Show("File must be a valid Excel file");
                }
            }
        }
    }
}
