namespace APTMentsAPI.Services.Logger
{
    public interface ILoggerService
    {
        /// <summary>
        /// 파일 로그 메시지
        /// </summary>
        /// <param name="message"></param>
        public void FileLogMessage(string message);

        /// <summary>
        /// 파일 API 메시지
        /// </summary>
        /// <param name="message"></param>
        public void FileAPIMessage(string message);

        /// <summary>
        /// 콘솔 로그 메시지
        /// </summary>
        /// <param name="message"></param>
        public void ConsoleLogMessage(string message);
    }
}
