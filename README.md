# Spyro: A Hero's Tail - Trigger Inspector
Windows Forms Tool that visualizes all trigger-related data in maps in Spyro: A Hero's Tail, complete with a gamescript decompiler. Compatible with all retail versions.

## What is a Trigger?
Every map in the game has a list of static objects that make things happen when the player enters its activation range. These are called *Triggers*, and they are essentially the building blocks for anything dynamic in a level.

A trigger has the following information:
* Type/SubType: Defines the trigger's general behavior.
* Game Flags
* Trigger Flags: Defines what optional information this trigger has.
* Position/Rotation/Scale
* (Optional) GFX Hashcode: The asset used for the visual component of the trigger.
* (Optional) Geo Hashcode: The geofile (.edb file) that the trigger needs loaded.
* (Optional) Tint: Color to tint the visual component of the trigger (RGBA).
* (Optional) GameScript Index: The index of the gamescript that the trigger uses.
* Data: 16 optional slots for any data the trigger might need.
* Links: 8 optional slots for any triggers it is linked to.

A trigger can have specific behavior programmed into it via *GameScripts* written in *EngineX's scripting language*, which had its first use in [Sphinx and the Cursed Mummy](https://sphinxandthecursedmummy.fandom.com/wiki/Sphinx_Scripting_Language). This tool will attempt to decompile gamescripts referenced by triggers, though the original syntax can only partially be fully determined, so it will be a rough estimate of how the source script looked.

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

In the Script Viewer, a textbox will contain the tool's best attempt at decompiling the referenced gamescript. The original bytecode can also be viewed by opening the "Raw Bytecode" tab.
