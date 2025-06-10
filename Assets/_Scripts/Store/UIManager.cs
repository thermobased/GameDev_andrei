using UnityEngine;
using UnityEngine.Serialization;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Store store;
    [SerializeField] private PausePopup pausePopup;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            store.gameObject.SetActive(true);
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausePopup != null)
            {
                if (pausePopup.gameObject.activeSelf)
                {
                    pausePopup.gameObject.SetActive(false);
                    Time.timeScale = 1f;
                }
                else
                {
                    pausePopup.gameObject.SetActive(true);
                    Time.timeScale = 0f;
                }
            }
        }
    }
    
    private void Awake()
    {
        if(store == null)
            Debug.LogError("UIManager: Store is null");
            
        if(pausePopup == null)
            Debug.LogError("UIManager: PausePopup is null");
    }
}