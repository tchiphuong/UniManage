Feature: User Management
  As a system administrator
  I want to manage users in the system
  So that I can control who has access to the application

  Background:
    Given the API is running
    And I am authenticated as an administrator

  @smoke @users
  Scenario: Create a new user successfully
    Given I have valid user information
      | Field       | Value              |
      | Username    | john.doe           |
      | DisplayName | John Doe           |
      | Email       | john.doe@example.com |
      | Password    | SecurePass123      |
    When I send a POST request to create the user
    Then the response status should be 200
    And the response should contain a user ID
    And the user should be created in the database

  @smoke @users @validation
  Scenario: Cannot create user with duplicate username
    Given a user with username "existing.user" already exists
    When I try to create another user with username "existing.user"
    Then the response status should be 200
    And the response should indicate validation error
    And the error message should contain "username"
    And the error message should contain "already"

  @users @validation
  Scenario Outline: Cannot create user with invalid email
    Given I have user information with email "<email>"
    When I send a POST request to create the user
    Then the response status should be 200
    And the response should indicate validation error
    And the error message should contain "email"

    Examples:
      | email           |
      | invalid         |
      | invalid@        |
      | @invalid.com    |
      | invalid@.com    |

  @users @validation
  Scenario: Cannot create user with weak password
    Given I have user information with password "123"
    When I send a POST request to create the user
    Then the response status should be 200
    And the response should indicate validation error
    And the error message should contain "password"

  @users
  Scenario: Retrieve user list with pagination
    Given there are 25 users in the system
    When I request users list with page 1 and page size 10
    Then the response status should be 200
    And the response should contain 10 users
    And the pagination info should show page 1 of 3
    And the total items should be 25

  @users
  Scenario: Search users by keyword
    Given the following users exist in the system:
      | Username    | DisplayName | Email                |
      | john.doe    | John Doe    | john@example.com     |
      | jane.smith  | Jane Smith  | jane@example.com     |
      | bob.admin   | Bob Admin   | bob@example.com      |
    When I search for users with keyword "john"
    Then the response status should be 200
    And the response should contain only users matching "john"

  @users
  Scenario: Update user information
    Given a user with username "testuser" exists
    When I update the user's display name to "Updated Name"
    And I update the user's email to "updated@example.com"
    Then the response status should be 200
    And the user's information should be updated in the database

  @users
  Scenario: Delete user
    Given a user with username "todelete" exists
    When I send a DELETE request for that user
    Then the response status should be 200
    And the user should no longer exist in the database

  @users @negative
  Scenario: Cannot delete non-existent user
    When I try to delete a user with ID 999999
    Then the response status should be 200
    And the response should indicate user not found

  @users @workflow
  Scenario: Complete user lifecycle
    # Create
    Given I have valid user information
    When I create a new user
    Then the user should be created successfully
    
    # Read
    When I retrieve the user by ID
    Then I should see the user's information
    
    # Update
    When I update the user's information
    Then the user's information should be updated
    
    # Delete
    When I delete the user
    Then the user should be removed from the system
    
    # Verify deletion
    When I try to retrieve the deleted user
    Then the user should not be found

  @users @sorting
  Scenario: Sort users by username ascending
    Given multiple users exist with different usernames
    When I request users sorted by username in ascending order
    Then the users should be returned in alphabetical order by username

  @users @status
  Scenario Outline: Filter users by status
    Given users exist with different statuses
    When I filter users by status "<status>"
    Then only users with status "<status>" should be returned

    Examples:
      | status   |
      | active   |
      | inactive |

  @users @performance @slow
  Scenario: Handle large user list efficiently
    Given there are 1000 users in the system
    When I request the first page of users
    Then the response should be returned within 2 seconds
    And the response should contain paginated results
