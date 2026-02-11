using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var user = await _service.GetUserAsync(id);
            if (user == null)
            {
                return NotFound(new { Message = $"Пользователь с id {id} не найден." });
            }
            return Ok();
        }

        /// <summary>
        /// Добавить нового пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns>Возвращает нового пользователя</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="400">Пустой обьект пользователя</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<User>> AddUser(User user)
        {
            if (user == null)
            {
                return BadRequest(new { Message = "Данные для добавления пользователя пустые." });
            }
            await _service.AddUserAsync(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        /// <summary>
        /// Обновление данных пользователя
        /// </summary>
        /// <param name="id">Id пользователя</param>
        /// <param name="user">Измененные данные пользователя</param>
        /// <response code="204">Успешное выполнение запроса</response>
        /// <response code="400">Пустой обьект пользователя</response>
        /// <response code="404">Пользователь не найден</response>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateUser(Guid id, User user)
        {
            if (user == null)
            {
                return BadRequest(new { Message = "Данные для добавления пользователя пустые." });
            }
            var currentUser = await _service.GetUserAsync(id);
            if (currentUser == null)
            {
                return NotFound(new { Message = $"Пользователь с id {id} не найден." });
            }

            currentUser.FirstName = user.FirstName;
            currentUser.LastName = user.LastName;
            currentUser.Login = user.Login;
            currentUser.Password = user.Password;

            await _service.UpdateUserAsync(currentUser);
            return NoContent();
        }

        /// <summary>
        /// Удаление данных о пользователе
        /// </summary>
        /// <param name="id">Id пользователя</param>
        /// <response code="204">Успешное выполнение запроса</response>
        /// <response code="404">Пользователь не найден</response>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _service.GetUserAsync(id);
            if (user == null)
            {
                return NotFound(new { Message = $"Пользователь с id {id} не найден." });
            }
            await _service.DeleteUserAsync(user);
            return NoContent();
        }
    }
}
