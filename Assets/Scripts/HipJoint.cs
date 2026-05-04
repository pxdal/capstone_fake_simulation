using UnityEngine;

public class HipJoint : ActiveRagdollJoint
{
    public float positionSpring;
    public float positiveDamper;

    public bool followingPosition = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Awake()
    {
        base.Awake();

        //GetConfigurableJoint().xMotion = ConfigurableJointMotion.Limited;
		//GetConfigurableJoint().yMotion = ConfigurableJointMotion.Limited;
		//GetConfigurableJoint().zMotion = ConfigurableJointMotion.Limited;
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        //if(followingPosition) GetConfigurableJoint().targetPosition = transform.position;
    }

    protected override void UpdateDriveSettings()
    {
        base.UpdateDriveSettings();

        if (followingPosition)
        {
            GetConfigurableJoint().xMotion = ConfigurableJointMotion.Locked;
		    GetConfigurableJoint().yMotion = ConfigurableJointMotion.Locked;
		    GetConfigurableJoint().zMotion = ConfigurableJointMotion.Locked;
        } else
        {
            GetConfigurableJoint().xMotion = ConfigurableJointMotion.Free;
		    GetConfigurableJoint().yMotion = ConfigurableJointMotion.Free;
		    GetConfigurableJoint().zMotion = ConfigurableJointMotion.Free;
        }

        /*
        GetConfigurableJoint().xMotion = ConfigurableJointMotion.Free;
        GetConfigurableJoint().yMotion = ConfigurableJointMotion.Free;
        GetConfigurableJoint().zMotion = ConfigurableJointMotion.Free;

        JointDrive drive = GetConfigurableJoint().xDrive;
        drive.positionSpring = positionSpring;
        drive.positionDamper = positiveDamper;
        GetConfigurableJoint().xDrive = drive;

        drive = GetConfigurableJoint().yDrive;
        drive.positionSpring = positionSpring;
        drive.positionDamper = positiveDamper;
        GetConfigurableJoint().yDrive = drive;

        drive = GetConfigurableJoint().zDrive;
        drive.positionSpring = positionSpring;
        drive.positionDamper = positiveDamper;
        GetConfigurableJoint().zDrive = drive;*/
    }
}
