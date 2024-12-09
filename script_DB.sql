CREATE DATABASE apiExtradosDB;
USE apiExtradosDB;

CREATE TABLE users (
    id INT AUTO_INCREMENT PRIMARY KEY,
    email VARCHAR(50) NOT NULL,
    hashed_password VARCHAR(255) NOT NULL,
    name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) NOT NULL,
    age INT NOT NULL,
    active BOOLEAN DEFAULT TRUE
);

SELECT * FROM users;

CREATE TABLE books (
	id INT AUTO_INCREMENT PRIMARY KEY,
	title VARCHAR(100) NOT NULL,
	author VARCHAR(50) NOT NULL
);

CREATE TABLE book_loans (
    id INT AUTO_INCREMENT PRIMARY KEY,
    book_id INT NOT NULL, 
    user_id INT NOT NULL, 
    loan_date DATE NOT NULL, 
    due_date DATE NOT NULL, 
    return_date DATE, 
    status VARCHAR(50),
    FOREIGN KEY (book_id) REFERENCES books(id),
    FOREIGN KEY (user_id) REFERENCES users(id)
);