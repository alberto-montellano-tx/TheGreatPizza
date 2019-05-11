# TheGreatPizza
An application that manages a Pizza menu for Test.

Steps to run the project: 
1. Re-Create the SQL server database, DB name = 'PizzaExample'. Execute script 
2. Verify the ports your local apps (Client and API) are using to run. Edit Web.config at \PizzaExample\PizzaMenu\PizzaClient\Web.config to set the correct port number where API is running: <add key="ServiceUrl" value="http://localhost:49789/"></add>



All the requirements have been accomplished. We are using the term 'Ingredient' instead of 'Topping'.

The API should be able to do the following:
1.	getPizzas - DONE
2.	getpizza - DONE
3.	GetToppings - DONE
4.	AddTopping - DONE
5.	DeleteTopping - DONE
6.	DeletePizza - DONE
7.	addPizza - DONE
8.	addToppingToPizza - DONE
9.	getToppingsForPizza - DONE

The UI should display the results of using the API 
1.	List of Pizzas - DONE
2.	A Pizza with the toppings listed  - DONE
3.	Ability to add, and delete a topping from a Pizza - DONE
4.	A list of available toppings - DONE
5.	Ability to add, and delete a topping from available toppings list - DONE

Technologies used:
DataStorage: SQL Server, Entity Framework
