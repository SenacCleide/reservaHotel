using Hotel.Application.Dto;
using Hotel.Application.Helper;
using Hotel.Application.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ILogRepository _logRepository;
        private readonly string connectionString;
        private readonly string databaseName;
        private readonly string booksCollectionNameUser;
        private readonly IMongoCollection<UserDto> _user;
        private readonly MongoClient client;

        public UserRepository(string connectionString, string databaseName,
            string booksCollectionNameUser, ILogRepository logRepository)
        {
            this.connectionString = connectionString;
            this.databaseName = databaseName;

            client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);

            _user = database.GetCollection<UserDto>(booksCollectionNameUser);
            _logRepository = logRepository;
        }
        public async Task<UserDto> GetUserEmail(AuthUserDto user)
        {
            var result = new UserDto();
            var password = Helper.GetHashMD5(user.Password);
            try
            {
                var filtro = new BsonDocument("Email", user.Email).Add("Password", password);
                return _user.FindSync<UserDto>(filtro).FirstOrDefault();
            }
            catch (Exception ex)
            {
                var logs = Helper.Log(ex.Message + "Email para consulta: " + user.Email, "GetUserEmailRepository");
                await _logRepository.Add(logs);
                return result;
            }

            
        }
        public async Task<UserDto> AddUser(UserDto user)
        {
            try
            {
                user.Password = Helper.GetHashMD5(user.Password);
                _user.InsertOne(user);  
                return user;
            }
            catch (Exception ex)
            {
                var logs = Helper.Log(ex.Message + "Email do Usuário: " + user.Email, "AddUser");
               await _logRepository.Add(logs);
                return user;
            }
        }
    }
}
