using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
public class Gravity : MonoBehaviour
{
    Rigidbody rb;
    const float G = 0.006674f; //Gravitional Constant 6.674

    //create a list of objects in the galaxy to attract
    public static List<Gravity> otherObjectList;

    [SerializeField] bool planet = false;
    [SerializeField] int orbitSpeed = 1000;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (otherObjectList == null)
        {
            otherObjectList = new List<Gravity>();
        }

        otherObjectList.Add(this);

        if (!planet) { rb.AddForce(Vector3.left * orbitSpeed); }
    }



    private void FixedUpdate()
    {
        foreach (Gravity obj in otherObjectList) 
        {
            if (obj != this)
            {
                Attract(obj);
            }
        }
    }
    void Attract(Gravity other)
    {
        Rigidbody otherRigidbody = other.rb;
        //get direction between 2 objects
        Vector3 direction = rb.position - otherRigidbody.position;

        //get only distance between 2 objects
        float distance = direction.magnitude;

        //if 2 objects are at the same position, just return = do nothing to avoid collision
        if (distance == 0f ) { return; }

        //F = G*(m1*m2)/r*r
        float forceMagnitude = G*(rb.mass * otherRigidbody.mass)/Mathf.Pow(distance,2);

        //Combine force magnitude with its direction (normalize)
        //ro form gravitional force (Vector)
        Vector3 gravityForce = forceMagnitude*direction.normalized;

        otherRigidbody.AddForce(gravityForce);
    }
}
