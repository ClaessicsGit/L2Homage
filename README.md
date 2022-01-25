# L2Homage
Customizing Lineage 2


## What is L2Homage
L2Homage (L2H) is a comprehensive modding tool for files with the same datastructure used for Lineage 2 - High Five. 

L2H makes modding Lineage 2 files faster, easier and safer, allowing users to rethink and reshape the world of Lineage 2, providing the hordes of burnt out players with new, exciting content, without having to spend hours learning how to manipulate the data structure.

The hope is to see a bunch of unique, new game worlds appear, instead of relaunches of the same L2 experience we’ve all been through too many times to count.

With this tool, you can edit all weapons, armors, misc items (etcs), recipes, npcs, droplists, spawns, system texts and much, much more. The list goes on, and expands as the project evolves.

## Special Thanks
A special thanks to the people who helped me make L2Homage possible!
L2H wouldn't be here if it wasn't for:
- Anarchy
- Eressea of L2Shrine
- Weedy
- Sepultribe
- L2Miko
- Rootware
- PARADISE
- Badwatermage & the players of L2Knight
- Sindelia of PlayINERA
- Matrix (EDL Team)
- And many, many more who provided answers when I needed them. Thank you! If I forgot to mention anyone, please let me know, you deserve the credit.
- Above all, thank you to the doctors and nurses who kept me alive these past years. L2H is a passion project, developed before, during and after my fight with leukemia. If you find some weird or bad code, this will also be my go-to excuse.

## Current Features
- Add, delete or modify weapons
- Add, delete or modify armors
- Add, delete or modify etc items
- Add, delete or modify sets
- Add, delete or modify spawn zones
- Add, delete or modify NPCs
- Add, delete or modify NPC spawns and parameters
- Add, delete or modify Skills
- Add, delete or modify recipes
- Add, delete or modify droplists
- Add, delete or modify shoplists (multisell)
- Modify any stats of any class
- Modify the starting equipment of any class
- Modify the starting location of all classes
- Modify when classes learn skills
- Modify what skills cost or require
- Modify NPC AI Properties (requires .nasc files in data/ai folder)
- Modify system text
- Modify class descriptions and titles
- Modify loading screen text
- Modify hunting areas
- Modify the chat filter
- Modify zone names
- Modify raids list (accessible through in-game map)
- Export server and client files simultaneously, ready for use immediately

## Installation
1. Download the newest L2H release and extract it in a folder of your choice.
2. Copy and paste the content of your Lineage 2 High Five "system" folder into the "Data/Encrypted Client Files" folder.
3. Copy and paste the files from your server's "script" folder into the "Data/Server" folder.
4. Open L2Homage.exe.

## Usage
### Initialization
In the Initialization block, you should see the "start" button. Click it. Everything should start decrypting and loading.
If installation was successful, you should see something similar to this:
![alt text](https://user-images.githubusercontent.com/76498760/150880428-207ee03f-52f9-4262-a45d-312ecf6ee1f0.png)

Content categories are in the menu on the left. Options are:
* Overview - Log, initialization and export options.
* Classes - Class descriptions, stats, when and how they learn skills, starting equipment, start location and more.
* Droplists - NPC drops.
* Experience - Exp Table for levels 1-99
* Items - Weapons, Armors, Etcs, Sets
* Multisell - Shops, NPC trades
* NPCs
* Recipes
* Skills
* System Text - Chat Filter, Game Tips, NPC Strings, System Messages, System Strings
* Zones - Zone names, Spawn zones, Hunting zones, Raids

Below the categories is a Mini Log. The Mini Log shows you a short description of any edits you performed for a quick confirmation.

Export settings are on the right. Options are:
* Server IP - Input the IP of your server.
* Custom Spawns Only - Export only custom spawns. This is used when creating an entirely custom world. No original NPC spawns will be included in the export.
* Use Export Module - This is for using a custom module to export content. It's only useful if you create your own exporting module and implement it in the source.

Clicking the "Export" button saves any changes and exports both server and client files. If you don't want to export the project, but save it for later, use the "Save" button below Initialization.

### Classes
There are two types of classes. The base classes and the specific classes.
You can choose between class type by clicking one of the two buttons below the ID search field on the left.
#### Base Classes
![classes_text](https://user-images.githubusercontent.com/76498760/150883485-879fbcb3-31f0-402a-8a6b-1947ba93f654.png)

Base classes are:
* Human Fighter
* Human Magic
* Elf Fighter
* Elf Magic
* Darkelf Fighter
* Darkelf Magic
* Orc Fighter
* Orc Shaman
* Dwarf Fighter
* Kamael Soldier

You can modify their stats individually by clicking on any of the buttons in the four categories on the right, Combat, Stats, Creation and Misc.

#### Specific Classes
![classes_2_text](https://user-images.githubusercontent.com/76498760/150883772-abfedb42-2117-4fba-a474-ce28192402cd.png)

Specific classes are all classes, including the base classes. 
In this section, you can modify which skills each class can learn, when they learn it, what it costs, any prerequisites and much more.

You can also choose to include another class as a base. Think of classes in L2 as branches or tiers. If you're tier 3, the game considers you tier 1 and tier 2 as well.
Take a Duelist for examples. It includes the "Gladiator" class, which includes the "Warrior" class, which includes the "Fighter" class. A duelist is considered all of those classes. 

Modifying a base class will affect all of the classes which include that specific base class.

### Droplists
I wanted to keep droplist editing simple, so I designed a custom system for this purpose. The first thing L2H does, is to _separate_ all drops from NPCs. All the drops are then stored and _assigned_ to each NPC. L2H keeps references to droplists across NPCs, which means you can _reuse_ the same droplists for different NPCs, if you so like. If you assign the same droplist to multiple NPCs, any changes you make to the droplist will affect the drops of all those NPCs. It's pretty neat.

There are two types of droplists, single and multi.

#### Single Droplist
![droplists_text](https://user-images.githubusercontent.com/76498760/150884830-c309cea9-66e4-49b7-8b6f-07b7748c7ff3.png)
A single droplist contains X amount of _drops_, but only _ONE_ drop can trigger per single droplist. Each drop has:
* %Chance to trigger when killing an assigned NPC.
* Minimum amount dropped when triggered
* Maximum amount dropped when triggered

#### Multi Droplist
![droplists_2_text](https://user-images.githubusercontent.com/76498760/150885966-c03e11df-1e36-49f9-aa86-5aafccb08706.png)
A multi droplist contains X amount of _single droplists_. Each single droplist can trigger independently.
This is the most commonly used droplist in L2. If you only use single droplists, each NPC would only be able to drop one thing at a time. That'd be a bit boring.

L2H shows which single droplists are assigned, and the probability of them triggering. You can edit this freely. 

### Experience
L2H allows you to modify the experience curve.
![experience_text](https://user-images.githubusercontent.com/76498760/150886936-3dce12d0-6cab-4145-9ae7-b7084704c6ae.png)
The experience value for each level is visualized with a dot on a graph, and shown in editable boxes on the right. 

To edit the experience curve, you can input the values in the boxes or simply drag the dots if you prefer to work visually.

**WARNING**

Changing the experience curve does not automatically change NPC levels or player levels! Each level is defined by the amount of exp the NPC or player has. Any change to the experience curve should be one of the very first things you do for a project. If you change the experience curve later, you'll have to manually edit all NPCs to fit your new settings. I've tried adding some custom algorithms to alleviate this issue, but it'll only take you so far. 

**WARNING**

#### Experience Algorithms
I've implemented five different experience algorithms. You can always click the "Reset EXP Table" button to revert changes to the EXP table, but any changes made with these algorithms are _permanent_. Make sure you know what you're doing. Also, keep a backup. Always keep a backup.

#### **Recalculate NPC EXPERIENCE based on LEVELS**
Maintains all NPC LEVELS and adjusts the EXPERIENCE values of all NPCs. The EXP value assigned will be the lowest EXP value of that level. 
Example:
1. Orc is level 2 with 300 EXP.
2. You change the EXP curve so level 2 starts at 400 exp.
3. Orc would now be considered level 1, even though all entries still show it as level 2.
4. Clicking the **Recalculate NPC EXPERIENCE based on LEVELS** algorithm will recalculate the EXP value of the NPC and set it to 400.

#### **Recalculate NPC LEVELS based on EXPERIENCE**
Maintains all NPC EXPERIENCE values and adjusts the LEVEL of all NPCs.
Example:
1. Orc is level 2 with 300 EXP.
2. You change the EXP curve so level 2 starts at 400 exp.
3. Orc would now be considered level 1, even though all entries still show it as level 2.
4. Clicking the **Recalculate NPC LEVELS based on EXPERIENCE** algorithm will recalculate the LEVEL of the NPC and set it to 1.

#### **Recalculate NPC EXPERIENCE PER KILL based on LEVELS**
Compares the original level value with the new level value and adjusts the EXPERIENCE PER KILL value by the percentage difference.
**EFFECT IS CUMMULATIVE, ONLY DO THIS ONCE PER PROJECT**

#### **Recalculate NPC SKILL POINTS PER KILL based on LEVELS**
Compares the original level value with the new level value and adjusts the SKILL POINTS PER KILL value by the percentage difference..
**EFFECT IS CUMMULATIVE, ONLY DO THIS ONCE PER PROJECT**

#### **Recalculate NPC REPUTATION POINTS PER KILL based on LEVELS**
Compares the original level value with the new level value and adjusts the REPUTATION POINTS PER KILL value by the percentage difference..
**EFFECT IS CUMMULATIVE, ONLY DO THIS ONCE PER PROJECT**

