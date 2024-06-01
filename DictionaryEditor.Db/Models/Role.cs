using System.ComponentModel.DataAnnotations;

namespace DictionaryEditor.Db.Models
{
    public class Role
    {
        [Key]
        public string Name { get; set; }
    }
}