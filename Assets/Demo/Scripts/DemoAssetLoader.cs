using UnityEngine;
using UnityScreenNavigator.Runtime.Foundation.AssetLoader;

namespace Demo.Scripts
{
    public sealed class DemoAssetLoader
    {
        private static IAssetLoader _defaultAssetLoader;
        
        public static IAssetLoader AssetLoader
        {
            get
            {
                if (_defaultAssetLoader == null)
                {
                    _defaultAssetLoader = ScriptableObject.CreateInstance<AddressableAssetLoaderObject>();
                }

                return _defaultAssetLoader;
            }
        }
    }
}