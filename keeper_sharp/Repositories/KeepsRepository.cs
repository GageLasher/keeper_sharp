using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keeper_sharp.Models;

namespace keeper_sharp.Repositories
{
    public class KeepsRepository
    {
        private readonly IDbConnection _db;

        public KeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Keep Create(Keep data)
        {
            string sql = @"
                INSERT INTO keeps
                (name, description, img, creatorId)
                VALUES
                (@Name, @Description, @Img, @CreatorId);
                SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, data);
            data.Id = id;
            return data;
        }

        internal Keep GetById(int id)
        {
            string sql = @"
            SELECT k.*,
            a.*
            FROM keeps k
            JOIN accounts a ON k.creatorId = a.id
            WHERE k.id = @id;
            ";
            return _db.Query<Keep, Account, Keep>(sql, (k, a) =>
            {
                k.Creator = a;
                return k;
            }, new { id }).FirstOrDefault();
        }

        internal Keep Update(Keep original)
        {
            string sql = @"
            UPDATE keeps
            SET
            name = @Name,
            description = @Description,
            img = @Img
            WHERE id = @Id;
            ";
            int rows = _db.Execute(sql, original);
            if (rows > 0)
            {
                return original;
            }
            throw new Exception("Sql error on update keeps, no rows affected");
        }

        internal string Remove(int id)
        {
            string sql = @"
            DELETE FROM keeps WHERE id = @id LIMIT 1;
            ";
            int rows = _db.Execute(sql, new { id });
            if (rows > 0)
            {
                return "Keep deleted";
            }
            throw new Exception("Sql error on remove keeps, no rows affected");
        }

        internal List<Keep> GetAll()
        {
            string sql = @"
            SELECT k.*,
            a.*
            FROM keeps k
            JOIN accounts a ON k.creatorId = a.id;

            ";
            return _db.Query<Keep, Account, Keep>(sql, (k, a) =>
            {
                k.Creator = a;
                return k;
            }).ToList();
        }
    }
}