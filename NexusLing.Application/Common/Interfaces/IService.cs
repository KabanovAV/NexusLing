using NexusLing.Application.Interfaces;

namespace NexusLing.Application.Common.Interfaces
{
    public interface IService
    {
        IUserService UserService { get; }
    }
}
