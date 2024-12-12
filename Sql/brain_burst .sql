-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: localhost:3306
-- Generation Time: Dec 11, 2024 at 04:10 AM
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
  `AccessFailedCount` int NOT NULL,
  `Created_At` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `aspnetusers`
--

INSERT INTO `aspnetusers` (`Id`, `FilePath`, `FullName`, `Role`, `UserName`, `NormalizedUserName`, `Email`, `NormalizedEmail`, `EmailConfirmed`, `PasswordHash`, `SecurityStamp`, `ConcurrencyStamp`, `PhoneNumber`, `PhoneNumberConfirmed`, `TwoFactorEnabled`, `LockoutEnd`, `LockoutEnabled`, `AccessFailedCount`, `Created_At`) VALUES
('11111111-1111-1111-1111-111111111111', '/profile_images/default.png', 'John Doe', 'Student', 'johndoe@example.com', 'JOHNDOE@EXAMPLE.COM', 'johndoe@example.com', 'JOHNDOE@EXAMPLE.COM', b'0', 'AQAAAAIAAYagAAAAEJmS2qCfDl8JcKCS4t0HTB0R/542bD9hGYAoMVUHhMrXgPq2lGQoSKjOyr4mn+vRUw==', '36YKRWUKRLE7EV6TADBZ6QV3BAONDCKV', 'dfa969cd-5961-46b4-b2bc-a767ea65bc50', NULL, b'0', b'0', NULL, b'1', 0, '2024-12-04 10:04:11'),
('22222222-2222-2222-2222-222222222222', '/profile_images/default.png', 'Jane Smith', 'Professor', 'janesmith@example.com', 'JANESMITH@EXAMPLE.COM', 'janesmith@example.com', 'JANESMITH@EXAMPLE.COM', b'0', 'AQAAAAIAAYagAAAAEAWtxybUTmZV8qrG6l3eoVkUpAf7lKUpAFiwcqi1u0We9QSWmYPIEeGm0OJpRAzEhQ==', 'I3DEKBMNZNLEZCKH2MCGKDO7B6YWQ3W3', '4af7d812-5320-4b8d-84ae-d0d1b6cbf3b0', NULL, b'0', b'0', NULL, b'1', 0, '2024-12-04 10:04:11'),
('33333333-3333-3333-3333-333333333333', '/profile_images/default.png', 'Alice Johnson', 'Admin', 'alicejohnson@example.com', 'ALICEJOHNSON@EXAMPLE.COM', 'alicejohnson@example.com', 'ALICEJOHNSON@EXAMPLE.COM', b'0', 'AQAAAAIAAYagAAAAELDWyTArfyG9F4Mrts0T4qsExKtc7/oohGNK1AQG29NcOM+mM4nK7LsONHAYcz+kzg==', '44JUEO3PUSGHEFQHRCDUASRBOLE7HQOS', 'a50211f0-5ecf-4816-98c3-4758f98026aa', NULL, b'0', b'0', NULL, b'1', 0, '2024-12-04 10:04:11'),
('44444444-4444-4444-4444-444444444444', '/profile_images/default.png', 'Bob Brown', 'Student', 'bobbrown@example.com', 'BOBBROWN@EXAMPLE.COM', 'bobbrown@example.com', 'BOBBROWN@EXAMPLE.COM', b'0', 'AQAAAAIAAYagAAAAEMBJmoWj9jrfvWV27qWPR3/pDj4HAaOFKiAam1gRLkj6vqPfFJedeJjan7mjLojDbA==', '3IUZVKXHLIPTJAXSP2SIHT2XHY2IWZIS', 'b95d831f-af99-48b0-bdd6-d9955336fb8f', NULL, b'0', b'0', NULL, b'1', 0, '2024-12-04 10:04:11'),
('55555555-5555-5555-5555-555555555555', '/profile_images/default.png', 'Charlie Davis', 'Professor', 'charliedavis@example.com', 'CHARLIEDAVIS@EXAMPLE.COM', 'charliedavis@example.com', 'CHARLIEDAVIS@EXAMPLE.COM', b'0', 'AQAAAAIAAYagAAAAEJmS2qCfDl8JcKCS4t0HTB0R/542bD9hGYAoMVUHhMrXgPq2lGQoSKjOyr4mn+vRUw==', '36YKRWUKRLE7EV6TADBZ6QV3BAONDCKV', 'dfa969cd-5961-46b4-b2bc-a767ea65bc50', NULL, b'0', b'0', NULL, b'1', 0, '2024-12-04 10:04:11'),
('581e473b-7763-4c6c-8a0c-74872f0ef0fa', '/profile_images/cnarido7ssss8@gmail.com_Green_Blue_Happy_Kids_Colorful_Achievement_Certificate_20241208 (2).png', 'Christian Paul NaridSDo', 'Admin', 'cnarido7ssss8@gmail.com', 'CNARIDO7SSSS8@GMAIL.COM', 'cnarido7ssss8@gmail.com', 'CNARIDO7SSSS8@GMAIL.COM', b'0', 'AQAAAAIAAYagAAAAEOQnWgsqE7LCuZhRQoLF7lQzbhpmA3fa3rgXC3jHJFXEQvC0nV360GQUpmgAcX/NOA==', 'CIHLKRRJQOTESEN7QLBESBFWUHZOWAIK', 'b05250b8-2914-41f4-9021-132598c199b4', NULL, b'0', b'0', NULL, b'1', 0, '2024-12-04 10:49:35'),
('885efb3e-8e0f-4593-b855-6489a1fd5279', '/profile_images/cnarido78@gmail.com_462554032_1603163513889654_4632221763728143409_n-removebg-preview.png', 'Christian Paul Narido', 'Admin', 'cnarido78@gmail.com', 'CNARIDO78@GMAIL.COM', 'cnarido78@gmail.com', 'CNARIDO78@GMAIL.COM', b'0', 'AQAAAAIAAYagAAAAEMBJmoWj9jrfvWV27qWPR3/pDj4HAaOFKiAam1gRLkj6vqPfFJedeJjan7mjLojDbA==', '3IUZVKXHLIPTJAXSP2SIHT2XHY2IWZIS', 'b95d831f-af99-48b0-bdd6-d9955336fb8f', NULL, b'0', b'0', NULL, b'1', 0, '2024-12-04 10:04:11'),
('9554553f-d996-4a05-9976-546e241fd136', '/profile_images/default.png', 'christinsdasd', 'Student', 'cnarido78s@gmail.com', 'CNARIDO78S@GMAIL.COM', 'cnarido78s@gmail.com', 'CNARIDO78S@GMAIL.COM', b'0', 'AQAAAAIAAYagAAAAEJmS2qCfDl8JcKCS4t0HTB0R/542bD9hGYAoMVUHhMrXgPq2lGQoSKjOyr4mn+vRUw==', '36YKRWUKRLE7EV6TADBZ6QV3BAONDCKV', 'dfa969cd-5961-46b4-b2bc-a767ea65bc50', NULL, b'0', b'0', NULL, b'1', 0, '2024-12-04 10:04:11'),
('bff75696-aec3-4ce3-8275-744303eb45d6', '/profile_images/default.png', 'Christian Paul Narido', 'Student', 'cnarido7sss8@gmail.com', 'CNARIDO7SSS8@GMAIL.COM', 'cnarido7sss8@gmail.com', 'CNARIDO7SSS8@GMAIL.COM', b'0', 'AQAAAAIAAYagAAAAEEIQ7zgjevO1TWQM5xba7L8hVv5U4iJGrp1lbiTNfopRiBE4Cs/G3fcdnyTUCb23lw==', 'KDTYAR3S5KEQIIMP5F2Q3RNN3NRZRBWQ', 'cc65c277-f4bc-48b8-8829-7e070dd0056b', NULL, b'0', b'0', NULL, b'1', 0, '0001-01-01 00:00:00'),
('e5dac1ad-8550-4075-be0d-664f8f9cb15b', '/profile_images/default.png', 'Christian Paul Narido', 'Admin', 'cnarido7ss8@gmail.com', 'CNARIDO7SS8@GMAIL.COM', 'cnarido7ss8@gmail.com', 'CNARIDO7SS8@GMAIL.COM', b'0', 'AQAAAAIAAYagAAAAEAWtxybUTmZV8qrG6l3eoVkUpAf7lKUpAFiwcqi1u0We9QSWmYPIEeGm0OJpRAzEhQ==', 'I3DEKBMNZNLEZCKH2MCGKDO7B6YWQ3W3', '4af7d812-5320-4b8d-84ae-d0d1b6cbf3b0', NULL, b'0', b'0', NULL, b'1', 0, '2024-12-04 10:04:11'),
('ef528ca6-3919-444d-9908-0db448c20bc1', '/profile_images/cnaridso@gmail.com_b86f4331-ed1a-472c-a661-6c9a1357f239.jfif', 'Christian Paul Narido', 'Student', 'cnaridso@gmail.com', 'CNARIDSO@GMAIL.COM', 'cnaridso@gmail.com', 'CNARIDSO@GMAIL.COM', b'0', 'AQAAAAIAAYagAAAAELDWyTArfyG9F4Mrts0T4qsExKtc7/oohGNK1AQG29NcOM+mM4nK7LsONHAYcz+kzg==', '44JUEO3PUSGHEFQHRCDUASRBOLE7HQOS', 'a50211f0-5ecf-4816-98c3-4758f98026aa', NULL, b'0', b'0', NULL, b'1', 0, '2024-12-04 10:04:11');

-- --------------------------------------------------------

--
-- Table structure for table `flashcards`
--

CREATE TABLE `flashcards` (
  `Id` int NOT NULL,
  `Title` varchar(255) NOT NULL,
  `Description` text NOT NULL,
  `JsonFilePath` varchar(255) DEFAULT NULL,
  `CreatedBy` varchar(450) NOT NULL,
  `CreatedAt` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UserId` varchar(450) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `flashcards`
--

INSERT INTO `flashcards` (`Id`, `Title`, `Description`, `JsonFilePath`, `CreatedBy`, `CreatedAt`, `UserId`) VALUES
(2, 'Addition', 'Flashcard for Highschool on Addition', 'wwwroot/flashcards/a3ce073d-b908-4663-936f-92bef745018c.json', 'Ai', '2024-12-03 19:52:48', ''),
(4, 'Hai', 'asdasdas', 'wwwroot/flashcards/96bf086d-cbcc-457e-9e24-8157dfb62908.json', 'Manual', '2024-12-03 19:58:42', ''),
(5, 'sad', 'asdads', 'wwwroot/flashcards/933de7b5-0de6-46a2-8783-5bffce90eac4.json', 'Manual', '2024-12-03 20:17:01', ''),
(6, 'Addition', 'Flashcard for Highschool on Addition', 'wwwroot/flashcards/ef5cd3b4-f835-466f-800c-7110f6e3db48.json', 'Ai', '2024-12-04 10:44:32', '9554553f-d996-4a05-9976-546e241fd136'),
(7, 'Botany', 'Flashcard for Elementary on Botany', 'wwwroot/flashcards/648e7e82-9a70-49d6-87b3-a6e4967116b5.json', 'Ai', '2024-12-10 04:04:52', '581e473b-7763-4c6c-8a0c-74872f0ef0fa'),
(8, 'sad', 'sad', 'wwwroot/flashcards/9e731107-7189-4537-a55c-99cb661ba871.json', 'Manual', '2024-12-10 04:07:35', '581e473b-7763-4c6c-8a0c-74872f0ef0fa'),
(9, 'sad', 'sad', 'wwwroot/flashcards/150df852-dd63-4fa6-a455-369e8c211019.json', 'Manual', '2024-12-10 04:09:39', '581e473b-7763-4c6c-8a0c-74872f0ef0fa'),
(10, 'OOP(OBJECT ORIENTED PROGRAMMING)', 'Flashcard for Elementary on OOP(OBJECT ORIENTED PROGRAMMING)', 'wwwroot/flashcards/6fee8dcb-1634-4f8d-8f51-f96cbaa0187d.json', 'Ai', '2024-12-10 04:28:31', '581e473b-7763-4c6c-8a0c-74872f0ef0fa'),
(11, 'OOP(OBJECT ORIENTED PROGRAMMING)', 'Flashcard for College on OOP(OBJECT ORIENTED PROGRAMMING)', 'wwwroot/flashcards/c529f519-f3ce-4e09-b6f1-5691f67b020b.json', 'Ai', '2024-12-10 06:06:58', '581e473b-7763-4c6c-8a0c-74872f0ef0fa');

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
(5, '1:**What is the sum of 345 and 678?', '1023', NULL, NULL, 2),
(6, '2:**Add -25 and +75. What is the result?', '50', NULL, NULL, 2),
(7, '3:**Solve: 1234 + 5678 + 9012', '16024', NULL, NULL, 2),
(8, '4:**A farmer has 125 apples and picks another 275.  How many apples does he have in total?', '400', NULL, NULL, 2),
(10, 'sadsdas', 's', NULL, NULL, 4),
(11, 'dscsd', 'sss', NULL, NULL, 4),
(12, 'asds', 's', NULL, NULL, 5),
(13, '1:**What is the sum of 25 and 37?', '62', NULL, NULL, 6),
(14, '2:**Add -15 and 22. What is the result?', '7', NULL, NULL, 6),
(15, '3:**Solve: 456 + 789 + 123', '1368', NULL, NULL, 6),
(16, '4:**If you have 1,250 apples and you buy 750 more, how many apples do you have in total?', '2000', NULL, NULL, 6),
(17, '1:** What part of the plant helps it soak up water and nutrients from the soil?', 'Roots', NULL, NULL, 7),
(18, '2:** What is the main job of a leaf?', 'To make food for the plant through photosynthesis', NULL, NULL, 7),
(19, '3:** What part of the plant produces seeds?', 'Flowers', NULL, NULL, 7),
(20, '4:** What is the stem of a plant?', 'The stem supports the plant and carries water and nutrients', NULL, NULL, 7),
(21, 'sad', 'asdasd', NULL, NULL, 8),
(22, 'sad', 'asdasd', NULL, NULL, 9),
(23, '1:**What is an object in Object-Oriented Programming?', 'An object is like a real-world thing, with its own properties (like color or size) and actions (like moving or making a sound).', NULL, NULL, 10),
(24, '1:** What is Object-Oriented Programming (OOP)?', 'A programming paradigm based on the concept of \"objects\", which can contain data and code: data in the form of fields (often known as attributes or properties), and code, in the form of procedures (often known as methods).', NULL, NULL, 11),
(25, '2:**  Name four fundamental principles of OOP.', 'Encapsulation, Inheritance, Polymorphism, Abstraction', NULL, NULL, 11),
(26, '3:** Explain Encapsulation in OOP.', 'Bundling data (fields) and methods that operate on that data within a class, and controlling access to that data and methods.  This protects data integrity and promotes modularity.', NULL, NULL, 11),
(27, '4:** What is Inheritance in OOP, and what are its benefits?', 'A mechanism where one class (child class or subclass) acquires the properties and methods of another class (parent class or superclass). Benefits include code reusability, reduced redundancy, and establishing a clear hierarchical relationship between classes.', NULL, NULL, 11);

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
  `GameCode` varchar(6) DEFAULT NULL,
  `Created_by` varchar(200) NOT NULL,
  `CreatedAt` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `quizzes`
--

INSERT INTO `quizzes` (`QuizId`, `GradeLevel`, `Topic`, `CorrectAnswers`, `JsonFilePath`, `UserId`, `HighestScore`, `GameCode`, `Created_by`, `CreatedAt`) VALUES
(9, 'Elementary', 'Physics', '[1,1,2,2,2,3]', 'wwwroot\\quizzes\\9246cee3-f2b7-4c14-a57d-312c473a55ee.json', '885efb3e-8e0f-4593-b855-6489a1fd5279', 0, '234255', 'Ai', '2023-12-04 01:11:48'),
(12, 'Highschool', 'Addition', '[3,1,0,0,1,3]', 'wwwroot\\quizzes\\88f7db86-6737-46f4-aef5-a26e1250f67b.json', '885efb3e-8e0f-4593-b855-6489a1fd5279', 0, '123233', 'Manual', '2024-12-04 01:11:48'),
(13, 'Elementary', 'Physics', '[\"d\"]', 'wwwroot\\quizzes\\bb6012c7-2689-42e3-b61f-79e7e40cfdfc.json', '885efb3e-8e0f-4593-b855-6489a1fd5279', 0, NULL, 'Ai', '2024-12-04 01:11:48'),
(14, 'Highschool', 'Addition', '', '', '885efb3e-8e0f-4593-b855-6489a1fd5279', 0, '208233', 'Manual', '2024-12-04 01:11:48'),
(17, 'College', 'OOP(OBJECT ORIENTED PROGRAMMING)', '[3,1]', 'wwwroot\\quizzes\\f0b4396f-3f49-4191-b611-3fe1aceed448.json', '885efb3e-8e0f-4593-b855-6489a1fd5279', 0, '128699', 'Manual', '2024-12-04 01:11:48'),
(18, 'College', 'Psychology', '[\"c\",\"c\",\"b\",\"b\",\"b\",\"d\",\"c\",\"b\",\"c\",\"c\"]', 'wwwroot\\quizzes\\d959680e-0463-4900-ac45-988691b3a83b.json', '885efb3e-8e0f-4593-b855-6489a1fd5279', 0, '234234', 'Ai', '2024-12-04 01:11:48'),
(19, 'College', 'Psychology', '[\"c\",\"b\",\"b\",\"b\",\"b\",\"b\",\"c\",\"b\",\"c\",\"d\"]', 'wwwroot\\quizzes\\73377844-3d87-4de0-ad03-05b7b414333d.json', '885efb3e-8e0f-4593-b855-6489a1fd5279', 0, '234343', 'Ai', '2024-12-04 01:11:48'),
(23, 'College', 'Addition', '[\"b\",\"c\",\"c\",\"a\",\"b\",\"a\",\"a\",\"b\",\"a\",\"a\"]', 'wwwroot\\quizzes\\27999987-0814-4764-b23f-c4ab9505a436.json', '885efb3e-8e0f-4593-b855-6489a1fd5279', 0, '123123', 'Ai', '2024-12-04 01:11:48'),
(24, 'Elementary', 'add', '[\"b\",\"c\",\"c\",\"c\",\"c\"]', 'wwwroot\\quizzes\\5ff924d3-8e1b-431f-bcf6-cd4b3f7ea569.json', 'ef528ca6-3919-444d-9908-0db448c20bc1', 0, '213123', 'Ai', '2024-12-04 01:11:48'),
(25, 'Highschool', 'dsdfdsf', '[3]', 'wwwroot\\quizzes\\c98054bd-09e9-4861-abfa-98c1ac809410.json', 'ef528ca6-3919-444d-9908-0db448c20bc1', 0, '948806', 'Ai', '2024-12-04 01:11:48'),
(27, 'Elementary', 'Addition', '[\"b\",\"c\",\"c\",\"c\",\"c\"]', 'wwwroot\\quizzes\\fdebe49c-0268-4e42-b041-0f52a1b52421.json', 'ef528ca6-3919-444d-9908-0db448c20bc1', 0, '213123', 'Manual', '2024-12-04 01:11:48'),
(28, 'College', 'Addition', '[1,1,0,1,0]', 'wwwroot\\quizzes\\6f9a7b5c-27e6-4563-8787-caa8950ac059.json', 'ef528ca6-3919-444d-9908-0db448c20bc1', 0, '963180', 'Manual', '2024-12-04 01:11:48'),
(29, 'College', 'Psychology', '[1,1,1,1,1]', 'wwwroot\\quizzes\\7190738c-ff29-4892-a9b3-7bc7aba50e38.json', 'ef528ca6-3919-444d-9908-0db448c20bc1', 0, '249671', 'Manual', '2024-12-04 01:11:48'),
(30, 'College', 'Addition', '[1,1,0,2,1]', 'wwwroot\\quizzes\\549e2326-fc56-4b8d-b3ce-09462e83770e.json', 'ef528ca6-3919-444d-9908-0db448c20bc1', 0, '871866', 'Manual', '2024-12-04 01:11:48'),
(31, 'Elementary', 'Addition', '[1,2,2,2,2]', 'wwwroot\\quizzes\\86f890b5-90f5-45b8-a59f-9269744a0d63.json', '885efb3e-8e0f-4593-b855-6489a1fd5279', 0, '430814', 'Ai', '2024-12-04 15:43:52'),
(32, 'College', 'BOTANY', '[2,2,1,2,2]', 'wwwroot\\quizzes\\5871d2e9-400d-4dab-8c63-4e8396519b2c.json', '581e473b-7763-4c6c-8a0c-74872f0ef0fa', 0, '203331', 'Ai', '2024-12-04 18:50:57'),
(33, 'Elementary', 'Addition', '[1,2,2,1,2]', 'wwwroot\\quizzes\\c7011d2f-4227-4b68-96b3-280016e3fa1b.json', '581e473b-7763-4c6c-8a0c-74872f0ef0fa', 0, '373967', 'Ai', '2024-12-10 14:13:25');

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
-- Table structure for table `review`
--

CREATE TABLE `review` (
  `Id` int NOT NULL,
  `UserName` varchar(255) NOT NULL,
  `UserRole` varchar(255) NOT NULL,
  `Rating` int NOT NULL,
  `Feedback` text,
  `FilePath` varchar(255) DEFAULT NULL,
  `CreatedAt` datetime DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `review`
--

INSERT INTO `review` (`Id`, `UserName`, `UserRole`, `Rating`, `Feedback`, `FilePath`, `CreatedAt`) VALUES
(1, 'christinsdasd', 'Student', 5, 'dfsdfsdf', '/profile_images/default.png', '2024-12-01 02:11:13'),
(2, 'Christian Paul Narido', 'Student', 1, 'sdasdasdasd', '/profile_images/default.png', '2024-12-11 02:11:13'),
(3, 'Christian Paul Narido', 'Student', 4, 'sdasdsad', '/profile_images/cnaridso@gmail.com_b86f4331-ed1a-472c-a661-6c9a1357f239.jfif', '2024-12-11 02:11:13'),
(4, 'Christian Paul Narido', 'Student', 4, 'sdasdsad', '/profile_images/cnaridso@gmail.com_b86f4331-ed1a-472c-a661-6c9a1357f239.jfif', '2024-12-11 02:11:13'),
(5, 'Christian Paul NaridSDo', 'Professor', 4, 'DSASAD', '/profile_images/cnarido7ssss8@gmail.com_Green_Blue_Happy_Kids_Colorful_Achievement_Certificate_20241208 (2).png', '2024-12-11 02:11:13');

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
(37, 28, 'ef528ca6-3919-444d-9908-0db448c20bc1', 0, '2024-12-03 09:29:31'),
(38, 28, 'ef528ca6-3919-444d-9908-0db448c20bc1', 0, '2024-12-03 10:50:01'),
(39, 29, 'ef528ca6-3919-444d-9908-0db448c20bc1', 3, '2024-12-03 15:24:37'),
(40, 29, 'ef528ca6-3919-444d-9908-0db448c20bc1', 1, '2024-12-03 15:25:20'),
(41, 29, 'ef528ca6-3919-444d-9908-0db448c20bc1', 1, '2024-12-03 15:25:28'),
(42, 31, '885efb3e-8e0f-4593-b855-6489a1fd5279', 1, '2024-12-04 07:44:47'),
(43, 31, '885efb3e-8e0f-4593-b855-6489a1fd5279', 1, '2024-12-04 07:44:51'),
(44, 12, '9554553f-d996-4a05-9976-546e241fd136', 0, '2024-12-04 10:47:55'),
(45, 12, '9554553f-d996-4a05-9976-546e241fd136', 0, '2024-12-04 10:49:09'),
(46, 12, '581e473b-7763-4c6c-8a0c-74872f0ef0fa', 2, '2024-12-10 04:39:08'),
(47, 12, '581e473b-7763-4c6c-8a0c-74872f0ef0fa', 2, '2024-12-10 04:39:16'),
(48, 32, '581e473b-7763-4c6c-8a0c-74872f0ef0fa', 1, '2024-12-10 04:54:10'),
(49, 32, '581e473b-7763-4c6c-8a0c-74872f0ef0fa', 0, '2024-12-10 05:14:27'),
(50, 32, '581e473b-7763-4c6c-8a0c-74872f0ef0fa', 0, '2024-12-10 05:17:37'),
(51, 33, '581e473b-7763-4c6c-8a0c-74872f0ef0fa', 1, '2024-12-10 16:17:49'),
(52, 33, '581e473b-7763-4c6c-8a0c-74872f0ef0fa', 1, '2024-12-10 16:17:49'),
(53, 33, '581e473b-7763-4c6c-8a0c-74872f0ef0fa', 1, '2024-12-10 16:17:49'),
(54, 33, '581e473b-7763-4c6c-8a0c-74872f0ef0fa', 1, '2024-12-10 16:17:58');

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
-- Indexes for table `review`
--
ALTER TABLE `review`
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
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT for table `questions`
--
ALTER TABLE `questions`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=28;

--
-- AUTO_INCREMENT for table `quizzes`
--
ALTER TABLE `quizzes`
  MODIFY `QuizId` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=35;

--
-- AUTO_INCREMENT for table `review`
--
ALTER TABLE `review`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `scorerecords`
--
ALTER TABLE `scorerecords`
  MODIFY `ScoreRecordId` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=55;

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
