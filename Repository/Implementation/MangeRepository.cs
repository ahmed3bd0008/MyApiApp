using Contracts.Interface;
using Entity.Context;
namespace Repository.Implementation
{
            public class MangeRepository : IMangeRepository
            {
                        public MangeRepository(RepoDbContext repoDbContext)
                        {
                            _repoDbContext=repoDbContext;
                        }
                       private CompanyRepository _companyRepository;
                       private EmployeeRepository _employeeRepository;
                        private readonly RepoDbContext _repoDbContext;

                        public IComponyRepository componyRepository
                        {get{
                                    if(_companyRepository==null)
                                                _companyRepository=new CompanyRepository(_repoDbContext);
                                    return _companyRepository;
                        }
                        }

                        public IEmployeeRepository employeeRepository {get
                            {
                              if (_employeeRepository==null)
                              {
                                  _employeeRepository=new EmployeeRepository(_repoDbContext);
                              }
                               return _employeeRepository;         
                            }}

                        public void Save()
                        {
                                    _repoDbContext.SaveChanges();
                        }
            }
}