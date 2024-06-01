using DictionaryEditor.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace DictionaryEditor.Db
{
    public class UserDbRepository
    {
        private readonly DatabaseContext databaseContext;
        private RoleDbRepository roleRepository;

        public UserDbRepository(DatabaseContext databaseContext,
            RoleDbRepository roleRepository)
        {
            this.databaseContext = databaseContext;
            this.roleRepository = roleRepository;
        }

        public void Add(User user)
        {
            databaseContext.Users.Add(user);
            databaseContext.SaveChanges();
        }

        public List<User> GetAll()
        {
            return databaseContext.Users
                .Include(u => u.Role)
                .ToList();
        }

        public void Remove(string login)
        {
            var user = TryGetByLogin(login);
            if (user is null)
                return;
            databaseContext.Users.Remove(user);
            databaseContext.SaveChanges();
        }

        //public void EditData(string login, EditUserData newData)
        //{
        //    var user = TryGetByLogin(login);
        //    if (user is null)
        //        return;
        //    user.Address = newData.Address;
        //    user.PhoneNumber = newData.PhoneNumber;
        //    user.Name = newData.Name;
        //    user.Surname = newData.Surname;
        //    databaseContext.SaveChanges();
        //}

        public void ChangePassword(string login, string newPassword)
        {
            var user = TryGetByLogin(login);
            if (user is null)
                return;
            user.Password = newPassword;
            databaseContext.SaveChanges();
        }

        public void ChangeRole(string login, string newRoleName)
        {
            var user = TryGetByLogin(login);
            var newRole = roleRepository.TryGetByName(newRoleName);
            if (user is null || newRole is null)
                return;
            user.Role = newRole;
            databaseContext.SaveChanges();
        }

        public User TryGetByLogin(string login)
        {
            return databaseContext.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Login == login);
        }
    }
}