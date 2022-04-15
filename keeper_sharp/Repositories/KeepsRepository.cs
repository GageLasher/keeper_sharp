using System.Data;
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
    }
}