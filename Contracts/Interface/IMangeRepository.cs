using System.Threading.Tasks;
namespace Contracts.Interface
{
    public interface IMangeRepository
    {
        public IComponyRepository componyRepository { get;  }
        public IEmployeeRepository employeeRepository{get;}
        void Save();
        Task<int>saveAsync();
    }
}