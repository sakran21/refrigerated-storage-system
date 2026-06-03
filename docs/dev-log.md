- Added Customers API checkpoint and verified endpoints.
- ## Planned Backend Testing Phase

Automated backend tests will be added after the core backend API shape is stable. Initial testing will focus on controller integration tests for Customers, StorageUnits, Rentals, Billing, Payments, and related operational endpoints.

Planned test coverage:
- Successful create, read, and update flows
- Validation failures returning 400 Bad Request
- Missing records returning 404 Not Found
- Created records returning 201 Created where appropriate
- Database persistence checks through a dedicated test database or test container

Manual testing will continue during early development using Swagger, PostgreSQL checks, `dotnet build`, and Git checkpoints.
