﻿using UnityEngine;
using System.Collections;

public class AnimationSpin : MonoBehaviour {

    Animation an;

	void Update () {
        an = gameObject.GetComponent<Animation>();
        an.Play();
	}
}
