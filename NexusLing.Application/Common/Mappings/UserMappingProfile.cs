using NexusLing.Application.DTOs;
using NexusLing.Domain.Entities;

namespace NexusLing.Application.Common.Mappings
{
    public static class UserMappingProfile
    {
        /// <summary>
        /// Маппинг из обьекта User в UserDTO
        /// </summary>
        /// <param name="entity">Обьект User</param>
        /// <returns>Возвращает UserDTO</returns>
        public static UserDTO? ToDto(this User entity)
            => entity == null ? null : new(entity.Id, entity.FirstName, entity.LastName, entity.Login.Value);

        /// <summary>
        /// Маппинг списка из обьектов User в список UserDTO
        /// </summary>
        /// <param name="entities">Список User</param>
        /// <returns>Возвращает список UserDTO</returns>
        public static List<UserDTO> ToDto(this IEnumerable<User> entities)
            => [.. entities.Where(e => e != null).Select(e => e.ToDto())];
    }
}
