BEGIN TRANSACTION;

INSERT INTO "TaxRate" (Id, TaxId, Rate, ValidityStartDate, ValidityEndDate) VALUES ('RED-1', 'RED', 0.10, '2017-01-01 00:00:00', '9999-12-31 23:59:59');
INSERT INTO "TaxRate" (Id, TaxId, Rate, ValidityStartDate, ValidityEndDate) VALUES ('REG-1', 'REG', 0.20, '2017-01-01 00:00:00', '9999-12-31 23:59:59');

INSERT INTO "OrderDish" (Id, Name) VALUES ('1', 'Fondue vietnamienne');
INSERT INTO "OrderDish" (Id, Name) VALUES ('B1', 'Nems');
INSERT INTO "OrderDish" (Id, Name) VALUES ('B2', 'Beignets de crevettes');
INSERT INTO "OrderDish" (Id, Name) VALUES ('90', 'Riz cantonais');
INSERT INTO "OrderDish" (Id, Name) VALUES ('W1', 'Pichet');

INSERT INTO "BillDish" (Id, "Type") VALUES ('1', 'Spécialités');
INSERT INTO "BillDish" (Id, "Type") VALUES ('B1', 'Entrée');
INSERT INTO "BillDish" (Id, "Type") VALUES ('B2', 'Entrée');
INSERT INTO "BillDish" (Id, "Type") VALUES ('90', 'Accompagnement');
INSERT INTO "BillDish" (Id, "Type") VALUES ('W1', 'Alcool');

INSERT INTO "DishPrice" (DishId, ValidityStartDate, ValidityEndDate, Amount) VALUES ('1', '2017-11-01 00:00:00', '9999-12-31 23:59:59', 15.0);
INSERT INTO "DishPrice" (DishId, ValidityStartDate, ValidityEndDate, Amount) VALUES ('B1', '2017-11-01 00:00:00', '2017-11-15 23:59:59', 3.0);
INSERT INTO "DishPrice" (DishId, ValidityStartDate, ValidityEndDate, Amount) VALUES ('B1', '2017-11-16 00:00:00', '9999-12-31 23:59:59', 3.5);
INSERT INTO "DishPrice" (DishId, ValidityStartDate, ValidityEndDate, Amount) VALUES ('B2', '2017-11-01 00:00:00', '2017-11-15 23:59:59', 3.2);
INSERT INTO "DishPrice" (DishId, ValidityStartDate, ValidityEndDate, Amount) VALUES ('B2', '2017-11-16 00:00:00', '9999-12-31 23:59:59', 3.8);
INSERT INTO "DishPrice" (DishId, ValidityStartDate, ValidityEndDate, Amount) VALUES ('90', '2017-11-01 00:00:00', '9999-12-31 23:59:59', 3.5);
INSERT INTO "DishPrice" (DishId, ValidityStartDate, ValidityEndDate, Amount) VALUES ('W1', '2017-11-01 00:00:00', '9999-12-31 23:59:59', 6.5);

INSERT INTO "Table" (Id) VALUES ('1');
INSERT INTO "Table" (Id) VALUES ('12A');

INSERT INTO "Order" (Id, TakeAway, TableId, NumberOfGuests, OrderingDate, Closed) VALUES (1, 0, '1', 1, '2018-05-05 12:05:00', 0);
INSERT INTO "Order" (Id, TakeAway, TableId, NumberOfGuests, OrderingDate, Closed) VALUES (2, 0, '12A', 2, '2018-05-05 12:10:00', 0);
INSERT INTO "Order" (Id, TakeAway, TableId, NumberOfGuests, OrderingDate, Closed) VALUES (3, 1, NULL, 0, '2018-05-05 13:20:00', 0);

INSERT INTO "OrderLine" (OrderId, "Index", DishId, Quantity) VALUES (1, 1, 'B1', 2);
INSERT INTO "OrderLine" (OrderId, "Index", DishId, Quantity) VALUES (1, 2, '90' ,1);
INSERT INTO "OrderLine" (OrderId, "Index", DishId, Quantity) VALUES (2, 1, 'B2', 1);
INSERT INTO "OrderLine" (OrderId, "Index", DishId, Quantity) VALUES (2, 2, 'W1', 1);
INSERT INTO "OrderLine" (OrderId, "Index", DishId, Quantity) VALUES (3, 1, '1', 1);

INSERT INTO "Discount" (Id, ApplicableTaxId, Rate) VALUES ('À emporter', 'RED', 0.10);

INSERT INTO "PaymentMethod" (Id, Name) VALUES ('E', 'Espèces');
INSERT INTO "PaymentMethod" (Id, Name) VALUES ('C', 'Chèque');
INSERT INTO "PaymentMethod" (Id, Name) VALUES ('CB', 'Carte bleue');
INSERT INTO "PaymentMethod" (Id, Name) VALUES ('TR', 'Tickets restaurant');
INSERT INTO "PaymentMethod" (Id, Name) VALUES ('CV', 'Chèques vacances');
INSERT INTO "PaymentMethod" (Id, Name) VALUES ('A', 'Avoir');

INSERT INTO "Day" (Date, Closed) VALUES ('2018-05-05 00:00:00', 0);

COMMIT;