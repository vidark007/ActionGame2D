using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    Image healtbar;

    private void Awake()
    {
        healtbar = GetComponent<Image>();
    }



    public void SetHealthAmount(float healthNormalized)
    {
        healtbar.fillAmount = healthNormalized;
    }
}
