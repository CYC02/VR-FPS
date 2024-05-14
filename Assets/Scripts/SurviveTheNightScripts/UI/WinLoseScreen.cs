using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinLoseScreen : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public void ShowWinScreen() {
        gameObject.SetActive(true);
        textMesh.SetText("You Win!");
    }

    public void ShowLoseScreen() {
        gameObject.SetActive(true);
        textMesh.SetText("You Lose!");
    }
}
