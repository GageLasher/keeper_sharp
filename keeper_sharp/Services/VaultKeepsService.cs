using keeper_sharp.Models;
using keeper_sharp.Repositories;

namespace keeper_sharp.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _vaultKeepsRepo;
        private readonly VaultsRepository _vaultsRepo;
        private readonly KeepsRepository _keepsRepo;

        public VaultKeepsService(VaultKeepsRepository vaultKeepsRepo, VaultsRepository vaultsRepo, KeepsRepository keepsRepo)
        {
            _vaultKeepsRepo = vaultKeepsRepo;
            _vaultsRepo = vaultsRepo;
            _keepsRepo = keepsRepo;
        }

        internal VaultKeep Create(VaultKeep data)
        {
            Vault found = _vaultsRepo.GetById(data.VaultId);
            if (found.CreatorId != data.CreatorId)
            {
                throw new System.Exception("bro this isn't your vault");
            }
            Keep keep = _keepsRepo.GetById(data.KeepId);
            keep.Kept++;
            _keepsRepo.Update(keep);

            return _vaultKeepsRepo.Create(data);
        }

        internal void Delete(int id, string userId)
        {
            VaultKeep found = _vaultKeepsRepo.GetById(id);
            if (found.CreatorId != userId)
            {
                throw new System.Exception("This isn't yours to delete");
            }
            _vaultKeepsRepo.Delete(id);
            Keep keep = _keepsRepo.GetById(found.KeepId);
            keep.Kept--;
            _keepsRepo.Update(keep);
        }
    }
}