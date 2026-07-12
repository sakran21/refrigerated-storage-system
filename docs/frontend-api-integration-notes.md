# Frontend API Integration Notes

## Purpose

This document records the current frontend API integration approach before expanding the React UI further.

The goal is to connect one screen at a time, test each change, and avoid mixing layout problems with backend/API problems.

## Current status

The frontend is now past the purely static phase.

Recent frontend progress includes:
- static customer list rendering
- customer type aligned with backend API response
- customer loading from backend API
- loading and error states for customer API calls
- frontend API base URL moved to an environment variable
- static storage unit list rendering

## API base URL

Frontend API calls should use the Vite environment variable:

VITE_API_BASE_URL

This avoids hardcoding the backend URL directly inside React components.

Local development value:

http://localhost:5183

## Integration order

Recommended integration order:

1. Customers
2. Storage units
3. Rentals
4. Charges and payments
5. Meter readings
6. Oversight/reporting views

This order keeps the frontend aligned with the core operational workflow.

## Testing rule

After each frontend API integration change:

1. Start the backend.
2. Start the frontend.
3. Confirm the screen loads.
4. Confirm loading state appears when relevant.
5. Confirm error state is understandable if the backend is unavailable.
6. Run the frontend build.

Frontend build command:

npm run build

## Current next candidate

The next implementation candidate is storage unit API loading.

That should be done as a small slice:
- keep fallback sample storage unit data
- add storage unit loading state
- add storage unit error state
- fetch storage units from the backend endpoint
- confirm the existing storage unit UI still renders correctly
