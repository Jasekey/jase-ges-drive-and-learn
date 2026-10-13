# JASE GES DRIVE & LEARN — Unity starter

This folder contains starter C# scripts for a 3D Unity version of the red-and-blue racing + GES JHS learning game. It is not yet a complete Unity project: scenes, 3D models, materials, and UI still need to be created in Unity Editor.

## 1. Create the Unity project
1. Install Unity Hub from the official Unity website and install a current **Unity 6 LTS** editor (or a recent supported Unity version).
2. In Unity Hub, choose **New project → Universal 3D** (or **3D Core**) and name it `JaseGESDriveLearn`.
3. Open the new project.
4. Copy the `Unity/Assets/Scripts` folder from this repository into the new project's `Assets` folder.
5. If prompted, import **TextMeshPro Essential Resources** via **Window → TextMeshPro → Import TMP Essential Resources**.

## 2. Make a simple test scene
1. Create a Plane for the ground and scale it to (3, 1, 20). Add a few cubes along the sides as barriers.
2. Create a Cube named `PlayerCar`, scale it roughly (1.8, 0.8, 3.2), and position it at (0, 0.8, 0). Add a **Rigidbody** and a **Box Collider**. Tag it **Player** (create the tag if necessary). Add `ArcadeCarController`.
3. Create a Camera, add `ChaseCamera`, and drag `PlayerCar` into its **Target** field. Adjust the offset if needed.
4. Create an empty GameObject named `GameManager` and add `RaceGameManager`.
5. Press Play. Use **W/S** or **↑/↓** to accelerate/reverse, **A/D** or **←/→** to steer, and **Space** to brake.

## 3. Add coins and checkpoints
- Coin: create a sphere or cylinder, add a Collider and enable **Is Trigger**, add `CoinPickup`, and tag the player car **Player`.
- Checkpoint: create a cube across the road, enable **Is Trigger** on its Collider, add `LearningCheckpoint`, and make a quiz panel with TextMeshPro labels and four Buttons. Assign those objects to the script fields in the Inspector.
- A correct answer awards coins and learning points. Add more questions and subjects in the Inspector.

## 4. Publish later
When the Unity game is playable, you can build a Windows version in **File → Build Profiles**. A WebGL build may also be possible, depending on your Unity version and project requirements. GitHub stores the project files; it does not run Unity Editor or automatically build the game.

## Current starter controls
- Accelerate: W / Up Arrow
- Reverse: S / Down Arrow
- Steer: A / D or Left / Right Arrow
- Brake: Space

The current scripts use Unity's classic Input Manager for the easiest first setup. If your project is configured for the new Input System only, set **Active Input Handling** to **Both** in Player settings or adapt the controller.
