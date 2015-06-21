Feature: OrderBook (orders persistance)
	In order to not lose my orders
	As a waiter/waitress
	I want to store my orders


Scenario: Taking an order
	Given an order
	When I take it
	Then I can keep it in my orderbook 

Scenario: Consulting an order
	Given an order in my orderbook
	When I search it
	Then I can consult the order's details
