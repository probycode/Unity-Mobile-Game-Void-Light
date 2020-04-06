using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TestStuff))]
public class TestEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TestStuff testStuff = (TestStuff)target;
        if (GUILayout.Button("AddWisps"))
        {
            ScoreManager.WispesCollected += testStuff.addWispsAmount;
        }
        if (GUILayout.Button("UnlockItem"))
        {
            ItemManager.UnlockItem(ItemManager.GetItem(testStuff.itemToUnlock));
        }
        if (GUILayout.Button("lockItem"))
        {
            ItemManager.LockItem(ItemManager.GetItem(testStuff.itemToUnlock));
        }
        if (GUILayout.Button("LockAllItem"))
        {
            ItemManager.LockAllItems();
        }
        if (GUILayout.Button("UnLockAllItem"))
        {
            ItemManager.UnLockAllItems();
        }
        if (GUILayout.Button("UnlockRandomItems"))
        {
            ItemManager.UnlockRandomItems(3);
        }
        if (GUILayout.Button("DebugListOfItemsLeftToUnlock"))
        {
            ItemManager.DebugListOfItemsLeftToUnlock();
        }

        if (GUILayout.Button("SaveGame"))
        {
            GameManager.SaveGame();
        }
    }
}
