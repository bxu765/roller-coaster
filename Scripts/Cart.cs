using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart : MonoBehaviour
{

    public SpriteRenderer cart;
    public DrawTrack draw;
    public float velocity = 0.1f;
    public float acceleration = 0.1f;
    public float friction = 0.1f;
    public float mass = 1.0f;

    public void changeVelocity(float newVelocity)
    {
        velocity = newVelocity;
    }

    public void changeAcceleration(float newAcceleration)
    {
        acceleration = newAcceleration;
    }
    public void changeFriction(float newFriction) { friction = newFriction;}

    public void changeMass(float newMass) {  mass = newMass; cart.size = new Vector2(Mathf.Sqrt(mass), Mathf.Sqrt(mass)); }

    Vector3[] positions;
    bool moving = false;
    int index;
    private void OnMouseDown()
    {
        draw.StartLine(transform.position);
    }
    private void OnMouseUp()
    {
        positions = new Vector3[draw.track.positionCount];
        draw.track.GetPositions(positions);
        moving = true;
        index = 0;
    }
    private void OnMouseDrag()
    {
        draw.UpdateLine();
    }
    void Start()
    {
        cart = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            Vector2 cur = positions[index];
            transform.position = Vector2.MoveTowards(transform.position, cur, velocity * Time.deltaTime);

            Vector2 dir = cur - (Vector2)transform.position;
            float ang = Mathf.Atan2(dir.normalized.y, dir.normalized.x);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, ang * Mathf.Rad2Deg), velocity);

            velocity -= Mathf.Sin(ang) * acceleration / 2;
            float frictionForce = Mathf.Abs(Mathf.Cos(ang) * acceleration * friction);
            if (frictionForce > Mathf.Abs(velocity))
            {
                velocity = 0;
            } else
            {
                if (velocity > 0)
                {
                    velocity -= frictionForce;
                } else
                {
                    velocity += frictionForce;
                }
                
            }

            float dist = Vector2.Distance(transform.position, cur);
            if (dist < 0.05f)
            {
                index++;
            }
            if (index > positions.Length - 1)
            {
                moving = false;
            }

        }
    }
}
