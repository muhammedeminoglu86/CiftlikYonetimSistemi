using CiftlikYonetimSistemi.Domain.Models;

namespace CiftlikYonetimSistemi.Business.Interfaces
{
    public interface IJavascriptService
    {
		Task<int> AddAsync(Javascript user);
		Task UpdateAsync(Javascript user);
		Task DeleteAsync(int id);
		Task<IEnumerable<Javascript>> GetAllAsync(string query, object param);
		Task<Javascript> GetOne(string query, object param);
	}
}