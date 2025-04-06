using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBarrel : InteractableObject
{
    private bool isCanMove = true;

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    public override void Interaction()
    {
        base.Interaction();

        if (isCanMove)
        {
            isCanMove = false;
            StartCoroutine(Moving());
        }
    }

    public void ResetPos()
    {
        transform.position = startPos;
    }

    private IEnumerator Moving()
    {
        Vector3 startPos = transform.position;

        Vector3 dir = (transform.position - GamePlayManager.instance.player.transform.position).normalized;

        dir.x = Mathf.Abs(dir.x) > Mathf.Abs(dir.z) ? Mathf.Round(dir.x) : 0;
        dir.z = Mathf.Abs(dir.z) > Mathf.Abs(dir.x) ? Mathf.Round(dir.z) : 0;
        dir.y = 0;

        dir *= 0.75f;

        Vector3 pos = transform.position + dir;
        Vector3 size = Vector3.Scale(transform.lossyScale, GetComponent<BoxCollider>().size);

        Collider[] objs = Physics.OverlapBox(pos, size / 2f, Quaternion.identity, ~LayerMask.GetMask("Floor"), QueryTriggerInteraction.Ignore);

        for(int i=0; i < objs.Length; i++)
        {
            print(objs[i].name);
        }

        if(objs.Length > 0)
        {
            isCanMove = true;
            yield break;
        }

        float progress = 0f;

        while(progress <= 1f)
        {
            progress += Time.deltaTime * 5f;
            transform.position = Vector3.Lerp(startPos, pos, progress);
            yield return null;
        }

        isCanMove = true;
        yield break;
    }
}
