using System;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Demo.Scripts;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityScreenNavigator.Runtime.Core.Screen;
using UnityScreenNavigator.Runtime.Core.Shared;

public class TestUniTask : MonoBehaviour
{
    //[SerializeField] private ScreenContainer screenContainer;
    AsyncReactiveProperty<int> point = new AsyncReactiveProperty<int>(0);
    [SerializeField] private Button button;

    private async void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            point.Value = i;
        }

        //point.TakeLast(2).Subscribe(next => { Debug.Log("next = " + next); }, () => { Debug.Log("completed"); });
        var list = await  point.TakeLast(2).ToListAsync();
        foreach (var i in list)
        {
            Debug.Log("next = " + i);
        }
    }
}

//custom editor TestUniTask
#if UNITY_EDITOR
[CustomEditor(typeof(TestUniTask))]
public class TestUniTaskEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var testUniTask = target as TestUniTask;
    }
}
#endif