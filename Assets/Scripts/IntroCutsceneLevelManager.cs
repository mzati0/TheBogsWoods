using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroCutsceneLevelManager : MonoBehaviour
{
    public DialogueAsset introCutsceneDialogue;
    public AudioClip music;

    private void Start()
    {
        GameEvents.onDialogueEnd += OnDialogueEnd;
        Debug.Log("IntroCutsceneLevelManager");
        DialogueManager.instance.StartDialogue(introCutsceneDialogue.dialogue);
        Debug.Log("Tried to start the scene");
        ApplySavedMusicVolume();
        AudioManager.instance.PlayMusic(music);
    }

    private void ApplySavedMusicVolume()
    {
        if (AudioManager.instance != null && AudioManager.instance.audioMixer != null)
        {
            float musicVolume = PlayerPrefs.HasKey("MusicVolume") ? PlayerPrefs.GetFloat("MusicVolume") : 0f;
            AudioManager.instance.audioMixer.SetFloat("MusicVolume", musicVolume);
        }
    }

    private void OnDialogueEnd(string dialogue)
    {
        AudioManager.instance.StopMusic();
        SceneManager.LoadScene("Level 0");
    }

    private void OnDestroy()
    {
        GameEvents.onDialogueEnd -= OnDialogueEnd;
    }

    private void OnDisable()
    {
        GameEvents.onDialogueEnd -= OnDialogueEnd;
    }
}
