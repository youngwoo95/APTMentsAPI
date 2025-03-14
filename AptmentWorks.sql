/*M!999999\- enable the sandbox mode */ 
-- MariaDB dump 10.19  Distrib 10.6.21-MariaDB, for Win64 (AMD64)
--
-- Host: 123.2.159.98    Database: AptmentWorks
-- ------------------------------------------------------
-- Server version	10.6.18-MariaDB-0ubuntu0.22.04.1

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `IO_ParkingRows`
--

DROP TABLE IF EXISTS `IO_ParkingRows`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE `IO_ParkingRows` (
  `PID` int(11) NOT NULL AUTO_INCREMENT,
  `IO_GUBUN` int(11) NOT NULL COMMENT '입출차 구분',
  `IO_SEQ` varchar(255) NOT NULL COMMENT '입출차 일련번호',
  `PARK_ID` varchar(255) NOT NULL COMMENT '주차장 ID',
  `CAR_NUM` varchar(255) NOT NULL COMMENT '차량 번호',
  `IO_STATUS_TP` varchar(255) NOT NULL COMMENT '입출 상태',
  `IO_STATUS_TP_NM` varchar(255) NOT NULL COMMENT '입출 상태 명',
  `IO_GATE_ID` varchar(255) NOT NULL COMMENT '입-출차 GATE ID',
  `IO_GATE_NM` varchar(255) NOT NULL COMMENT '입-출차 GATE NM',
  `IO_LINE_NUM` int(11) NOT NULL COMMENT '입-출차 라인번호',
  `IO_DTM` datetime NOT NULL COMMENT '입-출차 일시',
  `IO_LPR_STATUS` varchar(255) DEFAULT NULL COMMENT '입-출차 LPR 상태 ID',
  `IO_LPR_STATUS_NM` varchar(255) DEFAULT NULL COMMENT '입-출차 LPR 상태 명칭',
  `IO_TICKET_TP` varchar(255) NOT NULL COMMENT '입-출차 차량 구분 ID',
  `IO_TICKET_TP_NM` varchar(255) NOT NULL COMMENT '입-출차 차량 구분',
  `DONG` varchar(255) DEFAULT NULL COMMENT '동',
  `HO` varchar(255) DEFAULT NULL COMMENT '호',
  `IS_RESERVATION` varchar(255) DEFAULT NULL COMMENT '예약차량여부',
  `IS_BLACK_LIST` varchar(255) DEFAULT NULL COMMENT '블랙리스트 여부',
  `BLACK_LIST_REASON` varchar(255) DEFAULT NULL COMMENT '블랙리스트 사유',
  `REG_DTM` char(255) DEFAULT NULL COMMENT '블랙리스트 등록 일시',
  `IMG_PATH` varchar(255) NOT NULL COMMENT '이미지 경로',
  `IS_WAIT` varchar(255) DEFAULT NULL COMMENT '(입차전용) 해당차량을 입차처리할건지 대기할건지',
  `IS_WAIT_REASON` varchar(255) DEFAULT NULL COMMENT '(입차전용) 대기 걸린 차량의 이유',
  `PARK_DURATION` int(11) DEFAULT NULL COMMENT '주차시간',
  `VISIT_TIME` int(11) DEFAULT NULL COMMENT '방문 시간 (방문포인트 사용 시간)',
  `ETC` varchar(255) DEFAULT NULL COMMENT '예약 차량의 경우',
  `CREATE_DT` datetime NOT NULL DEFAULT current_timestamp() COMMENT '시스템 생성 일자',
  PRIMARY KEY (`PID`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci COMMENT='더함비즈 API_입출차 기록 테이블';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `IO_ParkingRows`
--

LOCK TABLES `IO_ParkingRows` WRITE;
/*!40000 ALTER TABLE `IO_ParkingRows` DISABLE KEYS */;
INSERT INTO `IO_ParkingRows` VALUES (17,1,'P2177010120240613082249851','2177','33마3434','10','입차','1','정문',1,'2024-06-13 05:38:07','0','','2','방문차량','101','1001','0','0','','','http://thehambizp0002.iptime.org:8000/image/2025\\02\\28\\102\\20250228100533_228오1005.jpg','0','',NULL,NULL,'','2025-03-14 14:40:04');
/*!40000 ALTER TABLE `IO_ParkingRows` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `IO_ParkingViewTB`
--

DROP TABLE IF EXISTS `IO_ParkingViewTB`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE `IO_ParkingViewTB` (
  `PID` int(11) NOT NULL AUTO_INCREMENT,
  `IO_SEQ` varchar(255) NOT NULL COMMENT '입출차 일련번호',
  `IN_PID` int(11) DEFAULT NULL COMMENT '최종입차_PID',
  `IN_DTM` datetime DEFAULT NULL COMMENT '최종입차_DT',
  `OUT_PID` int(11) DEFAULT NULL COMMENT '최종출차_PID',
  `OUT_DTM` datetime DEFAULT NULL COMMENT '최종출차_DT',
  `CAR_NUM` varchar(255) DEFAULT NULL COMMENT '차량번호',
  `DONG` varchar(255) DEFAULT NULL COMMENT '동',
  `HO` varchar(255) DEFAULT NULL COMMENT '호',
  PRIMARY KEY (`PID`),
  UNIQUE KEY `UK_SEQ` (`IO_SEQ`),
  KEY `IN_PID_202503141525` (`IN_PID`),
  KEY `OUT_PID_202503141526` (`OUT_PID`),
  CONSTRAINT `IN_PID_202503141525` FOREIGN KEY (`IN_PID`) REFERENCES `IO_ParkingRows` (`PID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `OUT_PID_202503141526` FOREIGN KEY (`OUT_PID`) REFERENCES `IO_ParkingRows` (`PID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci COMMENT='더함비즈 API_입출차 테이블';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `IO_ParkingViewTB`
--

LOCK TABLES `IO_ParkingViewTB` WRITE;
/*!40000 ALTER TABLE `IO_ParkingViewTB` DISABLE KEYS */;
INSERT INTO `IO_ParkingViewTB` VALUES (6,'P2177010120240613082249851',NULL,NULL,NULL,NULL,NULL,NULL,NULL);
/*!40000 ALTER TABLE `IO_ParkingViewTB` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `PatrolLogTBlist`
--

DROP TABLE IF EXISTS `PatrolLogTBlist`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE `PatrolLogTBlist` (
  `PID` int(11) NOT NULL AUTO_INCREMENT,
  `PATROL_DTM` datetime NOT NULL COMMENT '순찰 일시',
  `PATROL_CODE` int(11) NOT NULL COMMENT '순찰 상태 코드 0: 정상(입주민), 1: 방문객, 2: 순찰, 3:위반(블랙리스트)',
  `PATROL_NAME` varchar(255) DEFAULT NULL COMMENT '순찰 상태 명',
  `CAR_NUM` varchar(255) NOT NULL COMMENT '차량 번호',
  `PATROL_IMG` varchar(255) DEFAULT NULL COMMENT '순찰 이미지',
  `PATROL_REMARK` varchar(255) DEFAULT NULL COMMENT '순찰 비고',
  `CREATE_DT` datetime DEFAULT current_timestamp() COMMENT '시스템 생성시간',
  `S_PID` int(11) NOT NULL COMMENT '순찰정보 외래키',
  PRIMARY KEY (`PID`),
  KEY `PatrolPadLogTB_PID_202503131001` (`S_PID`),
  CONSTRAINT `PatrolPadLogTB_PID_202503131001` FOREIGN KEY (`S_PID`) REFERENCES `PatrolPadLogTB` (`PID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `PatrolLogTBlist`
--

LOCK TABLES `PatrolLogTBlist` WRITE;
/*!40000 ALTER TABLE `PatrolLogTBlist` DISABLE KEYS */;
/*!40000 ALTER TABLE `PatrolLogTBlist` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `PatrolPadLogTB`
--

DROP TABLE IF EXISTS `PatrolPadLogTB`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE `PatrolPadLogTB` (
  `PID` int(11) NOT NULL AUTO_INCREMENT,
  `PARK_ID` varchar(255) NOT NULL COMMENT '주차장 ID',
  `PATROL_USER_ID` varchar(255) NOT NULL COMMENT '순찰 담당자 ID',
  `PATROL_USER_NM` int(11) NOT NULL COMMENT '순찰 담당자 이름',
  `PATROL_START_DTM` datetime NOT NULL COMMENT '순찰 시작 일시',
  `PATROL_END_DTM` datetime NOT NULL COMMENT '순찰 종료 일시',
  `TOT_CNT` int(11) NOT NULL COMMENT '전체 데이터 개수',
  `CREATE_DT` datetime NOT NULL DEFAULT current_timestamp() COMMENT '시스템 생성시간',
  PRIMARY KEY (`PID`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `PatrolPadLogTB`
--

LOCK TABLES `PatrolPadLogTB` WRITE;
/*!40000 ALTER TABLE `PatrolPadLogTB` DISABLE KEYS */;
/*!40000 ALTER TABLE `PatrolPadLogTB` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-03-14 21:00:26
