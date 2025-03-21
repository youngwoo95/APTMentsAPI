using APTMentsAPI.Services.Logger;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace APTMentsAPI.SignalRHub
{
    public class BroadcastHub : Hub
    {
        /*
           ConcurrentDictionary
                - 스레드 안전(Thread-Safe)한 사전(Dictionary) 컬렉션이다.
                일반 Dictionary와 달리, 여러 스레드가 동시에 읽기 및 쓰기 작업을 수행할 때 별도의 동기화(lock)를 직접 구현하지 않아도 안전하게 데이터를 추가, 삭제, 수정할 수 있도록 설계됨.

            -> 특징
                1. 스레드 안전
                    : 여러 스레드가 동시에 접근해도 데이터 무결성을 보장한다.
                2. 내부 잠금 관리
                    : 내부적으로 잠금(lock) 메커니즘을 사용하여 동시 업데이트 시 충돌을 방지한다. 개발자가 별도로 락을 구현할 필요가 없다.
                3. 성능 최적화
                    : 높은 동시성을 요구하는 환경에서 성능 저하 없이 안전하게 사용할 수 있다.
         */

        // 각 ConnectID가 가입한 그룹 목록을 ConcurrentDictionary<string, byte>로 관리한다.
        // byte는 단순히 값이 필요 없으므로 자리 표시자로 사용한다.
        private static ConcurrentDictionary<string, ConcurrentDictionary<string, byte>> connectionGroups =
            new ConcurrentDictionary<string, ConcurrentDictionary<string, byte>>();

        private readonly ILoggerService LoggerService;

        public BroadcastHub(ILoggerService _loggerservice)
        {
            this.LoggerService = _loggerservice;
        }

        /// <summary>
        /// 소켓 그룹 가입
        /// </summary>
        /// <returns></returns>
        public async Task JoinSocketRoomAsync(string _roomname)
        {
            try
            {
                // SignalR 내장 메서드를 통해 그룹에 추가
                await Groups.AddToGroupAsync(Context.ConnectionId, _roomname);

                var groups = connectionGroups.GetOrAdd(Context.ConnectionId,
                    _ => new ConcurrentDictionary<string, byte>());

                groups.TryAdd(_roomname, 0);
#if DEBUG
                LoggerService.ConsoleLogMessage($"[INFO] AddGroup\nGroupCounting : {connectionGroups.Count}\nContextId : {Context.ConnectionId}\nGroupName : {_roomname}");
#endif
            }
            catch(Exception ex)
            {
                LoggerService.FileLogMessage(ex.ToString());
#if DEBUG
                LoggerService.ConsoleLogMessage($"[ERROR] {ex.ToString()}");
#endif
            }
        }

        /// <summary>
        /// 소켓 그룹에서 탈퇴
        /// </summary>
        /// <param name="_roomname"></param>
        /// <returns></returns>
        public async Task RemoveSocketRoomAsync(string _roomname)
        {
            try
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, _roomname);
#if DEBUG
                LoggerService.ConsoleLogMessage($"[INFO] RemoveGroup\nGroupCounting : {connectionGroups.Count}\nContextId : {Context.ConnectionId}\nGroupName : {_roomname}");
#endif
                if(connectionGroups.TryGetValue(Context.ConnectionId, out var groups))
                {
                    groups.TryRemove(_roomname, out _);

                    // 더 이상 가입한 그룹이 없으면 전체 connectionId 항목도 제거한다.
                    if(groups.IsEmpty)
                    {
                        connectionGroups.TryRemove(Context.ConnectionId, out _);
                    }
                }
            }
            catch(Exception ex)
            {
                LoggerService.FileLogMessage(ex.ToString());
#if DEBUG
                LoggerService.ConsoleLogMessage($"[ERROR] {ex.ToString()}");
#endif
            }
        }

        /// <summary>
        /// 연결 종료 시 모든 그룹에서 해당 connectionId를 제거한다.
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            try
            {
                await RemoveFromAllGroups(Context.ConnectionId);
                await base.OnDisconnectedAsync(exception);
            }
            catch(Exception ex)
            {
                LoggerService.FileLogMessage(ex.ToString());
#if DEBUG
                LoggerService.ConsoleLogMessage($"[ERROR] {ex.ToString()}");
#endif
            }
        }

        /// <summary>
        /// 주어진 connectionId가 가입한 모든 그룹에서 제거한다.
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public async Task RemoveFromAllGroups(string connectionId)
        {
            try
            {
                if(connectionGroups.TryRemove(connectionId, out var groups))
                {
                    foreach(var roomName in groups.Keys)
                    {
                        await Groups.RemoveFromGroupAsync(connectionId, roomName);
                    }
#if DEBUG
                    LoggerService.ConsoleLogMessage($"[INFO] RemoveGroup\nGroupCounting : {connectionGroups.Count}\nContextId : {Context.ConnectionId}");
#endif

                }
            }
            catch(Exception ex)
            {
                LoggerService.FileLogMessage(ex.ToString());
#if DEBUG
                LoggerService.ConsoleLogMessage($"[ERROR] {ex.ToString()}");
#endif
            }
        }

        /// <summary>
        /// 클라이언트 메시지 수신
        /// </summary>
        /// <param name="message"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task ClientSend(string message, string name)
        {
            Console.WriteLine($"{name}으로 부터 받은 메시지 : {message}");

            // 클라이언트에게 처리 결과를 다시 전송할 수도 있습니다.
            // 예: 그룹 "RoomGroup1"에 연결된 모든 클라이언트로 메시지 브로드캐스트
            await Clients.Group("RoomGroup1").SendAsync("SeverSend", message, "서버");
        }


    }
}
