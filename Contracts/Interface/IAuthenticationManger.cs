using System.Threading.Tasks;
using Entities.DataTransferObject;
using Entities.Model;
namespace Contracts.Interface
{
    public interface IAuthenticationManger
    {
        public User user { get; set; }
        Task<bool>VaildUser(LoginUserDto loginUserDto);
        Task<string>CreateToken();
    }
}