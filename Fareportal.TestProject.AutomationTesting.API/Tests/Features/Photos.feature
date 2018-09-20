Feature: Photos
	In order to avoid troubles with TestProject API for Photos service and its Subservices
	I want to run these tests

Scenario Outline: Verify_that_Photo_with_title_belongs_to_correct_user_with_email
	Given I have called GET for '/photos'
		And I have received the albumId by Photo title: <PhotoTitle>
	When I call GET for '/albums/{id}' with id provided
		And I get the userId from received album
	When I call GET for '/users/{id}' with id provided
	Then Response result code for Get operation is '200'
		And I see that received user email is equal to: <UserEmail>
	Examples:
	| PhotoTitle      | UserEmail         |
	| ad et natus qui | Sincere@april.biz |

Scenario: Verify_that_image_from_Photo_is_not_corrupted
	Given I have called GET for '/photos/{id}' with id of: 4
	When I get the image of the received Photo
		Then I see that received image matches to expected one