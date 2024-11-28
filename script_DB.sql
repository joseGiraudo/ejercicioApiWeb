CREATE DATABASE apiExtradosDB;
USE apiExtradosDB;


CREATE TABLE usuarios (
    id INT AUTO_INCREMENT PRIMARY KEY,
    email VARCHAR(50) NOT NULL,
    hashed_password VARCHAR(255) NOT NULL,
    name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) NOT NULL,
    age INT NOT NULL,
    active BOOLEAN DEFAULT TRUE
);

SELECT * FROM usuarios