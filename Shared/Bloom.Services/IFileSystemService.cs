using Bloom.State.Domain.Models;

namespace Bloom.Services
{
    public interface IFileSystemService
    {
        string CopyProfileImage(User user, string filePath);
    }
}
