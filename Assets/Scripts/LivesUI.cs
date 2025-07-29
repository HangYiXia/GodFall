using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LivesUI : MonoBehaviour
{
    public TextMeshProUGUI LivesText;
    // Update is called once per frame
    void Update()
    {
        LivesText.text = PlayerStats.Lives.ToString();    
    }
}
