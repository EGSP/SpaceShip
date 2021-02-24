using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Egsp.Extensions.Linq;
using Game.Entities;
using Game.Entities.Factories;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class TestRunner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var starSystem = StarSystemFactory.CreateRandomStarSystem();
        var image = starSystem.TakeImage();
        
        var imageJson = StarSystemImage.GetJson(image);
        File.WriteAllBytes(Application.dataPath+"//JsonTest.txt",imageJson);
        var newImage = StarSystemImage.GetImage(imageJson);
        
        MonoFactories.PlanetMonoFactory
            .InstancePrefab(new Planet(0.1f,0), OnPlanetMonoCreated);
    }

    private void OnPlanetMonoCreated(PlanetMono planetMono)
    {
        var system = StarSystemFactory.GenerateRandomSystem();

        var queue = new Queue<StarSystemObject>();
        queue.Enqueue(system);

        while (queue.Count > 0)
        {
            var next = queue.Dequeue();
            
            var visual = MonoFactories.PlanetMonoFactory
                .InstancePrefabImmediately(new Planet(next.Size+0.1f,0));
            visual.transform.position = new Vector3(next.GetPosition(),0);

            foreach (var obj in next.Line)
            {
                queue.Enqueue(obj);
            }
        }
    }
}
