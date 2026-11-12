# NETVERSE VR 🌐

**Explore. Build. Connect. Learn.**

NETVERSE VR is an immersive 3D network simulation and learning environment designed to make computer networking and network administration more interactive, accessible, and practical.

The project aims to transform traditional networking education into a hands-on experience where learners can explore virtual IT infrastructures, interact with networking equipment, build network topologies, configure devices, and troubleshoot connectivity issues.

Rather than relying exclusively on physical networking laboratories, NETVERSE VR seeks to provide a virtual environment where experiments can be performed safely, repeatedly, and at a lower cost.

## 🎯 Vision

Our vision is to make practical network education more accessible through immersive technologies, computer vision, and interactive simulation.

NETVERSE VR aims to bridge the gap between theoretical networking concepts and real-world technical practice.

## ✨ Core Features

The following capabilities represent the project's development roadmap.

- **Immersive 3D Environment:** Explore a virtual establishment containing computer laboratories, server rooms, offices, and network infrastructure.
- **Interactive Equipment:** Discover and interact with computers, routers, switches, servers, and other networking devices.
- **Network Simulation:** Build virtual network topologies, connect devices, configure IP addresses, and test connectivity.
- **Gesture-Based Interaction:** Use webcam-based hand tracking to navigate and interact with the virtual environment.
- **Practical Learning Scenarios:** Complete guided exercises, solve networking problems, and troubleshoot simulated failures.
- **Gesture Learning:** Explore customizable gesture recognition and personalized interaction.

## 🏗️ Technology Stack

| Technology | Purpose |
|---|---|
| Godot 4 .NET | 3D environment and simulation |
| C# | Game logic, interactions and network simulation |
| Python | Computer vision and gesture processing |
| OpenCV | Webcam processing |
| MediaPipe | Hand tracking and landmark detection |
| UDP | Communication between Python and Godot |
| scikit-learn | Potential future gesture classification |

## 🧠 System Architecture

The project is designed around two main components:

### 1. 3D Simulation Engine

Built with Godot 4 .NET and C#, responsible for:

- Rendering the virtual environment.
- Managing player movement and camera controls.
- Creating and managing virtual equipment.
- Handling object interactions.
- Simulating network configurations and connectivity.

### 2. Gesture Recognition Engine

Built with Python, OpenCV, and MediaPipe, responsible for:

- Capturing webcam input.
- Detecting and tracking hand landmarks.
- Processing hand movements.
- Recognizing navigation and interaction gestures.
- Sending gesture events to the 3D engine through UDP.

### Communication

The two components communicate locally using a lightweight UDP protocol.

This architecture separates computer vision processing from the 3D simulation engine.

## 🗺️ Development Roadmap

NETVERSE VR is being developed incrementally.

- [ ] **Phase 1 — 3D World:** Build the virtual establishment, corridors, computer laboratory, server room, and administration area.
- [ ] **Phase 2 — Navigation:** Implement first-person movement, camera controls, collisions, and object targeting.
- [ ] **Phase 3 — Gesture Control:** Integrate webcam hand tracking and gesture-based navigation.
- [ ] **Phase 4 — Interactive Equipment:** Enable users to select, inspect, move, and manipulate networking devices.
- [ ] **Phase 5 — Network Simulation:** Implement virtual connections, IP configuration, connectivity tests, and troubleshooting.
- [ ] **Phase 6 — Learning Experience:** Introduce guided exercises, challenges, and evaluation mechanisms.
- [ ] **Phase 7 — Gesture Learning Studio:** Explore personalized gesture recording and classification.

## 💻 Development Environment

The project is designed to run on standard desktop computers.

A VR headset is not required for the initial version. The first development milestones use keyboard and mouse controls, followed by webcam-based hand tracking.

### Requirements

- Windows 10/11 or another supported desktop operating system.
- Godot 4 .NET.
- Compatible .NET SDK.
- Python 3.11.
- Webcam for gesture-based interaction.

### Project Structure

The intended structure is:

```text
NETVERSE-VR/
│
├── godot/
│   ├── scenes/
│   ├── scripts/
│   ├── assets/
│   ├── materials/
│   └── project.godot
│
├── gesture-engine/
│   ├── models/
│   ├── scripts/
│   ├── requirements.txt
│   └── check_env.py
│
├── docs/
│
├── README.md
└── .gitignore
```

The structure will evolve as development progresses.

## ⚡ Project Status

**Early Development**

NETVERSE VR is currently in its initial development stage. The development process begins with the construction of the 3D environment and the gesture recognition foundation.

Features listed in the roadmap are planned capabilities, not necessarily implemented features.

## 🎓 Project Purpose

This project explores how immersive 3D environments and computer vision can improve practical learning in computer networking.

It is particularly focused on educational institutions, training centers, and organizations that want to provide practical networking experiences without depending entirely on physical laboratory equipment.

## 🤝 Contributions

Contributions, ideas, suggestions, and technical discussions are welcome.

As the project evolves, contribution guidelines and development documentation will be added.

## 📜 License

License to be determined.

---

**NETVERSE VR — Bringing Network Administration into an Interactive Virtual World.**
