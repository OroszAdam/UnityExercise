Written by Ádám Orosz

**Medacta Unity Exercise**

# **How to use**
Launch the MedactaExercise.exe. You will be greeted by the Main Menu where you can choose which scene to look at. There are two scenes: Main Task and Extra task.

In the Unity Editor, launch the SceneSelector scene in the folder Assets/Scenes.

**Main task**

This scene contains the solution for the main exercise.
One can move the objects by left clicking on them and dragging the mouse. By right clicking and dragging the mouse you can rotate them. The panel above one of the objects show the distance (in Unity’s measure of unit) and the relative rotation in degrees between the two objects. I think having all three axes of rotations written makes the usage easier. 

For the overlap I made the assumption that the objects never change shape or size. My way of calculating the overlap is based purely on distance and angle, no actual overlap volume is calculated. 
So, there is a „full” overlap, if the distance between the objects are 0.2 (units) and their relative rotation on each axes (in Euler) are less than 5 degrees. This is when they become green.
When there is a partial overlap, they are yellow, as the exercise asks (accompanied by a sound effect).

As for the code, it’s organized in the GameController class. It manages the three main features: Moving, Rotating and Updating the UI.

**Extra task**

In the extra task I used Unity’s built-in Spring Joint component. One object is completely static while the other can be moved around with the mouse just as in the main task. There is a spring (just a line, in fact) between them which changes color based on length, aiming to show the induced mechanical stress.

Managing and rendering this line is done by the SpringManager script.


Source of the 3D model in the main menu:

<https://sketchfab.com/3d-models/medical-equipment-bdfb8e3c46ed446a999f321af14518ac> 


