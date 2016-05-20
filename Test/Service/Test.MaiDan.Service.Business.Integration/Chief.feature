Feature: Chief
	In order to present my dishes to customers
	As a restaurant chief
	I want to elaborate the menu

@menu
Scenario: Add a dish to the menu
	Given A dish Fried rice that I want to propose to my customers
	When I add it to the menu
	Then the dish Fried rice can be ordered by the customers

Scenario: Remove a dish from the menu
	Given A dish NG - Nasi gorang in the menu
	When I update the name to Nasi goreng
	Then the dish NG is displayed as Nasi goreng
