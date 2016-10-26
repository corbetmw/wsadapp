using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wsad_app.Models.DataAccess;

namespace wsad_app.Models.Business
{
    public class UserManager
    {
        public User AddUser(User template)
        {
            using (wsadDbContext context = new wsadDbContext())
            {
                User newUserObj = context.Users.Add(template);

                context.SaveChanges();

                return newUserObj;
            }
        }

        public User UpdateUser(User userToUpdate)
        {
            using (wsadDbContext context = new wsadDbContext())
            {
                //Get User From Database
                User currentUserDTO = context.Users.Find(userToUpdate.Id);

                //Copy Values
                currentUserDTO.EmailAddress = userToUpdate.EmailAddress;
                currentUserDTO.EmailOpt = userToUpdate.EmailOpt;
                currentUserDTO.FirstName = userToUpdate.FirstName;
                currentUserDTO.LastName = userToUpdate.LastName;
                currentUserDTO.UserName = userToUpdate.UserName;

                //Does password need to change?
                if (string.IsNullOrWhiteSpace(userToUpdate.Password) == false &&
                    currentUserDTO.Password != userToUpdate.Password)
                {
                    currentUserDTO.Password = userToUpdate.Password;
                }

                //Save Changes
                context.SaveChanges();

                return currentUserDTO;
            }
        }

        //internal IQueryable<User_Role> GetUserRoles(int userId)
        //{
        //    //dbcontext
        //    wsadDbContext context = new wsadDbContext();

        //    //Select User_Roles based on user_Id
        //    IQueryable<User_Role> matches = context.UserRoles.Where(row => row.User_Id == userId);

        //    //REturn results -- matching user_roles
        //    return matches;

        //}

        public IQueryable<User> GetAllUsers()
        {
            wsadDbContext context = new wsadDbContext();
            //SELECT * FROM USERS
            return context.Users;
        }

        public User GetUser(int userId)
        {
            //SELECT TOP 1 * FROM USERS WHERE ID = @userId
            return GetAllUsers().FirstOrDefault(row => row.Id == userId);
        }

        public void DeleteUser(int userId)
        {
            using (wsadDbContext context = new wsadDbContext())
            {
                User userDTO = context.Users.Find(userId);

                context.Users.Remove(userDTO);

                context.SaveChanges();
            }
        }
    }
}