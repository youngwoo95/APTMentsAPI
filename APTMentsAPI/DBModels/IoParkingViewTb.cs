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

    public string? Memo { get; set; }

    public virtual IoParkingrow? InP { get; set; }

    public virtual IoParkingrow? OutP { get; set; }
}
