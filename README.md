# Selenium-xUnit-Blazor-Server-App
Selenium xUnit Blazor Server App
# BlazorAppTestAutomation

A **Blazor Server application** with country management features and automated UI tests using **xUnit** and **Selenium**.

---

## Features

### Blazor App
- **Country List**: View all countries in a table.  
- **Add Country**: Add a new country.  
- **Edit Country**: Edit existing country details inline.  
- **Delete Country**: Delete a country with confirmation.  
- **Async rendering** using Blazor Server and Entity Framework Core.
- **"Ctrl + F5 (Run Blazor App) then run Test or Debug xUnit"**

### Automated Tests
- **xUnit + Selenium** tests for UI automation.  
- Tests cover:
  - Navigation to pages
  - Element existence verification
  - Add/Edit/Delete CRUD operations
- Robust handling of **Blazor asynchronous rendering**.
- Prevents **StaleElementReferenceException** by re-querying DOM elements.
