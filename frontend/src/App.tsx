import { useState } from 'react'
import './App.css'

type Customer = {
  id: number
  fullName: string
  phoneNumber: string
  idType: string
  activeRentals: number
}

const initialCustomers: Customer[] = [
  {
    id: 1,
    fullName: 'Sample Customer One',
    phoneNumber: '555-0101',
    idType: 'Driver License',
    activeRentals: 1,
  },
  {
    id: 2,
    fullName: 'Sample Customer Two',
    phoneNumber: '555-0102',
    idType: 'Passport',
    activeRentals: 0,
  },
  {
    id: 3,
    fullName: 'Sample Customer Three',
    phoneNumber: '555-0103',
    idType: 'State ID',
    activeRentals: 2,
  },
]

function App() {
  const [customers, setCustomers] = useState<Customer[]>(initialCustomers)
  const [fullName, setFullName] = useState('')
  const [phoneNumber, setPhoneNumber] = useState('')
  const [idType, setIdType] = useState('')

function handleSubmit(event: React.SyntheticEvent<HTMLFormElement>) {    event.preventDefault()
    const newCustomer: Customer = {
      id: customers.length + 1,
      fullName,
      phoneNumber,
      idType,
      activeRentals: 0,
    }

    setCustomers([...customers, newCustomer])
    setFullName('')
    setPhoneNumber('')
    setIdType('')
  }

  return (
    <main className="app-shell">
      <section className="page-header">
        <p className="eyebrow">Refrigerated Storage System</p>
        <h1>Customers</h1>
        <p className="page-description">
          Static customer list used to validate frontend layout before API integration.
        </p>
      </section>

      <section className="customer-form-section">
        <h2>Add Customer</h2>

        <form className="customer-form" onSubmit={handleSubmit}>
          <label>
            Full Name
            <input
              type="text"
              value={fullName}
              onChange={(event) => setFullName(event.target.value)}
            />
          </label>

          <label>
            Phone Number
            <input
              type="tel"
              value={phoneNumber}
              onChange={(event) => setPhoneNumber(event.target.value)}
            />
          </label>

          <label>
            ID Type
            <input
              type="text"
              value={idType}
              onChange={(event) => setIdType(event.target.value)}
            />
          </label>

          <button type="submit">Add Customer</button>
        </form>
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
  )
}

export default App