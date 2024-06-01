using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryEditor.Db.Models
{
    public class Cases
    {
        public Guid Id { get; set; }
        [AllowNull]
        public OssetianWord Word { get; set; }
        [AllowNull]
        public string Nominative { get; set; }
        [AllowNull]
        public string Genitive { get; set; }
        [AllowNull]
        public string Dative { get; set; }
        [AllowNull]
        public string Directional { get; set; }
        [AllowNull]
        public string Postponement { get; set; }
        [AllowNull]
        public string External { get; set; }
        [AllowNull]
        public string  Internal  { get; set; }
        [AllowNull]
        public string Joint { get; set; }
    }
}
