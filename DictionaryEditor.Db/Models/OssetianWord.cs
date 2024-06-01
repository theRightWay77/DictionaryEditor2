using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryEditor.Db.Models
{
    public enum Gender
    {
        notSelected,
        male,
        female,
        neuter,
    }
    public enum Tense
    {
        thePresent,
        thePast,        
        theFuture,
    }

    public class OssetianWord
    {
        public Guid Id { get; set; }
        public string? Word { get; set; }
        public string? Singular { get; set; }
        public string? Plural { get; set; }
        public string? Case { get; set; }
        public Tense Tense { get; set; }
        public Gender Gender { get; set; }

        public Dictionary Dictionary {  get; set; }
        public List<RussianWord> RussianWords { get; set; } = new List<RussianWord>();
        public List<Example> Examples { get; set; } = new List<Example>();
        public List<Cases> Cases { get; set; } = new List<Cases>()
        {
            new Cases()
        };

    }
}
