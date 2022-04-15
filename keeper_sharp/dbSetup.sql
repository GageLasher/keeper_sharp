CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS vaults(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  name TEXT,
  description TEXT,
  isPrivate TINYINT(1),
  creatorId VARCHAR(255),
  FOREIGN KEY (creatorId) REFERENCES accounts(id)
) default charset utf8 COMMENT '';
ALTER TABLE
  vaults
ADD
  COLUMN img TEXT
AFTER
  name;
ALTER TABLE
  vaults ALTER isPrivate
SET
  DEFAULT false;
CREATE Table IF NOT Exists vaultKeeps(
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    keepId INT,
    vaultId INT,
    creatorId VARCHAR(255),
    FOREIGN KEY (creatorId) REFERENCES accounts(id),
    FOREIGN KEY (keepId) REFERENCES keeps(id),
    FOREIGN KEY (vaultId) REFERENCES vaults(id)
  ) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS keeps(
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    name TEXT,
    description TEXT,
    img TEXT,
    creatorId VARCHAR(255),
    views INT,
    kept INT,
    FOREIGN KEY (creatorId) REFERENCES accounts(id)
  ) default charset utf8 COMMENT '';
SELECT
  vk.*,
  a.*,
  k.*
FROM
  vaultKeeps vk
  JOIN accounts a ON vk.creatorId = a.id
  JOIN keeps k ON vk.keepId = k.id
WHERE
  vk.vaultId = 16