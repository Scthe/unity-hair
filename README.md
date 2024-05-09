# Unity hair - article supplement

This repository is a supplement to my article: ["Using Unity's strand-based hair package"](https://www.sctheblog.com/blog/unity-hair/). It includes a [Blender file](unity-hair.blend) with sample hairstyles that were exported to Unity's strand-based hair solution: [com.unity.demoteam.hair](https://github.com/Unity-Technologies/com.unity.demoteam.hair). More importantly, it's a complete Unity project that showcases all the techniques described in the article.

I've had tons of problems integrating this package into my ["AI-Iris-Avatar"](https://github.com/Scthe/ai-iris-avatar). So I've decided to do a more thorough investigation into its content.



https://github.com/Scthe/unity-hair/assets/9325337/26348d5f-6139-4cc6-97f9-41056cbbe916

**Using strand-based hair with animation. The hair system is parented to the mecanim's Head bone. You might also notice the SDF collider to prevent mesh penetration. This is the original Sintel hairstyle. You may arguee that the hair is too bouncy. I disagree!**



https://github.com/Scthe/unity-hair/assets/9325337/4eb1208b-30e6-4238-bfd2-a781860e8c07

**Using sphere collider to displace hair strands. In practice, it's even more fun than it looks.**



https://github.com/Scthe/unity-hair/assets/9325337/58a5997e-42aa-4522-b947-73f98335a6bb

**Using wind force to displace hair. Strength, direction, and randomness are adjustable. It even works with colliders. The hairstyle is "cyberpunk" from original Blender 3.5 sample files. Default Unity hair material.**


## Inside the Unity project

![screen-unity](https://github.com/Scthe/unity-hair/assets/9325337/7a69cb6e-cef1-42fa-b31e-b171400f4854)


> WARNING: Asset files for hair can be huge (120+ MB). For blender sample hairstyles, I've only included Alembic files. You have to [create the hair assets](https://www.sctheblog.com/blog/unity-hair/#creating-hair-asset) yourself. Afterward, link the asset files to the appropriate "Hair Instance" components.

Everything is inside "Scene_SintelHairTest" scene.

### Scene

* `--SCENE--`. Camera, lights, background, postFX volume.
* `--sintel2--`. The main character.
    * `Hair17` - the original hair cards (deactivated).
    * `Hair_collider` - simplified head mesh. Used to create SDF.
    * `UnityRig` - skeleton.
* `--SINTEL COLLIDERS--` - colliders for hair physics.
    * `SintelSDF_Volume` - SDF volume for the face.
    * `SintelHair - Sphere Collider` - sphere collider that interacts with the hair.
* `--SINTEL HAIR--` - original Sintel hair exported from Blender. Already parented to Sintel (in play mode, through parent constraint).
* `--BLENDER HAIR SAMPLES--` - game objects with hairstyles exported from the [Blender 3.5 sample file](https://www.blender.org/download/releases/3-5/). Already parented to Sintel (in play mode, through parent constraint).
* `--HAIR TESTS--` - simple hair systems for various tests. Usually, it's just a flat plane with hair growing out of it.
* `Hair Wind dir` - wind force game object.


### Inside **Assets** directory

* `Animations`. Character animations from [Mixamo](https://www.mixamo.com/). Showcases hair in motion during play mode.
* `Hair`
    * `blender-3.5-samples`. Both "curly" and "cyberpunk" hairstyles were directly exported from the [Blender 3.5 sample file](https://www.blender.org/download/releases/3-5/). This showcases that the procedure should work with any hair.
    * `Materials`. Sample materials to show how to add color to the hair. `Hair_GradientY` contains a shader for hair that has a gradient from root to tip.
    * `PlaneHair`. Simple hair "grown" from a plane.
    * `PlaneHair2`. Simple hair "grown" from a plane.
    * `SintelHair`. Original Sintel hair.
    * `SDF_CollisionTexture`s. The 3D texture used for signed distance field tests.
* `HDRI`. Just the usual HDRI image for additional lighting.
* `Scripts`. Nothing related to hair. Scripts for animation switching, blinking, eye look at constraints, etc.
* `Sintel`. Character from ["AI-Iris-Avatar"](https://github.com/Scthe/ai-iris-avatar). It also contains hair cards that were originally used. It's disabled in the scene, as we have much better-looking strands now.



## Inside Blender file


![screen-blender](https://github.com/Scthe/unity-hair/assets/9325337/95584a2a-8ada-4fef-9505-23c15a9e3955)


* `Sintel-Unity-Export` - collection for the 3D model. If you select and export everything inside `UnityRig`, you will get the original ".fbx" file.
* `Sintel-Colliders` - simplified meshes used for collision detection. The collider used in Unity is already inside `UnityRig`.
* `Sintel-Hair`
    * `SintelHairOriginal` - original Sintel's hair from "Sintel Lite 2.57b" asset. Ready for export into Alembic file.
    * `SimpleCustomHair` - custom quick and dirty hairstyle for tests. Ready for export into Alembic file.
* `Hair-Tests` - test hair "grown" on the plane.
* `Blender-3.5-samples`
    * `CurlyHairSample` one of the hairstyles from the Blender 3.5 sample file. Ready for export into Alembic file (see particle systems). Created from the new Blender hair system (based on curves). Read [my article](https://www.sctheblog.com/blog/unity-hair/) and you will know why there is a special "--squashed-mods" version.
* `Scene` - camera, lights, and background.


## FAQ

### Why is the licence GPLv3?

> This app includes source code/assets created by other people. Each such instance has a dedicated README.md in its subfolder that explains the licensing. The paragraphs below only affect things created by me.

It's the same license as my previous project - ["AI-Iris-Avatar"](https://github.com/Scthe/ai-iris-avatar). Check its readme for reasons.


### Q: Is the 3D model included?

Yes, check .fbx inside [unity-project/Assets/Sintel](unity-project/Assets/Sintel).



## Honourable mentions

* [Blender](https://www.blender.org/), [Blender Institute](https://www.blender.org/institute/).
* [GIMP](https://www.gimp.org/) and [Inkscape](https://inkscape.org/). I'm so used to them I almost forgot to mention.
* [Sintel Lite 2.57b](http://www.blendswap.com/blends/view/7093) by BenDansie. Used as base mesh.
* [21 Realtime man Hairstyles collection](https://sketchfab.com/3d-models/21-realtime-man-hairstyles-collection-6a876007572c464da5184eb99af0c5f7) by Vincent Page.
* [mixamo](https://www.mixamo.com) for animations.
* Tons of animation guides. I especially liked videos by Sir Wade Neistadt on YouTube:
    * [Animating Eyes: Character Blinks](https://www.youtube.com/watch?v=c0DimVO18ps),
    * [The Secret Workflow for Animating Dialogue](https://youtu.be/5cIxEZwZmS4?t=819).
