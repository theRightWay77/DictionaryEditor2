using DictionaryEditor.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryEditor.Db
{
    public class ExamplesDbRepository
    {
        private readonly DatabaseContext databaseContext;
        public ExamplesDbRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public void AddExample(Example example)
        {
            databaseContext.Examples.Add(example);
            databaseContext.SaveChanges();
        }
    }
}
