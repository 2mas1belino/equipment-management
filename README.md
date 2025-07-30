# Equipment Management Application (ASP.NET + Blazor WASM)

## Description

This project is a small application developed as part of a technical challenge.  
It is built with **ASP.NET 9.0 (Core Hosted)**, **Blazor WebAssembly**, and **Entity Framework Core**, and allows users to manage a list of equipment.

### Features
- Create, edit and delete equipment  
- Upload and remove equipment images  
- Manage equipment availability (date periods)  
- RESTful API endpoints in ASP.NET Core with EF Core  

---

## Technologies Used
- .NET 9.0  
- Blazor WebAssembly (hosted model)  
- ASP.NET Core (Web API)  
- Entity Framework Core (SQL Server)  
- Bootstrap 5 (UI)

---

## Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/en-us/download)  
- Microsoft.AspNetCore.Components.WebAssmbly.Server package
- Microsoft.EntityFrameworkCore package
- Microsoft.EntityFrameworkCore.SqlServer package
- Microsoft.EntityFramworkCore.Tools package
- (Optional) Visual Studio 2022 or Visual Studio Code  

---

## How to Run the Project

1. **Clone the repository**  
   ```bash
   git clone https://github.com/<your-username>/<repo-name>.git
   cd <repo-name>
   ```

2. **Set up the database**
    Update the connection string in appsettings.json inside the Server project (EquipmentManagement).

    Run the EF Core migrations:
    ```bash
    dotnet ef database update --project EquipmentManagement
    ```

3. **Run the application**
    ```bash
    dotnet run --project EquipmentManagement
    ```
    The application will be available at https://localhost:7117 (or the port shown in the console).

---

## Screenshots

<img width="1919" height="794" alt="image" src="https://github.com/user-attachments/assets/dbf5f9be-4cdf-4aac-86ff-fe5664bd4651" />
<img width="1923" height="916" alt="image" src="https://github.com/user-attachments/assets/ec4f9718-3a94-41e7-88c2-0a9aa80ecfbd" />
<img width="739" height="895" alt="image" src="https://github.com/user-attachments/assets/a2271131-8672-4ad3-a4e5-76b8b228c668" />

---

## API Overview

- ```GET /api/equipment``` - Get all equipment (paginated)
- ```POST /api/equipment``` - Create equipment
- ```PUT /api/equipment/{id}``` - Update equipment
- ```DELETE /api/equipment/{id}``` - Delete equipment
- ```POST /api/equipment/{id}/upload-image``` - Upload equipment image
- ```DELETE /api/equipment/{id}/image``` - Remove equipment image
- ```GET /api/equipment/{id}/availability``` - Get availability periods
- ```POST /api/equipment/{id}/availability``` - Add availability period
- ```DELETE /api/equipment/availability/{id}``` - Remove availability period

---

## Author

Developed by Tomás Umbelino
