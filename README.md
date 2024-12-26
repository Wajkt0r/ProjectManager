<h1 align="center" id="title">Project Manager</h1>
<p align="center"><img src="https://img.shields.io/badge/.NET-7.0-8A2BE2" alt="shields"></p>


<p id="description">Project <b>Project Manager</b> is an application designed to efficiently and organizedly manage projects. With Project Manager, users can create, plan, and track progress in various projects, including group projects where users can collaborate by adding team members. The application allows detailed task management (create, edit, delete tasks) within projects. Currently, it supports both individual and group task management functionalities, with additional features under development. The project is tailored for IT projects, with specific roles such as Backend, Frontend, and Tester for task allocation and management. In the future, the roles will be adapted to fit various types of projects beyond IT.</p>

## Contents
- [Technologies Used](#technologies-used)
- [Features](#features)
- [Future Features](#upcoming-features)
- [How to setup](#how-to-setup)
- [Screenshoots](#screenshots)

## Technologies Used

*   C#
*   .NET Core MVC
*   Entity Framework
*   HTML
*   CSS
*   Javascript
*   MediatR
*   Clean Architecture
*   SQL
*   Bootstrap
    
## Features

### Project Creation & Management:
Users can create both individual and group projects, with the ability to add contributors, assign tasks, and track progress. Project Leaders can assign specific roles such as "Backend", "Frontend", or "Tester" to users, ensuring each team member has clear responsibilities. This structure aids in better task allocation and will support task management based on these roles in future updates.

### Task Management:
Tasks can be created, edited, and deleted within projects. Users can assign deadlines, and overdue tasks are visually highlighted in red.

### User Roles:
Admin users have the ability to manage and remove users from the platform, as well as view all projects and users.

### Notification System:
The app uses Toastr for notifications, alerting users when tasks are created, updated, or deleted.

### User Authentication:
Integrated with Identity, allowing users to register, log in, and manage their accounts.

### Admin:
Admins can manage all projects and users, including assigning or removing users from projects.

## Upcoming Features

*   Application tests
*   Tasks assigned to project roles
*   Flexible Project Roles for Various Industries (Right now there're only IT Project Roles)


## How to setup

Soon
**Docker desktop instalation**
1. Clone the repository
  ```
  git clone https://github.com/Wajkt0r/ProjectManager.git
  ```
2. Navigate to the project folder
```
cd ProjectManager
```
3. Generate the SSL certificate: Since the application uses HTTPS, you need to generate a development certificate. Run the following command
```
dotnet dev-certs https --clean
dotnet dev-certs https -ep ./ProjectManager.MVC/Certificates/aspnetapp.pfx -p Pass@ord1
dotnet dev-certs https --trust
```
4. Build the application: Make sure you have Docker installed to run the application smoothly. Build the Docker containers with the following command:
```
docker-compose up --build
```
5. Access the application: Once the application is up, open your browser and navigate to:
```
https://localhost:5001
```
6. Default Administrator Account: The application comes with a default administrator account. Use the following credentials to log in and manage the system:
```
Email: admin@admin.com
Password: Admin@1
```

## Screenshots

### Project Overview
![ProjectsOverview](https://github.com/user-attachments/assets/6f2dcf1f-7311-434f-9104-3c05237cc825)

This screenshot shows the list of projects available in the application. Each project displays its name, description, and deadline. As the deadline nears, the date turns orange, and once the deadline is passed, it becomes red and bolded.

---
### Project Creation View
![ProjectCreationView](https://github.com/user-attachments/assets/f79b47a9-c4a4-4309-adf4-a97c9c55b8b9)

This view demonstrates the process of creating a new project. Users can fill out project details such as name, description, and set the project deadline.

---
### Project Edit View
![ProjectEditView](https://github.com/user-attachments/assets/1dc666ab-5cab-4e8c-b8cf-10c4f25f8f41)

In this screenshot, the project is being edited. Users can update the project name, description, and deadline.

---
### Task Overview
![TasksOverview](https://github.com/user-attachments/assets/f7290438-3b55-413b-8176-cc33cec3e63c)

This screenshot displays the list of tasks within a project. Each task shows the task name, assigned user, and its respective deadline. If the task is overdue, the deadline turns red. Users can mark tasks as completed or adjust their deadlines.

---
### Project Contributors Overview
![ProjectContributorsOverview](https://github.com/user-attachments/assets/b331ace2-e2da-4ea9-b027-2fc37f6dac9b)

Here, you can see the contributors assigned to a project. Users can be added or removed from projects, and their roles can be adjusted accordingly.

---
### Editing Project Contributor Roles
![ProjectContributorsEditRoles](https://github.com/user-attachments/assets/21ee99eb-79f8-493e-9fe0-a0c1640b8cfc)

This screenshot shows the interface for managing user roles within a project. Project Leaders can assign specific roles like "Backend", "Frontend", or "Tester" to project contributors.

---
### Toastr Notifications
![ToastrContributorRemoval](https://github.com/user-attachments/assets/be14804b-30fb-4f47-bcf8-3ce73c421e26)
![ToastrMaximumUserRoles](https://github.com/user-attachments/assets/25db806b-1bfe-43d9-aed6-f47d78260c4f)

Toastr notifications appear in the top-right corner after actions such as task creation, update, or deletion.

---
### User Management Panel
![UserManagment](https://github.com/user-attachments/assets/d32eae5c-1155-4fcd-a953-e230f9948295)

The admin user can manage users in the system. This panel allows admins to view, add, or delete users. They can also assign users to various projects.

---
### User Deletion Confirmation
![UserDeletionConfirmation](https://github.com/user-attachments/assets/5f22e1ed-0a17-4ec9-a7e1-149e7007b1ba)

When an admin attempts to delete a user, a confirmation prompt is displayed to confirm the action. This prevents accidental deletions.

---
### User Registration Form
![IdentityRegisterForm](https://github.com/user-attachments/assets/5737f087-7b09-4af0-804b-456862a594c5)

This screenshot illustrates Identity registration form.

---
### Forbidden Access
![ForbiddenAccess](https://github.com/user-attachments/assets/8b19fc0d-bf12-40c9-ae09-ad8f357f1b8f)

This screenshot shows the "Forbidden Access" error message displayed when a user tries to access a restricted area of the application without the necessary permissions.





