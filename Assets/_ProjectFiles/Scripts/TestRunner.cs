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
            .InstancePrefab(starSystem.AllEntities.FindType<Planet>(), OnPlanetMonoCreated);
    }

    private void OnPlanetMonoCreated(PlanetMono planetMono)
    {
        Debug.Log("Planet created.");
    }
}
