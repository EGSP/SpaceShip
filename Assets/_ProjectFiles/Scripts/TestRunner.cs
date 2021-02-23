using System.Collections;
using System.Collections.Generic;
using System.IO;
using Game.Entities;
using Sirenix.Serialization;
using UnityEngine;

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
    }
}
