Samuel Zelenka af Rólén
FG21GP-FT Development tools in game projects

Choose one of the following assignments:

(Chosen assignement)

2. Asteroid Destroyer.
Implement big asteroids splitting up into smaller pieces before being destroyed completely. 
Consider doing this logic outside of the Asteroid class in a class that knows about all the Asteroids. 
The new class can use a RuntimeSet ScriptableObject to keep track of all the asteroids.

Modified Scripts:

AsteroidDestroyer.cs
Asteroid.cs

Using ScriptableVariables to determine how many new asteroids will spawn if shot.
as well as how much smaller they'll become.

Using scriptableEvents to raise _onAsteroidDestroyed event. For score handling as well as spawning new smaller asteroids.