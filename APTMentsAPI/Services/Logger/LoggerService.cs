namespace APTMentsAPI.Services.Logger
{
    public class LoggerService : ILoggerService
    {
        /// <summary>
        /// 에러 로그 메시지 저장
        /// </summary>
        /// <param name="message"></param>
        public void FileLogMessage(string message)
        {
            try
            {
                DateTime Today = DateTime.Now;
                string dir_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SystemLog", "ErrorLog");

                DirectoryInfo di = new DirectoryInfo(dir_path);

                if (!di.Exists)
                {
                    di.Create();
                }

                // 년도 파일 없으면 생성
                dir_path = Path.Combine(dir_path, Today.Year.ToString());
                di = new DirectoryInfo(dir_path);
                if(!di.Exists)
                {
                    di.Create();
                }

                dir_path = Path.Combine(dir_path, Today.Month.ToString());
                di = new DirectoryInfo(dir_path);

                // 월 파일 없으면 생성
                if(!di.Exists)
                {
                    di.Create();
                }

                // 일
                dir_path = Path.Combine(dir_path, $"{Today.Year}_{Today.Month}_{Today.Day}.txt");

                // 일.txt + 로그내용
                using (StreamWriter sw = new StreamWriter(dir_path, true))
                {
                    System.Diagnostics.StackTrace objStackTrace = new System.Diagnostics.StackTrace(new System.Diagnostics.StackFrame(1));
                    var s = objStackTrace.ToString(); // 호출한 함수 위치
                    sw.Write($"[ERROR]_[{Today.ToString()}]\t{message}");

#if DEBUG
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Red;

                    // 로그 출력
                    Console.WriteLine($"[ERROR] {message}");

                    Console.ResetColor();
#endif
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 파일 API 메시지 저장
        /// </summary>
        /// <param name="message"></param>
        public void FileAPIMessage(string message)
        {
            try
            {
                DateTime Today = DateTime.Now;
                string dir_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SystemLog", "APILog");

                DirectoryInfo di = new DirectoryInfo(dir_path);

                // 년도 파일 없으면 생성
                if(!di.Exists)
                {
                    di.Create();
                }

                // 년도 파일 없으면 생성
                dir_path = Path.Combine(dir_path, Today.Year.ToString());
                di = new DirectoryInfo(dir_path);
                if(!di.Exists)
                {
                    di.Create();
                }

                dir_path = Path.Combine(dir_path, Today.Month.ToString());
                di = new DirectoryInfo(dir_path);

                // 월 파일 없으면 생성
                if(!di.Exists)
                {
                    di.Create();
                }

                // 일
                dir_path = Path.Combine(dir_path, $"{Today.Year}_{Today.Month}_{Today.Day}.txt");

                // 일.txt + 로그내용
                using (StreamWriter sw = new StreamWriter(dir_path, true))
                {
                    System.Diagnostics.StackTrace objStackTrace = new System.Diagnostics.StackTrace(new System.Diagnostics.StackFrame(1));
                    var s = objStackTrace.ToString(); // 호출한 함수 위치
                    sw.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}]\t{message}");

#if DEBUG
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Green;

                    // 로그 출력
                    Console.WriteLine($"[INFO] {message}");

                    Console.ResetColor();
#endif
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 콘솔 로그 메시지
        /// </summary>
        /// <param name="message"></param>
        public void ConsoleLogMessage(string message)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;

            // 로그 출력
            Console.WriteLine($"[INFO] {message}");

            Console.ResetColor();
        }

        
    }
}
