using System;
using keeper_sharp.Models;
using keeper_sharp.Repositories;

namespace keeper_sharp.Services
{
    public class ProfilesService
    {
        private readonly ProfilesRepository _profilesRepo;

        public ProfilesService(ProfilesRepository profilesRepo)
        {
            _profilesRepo = profilesRepo;
        }

        internal Account GetById(string id)
        {
            Account found = _profilesRepo.GetById(id);
            if (found == null)
            {
                throw new Exception("No profile by that id");
            }
            return found;
        }
    }
}