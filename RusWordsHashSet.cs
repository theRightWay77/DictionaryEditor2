using DictionaryEditor.Db;
using DictionaryEditor.Db.Models;

namespace DictionaryEditor2
{
    public class RusWordsHashSet
    {
        private readonly DatabaseContext databaseContext;
        public HashSet<string> rusWords;
        public RusWordsHashSet(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
            this.rusWords = new HashSet<string>();

            foreach (var rusWord in databaseContext.RussianWords)
            {
                rusWords.Add(rusWord.Word);
            }
        }

        //public HashSet<string> CreateHashSet()
        //{
        //    HashSet<string> rusWordsHashSet = new HashSet<string>();

        //    foreach (var rusWord in databaseContext.RussianWords)
        //    {
        //        rusWordsHashSet.Add(rusWord.Word);
        //    }
        //    return rusWordsHashSet;
        //}
    }
}
