using System.Collections;
using System.Collections.Generic;
using Game.Entities;
using UnityEngine;

public static class StarSystemFactory
{
    public static StarSystem CreateRandomStarSystem()
    {
        var starSystem = new StarSystem(new InGalaxyRelation(0));

        var sun = new Star(new InSystemPosition(Vector3.zero, 0, 1),
            new InSystemRelation(1, 0, 0),1);

        var earth = new Planet(new InSystemPosition(Vector3.right, 10, 2),
            new InSystemRelation(2, 1, 3), 0.5f);
        
        starSystem.AddPlanet(earth);
        starSystem.AddStar(sun);

        return starSystem;
    }
}
