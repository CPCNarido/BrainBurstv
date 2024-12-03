-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: localhost:3306
-- Generation Time: Dec 03, 2024 at 09:31 AM
-- Server version: 8.0.30
-- PHP Version: 8.1.10

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `brain_burst`
--

-- --------------------------------------------------------

--
-- Table structure for table `aspnetroleclaims`
--

CREATE TABLE `aspnetroleclaims` (
  `Id` int NOT NULL,
  `RoleId` varchar(450) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `ClaimType` longtext,
  `ClaimValue` longtext
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `aspnetroles`
--

CREATE TABLE `aspnetroles` (
  `Id` varchar(450) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `Name` varchar(256) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `NormalizedName` varchar(256) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `ConcurrencyStamp` longtext
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `aspnetuserclaims`
--

CREATE TABLE `aspnetuserclaims` (
  `Id` int NOT NULL,
  `UserId` varchar(450) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `ClaimType` longtext,
  `ClaimValue` longtext
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `aspnetuserlogins`
--

CREATE TABLE `aspnetuserlogins` (
  `LoginProvider` varchar(128) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `ProviderKey` varchar(128) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `ProviderDisplayName` longtext,
  `UserId` varchar(450) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `aspnetuserroles`
--

CREATE TABLE `aspnetuserroles` (
  `UserId` varchar(450) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `RoleId` varchar(450) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `aspnetusers`
--

CREATE TABLE `aspnetusers` (
  `Id` varchar(450) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `FilePath` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `FullName` longtext NOT NULL,
  `Role` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `UserName` varchar(256) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `NormalizedUserName` varchar(256) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `Email` varchar(256) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `NormalizedEmail` varchar(256) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `EmailConfirmed` bit(1) NOT NULL,
  `PasswordHash` longtext,
  `SecurityStamp` longtext,
  `ConcurrencyStamp` longtext,
  `PhoneNumber` longtext,
  `PhoneNumberConfirmed` bit(1) NOT NULL,
  `TwoFactorEnabled` bit(1) NOT NULL,
  `LockoutEnd` datetime DEFAULT NULL,
  `LockoutEnabled` bit(1) NOT NULL,
  `AccessFailedCount` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `aspnetusers`
--

INSERT INTO `aspnetusers` (`Id`, `FilePath`, `FullName`, `Role`, `UserName`, `NormalizedUserName`, `Email`, `NormalizedEmail`, `EmailConfirmed`, `PasswordHash`, `SecurityStamp`, `ConcurrencyStamp`, `PhoneNumber`, `PhoneNumberConfirmed`, `TwoFactorEnabled`, `LockoutEnd`, `LockoutEnabled`, `AccessFailedCount`) VALUES
('885efb3e-8e0f-4593-b855-6489a1fd5279', '/profile_images/cnarido78@gmail.com_462554032_1603163513889654_4632221763728143409_n-removebg-preview.png', 'Christian Paul Narido', 'Student', 'cnarido78@gmail.com', 'CNARIDO78@GMAIL.COM', 'cnarido78@gmail.com', 'CNARIDO78@GMAIL.COM', b'0', 'AQAAAAIAAYagAAAAEMBJmoWj9jrfvWV27qWPR3/pDj4HAaOFKiAam1gRLkj6vqPfFJedeJjan7mjLojDbA==', '3IUZVKXHLIPTJAXSP2SIHT2XHY2IWZIS', 'b95d831f-af99-48b0-bdd6-d9955336fb8f', NULL, b'0', b'0', NULL, b'1', 0),
('e5dac1ad-8550-4075-be0d-664f8f9cb15b', '/profile_images/default.png', 'Christian Paul Narido', 'Professor', 'cnarido7ss8@gmail.com', 'CNARIDO7SS8@GMAIL.COM', 'cnarido7ss8@gmail.com', 'CNARIDO7SS8@GMAIL.COM', b'0', 'AQAAAAIAAYagAAAAEAWtxybUTmZV8qrG6l3eoVkUpAf7lKUpAFiwcqi1u0We9QSWmYPIEeGm0OJpRAzEhQ==', 'I3DEKBMNZNLEZCKH2MCGKDO7B6YWQ3W3', '4af7d812-5320-4b8d-84ae-d0d1b6cbf3b0', NULL, b'0', b'0', NULL, b'1', 0),
('ef528ca6-3919-444d-9908-0db448c20bc1', '/profile_images/default.png', 'christin', 'Professor', 'cnaridso@gmail.com', 'CNARIDSO@GMAIL.COM', 'cnaridso@gmail.com', 'CNARIDSO@GMAIL.COM', b'0', 'AQAAAAIAAYagAAAAELDWyTArfyG9F4Mrts0T4qsExKtc7/oohGNK1AQG29NcOM+mM4nK7LsONHAYcz+kzg==', '44JUEO3PUSGHEFQHRCDUASRBOLE7HQOS', 'bef120e2-1f52-41ed-a90e-138eb60d8c24', NULL, b'0', b'0', NULL, b'1', 0);

-- --------------------------------------------------------

--
-- Table structure for table `flashcards`
--

CREATE TABLE `flashcards` (
  `Id` int NOT NULL,
  `Title` varchar(255) NOT NULL,
  `Description` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `flashcards`
--

INSERT INTO `flashcards` (`Id`, `Title`, `Description`) VALUES
(1, 'sasd', 'asdasd'),
(2, '', ''),
(3, 'sdas', 'asdasdsad');

-- --------------------------------------------------------

--
-- Table structure for table `questions`
--

CREATE TABLE `questions` (
  `Id` int NOT NULL,
  `QuestionText` text NOT NULL,
  `AnswerText` text NOT NULL,
  `ImageQuestionPath` varchar(255) DEFAULT NULL,
  `ImageAnswerPath` varchar(255) DEFAULT NULL,
  `FlashcardId` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `questions`
--

INSERT INTO `questions` (`Id`, `QuestionText`, `AnswerText`, `ImageQuestionPath`, `ImageAnswerPath`, `FlashcardId`) VALUES
(1, 'dssd', 's', NULL, NULL, 1),
(2, 'asdasdd', 'a', NULL, NULL, 2),
(3, 'asdasdd', 'a', NULL, NULL, 3);

-- --------------------------------------------------------

--
-- Table structure for table `quizzes`
--

CREATE TABLE `quizzes` (
  `QuizId` int NOT NULL,
  `GradeLevel` varchar(255) NOT NULL DEFAULT '',
  `Topic` varchar(255) NOT NULL DEFAULT '',
  `CorrectAnswers` text NOT NULL,
  `JsonFilePath` varchar(255) NOT NULL DEFAULT '',
  `UserId` varchar(450) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `HighestScore` int DEFAULT '0',
  `GameCode` varchar(6) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `quizzes`
--

INSERT INTO `quizzes` (`QuizId`, `GradeLevel`, `Topic`, `CorrectAnswers`, `JsonFilePath`, `UserId`, `HighestScore`, `GameCode`) VALUES
(9, 'Elementary', 'Physics', '[1,1,2,2,2,3]', 'wwwroot\\quizzes\\9246cee3-f2b7-4c14-a57d-312c473a55ee.json', '885efb3e-8e0f-4593-b855-6489a1fd5279', 0, '234255'),
(10, 'Elementary', 'Physics', '[0,2,2,1,2]', 'wwwroot\\quizzes\\d4ff01bd-f7e0-4d4f-b929-ad9e8e8b4fc6.json', '885efb3e-8e0f-4593-b855-6489a1fd5279', 0, '098976'),
(11, 'Elementary', 'Addition', '[1,2,1,1,2]', 'wwwroot\\quizzes\\c429f550-9936-4cb3-893c-e84d828a1e8e.json', '885efb3e-8e0f-4593-b855-6489a1fd5279', 0, '453453'),
(12, 'Highschool', 'Addition', '[3,1,0,0,1,3]', 'wwwroot\\quizzes\\88f7db86-6737-46f4-aef5-a26e1250f67b.json', '885efb3e-8e0f-4593-b855-6489a1fd5279', 0, '123233'),
(13, 'Elementary', 'Physics', '[\"d\"]', 'wwwroot\\quizzes\\bb6012c7-2689-42e3-b61f-79e7e40cfdfc.json', '885efb3e-8e0f-4593-b855-6489a1fd5279', 0, NULL),
(14, 'Highschool', 'Addition', '', '', '885efb3e-8e0f-4593-b855-6489a1fd5279', 0, '208233'),
(16, 'College', 'sdasd', '', '', '885efb3e-8e0f-4593-b855-6489a1fd5279', 0, '945735'),
(17, 'College', 'OOP(OBJECT ORIENTED PROGRAMMING)', '[3,1]', 'wwwroot\\quizzes\\f0b4396f-3f49-4191-b611-3fe1aceed448.json', '885efb3e-8e0f-4593-b855-6489a1fd5279', 0, '128699'),
(18, 'College', 'Psychology', '[\"c\",\"c\",\"b\",\"b\",\"b\",\"d\",\"c\",\"b\",\"c\",\"c\"]', 'wwwroot\\quizzes\\d959680e-0463-4900-ac45-988691b3a83b.json', '885efb3e-8e0f-4593-b855-6489a1fd5279', 0, '234234'),
(19, 'College', 'Psychology', '[\"c\",\"b\",\"b\",\"b\",\"b\",\"b\",\"c\",\"b\",\"c\",\"d\"]', 'wwwroot\\quizzes\\73377844-3d87-4de0-ad03-05b7b414333d.json', '885efb3e-8e0f-4593-b855-6489a1fd5279', 0, '234343'),
(20, 'College', 'Addition', '[\"a\",\"a\",\"a\",\"a\",\"b\",\"b\",\"a\",\"b\",\"b\",\"a\"]', 'wwwroot\\quizzes\\828f08ef-8d78-4c03-93ef-da8b13253df1.json', '885efb3e-8e0f-4593-b855-6489a1fd5279', 0, '342344'),
(21, 'College', 'Addition', '[\"a\",\"a\",\"a\",\"a\",\"c\",\"a\",\"a\",\"a\",\"a\",\"a\"]', 'wwwroot\\quizzes\\2c863111-d9b3-4a7a-bf40-1d03bd9e8f24.json', '885efb3e-8e0f-4593-b855-6489a1fd5279', 0, '786786'),
(22, 'College', 'Addition', '[\"a\",\"a\",\"c\",\"b\",\"c\",\"c\",\"a\",\"b\",\"a\",\"b\"]', 'wwwroot\\quizzes\\1b0907a4-83ff-42f5-9544-45568f94fda8.json', '885efb3e-8e0f-4593-b855-6489a1fd5279', 0, '123232'),
(23, 'College', 'Addition', '[\"b\",\"c\",\"c\",\"a\",\"b\",\"a\",\"a\",\"b\",\"a\",\"a\"]', 'wwwroot\\quizzes\\27999987-0814-4764-b23f-c4ab9505a436.json', '885efb3e-8e0f-4593-b855-6489a1fd5279', 0, '123123'),
(24, 'Elementary', 'add', '[\"b\",\"c\",\"c\",\"c\",\"c\"]', 'wwwroot\\quizzes\\5ff924d3-8e1b-431f-bcf6-cd4b3f7ea569.json', 'ef528ca6-3919-444d-9908-0db448c20bc1', 0, '213123'),
(25, 'Highschool', 'dsdfdsf', '[3]', 'wwwroot\\quizzes\\c98054bd-09e9-4861-abfa-98c1ac809410.json', 'ef528ca6-3919-444d-9908-0db448c20bc1', 0, '948806'),
(26, 'Elementary', 'addition', '[\"b\",\"c\",\"c\",\"c\",\"c\"]', 'wwwroot\\quizzes\\a1378346-dacb-45b0-88c6-208774aa8e63.json', 'ef528ca6-3919-444d-9908-0db448c20bc1', 0, NULL),
(27, 'Elementary', 'Addition', '[\"b\",\"c\",\"c\",\"c\",\"c\"]', 'wwwroot\\quizzes\\fdebe49c-0268-4e42-b041-0f52a1b52421.json', 'ef528ca6-3919-444d-9908-0db448c20bc1', 0, NULL),
(28, 'College', 'Addition', '[1,1,0,1,0]', 'wwwroot\\quizzes\\6f9a7b5c-27e6-4563-8787-caa8950ac059.json', 'ef528ca6-3919-444d-9908-0db448c20bc1', 0, '963180');

--
-- Triggers `quizzes`
--
DELIMITER $$
CREATE TRIGGER `set_default_correct_answers` BEFORE INSERT ON `quizzes` FOR EACH ROW BEGIN
    IF NEW.CorrectAnswers IS NULL THEN
        SET NEW.CorrectAnswers = '';
    END IF;
END
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Table structure for table `reviews`
--

CREATE TABLE `reviews` (
  `Id` int NOT NULL,
  `UserName` varchar(255) NOT NULL,
  `UserRole` varchar(255) NOT NULL,
  `Rating` int NOT NULL,
  `Feedback` text
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `scorerecords`
--

CREATE TABLE `scorerecords` (
  `ScoreRecordId` int NOT NULL,
  `QuizId` int NOT NULL,
  `UserId` varchar(255) NOT NULL,
  `Score` int NOT NULL,
  `SubmissionDate` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `scorerecords`
--

INSERT INTO `scorerecords` (`ScoreRecordId`, `QuizId`, `UserId`, `Score`, `SubmissionDate`) VALUES
(1, 12, '885efb3e-8e0f-4593-b855-6489a1fd5279', 2, '2024-12-03 04:03:13'),
(2, 12, '885efb3e-8e0f-4593-b855-6489a1fd5279', 2, '2024-12-03 04:03:14'),
(3, 12, 'ef528ca6-3919-444d-9908-0db448c20bc1', 3, '2024-12-03 04:04:05'),
(4, 12, 'ef528ca6-3919-444d-9908-0db448c20bc1', 3, '2024-12-03 04:04:13'),
(5, 12, 'ef528ca6-3919-444d-9908-0db448c20bc1', 2, '2024-12-03 04:09:48'),
(6, 12, 'ef528ca6-3919-444d-9908-0db448c20bc1', 2, '2024-12-03 04:09:57'),
(7, 12, 'ef528ca6-3919-444d-9908-0db448c20bc1', 0, '2024-12-03 04:10:56'),
(8, 12, 'ef528ca6-3919-444d-9908-0db448c20bc1', 0, '2024-12-03 04:10:56'),
(9, 12, 'ef528ca6-3919-444d-9908-0db448c20bc1', 0, '2024-12-03 04:10:56'),
(10, 12, 'ef528ca6-3919-444d-9908-0db448c20bc1', 0, '2024-12-03 04:12:07'),
(11, 12, 'ef528ca6-3919-444d-9908-0db448c20bc1', 1, '2024-12-03 04:13:28'),
(12, 12, 'ef528ca6-3919-444d-9908-0db448c20bc1', 1, '2024-12-03 04:13:28'),
(13, 12, 'ef528ca6-3919-444d-9908-0db448c20bc1', 1, '2024-12-03 04:13:28'),
(14, 12, 'ef528ca6-3919-444d-9908-0db448c20bc1', 1, '2024-12-03 04:13:37'),
(15, 12, 'ef528ca6-3919-444d-9908-0db448c20bc1', 0, '2024-12-03 04:15:11'),
(16, 12, 'ef528ca6-3919-444d-9908-0db448c20bc1', 0, '2024-12-03 04:15:20'),
(17, 12, 'ef528ca6-3919-444d-9908-0db448c20bc1', 2, '2024-12-03 04:17:30'),
(18, 12, 'ef528ca6-3919-444d-9908-0db448c20bc1', 4, '2024-12-03 04:28:01'),
(19, 12, 'ef528ca6-3919-444d-9908-0db448c20bc1', 1, '2024-12-03 04:28:14'),
(20, 12, 'ef528ca6-3919-444d-9908-0db448c20bc1', 4, '2024-12-03 04:28:50'),
(21, 12, '885efb3e-8e0f-4593-b855-6489a1fd5279', 2, '2024-12-03 04:35:46'),
(22, 12, '885efb3e-8e0f-4593-b855-6489a1fd5279', 2, '2024-12-03 04:35:54'),
(23, 9, '885efb3e-8e0f-4593-b855-6489a1fd5279', 5, '2024-12-03 06:23:52'),
(24, 9, '885efb3e-8e0f-4593-b855-6489a1fd5279', 5, '2024-12-03 06:23:53'),
(25, 9, '885efb3e-8e0f-4593-b855-6489a1fd5279', 5, '2024-12-03 06:23:53'),
(26, 9, '885efb3e-8e0f-4593-b855-6489a1fd5279', 5, '2024-12-03 06:23:53'),
(27, 9, '885efb3e-8e0f-4593-b855-6489a1fd5279', 5, '2024-12-03 06:23:53'),
(28, 9, '885efb3e-8e0f-4593-b855-6489a1fd5279', 5, '2024-12-03 06:23:53'),
(29, 17, '885efb3e-8e0f-4593-b855-6489a1fd5279', 1, '2024-12-03 06:46:36'),
(30, 17, '885efb3e-8e0f-4593-b855-6489a1fd5279', 2, '2024-12-03 06:48:45'),
(31, 25, 'ef528ca6-3919-444d-9908-0db448c20bc1', 1, '2024-12-03 07:29:07'),
(32, 25, 'ef528ca6-3919-444d-9908-0db448c20bc1', 1, '2024-12-03 07:29:14'),
(33, 25, 'ef528ca6-3919-444d-9908-0db448c20bc1', 1, '2024-12-03 09:01:54'),
(34, 25, 'ef528ca6-3919-444d-9908-0db448c20bc1', 1, '2024-12-03 09:01:59'),
(35, 28, 'ef528ca6-3919-444d-9908-0db448c20bc1', 2, '2024-12-03 09:28:55'),
(36, 28, 'ef528ca6-3919-444d-9908-0db448c20bc1', 1, '2024-12-03 09:29:17'),
(37, 28, 'ef528ca6-3919-444d-9908-0db448c20bc1', 0, '2024-12-03 09:29:31');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `aspnetusers`
--
ALTER TABLE `aspnetusers`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `flashcards`
--
ALTER TABLE `flashcards`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `questions`
--
ALTER TABLE `questions`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Questions_FlashcardId` (`FlashcardId`);

--
-- Indexes for table `quizzes`
--
ALTER TABLE `quizzes`
  ADD PRIMARY KEY (`QuizId`),
  ADD KEY `UserId` (`UserId`);

--
-- Indexes for table `reviews`
--
ALTER TABLE `reviews`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `scorerecords`
--
ALTER TABLE `scorerecords`
  ADD PRIMARY KEY (`ScoreRecordId`),
  ADD KEY `QuizId` (`QuizId`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `flashcards`
--
ALTER TABLE `flashcards`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `questions`
--
ALTER TABLE `questions`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `quizzes`
--
ALTER TABLE `quizzes`
  MODIFY `QuizId` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=29;

--
-- AUTO_INCREMENT for table `reviews`
--
ALTER TABLE `reviews`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `scorerecords`
--
ALTER TABLE `scorerecords`
  MODIFY `ScoreRecordId` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=38;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `questions`
--
ALTER TABLE `questions`
  ADD CONSTRAINT `FK_Questions_Flashcards_FlashcardId` FOREIGN KEY (`FlashcardId`) REFERENCES `flashcards` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `quizzes`
--
ALTER TABLE `quizzes`
  ADD CONSTRAINT `quizzes_ibfk_1` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`);

--
-- Constraints for table `scorerecords`
--
ALTER TABLE `scorerecords`
  ADD CONSTRAINT `scorerecords_ibfk_1` FOREIGN KEY (`QuizId`) REFERENCES `quizzes` (`QuizId`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
