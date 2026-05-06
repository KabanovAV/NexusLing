using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexusLing.Application.DTOs;
using NexusLing.Application.Interfaces;

namespace NexusLing.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ApiController
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        /// <summary>
        /// Получение списка пользователей
        /// </summary>
        /// <returns>Возвращает список пользователей</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUser()
        {
            var result = await _service.GetAllUserAsync();
            return Ok(result.Value);
        }

        /// <summary>
        /// Получение пользователя по Id
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <returns>Возвращает пользователя</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="404">Пользователь не найден</response>
        [HttpGet("{userId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDTO>> GetUser([FromRoute] Guid userId)
        {
            var result = await _service.GetUserByIdAsync(userId);
            return HandleOkResult(result);
        }

        /// <summary>
        /// Добавить нового пользователя
        /// </summary>
        /// <param name="rUser">Пользователь</param>
        /// <returns>Возвращает нового пользователя</returns>
        /// <response code="201">Успешное выполнение запроса</response>
        /// <response code="400">Ошибка валидации данных</response>
        [HttpPost, Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDTO>> AddUser([FromBody] RegisterUserDTO rUser)
        {
            var result = await _service.AddUserAsync(rUser);
            return HandleCreatedResult(nameof(GetUser), () => new { id = result.Value.Id }, result);
        }

        /// <summary>
        /// Обновление данных пользователя
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <param name="uUser">Измененные данные пользователя</param>
        /// <response code="204">Успешное выполнение запроса</response>
        /// <response code="400">Некорректный запрос</response>
        /// <response code="404">Пользователь не найден</response>
        [HttpPatch("{userId:guid}/profile"), Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid userId, [FromBody] UpdateUserDTO uUser)
        {
            var result = await _service.UpdateUserAsync(userId, uUser);
            return HandleNoContentResult(result);
        }

        /// <summary>
        /// Обновление пароля пользователя
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <param name="uPassword">Новый пароль пользователя</param>
        /// <response code="204">Успешное выполнение запроса</response>
        /// <response code="400">Некорректный запрос</response>
        /// <response code="404">Пользователь не найден</response>
        [HttpPatch("{userId:guid}/password"), Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePassword([FromRoute] Guid userId, [FromBody] ChangePasswordDTO uPassword)
        {
            var result = await _service.UpdatePasswordAsync(userId, uPassword);
            return HandleNoContentResult(result);
        }

        /// <summary>
        /// Удаление данных о пользователе
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <response code="204">Успешное выполнение запроса</response>
        [HttpDelete("{userId:guid}"), Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid userId)
        {
            await _service.DeleteUserAsync(userId);
            return NoContent();
        }
    }
}
