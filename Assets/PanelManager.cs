using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class PanelManager : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject settingsPanel;
    public GameObject level1Panel;
    public GameObject level2Panel;
    public GameObject level3Panel;
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject readyImg;
    public GameObject check;
    public GameObject cross;
    public VideoPlayer videoPlayer;
    public int counter = 0;

    private void DeactivateAllPanels()
    {
        menuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        level1Panel.SetActive(false);
        level2Panel.SetActive(false);
        level3Panel.SetActive(false);
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        cross.SetActive(false);
        check.SetActive(false);
        readyImg.SetActive(false);
    }

    public void ShowMenuPanel()
    {
        DeactivateAllPanels();
        menuPanel.SetActive(true);
    }

    public void ShowSettingsPanel()
    {
        DeactivateAllPanels();
        settingsPanel.SetActive(true);
    }

    public void ShowLevel1Panel()
    {
        DeactivateAllPanels();
        level1Panel.SetActive(true);
    }

    public void ShowLevel2Panel()
    {
        DeactivateAllPanels();
        StartCoroutine(ActivatePanelAfterDelay(level2Panel, 0.1f));
    }

    public void ShowLevel3Panel()
    {
        DeactivateAllPanels();
        StartCoroutine(ActivatePanelAfterDelay(level3Panel, 0.1f));
    }

    public void EndGame()
    {
        if (counter == 3)
        {
            ShowWinPanel();
        }
        else
        {
            ShowLosePanel();
        }
    }

    public void ShowWinPanel()
    {
        DeactivateAllPanels();
        winPanel.SetActive(true);
    }

    public void ShowLosePanel()
    {
        DeactivateAllPanels();
        losePanel.SetActive(true);
    }

    public void ShowCheck()
    {
        counter++;
        check.SetActive(true);
        StartCoroutine(TransitionToNextStateAfterDelay());
    }

    public void ShowCross()
    {
        cross.SetActive(true);
        StartCoroutine(TransitionToNextStateAfterDelay());
    }

    public void ExitGame()
    {
        // Exits the game (only works in a built application)
        Application.Quit();
#if UNITY_EDITOR
        // If running in the Unity Editor
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void PlayVideoAndShowLevel1()
    {
        DeactivateAllPanels();
        videoPlayer.gameObject.SetActive(true);  // Enable the VideoPlayer GameObject
        readyImg.SetActive(true);
        videoPlayer.Play();
        videoPlayer.loopPointReached += ShowLevel1AfterVideo;
    }

    private void ShowLevel1AfterVideo(VideoPlayer vp)
    {
        videoPlayer.gameObject.SetActive(false);  // Disable the VideoPlayer GameObject after the video ends
        ShowLevel1Panel();
    }

    private IEnumerator ActivatePanelAfterDelay(GameObject panel, float delay)
    {
        yield return new WaitForSeconds(delay);
        panel.SetActive(true);
    }

    private IEnumerator TransitionToNextStateAfterDelay()
    {
        yield return new WaitForSeconds(1f); // Wait for 1 second

        if (level1Panel.activeSelf)
        {
            ShowLevel2Panel();
        }
        else if (level2Panel.activeSelf)
        {
            ShowLevel3Panel();
        }
        else if (level3Panel.activeSelf)
        {
            yield return new WaitForSeconds(0.5f); // Wait for another second to show the check or cross
            EndGame();
        }
    }
}
