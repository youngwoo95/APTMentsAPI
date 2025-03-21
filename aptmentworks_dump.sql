/*M!999999\- enable the sandbox mode */ 
-- MariaDB dump 10.19  Distrib 10.6.21-MariaDB, for Win64 (AMD64)
--
-- Host: localhost    Database: Aptmentworks
-- ------------------------------------------------------
-- Server version	10.6.21-MariaDB

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
-- Table structure for table `io_parkingrows`
--

DROP TABLE IF EXISTS `io_parkingrows`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE `io_parkingrows` (
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
  `REG_DTM` varchar(255) DEFAULT NULL COMMENT '블랙리스트 등록 일시',
  `IMG_PATH` varchar(255) NOT NULL COMMENT '이미지 경로',
  `IS_WAIT` varchar(255) DEFAULT NULL COMMENT '(입차전용) 해당차량을 입차처리할건지 대기할건지',
  `IS_WAIT_REASON` varchar(255) DEFAULT NULL COMMENT '(입차전용) 대기 걸린 차량의 이유',
  `PARK_DURATION` int(11) DEFAULT NULL COMMENT '주차시간',
  `VISIT_TIME` int(11) DEFAULT NULL COMMENT '방문 시간 (방문포인트 사용 시간)',
  `ETC` varchar(255) DEFAULT NULL COMMENT '예약 차량의 경우',
  `CREATE_DT` datetime NOT NULL DEFAULT current_timestamp() COMMENT '시스템 생성 일자',
  `MEMO` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`PID`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=71 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci COMMENT='더함비즈 API_입출차 기록 테이블';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `io_parkingrows`
--

LOCK TABLES `io_parkingrows` WRITE;
/*!40000 ALTER TABLE `io_parkingrows` DISABLE KEYS */;
INSERT INTO `io_parkingrows` VALUES (64,0,'P2177010120240613082249851','2177','44거1010','20','출차','1','정문',2,'2025-03-19 05:38:07','0','','2','방문차량','101','1001','0','0','','','http://thehambizp0002.iptime.org:8000/image/2025\\02\\28\\102\\20250228100533_228오1005.jpg',NULL,NULL,36,36,'','2025-03-18 08:20:01',NULL),(65,1,'P2177010120240613082249851','2177','43가1414','10','입차','1','정문',1,'2024-06-13 05:38:07','0','','2','예약차량','101','1001','1','0','','','http://thehambizp0002.iptime.org:8000/image/2025\\02\\28\\102\\20250228100533_228오1005.jpg','0','',NULL,NULL,'친구방문','2025-03-18 13:19:56',NULL),(66,1,'P2177010120240613082249851','2177','12가1234','10','입차','1','정문',1,'2024-06-13 05:38:07','0','','2','방문차량','101','1001','0','1','허위차량','2024-06-13 05:38:07','http://thehambizp0002.iptime.org:8000/image/2025\\02\\28\\102\\20250228100533_228오1005.jpg','0','',NULL,NULL,'','2025-03-18 13:19:56',NULL),(67,1,'P2177010120240613082249851','2177','12가1234','10','입차','1','정문',1,'2024-06-13 05:38:07','0','','6','정기차량','101','1001','0','0','','','http://thehambizp0002.iptime.org:8000/image/2025\\02\\28\\102\\20250228100533_228오1005.jpg','0','',NULL,NULL,'','2025-03-18 13:19:57',NULL),(68,1,'P2177010120240613082249851','2177','33마3434','10','입차','1','정문',1,'2024-06-13 05:38:07','0','','2','방문차량','101','1001','0','0','','','http://thehambizp0002.iptime.org:8000/image/2025\\02\\28\\102\\20250228100533_228오1005.jpg','0','',NULL,NULL,'','2025-03-18 13:19:58',NULL),(69,1,'P2177010120240613082249851','2177','33마3434','10','입차','1','정문',1,'2024-06-13 05:38:07','0','','2','방문차량','101','1001','0','0','','','http://thehambizp0002.iptime.org:8000/image/2025\\02\\28\\102\\20250228100533_228오1005.jpg','0','',NULL,NULL,'','2025-03-18 13:46:55',NULL),(70,0,'P2177010120240613082249851','2177','44거1010','20','출차','1','정문',2,'2025-03-19 05:38:07','0','','2','방문차량','101','1001','0','0','','','http://thehambizp0002.iptime.org:8000/image/2025\\02\\28\\102\\20250228100533_228오1005.jpg',NULL,NULL,36,36,'','2025-03-18 13:46:58',NULL);
/*!40000 ALTER TABLE `io_parkingrows` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `io_parkingviewtb`
--

DROP TABLE IF EXISTS `io_parkingviewtb`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE `io_parkingviewtb` (
  `PID` int(11) NOT NULL AUTO_INCREMENT,
  `IO_SEQ` varchar(255) NOT NULL COMMENT '입출차 일련번호',
  `IN_STATUS_TP` varchar(255) NOT NULL COMMENT '입출 상태',
  `IN_STATUS_TP_NM` varchar(255) NOT NULL COMMENT '입출 상태 명',
  `IN_PID` int(11) DEFAULT NULL COMMENT '최종입차_PID',
  `IN_DTM` datetime DEFAULT NULL COMMENT '최종입차_DT',
  `OUT_PID` int(11) DEFAULT NULL COMMENT '최종출차_PID',
  `OUT_DTM` datetime DEFAULT NULL COMMENT '최종출차_DT',
  `CAR_NUM` varchar(255) DEFAULT NULL COMMENT '차량번호',
  `IO_TICKET_TP` varchar(255) DEFAULT NULL COMMENT '입-출차 차량 구분 ID',
  `IO_TICKET_TP_NM` varchar(255) DEFAULT NULL COMMENT '입-출차 차량 구분 명',
  `DONG` varchar(255) DEFAULT NULL COMMENT '동',
  `HO` varchar(255) DEFAULT NULL COMMENT '호',
  `PARING_DURATION` int(11) DEFAULT NULL COMMENT '주차시간',
  `IS_BLACK_LIST` varchar(255) DEFAULT NULL COMMENT '블랙리스트 여부',
  `BLACK_LIST_REASON` varchar(255) DEFAULT NULL COMMENT '블랙리스트 사유',
  `MEMO` varchar(255) DEFAULT NULL,
  `UPDATE_DT` datetime NOT NULL DEFAULT current_timestamp() COMMENT 'ROW Update 시간',
  PRIMARY KEY (`PID`),
  UNIQUE KEY `UK_SEQ` (`IO_SEQ`),
  KEY `IN_PID_202503141525` (`IN_PID`),
  KEY `OUT_PID_202503141526` (`OUT_PID`),
  CONSTRAINT `IN_PID_202503141525` FOREIGN KEY (`IN_PID`) REFERENCES `io_parkingrows` (`PID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `OUT_PID_202503141526` FOREIGN KEY (`OUT_PID`) REFERENCES `io_parkingrows` (`PID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci COMMENT='더함비즈 API_입출차 테이블';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `io_parkingviewtb`
--

LOCK TABLES `io_parkingviewtb` WRITE;
/*!40000 ALTER TABLE `io_parkingviewtb` DISABLE KEYS */;
INSERT INTO `io_parkingviewtb` VALUES (12,'P2177010120240613082249851','20','출차',69,'2024-06-13 05:38:07',70,'2024-06-13 05:38:07','44거1010','2','방문차량','101','1001',36,'0','',NULL,'2025-03-18 13:46:58');
/*!40000 ALTER TABLE `io_parkingviewtb` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `patrolpadlogtb`
--

DROP TABLE IF EXISTS `patrolpadlogtb`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8mb4 */;
CREATE TABLE `patrolpadlogtb` (
  `PID` int(11) NOT NULL AUTO_INCREMENT,
  `PARK_ID` varchar(255) NOT NULL COMMENT '주차장 ID',
  `PATROL_USER_ID` int(11) NOT NULL COMMENT '순찰 담당자 ID (사용안함)',
  `PATROL_USER_NM` varchar(255) NOT NULL COMMENT '순찰 담당자 이름',
  `PATROL_START_DTM` datetime NOT NULL COMMENT '순찰 시작 일시 (사용안함)',
  `PATROL_END_DTM` datetime NOT NULL COMMENT '순찰 종료 일시 (사용안함)',
  `TOT_CNT` int(11) NOT NULL COMMENT '전체 데이터 개수 (사용안함)',
  `PATROL_DTM` datetime NOT NULL COMMENT '순찰일시',
  `PATROL_CODE` int(11) NOT NULL COMMENT '순찰 상태 코드 0: 정상(입주민), 1: 방문객, 2: 순착, 3: 위반(블랙리스트)',
  `PATROL_NAME` varchar(255) DEFAULT NULL COMMENT '순찰상태명',
  `CAR_NUM` varchar(255) NOT NULL COMMENT '차량번호',
  `PATROL_IMG` varchar(255) DEFAULT NULL COMMENT '순찰 이미지',
  `PATROl_REMARK` varchar(255) DEFAULT NULL COMMENT '순찰비고',
  `CREATE_DT` datetime NOT NULL DEFAULT current_timestamp() COMMENT '시스템 생성시간',
  PRIMARY KEY (`PID`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `patrolpadlogtb`
--

LOCK TABLES `patrolpadlogtb` WRITE;
/*!40000 ALTER TABLE `patrolpadlogtb` DISABLE KEYS */;
INSERT INTO `patrolpadlogtb` VALUES (5,'P001',123,'홍길동','2025-03-21 09:00:00','2025-03-21 10:00:00',1,'2025-03-21 09:30:00',0,'정상','12가3456','http://thehambizp0002.iptime.org:8000/image/2025\\\\02\\\\28\\\\102\\\\20250228100533_228오1005.jpg','문제 없음','2025-03-21 15:54:35'),(6,'P001',123,'홍길동','2025-03-21 09:00:00','2025-03-21 10:00:00',2,'2025-03-21 09:30:00',0,'정상','12가3456','http://thehambizp0002.iptime.org:8000/image/2025\\\\02\\\\28\\\\102\\\\20250228100533_228오1005.jpg','문제 없음','2025-03-21 15:55:19'),(7,'P001',123,'홍길동','2025-03-21 09:00:00','2025-03-21 10:00:00',2,'2025-03-19 03:30:00',0,'정상','123가1234','http://thehambizp0002.iptime.org:8000/image/2025\\\\02\\\\28\\\\102\\\\20250228100533_228오1005.jpg','문제 없음','2025-03-21 15:55:19'),(8,'P001',123,'홍길동','2025-03-21 09:00:00','2025-03-21 10:00:00',2,'2025-03-21 09:30:00',0,'정상','12가3456','http://thehambizp0002.iptime.org:8000/image/2025\\\\02\\\\28\\\\102\\\\20250228100533_228오1005.jpg','문제 없음','2025-03-21 15:58:24'),(9,'P001',123,'홍길동','2025-03-21 09:00:00','2025-03-21 10:00:00',2,'2025-03-19 03:30:00',0,'정상','123가1234','http://thehambizp0002.iptime.org:8000/image/2025\\\\02\\\\28\\\\102\\\\20250228100533_228오1005.jpg','문제 없음','2025-03-21 15:58:24'),(10,'P001',123,'홍길동','2025-03-21 09:00:00','2025-03-21 10:00:00',2,'2025-03-15 03:30:00',1,'방문객','456가2303','http://thehambizp0002.iptime.org:8000/image/2025\\\\02\\\\28\\\\102\\\\20250228100533_228오1005.jpg','문제 없음','2025-03-21 15:58:24'),(11,'P001',123,'홍길동','2025-03-21 09:00:00','2025-03-21 10:00:00',2,'2025-03-13 03:30:00',2,'순찰','333가2303','http://thehambizp0002.iptime.org:8000/image/2025\\\\02\\\\28\\\\102\\\\20250228100533_228오1005.jpg','문제 없음','2025-03-21 15:58:24'),(12,'P001',123,'홍길동','2025-03-21 09:00:00','2025-03-21 10:00:00',2,'2025-03-11 03:30:00',3,'위반','333가2303','http://thehambizp0002.iptime.org:8000/image/2025\\\\02\\\\28\\\\102\\\\20250228100533_228오1005.jpg','타단지 차량','2025-03-21 15:58:24');
/*!40000 ALTER TABLE `patrolpadlogtb` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-03-21 16:45:12
