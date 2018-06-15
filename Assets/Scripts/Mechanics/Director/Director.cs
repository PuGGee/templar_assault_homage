using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Director {

  private ISpawnable spawnable;

  private const int SPAWN_DISTANCE = 7;

  public Director(ISpawnable spawnable) {
    this.spawnable = spawnable;
  }

  public void spawn_aliens(List<Vector2> player_locations) {
    var list = spawnable_locations(player_locations);
    var location = list[(int)(Random.value * list.Count)];
    spawnable.spawn(location);
  }

  private List<Vector2> spawnable_locations(List<Vector2> player_locations) {
    var result = new List<Vector2>();
    foreach (var location in spawnable.spawn_locations()) {
      if (!square_too_close(location, player_locations)) result.Add(location);
    }
    return result;
  }

  private bool square_too_close(Vector2 square, List<Vector2> player_locations) {
    bool result = false;
    foreach (var player_location in player_locations) {
      var dist = Mathf.Abs(square.x - player_location.x) + Mathf.Abs(square.y - player_location.y);
      if (dist < SPAWN_DISTANCE) result = true;
    }
    return result;
  }
}
