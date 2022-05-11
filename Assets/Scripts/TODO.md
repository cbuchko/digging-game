# TO-DO:
git

# TO-DO (in-scope):
tighten player movement so accidental wall digging isn't as common
 - maybe you have to hold against the wall for a little bit before a dig registers
first frame of dirt cracking is too short


# Bugs to-do:
double digging is easy to reproduce, pressing two keys at once
tutorial showing every reload for itch.io. Works for executable though ???
full screen fucked on itch.io

## long term to-do:

Fuel cost changes as tank size changes
    - price updates on fuel store
    - make sure refill calculation scales correctly as cost scales
Drill resistance, deeper takes longer. 
more sophisticated random generation
    - ore clustering
    - spawning rules (depth, etc.)
better player model
upgrades (fuel, drill speed etc.)
terrain changes (deeper you discover new biomes)
npc areas
real interfaces for shop/fuel/inventory etc.
balance the fuel/money system, how much should a full tank be (how much should one dig cost?), how big should tank be etc.
more ores (coal?)
inventory (instead of having resource counts directly in UI)
impassable obstacles (boulders)
items? (Dynamite, teleporters)
start menu
save/load
settings
pause menu
restart
better soundtrack
UI/UX for showing that fuel costs money
    -notifcation on how much you spend etc.

# ???:
hull (health) + fall damage 
    - do we need this? because I don't think it makes the game anymore fun
    - might need health later on for other damage sources, but fall damage sounds kind of unnecessary
treasures/artificats
    - probably too RNG based
player movement animation
    - tried this and the player moves too fast to even see the animation, but might be worth revisiting.

# ...unless?
Make it a tufffff speedrunning game with some hehe xd humor. Crab game type scuffness.
Shop NPC that is super quirky and says funny stuff. Very self-aware and breaks fourth wall. Build relationship as you progress farther.

# DONE:

World Borders
Jerry can image
Depth Indicator
Low fuel warning
game over screen
basic random generation
a basic player model
fix depth calculation for new block sizes
down player model
fix grounded detection
particle effects for digging
block destruction animation (cracking)
drill digging animations
digging sound effects (drill sounds, destroy sound)
background
clean up UI
fire particle for flying
flying sfx
procedural generation
consider refining procedural generation, when does the next block spawn, how much is in a block, etc.
have background extend with procedural generation
assets 
UI feedback for collection resources etc.
quality of life for low fuel warning
death stuff
sfx
animations
tutorial/instructions
music

# BUGS FIXED:
fix screen sizing so it looks good on all sizes
fix dig option detection so the correct block is always dug
fix player positioning so it isn't a pain to center self in a block (force centering while drilling like real game?)
depth calculation (find a better way to do this instead of hardcoding)
final depth score should always be accurate (digging down doesn't update score right away if you die on it)