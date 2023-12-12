# Avatar Scaling Utilities

A collection of utilities for building VRChat world which interact with VRChat's avatar scaling system.

## Installation Instructions

1. To use this package, you will need the [VRChat Creator Companion](https://vcc.docs.vrchat.com).
2. Go to https://scarlet-crystal.github.io/VCC-Package-Listing/ and click *Add to VCC.* [^1]
3. Go to the projects tab, click *Manage Project* for the project you wish to install this package to, then add the *Avatar Scaling Utilities* package to your project.

[^1]: On November 1st, 2023 the VCC listing for this package was moved from this repository to the one listed above. If you added the old listing, remove it from the VCC, then repeat step 2.

## What this package contains

### Object Scale Driver

Scales the local transform of the attached gameobject according to the player's eye height. Useful for making user interfaces that scales with the player.

### Relative Speed and Relative Voice

Adjusts various voice and locomotion properties according to player height.

### Scaling Trigger

Adjusts the player's height when they enter or leave a trigger.

### Scaling Collider

Adjusts the player's height when they collide with a physics object.

### Scaling Station

Adjusts the player's height when they enter or leave a station.

### Scaling Pickup

Adjusts the player's height when they pickup, drop, or use an object with the VRC Pickup script.

### Scaling Particles

Adjusts the player's height when they collide with a particle.

### Scaling Object

Adjust the player's height when the attached GameObject's enabled state changes.

## Sample Assets

This package contains an example scene that you can import into your project from the package manager window.
