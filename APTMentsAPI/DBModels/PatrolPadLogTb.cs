﻿using System;
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
    /// 순찰 담당자 ID (사용안함)
    /// </summary>
    public int PatrolUserId { get; set; }

    /// <summary>
    /// 순찰 담당자 이름
    /// </summary>
    public string PatrolUserNm { get; set; } = null!;

    /// <summary>
    /// 순찰 시작 일시 (사용안함)
    /// </summary>
    public DateTime PatrolStartDtm { get; set; }

    /// <summary>
    /// 순찰 종료 일시 (사용안함)
    /// </summary>
    public DateTime PatrolEndDtm { get; set; }

    /// <summary>
    /// 전체 데이터 개수 (사용안함)
    /// </summary>
    public int TotCnt { get; set; }

    /// <summary>
    /// 순찰일시
    /// </summary>
    public DateTime PatrolDtm { get; set; }

    /// <summary>
    /// 순찰 상태 코드 1: 위반(블랙리스트), 2: 정상(입주민), 3: 방문객(현장), 4:방문객(예약)
    /// </summary>
    public int PatrolCode { get; set; }

    /// <summary>
    /// 순찰상태명
    /// </summary>
    public string? PatrolName { get; set; }

    /// <summary>
    /// 차량번호
    /// </summary>
    public string CarNum { get; set; } = null!;

    /// <summary>
    /// 순찰 이미지
    /// </summary>
    public string? PatrolImg { get; set; }

    /// <summary>
    /// 순찰비고
    /// </summary>
    public string? PatrolRemark { get; set; }

    /// <summary>
    /// 시스템 생성시간
    /// </summary>
    public DateTime CreateDt { get; set; }
}
