using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class testChange : MonoBehaviour {

    public SkeletonDataAsset non;
    public SkeletonDataAsset red;
    public SkeletonAnimator sk;

	// Use this for initialization
	void Start () {
        GetComponent<SkeletonAnimator>();
	}

    public void ChangeToRed()
    {
        sk.skeletonDataAsset = red;
        sk.Initialize(true);
    }

    public void ChangeToNormal()
    {
        sk.skeletonDataAsset = non;
        sk.Initialize(true);
    }

}
