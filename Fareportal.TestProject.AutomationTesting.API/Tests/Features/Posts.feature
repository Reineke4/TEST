Feature: Posts
	In order to avoid troubles with TestProject API for Posts service and its Subservices
	I want to run these tests

Scenario: Verify_that_POST_for_'/posts'_with_valid_data_add_the_post_to_the_system
	Given I have generated the valid Post
	When I call POST for '/posts' with data generated
	Then Response result code for Post operation is '201'

Scenario: Verify_that_PUT_for_'/posts/{id}'_with_valid_data_update_the_post_in_the_system
	Given I have generated the valid Post
		And I have called POST for '/posts' with data generated
		And I have generated the valid Post again
	When I call PUT for '/posts/{id}' with data generated and id provided
	Then Response result code for Put operation is '204'

Scenario: Verify_that_DELETE_for_'/posts/{id}'_delete_the_post_from_the_system
	Given I have generated the valid Post
		And I have called POST for '/posts' with data generated
	When I call Delete for '/posts/{id}' with id provided
	Then Response result code for Delete operation is '200'


Scenario Outline: Verify_that_Post_with_titles_have_correct_user_names
	Given I have called GET for '/posts'
		And I have received the userId by Post title: <Title>
	When I call GET for '/users/{id}' with id provided
	Then Response result code for Get operation is '200'
		And I see that received user name is equal to: <UserName>
	Examples:
	| Title                                       | UserName         |
	| eos dolorem iste accusantium est eaque quam | Patricia Lebsack |
