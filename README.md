# SDU Hostel Management System

## 📌 Project Overview

The **SDU Hostel Management System** is a desktop-based hostel allocation and management application developed for **Southern Delta University, Ozoro**.

The system was designed to improve the traditional manual hostel allocation process by providing a computerized platform for managing student registration, hostel applications, room allocation, payment tracking, hostel records, and administrative activities.

The system provides separate interfaces for **students** and **administrators**, helping to improve the accuracy, transparency, efficiency, and reliability of hostel management.


## 🎯 Project Aim

To design and implement a computerized hostel allocation and management system that improves the efficiency, accuracy, transparency, and reliability of hostel allocation and management at Southern Delta University, Ozoro.


## 🎯 Project Objectives

The project was developed to:

* Design a computerized hostel allocation and management system.
* Develop a centralized database for student records, hostel information, room allocation, and occupancy.
* Evaluate the performance and effectiveness of the system in improving transparency, accuracy, and efficiency.


## ✨ Key Features

### 👨‍🎓 Student Module

Students can:

* Register and provide their personal information.
* Submit hostel applications.
* Select hostel preferences.
* View hostel application status.
* View room allocation information.
* Check payment status.
* Receive notifications.
* Track their allocation status.

### 👨‍💼 Administrator Module

Administrators can:

* Manage student records.
* Manage hostels and rooms.
* Review hostel applications.
* Approve or reject applications.
* Allocate available rooms to students.
* Monitor room occupancy.
* Prevent double allocation of rooms.
* Manage hostel information.
* Generate hostel allocation and occupancy reports.

### 🏠 Room Management

The system provides functionality for administrators to:

* Add rooms.
* Update room information.
* Remove rooms.
* View room numbers.
* Define room types.
* Assign rooms to hostels.
* Monitor room availability and status.

### 📊 Reporting

Administrators can generate reports including:

* Hostel occupancy reports.
* Allocation summaries.
* Housing distribution information.
* Filtered allocation information.


## 🛠️ Technologies Used

* **Programming Language:** C#
* **Framework:** .NET
* **Desktop UI:** Windows Forms (WinForms)
* **Database:** MySQL
* **Database Connectivity:** MySQL Connector/NET
* **IDE:** Visual Studio 2022


## 🏗️ System Architecture

The system follows a **three-layer client-server architecture** consisting of:

### 1. Presentation Layer

Provides the user interface through which students and administrators interact with the system.

### 2. Application Layer

Handles the system's business logic, authentication, authorization, hostel applications, and room allocation processes.

### 3. Database Layer

Stores and manages student, hostel, room, administrator, and allocation information.

The project was developed using an **Object-Oriented Programming (OOP)** approach, with major system entities represented as objects and organized into functional modules.


## 🗄️ Database Design

The system uses a relational MySQL database containing the following major tables:

### Student

Stores student information such as:

* Student ID
* First Name
* Last Name
* Gender
* Level
* Department
* Password

### Hostel

Stores hostel information including:

* Hostel ID
* Hostel Name
* Gender
* Location
* Capacity

### Room

Stores room information including:

* Room ID
* Room Number
* Room Type
* Hostel ID
* Room Status

### Allocation

Stores hostel allocation information including:

* Allocation ID
* Student ID
* Room ID
* Allocation Date
* Allocation Status

### Administrator

Stores administrator information including:

* Administrator ID
* Name
* Position
* Phone Number
* Password
* Access Level


## 🔐 Security Features

Security was considered during the design and implementation of the system.

The system includes:

* Role-Based Access Control (RBAC).
* Password hashing.
* Input validation.
* Parameterized SQL queries to help prevent SQL injection.
* Session management.
* Restricted database access.
* Separate student and administrator access.


## 🧪 Testing

The system was tested using:

* **Unit Testing**
* **System Testing**
* **User Acceptance Testing (UAT)**

Testing covered important operations including:

* Valid and invalid login.
* Student registration.
* Duplicate matriculation number prevention.
* Hostel application.
* Room allocation.
* Prevention of allocation to occupied rooms.
* Administrator room management.
* Report generation.
* Complete hostel allocation workflow.
* Prevention of double allocation.
* Gender-based hostel restrictions.
* Application cancellation.

User Acceptance Testing was also conducted to evaluate the system's navigation, speed, clarity, allocation accuracy, and overall satisfaction.


## 🖥️ Main System Interfaces

The application contains several major interfaces:

* Login Screen
* Student Dashboard
* Hostel Application Form
* Administrator Dashboard
* Room Management
* Allocation Management
* Report Screen

The interface was designed with emphasis on simplicity, clarity, accessibility, error prevention, and ease of navigation.


## 📁 Suggested Project Structure

```text
SDU-Hostel-Management-System/
│
├── Forms/
│   ├── LoginForm.cs
│   ├── StudentDashboard.cs
│   ├── HostelApplicationForm.cs
│   ├── AdminDashboard.cs
│   ├── RoomManagement.cs
│   ├── AllocationManagement.cs
│   └── ReportForm.cs
│
├── Database/
│   └── Database Scripts
│
├── Models/
│
├── Resources/
│
├── README.md
└── ...
```

> The exact folder structure may differ depending on the current version of the source code.


## ⚙️ Installation and Setup

### Prerequisites

Before running the application, ensure that the following are installed:

* Windows operating system
* Visual Studio 2022
* .NET development environment
* MySQL Server
* MySQL Connector/NET

### Setup

1. Clone this repository:

```bash
git clone https://github.com/YOUR-USERNAME/SDU-Hostel-Management-System.git
```

2. Open the project in **Visual Studio 2022**.

3. Install/configure the required MySQL connector package.

4. Create the required MySQL database.

5. Import or execute the database scripts provided with the project.

6. Update the database connection settings in the application to match your MySQL configuration.

7. Build the project.

8. Run the application from Visual Studio.

> **Note:** Do not upload real database passwords, administrator passwords, API keys, or other private credentials to GitHub.

---

## 🔄 Hostel Allocation Workflow

The major allocation process follows this general workflow:

```text
Student Registration
        ↓
Hostel Application
        ↓
Application Review
        ↓
Payment Verification
        ↓
Room Availability Check
        ↓
Room Allocation
        ↓
Allocation Status
        ↓
Student Notification
```

The system checks room availability during allocation to help prevent assigning an already occupied room.

---

## 💡 Problems Addressed

The project was developed to address problems associated with manual hostel management, including:

* Delays in hostel allocation.
* Errors in manual record keeping.
* Poor record management.
* Lack of transparency.
* Double allocation of rooms.
* Loss of records.
* Delayed processing.
* Student dissatisfaction.


## 📈 Expected Benefits

The system helps provide:

* Faster hostel allocation.
* More accurate records.
* Improved transparency.
* Better room and occupancy management.
* Reduced allocation errors.
* Improved administrative efficiency.
* Centralized student and hostel information.
* Easier report generation.


## 🎓 Academic Context

**Project Title:**
**Design and Implementation of a Hostel Allocation and Management System for Southern Delta University, Ozoro**

**Institution:** Southern Delta University, Ozoro
**Department:** Computer Science
**Faculty:** Computing
**Year:** 2026


## 👨‍💻 Author

**OGHOGHOMEH HERO FEGA**

Computer Science
Southern Delta University, Ozoro


## 📌 Project Status

**Completed — Final Year Project**

This project was developed as a final-year Computer Science project focused on applying software development, database management, object-oriented programming, system design, and software testing concepts to a real-world university hostel management problem.


## ⭐ Future Improvements

Potential future improvements may include:

* Online/cloud deployment.
* Mobile application support.
* Online payment integration.
* Email/SMS notifications.
* Advanced analytics and dashboards.
* Automated allocation optimization.
* Improved authentication mechanisms.
* Integration with existing university student information systems.


## 📄 Project Documentation

The complete academic project report contains the system analysis, design, implementation, database design, testing, results, and recommendations.


## ⭐ If You Find This Project Useful

Feel free to explore the project, review the implementation, and use it as a reference for learning about **C#, Windows Forms, MySQL database applications, object-oriented programming, and hostel management systems**.
