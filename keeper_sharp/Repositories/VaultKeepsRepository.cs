using System.Data;
using Dapper;
using keeper_sharp.Models;

namespace keeper_sharp.Repositories
{
    public class VaultKeepsRepository
    {
        private readonly IDbConnection _db;

        public VaultKeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal VaultKeep Create(VaultKeep data)
        {
            string sql = @"
                INSERT INTO vaultKeeps
                (vaultId, keepId, creatorId)
                VALUES
                (@VaultId, @KeepId, @CreatorId);
                SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, data);
            data.Id = id;
            return data;
        }
    }
}