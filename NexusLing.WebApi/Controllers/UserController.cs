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
        /// <param name="id">Id пользователя</param>
        /// <returns>Возвращает пользователя</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="404">Пользователь не найден</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<User>> GetUser([FromRoute] Guid id)
        {
            var user = await _service.GetUserAsync(id);
            if (user == null)
            {
                return NotFound(new { Message = $"Пользователь с id {id} не найден." });
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
        /// <param name="id">Id пользователя</param>
        /// <param name="uUser">Измененные данные пользователя</param>
        /// <response code="204">Успешное выполнение запроса</response>
        /// <response code="404">Пользователь не найден</response>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id, [FromBody] UpdateUserDTO uUser)
        {
            if (uUser == null)
            {
                return BadRequest(new { Message = "Данные для добавления пользователя пустые." });
            }
            await _service.UpdateUserAsync(id, uUser);
            return NoContent();
        }

        /// <summary>
        /// Удаление данных о пользователе
        /// </summary>
        /// <param name="id">Id пользователя</param>
        /// <response code="204">Успешное выполнение запроса</response>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            await _service.DeleteUserAsync(id);
            return NoContent();
        }
    }
}
