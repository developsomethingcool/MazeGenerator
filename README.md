# PHANTOMS LABYRINTH
- Unity Version => 2021.3.25f1

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

1. ## Player Movement
    - **Walking and Running**: The player can move freely in the game world using standard walking controls. Additionally, they have the option to increase their movement speed by running, allowing them to traverse the environment quickly.
        - Walking (WASD)
        - Running holding "shift"

    - **Jumping**: The player can perform jumps to overcome obstacles and reach higher platforms. They can execute single jumps or perform double jumps (after picking up the right loot) for enhanced vertical mobility.
        - Jump ("space")

    - **Shrinking**: The player has the  ability to shrink their character's size . This shrinking ability might grants them access to hidden or narrow passages, enabling them to discover secret areas and find alternative routes.
        - Shrinking ("C")

    - **Wallrunning**: To add an extra layer of agility, the player can execute wallruns. When near a suitable wall or surface, they can initiate a wallrun, defying gravity and traversing horizontally or vertically along the wall. This allows for dynamic and acrobatic movement through complex level designs.
        - Being next to a wall and pressing "Q"

    The combination of these movement functionalities provides players with a versatile set of options to navigate the game world.

2. ## Loot System

    - These pickups are dropped by defeated enemies, with each item having a specific chance to drop. The following seven pickups are available:

    1. **Armor** Up: This pickup increases the player's armor, providing additional protection against incoming damage. It has a 25% chance to drop upon defeating an enemy.
        -Armor up is represeted by a Hotdog

    2. **Critical Chance Up**: By acquiring this pickup, players enhance their critical hit chance. This pickup has a 50% chance to drop when an enemy is defeated.
        - Critical Chance Up is represented by a Cherry

    3. **Damage Up**: This pickup empowers the player's attacks, granting a temporary increase in their base damage. It has a 100% chance to drop upon defeating an enemy.
        - Damage Up is represented by a Block of cheese

    4. **HP Up**: Upon collecting this pickup, the player's maximum health pool is expanded. It has a 100% chance to drop when an enemy is defeated.
        - HP Up is represented by a Hamburger

    5. **Health Regen Up**: This pickup augments the player's natural health regeneration rate, allowing them to recover health more quickly over time. It has a 50% chance to drop upon defeating an enemy.
        - Health Regen Up is represented by a Banana

    6. **Jump Up**: By acquiring this pickup, the player's jumping ability is enhanced. They gain increased vertical height or distance. It has a 10% chance to drop when an enemy is defeated.
        - Jump Up is represented by an Olive

    7. **Lifesteal Up**: This pickup grants the player a lifesteal effect on their attacks. When dealing damage to enemies, a portion of the inflicted damage is restored to the player's health. It has a 25% chance to drop upon defeating an enemy.
        - Lifesteal Up is represented by a Watermelon

    - Each defeated enemy drops one of these pickups, adding an element of excitement and surprise to the gameplay. The loot that apears is randomly picked out of the loot which is elegable by having a higher loot chance then the number rolled by a d100

3. ## Enemy Types and AI Behavior

    The Unity project features two distinct enemy types, each with unique characteristics and behaviors. The enemies in the game are equipped with different weapons and utilize an AI system that governs their actions based on three states: Patrolling, Chasing, and Attacking.

    1. Sword Enemy

    The Sword Enemy is a formidable adversary armed with a sword. They possess moderate strength and are skilled in close combat engagements. Here's an overview of their AI behavior:

    - **Patrolling**: The Sword Enemy roams designated areas or follows predefined paths, ensuring the security of their assigned territory.

    - **Chasing**: When the Sword Enemy detects the player's presence, they enter the Chasing state, pursuing the player with determination.

    - **Attacking**: Once in close proximity to the player, the Sword Enemy  their sword, requiring players to fight back.

    2. Dual Axes Enemy

    The Dual Axes Enemy is a smaller and more agile adversary, equipped with two axes. They rely on nimble movements to outmaneuver their opponents. Here's an overview of their AI behavior:

    - **Patrolling**: The Dual Axes Enemy scouts their assigned areas and maintains a vigilant presence, utilizing their agility to navigate the environment effectively.

    - **Chasing**: When the Dual Axes Enemy spots the player, they swiftly transition into the Chasing state, relentlessly pursuing the player.

    - **Attacking**: In the Attacking state, the Dual Axes Enemy executes a flurry of agile and rapid attacks, challenging the player.

    The AI-driven behaviors of both enemy types enhance the challenge and variety of encounters, requiring players to adapt their strategies accordingly. Players must utilize their combat skills and situational awareness to overcome the adversaries they encounter.


4. ## Random Maze Generation
    The Unity project features a random maze generation system that creates unique mazes for players to navigate. The maze generation process follows the following steps:

    1. **Creating the Maze Cells**: A predefined number of maze cells is generated, with each cell initially containing all four walls.

    2. **Grid-based Movement System**: The project implements a grid-based movement system where the player character traverses through a grid of walls. The maze consists of two types of cells: valid cells and non-valid cells.

    3. **Valid and Non-Valid Cells**: Valid cells are those that the player character can visit, while non-valid cells include cells that have already been visited or cells located at the edge of the map. If the player character attempts to move to a cell outside the map boundaries or has already visited a cell, it is considered non-valid.

    4. **Exploration Algorithm**: To explore the maze, a simple algorithm is employed. The algorithm traverses through the maze, marking visited cells and identifying dead ends where there are no valid neighboring cells to move to.

    5. **Retracing Steps**: When the algorithm encounters a dead end, it backtracks its steps until it finds a valid neighboring cell. Once a valid neighbor is found, the algorithm resumes its exploration from that point.

    6. **Destruction of Walls**: As the player character moves through the maze, it destroys the walls it crosses. This dynamic wall destruction results in a unique labyrinth every time the maze is generated.

    By utilizing this random maze generation system, players can enjoy exploring new and diverse mazes in each playthrough, enhancing the overall gameplay experience.

5. ## Trap Generator

    The Trap Generator is a feature in the game that enhances the labyrinth experience by introducing randomly generated traps. This feature adds excitement and unpredictability to gameplay, ensuring a balanced and engaging challenge for players. Here are the key details of the Trap Generator:

    - **Random Trap Generation**: The Trap Generator randomly generates three types of traps throughout the labyrinth. The positions of these traps are determined based on the labyrinth size and difficulty level, ensuring an appropriate distribution of traps for the gameplay experience.

    - **Varied Trap Types**: Players can encounter various types of traps, each with its own unique effects and characteristics. These traps may include pitfalls, spikes, or trigger-based traps, adding variety and keeping the gameplay fresh and unpredictable.
        - Obstacle one and two: This type of trap presents a challenge for the player, acting as a physical barrier that blocks their path. The player has to jump over it. Any collision with these traps will slow down the player.
        - Lava Trap: The lava trap is an quit dangerous element spread throughout the game. Falling into the lava leads to instant death.Avoiding the hot lava becomes essential for survival.
    - **Balanced Game Design**: The number of traps generated by the Trap Generator is adjusted according to the labyrinth size and difficulty level. This ensures that the game remains balanced, challenging, and fair for players, providing an enjoyable and immersive experience.

    - **Obstacles and Challenges**: The presence of traps introduced by the Trap Generator adds obstacles and challenges to the labyrinth. Players need to navigate carefully, avoiding or disarming traps to progress further. This feature makes the game more interesting, requiring strategic thinking and quick reflexes from the players.

    By incorporating the Trap Generator, the game creates a dynamic and immersive environment for players to explore. The randomly generated traps add an element of surprise and challenge, keeping players engaged and enhancing the overall gameplay experience.

6. ## Goal Area

    The Goal Area is a key element in the game that plays a crucial role in the player's journey towards victory. This area serves as the ultimate objective for the player to reach, marking the completion of the game. Here's an overview of the Goal Area functionality:

    - **Activation Condition**: To activate the Goal Area, the player must fulfill a specific condition, which involves destroying a sufficient number of enemies throughout the game. This condition adds an additional challenge and incentive for players to engage in combat and overcome adversaries. When the goal area aktivates it changes it's color from red to green indicating the change and that it is now aktive.

    - **Accessing the Goal Area**: Once the activation condition is met, the Goal Area becomes accessible to the player. It serves as a designated location within the labyrinth that players must navigate towards to complete the game successfully.

    - **Objective and Victory**: The player's primary task upon reaching the Goal Area is to navigate the labyrinth and make their way to this specific area. Successfully reaching the Goal Area signifies the player's victory and accomplishment in the game.

    The inclusion of the Goal Area adds a sense of purpose and progression to the gameplay experience. By establishing a condition for activation and a clear objective, players are motivated to engage in combat, strategize their approach, and navigate the labyrinth effectively.


7. ## Menu 
    The game features four distinct menus that provide players with various options and interactions. Each menu serves a specific purpose and enhances the overall user experience. Here are the details of each menu:

    ## Main Menu

    The Main Menu serves as the entry point to the game and offers the following options:

    - Game Name: The Main Menu prominently displays the game's name, providing a visual identity and setting the tone for the gameplay experience.
    - Play Button: The Play Button allows players to start a new game or continue from a saved game, initiating the gameplay and immersing players in the maze-solving adventure.
    - Options Button: The Options Button opens the Options Menu, allowing players to customize certain game settings, such as maze size and difficulty level, to tailor the gameplay experience to their preferences.
    - Quit Button: The Quit Button provides an option to exit the game gracefully, allowing players to easily close the application when desired.

    ## Options Menu

    The Options Menu allows players to adjust specific game settings and fine-tune their gameplay experience. It offers the following features:

    - Maze Size Slider: The Maze Size Slider enables players to adjust the size of the maze they will be navigating. The slider typically has five resting points, allowing players to choose between different levels of complexity and challenge.
    - Difficulty Slider: The Difficulty Slider allows players to customize the game's difficulty level. With five resting points, players can select their preferred level of challenge, balancing the gameplay experience to suit their skills and preferences.

    ## Death Screen

    The Death Screen is displayed when the player character fails to successfully navigate the maze. It provides the following options:

    - Restart: The Restart option allows players to restart the maze from the beginning, providing an opportunity to improve their performance and overcome challenges.
    - Menu: The Menu option returns players to the Main Menu, providing an alternative path to explore different game modes or settings.

    ## Victory Screen

    The Victory Screen appears when the player successfully completes the maze, showcasing their accomplishment and offering the following options:

    - Congratulations: The Victory Screen displays a congratulatory message, celebrating the player's achievement and creating a sense of accomplishment.
    - Time Taken: The Victory Screen also reveals the time taken to complete the maze, providing a benchmark for players to challenge themselves and improve their speed.
    - Main Menu: The Main Menu option allows players to return to the Main Menu, providing an opportunity to explore different game modes, restart the game, or adjust settings.

    The menu system in the game enhances user engagement and provides convenient access to game features, settings, and progress. With intuitive navigation and clear options, players can easily navigate the menus, customize their gameplay experience, and continue their journey through the maze-solving adventure.

8. ## HUD Functionality

    The Heads-Up Display (HUD) in this Unity project provides players with important information and visual feedback to enhance their gameplay experience. The HUD includes the following elements:

    ## Health Indicator (Bottom Right)

    Located at the bottom right corner of the screen, the Health Indicator displays the player's current health status. It provides a visual representation of the player's remaining health, allowing players to quickly assess their well-being during intense gameplay moments. The Health Indicator is designed to be intuitive and easy to understand, ensuring players can monitor their health at a glance.

    ## Current Goal Display (Top Left)

    At the top left corner of the screen, the Current Goal Display showcases the current objective or goal that players need to accomplish. This element provides players with clear guidance and helps them stay focused on their objectives throughout the game. Whether it's a mission objective, a quest marker, or a specific task, the Current Goal Display ensures players are aware of their immediate goals.

    ## Timer (Counting Up) (Top Right)

    Positioned at the top right corner of the screen, the Timer displays a continuously updating count, measuring the elapsed time during gameplay. The Timer can be used to create time-based challenges or provide players with a sense of urgency. It adds an additional layer of excitement and competitiveness, allowing players to track their progress and strive for better completion times.

    The HUD elements are strategically placed on the screen to minimize distractions while providing crucial information to players. The Health Indicator, Current Goal Display, and Timer work together to enhance player immersion, improve situational awareness, and create a dynamic gameplay experience.

9. ## Camera Functionality

    The camera system in this Unity project is based on Cinemachine, offering smooth and immersive gameplay perspectives. The camera allows players to seamlessly switch between a third-person view and a first-person view by pressing the "O" button. Here are the details of the camera functionality:

    ## Third-Person View

    In the default third-person view, the camera is positioned behind the player character, providing a broader perspective of the game world. This perspective allows players to have a better sense of their surroundings and enhances situational awareness. The camera smoothly follows the character's movements, ensuring a smooth and immersive experience.

    ## First-Person View

    By pressing the "O" button, players can instantly switch to a first-person view. In this mode, the camera is positioned at the character's eye level, providing a more immersive and intimate perspective. This view allows players to experience the game world from the character's point of view, bringing them closer to the action.

    The ability to toggle between third-person and first-person views adds versatility to the gameplay experience. Players can choose the perspective that best suits their playstyle or adapt their view based on specific situations. Whether it's exploring the Maze in third person or engaging in intense combat sequences in first person, the camera system offers a seamless transition and enhances the overall immersion of the game.


## Trouble we encountert on the way

- When we randomly generated traps, multiple traps could be generated in the same position or they could overlap with Goal area.
- sometimes problem did arise out of nowhere with no concernable reason. But the easy fix was to save and start Unity again.
- Problems of the physics engine. Sometime ridigeBodys wouldn't fall down even though theye were effected by gravity.
- While merging the branches we crashed our solution multiple times and had to revert multiple commits.
- While generating scenery some buttons expanded and covered the whole script, so it was not easy to find out why some buttons did not work properly.
- Problems with the unity animation system. But when looking online we are not the only ones.
- Porting assably of scripts and gameobjects from one unity Project to another.
- In general minor changes in a code or the Unity interface could lead to a problem, which took a lot of time to identify but only a little to fix.


## How to play

1. Clone the repository: Start by cloning the repository to your local machine using the following command:

"git clone <repository-url>"

2. Build and run the game: Once you have the repository cloned, open the project in your preferred game development environment (e.g., Unity) and build the game. Run the built executable to launch the game.

3. Objective: The objective of the game is to navigate through the labyrinth, overcome various obstacles and traps, and reach the goal area to win the game. Pay attention to the game's specific rules and mechanics, as they may vary depending on the project.

4. Controls: Familiarize yourself with the game's controls to effectively navigate the game world. These controls may include standard movement controls (e.g., WASD keys), special abilities (e.g., jumping, shrinking), and interaction with objects and characters in the game.

## Guidelines for Contributing and Collaboration

1. Fork the repository: If you wish to contribute to the project or collaborate with others, start by forking the repository to your GitHub account. This will create a copy of the project under your account that you can freely modify.

2. Create a new branch: Before making any changes or additions, create a new branch in your forked repository. This helps keep your changes separate from the main codebase and makes it easier to track and manage modifications.

3. Make changes and additions: Make your desired changes or additions to the project code, assets, or documentation. Follow best practices, adhere to coding guidelines, and ensure that your modifications align with the project's goals and objectives.

4. Commit and push changes: Once you are satisfied with your modifications, commit your changes to the branch and push them to your forked repository.

5. Create a pull request: When you are ready to merge your changes into the main project, create a pull request. Provide a clear description of the changes you made, their purpose, and any relevant information that helps reviewers understand your contribution.

6. Collaborate and iterate: Collaborate with other contributors and maintainers through the pull request. Address any feedback or suggestions provided by reviewers and iterate on your changes as necessary. This process ensures that the project maintains a high standard of quality and consistency.

By following these guidelines, you can contribute to the project, collaborate effectively with others, and help shape the game into an even better experience for players.

## Credits

Team Members: Hendrik Kremer, Oleksandr Pometun, Asma Akter



