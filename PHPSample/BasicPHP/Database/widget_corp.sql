DROP TABLE IF EXISTS `pages`;
CREATE TABLE `pages` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `subject_id` int(11) NOT NULL,
  `menu_name` varchar(30) NOT NULL,
  `position` int(3) NOT NULL,
  `visible` tinyint(1) NOT NULL,
  `content` text,
  PRIMARY KEY (`id`),
  --INDEX (subject_id)
  KEY `subject_id` (`subject_id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;

INSERT INTO `pages` VALUES
(1,1,'Our Mission',1,1,'Our mission has always been...'),
(2,1,'Our History',2,1,'Founded in 1898 by two enterprising engineers...'),
(3,2,'Large Widgets',1,1,'Our large widgets have to be seen to be believed...'),
(4,2,'Small Widgets',2,1,'They say big things come in small packages...'),
(5,3,'Retrofitting',1,1,'We love to replace widgets...'),
(6,3,'Certification',2,1,'We can certify any widget, not just our own...');

DROP TABLE IF EXISTS `subjects`;
CREATE TABLE `subjects` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `menu_name` varchar(30) NOT NULL,
  `position` int(3) NOT NULL,
  `visible` tinyint(1) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

INSERT INTO `subjects` VALUES (1,'About Widget Corp',1,1),(2,'Products',2,1),(3,'Services',3,1);

--PHP Database Interaction in Five Steps
--1. Create a database connection
--2. Perform database query
--3. Use returned data
--4. Release returned data
--5. Close database connection
