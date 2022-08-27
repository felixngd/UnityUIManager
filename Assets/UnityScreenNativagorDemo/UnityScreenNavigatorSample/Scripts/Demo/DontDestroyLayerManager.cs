using System;
using Cysharp.Threading.Tasks;
using Demo.Scripts.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityScreenNavigator.Runtime.Core.Shared.Layers;
using UnityScreenNavigator.Runtime.Interactivity;

namespace Demo.Scripts.Demo
{
    public class DontDestroyLayerManager : MonoBehaviour
    {
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void OnGUI()
        {
            //Button
            if (GUI.Button(new Rect(10, 10, 200, 50), "Show Popup"))
            {
                ShowPopup();
            }
            
            //load new scene
            if (GUI.Button(new Rect(10, 50, 200, 50), "Load New Scene"))
            {
                LoadNewScene();
            }
            
            //get top layer
            if (GUI.Button(new Rect(10, 100, 200, 50), "Get Top Layer"))
            {
                GetTopLayer();
            }
            
        }

        void GetTopLayer()
        {
            var topLayer = ContainerLayerManager.GetTopVisibilityLayer();
            Debug.Log($"Top Layer Name: {topLayer.LayerName}, sorting order {topLayer.SortOrder}, layerType {topLayer.LayerType}");
        }
        private async UniTask ShowPopup()
        {
            AlertDialog.DialogKey = "prefab_alert_dialog";
            AlertDialog.DialogLayer = "DontDestroyModals";
            var result = await DefaultDialogService.ShowDialog("Hello World", "This is the first dialog in the demo",
                "OK", "Cancel");

            var button = await result.UserClick.WaitAsync();
            
            if (button == AlertDialog.ButtonPositive)
            {
                Debug.Log("Positive button clicked");
            }
            else if (button == AlertDialog.ButtonNegative)
            {
                Debug.Log("Negative button clicked");
            }
            else
            {
                Debug.Log("Neutral button clicked");
            }
        }

        private void LoadNewScene()
        {
            //load scene Demo3
            SceneManager.LoadScene("Demo3");
        }
    }
}