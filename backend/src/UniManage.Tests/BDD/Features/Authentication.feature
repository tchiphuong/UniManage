Feature: Authentication and Authorization
  As a user
  I want to authenticate and access the system
  So that I can use the application securely

  @auth @smoke
  Scenario: User login with valid credentials
    Given a user exists with username "testuser" and password "Test@12345"
    When I login with username "testuser" and password "Test@12345"
    Then I should receive an authentication token
    And the token should be valid for 24 hours

  @auth @negative
  Scenario: User login with invalid password
    Given a user exists with username "testuser"
    When I login with username "testuser" and password "WrongPassword"
    Then the login should fail
    And I should receive an unauthorized error

  @auth @negative
  Scenario: User login with non-existent username
    When I login with username "nonexistent" and password "AnyPassword"
    Then the login should fail
    And I should receive an unauthorized error

  @auth @validation
  Scenario Outline: Cannot login with missing credentials
    When I try to login with username "<username>" and password "<password>"
    Then the login should fail with validation error

    Examples:
      | username | password    |
      |          | Test@12345  |
      | testuser |             |
      |          |             |

  @auth @token
  Scenario: Access protected endpoint with valid token
    Given I am logged in as "testuser"
    When I access a protected endpoint
    Then I should get a successful response

  @auth @token @negative
  Scenario: Cannot access protected endpoint without token
    Given I am not authenticated
    When I try to access a protected endpoint
    Then I should receive an unauthorized error

  @auth @token @negative
  Scenario: Cannot access protected endpoint with expired token
    Given I have an expired authentication token
    When I try to access a protected endpoint with the expired token
    Then I should receive an unauthorized error

  @auth @refresh
  Scenario: Refresh authentication token
    Given I am logged in with a valid token
    When my token is about to expire
    And I request a token refresh
    Then I should receive a new valid token
    And the old token should be invalidated

  @auth @logout
  Scenario: User logout
    Given I am logged in as "testuser"
    When I logout from the system
    Then my authentication token should be invalidated
    And I should not be able to access protected endpoints

  @auth @permissions
  Scenario: Admin can access admin-only endpoints
    Given I am logged in as an administrator
    When I access an admin-only endpoint
    Then I should get a successful response

  @auth @permissions @negative
  Scenario: Regular user cannot access admin-only endpoints
    Given I am logged in as a regular user
    When I try to access an admin-only endpoint
    Then I should receive a forbidden error

  @auth @security
  Scenario: Password is hashed in database
    Given I create a user with password "PlainTextPassword123"
    When I check the user record in the database
    Then the password should be hashed
    And the password should not be stored in plain text

  @auth @security
  Scenario: Prevent brute force login attempts
    Given a user exists with username "testuser"
    When I make 5 failed login attempts
    Then the account should be temporarily locked
    And further login attempts should be rejected
