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
- Blender — mesh cleanup and retopology for real-time rendering

Tested on a Samsung Galaxy Tab.

## Running it

1. Open the project in Unity 6000.3.11f1
2. Switch the build target to Android
3. Build and deploy to an ARCore-supported device

## Assets and attribution

"Exam 3 Segmentation of Skull, Brain, and Tumor" by emwigdahl, Sketchfab.
Licensed under [CC Attribution 4.0](https://creativecommons.org/licenses/by/4.0/).
Segmented from CT and MR data in the CEREBRIX dataset.
https://sketchfab.com/3d-models/exam-3-segmentation-of-skull-brain-and-tumor-164afd6b605346d983422b19e314d8e6

The meshes have been modified for this project — decimated and retopologised for real-time rendering on mobile hardware, and split for per-layer control.

## Note

This is a prototype, built to explore whether layered anatomical data is easier to read in AR than on a screen. It is not a medical device and is not intended for clinical use.
