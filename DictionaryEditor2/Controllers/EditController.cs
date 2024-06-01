using DictionaryEditor.Db;
using DictionaryEditor.Db.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;
using System.Linq;


namespace DictionaryEditor2.Controllers
{ 
    public class EditController : Controller
    {
        private readonly OssetianWordsDbRepository ossetianWordsDbRepository;
        private readonly RussianWordsDbRepository russianWordsDbRepository;
        private readonly DatabaseContext databaseContext;
        public RusWordsHashSet rusWordsHashSet;

        public EditController(OssetianWordsDbRepository ossetianWordsDbRepository, DatabaseContext databaseContext, RusWordsHashSet rusWordsHashSet, RussianWordsDbRepository russianWordsDbRepository)
        {
            this.ossetianWordsDbRepository = ossetianWordsDbRepository;
            this.databaseContext = databaseContext;
            this.rusWordsHashSet = rusWordsHashSet;
            this.russianWordsDbRepository= russianWordsDbRepository;
        }

        public IActionResult Index()
        {

            return View(ossetianWordsDbRepository.GetWords());
        }
        public IActionResult OneWord(Guid id)
        {
            OssetianWord word = ossetianWordsDbRepository.TryGetById(id);
            return View(word);
        }

        [HttpPost]
        public IActionResult SaveChanges(Guid id, string? word, string? singular, string? plural, string? Case, Tense tense, string rusWords)
        {
            OssetianWord ossetWord = ossetianWordsDbRepository.TryGetById(id);
            if (word != null) { ossetWord.Word = word; }
            if (singular != null) { ossetWord.Singular = singular; }
            if (plural != null) { ossetWord.Plural = plural; }
            if (Case != null) { ossetWord.Case = Case; }
            if (tense == 0) ossetWord.Tense = Tense.thePresent;
            else if ((int)tense == 1) ossetWord.Tense = Tense.thePast;
            else if ((int)tense == 2) ossetWord.Tense = Tense.theFuture;

            List<string> allRusTrans = rusWords.Split(new[] { ',', '.', ';', ':', '-', '\n', '\r', ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            foreach (string rusTrans in allRusTrans)
            {
                if (!rusWordsHashSet.rusWords.Contains(rusTrans)) //если в хэш сете нет слова
                {                   
                    russianWordsDbRepository.AddNewWord(rusTrans, ossetWord);//создаем новое RussianWord                   
                }
                else if (!ossetWord.RussianWords.Select(x => x.Word).Contains(rusTrans))//если в хэш сете есть, а у нашего осетинского слова нет
                {
                    RussianWord russianWord = russianWordsDbRepository.TryGetByWord(rusTrans);
                    ossetWord.RussianWords.Add(russianWord);//создаем связь
                }
                
            }
            List<RussianWord> needDelete = new List<RussianWord>();
            foreach (var rusWordss in ossetWord.RussianWords)//проверка на удалание
            {
                
                if (!allRusTrans.Contains(rusWordss.Word)) //если у осет слова есть то, чего нет в пришедшем списке
                {
                    needDelete.Add(rusWordss);//добавим в список на удаление, потому что нельзя в цикле менять лист
                    //ossetWord.RussianWords.Remove(rusWordss);//удаляем связь осет слова с тем, чего нет в списке
                    rusWordss.OssetianWords.Remove(ossetWord);//и наоборот
                                      
                }
                
            }
            foreach (var wordToDelete in needDelete)
            {
                ossetWord.RussianWords.Remove(wordToDelete);
            }


            databaseContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
