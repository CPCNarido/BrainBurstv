-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: localhost:3306
-- Generation Time: Nov 30, 2024 at 02:01 PM
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
('885efb3e-8e0f-4593-b855-6489a1fd5279', '/profile_images/default.png', 'christin', 'Student', 'cnarido78@gmail.com', 'CNARIDO78@GMAIL.COM', 'cnarido78@gmail.com', 'CNARIDO78@GMAIL.COM', b'0', 'AQAAAAIAAYagAAAAEMBJmoWj9jrfvWV27qWPR3/pDj4HAaOFKiAam1gRLkj6vqPfFJedeJjan7mjLojDbA==', '3IUZVKXHLIPTJAXSP2SIHT2XHY2IWZIS', 'a3e33865-3896-4a6c-9c4f-1b0fba355737', NULL, b'0', b'0', NULL, b'1', 0),
('e5dac1ad-8550-4075-be0d-664f8f9cb15b', '/profile_images/default.png', 'Christian Paul Narido', 'Professor', 'cnarido7ss8@gmail.com', 'CNARIDO7SS8@GMAIL.COM', 'cnarido7ss8@gmail.com', 'CNARIDO7SS8@GMAIL.COM', b'0', 'AQAAAAIAAYagAAAAEAWtxybUTmZV8qrG6l3eoVkUpAf7lKUpAFiwcqi1u0We9QSWmYPIEeGm0OJpRAzEhQ==', 'I3DEKBMNZNLEZCKH2MCGKDO7B6YWQ3W3', '4af7d812-5320-4b8d-84ae-d0d1b6cbf3b0', NULL, b'0', b'0', NULL, b'1', 0);

-- --------------------------------------------------------

--
-- Table structure for table `quizzes`
--

CREATE TABLE `quizzes` (
  `QuizId` int NOT NULL,
  `GradeLevel` varchar(50) NOT NULL,
  `Topic` varchar(100) NOT NULL,
  `CorrectAnswers` text NOT NULL,
  `JsonFilePath` text NOT NULL,
  `UserId` varchar(450) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `quizzes`
--

INSERT INTO `quizzes` (`QuizId`, `GradeLevel`, `Topic`, `CorrectAnswers`, `JsonFilePath`, `UserId`) VALUES
(1, 'College', 'OOP(OBJECT ORIENTED PROGRAMMING)', '[]', 'wwwroot\\quizzes\\da728939-07bc-4e5f-851a-cc274b28a599.json', 'e5dac1ad-8550-4075-be0d-664f8f9cb15b'),
(2, 'College', 'OOP(OBJECT ORIENTED PROGRAMMING)', '[]', 'wwwroot\\quizzes\\a6998741-939f-4675-af70-511dcc6d9ac7.json', 'e5dac1ad-8550-4075-be0d-664f8f9cb15b'),
(3, 'College', 'OOP(OBJECT ORIENTED PROGRAMMING)', '[\"d\",\"d\",\"b\",\"a\",\"d\",\"d\",\"b\",\"a\",\"a\",\"b\"]', 'wwwroot\\quizzes\\c2087c14-77e1-46b6-a93d-63ecd49005b0.json', 'e5dac1ad-8550-4075-be0d-664f8f9cb15b');

-- --------------------------------------------------------

--
-- Table structure for table `reviews`
--

CREATE TABLE `reviews` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserName` varchar(255) NOT NULL,
  `UserRole` varchar(255) NOT NULL,
  `Rating` int NOT NULL,
  `Feedback` text,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Indexes for dumped tables
--

--
-- Indexes for table `aspnetusers`
--
ALTER TABLE `aspnetusers`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `quizzes`
--
ALTER TABLE `quizzes`
  ADD PRIMARY KEY (`QuizId`),
  ADD KEY `UserId` (`UserId`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `quizzes`
--
ALTER TABLE `quizzes`
  MODIFY `QuizId` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `quizzes`
--
ALTER TABLE `quizzes`
  ADD CONSTRAINT `quizzes_ibfk_1` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
