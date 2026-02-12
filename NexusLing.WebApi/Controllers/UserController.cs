using Microsoft.AspNetCore.Mvc;
using NexusLing.Application.DTOs;
using NexusLing.Application.Interfaces;
using NexusLing.Domain.Entities;

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
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUser()
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
        public async Task<ActionResult<User>> GetUser([FromRoute] Guid userId)
        {
            var user = await _service.GetUserAsync(userId);
            if (user == null)
            {
                return NotFound(new { Message = $"Пользователь с id {userId} не найден." });
            }
            return Ok(user);
        }

        /// <summary>
        /// Добавить нового пользователя
        /// </summary>
        /// <param name="rUser">Пользователь</param>
        /// <returns>Возвращает нового пользователя</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="400">Пустой обьект пользователя</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<User>> AddUser([FromBody] RegisterUserDTO rUser)
        {
            if (rUser == null)
            {
                return BadRequest(new { Message = "Данные для добавления пользователя пустые." });
            }
            var user = await _service.AddUserAsync(rUser);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        /// <summary>
        /// Обновление данных пользователя
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <param name="uUser">Измененные данные пользователя</param>
        /// <response code="204">Успешное выполнение запроса</response>
        /// <response code="404">Пользователь не найден</response>
        /// <response code="404">Id не совпадают</response>
        [HttpPut("{userId:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid userId, [FromBody] UpdateUserDTO uUser)
        {
            if (uUser == null)
            {
                return BadRequest(new { Message = "Данные для добавления пользователя пустые." });
            }
            if (userId != uUser.Id)
            {
                return BadRequest(new { Message = "ID в URL и ID в теле запроса не совпадаю." });
            }
            await _service.UpdateUserAsync(userId, uUser);
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
