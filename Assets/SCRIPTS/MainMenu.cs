﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public Canvas MenuCanvas;

    public Canvas OptionsCanvas;

    public Slider MusicSlider;
    public Text MusicText;

    public Slider SounderSlider;
    public Text Soundtext;

    public Slider fovSlider;
    public Text FOVText;

    public GameObject Player;

    public Transform PlayerSpawn;

    public void Play()
    {
        Instantiate(Player, PlayerSpawn.position, PlayerSpawn.rotation);
        Camera.main.fieldOfView = fovSlider.value;
        gameObject.SetActive(false);
    }

    public void Options()
    {
        MenuCanvas.enabled = false;
        OptionsCanvas.enabled = true;
    }

    public void Credits()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Music()
    {
        MusicText.text = MusicSlider.value.ToString() + "%";
    }

    public void Sound()
    {
        Soundtext.text = SounderSlider.value.ToString() + "%";
    }

    public void FOV()
    {
        //Control.cam.fieldOfView = fovSlider.value;
        FOVText.text = fovSlider.value.ToString();
    }

    public void Controls()
    {

    }

    public void Back()
    {
        MenuCanvas.enabled = true;
        OptionsCanvas.enabled = false;
    }
}
