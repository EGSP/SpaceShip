using System.Collections.Generic;
using System.IO;
using Game.Entities;
using Game.Entities.Factories;
using Game.Entities.Planets;
using UnityEngine;

public class TestRunner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // var starSystem = StarSystemFactory.CreateRandomStarSystem();
        // var image = starSystem.TakeImage();
        //
        // var imageJson = StarSystemImage.GetJson(image);
        // File.WriteAllBytes(Application.dataPath+"//JsonTest.txt",imageJson);
        // var newImage = StarSystemImage.GetImage(imageJson);
        
        EntityMonoFactories.PlanetMonoFactory
            .InstancePrefab(new Planet(new SolidType.DesertType(),0.1f,0), OnPlanetMonoCreated);
    }

    private void OnPlanetMonoCreated(PlanetMono planetMono)
    {
        var id = 0;
        var planetSystem = EntityFactories.PlanetFactory.GenerateRandomPlanetSystem(ref id);

        var json = planetSystem.Serialize();
        File.WriteAllBytes(Application.dataPath+"//JsonTestObject.txt",json);
        var serPlanetSystem = StarSystemObjectExtensions.Deserialize(json);
        
        return;

        var queue = new Queue<StarSystemObject>();
        queue.Enqueue(planetSystem);

        while (queue.Count > 0)
        {
            var next = queue.Dequeue();
            
            var visual = EntityMonoFactories.PlanetMonoFactory
                .InstancePrefabImmediately(next.Entity as Planet);
            visual.transform.position = new Vector3(next.GetPosition(),0);

            foreach (var obj in next.Line)
            {
                queue.Enqueue(obj);
            }
        }
    }
}
