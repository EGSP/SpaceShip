using System.Collections.Generic;
using System.IO;
using Game.Entities;
using Game.Entities.Factories;
using Game.Entities.Planets;
using Sirenix.Serialization;
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
            .InstancePrefab(new Planet(new PlanetType.SolidType.DesertType(),0.1f,0),
                OnPlanetMonoCreated);
    }

    private void OnPlanetMonoCreated(PlanetMono planetMono)
    {
        var id = 0;
        var starSystem = StarSystemFactory.CreateRandomStarSystem(ref id);

        var monoStarSystem = StarSystemMonoFactory.CreateFromRaw(starSystem);
    }
}
