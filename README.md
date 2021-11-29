# Airline Reservation System

In this project, I created a miniature version of a flight reservation software using WPF and WCF. The software allows the customers to book flights, handle concurrent booking of the same ticket, view seat availability for a flight. The admins can create a new flight schedule, modify existing seat availability and more.  The WPF app interacts with the WCF services to request/send data. The WCF service processes the data and interacts with SQL Server database to fetch data. This is a multi-threaded application, allowing and handling concurrent reservations of a particular seat by two different customers. 

# Features of the app

## 1. Login - for customers and admins

Upon launch, users are greeted with the following login page. Data for authentication is fetched from SQL Server. Upon successful login, sessions is created. Backend handles cases where user looses connection to the service (simulating wifi loss etc).

![Login Screen](https://user-images.githubusercontent.com/40236708/143803206-aa6970df-d859-47da-b0d5-4222814a33ea.PNG)


## 2. Main page view - for customers

Upon successful login, the customers are directed to the main page view below. The customers can set the source/destination locations, departure date and the travel class. After clicking on search, the data is processed in WCF & SQL Server and the search results obtained by the request are displayed in the row format user control as shown. For the figure below, I had 3 entries in DB for that search and the obtained results are shown below. For every search result, there is a "Book" button that allows the user to book that flight.
To execute a forced refresh on UI, there is a "Refresh" button. The user can also view his booking history using the "Booking History" button

![Main Page View (With Search Results)](https://user-images.githubusercontent.com/40236708/143803815-f6e90e73-d002-4c7e-8a12-99661e64493c.PNG)

In case there are no flight bookings, appropriate message will be displayed after the search button is clicked as shown in the figure below

![No Flight Available View](https://user-images.githubusercontent.com/40236708/143804544-f20c2410-5282-4d1b-910f-0956f14890ca.PNG)


## 3. Booking History view - for customers

When the customer clicks on Booking history button in the main page view, he is directed to the page shown below. Here, the customer can view the details of all the flights he successfully booked in the past

![Booking History View 2](https://user-images.githubusercontent.com/40236708/143804678-ba7e7d85-336a-45d5-abde-bd394ec95124.PNG)

