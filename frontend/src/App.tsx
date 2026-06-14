import './App.css'

function App() {
  return (
    <main className="app">
      <h1>Refrigerated Storage Rental System</h1>

      <section className="dashboard-section">
        <h2>Customers</h2>
        <p>Customer records and contact information will appear here.</p>
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