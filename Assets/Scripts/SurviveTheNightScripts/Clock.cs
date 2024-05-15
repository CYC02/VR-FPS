using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

//Author: Cindy Chan
//Keeps track of the time and updates the time text UI on the phone
public class Clock : MonoBehaviour
{
    public float clockSpeed = 60f; // 1 hour in game = 1 min in real life
    public TextMeshProUGUI phoneTimeText;

    private int clockMinutes = 0;
    private int clockHours = 0;
    private float seconds = 0;

    public WinLoseScreen screen;

    void Update()
    {
        seconds += Time.deltaTime * clockSpeed;

        if (seconds >= 60)
        {
            clockMinutes += Mathf.FloorToInt(seconds / 60f);
            seconds = seconds % 60;
        }

        if (clockMinutes >= 60)
        {
            clockHours += Mathf.FloorToInt(clockMinutes / 60f);
            clockMinutes = clockMinutes % 60;
        }

        if (clockHours >= 24)
        {
            clockHours = 0;
            clockMinutes = 0;
            seconds = 0;
        }

        string timeText = string.Format("{0:D2}:{1:D2}", clockHours, clockMinutes);

        phoneTimeText.text = timeText;

        if (screen.gameObject.activeSelf == false && clockHours >= 6) {
            screen.ShowWinScreen();
        }
    }
}

