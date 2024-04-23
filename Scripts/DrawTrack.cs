using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTrack : MonoBehaviour
{
    public LineRenderer track;
    private Vector3 prevPos;

    [SerializeField]
    private float minDist = 0.1f;
    [SerializeField,Range(0.1f,2f)]
    private float width;
    private void Start()
    {
        track = GetComponent<LineRenderer>();
        track.positionCount = 1;
        prevPos = transform.position;
        track.startWidth = track.endWidth = width;
    }

    public void StartLine(Vector3 pos)
    {
        track.positionCount = 1;
        track.SetPosition(0, pos);
    }

    // Update is called once per frame
    public void UpdateLine()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0f;

            if (Vector3.Distance(prevPos, pos) > minDist)
            {
                if (prevPos == transform.position)
                {
                    track.SetPosition(0, pos);
                }
                else
                {
                    track.positionCount++;
                    track.SetPosition(track.positionCount - 1, pos);
                }
                prevPos = pos;
            }
        }
    }
}
