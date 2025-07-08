using UnityEngine;

public class PrefabPool : MonoBehaviour 
{
    [SerializeField] private GameObject[] prefabPool;
    
    public GameObject GetRandomPrefab() 
    {
        if (prefabPool.Length == 0) return null;
        
        int randomIndex = Random.Range(0, prefabPool.Length);
        return prefabPool[randomIndex];
    }
}
