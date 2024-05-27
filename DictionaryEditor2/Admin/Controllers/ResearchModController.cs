using DictionaryEditor.Db;
using DictionaryEditor.Db.Models;
using Microsoft.AspNetCore.Mvc;

namespace DictionaryEditor2.Admin.Controllers
{
    public class ResearchModController : Controller
    {
        private readonly OssetianWordsDbRepository ossetianWordsDbRepository;
        public ResearchModController(OssetianWordsDbRepository ossetianWordsDbRepository)
        {
            this.ossetianWordsDbRepository = ossetianWordsDbRepository;
        }

        public IActionResult Index()
        {
            List<OssetianWord> wordsList = ossetianWordsDbRepository.GetProducts();
            return View(wordsList);
        }
    }
}
