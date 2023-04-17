using Dapper;
using Hotel.Application.Dto;
using Hotel.Application.Helper;
using Hotel.Application.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZstdSharp.Unsafe;
using static System.Reflection.Metadata.BlobBuilder;

namespace Hotel.Infrastructure.Repositories
{
    public class PurseRepository : IPurseRepository
    {
        private readonly string connectionSqlString;
        private readonly string connectionString;
        private readonly string booksCollectionNameLog;
        private readonly string databaseName;
        private readonly ILogRepository _logRepository;
        private readonly IMongoCollection<LogDto> _logs;

        public PurseRepository(string connectionSqlString, 
            string connectionString,
            string databaseName,
            string booksCollectionNameLog, ILogRepository logRepository)
        {
            this.connectionSqlString = connectionSqlString;
            this.connectionString = connectionString;
            this.databaseName = databaseName;
            this.booksCollectionNameLog = booksCollectionNameLog;
            _logRepository = logRepository;

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);

            _logs = database.GetCollection<LogDto>(booksCollectionNameLog);
        }

        public async Task<PurseDto> Get(string usersId)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(connectionSqlString))
                {
                    string sql = @"SELECT * FROM Purse WHERE IdUsers = @usersId";
                    var purse = await db
                        .QueryFirstOrDefaultAsync<PurseDto>(sql, new { usersId });
                    db.Close();
                    db.Dispose();
                    return purse;
                }
            }
            catch (Exception ex)
            {
                var logs = Helper.Log(ex.Message + "Id do Usuário: " + usersId, "AddPurseRepository");
                await _logRepository.Add(logs);
                throw;
            }
        }
        public async Task<int> Add(PurseDto purse)
        {
            int rows = 0;
            try
            {
                using (IDbConnection db = new SqlConnection(connectionSqlString))
                {
                    string insertPurseSQL = @"INSERT INTO Purse (IdUsers, Value, CreatedAt, UpdatedAt)
VALUES (@IdUsers, @Value, @CreatedAt, @UpdatedAt)
                SELECT CAST(SCOPE_IDENTITY() as int)";

                    DynamicParameters purseParameters = new DynamicParameters();
                    purseParameters.Add("@IdUsers", purse.IdUsers);
                    purseParameters.Add("@Value", purse.Value);
                    purseParameters.Add("@CreatedAt", DateTime.Now);
                    purseParameters.Add("@UpdatedAt", DateTime.Now);

                    var id = db.QueryAsync<int>(insertPurseSQL, purseParameters).Result;
                    rows =  id.Single();
                    db.Close();
                    db.Dispose();
                }
            }
            catch (Exception ex)
            {
                var logs = Helper.Log(ex.Message + "Id do Usuário: " + purse.IdUsers, "AddPurseRepository");
                await _logRepository.Add(logs);
                rows = 0;
                throw;
            }
            return rows;
        }
        public async Task<int> Update(PurseHistoryDto purse)
        {
            int rows = 0;
            using (IDbConnection db = new SqlConnection(connectionSqlString))
            {
                db.Open();
                using (var transactionScope = db.BeginTransaction())
                {
                    try
                    {
                        string updatePurse = @"update Purse SET Value = @Value, UpdatedAt = @UpdatedAt
                            where Id = @Id";
                        db.Query<int>(updatePurse, new
                        {
                            Id = purse.IdPurse,
                            Value = purse.Value,
                            UpdatedAt = DateTime.Now,
                        }, transactionScope);

                        string insertPurseHistory = @"INSERT INTO PurseHistory  (IdPurse, Value, ValueAdded, 
PreviousValue, WithdrawnAmount, CreatedAt)
VALUES (@IdPurse, @Value, @ValueAdded, @PreviousValue, @WithdrawnAmount, @CreatedAt)
SELECT CAST(SCOPE_IDENTITY() as int)";

                        rows = db.Query<int>(insertPurseHistory, new
                        {
                            IdPurse = purse.Id,
                            Value = purse.Value,
                            ValueAdded = purse.ValueAdded,
                            PreviousValue = purse.PreviousValue,
                            WithdrawnAmount = purse.WithDrawnAmont,
                            CreatedAt = DateTime.Now
                        }, transactionScope).Single();

                        transactionScope.Commit();
                    }
                    catch (Exception ex)
                    {
                        transactionScope.Rollback();
                        rows = 0;
                    }
                    db.Close();
                    db.Dispose();
                    return rows;
                }
            }
        }
    }
}
