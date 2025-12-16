# 🚗 Driving License Management System

The **Driving License Management System (DVLD)** is a desktop application designed to simplify and automate the process of managing driving licenses. The system is built using a **three-layer architecture**, ensuring clear separation of concerns and maintainability.  
This project is developed using **C# .NET** and **SQL Server**, with a dedicated folder included for the database scripts.

## 🔹 Project Architecture
1. **DVLD Presentation & Access Layer**
   - Provides forms and controls for managing users and people and Licenses and applications.
   - Handles CRUD operations (Add, Edit, Delete) for users/people.
   - Manages all interactions between the user and the system.

2. **Business Logic Layer (DVLD_Businesses)**
   - Processes business rules such as license issuance, renewals, and test results.
   - Ensures validation and workflow consistency.

3. **Data Access Layer (DVLD_DataAcces)**
   - Manages database interactions with SQL Server.
   - Provides data persistence for users, licenses, tests, and applications.

## 🔹 Key Features
- **License Management** 🚦  
  - Issue new licenses.  
  - Renew existing licenses.  
  - Replace lost or damaged licenses.  
  - View license details and license history.  

- **Test Management** 📝  
  - Schedule test appointments.  
  - Manage test results (Pass/Fail).  
  - Three types of tests:  
    - Vision Test 👀  
    - Written Test ✍️  
    - Street Test 🚘  

- **Application Management** 📂  
  - Manage different application types (e.g., new license, renewal, replacement).  
  - Manage test applications.  

- **User & People Management** 👤  
  - Add, edit, and delete users/people.  
  - Secure and structured access.
    
- **Detained Licenses Management** 🚓  
  - Detain a license.  
  - Release a detained license.  
  - Track detained licenses.
    
## 🔹 Forms Included
- **User/People Forms** → Manage users and personal information.  
- **License Forms** → Issue, renew, replace, and view license history.  
- **Application Type Form** → Manage types of applications.  
- **Application Test Form** → Manage test-related operations.  
