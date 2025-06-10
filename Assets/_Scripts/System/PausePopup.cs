using UnityEngine;
using UnityEngine.UI;

public class PausePopup : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button exitButton;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        
        if (continueButton != null)
            continueButton.onClick.AddListener(OnContinueClicked);
        
        if (optionsButton != null)
            optionsButton.onClick.AddListener(OnOptionsClicked);
        
        if (exitButton != null)
            exitButton.onClick.AddListener(OnExitClicked);
            
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if (_animator != null)
        {
            _animator.Play("PopupOpen", 0, 0f);
        }
    }

    private void OnContinueClicked()
    {
        _animator.SetTrigger("close");
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    private void OnOptionsClicked()
    {
        Debug.Log("Options clicked - to be implemented");
    }

    private void OnExitClicked()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void ClosePopup()
    {
        _animator.SetTrigger("close");
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
} 