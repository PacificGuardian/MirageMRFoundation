using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projection9 : MonoBehaviour
{
    public GameObject CamTruePos;
    public GameObject CamRot;
    public GameObject OppTruePos;
    public GameObject TargetBody;
    public GameObject TargetBodyOri;

    public bool stopBodyHTrack = false;
    public bool stopPosFeed = false;
    public bool stopOriFeed = false;
    public bool usePosDly = true;
    public bool useOriDly = true;

    public float VORORatio = 3.05F;
    public float CamTrackerDist = 16F;
    public bool LockBodyHeight = true;

    private float Multiplier = 31.5F;
    [Range(-180f, 180f)]
    public float RotationX = 0f;
    [Range(-180f, 180f)]
    public float RotationY = 0f;
    [Range(-180f, 180f)]
    public float RotationZ = 0f;
    Vector3 Cam;
    Vector3 Opp;
    Vector3 Proj;

    private List<Vector3> posLst = new List<Vector3>();
    private List<Quaternion> oriLst = new List<Quaternion>();
    [Range(15,30)]//0.02 -- 0.01 fixedDeltaTime
    public int delayFrame = 15;
    [Range(0.2f, 0.3f)]
    public float camdelay = 0.3f;
    private float fixedDeltaTime;

    void Start()
    {
        fixedDeltaTime = camdelay / delayFrame;
        if (!stopBodyHTrack)
            TargetBody.transform.localPosition = new Vector3(0, 0, CamTrackerDist / VORORatio);
    }

    void Update()
    {
        Time.fixedDeltaTime = fixedDeltaTime;
        if (!stopBodyHTrack && LockBodyHeight)
            TargetBody.transform.localPosition = new Vector3(0, 0, CamTrackerDist / VORORatio);
    }

    void FixedUpdate()
    {
        //Calculate position
        Cam = CamTruePos.transform.position;
        Opp = OppTruePos.transform.position;
        Proj = (Opp - Cam);

        //Input cam angle
        Quaternion qr; qr.x = CamRot.transform.localRotation.x; qr.y = CamRot.transform.localRotation.y; qr.z = CamRot.transform.localRotation.z; qr.w = CamRot.transform.localRotation.w;
        Vector3 pP = Quaternion.Euler(RotationX - qr.eulerAngles.x, RotationZ - qr.eulerAngles.z, qr.eulerAngles.y - RotationY) * Proj * Multiplier;

        //Calculate orientation
        TargetBodyOri.transform.rotation = OppTruePos.transform.rotation; //Local rotation
        TargetBodyOri.transform.Rotate(RotationX - qr.eulerAngles.x, RotationZ - qr.eulerAngles.z, qr.eulerAngles.y - RotationY, Space.World);

        posLst.Add(pP);
        oriLst.Add(TargetBodyOri.transform.rotation);
        if (delayFrame == 0)
        {
            posLst.RemoveAt(0);
            oriLst.RemoveAt(0);
        }
        else
            delayFrame--;

       
        if (!stopPosFeed)
        {
            if (usePosDly && posLst.Count > 0)
                transform.position = posLst[0];
            else
                transform.position = pP;
        }

        if (!stopOriFeed)
        {
            if (useOriDly && oriLst.Count > 0)
                TargetBody.transform.rotation = oriLst[0];
            else
                TargetBody.transform.rotation = TargetBodyOri.transform.rotation;
        }
    }
}