using System;
using System.Collections.Generic;
using APTMentsAPI.DBModels;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace APTMentsAPI.Repository;

public partial class AptContext : DbContext
{
    public AptContext()
    {
    }

    public AptContext(DbContextOptions<AptContext> options)
        : base(options)
    {
    }

    public virtual DbSet<IoParkingHistory> IoParkingHistories { get; set; }

    public virtual DbSet<IoParkingViewTb> IoParkingViewTbs { get; set; }

    public virtual DbSet<PatrolLogTblist> PatrolLogTblists { get; set; }

    public virtual DbSet<PatrolPadLogTb> PatrolPadLogTbs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("server=123.2.159.98;port=3306;database=AptmentWorks;user=root;password=stecdev1234!", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.6.18-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<IoParkingHistory>(entity =>
        {
            entity.HasKey(e => e.Pid).HasName("PRIMARY");

            entity.ToTable("IO_ParkingHistory", tb => tb.HasComment("더함비즈 API_입출차 기록 테이블"));

            entity.HasIndex(e => e.SPid, "FK_ViewTB_202503130949");

            entity.Property(e => e.Pid)
                .HasColumnType("int(11)")
                .HasColumnName("PID");
            entity.Property(e => e.BlackListReason)
                .HasMaxLength(255)
                .HasComment("블랙리스트 사유")
                .HasColumnName("BLACK_LIST_REASON");
            entity.Property(e => e.CarNum)
                .HasMaxLength(255)
                .HasComment("차량 번호")
                .HasColumnName("CAR_NUM");
            entity.Property(e => e.CreateDt)
                .HasDefaultValueSql("current_timestamp()")
                .HasComment("시스템 생성 일자")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DT");
            entity.Property(e => e.Dong)
                .HasMaxLength(255)
                .HasComment("동")
                .HasColumnName("DONG");
            entity.Property(e => e.Etc)
                .HasMaxLength(255)
                .HasComment("예약 차량의 경우")
                .HasColumnName("ETC");
            entity.Property(e => e.Ho)
                .HasMaxLength(255)
                .HasComment("호")
                .HasColumnName("HO");
            entity.Property(e => e.ImgPath)
                .HasMaxLength(255)
                .HasComment("이미지 경로")
                .HasColumnName("IMG_PATH");
            entity.Property(e => e.IoDtm)
                .HasComment("입-출차 일시")
                .HasColumnType("datetime")
                .HasColumnName("IO_DTM");
            entity.Property(e => e.IoGateId)
                .HasMaxLength(255)
                .HasComment("입-출차 GATE ID")
                .HasColumnName("IO_GATE_ID");
            entity.Property(e => e.IoGateNm)
                .HasMaxLength(255)
                .HasComment("입-출차 GATE NM")
                .HasColumnName("IO_GATE_NM");
            entity.Property(e => e.IoGubun)
                .HasComment("입출차 구분")
                .HasColumnType("int(11)")
                .HasColumnName("IO_GUBUN");
            entity.Property(e => e.IoLineNum)
                .HasComment("입-출차 라인번호")
                .HasColumnType("int(11)")
                .HasColumnName("IO_LINE_NUM");
            entity.Property(e => e.IoLprStatus)
                .HasMaxLength(255)
                .HasComment("입-출차 LPR 상태 ID")
                .HasColumnName("IO_LPR_STATUS");
            entity.Property(e => e.IoLprStatusNm)
                .HasMaxLength(255)
                .HasComment("입-출차 LPR 상태 명칭")
                .HasColumnName("IO_LPR_STATUS_NM");
            entity.Property(e => e.IoSeq)
                .HasMaxLength(255)
                .HasComment("입출차 일련번호")
                .HasColumnName("IO_SEQ");
            entity.Property(e => e.IoStatusTp)
                .HasMaxLength(255)
                .HasComment("입출 상태")
                .HasColumnName("IO_STATUS_TP");
            entity.Property(e => e.IoStatusTpNm)
                .HasMaxLength(255)
                .HasComment("입출 상태 명")
                .HasColumnName("IO_STATUS_TP_NM");
            entity.Property(e => e.IoTicketTp)
                .HasMaxLength(255)
                .HasComment("입-출차 차량 구분 ID")
                .HasColumnName("IO_TICKET_TP");
            entity.Property(e => e.IoTicketTpNm)
                .HasMaxLength(255)
                .HasComment("입-출차 차량 구분")
                .HasColumnName("IO_TICKET_TP_NM");
            entity.Property(e => e.IsBlackList)
                .HasMaxLength(255)
                .HasComment("블랙리스트 여부")
                .HasColumnName("IS_BLACK_LIST");
            entity.Property(e => e.IsReservation)
                .HasMaxLength(255)
                .HasComment("예약차량여부")
                .HasColumnName("IS_RESERVATION");
            entity.Property(e => e.IsWait)
                .HasMaxLength(255)
                .HasComment("(입차전용) 해당차량을 입차처리할건지 대기할건지")
                .HasColumnName("IS_WAIT");
            entity.Property(e => e.IsWaitReason)
                .HasMaxLength(255)
                .HasComment("(입차전용) 대기 걸린 차량의 이유")
                .HasColumnName("IS_WAIT_REASON");
            entity.Property(e => e.ParkDuration)
                .HasComment("주차시간")
                .HasColumnType("int(11)")
                .HasColumnName("PARK_DURATION");
            entity.Property(e => e.ParkId)
                .HasMaxLength(255)
                .HasComment("주차장 ID")
                .HasColumnName("PARK_ID");
            entity.Property(e => e.RegDtm)
                .HasMaxLength(255)
                .IsFixedLength()
                .HasComment("블랙리스트 등록 일시")
                .HasColumnName("REG_DTM");
            entity.Property(e => e.SPid)
                .HasComment("IO_ParkingViewTB FK")
                .HasColumnType("int(11)")
                .HasColumnName("S_PID");
            entity.Property(e => e.VisitTime)
                .HasComment("방문 시간 (방문포인트 사용 시간)")
                .HasColumnType("int(11)")
                .HasColumnName("VISIT_TIME");

            entity.HasOne(d => d.SP).WithMany(p => p.IoParkingHistories)
                .HasForeignKey(d => d.SPid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ViewTB_202503130949");
        });

        modelBuilder.Entity<IoParkingViewTb>(entity =>
        {
            entity.HasKey(e => e.Pid).HasName("PRIMARY");

            entity.ToTable("IO_ParkingViewTB", tb => tb.HasComment("더함비즈 API_입출차 테이블"));

            entity.Property(e => e.Pid)
                .HasColumnType("int(11)")
                .HasColumnName("PID");
            entity.Property(e => e.BlackListReason)
                .HasMaxLength(255)
                .HasComment("블랙리스트 사유")
                .HasColumnName("BLACK_LIST_REASON");
            entity.Property(e => e.CarNum)
                .HasMaxLength(255)
                .HasComment("차량 번호")
                .HasColumnName("CAR_NUM");
            entity.Property(e => e.Dong)
                .HasMaxLength(255)
                .HasComment("동")
                .HasColumnName("DONG");
            entity.Property(e => e.Etc)
                .HasMaxLength(255)
                .HasComment("ETC")
                .HasColumnName("ETC");
            entity.Property(e => e.Ho)
                .HasMaxLength(255)
                .HasComment("호")
                .HasColumnName("HO");
            entity.Property(e => e.InCreateDt)
                .HasDefaultValueSql("current_timestamp()")
                .HasComment("시스템 생성일자")
                .HasColumnType("datetime")
                .HasColumnName("IN_CREATE_DT");
            entity.Property(e => e.InDtm)
                .HasComment("입차 시간 yyyy-MM-dd HH:mm:ss")
                .HasColumnType("datetime")
                .HasColumnName("IN_DTM");
            entity.Property(e => e.InGateId)
                .HasMaxLength(255)
                .HasComment("입차 게이트 ID")
                .HasColumnName("IN_GATE_ID");
            entity.Property(e => e.InGateNm)
                .HasMaxLength(255)
                .HasComment("입차 게이트 명")
                .HasColumnName("IN_GATE_NM");
            entity.Property(e => e.InImagePath)
                .HasMaxLength(255)
                .HasComment("이미지 경로(입차)")
                .HasColumnName("IN_IMAGE_PATH");
            entity.Property(e => e.InLineNum)
                .HasComment("입차 라인 번호")
                .HasColumnType("int(11)")
                .HasColumnName("IN_LINE_NUM");
            entity.Property(e => e.InLprStatus)
                .HasMaxLength(255)
                .HasComment("입차 LPR 상태")
                .HasColumnName("IN_LPR_STATUS");
            entity.Property(e => e.InLprStatusNm)
                .HasMaxLength(255)
                .HasComment("입차 LPR 상태 명칭")
                .HasColumnName("IN_LPR_STATUS_NM");
            entity.Property(e => e.InTicketTp)
                .HasMaxLength(255)
                .HasComment("입차 차량 구분 # 2 : 일반차량 6 : 정기차량")
                .HasColumnName("IN_TICKET_TP");
            entity.Property(e => e.InTicketTpNm)
                .HasMaxLength(255)
                .HasComment("입차 차량 구분 명")
                .HasColumnName("IN_TICKET_TP_NM");
            entity.Property(e => e.IoSeq)
                .HasMaxLength(255)
                .HasComment("입출차 일련번호")
                .HasColumnName("IO_SEQ");
            entity.Property(e => e.IoStatusTp)
                .HasMaxLength(255)
                .HasComment("입출 상태")
                .HasColumnName("IO_STATUS_TP");
            entity.Property(e => e.IoStatusTpNm)
                .HasMaxLength(255)
                .HasComment("입출 상태명")
                .HasColumnName("IO_STATUS_TP_NM");
            entity.Property(e => e.IsBlackList)
                .HasMaxLength(255)
                .HasComment("블랙리스트 여부 (입차처리 *블랙리스트로 표시) OR 입차대기 걸고 블랙리스트 표시")
                .HasColumnName("IS_BLACK_LIST");
            entity.Property(e => e.IsReservation)
                .HasMaxLength(255)
                .HasComment("예약차량여부 0,1")
                .HasColumnName("IS_RESERVATION");
            entity.Property(e => e.IsWait)
                .HasMaxLength(255)
                .HasComment("해당차량을 입차처리할건지 대기처리할건지 0: 입차, 1: 입차대기, 2:입차대기후 승인")
                .HasColumnName("IS_WAIT");
            entity.Property(e => e.IsWaitReason)
                .HasMaxLength(255)
                .HasComment("대기 걸린 차량의 이유 - 방문차량, 블랙리스트, 진입금지 그룹")
                .HasColumnName("IS_WAIT_REASON");
            entity.Property(e => e.Memo)
                .HasMaxLength(255)
                .HasComment("메모")
                .HasColumnName("MEMO");
            entity.Property(e => e.OutCreateDt)
                .HasDefaultValueSql("current_timestamp()")
                .HasComment("시스템 생성일자")
                .HasColumnType("datetime")
                .HasColumnName("OUT_CREATE_DT");
            entity.Property(e => e.OutDtm)
                .HasMaxLength(255)
                .HasComment("출차 시간")
                .HasColumnName("OUT_DTM");
            entity.Property(e => e.OutGateId)
                .HasMaxLength(255)
                .HasComment("출차 게이트 ID")
                .HasColumnName("OUT_GATE_ID");
            entity.Property(e => e.OutGateNm)
                .HasMaxLength(255)
                .HasComment("출차 게이트 명")
                .HasColumnName("OUT_GATE_NM");
            entity.Property(e => e.OutImagePath)
                .HasMaxLength(255)
                .HasComment("이미지 경로(출차)")
                .HasColumnName("OUT_IMAGE_PATH");
            entity.Property(e => e.OutLineNum)
                .HasComment("출차 라인 번호")
                .HasColumnType("int(11)")
                .HasColumnName("OUT_LINE_NUM");
            entity.Property(e => e.OutLprStatus)
                .HasMaxLength(255)
                .HasComment("출차 LPR 상태")
                .HasColumnName("OUT_LPR_STATUS");
            entity.Property(e => e.OutLprStatusNm)
                .HasMaxLength(255)
                .HasComment("출차 LPR 상태 명칭")
                .HasColumnName("OUT_LPR_STATUS_NM");
            entity.Property(e => e.OutTicketTp)
                .HasMaxLength(255)
                .HasComment("출차 차량 구분")
                .HasColumnName("OUT_TICKET_TP");
            entity.Property(e => e.OutTicketTpNm)
                .HasMaxLength(255)
                .HasComment("출차 차량 구분 명")
                .HasColumnName("OUT_TICKET_TP_NM");
            entity.Property(e => e.ParkId)
                .HasMaxLength(255)
                .HasComment("주차장ID")
                .HasColumnName("PARK_ID");
            entity.Property(e => e.ParkingDuration)
                .HasComment("주차 시간")
                .HasColumnType("int(11)")
                .HasColumnName("PARKING_DURATION");
            entity.Property(e => e.RegDtm)
                .HasMaxLength(255)
                .HasComment("블랙리스트 등록일시")
                .HasColumnName("REG_DTM");
            entity.Property(e => e.VisitTime)
                .HasComment("방문 시간 (방문포인트 사용 시간)")
                .HasColumnType("int(11)")
                .HasColumnName("VISIT_TIME");
        });

        modelBuilder.Entity<PatrolLogTblist>(entity =>
        {
            entity.HasKey(e => e.Pid).HasName("PRIMARY");

            entity.ToTable("PatrolLogTBlist");

            entity.HasIndex(e => e.SPid, "PatrolPadLogTB_PID_202503131001");

            entity.Property(e => e.Pid)
                .HasColumnType("int(11)")
                .HasColumnName("PID");
            entity.Property(e => e.CarNum)
                .HasMaxLength(255)
                .HasComment("차량 번호")
                .HasColumnName("CAR_NUM");
            entity.Property(e => e.CreateDt)
                .HasDefaultValueSql("current_timestamp()")
                .HasComment("시스템 생성시간")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DT");
            entity.Property(e => e.PatrolCode)
                .HasComment("순찰 상태 코드 0: 정상(입주민), 1: 방문객, 2: 순찰, 3:위반(블랙리스트)")
                .HasColumnType("int(11)")
                .HasColumnName("PATROL_CODE");
            entity.Property(e => e.PatrolDtm)
                .HasComment("순찰 일시")
                .HasColumnType("datetime")
                .HasColumnName("PATROL_DTM");
            entity.Property(e => e.PatrolImg)
                .HasMaxLength(255)
                .HasComment("순찰 이미지")
                .HasColumnName("PATROL_IMG");
            entity.Property(e => e.PatrolName)
                .HasMaxLength(255)
                .HasComment("순찰 상태 명")
                .HasColumnName("PATROL_NAME");
            entity.Property(e => e.PatrolRemark)
                .HasMaxLength(255)
                .HasComment("순찰 비고")
                .HasColumnName("PATROL_REMARK");
            entity.Property(e => e.SPid)
                .HasComment("순찰정보 외래키")
                .HasColumnType("int(11)")
                .HasColumnName("S_PID");

            entity.HasOne(d => d.SP).WithMany(p => p.PatrolLogTblists)
                .HasForeignKey(d => d.SPid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PatrolPadLogTB_PID_202503131001");
        });

        modelBuilder.Entity<PatrolPadLogTb>(entity =>
        {
            entity.HasKey(e => e.Pid).HasName("PRIMARY");

            entity.ToTable("PatrolPadLogTB");

            entity.Property(e => e.Pid)
                .HasColumnType("int(11)")
                .HasColumnName("PID");
            entity.Property(e => e.CreateDt)
                .HasDefaultValueSql("current_timestamp()")
                .HasComment("시스템 생성시간")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DT");
            entity.Property(e => e.ParkId)
                .HasMaxLength(255)
                .HasComment("주차장 ID")
                .HasColumnName("PARK_ID");
            entity.Property(e => e.PatrolEndDtm)
                .HasComment("순찰 종료 일시")
                .HasColumnType("datetime")
                .HasColumnName("PATROL_END_DTM");
            entity.Property(e => e.PatrolStartDtm)
                .HasComment("순찰 시작 일시")
                .HasColumnType("datetime")
                .HasColumnName("PATROL_START_DTM");
            entity.Property(e => e.PatrolUserId)
                .HasMaxLength(255)
                .HasComment("순찰 담당자 ID")
                .HasColumnName("PATROL_USER_ID");
            entity.Property(e => e.PatrolUserNm)
                .HasComment("순찰 담당자 이름")
                .HasColumnType("int(11)")
                .HasColumnName("PATROL_USER_NM");
            entity.Property(e => e.TotCnt)
                .HasComment("전체 데이터 개수")
                .HasColumnType("int(11)")
                .HasColumnName("TOT_CNT");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
