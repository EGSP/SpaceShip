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
        // var imageJson = JsonConvert.SerializeObject(image);
        // var newImage = JsonConvert.DeserializeObject<StarSystemImage>(imageJson);
        var imageJson = SerializationUtility.SerializeValue(image, DataFormat.JSON);
        File.WriteAllBytes(Application.dataPath+"//JsonTest.txt",imageJson);
        var newImage = SerializationUtility.DeserializeValue<StarSystemImage>(imageJson, DataFormat.JSON).ResolveRelations();

        // var handle = Addressables.LoadAssetAsync<GameObject>("Assets/Prefabs/Planet.prefab");
        // handle.Completed += (handlex) => Instantiate(handle.Result);
        
        PlanetFactory.CreateMono(starSystem.AllEntities.FindType<Planet>(), null);
    }
}
