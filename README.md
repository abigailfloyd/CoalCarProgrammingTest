# CoalCarProgrammingTest
This is a tool to design simple levels in VR using primitive volumes - cubes, spheres, and cylinders. You are able to move around the plane, either with continuous movement or teleportation, spawning any of these objects wherever you like. You can use the Movement, Rotation and Scalar tools to adjust the objects, or the Delete button to remove an object from the scene. You are also able to name and save levels to a file, and load previously saved levels.

**CONTROLS:**
**Movement**
- Continuous movement/turning: joystick 
- Teleportation: right trigger and release (ray extending from controller must land on plane)
**UI Selection**
- Point ray at menu, press trigger to select a button 
    -This works with either hand
- Space bar toggles UI menu
- _Spawning_ items make them appear in front of you 
- _Move_ allows you to pick up objects and move them
- _Rotate_ allows you to rotate objects along any of the three axes
- _Scale_ allows you to scale objects, either uniformly using the topmost slider, or along any of the three axes
- _Delete_ allows you to select objects to be deleted
- _Save_ allows you to save a level   
    -To name level, click on text input and use computer keyboard to name, then click on _Save Level_
- _Load_ allows you load a previously saved level
       - You can choose any level from the dropdown menu of saved levels, then choose _Load Level_ to load it into the scene
           - This will replace the current contents of the level, and will not save the current level (unless already saved)

**Object Manipulation**
- After selecting _Move_ from the menu, you can move objects by pointing at them and grabbing using the side/squeeze buttons on the  controller, then moving the controller while continuing to hold down that button/buttons
- After selecting _Rotate_ from the menu, you can select any object by pointing at it and using the side/squeeze buttons on the controller to select it. Then you can use the UI sliders to adjust rotation
- After selecting _Scale_ from the menu, you can select any object by pointing at it and using the side/squeeze buttons on the controller to select it. Then you can use the UI sliders to adjust scale

**BUGS:**
- There are currently some bugs with scale and rotation for cylinders and spheres. Sometimes you won’t be able to select these, and the UI sliders will not appear. Movement should work fine, though
- Rotation/scale sliders do not appear until you select an object using the side/squeeze buttons. If you switch from rotation to scale, the rotation sliders will stay on screen until you reselect an object (and vice versa)

