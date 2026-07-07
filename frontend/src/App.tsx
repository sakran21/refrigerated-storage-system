import { useEffect, useState } from 'react'
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



type StorageUnit = {
  id: number;
  unitNumber: string;
  sizeCategory: string;
  status: "Available" | "Rented" | "Maintenance";
  currentCustomer?: string;
};

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

const storageUnits: StorageUnit[] = [
  {
    id: 1,
    unitNumber: "A-101",
    sizeCategory: "Small",
    status: "Available",
  },
  {
    id: 2,
    unitNumber: "B-204",
    sizeCategory: "Medium",
    status: "Rented",
    currentCustomer: "Sample Customer One",
  },
  {
    id: 3,
    unitNumber: "C-301",
    sizeCategory: "Large",
    status: "Maintenance",
  },
];

function App() {
  const [customers, setCustomers] = useState<Customer[]>(initialCustomers)
  const [fullName, setFullName] = useState('')
  const [phoneNumber, setPhoneNumber] = useState('')
  const [idType, setIdType] = useState('')
  const [formError, setFormError] = useState('')
  const [isLoadingCustomers, setIsLoadingCustomers] = useState(true)
  const [customerLoadError, setCustomerLoadError] = useState('')

  useEffect(() => {
  async function loadCustomers() {
  try {
        setIsLoadingCustomers(true)
        setCustomerLoadError('')

        const response = await fetch('http://localhost:5183/api/customers')

        if (!response.ok) {
          throw new Error('Failed to load customers.')
        }

        const data: Customer[] = await response.json()
        setCustomers(data)
      } catch {
        setCustomerLoadError('Unable to load customers from the API.')
      } finally {
        setIsLoadingCustomers(false)
      }
   }


  loadCustomers()
  }, [])

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
          Customer and storage unit interface used to validate frontend layout, form behavior,
          and early API integration.
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
        {isLoadingCustomers && <p>Loading customers...</p>}

        {customerLoadError && <p className="form-error">{customerLoadError}</p>}

        {!isLoadingCustomers &&
          !customerLoadError &&
          customers.map((customer) => (
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
      <section className="content-section">
        <h2>Storage Units</h2>

        <div className="card-list">
          {storageUnits.map((unit) => (
            <article className="info-card" key={unit.id}>
              <div>
                <h3>Unit {unit.unitNumber}</h3>
                <p>{unit.sizeCategory}</p>
              </div>

              <dl>
                <div>
                  <dt>Status</dt>
                  <dd>{unit.status}</dd>
                </div>

                <div>
                  <dt>Customer</dt>
                  <dd>{unit.currentCustomer ?? "None"}</dd>
                </div>
              </dl>
            </article>
          ))}
        </div>
      </section>
      
    </main>
  )
}

export default App