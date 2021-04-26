using FullStackBE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullStackBE.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUnique(string username);
        User Authenticate(string username, string password);
        User Register(string username, string password);
    }
}
