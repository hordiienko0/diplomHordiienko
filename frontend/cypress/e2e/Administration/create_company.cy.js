describe('Administartion', () => {
  beforeEach(() => {
    cy.viewport(1920, 1080)
    cy.visit('http://localhost:4200/')
    cy.contains('Sign in').click()


    cy.get('[type="email"]').first().type('admin@radency.com')
    cy.get('[type="password"]').type('admin')


    cy.get('[class="mat-focus-indicator btn mat-flat-button mat-button-base"]').click()
    cy.wait(500)
    cy.get('[class="mat-focus-indicator mat-flat-button mat-button-base alert-button success"]').click()
    cy.wait(500)
  })

  it('Create company', () => {
    cy.contains('Add Company').click()

    cy.wait(500)
    cy.get('[label="Company name"]').type('Test')
    cy.wait(500)

    cy.get('[type="email"]').first().type('1@gmail.com')
    cy.wait(500)

    cy.get('[label="Country"]').type('Test')
    cy.wait(500)

    cy.get('[label="City"]').type('Test')
    cy.wait(500)

    cy.get('[label="Address"]').type('Test')
    cy.wait(500)

    cy.get('[ng-reflect-type="submit"]').first().click()

    cy.get('[class="name"]').should('contain', 'Test')
  })

  it('Company information page', () => {
    // Write test to check company information page for UI layout
    // It has to contain "Information", Team", "Projects" sections, company`s logo, title and id
  })
})
