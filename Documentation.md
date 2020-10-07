# Documentation
This game primarily used numbers as input at screens.
When needing to make a choice, the player will be prompted to provide a number input corresponding to a choice.
_________________

### class Program
Variable | Use
---------|---------
Game game | Starts a new instance of Game

Function | Parameters | Use
:--------|:-----------|:----
static void Main | string[] args | Main function of the program. Used to run the game
__________________

### enum GameState
        
|Options
|:----
|MainMenu
|CharacterCreation
|Game
|GameOver

This enum is used to identify which gamestate we are in.
__________________

### class Game
Variable | Use
:--------|:---
bool _gameOver | Determines when we break out of Update()
GameState _gameState | Determines which part of the update loop will be run
Player _player | Stores a reference to the player
Enemy[] _enemies | Array of enemies to fight
int _round | Stores which round we are on
string _savePath | Path to the save file

Function | Parameters | Use
:--------|:-----------|:----
public void Run | N/A | Contains Start, End, and the main Update loop
private void Start | N/A | Contains all logic to be run before the game begins
private void Update() | N/A | Runs in a loop. Used for all game logic
private void End() | N/A | Runs when the Update loop breaks before the program terminates
| |
private void Save | Player player | Used to save the game to a file
private void Load | N/A | Used to load the game from a file
private void DoShop | Player player | Opens an instance of the shop and allows the player to purchase items
private void DoBattle | Player player, Actor enemy | Begins a battle between the player and an Actor opponent
private void PressAnyKeyToContinue | N/A | Helper function to streamline waiting

_________________________

### static class AsciiArt
Function | Parameters | Use
:--------|:-----------|:----
public static void DrawMainMenu | N/A | Writes the main menu ascii art to the console
______________________

### static class Input
Static input class to be used anywhere input is needed. 
Was intended to be much more robust, but was pared down towards the end for scope.

Function | Parameters | Use
:--------|:-----------|:----
public static char GetSelection | int options, bool allowQuit = false | Get the user's selection. Takes an int to give the number of options available, and a bool to allow the user to send a quit signal
______________
### class Item
Used to store information about a given item

Variable | Use
:--------|:---
public string _name | Stores the name of the Item
public int _goldValue | Stores the gold value of the Item for use in the shop
int _buff | How much to buff an attribute by
string _buffMessage | Message to send when the item is used

Function | Parameters | Use
:--------|:-----------|:----
public Item | string name, int buff, string buffMessage, int goldValue | Constructor. Used to create a new Item
public string[] GetRawStats | N/A | Returns the Item's variables as a string array for use in saving to a file
public string GetStoreString | N/A | Generates a string to display in the store based on the item's stats
public int ApplyBuff | N/A | Displays _buffMessage and returns _buff to be used in augmenting a stat
___________________

### class Inventory
Used to manage the inventory of anything that needs to store things of type Item

Variable | Use
:--------|:---
Item[] _inventory | Array of Items to store the goods

Function | Parameters | Use
:--------|:-----------|:----
public Inventory | int inventorySize | Constructor. Used to create a new Inventory
public void PrintContents | N/A | Prints the contents of the inventory to the console
public void SetItemAtIndex | int index, Item item | Used to set an item in _inventory at a specified index
public Item[] GetContents | N/A | Returns the contents of _inventory as an array of type Item
public void RemoveItem | int index | Deletes an item from _inventory at a specified index
______________

### class Shop
Used to create a shop for all your item needs

Variable | Use
:--------|:---
public Inventory _inventory | Stores the inventory of the shop

Function | Parameters | Use
:--------|:-----------|:----
public Shop | N/A | Constructor. Initializes _inventory and sets its contents
public bool SellItem | int index, Player player | Attempts to sell an item from the shop's inventory to a player
_______________
### abstract class Actor
Base class for Player and Enemy. This class contains all shared functionality between the two and allows us to pass any enemy into Game.DoBattle() with polymorphism.

Variable | Use
:--------|:---
public string _name | Stores the name of the Actor
protected int _maxHealth | Stores the maximum health value
protected int _health | Stores the actual health value
protected int _damage | Stores how much damage the Actor deals

Function | Parameters | Use
:--------|:-----------|:----
public virtual void Attack | Actor enemy | Attack another Actor. This function calls the given actor's TakeDamage function and gives it a randomized damage based on _damage and an allowed deviation
public virtual void PrintStats | N/A | Prints the Actor's stats
public virtual int TakeDamage | int damage | Reduces the actor's health by a given amount, not allowing it to fall below zero. Returns the damage dealt.
public virtual bool IsAlive | N/A | Returns whether the Actor is considered alive
protected virtual int GetRandomDamage | int deviation | Used by this and derived classes to calculate a random damage based on a deviation
________________________

### class Player : Actor
Derives from Actor. Used to represent the player.

Variable | Use
:--------|:---
int _armor | Stores the player's armor for use in damage reduction
int _gold | Stores the player's gold for use in the shop

Function | Parameters | Use
:--------|:-----------|:----
public Player | N/A | Base Constructor. Initializes a basic new player.
public Player | string name, int maxHealth | Overloaded Constructor. Initializes a new player with a name and maximum health.
public override int TakeDamage | int damage | Overrides Actor.TakeDamage to allow for armor and damage reduction
public override void PrintStats | N/A | Overrides Actor.PrintStats to add gold.
public string[] GetRawVariables | N/A | Gets the player's variables for use in saving to a file
public void SetRawVariables | string[] variables | Sets the player's variables based on a string array for use in loading
public void AddGold | int amount | Give the player an amount of gold.
public bool BuyItem | Item item | Attempts to purchase an item from the shop
public void UseItem | int inventorySlot | Takes an item from the player's inventory and applies it to their stats
_______________________

### class Enemy : Actor
Derives from Actor. Used to represent enemies.

Function | Parameters | Use
:--------|:-----------|:----
public Enemy | N/A | Constructor. Initializes a basic enemy.
public Enemy | string name, int maxHealth, int damage | Overloaded Constructor. Initializes a new enemy with a name, maximum health, and given damage.

