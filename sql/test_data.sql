INSERT INTO "Tax" (Id, TaxId, "Index", Percentage, ValidityStartDate, ValidityEndDate) VALUES ('RED-1', 'RED', 1, 10, '2017-01-01T00:00:00', '0001-01-01T00:00:00');
INSERT INTO "Tax" (Id, TaxId, "Index", Percentage, ValidityStartDate, ValidityEndDate) VALUES ('REG-1', 'REG', 1, 20, '2017-01-01T00:00:00', '0001-01-01T00:00:00');

INSERT INTO "Dish" (Id, Name) VALUES('1','Fondue vietnamienne');
INSERT INTO "Dish" (Id, Name) VALUES('B1','Nems');
INSERT INTO "Dish" (Id, Name) VALUES('B2','Beignets de crevettes');
INSERT INTO "Dish" (Id, Name) VALUES('90','Riz cantonais');

INSERT INTO "DishPrice" (DishId, ValidityStartDate, ValidityEndDate, Amount) VALUES('1', '2017-11-01T00:00:00', '0001-01-01T00:00:00', 15.0);
INSERT INTO "DishPrice" (DishId, ValidityStartDate, ValidityEndDate, Amount) VALUES('B1', '2017-11-01T00:00:00', '2017-11-15T23:59:59', 3.0);
INSERT INTO "DishPrice" (DishId, ValidityStartDate, ValidityEndDate, Amount) VALUES('B1', '2017-11-16T00:00:00', '0001-01-01T00:00:00', 3.5);
INSERT INTO "DishPrice" (DishId, ValidityStartDate, ValidityEndDate, Amount) VALUES('B2', '2017-11-01T00:00:00', '2017-11-15T23:59:59', 3.2);
INSERT INTO "DishPrice" (DishId, ValidityStartDate, ValidityEndDate, Amount) VALUES('B2', '2017-11-16T00:00:00', '0001-01-01T00:00:00', 3.8);
INSERT INTO "DishPrice" (DishId, ValidityStartDate, ValidityEndDate, Amount) VALUES('90', '2017-11-01T00:00:00', '0001-01-01T00:00:00', 3.5);

INSERT INTO "Table" (Id) VALUES ('1');
INSERT INTO "Table" (Id) VALUES ('12A');

INSERT INTO "Order" (Id, TakeAway, TableId, NumberOfGuests) VALUES(1, 0, '1', 1);
INSERT INTO "Order" (Id, TakeAway, TableId, NumberOfGuests) VALUES(2, 0, '12A', 2);
INSERT INTO "Order" (Id, TakeAway) VALUES (3, 1);

INSERT INTO "OrderLine" (Id, OrderId, "Index", DishId, Quantity) VALUES('1-1', 1, 1, 'B1',2);
INSERT INTO "OrderLine" (Id, OrderId, "Index", DishId, Quantity) VALUES('1-2', 1, 2, '90',1);
INSERT INTO "OrderLine" (Id, OrderId, "Index", DishId, Quantity) VALUES('2-1', 2, 1, 'B2',1);
INSERT INTO "OrderLine" (Id, OrderId, "Index", DishId, Quantity) VALUES('3-1', 3, 1, '1',1);