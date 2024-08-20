namespace B1TestProject.Utilities
{
    public static class DictionarySort
    {
        public static Dictionary<string, string> Sort(this  Dictionary<string, string> dictionary)
        {
            var wordKeys = dictionary.Keys.Where(key => !IsNum(key)).ToList();
            var numberKeys = dictionary.Keys.Where(key => IsNum(key)).OrderBy(key => int.Parse(key)).ToList();

            var sortedDictionary = new Dictionary<string, string>();

            foreach (var key in wordKeys)
            {
                sortedDictionary.Add(key, dictionary[key]);
            }

            foreach (var key in numberKeys)
            {
                sortedDictionary.Add(key, dictionary[key]);
            }

            return sortedDictionary;
        }

        private static bool IsNum(string key)
        {
            return int.TryParse(key, out _);
        }
    }
}
