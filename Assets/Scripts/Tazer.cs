using UnityEngine;

public class Tazer : MonoBehaviour
{
    public Animator animatedDummyController;
    public HipJoint hipJoint;

    public GameObject[] lightningObjects;

    public bool tazeKills = true;

    public float tazeSpeedup = 3f;
    public float tazeSpeedDecay = 0.95f;

    public float tazedSpring = 3000f;

    public bool taze = false;

    private bool m_tazing = false;

    private ActiveRagdollJoint[] m_joints;

    private float m_tazeSpeed = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_joints = GetComponentsInChildren<ActiveRagdollJoint>();

        foreach(GameObject lightning in lightningObjects)
        {
            lightning.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!m_tazing && (taze || Input.GetKeyDown(KeyCode.Space))) Taze();

        if (m_tazing)
        {
            if(tazeKills){
                hipJoint.following = false;
            } else
            {
                animatedDummyController.SetFloat("speed", m_tazeSpeed);

                m_tazeSpeed *= tazeSpeedDecay;

                if(m_tazeSpeed < 1)
                {
                    animatedDummyController.SetFloat("speed", 1);

                    lightningObjects[0].SetActive(false);
                    m_tazing = false;

                    foreach(ActiveRagdollJoint joint in m_joints)
                    {
                        joint.ReloadCachedSpring();
                    }
                }
            }
        }
    }

    void Taze(){
        m_tazing = true;

        if(tazeKills){
            animatedDummyController.SetFloat("speed", 0);
            
            foreach(ActiveRagdollJoint joint in m_joints)
            {
                joint.jittering = true;
            }

            foreach(GameObject lightning in lightningObjects)
            {
                lightning.SetActive(true);
                lightning.GetComponent<ParticleSystem>().Play();
            }
        } else
        {
            foreach(ActiveRagdollJoint joint in m_joints)
            {
                joint.CacheSpring();
                joint.spring = tazedSpring;
            }

            m_tazeSpeed = tazeSpeedup;
            lightningObjects[0].SetActive(true);
            lightningObjects[0].GetComponent<ParticleSystem>().Play();
        }
    }
}
