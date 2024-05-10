using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class box : MonoBehaviour
{
    public int Value=2;
    public TextMeshPro txt;

    private void FixedUpdate()
    {
        txt.text = Value.ToString();
    }
}
