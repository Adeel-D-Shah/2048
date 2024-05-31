using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class box : MonoBehaviour
{
    public int Value=2;
    public TextMeshPro txt;
    public GameObject CARD_;
    public GameObject MY_CARD_;

    private void Start()
    {
        GameObject tem =  Instantiate(CARD_, transform.position, Quaternion.identity);
        MY_CARD_ = tem;
        tem.GetComponent<SmoothFollow>().parentTransform = gameObject.transform;
    }

    private void OnDestroy()
    {
        Destroy(MY_CARD_);
    }
    private void FixedUpdate()
    {
        txt.text = Value.ToString();
    }
}
