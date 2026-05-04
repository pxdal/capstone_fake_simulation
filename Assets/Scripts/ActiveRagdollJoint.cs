using UnityEngine;

public class ActiveRagdollJoint : MonoBehaviour
{
    public Transform pairedTo;

    [Header("Drive Settings")]

    public float spring;
    public float damper;

    public bool following = true;

    [Header("Jitter Settings")]

    public bool jittering = false;

    public float jitterRate = 0.1f;
    public float jitterIncreaseRate = 0.1f;

    public float jitterMagnitudeDeg = 10;

    public float bigJitterEvery = 2;
    
    public float bigJitterMultiplier = 10;

    public float initialBigJitterLength = 0.5f;

    public float deathLimit = 100;

    private ConfigurableJoint m_joint;
    private Quaternion m_startRotationLocal;

    private float m_nextJitter = -1;
    private float m_initialBigJitterEnd = -1;

    private float m_oldSpring;

    public void CacheSpring()
    {
        m_oldSpring = spring;
    }

    public void ReloadCachedSpring()
    {
        spring = m_oldSpring;
    }
    
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
    protected virtual void FixedUpdate()
    {
        UpdateDriveSettings();
        
        if(following){
            Quaternion target = pairedTo.localRotation;

            if (jittering && Time.time > m_nextJitter)
            {
                if(m_nextJitter < 0)
                {
                    m_initialBigJitterEnd = Time.time + initialBigJitterLength;    
                }

                float jMag = jitterMagnitudeDeg;

                if(Time.time < m_initialBigJitterEnd || Random.Range(0, (int)(bigJitterEvery / jitterRate)) == 0)
                {
                    // big twitch
                    jMag *= bigJitterMultiplier;
                }

                Quaternion additional = Quaternion.Euler(Random.Range(-jMag, jMag), Random.Range(-jMag, jMag), Random.Range(-jMag, jMag));

                target *= additional;

                m_nextJitter = Time.time + jitterRate;
                jitterMagnitudeDeg += jitterIncreaseRate;

                if(jitterMagnitudeDeg > deathLimit){
                    following = false;
                    m_joint.angularXMotion = ConfigurableJointMotion.Limited;
                    m_joint.angularYMotion = ConfigurableJointMotion.Limited;
                    m_joint.angularZMotion = ConfigurableJointMotion.Limited;
                }
            }

            m_joint.SetTargetRotationLocal(target, m_startRotationLocal);
        }
    }

    protected virtual void UpdateDriveSettings()
    {
        JointDrive slerpDrive = m_joint.slerpDrive;
        slerpDrive.positionSpring = following ? spring : 0;
        slerpDrive.positionDamper = damper;
        m_joint.slerpDrive = slerpDrive;
    }
}
