using System;
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

        internal string Delete(int id)
        {

            string sql = @"
            DELETE FROM vaultKeeps WHERE id = @id LIMIT 1;
            ";
            int rowsAffected = _db.Execute(sql, new { id });
            if (rowsAffected > 0)
            {
                return "delorted";
            }
            throw new Exception("could not delete");
        }
    }
}