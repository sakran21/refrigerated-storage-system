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

## Storage Unit Status Oversight Concern

A storage unit marked as `maintenance` cannot be rented through the system. However, this does not by itself prevent an operator from marking a unit as maintenance and then renting it outside the system.

Planned control requirements:

* All storage unit status changes must be audit-logged.
* Status changes involving `maintenance` must require a reason.
* Normal workflow status changes, such as `available` to `rented`, should be logged but not automatically flagged.
* Suspicious maintenance-related activity should generate a ReviewFlag for Admin review.

Maintenance-related activity should be considered suspicious when:

* a unit remains in maintenance beyond a defined threshold
* the same unit is repeatedly placed into maintenance
* maintenance is set manually by an Operator
* maintenance begins shortly after a rental ends
* maintenance is reversed quickly without explanation

This concern should be addressed before the system is considered operationally safe for real business use.
