using APTMentsAPI.DTO;
using APTMentsAPI.DTO.PatrolDTO;
using APTMentsAPI.DTO.ViewsDTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Swashbuckle.AspNetCore.Filters;

namespace APTMentsAPI
{
    /// <summary>
    /// 전체 LIST Swagger 샘플
    /// </summary>
    public class ViewListResponseExample : IExamplesProvider<ResponsePage<InOutViewListDTO>>
    {
        public ResponsePage<InOutViewListDTO> GetExamples()
        {
            // InOutViewListDTO 예제 데이터를 리스트에 추가합니다.
            var items = new List<InOutViewListDTO>
            {
                new InOutViewListDTO
                {
                    pId = 12,
                    ioSeq = "P2177010120240613082249851",
                    ioTicketTp = "2",
                    ioTicketTpNm = "방문차량",
                    ioStatusTp = "20",
                    ioStatusTpNm = "출차",
                    carNum = "44거1010",
                    inDtm = DateTime.Parse("2024-06-13T05:38:07"),
                    outDtm = DateTime.Parse("2024-06-13T05:38:07"),
                    parkingDuration = 36,
                    inGateId = "1",
                    inGateNm = "정문",
                    outGateId = "1",
                    outGateNm = "정문",
                    dong = "101",
                    ho = "1001",
                    inImagePath = "http://thehambizp0002.iptime.org:8000/image/2025\\02\\28\\102\\20250228100533_228오1005.jpg",
                    outImagePath = "http://thehambizp0002.iptime.org:8000/image/2025\\02\\28\\102\\20250228100533_228오1005.jpg",
                    isBlacklist = "0",
                    blacklistReason = "",
                    memo = null
                }
            };

            var result = new ResponsePage<InOutViewListDTO>
            {
                Meta = new Meta
                {
                    pageNumber = 1,
                    pageSize = 15,
                    totalCount = 100
                },
                data = items,
                code = 200
            };

            return result;
        }
    }

    /// <summary>
    /// IoSeq 상세정보 조회 Swagger 샘플
    /// </summary>
    public class DetailViewResponseExample : IExamplesProvider<ResponsePage<DetailViewDTO>>
    {
        public ResponsePage<DetailViewDTO> GetExamples()
        {
            List<DetailViewDTO> item = new List<DetailViewDTO>();
            item.Add(new DetailViewDTO
            {
                pId = 62,
                ioGubun = 1,
                ioSeq = "P2177010120240613082249851",
                parkId = "2177",
                carNum = "33마3434",
                ioStatusTp = "10",
                ioStatusTpNm = "입차",
                ioGateId = "1",
                ioGateNm = "정문",
                ioLineNum = 1,
                ioDtm = DateTime.Parse("2024-06-13T05:38:07"),
                ioLprStatus = "0",
                ioLprStatusNm = "",
                ioTicketTp = "2",
                ioTicketTpNm = "방문차량",
                dong = "101",
                ho = "1001",
                isReservation = "0",
                isBlacklist = "0",
                blacklistReason = "",
                regDtm = "",
                imgPath = "http://thehambizp0002.iptime.org:8000/image/2025\\02\\28\\102\\20250228100533_228오1005.jpg",
                isWait = "0",
                isWaitReason = "",
                parkDuration = null,
                visitTime = null,
                etc = "",
                memo = null
            });
            item.Add(new DetailViewDTO
            {
                pId = 63,
                ioGubun = 1,
                ioSeq = "P2177010120240613082249851",
                parkId = "2177",
                carNum = "33마3434",
                ioStatusTp = "20",
                ioStatusTpNm = "출차",
                ioGateId = "1",
                ioGateNm = "정문",
                ioLineNum = 1,
                ioDtm = DateTime.Parse("2024-06-13T05:38:07"),
                ioLprStatus = "0",
                ioLprStatusNm = "",
                ioTicketTp = "2",
                ioTicketTpNm = "방문차량",
                dong = "101",
                ho = "1001",
                isReservation = "0",
                isBlacklist = "0",
                blacklistReason = "",
                regDtm = "",
                imgPath = "http://thehambizp0002.iptime.org:8000/image/2025\\02\\28\\102\\20250228100533_228오1005.jpg",
                isWait = "0",
                isWaitReason = "",
                parkDuration = 35,
                visitTime = null,
                etc = "",
                memo = null
            });
            return new ResponsePage<DetailViewDTO>() { data = item, code = 200 };
        }
    }

    public class LastViewListResponseExample : IExamplesProvider<ResponsePage<LastWeeksDTO>>
    {
        public ResponsePage<LastWeeksDTO> GetExamples()
        {
            var item1 = new LastWeeksDTO()
            {
                pId = 69,
                ioGubun = 1,
                ioSeq = "P2177010120240613082249851",
                parkId = "2177",
                carNum = "33마3434",
                ioStatusTp = "10",
                ioStatusTpNm = "입차",
                ioGateId = "1",
                ioGateNm = "정문",
                ioLineNum = 1,
                ioDtm = DateTime.Now.AddDays(-7),
                ioLprStatus = "0",
                ioLprStatusNm = "",
                ioTicketTp = "2",
                ioTicketTpNm = "방문차량",
                dong = "101",
                ho = "1001",
                isReservation = "0",
                isBlacklist = "0",
                blacklistReason = "",
                regDtm = "",
                imgPath = "http://thehambizp0002.iptime.org:8000/image/2025\\02\\28\\102\\20250228100533_228오1005.jpg",
                isWait = "0",
                isWaitReason = "",
                parkDuration = null,
                visitTime = null,
                etc = "",
                memo = null
            };

            var item2 = new LastWeeksDTO()
            {
                pId = 70,
                ioGubun = 0,
                ioSeq = "P2177010120240613082249851",
                parkId = "2177",
                carNum = "44거1010",
                ioStatusTp = "20",
                ioStatusTpNm = "출차",
                ioGateId = "1",
                ioGateNm = "정문",
                ioLineNum = 2,
                ioDtm = DateTime.Now,
                ioLprStatus = "0",
                ioLprStatusNm = "",
                ioTicketTp = "2",
                ioTicketTpNm = "방문차량",
                dong = "101",
                ho = "1001",
                isReservation = "0",
                isBlacklist = "0",
                blacklistReason = "",
                regDtm = "",
                imgPath = "http://thehambizp0002.iptime.org:8000/image/2025\\02\\28\\102\\20250228100533_228오1005.jpg",
                isWait = null,
                isWaitReason = null,
                parkDuration = 36,
                visitTime = 36,
                etc = "",
                memo = null
            };

            // 페이지네이션 DTO에 순찰 리스트 항목들을 할당합니다.
            var result = new ResponsePage<LastWeeksDTO>
            {
                data = new List<LastWeeksDTO> { item1,item2},
                code = 200
            };
            return result;
        }
    }

    /// <summary>
    /// 순찰 패드 전체 List Swagger 샘플
    /// </summary>
    public class PatrolListResponseExample : IExamplesProvider<ResponsePage<PatrolViewListDTO>>
    {
        public ResponsePage<PatrolViewListDTO> GetExamples()
        {
            // 첫 번째 순찰 항목
            var patrolItem1 = new PatrolViewListDTO
            {
                pId = 1,
                parkId = "P001",
                patrolUserNm = "홍길동",
                IoTicketTP = "방문차량",
                patrolCD = 1,
                patrolName = "위반(블랙리스트)",
                patrolDtm = DateTime.Now,
                patrolImg = "http://thehambizp0002.iptime.org:8000/image/2025\\02\\28\\102\\20250228100533_228오1005.jpg",
                carNum = "123가4567",
                patrolRemark = "타단지 차량"
            };

            var patrolItem2 = new PatrolViewListDTO
            {
                pId = 1,
                parkId = "P001",
                patrolUserNm = "홍길동",
                IoTicketTP = "방문차량",
                patrolCD = 2,
                patrolName = "정상(입주민)",
                patrolDtm = DateTime.Now,
                patrolImg = "http://thehambizp0002.iptime.org:8000/image/2025\\02\\28\\102\\20250228100533_228오1005.jpg",
                carNum = "5678가4567",
                patrolRemark = ""
            };

            // 페이지네이션 DTO에 순찰 리스트 항목들을 할당합니다.
            var result = new ResponsePage<PatrolViewListDTO>
            {
                Meta = new Meta
                {
                    pageNumber = 1,
                    pageSize = 15,
                    totalCount = 100
                },
                data = new List<PatrolViewListDTO> { patrolItem1, patrolItem2 },
                code = 200
            };

            return result;
        }
    }


}