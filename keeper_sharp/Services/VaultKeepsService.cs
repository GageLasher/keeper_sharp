using keeper_sharp.Models;
using keeper_sharp.Repositories;

namespace keeper_sharp.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _vaultKeepsRepo;

        public VaultKeepsService(VaultKeepsRepository vaultKeepsRepo)
        {
            _vaultKeepsRepo = vaultKeepsRepo;
        }

        internal VaultKeep Create(VaultKeep data)
        {
            return _vaultKeepsRepo.Create(data);
        }
    }
}