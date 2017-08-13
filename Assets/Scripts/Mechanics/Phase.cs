using UnityEngine;
using System.Collections;

public abstract class Phase {

  protected Map map;

  public Phase(Map map) {
    this.map = map;
  }

  public abstract void click(Vector2 grid_location);
}
