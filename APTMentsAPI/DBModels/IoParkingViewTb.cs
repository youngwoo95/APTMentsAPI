using System;
using System.Collections.Generic;

namespace APTMentsAPI.DBModels;

/// <summary>
/// 더함비즈 API_입출차 테이블
/// </summary>
public partial class IoParkingViewTb
{
    public int Pid { get; set; }

    /// <summary>
    /// 입출차 일련번호
    /// </summary>
    public string IoSeq { get; set; } = null!;

    /// <summary>
    /// 입차 차량 구분
    /// </summary>
    public string? InTicketTp { get; set; }

    /// <summary>
    /// 입차 차량 구분 명
    /// </summary>
    public string? InTicketTpNm { get; set; }

    /// <summary>
    /// 출차 차량 구분
    /// </summary>
    public string? OutTicketTp { get; set; }

    /// <summary>
    /// 출차 차량 구분 명
    /// </summary>
    public string? OutTicketTpNm { get; set; }

    /// <summary>
    /// 입출 상태
    /// </summary>
    public string? IoStatusTp { get; set; }

    /// <summary>
    /// 입출 상태명
    /// </summary>
    public string? IoStatusTpNm { get; set; }

    /// <summary>
    /// 차량 번호
    /// </summary>
    public string CarNum { get; set; } = null!;

    /// <summary>
    /// 입차 시간
    /// </summary>
    public DateTime? InDtm { get; set; }

    /// <summary>
    /// 출차 시간
    /// </summary>
    public DateTime? OutDtm { get; set; }

    /// <summary>
    /// 주차 시간
    /// </summary>
    public int? ParkingDuration { get; set; }

    /// <summary>
    /// 주차장ID
    /// </summary>
    public string? ParkId { get; set; }

    /// <summary>
    /// 입차 게이트 ID
    /// </summary>
    public string? InGateId { get; set; }

    /// <summary>
    /// 입차 게이트 명
    /// </summary>
    public string? InGateNm { get; set; }

    /// <summary>
    /// 출차 게이트 ID
    /// </summary>
    public string? OutGateId { get; set; }

    /// <summary>
    /// 출차 게이트 명
    /// </summary>
    public string? OutGateNm { get; set; }

    /// <summary>
    /// 동
    /// </summary>
    public string? Dong { get; set; }

    /// <summary>
    /// 호
    /// </summary>
    public string? Ho { get; set; }

    /// <summary>
    /// 시스템 생성일자
    /// </summary>
    public DateTime CreateDt { get; set; }

    /// <summary>
    /// 이미지 경로(입차)
    /// </summary>
    public string? ImagePath1 { get; set; }

    /// <summary>
    /// 이미지 경로(출차)
    /// </summary>
    public string? ImagePath2 { get; set; }

    /// <summary>
    /// 메모
    /// </summary>
    public string? Memo { get; set; }

    public virtual ICollection<IoParkingHistory> IoParkingHistories { get; set; } = new List<IoParkingHistory>();
}
