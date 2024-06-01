using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryEditor.Db.Models
{
    public class RussianWord
    {
        public Guid Id { get; set; }
        public string Word { get; set; }
        public List<OssetianWord> OssetianWords { get; set; } 
    }
}
