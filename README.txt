================================================================================
                          SMARTPLANT - SETUP INSTRUCTIONS
================================================================================

Project: SmartPlant - Intelligent Plant Care Assistant
Course: Soft3111 - Fall 2025
Students: 21COMP1047-Şevval Nur Kurban, 21COMP1002-Simge Eylül Özer

================================================================================
                              TABLE OF CONTENTS
================================================================================
1. Prerequisites
2. Database Setup
3. Running the Application
4. Default Login Credentials
5. Project Structure
6. Features Overview
7. Troubleshooting

================================================================================
                              1. PREREQUISITES
================================================================================
Before running the application, ensure you have:

- Visual Studio 2022 (or later)
- .NET 7.0 SDK or later
- SQL Server (LocalDB, Express, or Full version)
- Internet connection (for NuGet package restore)

================================================================================
                              2. DATABASE SETUP
================================================================================

OPTION A: AUTOMATIC DATABASE CREATION (RECOMMENDED)
----------------------------------------------------
The database will be created automatically when you first run the application.

1. Open the solution file: SmartPlant.sln

2. Open appsettings.json and verify the connection string:
   "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SmartPlantDb;Trusted_Connection=True;MultipleActiveResultSets=true"
   }

   NOTE: If you're using SQL Server Express, change to:
   "Server=.\\SQLEXPRESS;Database=SmartPlantDb;Trusted_Connection=True;MultipleActiveResultSets=true"

3. Open Package Manager Console (Tools > NuGet Package Manager > Package Manager Console)

4. Run the following commands:
   Update-Database

   This will:
   - Create the SmartPlantDb database
   - Apply all migrations
   - Create all tables (Users, Plants, UserPlants, Reminders, Feedback)

5. The database is now ready!


OPTION B: MANUAL DATABASE CREATION
-----------------------------------
If automatic creation fails:

1. Open SQL Server Management Studio (SSMS)

2. Connect to your SQL Server instance

3. Create a new database named "SmartPlantDb"

4. Update the connection string in appsettings.json with your server details

5. Run Update-Database in Package Manager Console


IMPORTANT: ADMIN USER SETUP
----------------------------
After the database is created, you need to insert an Admin user manually.

1. Open SQL Server Management Studio or Visual Studio's SQL Server Object Explorer

2. Connect to (localdb)\mssqllocaldb (or your SQL Server instance)

3. Find the SmartPlantDb database

4. Run the following SQL script to create the admin user:

   INSERT INTO Users (Name, Email, PasswordHash, Role, IsDeleted)
   VALUES (
       'Admin',
       'admin@smartplant.com',
       '$2a$11$vPDZE.yjXqY5bQYmLqYZ9OLWH2YHxZ9KL7HLCKGBdU4GbXCaKr7Du',
       'Admin',
       0
   );

   This creates an admin account with:
   - Email: admin@smartplant.com
   - Password: admin123

   SECURITY NOTE: Change this password immediately in production!

================================================================================
                          3. RUNNING THE APPLICATION
================================================================================

1. Open SmartPlant.sln in Visual Studio

2. Ensure "SmartPlant" is set as the startup project (right-click project > Set as Startup Project)

3. Press F5 or click the "Run" button (green triangle)

4. The application will open in your default browser at https://localhost:xxxxx

5. You should see the SmartPlant home page with the plant icon and "Get Started" button

================================================================================
                        4. DEFAULT LOGIN CREDENTIALS
================================================================================

ADMIN ACCOUNT:
--------------
Email: admin@smartplant.com
Password: admin123

Features available to Admin:
- Manage Plants (Add, Edit, Delete plant species in the database)
- Manage Users (View and manage all registered users)
- View Feedback (See all user feedback submissions)
- Admin Dashboard


USER ACCOUNT:
-------------
You can create a regular user account by clicking "Register" on the home page.

Features available to Users:
- Add plants from the database to their personal collection
- Set watering reminders for their plants
- View plant care instructions (light, temperature, watering frequency)
- Submit feedback
- Manage their profile

================================================================================
                           5. PROJECT STRUCTURE
================================================================================

SmartPlant/
│
├── SmartPlant.sln                    # Solution file
│
└── SmartPlant/                       # Main project folder
    │
    ├── Controllers/                  # MVC Controllers
    │   ├── AccountController.cs      # Login, Register, Profile
    │   ├── AdminController.cs        # Admin panel operations
    │   ├── FeedbackController.cs     # User feedback
    │   ├── HomeController.cs         # Home, About, Help pages
    │   ├── RemindersController.cs    # Watering reminders
    │   └── UserPlantsController.cs   # User's plant collection
    │
    ├── Models/                       # Data models
    │   ├── User.cs
    │   ├── Plant.cs
    │   ├── UserPlant.cs
    │   ├── Reminder.cs
    │   └── Feedback.cs
    │
    ├── Views/                        # Razor views
    │   ├── Account/                  # Login, Register, Profile views
    │   ├── Admin/                    # Admin panel views
    │   ├── Feedback/
    │   ├── Home/                     # Home, About, Help
    │   ├── Reminders/
    │   ├── UserPlants/
    │   └── Shared/
    │       ├── _Layout.cshtml        # User layout
    │       └── _AdminLayout.cshtml   # Admin layout
    │
    ├── Data/
    │   ├── ApplicationDbContext.cs   # EF Core DbContext
    │   ├── Configurations/           # Fluent API configurations
    │   └── Migrations/               # Database migrations
    │
    ├── Services/                     # Business logic layer
    │   ├── Interfaces/
    │   └── Implementations/
    │
    ├── Repositories/                 # Data access layer
    │   ├── Interfaces/
    │   └── Implementations/
    │
    ├── Helpers/                      # Utility classes
    │   └── FileUploadHelper.cs
    │
    ├── wwwroot/                      # Static files
    │   ├── uploads/                  # Uploaded plant images
    │   ├── css/
    │   ├── js/
    │   └── lib/
    │
    ├── appsettings.json              # Configuration file
    └── Program.cs                    # Application entry point

================================================================================
                          6. FEATURES OVERVIEW
================================================================================

USER FEATURES:
--------------
✓ User registration and authentication
✓ Personal plant collection management
✓ Browse and add plants from database
✓ View plant care information (light, water, temperature)
✓ Set and manage watering reminders
✓ Upload photos for personal plants
✓ Submit feedback to admin
✓ Profile management


ADMIN FEATURES:
---------------
✓ Admin authentication with persistent login
✓ Manage plant database (Add, Edit, Delete plant species)
✓ Upload plant photos
✓ Set plant characteristics:
  - Common Name & Scientific Name
  - Light Requirements
  - Watering Frequency
  - Temperature Range (min-max)
  - Care Instructions
✓ Manage users (View, Delete)
✓ View user feedback
✓ Cascade delete (when plant deleted, all user plants and reminders removed)


TECHNICAL FEATURES:
-------------------
✓ ASP.NET Core MVC 7.0
✓ Entity Framework Core with Code-First approach
✓ SQL Server database
✓ Repository and Service pattern
✓ Cookie-based authentication
✓ Role-based authorization (Admin/User)
✓ Responsive Bootstrap 5 UI
✓ File upload for plant images
✓ Soft delete pattern
✓ Fluent API for database configuration

================================================================================
                           7. TROUBLESHOOTING
================================================================================

PROBLEM: Database connection error
SOLUTION:
- Verify SQL Server is running
- Check connection string in appsettings.json
- Ensure (localdb)\mssqllocaldb is installed
- Try running: sqllocaldb start mssqllocaldb


PROBLEM: Migration errors
SOLUTION:
- Delete the Migrations folder
- Delete the database
- Run: Add-Migration InitialCreate
- Run: Update-Database


PROBLEM: Cannot login as admin
SOLUTION:
- Verify admin user was inserted into database
- Check password hash is exactly: $2a$11$vPDZE.yjXqY5bQYmLqYZ9OLWH2YHxZ9KL7HLCKGBdU4GbXCaKr7Du
- Ensure Role = 'Admin' (case-sensitive)


PROBLEM: NuGet package errors
SOLUTION:
- Right-click solution > Restore NuGet Packages
- Clean and rebuild solution


PROBLEM: Plant images not displaying
SOLUTION:
- Ensure wwwroot/uploads/plants folder exists
- Check file upload permissions
- Verify FileUploadHelper.cs is working


PROBLEM: Admin login not persisting after restart
SOLUTION:
- Clear browser cookies
- Login again
- Cookie is set to persist for 30 days


PROBLEM: About/Help pages showing wrong navigation
SOLUTION:
- Clear browser cache
- Rebuild solution
- Verify Layout is set correctly in About.cshtml and Help.cshtml

================================================================================
                                  NOTES
================================================================================

1. This is an educational project for Soft3111 course
2. Default admin password should be changed in production environment
3. File uploads are stored in wwwroot/uploads/plants
4. Database uses soft delete (IsDeleted flag) for data retention
5. All navigation bars adapt based on user role (Admin/User)
6. Temperature range uses two-input format (min-max)
7. Photo removal sets PhotoUrl to NULL in database

================================================================================
                            END OF DOCUMENTATION
================================================================================