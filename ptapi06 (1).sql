-- phpMyAdmin SQL Dump
-- version 4.8.4
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1:3306
-- Gegenereerd op: 14 mei 2019 om 07:57
-- Serverversie: 5.7.24
-- PHP-versie: 7.3.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `ptapi06`
--

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `guests`
--

DROP TABLE IF EXISTS `guests`;
CREATE TABLE IF NOT EXISTS `guests` (
  `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `username` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `password` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `email` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `ExpiresAt` date NOT NULL,
  `IsFrozen` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `migrations`
--

DROP TABLE IF EXISTS `migrations`;
CREATE TABLE IF NOT EXISTS `migrations` (
  `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `migration` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `batch` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=46 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Gegevens worden geëxporteerd voor tabel `migrations`
--

INSERT INTO `migrations` (`id`, `migration`, `batch`) VALUES
(41, '2014_10_12_000000_create_users_table', 1),
(42, '2014_10_12_100000_create_password_resets_table', 1),
(43, '2018_03_27_115350_create_guests_table', 1),
(44, '2018_03_29_184414_create_roles_table', 1),
(45, '2018_06_15_062626_create_permissons_table', 1);

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `password_resets`
--

DROP TABLE IF EXISTS `password_resets`;
CREATE TABLE IF NOT EXISTS `password_resets` (
  `email` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `token` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `permissons`
--

DROP TABLE IF EXISTS `permissons`;
CREATE TABLE IF NOT EXISTS `permissons` (
  `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `role_Id` int(11) NOT NULL,
  `IsAdmin` int(11) NOT NULL,
  `ScanPermisson` int(11) NOT NULL,
  `MakeCodePermisson` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Gegevens worden geëxporteerd voor tabel `permissons`
--

INSERT INTO `permissons` (`id`, `role_Id`, `IsAdmin`, `ScanPermisson`, `MakeCodePermisson`) VALUES
(1, 1, 1, 1, 1),
(2, 2, 0, 1, 1),
(3, 3, 0, 1, 0),
(4, 4, 0, 0, 0);

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `roles`
--

DROP TABLE IF EXISTS `roles`;
CREATE TABLE IF NOT EXISTS `roles` (
  `roleId` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `roleName` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  PRIMARY KEY (`roleId`)
) ENGINE=MyISAM AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Gegevens worden geëxporteerd voor tabel `roles`
--

INSERT INTO `roles` (`roleId`, `roleName`) VALUES
(1, 'Administrator'),
(2, 'Festival Owner'),
(3, 'Festival Worker'),
(4, 'Guest');

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `users`
--

DROP TABLE IF EXISTS `users`;
CREATE TABLE IF NOT EXISTS `users` (
  `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `username` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `email` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `password` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `roleId` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `IsFrozen` int(11) NOT NULL,
  `latitude` double NOT NULL DEFAULT '0',
  `longitude` double NOT NULL DEFAULT '0',
  `remember_token` varchar(100) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Gegevens worden geëxporteerd voor tabel `users`
--

INSERT INTO `users` (`id`, `username`, `email`, `password`, `roleId`, `IsFrozen`, `latitude`, `longitude`, `remember_token`, `created_at`, `updated_at`) VALUES
(1, 'test0', 'hgqg9CQaaR@gmail.com', '$2y$10$cTaRl7Esh19xDjKhA9.gV.PhZHK0fNGxn6mn2s79u5eOuFFuYEnIC', '1', 0, 0, 0, NULL, NULL, NULL),
(2, 'test1', '3MNqzhqIZo@gmail.com', '$2y$10$xqnKeKR0eOdKP1QVmvZekOT2SmJBxHoMbzrkCgrdjoN8qZ/iKiWcS', '1', 0, 0, 0, NULL, NULL, NULL),
(3, 'test2', '8HUcc9mD7v@gmail.com', '$2y$10$Hx37ebq0djn4caGZaEmD5OwXu/GuV5/iZPTbgG5TKY9KLmZtI2Ma6', '2', 0, 0, 0, NULL, NULL, NULL),
(4, 'test3', 'aaljd02pTg@gmail.com', '$2y$10$MBXXBR/2OGPsPignRhv6deghPG4c30ljxZuFWr9cBEgy12zlMY8fO', '2', 0, 0, 0, NULL, NULL, NULL),
(5, 'zhMJv06KY7', 'vtq85Rczpe@gmail.com', '$2y$10$uQpXRpnlSlWy6BNKIJoLy.ykw909Xm760lIDE69XbfyUQ3IlGsWHG', '4', 0, 0, 0, NULL, NULL, NULL),
(6, 'test4', 'BpmHa0T4Dq@gmail.com', '$2y$10$PPLleYjtW3cuVZFnKfkhdesbJ1fkpUr.pKhGxf0HkLIE9P9FvNZOK', '3', 0, 0, 0, NULL, NULL, NULL),
(7, 'festivalAdmin', 'justin@gmail.com', '$2y$10$AvsYBpuPsh74h/D0ray9fOKxs5rEs0EYhwheiUKGm6QbsZbb1jBBq', '1', 0, 0, 0, NULL, NULL, NULL);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
