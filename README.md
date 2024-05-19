# Easy_Task
Performing major CRUD operations

# Overview
This project demonstrates basic CRUD (Create, Read, Update, Delete) operations. Upon running the application, it seeds an admin user with predefined email and password credentials. These credentials can be used to log in, enabling authentication and providing a JWT token. This token is required to access and perform CRUD operations within the application.

# Seeded Admin Details
email: admin@gmail.com
password: Password@123

# Technologies Used
Programming Language: C#
Framework: Dotnet 8
Database: PostgreSQL
Logging: Serilog
WebSocket: SignalR(wss:///localhost:7203/streaming-hub)
Validation: Fluent Validation

# Installation
git clone https://github.com/Mazium/Easy_Task.git
cd Easy_Task

# Configuration
When you clone you can just update your connection string in your AppSettings file. Then run the application using DOTNET RUN in your CLI or just start in your visual studio

# Web Socket
wss:///localhost:7203/streaming-hub
