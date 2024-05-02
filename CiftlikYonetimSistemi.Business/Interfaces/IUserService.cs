using System.Collections.Generic;
using System.Threading.Tasks;
using CiftlikYonetimSistemi.Business.DTO;
using CiftlikYonetimSistemi.Domain.Models;

public interface IUserService
{
	Task<int> AddAsync(User user);
	Task UpdateAsync(User user);
	Task DeleteAsync(int id);
	Task<IEnumerable<User>> GetAllAsync(string query, object param);
	Task<User> GetOne(string query, object param);
	Task<User> ValidateLoginAsync(LoginDTO loginDTO);

}
