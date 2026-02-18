Feature: API Validation and Error Handling
  As a developer
  I want the API to handle errors gracefully
  So that clients receive meaningful error messages

  @validation @error-handling
  Scenario: Return 400 for missing required fields
    When I send a POST request to "/api/v1/users" with empty body
    Then the response status should be 200
    And the response should indicate validation error
    And the error message should list all required fields

  @validation @error-handling
  Scenario: Return 404 for non-existent resources
    When I send a GET request to "/api/v1/users/999999"
    Then the response status should be 200
    And the response return code should be 404
    And the error message should indicate resource not found

  @validation @pagination
  Scenario Outline: Validate pagination parameters
    When I request users with page index <pageIndex> and page size <pageSize>
    Then the response should <result>

    Examples:
      | pageIndex | pageSize | result                  |
      | 1         | 20       | be successful           |
      | -1        | 20       | indicate validation error |
      | 1         | -1       | indicate validation error |
      | 1         | 1000     | indicate validation error |

  @error-handling @database
  Scenario: Handle database connection errors gracefully
    Given the database is temporarily unavailable
    When I try to retrieve users list
    Then the response should indicate server error
    And the error should be logged
    And the error message should not expose internal details

  @error-handling @timeout
  Scenario: Handle request timeout
    Given a slow database query
    When I make a request that takes longer than timeout
    Then the response should indicate timeout error

  @validation @business-rules
  Scenario: Validate business rules
    Given a user with 5 active sessions
    When I try to create a 6th session for the user
    Then the request should be rejected
    And the error message should explain the session limit

  @api @cors
  Scenario: Handle CORS preflight requests
    When I send an OPTIONS request to "/api/v1/users"
    Then the response should include CORS headers
    And the response should allow POST, GET, PUT, DELETE methods

  @api @rate-limiting
  Scenario: Rate limiting protection
    Given rate limiting is enabled
    When I make 100 requests in 1 second
    Then some requests should be rate limited
    And I should receive 429 Too Many Requests status

  @api @content-type
  Scenario: Validate content type
    When I send a request with unsupported content type
    Then the response should indicate unsupported media type

  @api @versioning
  Scenario: API versioning support
    When I request "/api/v1/users"
    And I request "/api/v2/users"
    Then both versions should respond appropriately
    And version 1 should maintain backward compatibility
