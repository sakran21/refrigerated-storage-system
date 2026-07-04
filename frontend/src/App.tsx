import { useState } from 'react'
import './App.css'

type Customer = {
  id: number
  fullName: string
  phoneNumber: string
  idType: string
  idNumber: string
  emergencyContactName: string
  emergencyContactPhone: string
  createdAt: string
  updatedAt: string
}

const initialCustomers: Customer[] = [
  {
    id: 1,
    fullName: 'Sample Customer One',
    phoneNumber: '555-0101',
    idType: 'Driver License',
    idNumber: 'DL-001',
    emergencyContactName: 'Sample Contact One',
    emergencyContactPhone: '555-0201',
    createdAt: '2026-01-01T00:00:00Z',
    updatedAt: '2026-01-01T00:00:00Z',
  },
  {
    id: 2,
    fullName: 'Sample Customer Two',
    phoneNumber: '555-0102',
    idType: 'Passport',
    idNumber: 'P-002',
    emergencyContactName: 'Sample Contact Two',
    emergencyContactPhone: '555-0202',
    createdAt: '2026-01-01T00:00:00Z',
    updatedAt: '2026-01-01T00:00:00Z',
  },
  {
    id: 3,
    fullName: 'Sample Customer Three',
    phoneNumber: '555-0103',
    idType: 'State ID',
    idNumber: 'SID-003',
    emergencyContactName: 'Sample Contact Three',
    emergencyContactPhone: '555-0203',
    createdAt: '2026-01-01T00:00:00Z',
    updatedAt: '2026-01-01T00:00:00Z',
  },
]

function App() {
  const [customers, setCustomers] = useState<Customer[]>(initialCustomers)
  const [fullName, setFullName] = useState('')
  const [phoneNumber, setPhoneNumber] = useState('')
  const [idType, setIdType] = useState('')
  const [formError, setFormError] = useState('')

function handleSubmit(event: React.SyntheticEvent<HTMLFormElement>) {
  event.preventDefault()

  if (!fullName.trim() || !phoneNumber.trim() || !idType.trim()) {
    setFormError('Full name, phone number, and ID type are required.')
    return
  }

  const newCustomer: Customer = {
    id: customers.length + 1,
    fullName: fullName.trim(),
    phoneNumber: phoneNumber.trim(),
    idType: idType.trim(),
    idNumber: 'Pending',
    emergencyContactName: 'Pending',
    emergencyContactPhone: 'Pending',
    createdAt: new Date().toISOString(),
    updatedAt: new Date().toISOString(),
  }

  setCustomers([...customers, newCustomer])
  setFullName('')
  setPhoneNumber('')
  setIdType('')
  setFormError('')
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
          {formError && <p className="form-error">{formError}</p>}
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
                <dt>ID Number</dt>
                <dd>{customer.idNumber}</dd>
              </div>

              <div>
                <dt>Emergency Contact</dt>
                <dd>{customer.emergencyContactName}</dd>
              </div>
            </dl>
          </article>
        ))}
      </section>
    </main>
  )
}

export default App