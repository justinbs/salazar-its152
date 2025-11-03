# SALAZAR_LE6 – Custom Angular Frontend Design (Based on LE5)
**Student:** Justin Brylle G. Salazar  
**Course:** ITS152L – Systems Integration and Architecture 2 Laboratory  

---

## Activity Overview
This laboratory exercise (LE6) is a continuation of LE5, where I developed an Angular frontend connected to my LE4 Blog API backend.  
For this activity, the goal was to redesign the frontend using my own layout, color scheme, and user interface design while maintaining full functionality and data connectivity with the existing REST API.

LE6 focuses on user interface design, styling, and responsiveness, building upon the data integration already achieved in LE5.

---

## System Architecture Overview

| Layer | Technology Used | Description |
|:--|:--|:--|
| **Frontend** | Angular 17 | Redesigned UI and layout (Home, Post Details, Login, Register pages) |
| **Backend API** | ASP.NET Core 6 (from LE4) | Provides REST endpoints for Posts, Login, and Register |
| **Database** | SQL Server LocalDB | Stores Posts and User data |
| **Language(s)** | TypeScript, HTML, CSS, C# | Used for frontend, UI design, and backend logic |

---

## Design Description
In this LE6 activity, I applied my own design and styling to improve the look and usability of the Angular Blog frontend created in LE5.

### Key Improvements
- **Navigation Bar:** Clean header with “Home,” “Login,” and “Register” links  
- **Home Page:** Card grid layout for displaying posts with better spacing and visual hierarchy  
- **Post Details Page:** Modernized display for post titles, content, and metadata  
- **Login/Register Pages:** Redesigned input forms with improved readability and structure  
- **Global CSS Styling:** Added consistent color palette, hover effects, and responsive design  

All pages automatically adjust to different screen sizes (desktop to mobile) for accessibility and responsive design compliance.

---

## Implementation Notes
- Reused the LE4 Blog API as backend (`https://localhost:7067/api/Post`)  
- Added temporary `[AllowAnonymous]` attributes to allow API access from Angular  
- Updated `Program.cs` to enable CORS for `http://localhost:4200`  
- Normalized inconsistent backend field names (`id`, `Id`, `title`, `Title`, etc.) in `posts.service.ts`  
- Applied global styling updates in `styles.css` and component-level designs in each HTML file  

---

## Testing and Demo
1. Run the backend (`BlogAPI`) from LE4.  
2. Run the frontend:
   ```bash
   cd SALAZAR_LE6/frontend
   ng serve
