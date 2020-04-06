using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ItemManagerData
{
    public Dictionary<int, Item> items;
}

public static class ItemManager {

    public delegate void ItemUnlockedHandler(Item item);
    public static event ItemUnlockedHandler ItemUnlocked;
    public static bool shouldDebug;

    private static Dictionary<int, Item> items = new Dictionary<int, Item>();

    private static int _itemID;

    public static int ItemID
    {
        get
        {
            return _itemID;
        }
    }

    public static void OnItemUnlocked(Item item)
    {
        if (ItemUnlocked != null)
        {
            ItemUnlocked(item);
        }
    }

    public static List<Item> GetItemsLeftToUnlock()
    {
        List<Item> itemsLeftToUnlock = new List<Item>();

        for (int i = 0; i < items.Count; i++)
        {
            if (ContainsItem(i))
            {
                if (items[i].isLocked == true)
                {
                    itemsLeftToUnlock.Add(items[i]);
                }
            }
        }
        return itemsLeftToUnlock;
    }

    public static void DebugListOfItemsLeftToUnlock()
    {
        List<Item> itemsLeftToUnlock = GetItemsLeftToUnlock();

        foreach (var item in itemsLeftToUnlock)
        {
            if (shouldDebug)
            {
                Debug.Log(item.id);
            }
        }
    }

    public static List<Item> UnlockRandomItems(int amountOfItems)
    {
        List<Item> randomItems = new List<Item>();

        if (IsAllItemsUnlocked())
        {
            if (shouldDebug)
            {
                Debug.Log("ItemManager: All Items unlocked");
            }
            return randomItems;
        }

        for (int i = 0; i < amountOfItems; i++)
        {
            List<Item> itemsLeftToUnlock = GetItemsLeftToUnlock();

            if (IsAllItemsUnlocked())
            {
                foreach (var item in randomItems)
                {
                    UnlockItem(item);
                }
                if (shouldDebug)
                {
                    Debug.Log("ItemManager: UnlockRandomItems: All Items unlocked");
                }
                return randomItems;
            }

            int selector = UnityEngine.Random.Range(0, itemsLeftToUnlock.Count);

            randomItems.Add(itemsLeftToUnlock[selector]);

            UnlockItem(randomItems[i]);
        }
        return randomItems;
    }

    public static void AddItem(Item item)
    {
        if (items.ContainsKey(_itemID))
        {
            _itemID++;
            return;
        }
        item.id = _itemID;
        items.Add(_itemID, item);
        if (shouldDebug)
        {
            Debug.Log("ItemManager: Added Item: " + item.id);
        }
        _itemID++;
    }

    public static void RemoveItem(Item item)
    {
        if (items.ContainsKey(item.id))
        {
            items.Remove(item.id);
            if (shouldDebug)
            {
                Debug.Log("ItemManager: removed Item: " + item + " : " + item.id);
            }
        }
        else
        {
            if (shouldDebug)
            {
                Debug.Log("ItemManager:Cannot Remove Item: " + item + " : " + item.id + "Does Not Exist");
            }
        }
    }

    public static Item GetItem(int ItemId)
    {
        if (items.ContainsKey(ItemId))
        {
            return items[ItemId];
        }
        else
        {
            if (shouldDebug)
            {
                Debug.Log("ItemManager: Item with id:" + ItemId + " Does Not Exist");
            }
            if (items.Count == 0)
            {
                if (shouldDebug)
                {
                    Debug.Log("ItemManager:Cannot Get Item with id: " + ItemId + "Errer: Does Not Exist");
                }
            }
            return null;
        }
    }

    public static void UnlockItem(Item item)
    {
        if (items.ContainsKey(item.id))
        {
            item.isLocked = false;
            OnItemUnlocked(item);
            if (shouldDebug)
            {
                Debug.Log("ItemManager: Unlocked Item: " + item + " : " + item.id);
            }
        }
        else
        {
            if (shouldDebug)
            {
                Debug.Log("ItemManager: Cannot Unlock Item : " + item + " : " + item.id + "Errer: Does Not Exist");
            }
        }
    }

    public static void UnlockItem(int itemID)
    {
        Item item = GetItem(itemID);
        item.isLocked = false;
        OnItemUnlocked(item);
        if (shouldDebug)
        {
            Debug.Log("ItemManager: Unlocked Item: " + item + " : " + item.id);
        }
    }

    public static void LockItem(Item item)
    {
        if (items.ContainsKey(item.id))
        {
            item.isLocked = true;
            OnItemUnlocked(item);
            if (shouldDebug)
            {
                Debug.Log("ItemManager: locked Item: " + item + " : " + item.id);
            }
        }
        else
        {
            if (shouldDebug)
            {
                Debug.Log("ItemManager: Cannot Lock Item : " + item + " : " + item.id + "Errer: Does Not Exist");
            }
        }
    }

    public static void LockAllItems()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (ContainsItem(i))
            {
                LockItem(items[i]);
            }
        }
        if (shouldDebug)
        {
            Debug.Log("ItemManagger: All Items Locked");
        }
    }

    public static void UnLockAllItems()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (ContainsItem(i))
            {
                UnlockItem(items[i]);
            }
        }
        if (shouldDebug)
        {
            Debug.Log("ItemManager: All Items Unlocked");
        }
    }

    public static void LoadItemManagerData(ItemManagerData itemManagerData)
    {
        items = itemManagerData.items;
        if (items != null || items.Count >= 0)
        {
            if (shouldDebug)
            {
                Debug.Log("ItemManager: Loaded: " + items.Count + " Items");
            }
        }
    }

    public static ItemManagerData GetItemManagerData()
    {
        ItemManagerData itemManagerData = new ItemManagerData();

        itemManagerData.items = items;

        return itemManagerData;
    }

    public static bool ContainsItem(Item item)
    {
        if (items.ContainsKey(item.id))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool ContainsItem(int key)
    {
        if (items.ContainsKey(key))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool IsAllItemsUnlocked()
    {
        if (GetItemsLeftToUnlock().Count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
