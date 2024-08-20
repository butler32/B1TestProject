using B1TestProject.Interfaces;
using B1TestProject.Utilities;
using B1TestProject.Utilities.Interfaces;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace B1TestProject.ViewModels
{
    public class TextFilesTaskVM : ViewModelBase
    {
        private readonly string _folderName = "..\\GeneretedFiles";

        private ITextFilesService _textFilesService { get; }
        private ICancellationTokenService _tokenService { get; }

        private Dictionary<string, string> pairsFileAndPath;

        private ObservableCollection<string> _files;
        public ObservableCollection<string> Files
        {
            get => _files;
            set { _files = value; OnPropertyChanged(); }
        }

        private ObservableCollection<string> _fileContent;
        public ObservableCollection<string> FileContent
        {
            get => _fileContent;
            set { _fileContent = value; OnPropertyChanged(); }
        }

        private string _searchQuery;
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                if (string.IsNullOrEmpty(SearchQuery) && !string.IsNullOrEmpty(SelectedFile))
                {
                    _ = LoadFileContentAsync();
                }
                else if (!string.IsNullOrEmpty(SearchQuery) && !string.IsNullOrEmpty(SelectedFile))
                {
                    _ = Search();
                }
                OnPropertyChanged();
            }
        }

        private string _selectedFile;
        public string SelectedFile
        {
            get => _selectedFile;
            set { _selectedFile = value; OnPropertyChanged(); _ = LoadFileContentAsync(); }
        }

        private bool _isFilesMerged;
        public bool IsFilesMerged
        {
            get => _isFilesMerged;
            set { _isFilesMerged = value;  OnPropertyChanged(); }
        }

        private bool _isFilesGenerated;
        public bool IsFilesGenerated
        {
            get => _isFilesGenerated;
            set { _isFilesGenerated = value; OnPropertyChanged(); }
        }

        private string _mergeFilter = String.Empty;
        public string MergeFilter
        {
            get => _mergeFilter;
            set { _mergeFilter = value; OnPropertyChanged(); }
        }

        private int _removedLinesCount;
        public int RemovedLinesCount
        {
            get => _removedLinesCount;
            set { _removedLinesCount = value; OnPropertyChanged(); }
        }

        private int _linesImported;
        public int LinesImported
        {
            get => _linesImported;
            set 
            { 
                _linesImported = value;
                LinesRemain = TotalLines - _linesImported;
                ProgressValue = TotalLines > 0 ? (double)LinesImported / TotalLines * 100 : 0;
                OnPropertyChanged(); 
            }
        }

        private int _linesRemain;
        public int LinesRemain
        {
            get => _linesRemain;
            set { _linesRemain = value; OnPropertyChanged(); }
        }

        private int _totalLines;
        public int TotalLines
        {
            get => _totalLines;
            set { _totalLines = value; OnPropertyChanged(); }
        }

        private double _progressValue;
        public double ProgressValue
        {
            get => _progressValue;
            set { _progressValue = value; OnPropertyChanged(); }
        }

        public ICommand GenerateCommand { get; }
        public ICommand MergeCommand { get; }
        public ICommand ImportCommand { get; }
        public ICommand GetAllFilesCommand { get; }
        public ICommand DeleteCommand { get; }

        private async Task DeleteAllFromDBAsync()
        {
            await _textFilesService.DeleteAllFilesAsync(_tokenService.GetToken());
        }

        private async Task GenerateFilesFuncAsync()
        {
            MessageBox.Show("It's gonna take about 1 minute");

            IsFilesGenerated = await _textFilesService.GenerateTextFiles(_tokenService.GetToken());

            IsFilesMerged = _textFilesService.IsMergedFileExist();
            if (!IsFilesMerged)
            {
                RemovedLinesCount = 0;
            }

            await LoadFilesAsync();
        }

        private async Task MergeFilesFuncAsync()
        {
            MessageBox.Show("It's gonna take about 2 minutes");

            RemovedLinesCount = await _textFilesService.MergeTextFiles(_tokenService.GetToken(), MergeFilter);
            IsFilesMerged = _textFilesService.IsMergedFileExist();

            await LoadFilesAsync();
        }

        private async Task ImportFilesFuncAsync()
        {
            string separator = "||";
            string filePath = Path.Combine("..", "GeneretedFiles", "mergedFile.txt");

            TotalLines = _textFilesService.GetAmountOfLines() - _removedLinesCount;

            using (var reader = new StreamReader(filePath))
            {
                LinesImported = 0;

                List<string[]> lines = new List<string[]>();
                string line;

                while ((line = await reader.ReadLineAsync()) != null)
                {
                    string[] parsedLine = line.Split(separator);
                    lines.Add(parsedLine);
                    LinesImported++;

                    if (lines.Count == 1000)
                    {
                        await _textFilesService.ImportFileToDBAsync(lines, _tokenService.GetToken());
                        lines.Clear();
                    }
                }

                if (lines.Count > 0)
                {
                    await _textFilesService.ImportFileToDBAsync(lines, _tokenService.GetToken());
                }
            } 
                
            
        }


        private async Task Search()
        {
            if (SelectedFile == null)
            {
                return;
            }

            var list = await _textFilesService.SearchInFileAsync(pairsFileAndPath[SelectedFile], SearchQuery);

            FileContent.Clear();

            foreach (var file in list)
            {
                FileContent.Add(file);
            }
        }

        public async Task LoadFilesAsync()
        {
            pairsFileAndPath = new Dictionary<string, string>();
            Files = new ObservableCollection<string>();

            await Task.Run(() => LoadFiles());
        }

        private void LoadFiles()
        {
            if (Directory.Exists(_folderName))
            {
                string[] filePaths = Directory.GetFiles(_folderName, "*.txt");

                foreach (var filePath in filePaths)
                {
                    var splitedPath = filePath.Split('\\');
                    var file = splitedPath[splitedPath.Length - 1].Split('.');
                    pairsFileAndPath.Add(file[0], filePath);
                }

                pairsFileAndPath = pairsFileAndPath.Sort();

                foreach (var file in pairsFileAndPath)
                {
                    Application.Current.Dispatcher.Invoke(() => Files.Add(file.Key));
                }
            }
        }

        private async Task LoadFileContentAsync()
        {
            if (!string.IsNullOrEmpty(SelectedFile))
            {
                FileContent = new ObservableCollection<string>();

                using (var streamReader = new StreamReader(pairsFileAndPath[SelectedFile]))
                {
                    string line;
                    while ((line = await streamReader.ReadLineAsync()) != null)
                    {
                        FileContent.Add(line);
                    }
                }
            }
        }


        public TextFilesTaskVM(ITextFilesService textFilesService, ICancellationTokenService tokenService)
        {
            _textFilesService = textFilesService;
            _tokenService = tokenService;

            GenerateCommand = ICommand.From(GenerateFilesFuncAsync);
            MergeCommand = ICommand.From(MergeFilesFuncAsync);
            ImportCommand = ICommand.From(ImportFilesFuncAsync);
            GetAllFilesCommand = ICommand.From(LoadFilesAsync);
            DeleteCommand = ICommand.From(DeleteAllFromDBAsync);

            IsFilesGenerated = _textFilesService.IsTextFilesExist();
            IsFilesMerged = _textFilesService.IsMergedFileExist();

            GetAllFilesCommand.Execute(null);
        }
    }
}
