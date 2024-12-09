CREATE DATABASE apiExtradosDB;
USE apiExtradosDB;

CREATE TABLE users (
    id INT AUTO_INCREMENT PRIMARY KEY,
    email VARCHAR(50) NOT NULL,
    hashedPassword VARCHAR(255) NOT NULL,
    name VARCHAR(50) NOT NULL,
    lastName VARCHAR(50) NOT NULL,
    age INT NOT NULL,
    active BOOLEAN DEFAULT TRUE
);

SELECT * FROM users;

CREATE TABLE books (
	id INT AUTO_INCREMENT PRIMARY KEY,
	title VARCHAR(100) NOT NULL,
	author VARCHAR(50) NOT NULL,
	ACTIVE BOOLEAN DEFAULT TRUE
);


CREATE TABLE book_loans (
    id INT AUTO_INCREMENT PRIMARY KEY,
    bookId INT NOT NULL, 
    userId INT NOT NULL, 
    loanDate DATE NOT NULL, 
    dueDate DATE NOT NULL, 
    returnDate DATE, 
    status VARCHAR(50),
    FOREIGN KEY (bookId) REFERENCES books(id),
    FOREIGN KEY (userId) REFERENCES users(id)
);

SELECT * FROM books