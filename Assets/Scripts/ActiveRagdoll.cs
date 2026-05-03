using System.Collections.Generic;
using UnityEngine;

public class ActiveRagdoll : MonoBehaviour
{
    [Header("Drive Settings")]

    public float spring = 1;
    public float damper = 0;

    // PRIVATE //
    ActiveRagdollJoint[] m_joints;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_joints = GetComponentsInChildren<ActiveRagdollJoint>();

        // get all joints
        foreach(ActiveRagdollJoint joint in m_joints)
        {
            joint.spring = spring;
            joint.damper = damper;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
