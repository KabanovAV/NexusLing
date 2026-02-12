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
            => entity == null ? null : new(entity.Id, entity.FirstName, entity.LastName, entity.Login);

        /// <summary>
        /// Маппинг списка из обьектов User в список UserDTO
        /// </summary>
        /// <param name="entities">Список User</param>
        /// <returns>Возвращает список UserDTO</returns>
        public static List<UserDTO> ToDto(this IEnumerable<User> entities)
            => [.. entities.Where(e => e != null).Select(e => e.ToDto())];

        /// <summary>
        /// Маппинг из обьекта User в RegisterUserDTO
        /// </summary>
        /// <param name="entity">Обьект User</param>
        /// <returns>Возвращает RegisterUserDTO</returns>
        public static RegisterUserDTO? ToRegisterDto(this User entity)
            => entity == null ? null : new(entity.FirstName, entity.LastName, entity.Login, entity.Password);

        /// <summary>
        /// Маппинг списка из обьектов User в список RegisterUserDTO
        /// </summary>
        /// <param name="entities">Список User</param>
        /// <returns>Возвращает список RegisterUserDTO</returns>
        public static List<RegisterUserDTO> ToRegisterDto(this IEnumerable<User> entities)
            => [.. entities.Where(e => e != null).Select(e => e.ToRegisterDto())];

        /// <summary>
        /// Маппинг из обьекта RegisterUserDTO в User
        /// </summary>
        /// <param name="rDto">Обьект RegisterUserDTO</param>
        /// <returns>Возвращает User</returns>
        public static User? ToEntity(this RegisterUserDTO rDto)
            => rDto == null ? null : new User
            {
                FirstName = rDto.FirstName,
                LastName = rDto.LastName,
                Login = rDto.Login,
                Password = rDto.Password
            };

        /// <summary>
        /// Маппинг списка из обьектов RegisterUserDTO в список User
        /// </summary>
        /// <param name="rDtos">Список RegisterUserDTO</param>
        /// <returns>Возвращает список User</returns>
        public static List<User> ToEntity(this IEnumerable<RegisterUserDTO> rDtos)
            => [.. rDtos.Where(rDto => rDto != null).Select(rDto => rDto.ToEntity())];

        /// <summary>
        /// Маппинг из обьекта User в UpdateUserDTO
        /// </summary>
        /// <param name="entity">Обьект User</param>
        /// <returns>Возвращает UpdateUserDTO</returns>
        public static UpdateUserDTO? ToUpdateDto(this User entity)
            => entity == null ? null : new(entity.Id, entity.FirstName, entity.LastName, entity.Login, entity.Password);

        /// <summary>
        /// Маппинг списка из обьектов User в список UpdateUserDTO
        /// </summary>
        /// <param name="entities">Список User</param>
        /// <returns>Возвращает список UpdateUserDTO</returns>
        public static List<UpdateUserDTO> ToUpdateDto(this IEnumerable<User> entities)
            => [.. entities.Where(e => e != null).Select(e => e.ToUpdateDto())];

        /// <summary>
        /// Маппинг из обьекта UpdateUserDTO в User
        /// </summary>
        /// <param name="uDto">Обьект UpdateUserDTO</param>
        /// <returns>Возвращает User</returns>
        public static User? ToEntity(this UpdateUserDTO uDto)
            => uDto == null ? null : new User
            {
                FirstName = uDto.FirstName,
                LastName = uDto.LastName,
                Login = uDto.Login,
                Password = uDto.Password
            };

        /// <summary>
        /// Маппинг списка из обьектов UpdateUserDTO в список User
        /// </summary>
        /// <param name="uDtos">Список UpdateUserDTO</param>
        /// <returns>Возвращает список User</returns>
        public static List<User> ToEntity(this IEnumerable<UpdateUserDTO> uDtos)
            => [.. uDtos.Where(uDto => uDto != null).Select(uDto => uDto.ToEntity())];

        /// <summary>
        /// Маппинг обновления обьекта User
        /// </summary>
        /// <param name="entity">Обьект User</param>
        /// <param name="dto">Обьект UpdateUserDTO</param>
        public static void UpdateDto(this User entity, UpdateUserDTO dto)
        {
            if (dto == null) return;
            if (dto.FirstName != null && !string.IsNullOrEmpty(dto.FirstName) && entity.FirstName != dto.FirstName)
                entity.FirstName = dto.FirstName;
            if (dto.LastName != null && !string.IsNullOrEmpty(dto.LastName) && entity.LastName != dto.LastName)
                entity.LastName = dto.LastName;
            if (dto.Login != null && !string.IsNullOrEmpty(dto.Login) && entity.Login != dto.Login)
                entity.Login = dto.Login;
            if (dto.Password != null && !string.IsNullOrEmpty(dto.Password) && entity.Password != dto.Password)
                entity.Password = dto.Password;
        }
    }
}
