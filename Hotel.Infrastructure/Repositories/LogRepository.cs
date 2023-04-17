using Hotel.Application.Dto;
using Hotel.Application.Helper;
using Hotel.Application.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZstdSharp.Unsafe;
using static System.Reflection.Metadata.BlobBuilder;

namespace Hotel.Infrastructure.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly string connectionString;
        private readonly string booksCollectionNameLog;
        private readonly string databaseName;
        private readonly IMongoCollection<LogDto> _logs;

        public LogRepository(string connectionString, string databaseName,
            string booksCollectionNameLog)
        {
            this.connectionString = connectionString;
            this.databaseName = databaseName;
            this.booksCollectionNameLog = booksCollectionNameLog;

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);

            _logs = database.GetCollection<LogDto>(booksCollectionNameLog);
        }
        public async Task<LogDto> Add(LogDto log)
        {
            try
            {
                _logs.InsertOneAsync(log);
                return log;
            }
            catch (Exception ex)
            {
                return log;
            }
        }
    }
}
