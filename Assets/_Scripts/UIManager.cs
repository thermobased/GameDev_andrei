using UnityEngine;
using UnityEngine.Serialization;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Store store;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            store.gameObject.SetActive(true);
        }
    }
    
    private void Awake()
    {
        if(store == null)
            Debug.LogError("UIManager: Shop is null");
    }
}