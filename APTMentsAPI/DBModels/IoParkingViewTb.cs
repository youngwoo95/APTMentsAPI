using System;
using System.Collections.Generic;

namespace APTMentsAPI.DBModels;

/// <summary>
/// 더함비즈 API_입출차 테이블
/// </summary>
public partial class IoParkingviewtb
{
    public int Pid { get; set; }

    /// <summary>
    /// 입출차 일련번호
    /// </summary>
    public string IoSeq { get; set; } = null!;

    /// <summary>
    /// 입출 상태
    /// </summary>
    public string InStatusTp { get; set; } = null!;

    /// <summary>
    /// 입출 상태 명
    /// </summary>
    public string InStatusTpNm { get; set; } = null!;

    /// <summary>
    /// 최종입차_PID
    /// </summary>
    public int? InPid { get; set; }

    /// <summary>
    /// 최종입차_DT
    /// </summary>
    public DateTime? InDtm { get; set; }

    /// <summary>
    /// 최종출차_PID
    /// </summary>
    public int? OutPid { get; set; }

    /// <summary>
    /// 최종출차_DT
    /// </summary>
    public DateTime? OutDtm { get; set; }

    /// <summary>
    /// 차량번호
    /// </summary>
    public string? CarNum { get; set; }

    /// <summary>
    /// 동
    /// </summary>
    public string? Dong { get; set; }

    /// <summary>
    /// 호
    /// </summary>
    public string? Ho { get; set; }

    /// <summary>
    /// 주차시간
    /// </summary>
    public int? ParingDuration { get; set; }

    /// <summary>
    /// 블랙리스트 여부
    /// </summary>
    public string? IsBlackList { get; set; }

    /// <summary>
    /// 블랙리스트 사유
    /// </summary>
    public string? BlackListReason { get; set; }

    public string? Memo { get; set; }

    /// <summary>
    /// ROW Update 시간
    /// </summary>
    public DateTime UpdateDt { get; set; }

    public virtual IoParkingrow? InP { get; set; }

    public virtual IoParkingrow? OutP { get; set; }
}
