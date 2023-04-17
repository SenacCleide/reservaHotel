using Microsoft.AspNetCore.Mvc;

namespace Hotel.WebApi.Presenter
{
    public sealed class JsonContentResult : ContentResult
    {
        public JsonContentResult()
        {
            ContentType = "application/json";

        }
    }
}
