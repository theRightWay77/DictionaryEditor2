using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryEditor.Db.Models
{
    public class Example
    {
        public Guid Id { get; set; }
        public string? Sentence { get; set; }    
        public string? Translation { get; set; }

        public OssetianWord OssetianWord { get; set; }

    }
}
