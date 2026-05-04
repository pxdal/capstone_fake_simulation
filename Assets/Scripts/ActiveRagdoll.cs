using System.Collections.Generic;
using UnityEngine;

public class ActiveRagdoll : MonoBehaviour
{
    [Header("Drive Settings")]

    public float spring = 1;
    public float damper = 0;

    [Header("Jitter Settings")]

    public float jitterRate = 0.1f;
    public float jitterIncreaseRate = 0.1f;
    public float jitterMagnitudeDeg = 10;
    public float bigJitterEvery = 2;
    public float bigJitterMultiplier = 10;
    public float deathLimit = 100;

    // PRIVATE //
    ActiveRagdollJoint[] m_joints;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_joints = GetComponentsInChildren<ActiveRagdollJoint>();

        // get all joints
        foreach(ActiveRagdollJoint joint in m_joints)
        {
            if(joint.spring <= 0) joint.spring = spring;
            if(joint.damper < 0) joint.damper = damper;

            joint.jitterRate = jitterRate;
            joint.jitterMagnitudeDeg = jitterMagnitudeDeg;
            joint.jitterIncreaseRate = jitterIncreaseRate;
            joint.bigJitterEvery = bigJitterEvery;
            joint.bigJitterMultiplier = bigJitterMultiplier;
            joint.deathLimit = deathLimit;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
