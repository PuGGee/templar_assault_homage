using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Director {

  private ISpawnable spawnable;

  public Director(ISpawnable spawnable) {
    this.spawnable = spawnable;
  }

  public void spawn_aliens(List<Vector2> player_locations) {

  }
}
