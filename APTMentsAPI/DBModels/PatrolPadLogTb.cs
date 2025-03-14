using System;
using System.Collections.Generic;

namespace APTMentsAPI.DBModels;

public partial class Patrolpadlogtb
{
    public int Pid { get; set; }

    /// <summary>
    /// 주차장 ID
    /// </summary>
    public string ParkId { get; set; } = null!;

    /// <summary>
    /// 순찰 담당자 ID
    /// </summary>
    public string PatrolUserId { get; set; } = null!;

    /// <summary>
    /// 순찰 담당자 이름
    /// </summary>
    public int PatrolUserNm { get; set; }

    /// <summary>
    /// 순찰 시작 일시
    /// </summary>
    public string PatrolStartDtm { get; set; } = null!;

    /// <summary>
    /// 순찰 종료 일시
    /// </summary>
    public string PatrolEndDtm { get; set; } = null!;

    /// <summary>
    /// 전체 데이터 개수
    /// </summary>
    public int TotCnt { get; set; }

    /// <summary>
    /// 시스템 생성시간
    /// </summary>
    public DateTime CreateDt { get; set; }

    public virtual ICollection<Patrollogtblist> Patrollogtblists { get; set; } = new List<Patrollogtblist>();
}
