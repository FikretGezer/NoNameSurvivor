# [No Name Survivor](https://fikretgezer.itch.io/no-name-survivors)
## Screenshots
<div align="center">
  <img src="https://github.com/FikretGezer/NoNameSurvivor/assets/64322071/64c53fc5-658e-4e04-86ff-69b687758d30" width="400" height="220">
  <img src="https://github.com/FikretGezer/NoNameSurvivor/assets/64322071/267c9de7-248e-45d5-8a89-d99964ce0a76" width="400" height="220"> 
  <img src="https://github.com/FikretGezer/NoNameSurvivor/assets/64322071/c448d012-422f-4867-a0b3-ca09632a84aa" width="400" height="220"> 
  <img src="https://github.com/FikretGezer/NoNameSurvivor/assets/64322071/123d6950-2f22-4e27-aca9-8eaa0bd61c07" width="400" height="220"> 
</div>

## What is this game about?
This game is the result of my excessive playtime with a game called Brotato. Basically, this is a shoot'em up game where the goal is to survive 10 consecutive rounds of enemy waves and upgrading the stats using the money dropped by defeated enemies and equip new guns. At the end of each round, players have a rest period during which they can select new weapons or items to enhance their stats throughout the run. Players can choose only 6 weapons, and these weapons cannot be upgraded, so players must choose wisely. Additionally, each weapon is represented by a new character, spawning on the map when selected.

There are three types of enemies in the game: normal ones, dashers, and shooters:
* Normal enemies attempt to get close to the characters to inflict damage.
* Dashers try to approach characters and dash at them if they get close enough.
* Shooters follow the players until they are in range and then shoot with a rifle.

Also the spawn rates of enemies change with each round.

## Background and Development
Creating No Name Survivor taught me various skills, including implementing OOP, creating a mini-map, developing a dynamic store, and incorporating stats that affect the players. To clarify further:
* I used OOP to create different enemies and guns, avoiding code repetition and allowing easy modification of parameters or functions within one script used by similar objects. For instance, I centralized code for enemy behavior in one script and inherited enemy types from it. The use of abstract functions facilitated easy control over being hit by these enemies.
* While not essential, I implemented a mini-map to explore its functionality and provide players with a vision of their surroundings.
* Weapons and items are essential in these types of games. I utilized Scriptable Objects to define various weapons and items. After each round, a store appears, offering players options to strengthen their characters.

<div align="center">
  [![Everything Is AWESOME](https://img.youtube.com/vi/StTqXEQ2l-Y/0.jpg)](https://www.youtube.com/watch?v=StTqXEQ2l-Y "Everything Is AWESOME")
</div>
