# 🏠 Booking API

This project implements a RESTful API for retrieving a list of available homes within a specific date range. The system is built using **.NET 9 Web API**, follows **Clean Architecture**, and uses **in-memory storage** (no database).

##  Objective

Allow users to query homes that are available during a given date range. Each home has a list of available slots (dates), and the system filters only those homes whose slots match the entire date range.

---

## ⚙️ Technologies Used

- ✅ .NET 9 Web API  
- ✅ In-Memory Storage (`ConcurrentDictionary`)
- ✅ Asynchronous filtering with `async/await`
- ✅ Clean Architecture principles (Domain, Application, Infrastructure, API)
- ✅ Integration Testing with `xUnit`

---

##  Project Structure

```
BookingAPI/
│
├── src/
│   ├── BookingAPI.API/               --> API controllers and startup
│   ├── BookingAPI.Application/       --> Interfaces, DTOs, and business logic
│   ├── BookingAPI.Domain/            --> Core entities
│   └── BookingAPI.Infrastructure/    --> In-memory data providers
│
└── tests/
    └── BookingAPI.IntegrationTests/  --> Integration tests for API endpoints
```

---

##  How to Run

Make sure you have [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0) installed.

1. Clone the repository:
```bash
git clone https://github.com/faridrajabli23/BookingAPI.git
cd BookingAPI
```

2. Restore packages and build:
```bash
dotnet restore
dotnet build
```

3. Run the API:
```bash
dotnet run --project src/BookingAPI.API/BookingAPI.API.csproj
```

By default, the API will be available at:

- `https://localhost:5001/api/available-homes`

---

##  API Endpoint

### `GET /api/available-homes`

Retrieve a list of homes that are fully available during a specific date range.

#### Query Parameters:
| Name      | Type   | Format       | Description                          |
|-----------|--------|--------------|--------------------------------------|
| startDate | string | `YYYY-MM-DD` | Start of the requested date range    |
| endDate   | string | `YYYY-MM-DD` | End of the requested date range      |

#### Sample Request:
```
GET /api/available-homes?startDate=2025-07-15&endDate=2025-07-16
```

#### Sample Response:
```json
{
  "status": "OK",
  "homes": [
    {
      "homeId": "123",
      "homeName": "Home 1",
      "availableSlots": [
        "2025-07-15",
        "2025-07-16"
      ]
    }
  ]
}
```

---

##  Filtering Logic

The system returns only homes that have **all the dates** in the requested range available in their `availableSlots`.

---

##  How to Run Tests

Run all tests using the following command:

```bash
dotnet test
```

---

##  Design Principles

- **Separation of Concerns**
- **Clean Architecture**
- **SOLID Principles**

---

##  Limitations

- ❌ No database or persistent storage.
- ✅ All data is stored in memory.
- ⚠️ Data resets on application restart.

---

##  Success Criteria Checklist

| Category              | Status | Description |
|-----------------------|--------|-------------|
| Filtering Accuracy    | ✅     | Only fully available homes returned |
| Async Filtering       | ✅     | Non-blocking API logic |
| Clean Architecture    | ✅     | Domain, Application, Infrastructure layers |
| In-Memory Data        | ✅     | Uses `ConcurrentDictionary` |
| Integration Testing   | ✅     | Covers multiple scenarios |
| Code Quality          | ✅     | Clean, readable, modular |
| README Documentation  | ✅     | You're reading it! ✅ |

---

##  Contact

For any questions or contributions, feel free to create an issue or open a pull request.
