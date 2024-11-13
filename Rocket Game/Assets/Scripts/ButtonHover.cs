using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public UnityEngine.UI.Button button;
    public AudioSource buttonSound;
    [SerializeField] public Animator animParameter;
    [SerializeField] public Animator recordBtnAnim;
    [SerializeField] public GameManager gm;
    public PlayerPrefs cameraAnim;
    public int gameLoaded = 0;
    void Start()
    {
        button = GetComponent<UnityEngine.UI.Button>();
        button.onClick.AddListener(OnButtonClick);
        buttonSound = GetComponent<AudioSource>();
        gm.RecordsVisibility(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ColorBlock cb = button.colors;
        cb.normalColor = new Color(1f, 0.843f, 0.843f);
        button.colors = cb;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        ColorBlock cb = button.colors;
        cb.normalColor = new Color(1f, 0.647f, 0.647f);
        button.colors = cb;
    }
      private void OnButtonClick()
    {



        if(button.name.Equals("Play")) {
            buttonSound.Play();
            Invoke("LoadLevel", .5f);
            PlayerPrefs.SetInt("gameLoaded", 1);
            animParameter.SetBool("ButtonClicked", true);
        }
        if(button.name.Equals("Records")) {
            recordBtnAnim.SetBool("RecordClicked", true);
            Debug.Log("Records clicked");
            Invoke("GameModeVisibility",.5f);
            gm.RecordAnimation.SetBool("RecordClicked", true);
        }
        else if (button.name.Equals("Exit")) {
             Application.Quit();
        }
    }
    void GameModeVisibility() {
            gm.GameModeVisibility(false);
            gm.RecordsVisibility(true);
    }
    void LoadLevel() {
            SceneManager.LoadScene("Level 1");
            gameLoaded = 1;
    }
}
