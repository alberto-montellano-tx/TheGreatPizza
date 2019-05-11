CREATE TABLE Pizzas (
    Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Name varchar(255) NOT NULL
);

CREATE TABLE Ingredients (
    Id int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    Name varchar(255) NOT NULL
);

CREATE TABLE PizzasIngredients (
    PizzaId int FOREIGN KEY REFERENCES Pizzas(Id),
	IngredientId int FOREIGN KEY REFERENCES Ingredients(Id),
	CONSTRAINT PK_PizzasIngredients PRIMARY KEY (PizzaId,IngredientId)
);

INSERT INTO Pizzas VALUES ( 'Pepperoni')
INSERT INTO Pizzas VALUES ('Chesse and Ham')


INSERT INTO Ingredients VALUES ( 'Chesse')
INSERT INTO Ingredients VALUES ( 'Ham')
INSERT INTO Ingredients VALUES ('Tomato Sauce')
INSERT INTO Ingredients VALUES ('Bacon')

INSERT INTO PizzasIngredients VALUES (1,1)
INSERT INTO PizzasIngredients VALUES (1,2)
INSERT INTO PizzasIngredients VALUES (1,3)
INSERT INTO PizzasIngredients VALUES (2,1)
INSERT INTO PizzasIngredients VALUES (2,2)
INSERT INTO PizzasIngredients VALUES (2,4)