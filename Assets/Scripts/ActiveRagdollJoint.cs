using UnityEngine;

public class ActiveRagdollJoint : MonoBehaviour
{
    public Transform pairedTo;

    public float spring;
    public float damper;

    private ConfigurableJoint m_joint;
    private Quaternion m_startRotationLocal;

    public ConfigurableJoint GetConfigurableJoint()
    {
        return m_joint;    
    }

    protected virtual void Awake()
    {
        m_joint = GetComponent<ConfigurableJoint>();

        m_startRotationLocal = transform.localRotation;

        m_joint.SetupAsCharacterJoint();
        m_joint.angularXMotion = ConfigurableJointMotion.Free;
		m_joint.angularYMotion = ConfigurableJointMotion.Free;
		m_joint.angularZMotion = ConfigurableJointMotion.Free;

        UpdateDriveSettings();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        UpdateDriveSettings();
        m_joint.SetTargetRotationLocal(pairedTo.localRotation, m_startRotationLocal);
    }

    protected virtual void UpdateDriveSettings()
    {
        JointDrive slerpDrive = m_joint.slerpDrive;
        slerpDrive.positionSpring = spring;
        slerpDrive.positionDamper = damper;
        m_joint.slerpDrive = slerpDrive;
    }
}
