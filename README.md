# Lab_Assignment2_WhistConsoleApp
Welcome to the Whist Point Calculator Console Application. 
This console applications lets the user create a whist game, add rounds and calculate the score.
It keeps track of the games played in a database and lets the user search for the game using page like navigation.
An example of how to use the application could be:
1. __Press '2'__ to create a new Game
2. Input the __Game name__ and __Game Location__
3. Input the __Player names__ of the players
4. __Press 'Enter'__ to start the game
5. __Press '1'__ to add a new round
6. __Press '2'__ to end the game

If the user wants to find a game played, they follow these steps:
1. __Press '1'__ to find a game
2. Input the __Game name__ to show details of
3. __Press Enter__
4. The details are shown

# What Should i do before running it?
This is a test program, so you should create a database with the name WhistDatabase locally 
or modify the connection string inside the source code. If you create the database it is 
necessary to update the database with the current migration and tables.

If you experience any weird erros,
we advise to delete any migration and run these commands in Package Manager Console:
Add-Migration TestCreation
Update-Database 
