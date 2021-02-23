using System;
using Egsp.Utils.MeshUtilities;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace Game.Entities.Factories
{
    public static class PlanetFactory
    {
        private static GameObject _planetPrefab;
        private static AsyncOperationHandle<GameObject> _handle;
        static PlanetFactory()
        { 
            LoadPrefab(null);
        }

        private static void LoadPrefab(Action callback)
        {
            _handle = Addressables.LoadAssetAsync<GameObject>("Assets/Prefabs/Planet.prefab");
            _handle.Completed += (handle) => _planetPrefab = handle.Result;
        }
        
        public static void CreateMono(Planet planet, Action<PlanetMono> callback)
        {
            if (_planetPrefab == null)
            {
                if (!_handle.IsValid())
                {
                    LoadPrefab(() => CreateMonoInternal(planet, callback));
                }
                else
                {
                    _handle.Completed += (handle) => CreateMonoInternal(planet, callback);
                }
                return;
            }

            CreateMonoInternal(planet,callback);
        }

        private static void CreateMonoInternal(Planet planet, Action<PlanetMono> callback)
        {
            var mesh = CreateMesh(planet);
            var inst = Object.Instantiate(_planetPrefab).GetComponent<PlanetMono>();
            
            inst.Accept(planet);
            inst.MeshFilter.mesh = mesh;

            callback?.Invoke(inst);
        }
        
        public static Mesh CreateMesh(Planet planet)
        {
            var mesh = MeshUtils.Diamond(planet.Size, planet.Size);
            return mesh;
        }
    }
}