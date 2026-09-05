## MovieLog Project

#### Description
MovieLog is a web platform for managing personal movie collections, rating films, and writing reviews. The project demonstrates the integration of a RESTful API with an interactive frontend.

#### Technologies Used
- [ ] Backend: ASP.NET Core Web API (.NET 8)
- [ ] Frontend: MVC with Razor Views + Vue 3 widget (via CDN)
- [ ] Styling: HTML5, CSS3, Bootstrap 5
- [ ] Database: SQL Server with Entity Framework Core
- [ ] Authentication: ASP.NET Core Identity (Roles: Admin, User)
- [ ] API Documentation: Swagger / OpenAPI

#### Main Features
- [ ] Movie management: Admins can add, edit, and delete movies from the catalog.
- [ ] Watchlist: Users can save favorite movies for later viewing.
- [ ] Reviews and Ratings: Users can rate movies (1-10) and write opinions about films they have watched.
- [ ] Statistics widget: Home page displays the top 3 highest-rated movies, rendered dynamically with Vue.
- [ ] Validation: Data validation on both server side (DTOs) and client side (browser).
- [ ] Security: Protected routes based on user role (Admin vs User).
- [ ] Global exception handling: Centralized middleware returns consistent JSON error responses.
- [ ] Request logging: All HTTP requests are logged with method, path, status code, and duration.

#### Architecture
The project uses a hybrid model, where MVC handles page rendering while data interaction happens asynchronously through fetch calls to a dedicated REST API. The top-rated movies section on the home page is implemented as a Vue 3 component that consumes `/api/stats/top-rated`, demonstrating SPA-framework integration alongside the traditional MVC pages.

The backend follows a clean layered architecture:
- **Controllers** — handle HTTP requests and return DTOs
- **Services** — encapsulate business logic
- **Repositories** (via Unit of Work) — abstract data access
- **DTOs** — decouple API contracts from EF entities

Dependency Injection is configured in `Program.cs` for all services and repositories.
