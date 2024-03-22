# Blade & Card: Secret of the Land #

## Summary ##

**A paragraph-length pitch for your game.** 

## Gameplay Explanation ##
- `W` or `up arrow` key - Move up  
- `A` or `left arrow` key - Move left  
- `S` or `down arrow` key - Move down  
- `D` or `right arrow` key - Move right  
- `E` - Display inventory window  
  - Shows inventory along with a description of what each object is and the effect the object has  
- `H` - Displays window summarizing button mappings
- `tab` - Display current game stats
- Shows health, attack power, speed, armor, current level, and XP
- `Left-click` - Swing sword to attack monsters
  - Does damage in direction the player is facing
  - Must face monster in order to do damage to it

You as the player must navigate through the map and defeat the monsters that attack you. While navigating through the map, you should try to look for treasure chests. These chests contain items that will help you defeat enemies such as health potions, attack potions, defense potions, and swords of varying attack power. You can access these items by pressing `E` on the keyboard. You can look at the bottom left corner of the screen to see your health. If you get killed, selecting `Try Again` will restart the game, and you will start with the stats that you left off on. 

When you find an NPC, `Left-click` on them to begin your interaction. The NPCs give you the option to play a card game against them in order to gain a future combative advantage. ** Insert how to play card game **.


**Add it here if you did work that should be factored into your grade but does not fit easily into the proscribed roles! Please include links to resources and descriptions of game-related material that does not fit into roles here.**

# Main Roles #

Your goal is to relate the work of your role and sub-role in terms of the content of the course. Please look at the role sections below for specific instructions for each role.

Below is a template for you to highlight items of your work. These provide the evidence needed for your work to be evaluated. Try to have at least four such descriptions. They will be assessed on the quality of the underlying system and how they are linked to course content. 

*Short Description* - Long description of your work item that includes how it is relevant to topics discussed in class. [link to evidence in your repository](https://github.com/dr-jam/ECS189L/edit/project-description/ProjectDocumentTemplate.md)

Here is an example:  
*Procedural Terrain* - The game's background consists of procedurally generated terrain produced with Perlin noise. The game can modify this terrain at run-time via a call to its script methods. The intent is to allow the player to modify the terrain. This system is based on the component design pattern and the procedural content generation portions of the course. [The PCG terrain generation script](https://github.com/dr-jam/CameraControlExercise/blob/513b927e87fc686fe627bf7d4ff6ff841cf34e9f/Obscura/Assets/Scripts/TerrainGenerator.cs#L6).

You should replay any **bold text** with your relevant information. Liberally use the template when necessary and appropriate.

## Producer - Chelsea Huffman

**Describe the steps you took in your role as producer. Typical items include group scheduling mechanisms, links to meeting notes, descriptions of team logistics problems with their resolution, project organization tools (e.g., timelines, dependency/task tracking, Gantt charts, etc.), and repository management methodology.**

## User Interface and Input - Yifan Cui

**Describe your user interface and how it relates to gameplay. This can be done via the template.**
**Describe the default input configuration.**

**Add an entry for each platform or input style your project supports.**

## Movement/Physics - Yifan Cui

**Describe the basics of movement and physics in your game. Is it the standard physics model? What did you change or modify? Did you make your movement scripts that do not use the physics system?**

## Animation and Visuals - Jinzhuang Li

**List your assets, including their sources and licenses.**

**Describe how your work intersects with game feel, graphic design, and world-building. Include your visual style guide if one exists.**

## Game Logic - Jinzhuang Li

**Document the game states and game data you managed and the design patterns you used to complete your task.**

### Card Game Logic
**General Design**

The card battle in our game is like Slay the Spire and Inscryption. I want the player have a trade-off every time in player round. In order to do this, I limited the resource of player in each round. The player can only get either energy or cards. To help player make choice, the enemy action in next round will  be displayed on the head of screen so player will not dropped into a boring loop of just restore each resouce one time every two round.

Some card can add shields of player and defend some demage in thie round, but the shield can only exist in one round. Therefore the right time of defend and always remember to keep some defend card on hand is important.

**Cards**

A card game most have various cards. Therefore a pattern to make adding different cards in future is my goal. I used scriable object so I can easily create new card perfebs. In order to implement the effects in game, I give each type of card an public integer field `ID`. This is used by `CardBattleManager` to decide each card effects. Also, IDs allow the game manager store the piles into a integer list. When player is using a card, the card will give `CardGameManager` an ID and destroy itself. The `CardGameManager` will send this ID to `CardBattleManager` to process effect and `HandCardManager` to remove the card from hand. The Deck of player is also a integer list that will not be destroy when loading scene. Every time card battle start, the `drawPile` will get a copy of player deck. Winning a card battle will give player a powerful card in deck so the player can keep their cards in RPG world and become stronger after each battle.

**Hand Cards UI**

The hand cards in our games are game objects. I used `OnMouseEnter()` to implement the effect of lifting the card when player's mouse is on the card. If the mouse is stay one the card for a while, the player can also see a description of that card. This is implemented by using a counter to control the game object of that banner.

**Card Battle Logic**

In our card game there are 6 game stages. `BeforePlayerRound`, `PlayerRound`, `AfterPlayerRound`, `BeforeEnemyRound`, `EnemyRound`, and `AfterEnemyRound`. Different stage have diffent events. In `BeforePlayerRound` the player can choose to get card or get energy and hand cards are not avaliable in this stage. In `PlayerRound` the player can use hand cards to fight with enemy or just prepare for next round. `AfterPlayerRound` will update the hand cards UI for player. `BeforeEnemyRound` has no function so far. `EnemyRound` is where enemy action will process. In `AfterEnemyRound` the enemy will decide what is the next action in next enemy round so that the player can see the enemy action when player making choise in `BeforePlayerRound`.

**Enemy**

The enemies actions in card battle are following their pattern and keep looping until the battle end. For example, if an enemy has pattern "Attack, Defend, Defend", then the enemy will attack in first round, defend in second round, and defend in third round, and keep looping. If the player get the pattern of an enemy, it is possible to predict the action type before two or three round. However, the amount of damage or shield the enemy will attack or defend is still randomized. This number can only be seen on UI before the player round.

# Sub-Roles

## Audio

**List your assets, including their sources and licenses.**

**Describe the implementation of your audio system.**

**Document the sound style.** 

## Gameplay Testing - Yifan Cui

**Add a link to the full results of your gameplay tests.**

**Summarize the key findings from your gameplay tests.**

## Narrative Design - tbd

**Document how the narrative is present in the game via assets, gameplay systems, and gameplay.** 

## Press Kit and Trailer - Chelsea Huffman

[Press Kit](PressKit.md) and [Trailer](https://youtu.be/sDAz_RW6fDw)

### Trailer  
The thumbnail for this trailer includes the title of the game along with an image from the game. I made this choice so people can become curious on what the game entails, and they would already gain insight into the game. In the trailer for this game, I wanted to advertise all the different features the game has that makes it fun to play. The first few clips show off different parts of the world map, so the player can see what there is to explore. This was included with the intention of providing the watcher with background information about the game's setting and increase their curiosity. Next, I displayed the treasure chests. I put this part before fighting off the monsters because the elements found in the treasure chest will help the player defeat the monsters. Then, I included a sneak peak of NPC dialogue which leads to a card game. This was included because the card game is also a main feature of this game; Therefore, it's important that the player knows how to reach it and get a peak of what it looks like. The final clip was the player defeating the monsters and leveling up. It was important to show what the game looks like when it is played, and it also displays what you can gain when you level up. I found the UI of our game to be very visually appealing, so I wanted to show off the different UI elements that appear throughout the trailer. The text I added to the trailer clips were to provide the watcher with information on what part of the game they were watching. I chose a font and transition that I felt matched up the overall theme and aesthetic of our game.

### Press Kit  
The press kit was created as a Markdown file, and I used the outline of the press kits from [dopresskit](https://dopresskit.com/) to help me compile all the information it needed. The first thing that the user will see when they open the press kit is an image that provides some insight into the game, like what a part of the land looks like and what a monster looks like. Then, upon scrolling, they will see a brief description about the game and the purpose of it. I wanted to set the scene for the user on what they could be playing if they continued with downloading the game. There is also a section about the current features of the game. I included this because I wanted the player to know what to expect when they play the game. The trailer appears next on the page so the user can gain additional knowledge of the game and understand how it is meant to be played. Following the trailer are screenshots that I believed were useful to share, and an "About the Team" section that contains everyones roles and subroles.

**Screenshots**
I wanted to use these screenshots to showcase our game's UI. First, I chose to display a part of the world map so the player knows what the world looks like. Also, since we did not have time to include a mini-map in our game, I believed this would be a good way for the player to see what the map looks like and what they can explore. A few screenshots work to share what some elements of the world look like. For example, I included a screenshot of the monsters that attack the player, the player talking to an NPC, and the player standing in a different area of the game. Some screenshots display different pop-ups that can appear on the screen throughout the game. I thought it was important to show what leveling up will look like, as that is an important aspect of our game. Additionally, the `Help`, `Inventory`, and `Stats` windows all share different types of information that I believed it would benefit the player to know. If they see all this information while playing the game, they will know what features they can access throughout the game. 

**Describe how you showcased your work. How did you choose what to show in the trailer? Why did you choose your screenshots?**

## Game Feel and Polish - Yifan Cui

**Document what you added to and how you tweaked your game to improve its game feel.**
