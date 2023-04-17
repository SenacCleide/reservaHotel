using Hotel.Application.Dto;
using Hotel.WebApi.Serialization;
using System.Net;

namespace Hotel.WebApi.Presenter
{
    public sealed class UserPresenter
    {
        public JsonContentResult ContentResult { get; }

        public UserPresenter()
        {
            ContentResult = new JsonContentResult();
        }
        public void Populate(ResponseDto dto)
        {
            if (dto == null)
            {
                ContentResult.StatusCode = (int)(HttpStatusCode.NoContent);
                return;
            }
            else if(dto.Message == "USUÁRIO INVÁLIDO")
            {
                ContentResult.StatusCode = (int)(HttpStatusCode.Unauthorized);
                return;
            }

            ContentResult.StatusCode = (int)(HttpStatusCode.OK);
            ContentResult.Content = JsonSerializer.SerializeObject(dto);
        }
    }
}
