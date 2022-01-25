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
- Above all, thank you to the doctors and nurses who kept me alive these past years. L2H is a passion project, developed before, during and after my fight with leukemia. If you find some weird or bad code, that will also be my go-to excuse.

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
5. If you have any AI files, put them into the "Data/AI" folder.
6. Open L2Homage.exe.

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

---
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

---

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
![Items_Weapon_Text](https://user-images.githubusercontent.com/76498760/150889947-e492d28b-37d1-4b4d-a365-d92c7cfcf2a5.png)
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
| Attack Speed | Milliseconds delay between attacks. Lower is faster| 
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
![Items_Armor](https://user-images.githubusercontent.com/76498760/150892233-4ec5777d-b950-4c6d-aa84-00bab658efca.png)
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
![Items_Etc](https://user-images.githubusercontent.com/76498760/151055217-71a625b9-ae90-43b5-a55c-a1a2a29f6489.png)
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
![Items_Set](https://user-images.githubusercontent.com/76498760/150892261-415d69da-6e74-4aa4-adc4-555ec1e5d10d.png)
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
![Multisell](https://user-images.githubusercontent.com/76498760/150894788-59b50377-cfe1-4c33-8542-78767b2ed474.png)

---

### NPCs
![NPCs](https://user-images.githubusercontent.com/76498760/150894909-ddf00150-7a94-4134-b7d4-7374ce43d17f.png)

---

### Recipes
![Recipes](https://user-images.githubusercontent.com/76498760/150894935-9411daa7-093e-4579-b6ff-105ed17a4074.png)

---

### Skills
![Skill](https://user-images.githubusercontent.com/76498760/150895042-f9d9fa74-2627-4149-8454-cc62b2614df9.png)

#### Skill Batch Editing
![Skill_Batch](https://user-images.githubusercontent.com/76498760/150895078-e9a7f941-fc2a-4e69-93c6-f500c9c14dcd.png)

---

### System Text
![]

---

### Zones
![Choose](https://user-images.githubusercontent.com/76498760/150895156-ae4b25f9-7967-48e6-9e6d-7138b6f00003.png)
![Spawn](https://user-images.githubusercontent.com/76498760/150895204-fa0507ed-a222-4223-84f1-cafdd2619010.png)
![Hunting](https://user-images.githubusercontent.com/76498760/150895166-9c2fd813-e163-4f8f-8ea1-afdae9b4d82f.png)
![Name](https://user-images.githubusercontent.com/76498760/150895179-3223bb2b-fa2f-4dc3-8b21-8c2a0c82130c.png)
![Raid](https://user-images.githubusercontent.com/76498760/150895195-6941f9e3-839a-49ee-8b5b-5517d0b5ff98.png)
