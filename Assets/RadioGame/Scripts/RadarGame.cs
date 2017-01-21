﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarGame : MonoBehaviour {

    public Signal[] signals;

    [SerializeField]
    GameObject signal;

    [SerializeField]
    GameObject armPivot, detector;
    float armAngle;

    [SerializeField]
    float detectorSpeed, detectorMinY, detectorMaxY;

    float xAxis;
    float yAxis;
    float zAxis;

    // Use this for initialization
    void Start() {
        //detector = armPivot.transform.FindChild("Detector").gameObject;
        PlaceSignals();
    }

    // Update is called once per frame
    void Update() {
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");
        zAxis = Input.GetAxis("Z Axis");

        if (Mathf.Abs(xAxis) >= 0.1f || Mathf.Abs(yAxis) >= 0.1f)
        {
            ShowArm();
            SetArmAngle();
            SetDetectorPos();

        } else
        {
            HideArm();
        }

        Debug.Log(zAxis);
        
    }

    void PlaceSignals ()
    {
        for (int i = 0; i < signals.Length; i++)
        {
            Vector3 signalPos = signals[i].position.position;
            GameObject newSignal = Instantiate(signal, signalPos, Quaternion.identity) as GameObject;
            newSignal.transform.GetChild(0).localScale = new Vector3(signals[i].innerRange, 0.1f, signals[i].innerRange);
            newSignal.transform.GetChild(1).localScale = new Vector3(signals[i].outerRange, 0.1f, signals[i].outerRange);
        }
    }

    /*bool DetectorInInnerRange (Signal signal)
    {
        float x = Mathf.Sin(armAngle);

        //return true;
    }*/

    bool DetectorInOuterRange(Signal signal)
    {
        if (true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void SetArmAngle()
    {
        armAngle = (Mathf.Atan2(yAxis, xAxis) * Mathf.Rad2Deg) - 90f;
        armPivot.transform.localRotation = Quaternion.Euler(armPivot.transform.localRotation.x, armPivot.transform.localRotation.y, armAngle);
    }

    void ShowArm ()
    {
        armPivot.SetActive(true);
    }

    void HideArm ()
    {
        armPivot.SetActive(false);
    }

    void SetDetectorPos ()
    {
        Vector3 detectorPos = detector.transform.localPosition;
        if (zAxis >= 0)
            detector.transform.localPosition = new Vector3(detectorPos.x, detectorMinY + (zAxis * detectorMaxY * 2), detectorPos.z);
    }
}
