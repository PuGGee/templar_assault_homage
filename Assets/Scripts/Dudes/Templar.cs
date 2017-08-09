using UnityEngine;
using System.Collections;

public class Templar : MonoBehaviour {

  public int vitality;
  public int armour;
  public int movement;

  public int shots;
  public int damage;

  private int _health;

  void Start() {
    _health = vitality;
  }
}
