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
    /// 주차장ID
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
    /// 입출 상태명
    /// </summary>
    public string IoStatusTpNm { get; set; } = null!;

    /// <summary>
    /// 입차 게이트 ID
    /// </summary>
    public string InGateId { get; set; } = null!;

    /// <summary>
    /// 입차 게이트 명
    /// </summary>
    public string InGateNm { get; set; } = null!;

    /// <summary>
    /// 입차 라인 번호
    /// </summary>
    public int InLineNum { get; set; }

    /// <summary>
    /// 입차 시간 yyyy-MM-dd HH:mm:ss
    /// </summary>
    public DateTime InDtm { get; set; }

    /// <summary>
    /// 입차 LPR 상태
    /// </summary>
    public string? InLprStatus { get; set; }

    /// <summary>
    /// 입차 LPR 상태 명칭
    /// </summary>
    public string? InLprStatusNm { get; set; }

    /// <summary>
    /// 입차 차량 구분 # 2 : 일반차량 6 : 정기차량
    /// </summary>
    public string InTicketTp { get; set; } = null!;

    /// <summary>
    /// 입차 차량 구분 명
    /// </summary>
    public string InTicketTpNm { get; set; } = null!;

    /// <summary>
    /// 블랙리스트 여부 (입차처리 *블랙리스트로 표시) OR 입차대기 걸고 블랙리스트 표시
    /// </summary>
    public string? IsBlackList { get; set; }

    /// <summary>
    /// 블랙리스트 사유
    /// </summary>
    public string? BlackListReason { get; set; }

    /// <summary>
    /// 블랙리스트 등록일시
    /// </summary>
    public string? RegDtm { get; set; }

    /// <summary>
    /// 이미지 경로(입차)
    /// </summary>
    public string InImagePath { get; set; } = null!;

    /// <summary>
    /// 동
    /// </summary>
    public string? Dong { get; set; }

    /// <summary>
    /// 호
    /// </summary>
    public string? Ho { get; set; }

    /// <summary>
    /// 예약차량여부 0,1
    /// </summary>
    public string? IsReservation { get; set; }

    /// <summary>
    /// 해당차량을 입차처리할건지 대기처리할건지 0: 입차, 1: 입차대기, 2:입차대기후 승인
    /// </summary>
    public string? IsWait { get; set; }

    /// <summary>
    /// 대기 걸린 차량의 이유 - 방문차량, 블랙리스트, 진입금지 그룹
    /// </summary>
    public string? IsWaitReason { get; set; }

    /// <summary>
    /// ETC
    /// </summary>
    public string? Etc { get; set; }

    /// <summary>
    /// 메모
    /// </summary>
    public string? Memo { get; set; }

    /// <summary>
    /// 출차 게이트 ID
    /// </summary>
    public string? OutGateId { get; set; }

    /// <summary>
    /// 출차 게이트 명
    /// </summary>
    public string? OutGateNm { get; set; }

    /// <summary>
    /// 출차 라인 번호
    /// </summary>
    public int? OutLineNum { get; set; }

    /// <summary>
    /// 출차 시간
    /// </summary>
    public string? OutDtm { get; set; }

    /// <summary>
    /// 출차 LPR 상태
    /// </summary>
    public string? OutLprStatus { get; set; }

    /// <summary>
    /// 출차 LPR 상태 명칭
    /// </summary>
    public string? OutLprStatusNm { get; set; }

    /// <summary>
    /// 출차 차량 구분
    /// </summary>
    public string? OutTicketTp { get; set; }

    /// <summary>
    /// 출차 차량 구분 명
    /// </summary>
    public string? OutTicketTpNm { get; set; }

    /// <summary>
    /// 시스템 생성일자
    /// </summary>
    public DateTime InCreateDt { get; set; }

    /// <summary>
    /// 시스템 생성일자
    /// </summary>
    public DateTime? OutCreateDt { get; set; }

    /// <summary>
    /// 이미지 경로(출차)
    /// </summary>
    public string? OutImagePath { get; set; }

    /// <summary>
    /// 주차 시간
    /// </summary>
    public int? ParkingDuration { get; set; }

    /// <summary>
    /// 방문 시간 (방문포인트 사용 시간)
    /// </summary>
    public int? VisitTime { get; set; }

    public virtual ICollection<IoParkingHistory> IoParkingHistories { get; set; } = new List<IoParkingHistory>();
}
