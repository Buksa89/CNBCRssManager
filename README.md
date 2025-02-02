# CNBC RSS Manager

CNBC RSS Manager is a full-stack application that allows users to manage and read RSS feed items from CNBC. The application consists of a React frontend and a .NET Core backend API.

## Technologies Used

### Frontend
- React
- JavaScript
- HTML/CSS

### Backend
- .NET Core 8.0
- C#
- Entity Framework Core
- SQLite

### Other
- RSS Feed Parsing
- Memory Caching
- Dependency Injection
- CORS (Cross-Origin Resource Sharing)

## Architecture

The application follows a client-server architecture:

1. **Frontend**: A React application that provides the user interface for displaying and interacting with RSS feed items.

2. **Backend**: A .NET Core API that handles data management, RSS feed fetching, and serves as the intermediary between the frontend and the database.

3. **Database**: SQLite database for storing RSS feed items.

## Key Components

### Backend

- **Controllers**: Handle HTTP requests and responses (FeedController.cs)
- **Models**: Define data structures (FeedItem.cs)
- **Services**: Implement business logic (RssFeedService.cs)
- **Repositories**: Handle data access (FeedRepository.cs)
- **DbContext**: Manages database connections and operations (FeedDbContext.cs)

### Frontend

- **Components**: Reusable UI elements (FeedItem.js)

## How to Build and Run

### Backend

1. Navigate to the backend directory:
   ```
   cd backend/CNBCRssManager.API
   ```

2. Restore dependencies:
   ```
   dotnet restore
   ```

3. Migrate the database:
   ```
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

4. Build the project:
   ```
   dotnet build
   ```

5. Run the application:
   ```
   dotnet run
   ```

The API will be available at `https://localhost:5170` by default.

### Frontend

1. Navigate to the frontend directory:
   ```
   cd frontend
   ```

2. Install dependencies:
   ```
   npm install
   ```

3. Start the development server:
   ```
   npm start
   ```

The React application will be available at `http://localhost:3000`.

## Configuration

- Backend configuration is stored in `appsettings.json`
- The RSS feed URL and refresh interval can be adjusted in the `RssFeedOptions` section of `appsettings.json`

## Features

- Fetch and display RSS feed items from CNBC
- Mark items as read
- Delete items
- Automatic feed refresh (configurable interval)
- Caching for improved performance

## TODOs

1. Fix npm vulnerabilities
2. Secure connection with HTTPS
3. Implement user authentication and authorization
4. Add unit tests for both frontend and backend
5. Implement pagination for large numbers of feed items
6. Set up a separate SQL database