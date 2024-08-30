# Spyro: A Hero's Tail - Trigger Inspector
Windows Forms Tool that visualizes all trigger-related data in maps in Spyro: A Hero's Tail, complete with a gamescript decompiler. Compatible with all retail versions.

## What is a Trigger?
Every map in the game has a list of static objects that make things happen when the player enters its activation range. These are called *Triggers*, and they are essentially the building blocks for anything dynamic in a level.

A trigger has the following information:
* Type/SubType: Defines the trigger's general behavior.
* Game Flags
* Trigger Flags: Defines what optional information this trigger has.
* Position/Rotation/Scale
* Range (either defined in the trigger itself or assigned a default one from the map.)
* (Optional) GFX Hashcode: The asset used for the visual component of the trigger.
* (Optional) Geo Hashcode: The geofile (.edb file) that the trigger needs loaded.
* (Optional) Tint: Color to tint the visual component of the trigger (RGBA).
* (Optional) GameScript Index: The index of the gamescript that the trigger uses.
* Data: 16 optional slots for any data the trigger might need.
* Links: 8 optional slots for any triggers it is linked to.

## What Is A GameScript?
For more information, read about the scripting language as it was implemented in <i>Sphinx and the Cursed Mummy</i> [here](https://sphinxandthecursedmummy.fandom.com/wiki/Sphinx_Scripting_Language).

A trigger can have specific behavior programmed into it via *GameScripts* written in *EngineX's scripting language*, which had its first use in <i>Sphinx and the Cursed Mummy</i>. GameScripts are syntactically very similar to BASIC, so it's easy to write and read. This tool will attempt to decompile a GameScript's bytecode, though the original syntax can only partially be determined, so it will be a rough estimate of how the source script looked.

If you want to know what a command does, head to this [wiki page](https://github.com/BrinchEbsen/AHT_TriggerInspector/wiki/Game%E2%80%90Specific-Commands-for-Spyro%3A-A-Hero%27s-Tail) for more information on the commands used in Spyro: A Hero's Tail.

## Internal Globals
More on these [here](https://sphinxandthecursedmummy.fandom.com/wiki/Sphinx_Scripting_Language#Internal_Globals).<br>
There are 24 global variables that are always present no matter the script. Since only a few of them are used in any of Spyro AHT's scripts, not much is known about them, but they're generally used for special stuff like the result of a YesNo box when it's closed or the message value when the `OnMessage` procedure is run. These globals are marked grey in the name editor window, but you can still rename them as you see fit.

## How To Use
To open a map file, click the "Load File" button or drag n' drop the file into the window.

To find a map file, the game's filelist must first be extracted. [Eurochef](https://github.com/eurotools/eurochef)'s command-line version can be used for this.

This tool will only open .edb files that contain maps. The main levels have a naming format: "realm" followed by the realm number (1, 2, 3...), then the level number (a, b, c...). For example, Gloomy Glacier is level "b" in the 3rd realm, thus its file name is "realm3b".<br>
"Link maps" which connect levels to mask loading screens also have a naming format: "r" followed by the realm number, then "link" and then the two levels that are connected (a, b, c...). For example, the elevator leading up from Coastal Remains to Cloudy Domain is in realm 2 and connects levels "a" and "c", thus its file name is "r2linkac".<br>
There are also many test levels labelled "test_" followed by the initials of the developer who used it.

.edb files can contain multiple maps, and when a file is loaded, the list of maps in the file is displayed in the top-left. Double-click a map to fill the list below with all the triggers found in the map.<br>
Click on a trigger to populate the right side of the window with information about it.<br>
Any trigger linked to the currently viewed trigger will be coloured in the trigger list.<br>
If the trigger references a gamescript, you can click "View GameScript" to open the Script Viewer window.

In the Script Viewer, a textbox will contain the tool's best attempt at decompiling the referenced gamescript. The original bytecode can also be viewed by opening the "Raw Bytecode" tab.<br>

Click "Edit Names" to open the name editor, where the auto-generated names for variables, procedures and labels can be changed manually to better reflect what's happening in the code.
When you click "Apply", these names are saved on disk in the `gs_saved` folder (located together with the .exe), and can then be loaded by the tool later. If this folder doesn't exist, the tool will automatically create it.<br>
<i>Note: The names of procedures are unique in that they're stored in the script data, however they are cut off after 9 characters. When the decompiler is generating the names, if two or more procedures end up with the same name, it will add a number onto it to make it distinguishable.</i>
