using System;
using System.Collections.Generic;
using keeper_sharp.Models;
using keeper_sharp.Repositories;

namespace keeper_sharp.Services
{
    public class VaultsService
    {
        private readonly VaultsRepository _vaultsRepo;

        public VaultsService(VaultsRepository vaultsRepo)
        {
            _vaultsRepo = vaultsRepo;
        }

        internal Vault Create(Vault data)
        {
            return _vaultsRepo.Create(data);
        }

        internal Vault GetById(int id, string userId)
        {
            Vault found = _vaultsRepo.GetById(id);
            if (found.IsPrivate & found.CreatorId != userId)
            {
                throw new Exception("this is a private vault");
            }

            if (found == null)
            {
                throw new Exception("no vault by that id");
            }
            return found;
        }

        internal Vault Edit(Vault updateData)
        {
            Vault original = GetById(updateData.Id, updateData.CreatorId);
            if (updateData.CreatorId != original.CreatorId)
            {
                throw new System.Exception("Cannot Edit, not your vault");
            }
            original.Name = updateData.Name ?? original.Name;
            original.Description = updateData.Description ?? original.Description;
            original.Img = updateData.Img ?? original.Img;
            original.IsPrivate = updateData.IsPrivate;
            return _vaultsRepo.Update(original);
        }

        internal void Remove(int id1, string id2)
        {
            Vault found = GetById(id1, id2);
            if (found.CreatorId != id2)
            {
                throw new System.Exception("This isn't yours to delete");
            }
            _vaultsRepo.Remove(id1);
        }

        internal List<Vault> GetVaultsByProfileId(string id)
        {
            List<Vault> vaults = _vaultsRepo.GetVaultsByProfileId(id);
            List<Vault> filtered = vaults.FindAll(v => v.IsPrivate != true);
            return filtered;

        }
    }
}