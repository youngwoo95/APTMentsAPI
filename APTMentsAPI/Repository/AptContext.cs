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

    public virtual DbSet<IoParkingrow> IoParkingrows { get; set; }

    public virtual DbSet<IoParkingviewtb> IoParkingviewtbs { get; set; }

    public virtual DbSet<Patrolpadlogtb> Patrolpadlogtbs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("server=127.0.0.1;port=3306;database=AptmentWorks;user=root;password=rladyddn!!95", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.6.21-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("latin1_swedish_ci")
            .HasCharSet("latin1");

        modelBuilder.Entity<IoParkingrow>(entity =>
        {
            entity.HasKey(e => e.Pid).HasName("PRIMARY");

            entity
                .ToTable("io_parkingrows", tb => tb.HasComment("더함비즈 API_입출차 기록 테이블"))
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

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
            entity.Property(e => e.Memo)
                .HasMaxLength(255)
                .HasColumnName("MEMO");
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
                .HasComment("블랙리스트 등록 일시")
                .HasColumnName("REG_DTM");
            entity.Property(e => e.VisitTime)
                .HasComment("방문 시간 (방문포인트 사용 시간)")
                .HasColumnType("int(11)")
                .HasColumnName("VISIT_TIME");
        });

        modelBuilder.Entity<IoParkingviewtb>(entity =>
        {
            entity.HasKey(e => e.Pid).HasName("PRIMARY");

            entity
                .ToTable("io_parkingviewtb", tb => tb.HasComment("더함비즈 API_입출차 테이블"))
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.HasIndex(e => e.InPid, "IN_PID_202503141525");

            entity.HasIndex(e => e.OutPid, "OUT_PID_202503141526");

            entity.HasIndex(e => e.IoSeq, "UK_SEQ").IsUnique();

            entity.Property(e => e.Pid)
                .HasColumnType("int(11)")
                .HasColumnName("PID");
            entity.Property(e => e.BlackListReason)
                .HasMaxLength(255)
                .HasComment("블랙리스트 사유")
                .HasColumnName("BLACK_LIST_REASON");
            entity.Property(e => e.CarNum)
                .HasMaxLength(255)
                .HasComment("차량번호")
                .HasColumnName("CAR_NUM");
            entity.Property(e => e.Dong)
                .HasMaxLength(255)
                .HasComment("동")
                .HasColumnName("DONG");
            entity.Property(e => e.Ho)
                .HasMaxLength(255)
                .HasComment("호")
                .HasColumnName("HO");
            entity.Property(e => e.InDtm)
                .HasComment("최종입차_DT")
                .HasColumnType("datetime")
                .HasColumnName("IN_DTM");
            entity.Property(e => e.InPid)
                .HasComment("최종입차_PID")
                .HasColumnType("int(11)")
                .HasColumnName("IN_PID");
            entity.Property(e => e.InStatusTp)
                .HasMaxLength(255)
                .HasComment("입출 상태")
                .HasColumnName("IN_STATUS_TP");
            entity.Property(e => e.InStatusTpNm)
                .HasMaxLength(255)
                .HasComment("입출 상태 명")
                .HasColumnName("IN_STATUS_TP_NM");
            entity.Property(e => e.IoSeq)
                .HasComment("입출차 일련번호")
                .HasColumnName("IO_SEQ");
            entity.Property(e => e.IoTicketTp)
                .HasMaxLength(255)
                .HasComment("입-출차 차량 구분 ID")
                .HasColumnName("IO_TICKET_TP");
            entity.Property(e => e.IoTicketTpNm)
                .HasMaxLength(255)
                .HasComment("입-출차 차량 구분 명")
                .HasColumnName("IO_TICKET_TP_NM");
            entity.Property(e => e.IsBlackList)
                .HasMaxLength(255)
                .HasComment("블랙리스트 여부")
                .HasColumnName("IS_BLACK_LIST");
            entity.Property(e => e.Memo)
                .HasMaxLength(255)
                .HasColumnName("MEMO");
            entity.Property(e => e.OutDtm)
                .HasComment("최종출차_DT")
                .HasColumnType("datetime")
                .HasColumnName("OUT_DTM");
            entity.Property(e => e.OutPid)
                .HasComment("최종출차_PID")
                .HasColumnType("int(11)")
                .HasColumnName("OUT_PID");
            entity.Property(e => e.ParingDuration)
                .HasComment("주차시간")
                .HasColumnType("int(11)")
                .HasColumnName("PARING_DURATION");
            entity.Property(e => e.UpdateDt)
                .HasDefaultValueSql("current_timestamp()")
                .HasComment("ROW Update 시간")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DT");

            entity.HasOne(d => d.InP).WithMany(p => p.IoParkingviewtbInPs)
                .HasForeignKey(d => d.InPid)
                .HasConstraintName("IN_PID_202503141525");

            entity.HasOne(d => d.OutP).WithMany(p => p.IoParkingviewtbOutPs)
                .HasForeignKey(d => d.OutPid)
                .HasConstraintName("OUT_PID_202503141526");
        });

        modelBuilder.Entity<Patrolpadlogtb>(entity =>
        {
            entity.HasKey(e => e.Pid).HasName("PRIMARY");

            entity
                .ToTable("patrolpadlogtb")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.Property(e => e.Pid)
                .HasColumnType("int(11)")
                .HasColumnName("PID");
            entity.Property(e => e.CarNum)
                .HasMaxLength(255)
                .HasComment("차량번호")
                .HasColumnName("CAR_NUM");
            entity.Property(e => e.CreateDt)
                .HasDefaultValueSql("current_timestamp()")
                .HasComment("시스템 생성시간")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DT");
            entity.Property(e => e.ParkId)
                .HasMaxLength(255)
                .HasComment("주차장 ID")
                .HasColumnName("PARK_ID");
            entity.Property(e => e.PatrolCode)
                .HasComment("순찰 상태 코드 0: 정상(입주민), 1: 방문객, 2: 순착, 3: 위반(블랙리스트)")
                .HasColumnType("int(11)")
                .HasColumnName("PATROL_CODE");
            entity.Property(e => e.PatrolDtm)
                .HasComment("순찰일시")
                .HasColumnType("datetime")
                .HasColumnName("PATROL_DTM");
            entity.Property(e => e.PatrolEndDtm)
                .HasComment("순찰 종료 일시 (사용안함)")
                .HasColumnType("datetime")
                .HasColumnName("PATROL_END_DTM");
            entity.Property(e => e.PatrolImg)
                .HasMaxLength(255)
                .HasComment("순찰 이미지")
                .HasColumnName("PATROL_IMG");
            entity.Property(e => e.PatrolName)
                .HasMaxLength(255)
                .HasComment("순찰상태명")
                .HasColumnName("PATROL_NAME");
            entity.Property(e => e.PatrolRemark)
                .HasMaxLength(255)
                .HasComment("순찰비고")
                .HasColumnName("PATROl_REMARK");
            entity.Property(e => e.PatrolStartDtm)
                .HasComment("순찰 시작 일시 (사용안함)")
                .HasColumnType("datetime")
                .HasColumnName("PATROL_START_DTM");
            entity.Property(e => e.PatrolUserId)
                .HasComment("순찰 담당자 ID (사용안함)")
                .HasColumnType("int(11)")
                .HasColumnName("PATROL_USER_ID");
            entity.Property(e => e.PatrolUserNm)
                .HasMaxLength(255)
                .HasComment("순찰 담당자 이름")
                .HasColumnName("PATROL_USER_NM");
            entity.Property(e => e.TotCnt)
                .HasComment("전체 데이터 개수 (사용안함)")
                .HasColumnType("int(11)")
                .HasColumnName("TOT_CNT");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
