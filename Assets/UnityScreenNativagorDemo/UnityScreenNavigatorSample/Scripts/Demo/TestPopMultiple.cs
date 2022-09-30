using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityScreenNavigator.Runtime.Core.Screen;

namespace Demo.Scripts.Demo
{
    public class TestPopMultiple : MonoBehaviour
    {
        private void OnGUI()
        {
            if (GUILayout.Button("Pop To Root"))
            {
                 ScreenContainer.Find(ContainerKey.MainContainerLayer).PopToRoot(true).Forget();
            }
            
            if (GUILayout.Button("Pop To prefab_demo_page_top"))
            {
                ScreenContainer.Find(ContainerKey.MainContainerLayer).PopTo("prefab_demo_page_top", true).Forget();
            }
            
            if (GUILayout.Button("Pop All"))
            {
                ScreenContainer.Find(ContainerKey.MainContainerLayer).PopAll().Forget();
            }
        }
    }
}