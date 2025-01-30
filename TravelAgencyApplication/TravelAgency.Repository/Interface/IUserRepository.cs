using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Domain;

namespace TravelAgency.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<Customer> GetAll();
        Customer Get(string? id);
        void Insert(Customer entity);
        void Update(Customer entity);
        void Delete(Customer entity);
    }
}
