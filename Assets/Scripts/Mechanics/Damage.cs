using UnityEngine;
using System.Collections;

public class Damage {

  private int armour;
  private int damage;
  private int accuracy;

  public Damage(int damage, int armour, int accuracy) {
    this.armour = armour;
    this.damage = damage;
    this.accuracy = accuracy;
  }

  public int calculate() {
    if (Random.value * 100 > accuracy) {
      Debug.Log("shot misses");
      return 0;
    } else if (Random.value * 100 < armour) {
      Debug.Log("armour deflects");
      return 0;
    } else {
      Debug.Log("shot hits!");
      return damage;
    }
  }
}
