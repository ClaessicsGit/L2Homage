# L2Homage
Customizing Lineage 2

*Imagine a really fancy banner right here*

I'm finally ready to share this. It's taken way, way longer than I hoped for, but here we are. I hope someone out there will put this tool to good use. I've poured a lot of love into this over the years.

Thank you for your patience.

If you have any questions, feel free to join L2Homage discord at https://discord.gg/97yAsCA

- Bumble / Claessic

## What is L2Homage
L2Homage (L2H) is a comprehensive modding tool for files with the same datastructure used for Lineage 2 - High Five. 

**This is not for L2j!** It will only work with files similar to L2OFF. 

This project contains no illegal content. It's based entirely on publically available information. 
It includes a few executables, property of their respective developers.
If you have any complaints, please reach out to me.

I originally developed L2H for Lineage 2 - Gracia Epilogue, but I've upgraded it _vastly_. I've even added multithreading to speed up loadtimes, I've polished the interface and interaction design significantly, and it now works for Lineage 2 - High Five. This is the only chronicle I've developed this version for. It will not work for Interlude or anything newer than H5, but feel free to branch this project and convert it to that chronicle if you want to. You have my full support! Let's get modding!

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
- Badwatermagic & the players of L2Knight
- Sindelia of PlayINERA
- Matrix (EDL Team)
- Pada
- DStuff
- And many, many more who provided answers when I needed them. Thank you! If I forgot to mention anyone, please let me know, you deserve the credit.

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
4. If you have any images, put them into the Images folder.
5. If you don't care about the images, you still need the worldmap and worldmap thumbnail images, or L2H won't start.
6. If you have any AI files, put them into the "Data/AI" folder.
7. Open L2Homage.exe.

If nothing happens when L2Homage.exe is opened, something is wrong with the data files.

## Usage

Before you start, and this is the most important part, always, always, ALWAYS **REMEMBER TO TAKE BACKUPS!**. To do this, just save a copy of the whole /Data folder somewhere, or simply compress it into an archive. If something breaks, you can just replace the entire data folder with your backup and continue your project. Now then, let's do this.

### Initialization
In the Initialization block, you should see the "start" button. Click it. Everything should start decrypting and loading.
If installation was successful, you should see something similar to this:
![overview](https://user-images.githubusercontent.com/76498760/151079976-e775c69c-5360-4d5f-b52e-322c413a23c0.png)

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

---
### Classes
There are two types of classes. The base classes and the specific classes.
You can choose between class type by clicking one of the two buttons below the ID search field on the left.
#### Base Classes
![classes_text](https://user-images.githubusercontent.com/76498760/151079955-42a0c412-17f2-41ca-8b26-2edfd1c9602b.png)

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
![classes_2_text](https://user-images.githubusercontent.com/76498760/151079958-96c221e9-41a5-4f89-ab6e-43343fdc5111.png)

Specific classes are all classes, including the base classes. 
In this section, you can modify which skills each class can learn, when they learn it, what it costs, any prerequisites and much more.

You can also choose to include another class as a base. Think of classes in L2 as branches or tiers. If you're tier 3, the game considers you tier 1 and tier 2 as well.
Take a Duelist for examples. It includes the "Gladiator" class, which includes the "Warrior" class, which includes the "Fighter" class. A duelist is considered all of those classes. 

Modifying a base class will affect all of the classes which include that specific base class.

---

### Droplists
I wanted to keep droplist editing simple, so I designed a custom system for this purpose. The first thing L2H does, is to _separate_ all drops from NPCs. All the drops are then stored and _assigned_ to each NPC. L2H keeps references to droplists across NPCs, which means you can _reuse_ the same droplists for different NPCs, if you so like. If you assign the same droplist to multiple NPCs, any changes you make to the droplist will affect the drops of all those NPCs. It's pretty neat.

There are two types of droplists, single and multi.

#### Single Droplist
![droplists_text](https://user-images.githubusercontent.com/76498760/151079960-d82f2901-0ed2-44b3-ba8d-056a2bbcdeb9.png)
A single droplist contains X amount of _drops_, but only _ONE_ drop can trigger per single droplist. Each drop has:
* %Chance to trigger when killing an assigned NPC.
* Minimum amount dropped when triggered
* Maximum amount dropped when triggered

#### Multi Droplist
![droplists_2_text](https://user-images.githubusercontent.com/76498760/151079962-75a43ca7-55ef-400c-902f-09eeb5b39052.png)
A multi droplist contains X amount of _single droplists_. Each single droplist can trigger independently.
This is the most commonly used droplist in L2. If you only use single droplists, each NPC would only be able to drop one thing at a time. That'd be a bit boring.

L2H shows which single droplists are assigned, and the probability of them triggering. You can edit this freely. 

**WARNING**

If the trigger chance of all the drops in a single droplist, or all the single droplists in a multi droplist exceeds 100, only the first 100 will work.

Example:
1. Gremlin has a single droplist assigned
2. The single droplist has 5 items that can drop, each with 20% chance to trigger
3. You decide to increase the drop chance of the first item to 50%
4. The gremlin now has:
* 50% chance to drop item 1
* 20% chance to drop item 2
* 20% chance to drop item 3
* 10% chance to drop item 4
* 0% chance to drop item 5

The same applies to single droplists inside of a multi droplist.

**WARNING**

---

### Experience
L2H allows you to modify the experience curve.
![experience_text](https://user-images.githubusercontent.com/76498760/151079963-b4a4a1c6-1b39-4097-9e40-7ee7bf118f5f.png)
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
**EFFECT IS CUMULATIVE, ONLY DO THIS ONCE PER PROJECT**

#### **Recalculate NPC SKILL POINTS PER KILL based on LEVELS**
Compares the original level value with the new level value and adjusts the SKILL POINTS PER KILL value by the percentage difference..
**EFFECT IS CUMULATIVE, ONLY DO THIS ONCE PER PROJECT**

#### **Recalculate NPC REPUTATION POINTS PER KILL based on LEVELS**
Compares the original level value with the new level value and adjusts the REPUTATION POINTS PER KILL value by the percentage difference..
**EFFECT IS CUMULATIVE, ONLY DO THIS ONCE PER PROJECT**

---

### Items
L2H lets you edit almost any item in the game. 

To create a new item, find an item you want to use as a template. Select it, and click the "Clone" button. You can also delete an item, but you should only ever delete custom items. Deleting original content causes trouble.
The module button does nothing by default, but you can program your own module for modifying items.

Items are split into four categories:

* Weapons
* Armors - All armor types, including accessories and decorations.
* Etcs - Everything else, including arrows, quest items, consumables and so on.
* Sets

Items share some basic properties, but each item type has properties unique to that type.


I've exposed almost all interesting properties for users to modify. I've hidden the hundreds of properties that aren't of use to content modding. The hidden stuff is mostly client-side stuff, like how an armor piece fits each race model, rotation and so on.

Most properties are self-explanatory, but some are a bit more obscure. Below are descriptions of each item property.

#### Weapons
![Items_Weapon_Text](https://user-images.githubusercontent.com/76498760/151079969-cb856133-ff66-489e-916e-f023903f0abc.png)
| Base Property      | Description |
| ----------- | ----------- |
| ID  | Unique integer Item ID. | 
| Template      | Item ID this item is copied from. This is an L2H property to keep track of custom item origins.       |
| Name ID   | Unique string ID      |
| Grade | Grade from none to s84| 
| Type | Item type| 
| In-Game Name | Name of item as shown in-game| 
| Sub Type | Specific type of weapon| 
| Additional Name | Added name to item. This is usually "Focus" or "Haste" etc| 
| Slot Type| Where this item is equipped| 
| Price | Base price before any taxes are applied| 
| Durability | Used for shadow weapons. If -1, it's permanent. Any value above 0 is minutes the item can be equipped before disappearing|
| Duration| How long the item exists after spawning| 
| Weight | Weight| 
| Enchanted| Amount of enchants the base item has| 

| Appearance Property      | Description |
| ----------- | ----------- |
| Mesh 1-2| 3D model used to display the item in-game. Usually only 1 mesh is used. More can be used (for dual swords for instance)| 
| Drop Mesh 1-3 | 3D model used to display the item on the ground. Usually only 1 mesh is used| 
| Effect| Visual Effect| 
| Texture 1-4| Texture used on Mesh 1-2| 
| Drop Texture 1-4| Texture used on Drop Mesh 1-3| 
| Equip Sound | Sound played when equipping item| 
| Drop Sound | Sound played when dropping item| 
| Sound 1-4 | Sound played when using item| 
| Icon 1-5 | Icon used for item in-game. Usually only 1 icon is used| 

| Combat Property      | Description |
| ----------- | ----------- |
| Physical Attack | P.Atk value| 
| Magical Attack | M.Atk value| 
| Crit Rate | Added chance of critical strike| 
| Hit Rate| Added chance of hitting target| 
| Attack Range| Max distance before an attack can happen| 
| Damage Range | Not sure| 
| Attack Speed | Modifier to attack speed. Max attack speed cap by default is 1500| 
| Random Damage | Amount damage output may vary| 
| Attack Attribute| Damage Type (Fire, Water, Wind, Earth, Holy, Unholy)| 
| Attribute Value | %Amount of damage is of Attack Attribute. Example: Attack attribute is set to Holy, Attribute value is set to 20. This means mean 20% of damage is holy, 80% is physical|
| Shield Rate|Added Chance of blocking| 
| Shield P Defense | Physical defense value added on successful block| 
| Evasion| Added chance to avoid taking damage| 
| Resistances| Adds attribute resistances| 

| Conditions Property      | Description |
| ----------- | ----------- |
| Crystallizable| Can this item be crystallized and how many crystals will it make when crystallized|  
| Hero| Is item for heroes only| 
| Droppable | Can item be dropped| 
| Tradable| Can item be traded| 
| Private Store-able| Can Item be sold in private stores| 
| Destructible| Can item be destroyed| 
| Enchantable| Can item be enchanted| 
| Use Elemental Attribute| Will item use its elemental attribute on attack| 
| Magic Weapon| Is this item considered a magic weapon| 
| NPC Use Only| Is this item only for NPCs| 
| Allowed in Olympiad| Can this item be used during Olympiad battles| 
| For Premium Users Only| Is this item restricted to accounts with premium status| 
| Equip| Conditions for equipping the item. This can be level requirements or restricting item to certain races| 
| Situational| Restrictions for when this item can be used when equipped. This is the item's behavior, you can set this to no_attack to prevent it from attacking if you want | 
| Use| Conditions for using this item| 

| Triggers Property      | Description |
| ----------- | ----------- |
| Soulshot Cost| How many soulshots item will use per attack| 
| Spiritshot Cost| How many spiritshots item will use per spell| 
| Reduce SS Chance| % Chance to reduce soulshot cost| 
| Reduce SS Count| Soulshot cost reduced by this amount if reduce SS chance triggers| 
| Reduce SPS Chance| % Chance to reduce spiritshot cost| 
| Reduce SPS Count| Spiritshot cost reduced by this amount if reduce SPS chance triggers| 
| MP Per Attack| MP cost per attack| 
| +4 Skill| Gives this skill when enchanted to +4| 
| Reuse Delay| Cooldown for reusing this item| 
| Reuse Group| Shares cooldown with items in the same group| 
| Equip Delay| Cooldown for equipping this item again| 
| Item Skill| Active skill cast when using item| 
| MP Cost per Use| MP cost when using Item Skill| 
| Attack Skill| Skill triggering when attacking| 
| Critical Skill| Skill triggering when a critical strike occurs| 
| Magic Skill and Chance | Skill triggering when casting a spell at % chance| 
| Unequip Skill| Skill triggered when unequipping item| 
| Multi Skills| Skills provided by item when equipped, usually passive skills| 

#### Armors
![Items_Armor](https://user-images.githubusercontent.com/76498760/151079964-cf28a540-58f8-4d64-aaea-8ccb773e61de.png)
| Base Property      | Description |
| ----------- | ----------- |
| ID  | Unique integer Item ID. | 
| Template      | Item ID this item is copied from. This is an L2H property to keep track of custom item origins.       |
| Name ID   | Unique string ID      |
| Grade | Grade from none to s84| 
| Armor Type | Armor type| 
| In-Game Name | Name of item as shown in-game| 
| Body Part | Body part of armor item| 
| Additional Name | Added name to item| 
| Price | Base price before any taxes are applied| 
| Durability | Used for shadow weapons. If -1, it's permanent. Any value above 0 is minutes the item can be equipped before disappearing|
| Duration| How long the item exists after spawning| 
| Weight | Weight| 

| Appearance Property      | Description |
| ----------- | ----------- |
| Drop Mesh 1-3 | 3D model used to display the item on the ground. Usually only 1 mesh is used| 
| Drop Texture 1-4| Texture used on Drop Mesh 1-3| 
| Equip Sound | Sound played when equipping item| 
| Drop Sound | Sound played when dropping item| 
| Sound 1-4 | Sound played when using item| 
| Icon 1-5 | Icon used for item in-game. Usually only 1 icon is used| 

| Combat Property      | Description |
| ----------- | ----------- |
|  Physical Defense|  P.Def |  
|  Magical Defense|  M.Def|  
|  Evasion|  Added chance to evade|  
|  MP Bonus|  MP Bonus when equipped|  
|  Resistances|  Adds resistances (Fire, Water, Wind, Earth, Holy, Unholy)|  

| Conditions Property      | Description |
| ----------- | ----------- |
| Crystallizable| Can this item be crystallized and how many crystals will it make when crystallized| 
| Droppable | Can item be dropped| 
| Tradable| Can item be traded| 
| Private Store-able| Can Item be sold in private stores| 
| Destructible| Can item be destroyed| 
| Enchantable| Can item be enchanted| 
| Use Elemental Attribute| Is item considered Elemental| 
| Magic Weapon| Is this item considered a magic weapon| 
| NPC Use Only| Is this item only for NPCs| 
| Allowed in Olympiad| Can this item be used during Olympiad battles| 
| For Premium Users Only| Is this item restricted to accounts with premium status| 
| Equip| Conditions for equipping the item. This can be level requirements or restricting item to certain races| 
| Situational| Restrictions for when this item can be used when equipped| 
| Use| Conditions for using this item| 

| Triggers Property      | Description |
| ----------- | ----------- |
| +4 Skill| Gives this skill when enchanted to +4| 
| Reuse Delay| Cooldown for reusing this item| 
| Reuse Group| Shares cooldown with items in the same group| 
| Equip Delay| Cooldown for equipping this item again| 
| Item Skill| Active skill cast when using item| 
| MP Cost per Use| MP cost when using Item Skill| 
| Unequip Skill| Skill triggered when unequipping item| 
| Multi Skills| Skills provided by item when equipped, usually passive skills| 

#### Etcs
![Items_Etc](https://user-images.githubusercontent.com/76498760/151079967-50e144ec-f11c-4b0b-8240-e430028fbaad.png)
| Base Property      | Description |
| ----------- | ----------- |
| ID  | Unique integer Item ID. | 
| Template      | Item ID this item is copied from. This is an L2H property to keep track of custom item origins.       |
| Name ID   | Unique string ID      |
| Grade | Grade from none to s84| 
| Type | Item type| 
| In-Game Name | Name of item as shown in-game| 
| Item Type | Specific item type| 
| Additional Name | Added name to item. This is usually "Focus" or "Haste" etc| 
| Etc Type| Specific sub type of Etc items| 
| Price | Base price before any taxes are applied| 
| Durability | Used for shadow weapons. If -1, it's permanent. Any value above 0 is minutes the item can be equipped before disappearing|
| Duration| How long the item exists after spawning| 
| Weight | Weight| 
| Family| Not entirely sure of this one| 

| Appearance Property      | Description |
| ----------- | ----------- |
| Drop Mesh 1-3 | 3D model used to display the item on the ground. Usually only 1 mesh is used| 
| Drop Texture 1-4| Texture used on Drop Mesh 1-3| 
| Equip Sound | Sound played when equipping item| 
| Item Sound | Sound played when dropping or using item| 
| Icon 1-5 | Icon used for item in-game. Usually only 1 icon is used| 

| Conditions Property      | Description |
| ----------- | ----------- |
| Crystallizable| Can this item be crystallized and how many crystals will it make when crystallized| 
| Droppable | Can item be dropped| 
| Tradable| Can item be traded| 
| Private Store-able| Can Item be sold in private stores| 
| Destructible| Can item be destroyed| 
| Stackable| Can item be stacked, and how much|
| Enchantable| Can item be enchanted| 
| Use Elemental Attribute| Is item considered Elemental| 
| Magic Weapon| Is this item considered a magic weapon| 
| NPC Use Only| Is this item only for NPCs| 
| Allowed in Olympiad| Can this item be used during Olympiad battles| 
| For Premium Users Only| Is this item restricted to accounts with premium status| 
| Equip| Conditions for equipping the item. This can be level requirements or restricting item to certain races| 
| Situational| Restrictions for when this item can be used when equipped| 
| Use| Conditions for using this item| 

| Triggers Property      | Description |
| ----------- | ----------- |
| Item Skill| Active skill cast when using item| 
| MP Cost per Use| MP cost when using Item Skill| 
| Unequip Skill| Skill triggered when unequipping item| 
| +4 Skill| Gives this skill when enchanted to +4| 
| Reuse Delay| Cooldown for reusing this item| 
| Reuse Group| Shares cooldown with items in the same group| 
| Equip Delay| Cooldown for equipping this item again| 
| Consume Type| What happens when this item is triggered|
| Default Action| What happens when this item is used|
| Multi Skills| Skills provided by item when equipped, usually passive skills| 
| Capsuled Items | Does this item give any items when used| 

#### Sets
Sets are a combination of different items that, when equipped together, provide a bonus to the player.

Sets are tied to the **Chest Piece** of the set. The chest holds the reference to all other set pieces. Because of this structure, a chest piece cannot be part of more than one set. Well, maybe it can, but you definitely won't be able to see it reflected in-game. In short, keep sets to one chest piece.

To create a set, make sure no set is selected, then click the "Create New Set" button in the middle of the screen.

Select a set you want to edit. You should see something like this:
![Items_Set](https://user-images.githubusercontent.com/76498760/151079968-4f5460b5-1288-443e-8b0b-708629d1180f.png)
If you want to add any items to the set, click an empty slot. A popup will appear, showing you only valid options for that slot.
If you want to change any set pieces, click on the set piece. A popup will appear, showing you only valid options for that slot.
If you want to remove an item from a set, right click the set piece.

It's that simple.

Each set has different properties.


| Set Property      | Description |
| ----------- | ----------- |
| Set Name| Give the set a name. Only visible in L2H| 
| Set ID| Unique integer identifier| 
| Template| ID of set this set is based on| 
| Set Skill| Skill triggered when full set is equipped| 
| Set Effect Skill| Set bonus effect enabled when full set (except for offhand) is equipped| 
| Set Effect Skill (With Additional Slot)| Set bonus effect enabled when full set (including offhand) is equipped. This does not remove the regulard effect skill, but stacks with it| 
| Enchanted Skill and #Enchants Required| Skill enabled when each set item is enchanted to # number of enchants| 
| Description| Description of Set Effect Skill in-game. This has no effect on the actual skill bonus, this is only text visible to the player| 
| Additional Description | Description of Set Effect Skill (With Additional Slot) in-game. This has no effect on the actual skill bonus, this is only text visible to the player| 
| Enchanted Description | Description of Enchanted Skill. This has no effect on the actual skill bonus, this is only text visible to the player| 
| Attribute Bonuses| Fixed bonuses to stats when a full set (except for offhand) is equipped| 

---

### Multisell
Multisell is the term for NPC traders in the game.

Adding a multisell to the actual game requires editing the AI and HTML files, but you can freely edit existing ones, if you don't feel like doing that.
![Multisell](https://user-images.githubusercontent.com/76498760/151079972-d53d8079-e665-4c01-88dd-8231644fec88.png)

There are four types of settings for a multisell list.

| Multisell Property      | Description |
| ----------- | ----------- |
| Taxed| Will this multisell give castle owners tax| 
| Keep Enchants| Will items traded from NPCs keep their enchants when transferred to the player| 
| Show All| Will NPC show all items for sale, or only the items the player can purchase (useful for huge multisell lists)| 
| Show Variations | Will NPC show different types of the same weapon base (used for NPCs selling enchanted stuff)| 

You can also assign a required NPC, as an added security measure. If a multisell has a required NPC, it cannot work for any other NPCs, even if it's assigned to it through AI and HTML.

There are two parts to a multisell list. A sale and a cost.

A sale is one or more items (a bundle) for sale. It is a single transaction.

A cost is the price for that one transaction.

Usually, there's only one thing in a sale, with one or more items as its cost. There _can_ be more items for sale though.

The following options are available for multisell sales:

![sales](https://user-images.githubusercontent.com/76498760/151060763-11c9f07c-f7f4-4a8d-b6c0-d57506aa6a4f.png)
| Sale      |  |
| ----------- | ----------- |
| A)|  Move sale up (affects in-game order too)
| B)|  Move sale down
| C)|  Remove item from sale bundle. If no other items are in the sale, remove the sale as a whole
| D)|  Add item to the sale bundle
| E)|  Remove sale

You can also adjust the castle tax base value above the costs. This is the amount that's used to generate income for castle owners. It is independent from the actual cost of the item.

---

### NPCs
Modifying NPCs is very similar to modifying items. You can clone or delete NPCs, or edit the _properties_ of their AI variables. This is very different from editing their AI. More on that below.
![NPCs](https://user-images.githubusercontent.com/76498760/151079973-0a004219-c692-435c-a4e8-1872e0b9fdb3.png)
This is Vrykolakas in all his glory. He was taken from all of us much too soon.

| Base Property      | Description |
| ----------- | ----------- |
| ID| Unique integer ID| 
| Template| ID of NPC this NPC is based on| 
| Name ID| Unique string ID| 
| In-Game Name| Name seen In-Game| 
| Description| Description of NPC| 
| Undead| Is NPC Undead| 
| Can Move| Can NPC move| 
| Flying| Is NPC flying| 
| Targetable| Can NPC be targeted| 
| Attackable| Can NPC be attacked| 
| Event Flag| Can this NPC trigger events - Necessary for aggressive NPCs for instance| 
| Unique| Is NPC unique| 

| Items Property      | Description |
| ----------- | ----------- |
| Equipment
| Chest| Item equipped in Chest slot| 
| Right Hand| Item equipped in Right Hand| 
| Left Hand| Item equipped in Left Hand| 
| Droplists
| Normal| Assigned normal droplist| 
| Spoil| Assigned spoil droplist| 
| Multi| Assigned multi droplist| 
| Extra| Assigned extra droplist - This is just an extra multi droplist. It's pretty much only used for herbs, so they don't take up drop slots|

| Stats Property      | Description |
| ----------- | ----------- |
| STR| Strength| 
| INT| Intelligence| 
| DEX| Dexterity| 
| WIT| Wit| 
| CON| Constitution| 
| MEN| ....Men I guess| 

| Level Specifics Property      | Description |
| ----------- | ----------- |
| Level| Level| 
| Experience| Experience| 
| Kill Experience| Experience rewarded when killed| 
| Kill Skill Points| Skill points rewarded when killed| 
| Kill Reputation Points| Reputation points rewarded when killed| 

| Offense Specifics Property      | Description |
| ----------- | ----------- |
| P.Atk| Physical Attack| 
| M.Atk| Magical Attack| 
| Atk. Speed| Attack speed| 
| Atk. Type| Attack Type| 
| Atk. Range| Attack Range| 
| Reuse Delay| Cooldown for reusing skills| 
| Crit Chance| Added chance for critical strikes| 
| Hit Chance| Added chance to hit| 
| DMG Range| Not really sure| 
| Random DMG| How much damage can vary per hit| 

| Defense Specifics Property      | Description |
| ----------- | ----------- |
| P.Def| Physical Defense| 
| M.Def| Magical Defense| 
| Evasion| Evasion Rate| 
| Shield Defense Rate| Added chance to block| 
| Shield P. Def.| Physical defense added on successful block| 
| Resistances| Resistances to attributes (Fire, water, wind, earth, holy, unholy)| 


| Misc Specifics Property      | Description |
| ----------- | ----------- |
| Race| Race| 
| Sex| Male or Female| 
| Clan| Group of NPCs considered friends. This is related to NPC behavior. If clan members are attacked nearby, NPC will help friendly clans| 
| Ignore Clan| NPC won't help these clans| 
| Clan Help Range| How far away will NPC help friendly clan members| 
| Corpse Duration| How long will NPC corpse stay on the ground| 
| Aggro Range| How far away will NPC attack hostiles| 
| Has Summoner| Is NPC a summoned minion| 
| Unsowing| Can NPC be sown with manor seeds| 
| No Sleep Mode| Not really sure, but it sounds really convenient. I wish I had a setting like that.| 

At the bottom of the NPC page, you'll see a bar with a list of all the passive skills assigned to this NPC. It can be edited freely as well.

Now, about those AI settings I mentioned earlier.

If you have AI files that can be read, and are placed in the Data/AI folder, you can click the "AI" button next to the delete button. That'll open a popup, showing you all the different variables for the AI assigned to the NPC, _including_ any variables of inherited NPC AI files. For Vrykolakas, it looks like this:

![text](https://user-images.githubusercontent.com/76498760/151066571-4e9a1f43-3139-486b-9ce2-b7e733997a4d.png)

It's simple to edit. Toggle variables on/off and fill in the values. That's it. You can't change the actual AI this way, but you can modify the existing AI parameters. You can change skills the NPC casts during combat, movement timings and stuff like that.



---

### Recipes
The recipe page is for editing existing recipes. You cannot add recipes in here. Why, you may ask? Well, that's because recipes are tied to the item that teaches you that recipe. If you want to create a new recipe, go to the items page, find a recipe and clone it. The new, cloned recipe will appear in the Recipes page for you to edit.

![Recipes](https://user-images.githubusercontent.com/76498760/151079977-38d2e6f5-6701-487d-b5ab-4b286dd821bb.png)

Recipes have at least one outcome, two at most. You can click the outcome buttons to change the result of a recipe.

You can also edit the amount of items a recipe creates. 

Below the amount, you can edit the chance for that specific outcome to happen. **You have to make sure the chances of the outcomes equal 100%.**

Required materials are listed below, in the Materials section. A recipe can have at most 11 types of required materials. You can add or replace an item by clicking the image or an empty slot. To remove an item from the recipe, use right click instead. Simple as that.

Recipes have the following properties:

| Misc Specifics Property      | Description |
| ----------- | ----------- |
| Owner Item ID| Name of the item referencing this specific recipe| 
| Recipe ID| Unique integer ID| 
| Name ID| Unique string ID| 
| Craft Level| Level of item crafting skill required to learn and use this recipe| 
| MP Cost| MP cost of recipe per craft| 
| Success rate| Success rate of recipe in %| 
| Common Recipe| Is recipe common| 
| Require Extra Craft Recipe| Does recipe require an additional recipe on top of material costs| 
| Description| Description of recipe| 

---

### Skills
L2H allows for modifying and cloning existing skills. 
![Skill](https://user-images.githubusercontent.com/76498760/151079978-97abc548-0958-446b-8267-03a326643583.png)

Skills are either Active, Passive or Toggleable.

There's _a lot_ of properties for skills. Let's get into it.

First of all, skills can come in different varieties. Each variety of a skill is available in the "Variants" block. You can navigate freely between them.

If you want to add a skill, find it and click "Clone". To add more levels to your new skill, click the "Add Levels" button below the "Levels" block.

You can also delete custom skills and batch edit all levels of a skill at once - we'll get back to that later.

First, let's get all those properties out of the way:
| Base Property      | Description |
| ----------- | ----------- |
| ID | Unique Integer ID| 
| Template | ID of skill this skill is based on| 
| Name ID| Unique string ID| 
| In-Game Name| Name of skill as seen In-Game| 
| Level| Level of skill| 
| Client Category| The group in which this skill appears in the skill list ingame| 
| Magic Skill| Is skill considered Magic| 
| Magic Level| Magic Level of skill. Used for calculating hit chance, crit chance and more| 
| Attribute, Type and Value| Attribute of skill (fire, wind, etc etc), the type and value| 
| Add. Description| Additional Description| 
| Add. Description 2| Additional Description 2| 
| Character Animation| The type of animation a character will perform when casting this skill| 
| Cast Style| I can't recall right now| 
| Caster Animation Skill Effect| ID of skill effect appearing on caster while casting this skill| 
| Target Animation Skill Effect| ID of skill effect appearing on target while casting this skill| 
| Icon 1-2| Icon of skill| 


| Casting Property      | Description |
| ----------- | ----------- |
| MP Cost (Initial)| MP cost immediately when using skill| 
| MP Cost (Finish) | MP cost after successfully casting skill| 
| MP Cost Per Tick| MP cost each tick| 
| HP Cost| HP cost| 
| Item Cost| Item required to cast skill| 
| Etc Cost| Etc stuff required to cast skill| 
| Cast Range| How far away can this be cast| 
| Effective Range| How far away is this skill effective - used for still hitting when target runs away| 
| Cast Time| Base value of how many seconds it takes to cast this skill| 
| Animation Time| | 
| Cancel Time Window| How long before the skill is cast can it be cancelled (I think)| 
| Hit Chance| Added/Reduced chance to hit| 
| Reuse Delay| Cooldown of skill| 
| Lock Reuse Delay|| 


| Targeting Property      | Description |
| ----------- | ----------- |
| Target Type| What can skill be used on| 
| Target Object| What kind of relation is required to attack the target (friend, enemy, all etc)
| Scope| How does the skill hit, single or AoE, what type of AoE etc| 
| Affect Range| If AoE, how far does skill hit| 
| Fan Range| If fan, how does the fan look - think of it as a cone| 
| Scope Height| How far does skill hit vertically| 
| Target Limits| How many targets can skill hit, min/max| 


| Effects Property      | Description |
| ----------- | ----------- |
| Server Operate Type| Skill Type - Described in dropdowns| 
| Start Effect| This happens as soon as you start casting skill| 
| Effect| This happens when skill is cast| 
| Self Effect| This happens to yourself when you cast the skill| 
| Tick Effect| This happens at every tick interval| 
| Attached Skill| Skill is cast at every tick interval| 
| Tick Interval| How often does the skill tick| 
| End Effect| What happens when the skill ends| 
| PvP Effect| Does skill behave differently in PVP| 
| PvE Effect| Does skill behave differently in PvE| 


| Buffs/Debuffs Property      | Description |
| ----------- | ----------- |
| Type| Type of buff/debuff| 
| Duration| Duration of buff/debuff| 
| Trait| Trait of buff/debuff| 
| Buff/Debuff Level| Level of buff/debuff. Used for calculating hit chance, resistances etc| 
| Buff/Debuff Protect Level| Same as above| 
| Debuff | Is skill considered debuff| 
| Irreplacable| If skill is irreplacable, it cannot be removed with debuffs| 
| Instant Effect| No delay for skill, usually for instant effect when picking up herbs| 
| Remove On Logout| Does buff/debuff disappear when character logs out| 


| Conditions Property      | Description |
| ----------- | ----------- |
| Operate Condition| When can player use this skill| 
| Target Operate Condition| When can skill be used on specific target| 
| Passive Condition| Constant restrictions| 
| Basic Property| 
| Ride State| Can this skill be used while mounted| 
| Allow in Olympiad| Can skill be used in Olympiad Battles| 
| Block Action Use Skill| | 
| Multi Class| | 

| Misc Property      | Description |
| ----------- | ----------- |
| Next Action| Automatically perform an action after skill is cast. Continuing attack target for instance, when using melee skills| 
| Transform Type| 


Enchanted skills are not as easily edited in High Five, but I've exposed the variables I could find.


#### Skill Batch Editing
When editing skills with many different levels, it became apparent that it's way too much work to adjust the values of each level individually. 

Enter **BATCH EDITING**!

Select a skill and click the "Batch" button. You'll be greeted with a popup like this:

![Skill_Batch](https://user-images.githubusercontent.com/76498760/151079980-e5646a26-941a-4dd4-a901-6c466d74bb2e.png)
Select a property with the dropdown on the left and easily edit that property across all levels of selected skill. 

You can write the values manually or drag the points if you prefer to work visually.

The value type is tied to the existing types. If all numbers are integers, only integers are allowed. If L2H sees a float value, floats are allowed.

---

### System Text
System Text require no description. It's simple stuff.

---

### Zones
When clicking the zones category, you'll be faced with a world map.
![Choose](https://user-images.githubusercontent.com/76498760/151079981-0d3207ff-ef71-4e77-a0a9-6f3c063d5abf.png)

You can click a part of the world map to go see all the zones in that part of the world. You can also click on the shortcuts on the left, if you don't quite remember where areas of interest are.

When you've selected an area, you'll be taken to the spawn areas page.

#### Spawns
![Spawn](https://user-images.githubusercontent.com/76498760/151079990-ea2ddb1b-58c3-4cc0-ac52-830ac69f6717.png)

Above is the Execution Grounds area. The list on the left are all the existing spawn areas, available for use. You can hover your mouse over each entry to see exactly where it is. Just right of the list of spawn areas, you can see all layers in this part of the world, if it has any. As you may or may not now, there's a catacomb beneath Execution Grounds. You can click the little thumbnail of that map to switch to that layer if you so please. I'll stay above grounds for this documentation though. Above the list of spawn areas, you'll see four buttons; Spawn Areas, Hunting Zones, Zone Names, Raids. Let's start with spawn areas.

Click on a spawn area to lock it into place, and you'll see existing NPC Makers in the NPC Makers block. NPC Makers are responsible for spawning NPCs in an assigned area.

If no NPC makers exist, you won't see any (for good reasons I'll say)

In the example above, I've selected the spawn zone called _dion02_2122_22_. There's only one NPC maker, so I selected that one. I can see the properties next to the NPC Maker list. The properties are:

| NPC Maker Parameters      | Maker Type | Description |
| ----------- | ----------- | ----------- |
| Maker Type| | Basic or Extended Maker type| 
| Initial Spawns| Basic | How many spawns are spawned when spawn area is enabled (when starting NPC server for instance)
| Spawn Time| Basic | 
| Max Spawns| Both | How many spawns can be active in this NPC Maker at once| 
| Name| Extended | Unique string ID of NPC Maker| 
| AI| Extended | Assigned AI for NPC Maker|
| AI Parameters| Extended | Parameters for Above| 
| Flying| Extended| NPC Maker can spawn NPCs in air| 
| Banned Territory| Extended| NPC Maker cannot spawn NPCs in banned territories| 

In the top-right corner, you'll see the NPC Maker Spawns block. This is a list of all NPCs assigned to the selected NPC maker. You can add or delete NPC spawns with the buttons just below the spawns.

Below the buttons are the NPC Spawn Parameters. These parameters apply to the selected NPC Maker Spawn:

| NPC Maker Spawn Parameters      | Description |
| ----------- | ----------- |
| Name| Name of NPC spawning here| 
| Max Possible Spawns| How many of this specific NPC can spawn in the parent NPC Maker| 
| Respawn Time| How long does it take for NPC to respawn after being killed| 
| Respawn Deviance| Variance in respawn time| 
| Fixed Position(s)| Does NPC spawn in a specific spot or randomly in assigned NPC Maker Area| 
| X,Y,Z,Yaw,%| If a fixed position is added to the list of fixed positions for this NPC Spawn, the NPC will spawn at the fixed position. X,Y,Z are world coordinates, you can find these by going somewhere in Lineage 2 and writing //loc. Yaw is direction the NPC will look. Value ranges from 0-65535. % is chance of this spawn point being selected when spawning. You can add more if you want to annoy users by making them running around for an NPC that can spawn a number of specific places. I'm looking at you Master Toma| 
| Database Name| Name used for storing NPC spawn data in database| 
| Database Save| 
| Boss Respawn| If Boss Respawn, cooldowns are preserved across server restarts| 
| Minions| Does NPC spawn minions| 
| Nickname| Nickname| 
| Maker AI| Specific AI used for this NPC Maker| 
| Maker AI Parameters| AI Parameters for AI assigned above

That's all there is to spawning NPCs. You can clone spawn areas, add NPC Makers, add NPC Maker Spawns and make the world your own.

Let's move on to Hunting Zones.

#### Hunting Zones
Hunting zones are the list of areas accessible through the in-game map. It's nothing more than a list of points of interest for users to see. That makes it easy to edit.
![Hunting](https://user-images.githubusercontent.com/76498760/151079984-d34c5f3d-dfd6-47ab-8bfc-2217a4d0fc7c.png)

Hunting zones are just points in the game world. The properties for a hunting zone are:

| Hunting Zone Parameters      | Description |
| ----------- | ----------- |
| ID | Unique integer ID| 
| Name| In-Game name of the hunting zone| 
| Affiliated Area ID| 
| Hunting Type| 
| Level| NPC levels in this area| 
| Position| X, Y, Z values of the location| 

That's it for hunting zones.

#### Zone Names
Zone names are the names displayed when entering the area in the game. You cannot change the location of these areas, but you can change the existing ones. Well, that's not entirely true, but it's a lot of work to change the areas. It's assigned with a Color ID, targeting a multi-layered image encrypted in the game files, which can tell you exactly which zone you're in when the game reads your location and compares it to the specific layer ID on the encrypted image. In short, it's a hassle. Let's look at the parameters instead.

![Name](https://user-images.githubusercontent.com/76498760/151079986-478532cf-03b9-4cef-87d6-695164cdf524.png)

| Zone Names Parameters      | Description |
| ----------- | ----------- |
| ID | Unique integer ID| 
| Name| Name of zone as seen In-Game| 
| Color ID| Layer ID of encrypted multi-layered image| 
| Continent ID| Which continent is the zone name located| 
| World Grid X,Y| Which world block is the zone name located in| 
| Top Z, Bottom Z| The z height of the zone name| 
| Extra Map Properties | These are properties for the little '+' icon next to towns and castles on the world map| 
| +Button X, Y Position  | position of the + button on the map, based on the image dimensions (not world location)|
| Center X, Y Position | 
| Map Width, Height, Scale, Image Name| If the + button is clicked, Map Image Name is shown based on the width, height and scale entered. This is what happens when you click to zoom into a town map for instance| 

#### Raids
Editing raid zones are very similar to editing hunting grounds. It's a simple location with an NPC assigned. This doesn't have anything to do with the actual raids, this is for the Raids list accessible through the in-game map.

![Raid](https://user-images.githubusercontent.com/76498760/151079987-c69e4251-ec50-4002-9871-5ea8001c5323.png)

| Raids Parameters      | Description |
| ----------- | ----------- |
| ID | Unique integer ID| 
| NPC| Target NPC Raid - Click to change| 
| NPC Level| NPC Level| 
| Affiliated Area ID| 
| Position| X,Y,Z position of the raid|
| Description| Raid description| 


I genuinely look forward to seeing what you guys will create!

Cheers,
- Bumble / Claessic
