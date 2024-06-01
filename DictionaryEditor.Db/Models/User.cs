using System.ComponentModel.DataAnnotations;
namespace DictionaryEditor.Db.Models
{
    public class User
    {
        [Key]
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool LockoutEnabled { get; set; }
        public Role Role { get; set; }
        public string RoleName { get; set; }
    }
}