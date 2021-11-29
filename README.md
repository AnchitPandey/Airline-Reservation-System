# Airline Reservation System

In this project, I created a miniature version of a flight reservation software using WPF and WCF. The software allows the users to book flights, handle concurrent booking of the same ticket, view seat availability for a flight. The admins can create a new flight schedule, modify existing seat availability and more.  The WPF app interacts with the WCF services to request/send data. The WCF service processes the data and interacts with SQL Server database to fetch data. This is a multi-threaded application, allowing and handling concurrent reservations of a particular seat by two different customers. 

# Features of the app

## Login - for customers and admins

Upon launch, users are greeted with the following login page. Data for authentication is fetched from SQL Server. Upon successful login, sessions is created. Backend handles cases where user looses connection to the service (simulating wifi loss etc).

![Login Screen](https://user-images.githubusercontent.com/40236708/143803206-aa6970df-d859-47da-b0d5-4222814a33ea.PNG)
