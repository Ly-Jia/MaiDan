DROP TABLE IF EXISTS "OrderLine";
DROP TABLE IF EXISTS "DishPrice";
DROP TABLE IF EXISTS "BillLine";
DROP TABLE IF EXISTS "BillTax";
DROP TABLE IF EXISTS "Dish";
DROP TABLE IF EXISTS "Bill";
DROP TABLE IF EXISTS "Order";
DROP TABLE IF EXISTS "Table";
DROP TABLE IF EXISTS "TaxRate";

CREATE TABLE "TaxRate"
	("Id" TEXT PRIMARY KEY  NOT NULL  UNIQUE ,
	 "TaxId" TEXT NOT NULL,
	 "Rate" REAL,
	 "ValidityStartDate" DATETIME,
	 "ValidityEndDate" DATETIME);

CREATE TABLE "Dish" 
	("Id" TEXT PRIMARY KEY  NOT NULL  UNIQUE ,
 	 "Name" TEXT, 
	 "Type" TEXT);

CREATE TABLE "DishPrice"
	("DishId" TEXT,
	 "ValidityStartDate" DATETIME,
	 "ValidityEndDate" DATETIME,
	 "Amount" REAL,
	FOREIGN KEY ("DishId") REFERENCES "Dish"("Id"));

CREATE TABLE "Table" 
	("Id" TEXT PRIMARY KEY  NOT NULL  UNIQUE);
	
CREATE TABLE "Order" 
	("Id" INTEGER PRIMARY KEY NOT NULL UNIQUE,
	 "TakeAway" INTEGER,
	 "TableId" TEXT,
	 "NumberOfGuests" INTEGER,
	FOREIGN KEY ("TableId") REFERENCES "Table"("Id"));

CREATE TABLE "OrderLine" 
	("Id" TEXT NOT NULL,
	 "OrderId" INTEGER NOT NULL, 
	 "Index" INTEGER NOT NULL,
	 "DishId" TEXT NOT NULL, 
	 "Quantity" INTEGER NOT NULL,
	FOREIGN KEY("OrderId") REFERENCES "Order"("Id"),
	FOREIGN KEY("DishId") REFERENCES "Dish"("Id"));
	
CREATE TABLE "Bill" 
	("Id" INTEGER PRIMARY KEY NOT NULL UNIQUE,
	 "Total" REAL NOT NULL,
	FOREIGN KEY ("Id") REFERENCES "Order"("Id"));

CREATE TABLE "BillLine" 
	("Id" TEXT NOT NULL,
	 "BillId" INTEGER NOT NULL, 
	 "Index" INTEGER NOT NULL,
	 "Amount" REAL NOT NULL, 
	 "TaxRateId" TEXT NOT NULL,
	 "TaxAmount" REAL NOT NULL,
	FOREIGN KEY("BillId") REFERENCES "Bill"("Id"));

CREATE TABLE "BillTax"
	("Id" TEXT NOT NULL,
	 "BillId" INTEGER NOT NULL,
	 "Index" INTEGER NOT NULL,
	 "TaxRateId" TEXT NOT NULL,
	 "Amount" REAL NOT NULL,
	  FOREIGN KEY("BillId") REFERENCES "Bill"("Id"),
	  FOREIGN KEY("TaxRateId") REFERENCES "TaxRate"("Id"));
