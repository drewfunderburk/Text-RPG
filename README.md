# Welcome to The Game!
This is about as generic as it gets, folks! This is a text based console game meant to show an understanding of the C# programming language.

It is played simply by entering numbers based on an on-screen prompt.

### How to get The Game
- Download the latest release
- Unzip the file into its own folder
- Run TextRPG.exe

### Main Menu
Here you will be given three options:
* Continue
* New Game
* Quit

##### Continue
This option will only work if a save file is found from a previous game. 
If one is found, all of the player's variables as well as the current round number will be loaded back into their respective places.

##### New Game
This option will send the player to character creation where they will be asked for their name.
This could easily be expanded upon in the future to include a combat class or any number of other customizing features.

##### Quit
Quits the game

### Character Creation
This is where the player object is created and is the first stop after starting a new game.
The player currently can only set their name, but there is room for just about any customization in the future.

### Gameplay
The Game sees the player moving between a shop and a series of enemies to fight. 

##### Shop
At the shop, the player may purchase items to help them in their battles ahead.

The shop sells:

Name | Price | Description
-----|-------|------------
Health Potion | 20 gp | Restores health to the user
Armor Potion | 40gp | Increases the player's armor
Damage Potion | 30gp | Increases the player's damage

##### Battle
There are 5 scaling enemies for the player to fight. As the player defeats each one, they have a chance to drop a small amount of gold.

During battle, the player will have the option to:
* **Attack**: Attacking will attempt to deal damage to the enemy based on the player's damage stat. This can be increased with a damage potion.
* **Use item**: Using an item from the player's inventory will increase a stat based on the type of item.

After the player has taken their turn, the enemy will attack. If the player's health falls to zero, the game is lost. 
If the enemy's health falls to zero, it is defeated and the player will move on to the next round.
At 5 rounds, the game is completed.