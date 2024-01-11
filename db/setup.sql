CREATE TABLE `BeatifyUser`.`Users` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(200) NULL,
  `email` VARCHAR(200) NULL,
  `password` VARCHAR(200) NULL,
  PRIMARY KEY (`id`));
  
INSERT INTO Users (name, email, password)
VALUES ('Jane Doe', 'jane.doe@example.com', '789012');

