# AR Anatomy Viewer

An Android AR application that places a segmented CT model — skull, brain and tumour as separate meshes — in the room at real scale, and lets you look inside it.

**Video:** https://youtu.be/c7RZ4Ymnb2A

A flat screen shows one slice at a time. Anatomy is spatial, and the relationship between a tumour and the structures around it is the thing a surgeon or a student actually needs to read. Holding the model at real scale and cutting through it answers that directly.

## What it does

- **Layer visibility** — skull, brain and tumour can each be shown or hidden independently
- **Per-layer transparency** — fade the skull to see the structures behind it in place rather than in isolation
- **Clipping plane** — cut the model at any depth along any axis, with independent minimum and maximum bounds per axis. The cut is computed in the shader in world space, so the cross-section stays correct as the model is moved
- **Reset view** — return the model to its initial placement
- The model is placed in front of the camera on launch and anchored in world space, so it stays put as you walk around it

## Built with

- Unity 6000.3.11f1
- AR Foundation / ARCore
- XR Interaction Toolkit
- Custom HLSL shaders for the clipping plane
- 3D Slicer — segmentation of the imaging volume
- Blender — mesh cleanup and retopology for real-time rendering

Tested on a Samsung Galaxy Tab.

## Running it

1. Open the project in Unity 6000.3.11f1
2. Switch the build target to Android
3. Build and deploy to an ARCore-supported device

## Assets and attribution

The anatomical geometry is derived from a third-party model. <!-- TODO: replace with the model name, author, licence and link from its Sketchfab page -->

Segmentation, mesh preparation, shaders, UI and all application code are my own.

## Note

This is a prototype, built to explore whether layered anatomical data is easier to read in AR than on a screen. It is not a medical device and is not intended for clinical use.
