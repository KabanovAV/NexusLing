using Microsoft.AspNetCore.Mvc;
using NexusLing.Application.DTOs;
using NexusLing.Application.Interfaces;

namespace NexusLing.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ApiController
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <returns>Возвращает токен</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> Login([FromBody] LoginDTO login)
        {
            var tokenResult = await _service.LoginAsync(login);
            return HandleOkResult(tokenResult);
        }
    }
}
