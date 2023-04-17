using Hotel.Application.Dto;
using Hotel.Application.Repositories;
using Hotel.Application.UseCases.AuthUse;
using Hotel.Application.UseCases.Purse.AddPurse;
using Hotel.Application.UseCases.StayHotel.AddStayHotelUseCase;
using Hotel.Application.UseCases.StayHotel.ListStayHotel;
using Hotel.Application.UseCases.Users.AddUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Hotel.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly IAddStayHotelUseCase _addStayHotelUseCase;
        private readonly IListStayHotelUseCase _listStayHotelUseCase;
        private readonly IHttpContextAccessor _accessor;
        private readonly IConfiguration _configuration;

        public HotelController(IAddStayHotelUseCase addStayHotelUseCase,
            IListStayHotelUseCase listStayHotelUseCase,
            IHttpContextAccessor accessor,
            IConfiguration configuration)
        {
            _addStayHotelUseCase = addStayHotelUseCase;
            _listStayHotelUseCase = listStayHotelUseCase;
            _accessor = accessor;
            _configuration = configuration;
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("AddStayHotel")]
        public async Task<IActionResult> AddStayHotel(StayHotelDto stayHotel)
        {
            ResponseDto result = await _addStayHotelUseCase.Execute(stayHotel);
            return Ok(result);
        }

        [HttpGet("List")]
        public async Task<IActionResult> List(bool reservated)
        {
            var result = await _listStayHotelUseCase.Execute(reservated);
            return Ok(result);
        }

        /// <summary>
        /// Get Download 
        /// </summary>
        /// <remarks>
        /// Para retornar o pdf em pagamento, enviar o id da reserva e underline Pay Exemplo: 6434599b500f6b6029055f86_Pay
        /// Para retornar o pdf da reserva enviar apenas o id da reserva, exemplo: 6434599b500f6b6029055f86
        /// </remarks>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("DownloadStay")]
        public async Task<IActionResult> DownloadStay(string file)
        {
            var folder = _configuration.GetSection("modules:0:Path:Folder").Get<string>();
            string path = folder + file + ".pdf";

            if (System.IO.File.Exists(path))
            {
                return File(System.IO.File.OpenRead(path), "application/octet-stream", Path.GetFileName(path));
            }
            return NotFound();
        }
    }
}