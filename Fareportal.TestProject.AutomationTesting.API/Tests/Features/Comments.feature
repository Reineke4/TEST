Feature: Comments
	In order to avoid troubles with TestProject API for Comments service and its Subservices
	I want to run these tests

Scenario Outline: Verify_that_Comment_with_body_have_correct_user_email
	When I call GET for '/comments'
	Then Response result code for Get operation is '200'
		And I see that user who left a comment with text: <CommentBody>, email is <UserEmail>
	Examples:
	| CommentBody   | UserEmail       |
	| ipsum dolorem | Marcia@name.biz |