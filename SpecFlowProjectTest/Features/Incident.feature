Feature: Incident
	For all incident related operations

@ValidUser
Scenario: Validate the user authentication
	Given  the ServiceNow user with ID "TestUser" and Password "Passw0rd"
	When Validating the user authentication
	Then the response code should be "OK"

@InvalidUser
Scenario: Validate the user authentication
	Given  the ServiceNow user with ID "xyz" and Password "123456"
	When Validating the user authentication
	Then the response code should be "Unauthorized"

@IncidentNumber,@ResponseCode
Scenario: Retireve and Validate the incident specific field details with the given incident ID
	Given User with ID "TestUser" and Password "Passw0rd" is authenticated for the ServiceNow Instance
	When an incident id is shared for the user with number as "INC0000060"
	Then the most recent incident  is as follows
		| attribute    | value      |
		| number       | INC0000060 |
		| ResponseCode | 200        |

@ChildIncidents
Scenario: Retireve and Validate the child_incidents details with the given incident ID
	Given  the user with ID "admin" is authenticated for the ServiceNow Instance
	When a incident id is shared for the user with number as "INC0000060"
	Then the most recent incident for user admin includes:
		| child_incidents | 0             |
	And the response code should be "200"

@ChildIncidents
#Increasing Maintainability 
#Scenario outlines - Group all "And tan incident id is shared for the user with number X" statement into one scenario
Scenario Outline: Retireve and Validate the child_incidents details with the given incident ID
	Given User with ID "TestUser" and Password "Passw0rd" is authenticated for the ServiceNow Instance
	And  an incident id is shared for the user with number as <incidentnumber> USD
	Then response code should be <ResponseCode>
	And child incidents should be <child_incidents>
	
	Examples:
		| incidentnumber | ResponseCode | child_incidents |
		| INC0000060 | 200   | 0           |
		| INC0000061 | 200   | 0           |
		| INC0000062 | 200   | 0           |
