namespace APTMentsAPI.Services.FileService
{
    public interface IFileService
    {
        Task<bool> AddImageFile(string filePath, string targetfolder);

        Task<byte[]?> GetImageFile(string filePath, string targetfolder);
    }
}
