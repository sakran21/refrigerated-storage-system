type Customer = {
  id: number;
  fullName: string;
  phoneNumber: string;
  idType: string;
  activeRentals: number;
};

const customers: Customer[] = [
  {
    id: 1,
    fullName: "Sample Customer One",
    phoneNumber: "555-0101",
    idType: "Driver License",
    activeRentals: 1,
  },
  {
    id: 2,
    fullName: "Sample Customer Two",
    phoneNumber: "555-0102",
    idType: "Passport",
    activeRentals: 0,
  },
  {
    id: 3,
    fullName: "Sample Customer Three",
    phoneNumber: "555-0103",
    idType: "State ID",
    activeRentals: 2,
  },
];

function App() {
  return (
    <main className="app-shell">
      <section className="page-header">
        <p className="eyebrow">Refrigerated Storage System</p>
        <h1>Customers</h1>
        <p className="page-description">
          Static customer list used to validate frontend layout before API
          integration.
        </p>
      </section>

      <section className="customer-list" aria-label="Customer list">
        {customers.map((customer) => (
          <article className="customer-card" key={customer.id}>
            <div>
              <h2>{customer.fullName}</h2>
              <p>{customer.phoneNumber}</p>
            </div>

            <dl>
              <div>
                <dt>ID Type</dt>
                <dd>{customer.idType}</dd>
              </div>

              <div>
                <dt>Active Rentals</dt>
                <dd>{customer.activeRentals}</dd>
              </div>
            </dl>
          </article>
        ))}
      </section>
    </main>
  );
}

export default App;