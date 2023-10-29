# ![Logo](https://raw.githubusercontent.com/exilekit/PoESplit/main/PoESplit/MarkdownResources/ExperimentedUniqueSymbol.png) PoESplit

## About
PoESplit is a timer program for players, solo speedrunning the Path of Exile campaign, with a multi-monitor configuration.

![IdealSetup](https://raw.githubusercontent.com/exilekit/PoESplit/main/PoESplit/MarkdownResources/IdealSetup.png)

## Features

### Realtime Tracking
The `Show Map` button displays a minimap that tracks your location and displays area levels (and relative XP percentages) color-coded to your character level.

* Red - The area is lower level than you.
  * It is never ideal to be in an area that is lower level than you.
  * *You are overleveled; skip more mobs!*
* Yellow - The area is much higher level than you (there is an XP penalty). 
  * The relative XP percentage that is displayed takes into consideration any XP penalties that apply; an area color-coded yellow may still be the best area for XP.
  *  *You may be skipping too many mobs!*
* Green - The area is neither lower level nor is there an XP penalty.
  * *Your pacing is great!*

![RealtimeTracking](https://raw.githubusercontent.com/exilekit/PoESplit/main/PoESplit/MarkdownResources/RealtimeTracking.png)

### Ease of Use
The first time PoESplit is ran, you will need to "connect" it to the Path of Exile `Client.txt` log file. No further configuration is required; you are ready to `Begin Run`. Users experienced with Path of Building will notice a familiar look and feel.

![Interface](https://raw.githubusercontent.com/exilekit/PoESplit/main/PoESplit/MarkdownResources/Interface.png)

### Export to CSV
The `Export to CSV` button allows you to export your run to a format that can be imported by spreadsheet editors (like Google Docs, Microsoft Excel, or OpenOffice).
![CSV](https://raw.githubusercontent.com/exilekit/PoESplit/main/PoESplit/MarkdownResources/CSV.png)

## Download
Windows Installers are provided on the [releases](https://github.com/exilekit/PoESplit/releases)
 page. We do not provide an installer for Linux or OSX platforms.

## How does it work?
PoESplit is an executable app that runs independently from the game, and monitors the game's log files. The act of monitoring the game's log files is explicitly permitted by the game's [Third-Party Policy](https://www.pathofexile.com/developer/docs) so long as the app discloses how that information is being used.

The game's log files contain (in real-time) when the player has performed specific actions (such as leveling up, changing areas, or allocating passives). PoESplit uses that information to provide on-screen metrics of your run.

It is the intent of the developer(s) of PoESplit (and its parent project, ExileKit) to adhere to the game's [Third-Party Policy](https://www.pathofexile.com/developer/docs) and [Terms of Use](https://www.pathofexile.com/legal/terms-of-use-and-privacy-policy).