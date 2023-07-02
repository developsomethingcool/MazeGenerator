# Maze Game

Welcome to the Maze Game repository! This is a simple maze game implemented in Unity. Navigate through the maze, kill enemies, and try to find the exit!

## Features

- Randomly generated mazes for a new experience every time.
- Enemies generated at random positions


# PHANTOMS LABYRINTH

Welcome to the game "Phantom's Labyrinth"! In this thrilling adventure, players are tasked with finding their way through a treacherous maze filled with cunning traps and formidable enemies. Armed with their trusty sword, players must eliminate all enemies to successfully escape the labyrinth. The challenge lies in completing the mission as quickly as possible, testing the player's speed and strategic thinking. Are you ready to take on the Phantom's Labyrinth and prove your mettle?

## Table of Contents

- [Project Structure](#project-structure)
- [Functionality and Features](#functionality-and-features)
- [Code Documentation](#code-documentation)
- [Assets and Resources](#assets-and-resources)
- [Troubleshooting and FAQs](#troubleshooting-and-faqs)
- [Contributing and Collaboration](#contributing-and-collaboration)
- [Credits and Acknowledgments](#credits-and-acknowledgments)
- [Appendices](#appendices)


## Project Structure

In the asset folder of our project one can find several folders

1. Loot - This folder holds everything needed for loot iteams, these include:
    - models for the loot
    - Loot script which defines the scriptable object which is used as a foundation for the loot
    -the scripable objects themself
    -and the loot-profab
2. Matirials - This folderholds the matirials which are used in the game for traps, the player, enemys and the maze itself

3. NavMeshComponets - This is an extention not present in the unity-Asset stor. In this project it is used to creat the NavMeshSurface after generating the Maze-

4. Own_Animations - This folder holds basic animations and animatorControllers for both the player and enemys in thir respective folders.

5. Prefabs - This folder holds all the prefabs which are used in the came
    - Camera - A prefab of the Camera 
    - Enemy - The prefabs of the two enemy types aswell as thier weapons
    - Mazeprefab - Prefabs for the maze, including the normal maze cell and traps
    -Player - a prefab of the player and the Canvas used as ingame UI

6. Scene - This folder holds the diffrent Scences used in the project.
    - MenuScene - This scene is used for the diffrent menues
        - Main Menu
        - Options Menu
        - Game Over Screen
        - Victory Menu
    - GameplayScene - This Scene is where the game is aktually happening

7. Scripts - This folder holds all the scripts
    - Scripts_requiered_for_Enemy This folder holds the scripts which are needed for the enemy to work
        -   Billbord script which rotates the enemys healthbar towards the player
        - LootBag and Collion Detection which are used for the Loot dropt by th enemy
        - Enemy_AI and FieldOfView which provide the funktionality of the Enemy
        - And enemy Renderer which spawns the enemys
    - Scripts_requiered_for_Maze This folder holds the scripts nessesary for the maze to be created and to be finished
        - Maze Generator, Maze Renderer, Trap Generator, TrapRenderer, GoalAreaRenderer are sued to render the Maze
        - Data_Percistence, SliderScript, TextModifyer, TimerSelf, UniqzNumberGenerator provide funktionality to the maze and mazeeffecting menues
        - MainMenu, MenuController, RestartMe are used for the menu controll
    - Scripts_requiered_for_Player This folder holds the two scripts unique to the player
        - PlayerMovment Script used for multiple funktionalitys but mainly used for the movment of the player
        -ThirdPersonCam Script which manages the diffrent Camera Types in the game(more explained in the functionality)
    -Scripts_shared_byPlayer_and_Enemy This folder holds scripts which add functionality to both the player and the enemy
        - AttributManagaer This script manages the attributes like health, attackdamage, armor. etc. of player and enemy.
        -Healthbarscript This script is resposible for changing the health displayed by the healthbar
        -Weapon This script manages the attack funktionalitys of the player and of the enemys

8. SkyBox_Examples - In this folder multiple options of skyboxes are stored. But most importantly the one in use called "SkyBox_InUse"

9. Sprites - This folder holds the sprite for the healthbar

10. TextMeshPro - used for all the texts displayed in the game

11. GameManager - This script has a special place as like the name says it manages the game(or the game menu)

12. Gold - A gradiant used for text



## Functionality and Features

A description of the project's major functionalities and features.

## Code Documentation

Detailed explanations of key scripts and code components.

## Assets and Resources

A list and description of third-party assets, libraries, and resources used.

## Troubleshooting and FAQs

Common issues, troubleshooting tips, and frequently asked questions.

## Contributing and Collaboration

Guidelines for contributing to the project and collaborating with others.

## Credits and Acknowledgments

Acknowledgment of individuals and resources that contributed to the project.

## Appendices

Additional information, diagrams, or technical details.


