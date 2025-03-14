using System;
using System.Collections.Generic;

namespace APTMentsAPI.DBModels;

public partial class Patrollogtblist
{
    public int Pid { get; set; }

    /// <summary>
    /// 순찰 일시
    /// </summary>
    public DateTime PatrolDtm { get; set; }

    /// <summary>
    /// 순찰 상태 코드 0: 정상(입주민), 1: 방문객, 2: 순찰, 3:위반(블랙리스트)
    /// </summary>
    public int PatrolCode { get; set; }

    /// <summary>
    /// 순찰 상태 명
    /// </summary>
    public string? PatrolName { get; set; }

    /// <summary>
    /// 차량 번호
    /// </summary>
    public string CarNum { get; set; } = null!;

    /// <summary>
    /// 순찰 이미지
    /// </summary>
    public string? PatrolImg { get; set; }

    /// <summary>
    /// 순찰 비고
    /// </summary>
    public string? PatrolRemark { get; set; }

    /// <summary>
    /// 시스템 생성시간
    /// </summary>
    public DateTime? CreateDt { get; set; }

    /// <summary>
    /// 순찰정보 외래키
    /// </summary>
    public int SPid { get; set; }

    public virtual Patrolpadlogtb SP { get; set; } = null!;
}
