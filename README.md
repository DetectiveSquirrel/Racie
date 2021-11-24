# LVL Guide

PoE leveling guide plugin for PoeHelper

##### Example Guide
```
[QS a1q1 3]
Talk to Tarkleigh [QT a1q1]

Get The Coast WP [P The Coast]
[G The Mud Flats]
[QS a1q4 2]
Enter The Submerged Passage [G The Submerged Passage]

Get The Submerged Passage WP [P The Submerged Passage]
[WP The Coast]

[G The Tidal Island]
Kill Hailrake [QS a1q5 2]
[XP 4]

```
**Opperation**|**Example**|**Description**
:-----:|:-----:|:-----:
WP|[WP The Coast]|"Take Waypoint" Triggers the Opperation once Local Player's area is the same "The Coast"
G|[G The Mud Flats]|"Go To" Triggers the Opperation once Local Player's area is the same "The Mud Flats"
QS|[QS a1q1 3]|"Queast State" Triggers once the quest "a1q1" state is equal or higher than the wanted stage "3" [0-1-2-3-4] - State ID's of "a1q1"
QT|[QT a1q1]|"Quest Trigger" state 0 = complete (state id's are in reverse order) once "a1q1" is completed its stateid = 0 and opperation triggers
P|[P The Submerged Passage]|Triggers when player has the waypoint for area "The Submerged Passage"
XP|[XP 4]| Triggers once player level is equal or above "4"
