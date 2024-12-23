using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MineriosAPI.Data;
using MineriosAPI.DTOs;
using MineriosAPI.Models;

namespace MineriosAPI.Services
{
    public class ServiceMinerio : IServiceMinerio
    {
        private readonly ContextMineriosDb _dbContext;

        public ServiceMinerio(ContextMineriosDb dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<MineriosModel>> GetAllMinerios() => await _dbContext.Minerios.ToListAsync();

        public async Task<MineriosModel> GetMinerioById(int id) => await _dbContext.Minerios.Where(m => m.id == id).FirstOrDefaultAsync();

        public async Task<MineriosModel> UpdateMinerio(int id, UpdateMinerioDto updateMinerio) 
        {
            var minerio = await _dbContext.Minerios.FindAsync(id);
            
            minerio.nome = updateMinerio.nome;
            minerio.estado = updateMinerio.estado;
            minerio.descricao = updateMinerio.descricao;

            await _dbContext.SaveChangesAsync();

            return minerio;
        }

        public async Task<MineriosModel> CreateMinerio(CreateMinerioDto createMinerio) 
        {
            var minerio = new MineriosModel
            {
                nome = createMinerio.nome,
                descricao = createMinerio.descricao,
                estado = createMinerio.estado
            };

            await _dbContext.Minerios.AddAsync(minerio);
            await _dbContext.SaveChangesAsync();

            return minerio;
        }

        public async Task<MineriosModel> DeleteMinerio(int id) 
        {
            var deleteMinerio = await _dbContext.Minerios.FindAsync(id);
            
            _dbContext.Minerios.Remove(deleteMinerio);
            await _dbContext.SaveChangesAsync();

            return deleteMinerio;
        }

        public async Task DeleteAllMinerios()
        {
            await _dbContext.Minerios.ExecuteDeleteAsync();
        }
    }
}
