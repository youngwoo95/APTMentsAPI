﻿using APTMentsAPI.DTO;
using APTMentsAPI.DTO.InCarDTO;
using APTMentsAPI.DTO.OutCarDTO;
using APTMentsAPI.DTO.PatrolDTO;
using APTMentsAPI.DTO.ViewsDTO;

namespace APTMentsAPI.Services.TheHamBizService
{
    public interface ITheHamBizServices
    {
        #region 더함비즈 API 호출
        /// <summary>
        /// 입차 등록
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Task<int> AddInCarSerivce(RequestInTheHamBizDTO dto);

        /// <summary>
        /// 출차 등록
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Task<int> AddOutCarService(RequestOutTheHamBizDTO dto);

        /// <summary>
        /// 순찰 등록
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Task<int> AddPatrolService(RequestPadTheHamBizDTO dto);
        #endregion
        #region 입-출차
        /// <summary>
        /// 입-출차 리스트
        /// </summary>
        /// <returns></returns>
        //public Task<ResponseUnit<PageNationDTO<InOutViewListDTO>?>> InOutViewListService(int pageNumber, int pageSize);
        public Task<ResponsePage<PageNationDTO<InOutViewListDTO>>?> InOutViewListService(int pageNumber, int PageSize, DateTime? StartDate, DateTime? EndDate, string? inStatusTp, string? CarNumber, string? Dong, string? Ho, int? PackingDuration, string? ioTicketTpNm);

        /// <summary>
        /// 시퀀스 상세내역 조회
        /// </summary>
        /// <param name="ioSeq"></param>
        /// <returns></returns>
        public Task<ResponsePage<List<DetailViewDTO>>?> DetailViewService(string ioSeq);

        /// <summary>
        /// 해당 차량 최근 7일치 조회
        /// </summary>
        /// <param name="carNum"></param>
        /// <returns></returns>
        public Task<ResponsePage<LastWeeksDTO>?> LastWeeksService(string carNum);

        /// <summary>
        /// View 테이블 Memo 컬럼 수정
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Task<ResponseUnit<bool>> UpdateViewMemoService(UpdateMemoDTO dto);

        /// <summary>
        /// Rows 테이블 Memo 컬럼 수정
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Task<ResponseUnit<bool>> UpdateRowsMemoService(UpdateMemoDTO dto);
        #endregion

        #region 순찰
        /// <summary>
        /// 순찰 리스트 조회
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public Task<ResponsePage<PageNationDTO<PatrolViewListDTO>>?> PatrolViewListService(int pageNumber, int PageSize);
        #endregion


    }
}
