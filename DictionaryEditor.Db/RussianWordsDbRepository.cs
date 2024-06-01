using DictionaryEditor.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryEditor.Db
{
    public class RussianWordsDbRepository
    {
        private readonly DatabaseContext databaseContext;
        
        public RussianWordsDbRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public RussianWord TryGetByWord(string word)
        {
            return databaseContext.RussianWords.FirstOrDefault(x => x.Word == word);
        }

        public void AddWord(RussianWord word)
        {
            databaseContext.RussianWords.Add(word);
            databaseContext.SaveChanges();
        }
        
        public void AddNewWord(string word, OssetianWord ossetWord)
        {
            RussianWord newWord = new RussianWord
            {
                Id = Guid.NewGuid(),
                Word = word,
                OssetianWords = new List<OssetianWord> { ossetWord }
            };
            databaseContext.RussianWords.Add(newWord);
            ossetWord.RussianWords.Add(newWord);
            databaseContext.SaveChanges();
        }
    }
}
