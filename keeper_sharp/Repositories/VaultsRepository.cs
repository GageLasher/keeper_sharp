using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keeper_sharp.Models;

namespace keeper_sharp.Repositories
{
    public class VaultsRepository
    {
        private readonly IDbConnection _db;

        public VaultsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Vault Create(Vault data)
        {
            string sql = @"
                INSERT INTO vaults
                (name, description, img, creatorId, isPrivate)
                VALUES
                (@Name, @Description, @Img, @CreatorId, @IsPrivate);
                SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, data);
            data.Id = id;
            return data;
        }

        internal Vault GetById(int id)
        {
            string sql = @"
            SELECT v.*,
            a.*
            FROM vaults v
            JOIN accounts a ON v.creatorId = a.id
            WHERE v.id = @id;
            ";
            return _db.Query<Vault, Account, Vault>(sql, (v, a) =>
            {
                v.Creator = a;
                return v;
            }, new { id }).FirstOrDefault();
        }

        internal Vault Update(Vault original)
        {
            string sql = @"
            UPDATE vaults
            SET
            name = @Name,
            description = @Description,
            img = @Img,
            isPrivate = @IsPrivate
            WHERE id = @Id;
            ";
            int rows = _db.Execute(sql, original);
            if (rows > 0)
            {
                return original;
            }
            throw new Exception("Sql error on update vaults, no rows affected");
        }

        internal List<Vault> GetVaultsByProfileId(string id)
        {
            string sql = @"
            SELECT
            v.*,
            a.*
            FROM vaults v
            JOIN accounts a ON v.creatorId = a.id
            WHERE v.creatorId = @id;
            ";
            return _db.Query<Vault, Account, Vault>(sql, (v, a) =>
            {
                v.Creator = a;
                return v;
            }, new { id }).ToList();
        }

        internal string Remove(int id1)
        {
            string sql = @"
            DELETE FROM vaults WHERE id = @id1 LIMIT 1;
            ";
            int rows = _db.Execute(sql, new { id1 });
            if (rows > 0)
            {
                return "Keep deleted";
            }
            throw new Exception("Sql error on remove vaults, no rows affected");
        }
    }
}