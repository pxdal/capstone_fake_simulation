using UnityEngine;

public class Tazer : MonoBehaviour
{
    public Animator animatedDummyController;
    public HipJoint hipJoint;

    public GameObject[] lightningObjects;

    public bool taze = false;

    private bool m_tazing = false;

    private ActiveRagdollJoint[] m_joints;

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
            hipJoint.following = false;
        }
    }

    void Taze(){
        m_tazing = true;

        animatedDummyController.SetFloat("speed", 0);
        
        foreach(ActiveRagdollJoint joint in m_joints)
        {
            joint.jittering = true;
        }

        foreach(GameObject lightning in lightningObjects)
        {
            lightning.SetActive(true);
        }
    }
}
