using APTMentsAPI.DTO;
using APTMentsAPI.DTO.PatrolDTO;
using APTMentsAPI.DTO.ViewsDTO;
using Swashbuckle.AspNetCore.Filters;

namespace APTMentsAPI
{
    /// <summary>
    /// 전체 LIST Swagger 샘플
    /// </summary>
    public class ViewListResponseExample : IExamplesProvider<ResponseUnit<PageNationDTO<InOutViewListDTO>>>
    {
        public ResponseUnit<PageNationDTO<InOutViewListDTO>> GetExamples()
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

            // 페이지네이션 DTO에 예제 데이터를 할당합니다.
            var pageNation = new PageNationDTO<InOutViewListDTO>
            {
                Items = items,
                pageNumber = 1,
                pageSize = 15,
                totalCount = 130
            };

            // ResponseList의 data 프로퍼티는 List<PageNationDTO<InOutViewListDTO>> 타입이어야 합니다.
            return new ResponseUnit<PageNationDTO<InOutViewListDTO>>()
            {
                message = "요청이 정상 처리되었습니다",
                data = pageNation,
                code = 200
            };
        }
    }

    /// <summary>
    /// IoSeq 상세정보 조회 Swagger 샘플
    /// </summary>
    public class DetailViewResponseExample : IExamplesProvider<ResponseList<DetailViewDTO>>
    {
        public ResponseList<DetailViewDTO> GetExamples()
        {
            return new ResponseList<DetailViewDTO>()
            {
                message = "요청이 정상 처리되었습니다.",
                data = new List<DetailViewDTO>
                {
                    new DetailViewDTO
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
                    },
                     new DetailViewDTO
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
                    }
                },
                code = 200
            };
        }

        /// <summary>
        /// 순찰 패드 전체 List Swagger 샘플
        /// </summary>
        public class PatrolListResponseExample : IExamplesProvider<ResponseUnit<PageNationDTO<PatrolViewListDTO>>>
        {
            public ResponseUnit<PageNationDTO<PatrolViewListDTO>> GetExamples()
            {
                // 첫 번째 순찰 항목
                var patrolItem1 = new PatrolViewListDTO
                {
                    pId = 1,
                    parkId = "2177",
                    partolUserId = "33마3434",
                    patrolUserNm = 10,
                    patrolStartDtm = "입차",
                    patrolEndDtm = "1",
                    totCnt = 3,
                    lowList = new List<PatrolLowList>
                {
                    new PatrolLowList {
                        pId = 1,
                        patrolDtm = DateTime.Parse("2025-11-22T22:22:22"),
                        patrolCode = 0,
                        patrolName = "방문객",
                        carNum = "99버9999",
                        patrolImg = "http://thehambizp0002.iptime.org:8000/image/2025\\02\\28\\102\\20250228100533_228오1005.jpg",
                        patrolRemark = "친척방문"
                    },
                    new PatrolLowList {
                        pId = 2,
                        patrolDtm = DateTime.Parse("2025-11-22T22:25:25"),
                        patrolCode = 1,
                        patrolName = "입주민",
                        carNum = "33루3333",
                        patrolImg = "http://thehambizp0002.iptime.org:8000/image/2025\\02\\28\\102\\20250228100533_228오1005.jpg",
                        patrolRemark = ""
                    },
                    new PatrolLowList {
                        pId = 3,
                        patrolDtm = DateTime.Parse("2025-11-22T22:30:34"),
                        patrolCode = 0,
                        patrolName = "블랙리스트",
                        carNum = "88머8888",
                        patrolImg = "http://thehambizp0002.iptime.org:8000/image/2025\\02\\28\\102\\20250228100533_228오1005.jpg",
                        patrolRemark = "상습주차범"
                    }
                }
                };

                // 두 번째 순찰 항목
                var patrolItem2 = new PatrolViewListDTO
                {
                    pId = 2,
                    parkId = "2177",
                    partolUserId = "33마3434",
                    patrolUserNm = 10,
                    patrolStartDtm = "입차",
                    patrolEndDtm = "1",
                    totCnt = 3,
                    lowList = new List<PatrolLowList>
                {
                    new PatrolLowList {
                        pId = 4,
                        patrolDtm = DateTime.Parse("2025-11-22T22:22:22"),
                        patrolCode = 0,
                        patrolName = "방문객",
                        carNum = "99버9999",
                        patrolImg = "http://thehambizp0002.iptime.org:8000/image/2025\\02\\28\\102\\20250228100533_228오1005.jpg",
                        patrolRemark = "친척방문"
                    },
                    new PatrolLowList {
                        pId = 5,
                        patrolDtm = DateTime.Parse("2025-11-22T22:25:25"),
                        patrolCode = 1,
                        patrolName = "입주민",
                        carNum = "33루3333",
                        patrolImg = "http://thehambizp0002.iptime.org:8000/image/2025\\02\\28\\102\\20250228100533_228오1005.jpg",
                        patrolRemark = ""
                    },
                    new PatrolLowList {
                        pId = 6,
                        patrolDtm = DateTime.Parse("2025-11-22T22:30:34"),
                        patrolCode = 0,
                        patrolName = "블랙리스트",
                        carNum = "88머8888",
                        patrolImg = "http://thehambizp0002.iptime.org:8000/image/2025\\02\\28\\102\\20250228100533_228오1005.jpg",
                        patrolRemark = "상습주차범"
                    }
                }
                };

                // 세 번째 순찰 항목
                var patrolItem3 = new PatrolViewListDTO
                {
                    pId = 3,
                    parkId = "2177",
                    partolUserId = "33마3434",
                    patrolUserNm = 10,
                    patrolStartDtm = "입차",
                    patrolEndDtm = "1",
                    totCnt = 3,
                    lowList = new List<PatrolLowList>
                {
                    new PatrolLowList {
                        pId = 7,
                        patrolDtm = DateTime.Parse("2025-11-22T22:22:22"),
                        patrolCode = 0,
                        patrolName = "방문객",
                        carNum = "99버9999",
                        patrolImg = "http://thehambizp0002.iptime.org:8000/image/2025\\02\\28\\102\\20250228100533_228오1005.jpg",
                        patrolRemark = "친척방문"
                    },
                    new PatrolLowList {
                        pId = 8,
                        patrolDtm = DateTime.Parse("2025-11-22T22:25:25"),
                        patrolCode = 1,
                        patrolName = "입주민",
                        carNum = "33루3333",
                        patrolImg = "http://thehambizp0002.iptime.org:8000/image/2025\\02\\28\\102\\20250228100533_228오1005.jpg",
                        patrolRemark = ""
                    },
                    new PatrolLowList {
                        pId = 9,
                        patrolDtm = DateTime.Parse("2025-11-22T22:30:34"),
                        patrolCode = 0,
                        patrolName = "블랙리스트",
                        carNum = "88머8888",
                        patrolImg = "http://thehambizp0002.iptime.org:8000/image/2025\\02\\28\\102\\20250228100533_228오1005.jpg",
                        patrolRemark = "상습주차범"
                    }
                }
                };

                // 페이지네이션 DTO에 순찰 리스트 항목들을 할당합니다.
                var pageNation = new PageNationDTO<PatrolViewListDTO>
                {
                    Items = new List<PatrolViewListDTO> { patrolItem1, patrolItem2, patrolItem3 },
                    pageNumber = 1,
                    pageSize = 15,
                    totalCount = 3
                };

                return new ResponseUnit<PageNationDTO<PatrolViewListDTO>>
                {
                    message = "요청이 정상 처리되었습니다.",
                    data = pageNation,
                    code = 200
                };
            }
        }
    }

}