//using DictionaryEditor.Db;
//using DictionaryEditor.Db.Models;
//using DictionaryEditor.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DictionaryEditor2.Controllers
//{
//    public class LoadingController : Controller
//    {
//        private readonly DatabaseContext databaseContext;
//        private readonly RussianWordsDbRepository russianWordsDbRepository;
//        private readonly OssetianWordsDbRepository ossetianWordsDbRepository;
//        private readonly ExamplesDbRepository examplesDbRepository;


//        public LoadingController(DatabaseContext databaseContext, RussianWordsDbRepository russianWordsDbRepository, OssetianWordsDbRepository ossetianWordsDbRepository, ExamplesDbRepository examplesDbRepository)
//        {
//            this.databaseContext = databaseContext;
//            this.russianWordsDbRepository = russianWordsDbRepository;
//            this.examplesDbRepository = examplesDbRepository;
//            this.ossetianWordsDbRepository = ossetianWordsDbRepository;
//        }

//        public IActionResult Index()
//        {
//            StreamReader reader = new StreamReader("E:\\ProjactRubesh\\DictionaryEditor2\\DictionaryEditor2\\wwwroot\\json\\JSONOssetWords.json");

//            string json = reader.ReadToEnd();


//            List<Word> res = JsonConvert.DeserializeObject<List<Word>>(json);

//            foreach (var word in res)
//            {
//                OssetianWord ossetianWord = new OssetianWord
//                {
//                    Id = Guid.NewGuid(),
//                    Word = word.OssetianWord,
//                };
//                ossetianWordsDbRepository.AddWord(ossetianWord);
//                foreach (var meaning in word.Meanings)
//                {
//                    foreach (var translation in meaning.Translations)
//                    {
//                        RussianWord existingRussianWord = russianWordsDbRepository.TryGetByWord(translation);
//                        if (existingRussianWord != null)
//                        {
//                            existingRussianWord.OssetianWords.Add(ossetianWord);


//                            ossetianWord.RussianWords.Add(existingRussianWord);
//                            databaseContext.SaveChanges();
//                        }
//                        else
//                        {
//                            RussianWord newRussianWord = new RussianWord
//                            {
//                                Id = Guid.NewGuid(),
//                                Word = translation,
//                                OssetianWords = new List<OssetianWord>()
//                                {
//                                    ossetianWord
//                                },
//                            };
//                            ossetianWord.RussianWords.Add(newRussianWord);
//                            russianWordsDbRepository.AddWord(newRussianWord);
//                        }

//                    }
//                    foreach (var example in meaning.Examples)
//                    {
//                        Example newExample = new Example
//                        {
//                            Id = Guid.NewGuid(),
//                            Sentence = example.Item1,
//                            Translation = example.Item2,
//                            OssetianWord = ossetianWord
//                        };

//                        examplesDbRepository.AddExample(newExample);

//                    }
//                }


//            }

//            return View();
//        }
//    }
//}
