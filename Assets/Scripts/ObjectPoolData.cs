using UnityEngine;

[System.Serializable]
public class ObjectPoolData
{
    //Unique Id
    public string id;
    //Prefab of the object that we want to make copies of
    public GameObject objectToPool;
    //Parent transfom of the object once it is instantiated
    public Transform parent;
    //How many object will be instantiated at the beginning
    public int amountToPool;
}
