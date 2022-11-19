using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public Text textSpeed;
 
    public void setSpeed(float speed)
    {
        textSpeed.text = "Скорость: " + String.Format("{0: 0.0}", speed);
    }
}
