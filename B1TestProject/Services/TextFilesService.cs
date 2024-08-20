using B1TestProject.Core.DTO;
using B1TestProject.Core.Interfaces;
using System.IO;
using System.Text;

namespace B1TestProject.Interfaces
{
    public class TextFilesService : ITextFilesService
    {
        private readonly int numOfFiles = 100;
        private readonly int numOfLines = 100000;
        private readonly int lastYears = 5;
        private readonly int numOfLatinLetters = 10;
        private readonly int numOfRussianLetters = 10;
        private readonly int minInt = 1;
        private readonly int maxInt = 100000000;
        private readonly int minFloat = 1;
        private readonly int maxFloat = 20;

        private readonly string mergedFileName = "mergedFile.txt";
        private readonly string directoryPath = "..\\GeneretedFiles";

        private readonly ITextLinesService _textLinesService;

        StringBuilder sb = new StringBuilder();
        Random random = new Random();

        public TextFilesService(ITextLinesService textLinesService)
        {
            _textLinesService = textLinesService;
        }

        public async Task<List<string>> SearchInFileAsync(string fileName, string searchQuery)
        {
            string filePath = Path.Combine(directoryPath, fileName);

            if (string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(searchQuery))
            {
                return new List<string> { "No matching lines found." };
            }

            try
            {
                string[] lines = await File.ReadAllLinesAsync(filePath);
                var matchingLines = lines.Where(line => line.Contains(searchQuery)).ToList();

                if (matchingLines.Any())
                {
                    return matchingLines;
                }
                else
                {
                    return new List<string> { "No matching lines found." };
                }
            }
            catch (IOException ex)
            {
                return new List<string> { $"Error reading file: {ex.Message}" };
            }
        }

        public async Task<bool> GenerateTextFiles(CancellationToken cancellationToken)
        {
            if (Directory.Exists(directoryPath))
            {
                Directory.Delete(directoryPath, true);
            }

            Directory.CreateDirectory(directoryPath);

            DateTime startDate = DateTime.Now.AddYears(-lastYears);
            DateTime endDate = DateTime.Now;

            int range = (endDate - startDate).Days;

            for (int i = 0; i < numOfFiles; i++)
            {
                using (StreamWriter streamWriter = new StreamWriter(Path.Combine(directoryPath, $"{i + 1}.txt")))
                {
                    sb.Clear();
                    for (int j = 0; j < numOfLines; j++)
                    {
                        sb.Append(startDate.AddDays(random.Next(range)).ToShortDateString());
                        sb.Append("||");

                        for (int k = 0; k < numOfLatinLetters; k++)
                        {
                            bool isUpperCase = random.Next(2) == 0;

                            if (isUpperCase)
                            {
                                sb.Append((char)random.Next('A', 'Z' + 1));
                            }
                            else
                            {
                                sb.Append((char)random.Next('a', 'z' + 1));
                            }
                        }
                        sb.Append("||");

                        for (int k = 0; k < numOfRussianLetters; k++)
                        {
                            bool isUpperCase = random.Next(2) == 0;

                            if (isUpperCase)
                            {
                                sb.Append((char)random.Next('А', 'Я' + 1));
                            }
                            else
                            {
                                sb.Append((char)random.Next('а', 'я' + 1));
                            }
                        }
                        sb.Append("||");

                        sb.Append(random.Next(minInt, maxInt + 1));
                        sb.Append("||");

                        int integerPart = random.Next(minFloat, maxFloat);
                        double fractionalPart = random.NextDouble();
                        sb.Append((integerPart + fractionalPart).ToString("F8")); // string format to set 8 digits after point
                        sb.Append("||");
                        sb.AppendLine();
                    }

                    await streamWriter.WriteLineAsync(sb, cancellationToken).ConfigureAwait(false);
                }
            }

            return true;
        }

        public async Task<int> MergeTextFiles(CancellationToken cancellationToken, string? filterString = null)
        {
            string[] filePaths = new string[numOfFiles];

            for (int i = 0; i < numOfFiles; i++)
            {
                filePaths[i] = Path.Combine(directoryPath, $"{i + 1}.txt");
            }

            var mergedContent = new List<string>();
            int removedLinesCount = 0;

            foreach (var filePath in filePaths)
            {
                if (File.Exists(filePath))
                {
                    var lines = await File.ReadAllLinesAsync(filePath, cancellationToken);
                    foreach (var line in lines)
                    {
                        // Skip empty lines
                        if (string.IsNullOrWhiteSpace(line))
                        {
                            continue;
                        }

                        if (string.IsNullOrEmpty(filterString) || !line.Contains(filterString))
                        {
                            mergedContent.Add(line);
                        }
                        else
                        {
                            removedLinesCount++;
                        }
                    }
                }
            }

            string outputFilePath = Path.Combine(directoryPath, mergedFileName);
            await File.WriteAllLinesAsync(outputFilePath, mergedContent, cancellationToken);

            return removedLinesCount;
        }


        public async Task ImportFileToDBAsync(List<string[]> lineData, CancellationToken cancellationToken)
        {
            await _textLinesService.AddBunchAsync(lineData.Select(Convert).ToList(), cancellationToken);
        }

        public bool IsMergedFileExist()
        {
            return File.Exists(Path.Combine(directoryPath, mergedFileName));
        }

        public bool IsTextFilesExist()
        {
            for(int i = 0; i < numOfFiles; i++)
            {
                if (!File.Exists(Path.Combine(directoryPath, $"{i + 1}.txt")))
                {
                    return false;
                }
            }

            return true;
        }

        public async Task DeleteAllFilesAsync(CancellationToken cancellationToken)
        {
            await _textLinesService.DeleteAllLinesAsync(cancellationToken);
        }

        private TextLineDTO Convert(string[] strings)
        {
            return new TextLineDTO
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Parse(strings[0]),
                Latin = strings[1],
                Russian = strings[2],
                IntegerNum = int.Parse(strings[3]),
                DoubleNum = double.Parse(strings[4])
            };
        }

        public int GetAmountOfLines()
        {
            return numOfFiles * numOfLines;
        }
    }
}
