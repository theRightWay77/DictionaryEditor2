using DictionaryEditor.Db;
using DictionaryEditor.Db.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryEditor2.DatabaseFulling
{
    public class LoadingController : Controller
    {
        private readonly DatabaseContext databaseContext;
        private readonly RussianWordsDbRepository russianWordsDbRepository;
        private readonly OssetianWordsDbRepository ossetianWordsDbRepository;
        private readonly ExamplesDbRepository examplesDbRepository;
        private readonly UserDbRepository userRepository;
        private readonly RoleDbRepository roleRepository;
        private readonly DictionariesRepository dictionariesDbRepository;

        public LoadingController(DatabaseContext databaseContext,
            RussianWordsDbRepository russianWordsDbRepository,
            OssetianWordsDbRepository ossetianWordsDbRepository,
            ExamplesDbRepository examplesDbRepository,
            UserDbRepository userRepository,
            RoleDbRepository roleRepository,
            DictionariesRepository dictionariesDbRepository)
        {
            this.databaseContext = databaseContext;
            this.russianWordsDbRepository = russianWordsDbRepository;
            this.examplesDbRepository = examplesDbRepository;
            this.ossetianWordsDbRepository = ossetianWordsDbRepository;
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            this.dictionariesDbRepository = dictionariesDbRepository;
            if (roleRepository.GetAll().Count() == 0)
            {
                roleRepository.Add(new Role() { Name = "User" });
                roleRepository.Add(new Role() { Name = "Admin" });
                roleRepository.Add(new Role() { Name = "Redactor" });
            }
            if (userRepository.GetAll().Count() == 0)
            {
                userRepository.Add(new User()
                {
                    Login = "marat",
                    Password = "marat",
                    Name = "marat",
                    Surname = "marat",
                    Role = roleRepository.TryGetByName("Admin")
                });
            }
        }

        public IActionResult Index()
        {
            StreamReader reader = new StreamReader("E:\\ProjactRubesh\\DictionaryEditor2\\DictionaryEditor2\\wwwroot\\json\\JSONOssetWords.json");

            string json = reader.ReadToEnd();


            List<Word> res = JsonConvert.DeserializeObject<List<Word>>(json);
            Dictionary newDict = new Dictionary() { Id = Guid.NewGuid(), Name = "Осетинско-русский" };
            dictionariesDbRepository.AddDict(newDict);
            foreach (var word in res)
            {
                OssetianWord ossetianWord = new OssetianWord
                {
                    Id = Guid.NewGuid(),
                    Word = word.OssetianWord,
                    Dictionary = newDict,
                };
                ossetianWordsDbRepository.AddWord(ossetianWord);
                foreach (var meaning in word.Meanings)
                {
                    foreach (var translation in meaning.Translations)
                    {
                        RussianWord existingRussianWord = russianWordsDbRepository.TryGetByWord(translation);
                        if (existingRussianWord != null)
                        {
                            existingRussianWord.OssetianWords.Add(ossetianWord);


                            ossetianWord.RussianWords.Add(existingRussianWord);
                            databaseContext.SaveChanges();
                        }
                        else
                        {
                            RussianWord newRussianWord = new RussianWord
                            {
                                Id = Guid.NewGuid(),
                                Word = translation,
                                OssetianWords = new List<OssetianWord>()
                                {
                                    ossetianWord
                                },
                            };
                            ossetianWord.RussianWords.Add(newRussianWord);
                            russianWordsDbRepository.AddWord(newRussianWord);
                        }

                    }
                    foreach (var example in meaning.Examples)
                    {
                        Example newExample = new Example
                        {
                            Id = Guid.NewGuid(),
                            Sentence = example.Item1,
                            Translation = example.Item2,
                            OssetianWord = ossetianWord
                        };

                        examplesDbRepository.AddExample(newExample);

                    }
                }


            }

            return View();
        }
    }
}
