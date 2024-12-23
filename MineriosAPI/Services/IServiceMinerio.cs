using MineriosAPI.DTOs;
using MineriosAPI.Models;

namespace MineriosAPI.Services
{
    public interface IServiceMinerio
    {
        Task<IEnumerable<MineriosModel>> GetAllMinerios();
        Task<MineriosModel> GetMinerioById(int id);
        Task<MineriosModel> UpdateMinerio(int id, UpdateMinerioDto updateMinerio);
        Task<MineriosModel> CreateMinerio(CreateMinerioDto createMinerio);
        Task<MineriosModel> DeleteMinerio(int id);
        Task DeleteAllMinerios();
    }
}

