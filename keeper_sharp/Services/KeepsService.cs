using System;
using System.Collections.Generic;
using keeper_sharp.Models;
using keeper_sharp.Repositories;

namespace keeper_sharp.Services
{
    public class KeepsService
    {
        private readonly KeepsRepository _keepsRepo;

        public KeepsService(KeepsRepository keepsRepo)
        {
            _keepsRepo = keepsRepo;
        }

        internal Keep Create(Keep data)
        {
            return _keepsRepo.Create(data);
        }

        internal List<Keep> GetAll()
        {
            return _keepsRepo.GetAll();
        }

        internal Keep GetById(int id)
        {
            Keep found = _keepsRepo.GetById(id);
            if (found == null)
            {
                throw new Exception("no keep by that id");
            }
            return found;
        }

        internal Keep Edit(Keep updateData)
        {
            Keep original = GetById(updateData.Id);
            if (updateData.CreatorId != original.CreatorId)
            {
                throw new System.Exception("Cannot Edit, not your keep");
            }
            original.Name = updateData.Name ?? original.Name;
            original.Description = updateData.Description ?? original.Description;
            original.Img = updateData.Img ?? original.Img;
            return _keepsRepo.Update(original);
        }
        internal void Remove(int id, string userId)
        {
            Keep found = GetById(id);
            if (found.CreatorId != userId)
            {
                throw new System.Exception("This isn't yours to delete");
            }
            _keepsRepo.Remove(id);
        }

        internal List<VaultKeepViewModel> GetVaultKeepsByVaultId(int id)
        {
            return _keepsRepo.GetVaultKeepsByVaultId(id);
        }
    }
}