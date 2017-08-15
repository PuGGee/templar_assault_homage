using UnityEngine;
using System.Collections;

public class Alien : MonoBehaviour {

  public int vitality;
  public int armour;
  public int movement;

  public int damage;

  private int _health;

  public Map map {
    get; set;
  }

  void Start() {
    _health = vitality;
  }

  public void hurt(int amount) {
    _health -= amount;
    Debug.Log("reduced to " + _health + " health");
  }
}
