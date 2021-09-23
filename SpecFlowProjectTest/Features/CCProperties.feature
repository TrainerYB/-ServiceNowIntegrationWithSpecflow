Feature: Credit Card Properties
As a new credit card holder
I want my credit card attributes to be correctly represented

#Scenario without setting up anything in VS - No background will be given
#Creating first scenario - 1st scenario and creating blank functions
#Writing test automation code - Writing code for 1st scenario
#Running and debugging test scenarios
Scenario: Credit card should be active after creation
	Given I have a credit card
	And the credit card limit is 50000 USD
	Then credit card should be active

#Adding additional scenario - Scenario 1, 2 & 3 - Duplicate Given statements will be written deliberately
#Participants will write scenario
Scenario: Initial credit card's balance should be equal to the linit
	Given I have a credit card
	And the credit card limit is 70000 USD
	Then balance should be 70000

Scenario: Initial credit card outstanding amount should be 0
	Given I have a credit card
	And the credit card limit is 90000 USD
	Then outstanding amount should be 0

#Reviewing maintainability- show that the duplicate steps are of similar kind
#Increasing Maintainability -
#SHARED STEPS and Parameters - Merge the code for duplicate steps and create parameters
#Using AND keyword - Group then statements using AND keyword into a single scenario
#Scenario outlines - Group all "And the credit card limit is X USD" statement into one scenario
#excel - pending
Scenario Outline: Combined Scenario
	Given I have a credit card
	And the credit card limit is <limit> USD
	Then credit card should be active
	And balance should be <balance>
	And outstanding amount should be <outstanding>

	Examples:
		| limit | balance | outstanding |
		| 50000 | 50000   | 0           |
		| 70000 | 70000   | 0           |
		| 90000 | 90000   | 0           |

#Introducing Scenario Backgrounds - Move Given statement from each scenario to BACKGROUND - Debug for multiple scenarios in one go
#Step Argument Conversion -
#Implicit -
#string to string - already implemented - just explain
#string to double - already implemented - just explain
#string to bool - We'll create scenario below and show bool
Scenario: Card is blocked
	Given I have a credit card
	And the credit card limit is 50000 USD
	When credit card is blocked
	Then credit card's IsActive flag should be false

#string to enum conversion
Scenario: Extra limit for Gold member
	Given I have a credit card
	And the credit card limit is 50000 USD
	And the card category is Gold
	Then total limit should be 52500

#Using Data Tables in Scenario Steps - Create below scenario using AND statement in Given
Scenario: Extra 10% limit
	Given I have a credit card
	And the credit card limit is 50000 USD
	And extra offer on limit is 10%
	Then total limit should be 55000

#Use DataTable to rewrite above Scenario
#Use Linq to read data
#Create instance
#Dynamic instance
Scenario: Extra 10% limit usig attributes
	Given I have a credit card
	And limit and offer is as follows
		| attribute | value |
		| limit     | 50000 |
		| offer     | 10    |
	Then total limit should be 55000

#Multi-column Step Table Data -
#Weakly typed - show failure scenario with case difference
#Strongly typed - CreateSet
#Dynamic - Create Dynamic set
Scenario: Bill multiple transactions with cashback
	Given I have a credit card
	And the credit card limit is 50000 USD
	When a transaction with below attributes is billed
		| ItemPrice | CashBackPercentage | MaxCashBack |
		| 200       | 10                 | 15          |
		| 300       | 20                 | 50          |
		| 400       | 25                 | 70          |
	Then outstanding amount should be 765

#185+250+330=765
#Custom Transformation - 3 days ago
Scenario: Card is pastDue if bill date is passed
	Given I have a credit card
	And the credit card limit is 50000 USD
	When the credit card due date has passed 3 days ago
	Then credit card's status should be past due

#Bill multiple transactions using automatic custom conversion
Scenario: Bill multiple transactions with cashback using automatic custom conversion
	Given I have a credit card
	And the credit card limit is 50000 USD
	When a transaction with below attributes is billed - use automatic custom conversion
		| ItemPrice | CashBackPercentage | MaxCashBack |
		| 200       | 10                 | 15          |
		| 300       | 20                 | 50          |
		| 400       | 25                 | 70          |
	Then outstanding amount should be 765
#Context Injection - Pending