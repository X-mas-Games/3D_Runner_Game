Unity 3D Game


ScriptableObject Usage
Leverages Unity’s ScriptableObject for clean data management and configuration, especially for coin management settings.

Singletons & Event-driven Design
Implements event-based updates and singleton pattern for centralized service management (SaveManager, SceneLoader, CoinPool).

Object Pooling
Efficient management of coin objects using pooling to optimize performance.

Scene Management
Asynchronous scene loading with progress tracking and delayed transitions.



🎮 Unity Ads Demo Module
This module demonstrates basic Unity Ads SDK integration with:

Conditional compilation flag: UNITY_ADS_SDK

Use of interface ISdkModule for modularity.

Minimal UI for ads demonstration.

Clean separation so the ads module doesn’t impact main game builds.

Note:
To enable this module, add UNITY_ADS_SDK to Scripting Define Symbols in Project Settings → Player.

How to enable Unity Ads Demo Module
💡 Why This Project Stands Out Clean Architecture & SOLID Principles Every class has a single responsibility and depends on abstractions, making the code easy to extend and maintain.

Event-driven UI Updates Real-time UI changes via events, minimizing coupling between systems.

Pooling System Optimizes runtime performance by reusing objects instead of creating/destroying repeatedly.

Cross-platform AR Support Integrates industry-standard AR SDKs with a unified approach.

Modular Ads Integration Ads code isolated and optionally enabled, demonstrating best practices in SDK integration.

🎯 Technologies & Tools Unity 3D (C#)

AR Foundation, ARKit, ARCore, Vuforia

Unity Ads SDK (modular demo)

ScriptableObjects

XRSubsystems

Editor scripting for enhanced inspector functionality
