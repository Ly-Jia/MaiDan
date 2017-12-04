INSERT INTO "Dish" (Id, Name) VALUES('1','Fondue vietnamienne');
INSERT INTO "Dish" (Id, Name) VALUES('B1','Nems');
INSERT INTO "Dish" (Id, Name) VALUES('B2','Beignets de crevettes');
INSERT INTO "Dish" (Id, Name) VALUES('90','Riz cantonais');

INSERT INTO "Table" (Id) VALUES ('1');
INSERT INTO "Table" (Id) VALUES ('12A');

INSERT INTO "Order" (Id, TakeAway, TableId, NumberOfGuests) VALUES(1, 0, '1', 1);
INSERT INTO "Order" (Id, TakeAway, TableId, NumberOfGuests) VALUES(2, 0, '12A', 2);
INSERT INTO "Order" (Id, TakeAway) VALUES (3, 1);

INSERT INTO "OrderLine" (OrderId, DishId, Quantity) VALUES(1,'B1',2);
INSERT INTO "OrderLine" (OrderId, DishId, Quantity) VALUES(1,'90',1);
INSERT INTO "OrderLine" (OrderId, DishId, Quantity) VALUES(2,'B2',1);
INSERT INTO "OrderLine" (OrderId, DishId, Quantity) VALUES(3,'1',1);