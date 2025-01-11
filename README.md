<h1 align="center" id="title">Project Manager</h1>
<p align="center"><img src="https://img.shields.io/badge/.NET-7.0-8A2BE2" alt="shields"></p>

<p id="description"><b>Project Manager</b> is an application designed to efficiently manage projects across various industries. It enables users to create, plan, and track progress in different types of projects, including group projects that emphasize team collaboration. The application offers comprehensive task management functionality, allowing users to create, edit, and delete tasks, assign them to specific team members, and log time spent on each task. Additionally, users can enhance communication by providing real-time feedback through the built-in comments section. The app supports the creation of custom project roles, while features such as time logging ensure efficient progress tracking.</p>

## Contents
- [Technologies Used](#technologies-used)
- [Features](#features)
- [Upcoming features](#upcoming-features)
- [How to setup](#how-to-setup)
- [Screenshots](#screenshots)

## Technologies Used

*   C#
*   .NET Core MVC
*   Entity Framework
*   HTML
*   CSS
*   Javascript
*   MediatR
*   Clean Architecture
*   Microsoft SQL Server
*   Bootstrap
    
## Features

### Project Creation & Management:
Users can create both individual and group projects, with the ability to add contributors, assign tasks, and track progress. Project Leaders can create custom roles for each project, allowing flexibility in defining responsibilities tailored to the specific needs of the project. Whether it's traditional IT roles like "Backend" or "Tester" or non-IT roles like "Designer" or "Marketing Lead", custom roles ensure that each team member has a clear set of responsibilities. This flexible structure aids in better task allocation and supports task management based on these roles. Additionally, contributors can be added or removed as needed, making collaboration seamless and adaptable.

### Task Management:
Tasks can be created, edited, and deleted within projects. Users can assign deadlines, and overdue tasks are highlighted in red.

### Task Assignment to Users:
Tasks can be assigned to specific users within the project. This ensures accountability and clear ownership of tasks, facilitating better collaboration and progress tracking.

### Commenting System:
Users can add comments to tasks, enabling real-time communication and updates directly within the task details. This feature improves teamwork and helps keep all discussions in one place.

### Time Logging:
Users can log time spent on specific tasks, helping to monitor progress and manage workload effectively. Logged time provides insights into the overall effort required to complete tasks and can be used to improve future project planning.

### Continuous Integration:
The project includes Continuous Integration (CI) using GitHub Actions. CI ensures that code changes are automatically tested and verified before being merged, improving code quality and reducing errors.

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

## How to setup


1. Clone the repository
  ```
  git clone https://github.com/Wajkt0r/ProjectManager.git
  ```
2. Navigate to the project folder
```
cd ProjectManager
```
3. Generate the SSL certificate: Since the application uses HTTPS, you need to generate a development certificate. Run the following command:
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
![ProjectsList](https://github.com/user-attachments/assets/497f086d-45aa-4935-ad72-94a33a2f90fc)

This screenshot shows the list of projects available in the application. Each project displays its name, description, and deadline. As the deadline nears, the date turns orange, and once the deadline is passed, it becomes red and bolded.

---
### Project Creation View
![ProjectCreationView](https://github.com/user-attachments/assets/f79b47a9-c4a4-4309-adf4-a97c9c55b8b9)

This view demonstrates the process of creating a new project. Users can fill out project details such as name, description, and set the project deadline.

---
### Project Overview
![image](https://github.com/user-attachments/assets/246b0623-3696-4223-ac31-1dd41aee7c48)

This view provides an overview of the project, displaying key information at a glance. In the Project Details section, you can see a summary of task distribution by percentage, including tasks that are not assigned, in progress, and completed. Additionally, the number of contributors involved in the project is listed under Contributors. The Project Roles section allows users to manage roles by adding new ones or deleting existing roles by clicking on them. This ensures clarity in team responsibilities and role assignments.


---
### Project Edit View
![ProjectEditView](https://github.com/user-attachments/assets/1dc666ab-5cab-4e8c-b8cf-10c4f25f8f41)

In this screenshot, the project is being edited. Users can update the description, and deadline.

---
### Task Overview
![ProjectTasksList](https://github.com/user-attachments/assets/c876656d-59cf-41ff-8047-1d4cd03c7b0b)

This screenshot displays the list of tasks within a project. Each task shows the task name and its respective deadline. If the task is overdue, the deadline turns red. Users can mark tasks as completed or adjust their deadlines.

---
### Add Task View
![AddTaskView](https://github.com/user-attachments/assets/a6afeae2-9791-4f06-82b2-945c7e2cbeec)

This screenshot displays the interface for creating a new task within a project.

---
### Task Details With Comments
![TaskDetailsWithCommentSection](https://github.com/user-attachments/assets/4da24b25-b9eb-475b-ba62-5bf17bf3edfd)

This view displays detailed information about a specific task, including the assigned user, description, status, and deadline. The comments section allows team members to leave feedback, ask questions, or provide updates related to the task. Additionally, team members can log time spent on the task, helping to track progress and manage workload effectively.


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

Toastr notifications appear in the top-right corner after actions like creation, update, or deletion.

---
### User Management Panel
![AdminPanel](https://github.com/user-attachments/assets/fffb37e5-ae34-4944-826e-ccc36469d01e)

The admin user can manage users in the system. This panel allows admins to view, edit app roles or delete users.

---
### User Deletion Confirmation
![UserDeletionConfirmation](https://github.com/user-attachments/assets/5f22e1ed-0a17-4ec9-a7e1-149e7007b1ba)

When an admin attempts to delete a user, a confirmation prompt is displayed to confirm the action. This prevents accidental deletions.

---
### User Registration Form
![UserRegisterForm](https://github.com/user-attachments/assets/fa8006a7-6f45-490c-8be9-09ca023fb65a)

This screenshot illustrates Identity registration form.

---
### Forbidden Access
![ForbiddenAccess](https://github.com/user-attachments/assets/8b19fc0d-bf12-40c9-ae09-ad8f357f1b8f)

This screenshot shows the "Forbidden Access" error message displayed when a user tries to access a restricted area of the application without the necessary permissions.





