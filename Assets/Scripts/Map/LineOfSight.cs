using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LineOfSight {

  private Vector2 start;
  private Vector2 finish;
  private Map map;
  
  public LineOfSight(Vector2 start, Vector2 finish, Map map) {
    this.start = start;
    this.finish = finish;
    this.map = map;
  }
  
  public bool blocked() {
    float blockage = 0f;
    var delta = finish - start;
    if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y)) {
      var ratio = delta.y / Mathf.Abs(delta.x);
      for (int i = 0; i < Mathf.Abs(delta.x) - 0.1f; i++) {
        var location = new Vector2(start.x + i * Mathf.Sign(delta.x), Mathf.Round(start.y + ratio * i));
        blockage += map.blockage(location);
      }
    } else {
      var ratio = delta.x / Mathf.Abs(delta.y);
      for (int i = 0; i < Mathf.Abs(delta.y) - 0.1f; i++) {
        var location = new Vector2(Mathf.Round(start.x + ratio * i), start.y + i * Mathf.Sign(delta.y));
        blockage += map.blockage(location);
      }
    }
    return blockage >= 1;
  }
}
