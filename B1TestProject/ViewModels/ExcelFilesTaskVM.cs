using B1TestProject.Core.DTO;
using B1TestProject.Interfaces;
using B1TestProject.Models;
using B1TestProject.Utilities.Interfaces;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows;

namespace B1TestProject.ViewModels
{
    public class ExcelFilesTaskVM : ViewModelBase
    {
        private readonly IExcelFilesService _excelFilesService;
        private readonly ICancellationTokenService _cancellationTokenService;

        private ExcelFilesDTO _selectedTable;
        public ExcelFilesDTO SelectedTable
        {
            get => _selectedTable;
            set 
            {
                _selectedTable = value;
                if (_selectedTable != null)
                {
                    BalanceSheet = SortBalanceSheet(_selectedTable.BalanceSheets);
                }
                OnPropertyChanged(); 
            }
        }

        private ObservableCollection<ExcelFilesDTO> _excelFilesList;
        public ObservableCollection<ExcelFilesDTO> ExcelFilesList
        {
            get => _excelFilesList;
            set { _excelFilesList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<BalanceSheetModel> _balanceSheet;
        public ObservableCollection<BalanceSheetModel> BalanceSheet
        {
            get => _balanceSheet;
            set { _balanceSheet = value; OnPropertyChanged(); }
        }

        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            set { _filePath = value; OnPropertyChanged(); }
        }

        public ICommand DialogCommand { get; }
        public ICommand ImportCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand GetFilesCommand { get; }

        private async Task GetFilesAsync()
        {
            ExcelFilesList = new ObservableCollection<ExcelFilesDTO>(await _excelFilesService.GetAllFilesFromDBAsync(_cancellationTokenService.GetToken()));
        }

        private async Task DeleteFiles()
        {
            await _excelFilesService.DeleteAllFilesFromDBAsync(_cancellationTokenService.GetToken());
            await GetFilesAsync();
        }

        private void OpenDialog()
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Filter = "Excel Files|*.xls;*.xlsx;*.xlsm"
            };

            if (fileDialog.ShowDialog() == true)
            {
                if (fileDialog.FileName.Split('.').Last() == "xls" || fileDialog.FileName.Split('.').Last() == "xlsx" || fileDialog.FileName.Split('.').Last() == "xlsm")
                {
                    FilePath = fileDialog.FileName;
                }
                else
                {
                    MessageBox.Show("File must be a valid Excel file");
                }
            }
        }

        private async Task Import()
        {
            await _excelFilesService.ImportExcelToDBAsync(FilePath, _cancellationTokenService.GetToken());
            ExcelFilesList = new ObservableCollection<ExcelFilesDTO>(await _excelFilesService.GetAllFilesFromDBAsync(_cancellationTokenService.GetToken()));
        }

        public ExcelFilesTaskVM(IExcelFilesService excelFilesService, ICancellationTokenService cancellationTokenService)
        {
            _excelFilesService = excelFilesService;
            _cancellationTokenService = cancellationTokenService;

            DialogCommand = ICommand.From(OpenDialog);
            ImportCommand = ICommand.From(Import);
            DeleteCommand = ICommand.From(DeleteFiles);
            GetFilesCommand = ICommand.From(GetFilesAsync);

            GetFilesCommand.Execute(null);
        }

        private ObservableCollection<BalanceSheetModel> SortBalanceSheet(ICollection<BalanceSheetDTO> balanceSheets)
        {
            var sortedList = balanceSheets
                                .OrderBy(b => b.Class)
                                .ThenBy(b => GetGroup(b.AccountNumber))
                                .ThenBy(b => GetSortOrder(b.AccountNumber))
                                .ToList();

            var groupedList = new ObservableCollection<BalanceSheetModel>();

            int? previousClass = null;

            foreach (var item in sortedList)
            {
                if (previousClass != item.Class)
                {
                    groupedList.Add(new BalanceSheetModel
                    {
                        Class = item.Class,
                        AccountNumber = $"КЛАСС {item.Class}",
                        IsClassHeader = true
                    });
                    previousClass = item.Class;
                }

                var modelItem = Convert(item);

                if (modelItem.AccountNumber == "БАЛАНС" || modelItem.AccountNumber == "ПО КЛАССУ")
                {
                    modelItem.IsSpecialRow = true;
                }

                groupedList.Add(modelItem);
            }

            return groupedList;
        }

        private static BalanceSheetModel Convert(BalanceSheetDTO balanceSheetDTO)
        {
            return new BalanceSheetModel
            {
                Id = balanceSheetDTO.Id,
                TurnoverDebit = balanceSheetDTO.TurnoverDebit,
                AccountNumber = balanceSheetDTO.AccountNumber,
                Class = balanceSheetDTO.Class,
                IncomingBalanceActive = balanceSheetDTO.IncomingBalanceActive,
                IncomingBalancePassive = balanceSheetDTO.IncomingBalancePassive,
                OutgoingBalanceActive = balanceSheetDTO.OutgoingBalanceActive,
                OutgoingBalancePassive = balanceSheetDTO.OutgoingBalancePassive,
                TurnoverCredit = balanceSheetDTO.TurnoverCredit,
            };
        }

        private static int GetGroup(string accountNumber)
        {
            if (accountNumber == "БАЛАНС" || accountNumber == "ПО КЛАССУ")
                return int.MaxValue;

            if (accountNumber.Length >= 2 && int.TryParse(accountNumber.Substring(0, 2), out int group))
            {
                return group;
            }

            return int.MaxValue - 1;
        }

        private static int GetSortOrder(string accountNumber)
        {
            if (accountNumber == "БАЛАНС")
                return int.MaxValue;

            if (accountNumber == "ПО КЛАССУ")
                return int.MaxValue - 1;

            if (accountNumber.Length == 2)
            {
                return int.MaxValue - 2;
            }

            if (accountNumber.Length == 4 && int.TryParse(accountNumber.Substring(2, 2), out int number))
            {
                return number;
            }

            return int.MaxValue - 3;
        }
    }
}
