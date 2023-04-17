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
    public class StayHotelRepository : IStayHotelRepository
    {
        private readonly ILogRepository _logRepository;
        private readonly string connectionString;
        private readonly string booksCollectionNameStayHotel;
        private readonly string booksCollectionNameUserStay;
        private readonly string databaseName;
        private readonly IMongoCollection<StayHotelDto> _stayHotel;
        private readonly IMongoCollection<StayHotelUserDto> _userStay;

        public StayHotelRepository(string connectionString, string databaseName,
            string booksCollectionNameStayHotel, string booksCollectionNameUserStay, ILogRepository logRepository)
        {
            this.connectionString = connectionString;
            this.databaseName = databaseName;
            this.booksCollectionNameStayHotel = booksCollectionNameStayHotel;
            this.booksCollectionNameUserStay = booksCollectionNameUserStay;

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);

            _stayHotel = database.GetCollection<StayHotelDto>(booksCollectionNameStayHotel);
            _userStay = database.GetCollection<StayHotelUserDto>(booksCollectionNameUserStay);
            _logRepository = logRepository;
        }
        public async Task<StayHotelDto> Add(StayHotelDto stay)
        {
            stay.UpdatedAt = null;
            try
            {
                _stayHotel.InsertOne(stay);
                return stay;
            }
            catch (Exception ex)
            {
                var logs = Helper.Log(ex.Message + "Nome do Hotel: " + stay.Name, "AddStayRepository");
                await _logRepository.Add(logs);
                return stay;
            }
        }
        public async Task<List<StayHotelDto>> ListStayHotel(bool isReserved)
        {
            var result = new List<StayHotelDto>();

            try
            {
                var filtro = new BsonDocument("IsReserved", isReserved);
                return _stayHotel.FindSync<StayHotelDto>(filtro).ToList();
            }
            catch (Exception ex)
            {
                var logs = Helper.Log(ex.Message + "Listar Reservas: ", "ListIStayHotelRepository");
                await _logRepository.Add(logs);
                return result;
            }
        }
        public async Task<StayHotelDto> GetStayHotel(string id, bool isReserved)
        {
            var result = new StayHotelDto();

            try
            {
                var filtro = new BsonDocument("IsReserved", isReserved).Add("_id", ObjectId.Parse(id));
                return _stayHotel.FindSync<StayHotelDto>(filtro).First();
            }
            catch (Exception ex)
            {
                var logs = Helper.Log(ex.Message + "Buscar Reservas: ", "GetStayHotel");
                await _logRepository.Add(logs);
                return result;
            }
        }
        public async Task<bool> Update(StayHotelDto stayHotel, StayHotelUserDto stay)
        {
            var result = false;
            try
            {
                Expression<Func<StayHotelDto, bool>> filter = m => (m.Id == stayHotel.Id)
                                && (m.UpdatedAt == null || m.CreatedAt < DateTime.UtcNow); // valiar o UpdatedAt se está correto 

                var update = Builders<StayHotelDto>.Update
                    .Set(m => m.IsReserved, stayHotel.IsReserved);

                var options = new FindOneAndUpdateOptions<StayHotelDto, StayHotelDto>
                {
                    IsUpsert = false,
                    ReturnDocument = ReturnDocument.After
                };

                var instance = _stayHotel.FindOneAndUpdateAsync(filter, update, options).Result;
                result = instance?.IsReserved == false;

                if(!!result)
                    _userStay.InsertOne(stay);

                return result;
            }
            catch (Exception ex)
            {
                var logs = Helper.Log(ex.Message + "Atualizar e Add Users Reservas: ", "UpdateStayHotelUser");
                await _logRepository.Add(logs);
                return result;
            }            
        }
        public async Task<StayHotelUserDto> AddUserStay(StayHotelUserDto stay)
        {
            try
            {
                _userStay.InsertOne(stay);
                return stay;
            }
            catch (Exception ex)
            {
                var logs = Helper.Log(ex.Message + "Erro para Adicionar reserva de Usuário: " + stay.Tennant, "AddUserStay_UserRepository");
                await _logRepository.Add(logs);
                return stay;
            }
        }
    }
}
