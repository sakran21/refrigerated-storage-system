import './App.css'

function App() {
  return (
    <main className="app">
      <h1>Refrigerated Storage Rental System</h1>

      <section className="dashboard-section">
        <h2>Customer Intake</h2>

        <form className="customer-form">
          <label>
            Full Name
            <input type="text" placeholder="Customer full name" />
          </label>

          <label>
            Phone
            <input type="tel" placeholder="Customer phone number" />
          </label>

          <label>
            ID Number
            <input type="text" placeholder="ID number" />
          </label>

          <button type="button">Save Customer</button>
        </form>
      </section>

      <section className="dashboard-section">
        <h2>Customers</h2>

        <table className="customer-table">
          <thead>
            <tr>
              <th>Name</th>
              <th>Phone</th>
              <th>ID Number</th>
              <th>Status</th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td>Example Customer</td>
              <td>555-0100</td>
              <td>ID-0001</td>
              <td>Placeholder</td>
            </tr>
          </tbody>
        </table>
      </section>

      <section className="dashboard-section">
        <h2>Storage Units</h2>
        <p>Unit availability, rental status, and maintenance status will appear here.</p>
      </section>

      <section className="dashboard-section">
        <h2>Rentals</h2>
        <p>Active rentals, billing periods, and delinquency status will appear here.</p>
      </section>

      <section className="dashboard-section">
        <h2>Payments</h2>
        <p>Cash payments and charge allocation will appear here.</p>
      </section>
    </main>
  )
}

export default App