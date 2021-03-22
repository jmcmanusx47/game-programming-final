using UnityEngine;
using System.Collections;

public class ParticleMove : MonoBehaviour
{
    public float speed = 0.1f;

	void Update () {
        transform.Translate(Vector3.back * speed);
	}
}
