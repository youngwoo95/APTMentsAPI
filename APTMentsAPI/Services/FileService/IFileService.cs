namespace APTMentsAPI.Services.FileService
{
    public interface IFileService
    {
        /// <summary>
        /// 이미지 저장
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="targetfolder"></param>
        /// <returns></returns>
        Task<bool> AddImageFile(string filePath, string targetfolder);

        /// <summary>
        /// 이미지 조회
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="targetfolder"></param>
        /// <returns></returns>
        Task<byte[]?> GetImageFile(string filePath, string targetfolder);

        /// <summary>
        /// 이미지 삭제
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="targetfolder"></param>
        /// <returns></returns>
        Task<bool> DeleteImageFile(string filePath, string targetfolder);
    }
}
