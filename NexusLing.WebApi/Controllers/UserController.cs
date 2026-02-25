using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexusLing.Application.DTOs;
using NexusLing.Application.Interfaces;

namespace NexusLing.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
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
        [Authorize]
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUser()
            => Ok(await _service.GetAllUserAsync());

        /// <summary>
        /// Получение пользователя по Id
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <returns>Возвращает пользователя</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="404">Пользователь не найден</response>
        [HttpGet("{userId:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<UserDTO>> GetUser([FromRoute] Guid userId)
        {
            var user = await _service.GetUserByIdAsync(userId);
            return Ok(user);
        }

        /// <summary>
        /// Добавить нового пользователя
        /// </summary>
        /// <param name="rUser">Пользователь</param>
        /// <returns>Возвращает нового пользователя</returns>
        /// <response code="201">Успешное выполнение запроса</response>
        /// <response code="400">Ошибка валидации данных</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<UserDTO>> AddUser([FromBody] RegisterUserDTO rUser)
        {
            var user = await _service.AddUserAsync(rUser);
            return CreatedAtAction(nameof(GetUser), new { userId = user.Id }, user);
        }

        /// <summary>
        /// Обновление данных пользователя
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <param name="uUser">Измененные данные пользователя</param>
        /// <response code="204">Успешное выполнение запроса</response>
        /// <response code="400">Ошибка валидации данных</response>
        /// <response code="400">Несовпадение идентификаторов</response>
        [HttpPatch("{userId:guid}/profile")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid userId, [FromBody] UpdateUserDTO uUser)
        {
            await _service.UpdateUserAsync(userId, uUser);
            return NoContent();
        }

        /// <summary>
        /// Обновление пароля пользователя
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <param name="uPassword">Новый пароль пользователя</param>
        /// <response code="204">Успешное выполнение запроса</response>
        /// <response code="400">Ошибка валидации данных</response>
        /// <response code="400">Несовпадение идентификаторов</response>
        [HttpPatch("{userId:guid}/password")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdatePassword([FromRoute] Guid userId, [FromBody] ChangePasswordDTO uPassword)
        {
            await _service.UpdatePasswordAsync(userId, uPassword);
            return NoContent();
        }

        /// <summary>
        /// Удаление данных о пользователе
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <response code="204">Успешное выполнение запроса</response>
        [HttpDelete("{userId:guid}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid userId)
        {
            await _service.DeleteUserAsync(userId);
            return NoContent();
        }
    }
}
