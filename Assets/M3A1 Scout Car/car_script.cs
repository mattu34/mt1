using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor; //このホイールがエンジンにアタッチされているかどうか
    public bool steering; // このホイールがハンドルの角度を反映しているかどうか
}

public class car_script : MonoBehaviour
{

    public List<AxleInfo> axleInfos; // 個々の車軸の情報
    public float maxMotorTorque; //ホイールに適用可能な最大トルク
    public float maxSteeringAngle; // 適用可能な最大ハンドル角度

    public WheelCollider wheelFL;
    public WheelCollider wheelFR;
    public WheelCollider wheelBL;
    public WheelCollider wheelBR;

    public Transform wheelFLTrans;
    public Transform wheelFRTrans;
    public Transform wheelBLTrans;
    public Transform wheelBRTrans;
    float steering = 0.0f;
    float motor = 0.0f;

    void Start()
    {   
        //pythonでいうfor a in A:的なやつ
        /*int[] array = { 0, 1, 2, 3, 4, 5 };

        foreach (int i in array)
        {
            if (i == 2) continue;
            else if (i == 3) break;

            Debug.Log(i);
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        //wheelcolliderの回転速度に合わせてタイヤモデルを回転させる(x軸を中心に回転)
        wheelFLTrans.Rotate(wheelFL.rpm / 60 * 360 * Time.deltaTime, 0, 0); //「なぜ速度にTime.deltaTimeを掛けないといけないのか」の答えは「実際にそのフレームで進んだ"距離"を求める必要があるため」
        wheelFRTrans.Rotate(wheelFR.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        wheelBLTrans.Rotate(wheelBL.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        wheelBRTrans.Rotate(wheelBR.rpm / 60 * 360 * Time.deltaTime, 0, 0);

        //wheelcolliderの角度に合わせてタイヤモデルを回転する（フロントのみ）(y軸を中心に回転)
        wheelFLTrans.localEulerAngles = new Vector3(wheelFLTrans.localEulerAngles.x, wheelFL.steerAngle - wheelFLTrans.localEulerAngles.z, wheelFLTrans.localEulerAngles.z);
        wheelFRTrans.localEulerAngles = new Vector3(wheelFRTrans.localEulerAngles.x, wheelFR.steerAngle - wheelFRTrans.localEulerAngles.z, wheelFRTrans.localEulerAngles.z);

        //Debug.Log(Input.GetAxis("Horizontal"));

    }

    public void FixedUpdate()
    {
        motor = -maxMotorTorque * Input.GetAxis("Vertical");
        steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
        }
    }

}
