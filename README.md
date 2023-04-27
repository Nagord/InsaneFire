# InsaneFire

## Information
For Game Version 1.2.03  
For PML Version 0.11.1  
Mod Version 1.5.1  
Developed by: Dragon  
Host/Client Requirements: Host | Clients get better experience when installed

Support the developer: https://www.patreon.com/DragonFire47


## Installation 
- have PulsarModLoader installed  
- go to \PULSARLostColony\Mods  
- add the .dll included with this package

## Features
- Modifies fire cap from 20 to 10,000.  
- Makes fire nodes spread more than once, as opposed to once per node, removing the snake effect  
- Modifies oxygen consumption per fire.  
- Syncs oxygen consumption between players with mod.  
- Provides GUI for settings management  
- Provides Commands for settings management  
- Allows toggling between moddified fire and vanilla fire  
- Saves and loads settings automatically.

## Usage
Mod works by just being installed, and can be fine-tuned with commands or via the GUI.
Command and subcommands are not case-sensitive and can be shortened to the capitalized letters.

### Commands: (All commands and subcommands can be shortened to their capital letters.)  
/InsaneFire [Subcommand] [value (depends on command)] - Controls Subcommands

### SubCommands:  
toggle - Toggles mod on and off  
Usage: /if toggle  
limit - Sets fire limit  
Usage: /if limit [value]  
O2Rate - Modifies o2 consumption rate. 1 is 1x consumption rate, 1.5 is 1.5x, etc.  
Usage: /if toggle [value]  

## Common issues
- Fires may de-sync often between host and clients. This is due to how fire is communicated between players in vanilla.  
- O2 may de-sync between host and clients. This is caused by a de-sync of oxygen consumption, and can be fixed by installation of the mod by clients.  
- Sound will start to bug out while a lot of fire exists. This is an issue with sound coming from every fire.
