using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{

    public float lifeMax;
    public float life;
    public Image lifebar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
           lifebar.fillAmount = life / lifeMax;
    }
}
