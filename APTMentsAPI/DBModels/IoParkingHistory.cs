using System;
using System.Collections.Generic;

namespace APTMentsAPI.DBModels;

/// <summary>
/// 더함비즈 API_입출차 기록 테이블
/// </summary>
public partial class IoParkingHistory
{
    public int Pid { get; set; }

    /// <summary>
    /// 입출차 구분
    /// </summary>
    public int IoGubun { get; set; }

    /// <summary>
    /// 입출차 일련번호
    /// </summary>
    public string IoSeq { get; set; } = null!;

    /// <summary>
    /// 주차장 ID
    /// </summary>
    public string ParkId { get; set; } = null!;

    /// <summary>
    /// 차량 번호
    /// </summary>
    public string CarNum { get; set; } = null!;

    /// <summary>
    /// 입출 상태
    /// </summary>
    public string IoStatusTp { get; set; } = null!;

    /// <summary>
    /// 입출 상태 명
    /// </summary>
    public string IoStatusTpNm { get; set; } = null!;

    /// <summary>
    /// 입-출차 GATE ID
    /// </summary>
    public string IoGateId { get; set; } = null!;

    /// <summary>
    /// 입-출차 GATE NM
    /// </summary>
    public string IoGateNm { get; set; } = null!;

    /// <summary>
    /// 입-출차 라인번호
    /// </summary>
    public int IoLineNum { get; set; }

    /// <summary>
    /// 입-출차 일시
    /// </summary>
    public DateTime IoDtm { get; set; }

    /// <summary>
    /// 입-출차 LPR 상태 ID
    /// </summary>
    public string? IoLprStatus { get; set; }

    /// <summary>
    /// 입-출차 LPR 상태 명칭
    /// </summary>
    public string? IoLprStatusNm { get; set; }

    /// <summary>
    /// 입-출차 차량 구분 ID
    /// </summary>
    public string IoTicketTp { get; set; } = null!;

    /// <summary>
    /// 입-출차 차량 구분
    /// </summary>
    public string IoTicketTpNm { get; set; } = null!;

    /// <summary>
    /// 동
    /// </summary>
    public string? Dong { get; set; }

    /// <summary>
    /// 호
    /// </summary>
    public string? Ho { get; set; }

    /// <summary>
    /// 예약차량여부
    /// </summary>
    public string? IsReservation { get; set; }

    /// <summary>
    /// 블랙리스트 여부
    /// </summary>
    public string? IsBlackList { get; set; }

    /// <summary>
    /// 블랙리스트 사유
    /// </summary>
    public string? BlackListReason { get; set; }

    /// <summary>
    /// 블랙리스트 등록 일시
    /// </summary>
    public string? RegDtm { get; set; }

    /// <summary>
    /// 이미지 경로
    /// </summary>
    public string ImgPath { get; set; } = null!;

    /// <summary>
    /// (입차전용) 해당차량을 입차처리할건지 대기할건지
    /// </summary>
    public string? IsWait { get; set; }

    /// <summary>
    /// (입차전용) 대기 걸린 차량의 이유
    /// </summary>
    public string? IsWaitReason { get; set; }

    /// <summary>
    /// 주차시간
    /// </summary>
    public int? ParkDuration { get; set; }

    /// <summary>
    /// 방문 시간 (방문포인트 사용 시간)
    /// </summary>
    public int? VisitTime { get; set; }

    /// <summary>
    /// 예약 차량의 경우
    /// </summary>
    public string? Etc { get; set; }

    /// <summary>
    /// 시스템 생성 일자
    /// </summary>
    public DateTime CreateDt { get; set; }

    /// <summary>
    /// IO_ParkingViewTB FK
    /// </summary>
    public int SPid { get; set; }

    public virtual IoParkingViewTb SP { get; set; } = null!;
}
