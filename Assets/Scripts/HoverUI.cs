using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class HoverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public string gameplayScene = "Gameplay";
    public RectTransform rectTransform;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        rectTransform.localScale = new Vector3(1.2f,1.2f,1.0f);
        
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        rectTransform.localScale = new Vector3(1.0f,1.0f,1.0f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadSceneAsync(gameplayScene);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}