import './App.css'

const customers = [
  {
    id: 1,
    fullName: 'Example Customer',
    phone: '555-0100',
    idNumber: 'ID-0001',
    status: 'Placeholder',
  },
  {
    id: 2,
    fullName: 'Second Customer',
    phone: '555-0101',
    idNumber: 'ID-0002',
    status: 'Placeholder',
  },
]

function App() {
  return (
    <main className="app">
      <h1>Refrigerated Storage Rental System</h1>
      <section className="summary-cards">
          <article className="summary-card">
            <h2>Available Units</h2>
            <p>Placeholder count</p>
          </article>

          <article className="summary-card">
            <h2>Active Rentals</h2>
            <p>Placeholder count</p>
          </article>

          <article className="summary-card">
            <h2>Delinquent Rentals</h2>
            <p>Placeholder count</p>
          </article>
      </section>
      <div className="dashboard-grid">
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
          <div className="table-wrapper">
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
                  {customers.map((customer) => (
                    <tr key={customer.id}>
                      <td>{customer.fullName}</td>
                      <td>{customer.phone}</td>
                      <td>{customer.idNumber}</td>
                      <td>{customer.status}</td>
                    </tr>
                  ))}
                </tbody>
            </table>
          </div>
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
      </div>  
    </main>
  )
}

export default App