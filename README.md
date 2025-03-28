# Zuul

A !boring textadventure.

## How to play

Install the latest [dotnet](https://dotnet.microsoft.com/en-us/download) or latest LTS version.

Open this directory (with the Zuul.csproj file) in the terminal and type:

```
dotnet run
```

# How do you play the game

The main goal is the stay alive and find the endroom that you can open with the end-key that you have to find first.

this is a short tutorial on how to play the game.

You can type:
```
go *direction*
```
The direction is: north, east, south and west.
Use the key words to move between rooms.

If you want to look where you are or what items are in the current room, type:
```
look
```

If you see an item you want to pick up, type:
```
take *itemname*
```
The itemname is the same as that was printed by the look command

You also want to use the item that you have in your inventory.
Then type:
```
use *itemname*
```

Some items need more information for example a key. Then type this:
```
use key north
```
In this example there is a locked door in the north with this command you opened the door and now you can go in the room.

You also want to see how many health you have left, then type:
```
status
```

This is about everything you should know to play the game. Good luck!
