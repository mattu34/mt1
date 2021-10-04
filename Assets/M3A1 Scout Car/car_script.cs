using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor; //���̃z�C�[�����G���W���ɃA�^�b�`����Ă��邩�ǂ���
    public bool steering; // ���̃z�C�[�����n���h���̊p�x�𔽉f���Ă��邩�ǂ���
}

public class car_script : MonoBehaviour
{

    public List<AxleInfo> axleInfos; // �X�̎Ԏ��̏��
    public float maxMotorTorque; //�z�C�[���ɓK�p�\�ȍő�g���N
    public float maxSteeringAngle; // �K�p�\�ȍő�n���h���p�x

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
        //python�ł���for a in A:�I�Ȃ��
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
        //wheelcollider�̉�]���x�ɍ��킹�ă^�C�����f������]������(x���𒆐S�ɉ�])
        wheelFLTrans.Rotate(wheelFL.rpm / 60 * 360 * Time.deltaTime, 0, 0); //�u�Ȃ����x��Time.deltaTime���|���Ȃ��Ƃ����Ȃ��̂��v�̓����́u���ۂɂ��̃t���[���Ői��"����"�����߂�K�v�����邽�߁v
        wheelFRTrans.Rotate(wheelFR.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        wheelBLTrans.Rotate(wheelBL.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        wheelBRTrans.Rotate(wheelBR.rpm / 60 * 360 * Time.deltaTime, 0, 0);

        //wheelcollider�̊p�x�ɍ��킹�ă^�C�����f������]����i�t�����g�̂݁j(y���𒆐S�ɉ�])
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
