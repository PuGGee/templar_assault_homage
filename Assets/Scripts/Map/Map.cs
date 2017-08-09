using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {

  void Start() {
    transform.localPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 20));
  }
}
