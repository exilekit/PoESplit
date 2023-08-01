# ![Logo](https://raw.githubusercontent.com/exilekit/PoESplit/main/PoESplit/MarkdownResources/ExperimentedUniqueSymbol.png) PoESplit

## About
PoESplit is a timer program for speedrunners playing the Path of Exile campaign. It is intended to help players identify inefficiencies during the course of their run.

PoESplit was inspired by other timer programs, like [LiveSplit](https://github.com/LiveSplit/LiveSplit), but is specialized for Path of Exile.

***Warning: This project is in its infancy; it is not ready for use by the general public***

## Download
Windows Installers are provided on the [releases](https://github.com/exilekit/PoESplit/releases)
 page. We do not currently provide an installer for Linux or OSX platforms.

## How does it work?
PoESplit is an executable app that runs independently from the game, and monitors the game's log files. The act of monitoring the game's log files is explicitly permitted by the game's [Third-Party Policy](https://www.pathofexile.com/developer/docs) so long as the app discloses how that information is being used.

The game's log files contain (in real-time) when the player has performed specific actions (such as leveling up, changing zones, or allocating passives). PoESplit uses that information to provide on-screen metrics of your run.

It is the intent of the developer(s) of PoESplit (and its parent project, ExileKit) to adhere to the game's [Third-Party Policy](https://www.pathofexile.com/developer/docs) and [Terms of Use](https://www.pathofexile.com/legal/terms-of-use-and-privacy-policy).

## What's on the agenda for features?
 * Capture `WM_WINDOWPOSCHANGING` or `WM_EXITSIZEMOVE` so that resizing the map window perserves aspect ratio.
 * Display a broken line for map connections that are conditional.
    * Perhaps even repair the broken line when players reach quest waypoints.
 * Automatically detect the Path of Exile installation directory (see `find_poe.cpp`)
 * Character auto-detection logic
   * It is a dropdown list that is only populated by characters reaching LVL2 in the Twilight Strand.
   * Characters populated, are tracked for the rest of the game.
   * When a tracked character levels, it becomes top of the list, and is the assumed character.
 * Character EXP/LVL logic
   * Show what the base XP is for monsters in a zone that matches your character level, and then compare that to the base XP for monsters at the level of the zone you're actually in.
 * Vaal Side Areas cause the map to lose track of where you are.