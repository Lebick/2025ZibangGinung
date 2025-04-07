using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageJoin : MonoBehaviour
{
    public int stage;
    public string stageName;

    public Animator stageAnim;
    public Text stageText;

    private void OnTriggerEnter(Collider other)
    {
        GamePlayManager.instance.stageIndex = stage;
        stageAnim.SetTrigger("Show");
        stageText.text = stageName;
    }
}
