Feature: Chief
	In order to present my dishes to customers
	As a restaurant chief
	I want to elaborate the menu

@menu
Scenario: Add a dish to the menu
	Given A dish Fried rice that I want to propose to my customers
	When I add it to the menu
	Then the dish Fried rice can be ordered by the customers
