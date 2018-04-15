INSERT INTO "TaxRate" (Id, TaxId, Rate, ValidityStartDate, ValidityEndDate) VALUES ('RED-1', 'RED', 10, '2017-01-01T00:00:00', '9999-12-31T23:59:59');
INSERT INTO "TaxRate" (Id, TaxId, Rate, ValidityStartDate, ValidityEndDate) VALUES ('REG-1', 'REG', 20, '2017-01-01T00:00:00', '9999-12-31T23:59:59');

INSERT INTO "Dish" (Id, Name, "Type") VALUES('1','Fondue vietnamienne', 'Spécialités');
INSERT INTO "Dish" (Id, Name, "Type") VALUES('B1','Nems', 'Entrée');
INSERT INTO "Dish" (Id, Name, "Type") VALUES('B2','Beignets de crevettes', 'Entrée');
INSERT INTO "Dish" (Id, Name, "Type") VALUES('90','Riz cantonais', 'Accompagnement');
INSERT INTO "Dish" (Id, Name, "Type") VALUES('W1','Pichet', 'Alcool');

INSERT INTO "DishPrice" (DishId, ValidityStartDate, ValidityEndDate, Amount) VALUES('1', '2017-11-01T00:00:00', '9999-12-31T23:59:59', 15.0);
INSERT INTO "DishPrice" (DishId, ValidityStartDate, ValidityEndDate, Amount) VALUES('B1', '2017-11-01T00:00:00', '2017-11-15T23:59:59', 3.0);
INSERT INTO "DishPrice" (DishId, ValidityStartDate, ValidityEndDate, Amount) VALUES('B1', '2017-11-16T00:00:00', '9999-12-31T23:59:59', 3.5);
INSERT INTO "DishPrice" (DishId, ValidityStartDate, ValidityEndDate, Amount) VALUES('B2', '2017-11-01T00:00:00', '2017-11-15T23:59:59', 3.2);
INSERT INTO "DishPrice" (DishId, ValidityStartDate, ValidityEndDate, Amount) VALUES('B2', '2017-11-16T00:00:00', '9999-12-31T23:59:59', 3.8);
INSERT INTO "DishPrice" (DishId, ValidityStartDate, ValidityEndDate, Amount) VALUES('90', '2017-11-01T00:00:00', '9999-12-31T23:59:59', 3.5);
INSERT INTO "DishPrice" (DishId, ValidityStartDate, ValidityEndDate, Amount) VALUES('W1', '2017-11-01T00:00:00', '9999-12-31T23:59:59', 6.5);

INSERT INTO "Table" (Id) VALUES ('1');
INSERT INTO "Table" (Id) VALUES ('12A');

INSERT INTO "Order" (Id, TakeAway, TableId, NumberOfGuests) VALUES(1, 0, '1', 1);
INSERT INTO "Order" (Id, TakeAway, TableId, NumberOfGuests) VALUES(2, 0, '12A', 2);
INSERT INTO "Order" (Id, TakeAway) VALUES (3, 1);

INSERT INTO "OrderLine" (Id, OrderId, "Index", DishId, Quantity) VALUES('1-1', 1, 1, 'B1',2);
INSERT INTO "OrderLine" (Id, OrderId, "Index", DishId, Quantity) VALUES('1-2', 1, 2, '90',1);
INSERT INTO "OrderLine" (Id, OrderId, "Index", DishId, Quantity) VALUES('2-1', 2, 1, 'B2',1);
INSERT INTO "OrderLine" (Id, OrderId, "Index", DishId, Quantity) VALUES('2-2', 2, 2, 'W1',1);
INSERT INTO "OrderLine" (Id, OrderId, "Index", DishId, Quantity) VALUES('3-1', 3, 1, '1',1);