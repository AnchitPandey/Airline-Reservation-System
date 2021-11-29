# Airline Reservation System

In this project, I created a miniature version of a flight reservation software using WPF, WCF, SQL Server and Entity Framework. The software allows the customers to book flights, handle concurrent booking of the same ticket, view seat availability for a flight. The admins can create a new flight schedule, modify existing seat availability and more.  The WPF app interacts with the WCF services to request/send data. The WCF service processes the data and interacts with SQL Server database to fetch data. I used Entity Framework to interface the WCF service with SQL Server database. This is a multi-threaded application, allowing and handling concurrent reservations of a particular seat by two different customers. 

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


## 4. Reservation Confirmation - Step 1 - for customers

When the customer clicks on the "Book" button for any flight schedule in the main page view, he is greeted with the details confirmation page as shown below. Upon filling the details and clicking on the "Proceed" button, the customer will be directed to the seat availability view. There are sanity checks for all the fields in the UI to ensure correct data format is received at the WCF service

![Reservation Step 1](https://user-images.githubusercontent.com/40236708/143804956-29f0a0db-65ca-4536-92ac-09dd30957f1d.PNG)


## 5. Seat Availability View - for customers

Upon filling the details of confirmation page as shown in the figure above, the customer will be directed to the seat selection page as shown below. The seats show in red are indicate that they have been occupied already. The seats in green are the available seats. The current seat selection of the customer is denoted by the colour yellow. The customer is limited to select at most and atleast 1 seat. Any further selection will result in a prompt. The refresh button allows the customer to force a refresh in multithreaded/ concurrent scenarios.

![Seat Reservation User View](https://user-images.githubusercontent.com/40236708/143805691-4b86b450-cb7a-448c-a742-464ba9eb9b7f.PNG)

The below figure shows a prompt that appears when the customer tries to select an already reserved seat

![Already Reserved User View](https://user-images.githubusercontent.com/40236708/143805744-189de3d2-5f84-4904-872d-b8138dc97cb5.PNG)


## 4. Reservation Confirmation - Step 2 - for customers

This is the final confirmation page, upon which the WCF service determines seat availability, makes changes in SQL Server database if the user clicked on "Confirm" button

![Reservation Confirmation Page](https://user-images.githubusercontent.com/40236708/143806424-d18628bc-04ec-4650-806a-242b6e5020c7.PNG)

If the seat booking is successful, the customer is greeted with the following message as shown and this new flight schedule gets added to the users booking history view. 

![Booking Successful Airline Confirmation](https://user-images.githubusercontent.com/40236708/143806909-9300c7dc-df81-4f83-95b5-ea8aa2bbf021.PNG)

If the seat was already booked by the time the customer tried to book it, then the customer is shown the following notification and he is taken back to seat selection page in case there are available seats or directly to the main page in case there are no available seats for that flight.

![Concurrent Booking](https://user-images.githubusercontent.com/40236708/143806719-49be1ddc-34bb-4230-b279-2edfaebbe77e.PNG)

## 5. Main page view - for admins

The admins can set up flight between a source and destination location. They have the flexibility to assign the number of available business class or economy class seats in the flight. They can also set the layout of available business and economy class seats as shown below. 

![Admin View](https://user-images.githubusercontent.com/40236708/143807166-1ea4ce7e-16cf-41fd-b62d-82b9c9f063d8.PNG)

## 6. Setting seat layout for flights - for admins

The number of seats that the admin can assign depends on the number he entered in the figure shown above. After clicking on "Open Flight layout" button, the admin will be directed to the following page. Here I chose 2 seats for admin, hence I selected 2 seats in the flight to be available.

![Seat View Admin](https://user-images.githubusercontent.com/40236708/143807518-0d0f731e-abff-4a92-95df-e7a202471878.PNG)

If admin selects few seats for economy class, he cannot reselect those seats in business class and vice versa. This validation is performed and the prompt is displayed if admin tries to select a seat in "red" which implies the seats he already booked previously for a different class (economy or business)

![Seat View Admin Validation](https://user-images.githubusercontent.com/40236708/143807779-62b089e7-e7a4-4510-9cdd-34b275c01022.PNG)


## 7. Data Model 

I used SQL Server as the database. The figure below shows the complete data model for this application

![Data Model](https://user-images.githubusercontent.com/40236708/143808051-3005f469-75ad-4552-a2f7-b3ac7add646f.PNG)

