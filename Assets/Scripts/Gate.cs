using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private Transform LeftHinge;
    [SerializeField] private Transform RightHinge;
    [SerializeField] private float duration;
    [SerializeField] private BoxCollider boxCollider;

    private bool rotating;
    public Queue<IEnumerator> coroutineQueue = new Queue<IEnumerator>();

    void Awake()
    {
        LeftHinge = GameObject.Find("LeftHinge").GetComponent<Transform>();
        RightHinge = GameObject.Find("RightHinge").GetComponent<Transform>();
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
    }

    void Start()
    {
        StartCoroutine(CoroutineCoordinator());
    }

    IEnumerator CoroutineCoordinator()
    {
        while (true)
        {
            while (coroutineQueue.Count > 0)
                yield return StartCoroutine(coroutineQueue.Dequeue());
            boxCollider.enabled = false;
            yield return null;
        }
    }

    public IEnumerator Open()
    {
        if (rotating)
        {
            yield break;
        }
        rotating = true;

        Vector3 LeftNewRot = LeftHinge.transform.eulerAngles - new Vector3(0, 90, 0);
        Vector3 RightNewRot = RightHinge.transform.eulerAngles + new Vector3(0, 90, 0);

        Vector3 LeftCurrentRot = LeftHinge.transform.eulerAngles;
        Vector3 RightCurrentRot = RightHinge.transform.eulerAngles;

        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            LeftHinge.transform.eulerAngles = Vector3.Lerp(LeftCurrentRot, LeftNewRot, counter / duration);
            RightHinge.transform.eulerAngles = Vector3.Lerp(RightCurrentRot, RightNewRot, counter / duration);

            yield return null;
        }
        rotating = false;
    }

    public IEnumerator Close()
    {
        boxCollider.enabled = true;
        if (rotating)
        {
            yield break;
        }
        rotating = true;

        Vector3 LeftNewRot = LeftHinge.transform.eulerAngles + new Vector3(0, 90, 0);
        Vector3 RightNewRot = RightHinge.transform.eulerAngles - new Vector3(0, 90, 0);

        Vector3 LeftCurrentRot = LeftHinge.transform.eulerAngles;
        Vector3 RightCurrentRot = RightHinge.transform.eulerAngles;

        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            LeftHinge.transform.eulerAngles = Vector3.Lerp(LeftCurrentRot, LeftNewRot, counter / duration);
            RightHinge.transform.eulerAngles = Vector3.Lerp(RightCurrentRot, RightNewRot, counter / duration);

            yield return null;
        }
        rotating = false;
    }
}
