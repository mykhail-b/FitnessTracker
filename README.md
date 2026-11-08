# 💪 Fitness Tracker fullstack app 

A RESTful API for tracking workouts, exercises, and body metrics

## Features

- User registration and authentication
- Workout management
- Exercise catalog
- Users workout lists and activity data
- Meal catalog
- JWT authentication

## 🔨 Tech Stack

* **Backend:** ASP.NET Core
* **Database:** Microsoft SQL Server (LocalDB)
* **ORM:** Entity Framework Core
* **Authentication:** ASP.NET Core Identity
* **Authorization:** Cookie Authentication
* **API Documentation:** Swagger

<!-- ## Architecture -->

### Authentication

Authentication is implemented with **ASP.NET Core Identity** using **JWT authentication cookies**.


## API Endpoints

### Auth
| Method | Endpoint | Description |
|--------|----------|--------------|
| POST | `/api/auth/register` | Register a new user |
| POST | `/api/auth/login` | Log in, sets HttpOnly auth cookie |
| POST | `/api/auth/logout` | Log out, clears auth cookie |
| GET | `/api/auth/me` | Get currently authenticated user |

### UserProfile
| Method | Endpoint | Description |
|--------|----------|--------------|
| GET | `/api/user/me` | Get current user's info |
| GET | `/api/user/{userId}` | Get user info by id |
| POST | `/api/user/{userId}` | Update user info |
| DELETE | `/api/user/{userId}` | Delete user account |


### Exercises
| Method | Endpoint | Description |
|--------|----------|--------------|
| GET | `/api/exercise?pageNumber=1&pageSize=10` | Get paged list of exercises |
| GET | `/api/exercise/{exerciseId}` | Get exercise details |
| POST | `/api/exercise` | Create a new exercise |
| PUT | `/api/exercise/{exerciseId}` | Update an exercise |
| DELETE | `/api/exercise/{exerciseId}` | Delete an exercise |
 
### Food
| Method | Endpoint | Description |
|--------|----------|--------------|
| GET | `/api/food?pageNumber=1&pageSize=10` | Get paged list of food items |
| GET | `/api/food/{foodId}` | Get food item details |
| POST | `/api/food` | Create a new food item |
| PUT | `/api/food/{foodId}` | Update a food item |
| DELETE | `/api/food/{foodId}` | Delete a food item |

### Meal
| Method | Endpoint | Description |
|--------|----------|--------------|
| GET | `/api/meal?pageNumber=1&pageSize=10` | Get paged list of meals |
| GET | `/api/meal/{mealId}` | Get users meal details |
| POST | `/api/meal` | Create a new meal |
| PUT | `/api/meal/{mealId}` | Update a meal |
| DELETE | `/api/meal/{mealId}` | Delete a meal |
 
### Workouts
| Method | Endpoint | Description |
|--------|----------|--------------|
| GET | `/api/workout?pageNumber=1&pageSize=10` | Get paged list of workouts for the current user |
| GET | `/api/workout/{workoutId}` | Get a specific workout |
| GET | `/api/workout/activity` | Get the current user's workout activity dates |
| POST | `/api/workout` | Create a new workout |
| PUT | `/api/workout/{workoutId}` | Update a workout |
| DELETE | `/api/workout/{workoutId}` | Delete a workout |

<!--
## 🚀 Getting started 
### Prerequisites
- .NET 10 SDK
- Microsoft SQL Server LocalDB

### Setup

1. Clone the repository
    ```bash
   > git clone https://github.com/mykhail-b/FitnessTracker.git
   > cd FitnessTracker
   > dotnet restore
    ```

2. Set up local secrets (connection string, SMTP config)
    ```bash
    > cd FitTrackApi.Server
    > dotnet user-secrets init
    > dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=(localdb)\\mssqllocaldb;Database=FitTrackDb;Trusted_Connection=True;"
    ```

3. Apply EF Core migrations
   ```bash
   > dotnet ef database update
   ```

4. Run the API
   ```bash
   > dotnet run --project /src/FitTrackApi.Server
   ```

5. Open Swagger UI at `https://localhost:7114/swagger`

-->