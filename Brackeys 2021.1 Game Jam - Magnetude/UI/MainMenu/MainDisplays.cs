using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainDisplays : MonoBehaviour
{
    public string displayType;

    void Update()
    {
        switch (displayType) 
        {
            case "Music":
                GetComponent<Text>().text = $"Music: {AudioDirector.Instance.musicVolume * 100}";
                break;
            case "MusicSlider":
                GetComponent<Slider>().value = AudioDirector.Instance.musicVolume * 100;
                break;
            case "Sound":
                GetComponent<Text>().text = $"Sound: {AudioDirector.Instance.sfxVolume * 100}";
                break;
            case "SoundSlider":
                GetComponent<Slider>().value = AudioDirector.Instance.sfxVolume * 100;
                break;
            case "Difficulty":
                GetComponent<Text>().text = $"Difficulty: {GetDifficulty()}";
                break;
            case "ScreenShake":
                GetComponent<Text>().text = $"Screen Shake: {GetShake()}";
                break;
        }
    }

    private string GetDifficulty() 
    {
        switch (GameManager.Instance.difficulty) 
        {
            case 1:
                name = "Easy";
                GetComponent<Text>().color = Color.white;
                break;
            case 2:
                name = "Medium";
                GetComponent<Text>().color = Color.white;
                break;
            case 3:
                name = "Hard";
                GetComponent<Text>().color = Color.white;
                break;
            case 5:
                name = "Expert";
                GetComponent<Text>().color = Color.white;
                break;
            case 15:
                name = "Master";
                GetComponent<Text>().color = Color.red;
                break;
        }
        return(name);
    }

    private string GetShake() 
    {
        string s;
        if (GameManager.Instance.screenShake)
        {
            s = "On";
        }
        else 
        {
            s = "Off";
        }
        return (s);
    }
}
