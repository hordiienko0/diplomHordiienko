describe('Login', () => {
  it('Admin logs in', { scrollBehavior: false }, () => {

    cy.viewport(1920, 1080) //screen size
    cy.visit('http://localhost:4200/') //open landing page

    cy.contains('Sign in').click() //find and click button

    //fill fields
    cy.get('[type="email"]').first().type('admin@radency.com')
    cy.get('[type="password"]').type('12345')

    //login and expect admin url
    cy.get('[type="submit"]').click()
    cy.url().should('eq', 'http://localhost:4200/company-list')

  })

  it('Operational manager logs in', () => {
    // Here must be code
  })

  it('Project manager logs in', () => {
    // Here must be code
  })

  it('Main engineer logs in', () => {
    // Here must be code
  })

  it('Foreman logs in', () => {
    // Here must be code
  })
})
