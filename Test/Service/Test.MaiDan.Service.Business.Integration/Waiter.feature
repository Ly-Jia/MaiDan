Feature: Waiter
	In order to serve customers
	As a waiter/waitress
	I want to take or modify my orders

@waiter
Scenario: Taking an order
	Given an order
	When I take it
	Then I can keep it in my orderbook 

@waiter
Scenario: Consulting an order
	Given an order in my orderbook
	When I search it
	Then I can consult the order's details

@waiter
Scenario: Modifying an order
	Given an order in my orderbook with
	| Quantity | DishId   |
	| 1        | Coffee |
	When I modify it with 2 Coffee 
	Then this order should be
	| Quantity | DishId   |
	| 2        | Coffee |