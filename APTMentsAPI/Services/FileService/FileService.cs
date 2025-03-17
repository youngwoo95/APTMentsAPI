
using APTMentsAPI.Services.Logger;
using SkiaSharp;

namespace APTMentsAPI.Services.FileService
{
    public class FileService : IFileService
    {
        private readonly ILoggerService LoggerService;

        public FileService(ILoggerService _loggerservice)
        {
            this.LoggerService = _loggerservice;
        }

        public async Task<bool> AddImageFile(string filePath, string targetfolder)
        {
            try
            {
                string FileFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FileServer", targetfolder);

                // 파일 폴더 없으면 생성
                DirectoryInfo di = new DirectoryInfo(FileFolderPath);
                if (!di.Exists)
                {
                    di.Create();
                }

                using (var client = new HttpClient())
                {
                    // 이미지 데이터를 바이트 배열로 다운로드
                    var imageBytes = await client.GetByteArrayAsync(filePath);


                    Uri uri = new Uri(filePath);
                    string fileName = Path.GetFileName(uri.AbsolutePath);

                    // URL 인코딩이 되어 있을 경우 디코딩 처리
                    fileName = Uri.UnescapeDataString(fileName);

                    // 원하는 경로와 파일명으로 저장합니다.
                    string localPath = Path.Combine(FileFolderPath, fileName);
                    await File.WriteAllBytesAsync(localPath, imageBytes);
                }
                return true;
            }
            catch (HttpRequestException httpEx)
            {
                // 네트워크 관련 문제 로깅
                LoggerService.FileLogMessage($"HTTP 요청 실패: {httpEx}");
                return false;
            }
            catch (IOException ioEx)
            {
                // 파일 I/O 문제 로깅
                LoggerService.FileLogMessage($"파일 I/O 에러: {ioEx}");
                return false;
            }
            catch (Exception ex)
            {
                // 예기치 않은 오류 로깅
                LoggerService.FileLogMessage($"알 수 없는 오류: {ex}");
                return false;
            }
        }

        /// <summary>
        /// 파일 읽기
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public async Task<byte[]?> GetImageFile(string filePath, string targetfolder)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(filePath))
                    return null;

                Uri uri = new Uri(filePath);
                string fileName = Path.GetFileName(uri.AbsolutePath);

                // URL 인코딩이 되어 있을 경우 디코딩 처리
                fileName = Uri.UnescapeDataString(fileName);

                byte[]? fileArr = default;
                string FileFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FileServer", targetfolder);

                string search = Path.Combine(FileFolderPath, fileName);

                // 파일이 있는지 확인한 후 읽기
                if (File.Exists(search))
                {
                    fileArr = await File.ReadAllBytesAsync(search);
                }

                if (fileArr is null)
                    return null;

                // 메모리 스트림 준비
                await using var memoryStream = new MemoryStream(fileArr);
                // 스트림의 위치를 처음으로 돌려 디코딩 준비
                memoryStream.Position = 0;

                using var originalBitmap = SKBitmap.Decode(memoryStream);
                if (originalBitmap == null)
                    return null;

                // 이미지 크기 조정 (비율 유지)
                using var resizedBitmap = ResizeBitmapWithAspectRatio(originalBitmap, 700, 700);
                if (resizedBitmap == null)
                    return null;

                // 인코딩 형식 결정
                // 원본 파일의 확장자에 따라 인코딩 형식 결정
                string extension = Path.GetExtension(fileName).ToLowerInvariant();
                SKEncodedImageFormat encodedFormat = extension switch
                {
                    ".png" => SKEncodedImageFormat.Png,
                    ".bmp" => SKEncodedImageFormat.Bmp,
                    ".gif" => SKEncodedImageFormat.Gif,
                    ".webp" => SKEncodedImageFormat.Webp,
                    _ => SKEncodedImageFormat.Jpeg // 기본은 JPEG
                };

                // 이미지를 바이트 배열로 변환
                using var image = SKImage.FromBitmap(resizedBitmap);
                using var data = image.Encode(encodedFormat, 100);

                return data.ToArray();
            }
            catch (HttpRequestException httpEx)
            {
                // 네트워크 관련 문제 로깅
                LoggerService.FileLogMessage($"HTTP 요청 실패: {httpEx}");
                return null;
            }
            catch (IOException ioEx)
            {
                // 파일 I/O 문제 로깅
                LoggerService.FileLogMessage($"파일 I/O 에러: {ioEx}");
                return null;
            }
            catch (Exception ex)
            {
                LoggerService.FileLogMessage($"알 수 없는 오류: {ex}");
                return null;
            }
        }

        /// <summary>
        /// 리사이징
        /// </summary>
        /// <param name="originalBitmap"></param>
        /// <param name="maxWidth"></param>
        /// <param name="maxHeight"></param>
        /// <returns></returns>
        private SKBitmap? ResizeBitmapWithAspectRatio(SKBitmap originalBitmap, int maxWidth, int maxHeight)
        {
            try
            {
                // 원본 비율 계산
                float aspectRatio = (float)originalBitmap.Width / originalBitmap.Height;

                // 목표 크기에 맞춰 조정된 가로, 세로 계산
                int newWidth = maxWidth;
                int newHeight = maxHeight;

                if (aspectRatio > 1)
                {
                    // 가로가 더 긴 경우, 가로에 맞추고 세로는 비율에 맞게 조정
                    newHeight = (int)(maxWidth / aspectRatio);
                }
                else
                {
                    // 세로가 더 긴 경우, 세로에 맞추고 가로는 비율에 맞게 조정
                    newWidth = (int)(maxHeight / aspectRatio);
                }

                // 크기 조정된 새 비트맵 생성
                return originalBitmap.Resize(new SKImageInfo(newWidth, newHeight), SKFilterQuality.High);
            }
            catch (Exception ex) 
            {
                LoggerService.FileLogMessage(ex.ToString());
                return null;
            }

        }

    }
}
