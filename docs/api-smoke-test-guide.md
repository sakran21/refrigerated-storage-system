# API Smoke Test Guide

## Purpose

This guide records the basic manual checks used to confirm that the backend API is still functioning after changes.

Smoke tests are not full automated test coverage. They are quick checks that confirm the main system paths still respond correctly.

## Before testing

Start the backend API:

dotnet run --project backend

Open Swagger:

http://localhost:5183/swagger

Confirm PostgreSQL is running and the storage_dev database is available.

## Customer checks

### List customers

Endpoint:
GET /api/customers

Expected result:
- returns 200 OK
- returns a JSON list
- does not require manual database inspection for basic confirmation

### Create customer

Endpoint:
POST /api/customers

Expected result:
- returns 201 Created
- response includes the created customer
- required fields are enforced
- invalid or missing required fields return a validation error

### Get customer by ID

Endpoint:
GET /api/customers/{id}

Expected result:
- existing customer returns 200 OK
- missing customer returns 404 Not Found

### Update customer

Endpoint:
PUT /api/customers/{id}

Expected result:
- existing customer can be updated
- missing customer returns 404 Not Found
- validation rules still apply

## Storage unit checks

### List storage units

Endpoint:
GET /api/storageunits

Expected result:
- returns 200 OK
- returns a JSON list of units

### List available units

Endpoint:
GET /api/storageunits/available

Expected result:
- returns only units available for rental
- rented and maintenance units are excluded

### Create storage unit

Endpoint:
POST /api/storageunits

Expected result:
- returns 201 Created
- unit number/status fields are saved correctly

### Update storage unit

Endpoint:
PUT /api/storageunits/{id}

Expected result:
- existing unit can be updated
- missing unit returns 404 Not Found

## Rental-related checks

### List active rentals

Endpoint:
GET /api/rentals/active

Expected result:
- returns 200 OK
- returns active rentals only
- closed rentals are excluded

### Rental history endpoints

Expected result:
- rental charge history returns charges for the rental
- rental deposit history returns deposit credits for the rental
- rental meter reading history returns readings for the rental
- billing period reading history returns readings linked to that billing period

## Database confirmation

Use psql only when API output looks wrong or when confirming migrations.

Common command:

psql -U postgres -d storage_dev

Useful checks:
- confirm tables exist
- confirm inserted rows persisted
- confirm foreign key relationships look correct
- confirm enum/status values are stored as expected

## Smoke test rule

If a smoke test fails, do not keep adding features.

First determine whether the failure is caused by:
- backend code
- database state
- migration mismatch
- wrong endpoint
- invalid request body
- frontend/client assumption
