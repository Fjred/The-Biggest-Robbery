using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public StealItem player;

    public AudioClip moneySound;

    public AudioSource audioSource;

    public GameObject moneyRain;

    public TMP_Text moneyCount;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            SceneManager.LoadScene(0);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (player.money > 0)
        {
            player.EndGame();

            ReplaceAudioClip();
            StartMoneyRain();
        }
    }

    private void ReplaceAudioClip()
    {
        audioSource.clip = moneySound;

        audioSource.Play();
    }

    private void StartMoneyRain()
    {
        moneyRain.SetActive(true);
        moneyCount.text = $"{player.money}";
    }
}
