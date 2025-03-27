using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APTmentsAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "latin1");

            migrationBuilder.CreateTable(
                name: "apartmentname",
                columns: table => new
                {
                    PID = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    APTName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.PID);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "io_parkingrows",
                columns: table => new
                {
                    PID = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IO_GUBUN = table.Column<int>(type: "int(11)", nullable: false, comment: "입출차 구분"),
                    IO_SEQ = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "입출차 일련번호", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PARK_ID = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "주차장 ID", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CAR_NUM = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "차량 번호", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IO_STATUS_TP = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "입출 상태", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IO_STATUS_TP_NM = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "입출 상태 명", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IO_GATE_ID = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "입-출차 GATE ID", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IO_GATE_NM = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "입-출차 GATE NM", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IO_LINE_NUM = table.Column<int>(type: "int(11)", nullable: false, comment: "입-출차 라인번호"),
                    IO_DTM = table.Column<DateTime>(type: "datetime", nullable: false, comment: "입-출차 일시"),
                    IO_LPR_STATUS = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, comment: "입-출차 LPR 상태 ID", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IO_LPR_STATUS_NM = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, comment: "입-출차 LPR 상태 명칭", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IO_TICKET_TP = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "입-출차 차량 구분 ID", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IO_TICKET_TP_NM = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "입-출차 차량 구분", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DONG = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, comment: "동", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HO = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, comment: "호", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IS_RESERVATION = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, comment: "예약차량여부", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IS_BLACK_LIST = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, comment: "블랙리스트 여부", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BLACK_LIST_REASON = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, comment: "블랙리스트 사유", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    REG_DTM = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, comment: "블랙리스트 등록 일시", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IMG_PATH = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "이미지 경로", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IS_WAIT = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, comment: "(입차전용) 해당차량을 입차처리할건지 대기할건지", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IS_WAIT_REASON = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, comment: "(입차전용) 대기 걸린 차량의 이유", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PARK_DURATION = table.Column<int>(type: "int(11)", nullable: true, comment: "주차시간"),
                    VISIT_TIME = table.Column<int>(type: "int(11)", nullable: true, comment: "방문 시간 (방문포인트 사용 시간)"),
                    ETC = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, comment: "예약 차량의 경우", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CREATE_DT = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "current_timestamp()", comment: "시스템 생성 일자"),
                    MEMO = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.PID);
                },
                comment: "더함비즈 API_입출차 기록 테이블")
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "patrolpadlogtb",
                columns: table => new
                {
                    PID = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PARK_ID = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "주차장 ID", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PATROL_USER_ID = table.Column<int>(type: "int(11)", nullable: false, comment: "순찰 담당자 ID (사용안함)"),
                    PATROL_USER_NM = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "순찰 담당자 이름", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PATROL_START_DTM = table.Column<DateTime>(type: "datetime", nullable: false, comment: "순찰 시작 일시 (사용안함)"),
                    PATROL_END_DTM = table.Column<DateTime>(type: "datetime", nullable: false, comment: "순찰 종료 일시 (사용안함)"),
                    TOT_CNT = table.Column<int>(type: "int(11)", nullable: false, comment: "전체 데이터 개수 (사용안함)"),
                    PATROL_DTM = table.Column<DateTime>(type: "datetime", nullable: false, comment: "순찰일시"),
                    PATROL_CODE = table.Column<int>(type: "int(11)", nullable: false, comment: "순찰 상태 코드 1: 위반(블랙리스트), 2: 정상(입주민), 3: 방문객(현장), 4:방문객(예약)"),
                    PATROL_NAME = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, comment: "순찰상태명", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CAR_NUM = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "차량번호", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PATROL_IMG = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, comment: "순찰 이미지", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PATROl_REMARK = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, comment: "순찰비고", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CREATE_DT = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "current_timestamp()", comment: "시스템 생성시간")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.PID);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "io_parkingviewtb",
                columns: table => new
                {
                    PID = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IO_SEQ = table.Column<string>(type: "varchar(255)", nullable: false, comment: "입출차 일련번호", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IN_STATUS_TP = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "입출 상태", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IN_STATUS_TP_NM = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "입출 상태 명", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IN_PID = table.Column<int>(type: "int(11)", nullable: true, comment: "최종입차_PID"),
                    IN_DTM = table.Column<DateTime>(type: "datetime", nullable: true, comment: "최종입차_DT"),
                    OUT_PID = table.Column<int>(type: "int(11)", nullable: true, comment: "최종출차_PID"),
                    OUT_DTM = table.Column<DateTime>(type: "datetime", nullable: true, comment: "최종출차_DT"),
                    CAR_NUM = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, comment: "차량번호", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IO_TICKET_TP = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, comment: "입-출차 차량 구분 ID", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IO_TICKET_TP_NM = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, comment: "입-출차 차량 구분 명", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DONG = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, comment: "동", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HO = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, comment: "호", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PARING_DURATION = table.Column<int>(type: "int(11)", nullable: true, comment: "주차시간"),
                    IS_BLACK_LIST = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, comment: "블랙리스트 여부", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BLACK_LIST_REASON = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, comment: "블랙리스트 사유", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MEMO = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UPDATE_DT = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "current_timestamp()", comment: "ROW Update 시간")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.PID);
                    table.ForeignKey(
                        name: "IN_PID_202503141525",
                        column: x => x.IN_PID,
                        principalTable: "io_parkingrows",
                        principalColumn: "PID");
                    table.ForeignKey(
                        name: "OUT_PID_202503141526",
                        column: x => x.OUT_PID,
                        principalTable: "io_parkingrows",
                        principalColumn: "PID");
                },
                comment: "더함비즈 API_입출차 테이블")
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateIndex(
                name: "IN_PID_202503141525",
                table: "io_parkingviewtb",
                column: "IN_PID");

            migrationBuilder.CreateIndex(
                name: "OUT_PID_202503141526",
                table: "io_parkingviewtb",
                column: "OUT_PID");

            migrationBuilder.CreateIndex(
                name: "UK_SEQ",
                table: "io_parkingviewtb",
                column: "IO_SEQ",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "apartmentname");

            migrationBuilder.DropTable(
                name: "io_parkingviewtb");

            migrationBuilder.DropTable(
                name: "patrolpadlogtb");

            migrationBuilder.DropTable(
                name: "io_parkingrows");
        }
    }
}
