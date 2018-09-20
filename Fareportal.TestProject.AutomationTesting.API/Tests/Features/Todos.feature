Feature: Todos
	In order to avoid troubles with TestProject API for Todos service and its Subservices
	I want to run these tests

Scenario Outline: Verify_that_Post_with_titles_have_correct_user_names
	Given I have called GET for '/users'
		And I have got the userId with name <FirstUser>
		And I have got the userId with name <SecondUser>
	When I call GET for '/todos'
	Then Response result code for Get operation is '200'
		And I see that <FirstUser> has more than 3 completed TODOs than <SecondUser>
	Examples:
	| FirstUser     | SecondUser   |
	| Leanne Graham | Ervin Howell |