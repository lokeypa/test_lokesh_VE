
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager instance;
    public GameObject StartScene, GameplayScene, EndScene,helpscreen;
    public Button playBtn, playBtn2, reloadBtn, homeBtn, volumeBtn , rateBtn,helpBtn;

    public Sprite volOnImg, volOffImg;
    private bool isVolOn = true;

    public Sprite PlayImg, PauseImg;
    public static bool isplaying = true;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        //Setting all the listners for btns.
        playBtn.onClick.AddListener(() => PlayBtn());
        homeBtn.onClick.AddListener(() => ReloadBtn());
        reloadBtn.onClick.AddListener(() => ReloadBtn());
        volumeBtn.onClick.AddListener(() => onToggleSoundBtn());
        rateBtn.onClick.AddListener(() => RateBtn());
        helpBtn.onClick.AddListener(() => HelpBtn());
        playBtn2.onClick.AddListener(() => onTogglePlayBtn());
    }


    public void ShowStartScene()
    {
        StartScene.SetActive(true);
        GameplayScene.SetActive(false);
        EndScene.SetActive(false);
    }

    public void ShowGameplayScene()
    {
        GameplayScene.SetActive(true);
        StartScene.SetActive(false);
        EndScene.SetActive(false);
    }

    public void ShowFinalScene()
    {
        EndScene.SetActive(true);
        StartScene.SetActive(false);
        GameplayScene.SetActive(false);
    }

    public void onToggleSoundBtn()
    {
        isVolOn = !isVolOn;
        if (isVolOn)
        {
            volumeBtn.GetComponent<Image>().sprite = volOnImg;
            GetComponent<AudioSource>().Play();
        }
        else
        {
            volumeBtn.GetComponent<Image>().sprite = volOffImg;
            GetComponent<AudioSource>().Stop();
        }
    }

    public void onTogglePlayBtn()
    {
        isplaying = !isplaying;
        if (isplaying)
        {
            playBtn2.GetComponent<Image>().sprite = PauseImg;
        }
        else
        {
            playBtn2.GetComponent<Image>().sprite = PlayImg ;
        }
    }

    public void PlayBtn()
    {
        ShowGameplayScene();
        GetComponent<GamePlayManager>().OnTouchPassed();
    }

    public void RateBtn()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.Zykoon.PinTheColor&hl=en_IN");
    }

    public void HelpBtn()
    {
        helpscreen.SetActive(true);
    }

    public void ReloadBtn()
    {
        SceneManager.LoadScene(0);
    }



}
