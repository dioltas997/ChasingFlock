# **ChasingFlock**
Unimi AI for videogames project of February 2025

## **Goal**
The goal of this project is to simulate a flock of agents moving inside a scene characterized by moving obstacles.  
Moreover, a target is placed randomly in the scene: the path to reach the target must be computed, and the flock must move towards the target.

## **Setup**

### **Platform**
For simplicity, the project can consider a 2D closed environment (a 100 by 100 meters square area). The sides of the square must be considered as walls for the flock.

### **Obstacles**
Several obstacles (graphically, grey circles of radius = 1 meter) are placed randomly in the scene. They must move back and forth horizontally inside the scene (without colliding with each other). The speed of every obstacle must be between 5 and 20 meters/s.

### **Target**
A random target (graphically, a small red circle of radius = 0.1 m) is placed randomly in the scene. At the beginning of the simulation, a corner of the scene is picked up randomly, and the target is placed randomly at a maximum distance of 10 meters from the corner.

### **Agents**
A flock of 50 agents moves inside the scene, each agent with a fixed speed of 10 meters/s. The flock follows the original approach of Craig Reynolds (as seen during the course). However, the flocking movement is affected by the presence of the target: the path to reach the target must be computed, and the flock will move towards the target, taking into consideration the moving obstacles. When one of the agents reaches the target, a new target is placed randomly in the scene (following the same approach used for the first target but excluding the current corner in the computation of the position of the new target), and the flock must move towards the new target.


## Unity Versions
The code provided in the course repository will be developed and tested with the last LTS version available at the beginning of the course (this year it is 2022.3.48f1 LTS).