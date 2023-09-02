How To Set Up The Project

This template starter kit has been created by GameDev.tv. When you purchased this asset pack (you did purchase it right?!) you would have been given access to the video tutorials which explain how to set up the project, understand the way it is structured, and how to expand on the project to make your own cool game. You can find those videos when you log in to your account at https://gamedev.tv.

This template needs a couple of quick steps in order to get it working and playable. You'll see an explanation of each of the steps in the Project File Setup video but if you want to dive in quickly, here are the steps:

1. To fix the initial input error:  Open Package Manager, change the selection to Packages: Unity Registry and search for Input System. Click install and go through the process.
2. To fix the sorting layers:  Open the Town scene. Click on any object. In the inspector, select the Layer pulldown and click on Add Layer. In the top right of this window you'll see an icon that looks like 2 horizontal lines with sliders (inbetween the ? and the three dots). Click on that and then double click on the Layers and Tags Preset. Then open up one of the other scenes and return back to the Town and things should work in correctly arranging the layers. 
3. To ensure the layers show properly: Go to Project Settings (Edit -> Project Settings) and select Graphics. Change the Transparency Sort Mode to Custom Axis. Then change the X, Y, Z values to be X = 0, Y = 1, Z = 0.
4. To fix aspect ratio: Click on your game tab and change from Free Aspect to 1920x1080.
5. To load scenes: Add your Scenes so that they load correctly by going to File -> Build Settings and dragging your scenes into the "Scenes In Build". You can remove the Sample Scene if you have one.

If you get stuck, be sure to watch the set up video.
