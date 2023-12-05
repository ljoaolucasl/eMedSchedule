# eMedSchedule

## Description

eMedSchedule is a web application developed at the end of the Academy of Programmers 2023 course. Its purpose is to organize and maintain a clinic's schedule of medical activities. The application allows you to schedule medical appointments and surgeries, managing doctors and their working hours efficiently.

eMedSchedule is an API, if you want to view the web part, follow the link: [eMedScheduleWeb](https://github.com/ljoaolucasl/eMedScheduleWeb)

## Specifications

### Medical Module

#### Properties:

- **Id (Guid)**
- **Name (string)**
- **CRM (string)**
- **Activities (List<Activity>)**
- **WorkedHours (TimeSpan)**

#### Methods:

- **Requirement 1.1: Register Doctor**
  - *Criteria:*
    - The doctor's registration should include a name and CRM.
    - The CRM should consist of five digits followed by the state abbreviation (e.g., 78806-SP).
    - The name must be at least 3 characters.
    - The name should not contain special characters.
    - It should not be possible to register a doctor with an existing CRM.

- **Requirement 1.2: Edit Doctor**
  - *Criteria:*
    - The validation criteria are the same as Requirement 1.1.

- **Requirement 1.3: Delete Doctor**
  - *Criteria:*
    - The doctor must not have pending activities scheduled to be deleted.

- **Requirement 1.4: View All Doctors**
  - *Criteria:*
    - Should display Id and Name of all registered doctors.

### Activities Module

#### Properties:

- **Id (Guid)**
- **Title (string)**
- **Activity Type (enum: Consultation, Surgery)**
- **Responsible Doctor(s) (List<Doctor>)**
- **Start Time (TimeSpan)**
- **End Time (TimeSpan)**
- **Date (DateTime)**
- **Recovery Time (TimeSpan)**

#### Methods:

- **Requirement 2.1: Schedule Activity**
  - *Criteria:*
    - It should be possible to indicate start and end times, activity date, activity type, and responsible doctor(s).
    - Recovery time should be automatically calculated based on the type of activity.
    - It should be possible to schedule activities for any time (future or past).
    - For a Consultation, only one doctor should be allowed.
    - For a Surgery, one or more doctors can be involved.

- **Requirement 2.2: Modify Existing Activity Times**
  - *Criteria:*
    - The user should be able to modify the times of an existing activity.

- **Requirement 2.3: Delete Existing Activity**
  - *Criteria:*
    - The user should be able to delete an existing activity.

- **Requirement 2.4: Schedule Conflicts**
  - *Criteria:*
    - The application should indicate conflicts when an activity overlaps with other activities of the same doctor.
    - Users should be able to adjust schedules to resolve conflicts.

- **Requirement 2.5: List Doctors Ranking by Worked Hours**
  - *Criteria:*
    - The application should display a list of doctors who worked the most hours within a period.

## Architecture

The project follows a layered architecture:

- **Distribution Layer:** Interfaces with external components, such as APIs or user interfaces.
- **Application Layer:** Contains application logic and use cases.
- **Domain Layer:** Represents the core business logic and entities.
- **Infrastructure Layer:** Handles external concerns like databases and services.
- **Test Layer:** Contains unit tests and integration tests.

## Technologies

The project incorporates various technologies:

- **Languages:** C#, SQL
- **Frameworks:** .NET Core, ASP.NET Web APIs
- **Database:** Microsoft SQL Server
- **ORM:** Entity Framework Core
- **Testing:** MSTest, Serilog, NBuilder, FluentAssertions
- **Source Control:** Git

## How to Use

To set up the database and run the application, follow these steps:

1. Clone the repository.
2. Open a terminal and navigate to the project directory.
3. Run the following commands:

    ```bash
    dotnet ef migrations add InitialMigration
    dotnet ef database update
    ```

4. Build and run the application:

    ```bash
    dotnet build
    dotnet run
    ```

5. Access the application through the specified endpoint.

Feel free to explore the various functionalities of the application, including doctor registration, activity scheduling, and viewing reports.

# eMedSchedule API Documentation

## Table of Contents

1. [Introduction](#introduction)
2. [Authentication](#authentication)
  - [1. Register](#1-register)
  - [2. Login](#2-login)
  - [3. Logout](#3-logout)
3. [Doctor Endpoints](#doctor-endpoints)
  - [1. Register Doctor](#1-register-doctor)
  - [2. Update Doctor](#2-update-doctor)
  - [3. Delete Doctor](#3-delete-doctor)
  - [4. Get All Doctors](#4-get-all-doctors)
  - [5. Get Doctor By Id](#5-get-doctor-by-id)
  - [6. Get Complete Doctor By Id](#6-get-complete-doctor-by-id)
  - [7. List Doctors Ranking](#7-list-doctors-ranking)
4. [Activity Endpoints](#activity-endpoints)
  - [1. Schedule Activity](#1-schedule-activity)
  - [2. Update Activity Times](#2-update-activity-times)
  - [3. Delete Activity](#3-delete-activity)
  - [4. Get All Activities](#4-get-all-activities)
  - [5. Get Activity By Id](#5-get-activity-by-id)
  - [6. Get Complete Activity By Id](#6-get-complete-activity-by-id)
5. [Error Handling](#error-handling)
6. [Conclusion](#conclusion)

## Introduction

The eMedSchedule API provides endpoints to manage doctors and their scheduled activities in a medical clinic. This documentation outlines the available endpoints, their input parameters, and expected responses.

## Authentication

Certain actions may require authorization. Token-based authentication is recommended for secure operations.

### 1. Register

**Endpoint:** `POST /api/account/register`

**Request:**

```json
{
  "name": "String",
  "email": "string@gmail.com",
  "password": "String@123"
}
```

**Response (Success):** `200 Success`

```json
{
  "success": true,
  "data": {
    "token": "Bearer token",
    "expirationDate": "2023-12-01T21:28:27.3192224-03:00",
    "user": {
      "id": "guid1",
      "email": "string@gmail.com",
      "name": "String"
    }
  }
}
```

### 2. Login

**Endpoint:** `POST /api/account/login`

**Request:**

```json
{
  "email": "string@gmail.com",
  "password": "String@123"
}
```

**Response (Success):** `200 Success`

```json
{
  "success": true,
  "data": {
    "token": "Bearer token",
    "expirationDate": "2023-01-10",
    "user": {
      "id": "guid1",
      "email": "string@gmail.com",
      "name": "String"
    }
  }
}
```

### 3. Logout

**Endpoint:** `POST /api/account/logout`

**Response (Success):** `200 Success`

## Doctor Endpoints

### 1. Register Doctor

**Endpoint:** `POST /api/doctor`

**Request:**

```json
{
  "name": "Smith",
  "crm": "12345-SP",
  "profilePictureBase64": "string"
}
```

**Response (Success):** `200 Success`

```json
{
  "name": "Smith",
  "crm": "12345-SP",
  "profilePictureBase64": "string"
}
```

**Response (Error):** `400 Bad Request`

```json
{
  "success": false,
  "errors": [
    "Name must have at least 3 characters.",
    "CRM is invalid."
  ]
}
```

### 2. Update Doctor

**Endpoint:** `PUT /api/doctor/{id}`

**Request:**

```json
{
  "name": "Johnson",
  "crm": "12345-SP",
  "profilePictureBase64": "string"
}
```

**Response (Success):** `200 Success`

```json
{
  "name": "Johnson",
  "crm": "12345-SP",
  "profilePictureBase64": "string"
}
```

**Response (Error):** `404 Not Found`

```json
{
  "success": false,
  "errors": [
    "Doctor not found."
  ]
}
```

### 3. Delete Doctor

**Endpoint:** `DELETE /api/doctor/{id}`

**Response (Success):** `200 Success`

```json
{
  "name": "string",
  "crm": "string",
  "profilePictureBase64": "string"
}
```

**Response (Error):** `400 Bad Request`

```json
{
  "success": false,
  "errors": [
    "Doctor has scheduled activities and cannot be deleted."
  ]
}
```

### 4. Get All Doctors

**Endpoint:** `GET /api/doctor`

**Response (Success):** `200 Success`

```json
[
  {
    "id": "guid1",
    "name": "Smith",
    "profilePictureBase64": "string"
  },
  {
    "id": "guid2",
    "name": "Johnson",
    "profilePictureBase64": "string"
  }
]
```

### 5. Get Doctor By Id

**Endpoint:** `GET /api/doctor/{id}`

**Response (Success):** `200 Success`

```json
{
  "name": "Smith",
  "crm": "12345-SC",
  "profilePictureBase64": "string"
}
```

### 6. Get Complete Doctor By Id

**Endpoint:** `GET /api/doctor/complete-view/{id}`

**Response (Success):** `200 Success`

```json
{
  "id": "guid1",
  "name": "Smith",
  "crm": "12345-SC",
  "profilePictureBase64": "string",
  "activities": [
    {
      "id": "guid1",
      "title": "Consultation",
      "activityType": 0,
      "startTime": "10:00:00",
      "endTime": "11:30:00",
      "date": "2023-01-10"
    }
  ]
}
```

### 7. List Doctors Ranking

**Endpoint:** `GET /api/doctor/worked-hours/{startDate}={endDate}`

**Response (Success):** `200 Success`

```json
[
  {
    "id": "guid1",
    "name": "Smith",
    "crm": "12345-SP",
    "workedHours": "25:30:00"
  },
  {
    "id": "guid2",
    "name": "Johnson",
    "crm": "67890-SP",
    "workedHours": "20:15:00"
  }
]
```

## Activity Endpoints

### 1. Schedule Activity

**Endpoint:** `POST /api/doctoractivity`

**Request:**

```json
{
  "title": "Consultation",
  "activityType": 0,
  "selectedDoctors": ["guid1"],
  "startTime": "10:00:00",
  "endTime": "11:30:00",
  "date": "2023-01-10"
}
```

**Response (Success):** `200 Success`

```json
{
  "title": "Consultation",
  "activityType": 0,
  "selectedDoctors": ["guid1"],
  "startTime": "10:00:00",
  "endTime": "11:30:00",
  "date": "2023-01-10"
  "recoveryTime": "00:20:00"
}
```

**Response (Error):** `400 Bad Request`

```json
{
  "success": false,
  "errors": [
    "Activity must have at least one doctor."
  ]
}
```

### 2. Update Activity Times

**Endpoint:** `PUT /api/doctoractivity/{id}`

**Request:**

```json
{
  "title": "Surgery",
  "activityType": 1,
  "selectedDoctors": ["guid1"],
  "startTime": "10:00:00",
  "endTime": "11:30:00",
  "date": "2023-01-10"
}
```

**Response (Success):** `200 Success`

```json
{
  "title": "Surgery",
  "activityType": 1,
  "selectedDoctors": ["guid1"],
  "startTime": "10:00:00",
  "endTime": "11:30:00",
  "date": "2023-01-10"
  "recoveryTime": "04:00:00"
}
```

**Response (Error):** `404 Not Found`

```json
{
  "success": false,
  "errors": [
    "Activity not found."
  ]
}
```

### 3. Delete Activity

**Endpoint:** `DELETE /api/doctoractivity/{id}`

**Response (Success):** `200 Success`

```json
{
  "title": "Surgery",
  "activityType": 1,
  "selectedDoctors": ["guid1"],
  "startTime": "10:00:00",
  "endTime": "11:30:00",
  "date": "2023-01-10"
  "recoveryTime": "04:00:00"
}
```

**Response (Error):** `400 Bad Request`

```json
{
  "success": false,
  "errors": [
    "string"
  ]
}
```

### 4. Get All Activities

**Endpoint:** `GET /api/doctoractivity`

**Response (Success):** `200 Success`

```json
[
  {
    "id": "guid1",
    "title": "Surgery",
    "activityType": 1,
    "startTime": "10:00:00",
    "endTime": "11:30:00",
    "date": "2023-01-10"
  },
  {
    "id": "guid2",
    "title": "Consultation",
    "activityType": 0,
    "startTime": "10:00:00",
    "endTime": "11:30:00",
    "date": "2023-01-10"
  }
]
```

### 5. Get Activity By Id

**Endpoint:** `GET /api/doctoractivity/{id}`

**Response (Success):** `200 Success`

```json
{
  "title": "Surgery",
  "activityType": 1,
  "selectedDoctors": ["guid1"],
  "startTime": "10:00:00",
  "endTime": "11:30:00",
  "date": "2023-01-10"
  "recoveryTime": "04:00:00"
}
```

### 6. Get Complete Activity By Id

**Endpoint:** `GET /api/doctoractivity/complete-view/{id}`

**Response (Success):** `200 Success`

```json
{
  "id": "guid1",
  "title": "Surgery",
  "activityType": 1,
  "doctors": [
    {
    "id": "guid1",
    "name": "Smith",
    "profilePictureBase64": "string",
    }
  ],
  "startTime": "10:00:00",
  "endTime": "11:30:00",
  "date": "2023-01-10"
  "recoveryTime": "04:00:00"
}
```

## Error Handling

The API returns standard HTTP status codes for various operations. In case of an error, the response body contains details about the issue.

Common HTTP status codes include:

- `400 Bad Request`: Invalid input or validation failure.
- `404 Not Found`: Resource not found.
- `500 Internal Server Error`: Server-side error.

## Conclusion

The eMedSchedule API provides comprehensive functionality for managing doctors and their scheduled activities. It allows for easy integration into various client applications, enabling efficient medical scheduling in a clinic setting.


## Developed by
- [@João Lucas Claudino](https://www.linkedin.com/in/joao-lucas-claudino/)
