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

  public void test(List<Vector2> player_locations) {
    spawnable.test(spawnable_locations(player_locations));
  }

  private List<Vector2> spawnable_locations(List<Vector2> player_locations) {
    var result = new List<Vector2>();
    foreach (var player_location in player_locations) {
      for (int i = -SPAWN_DISTANCE; i <= SPAWN_DISTANCE; i++) {
        int x_pos = SPAWN_DISTANCE - i;
        int y_pos = SPAWN_DISTANCE - Mathf.Abs(x_pos);
        var top_square = new Vector2(x_pos, y_pos);
        if (!square_too_close(top_square, player_locations) && spawnable.location_spawnable(top_square)) {
          result.Add(top_square);
        }
        if (Mathf.Abs(x_pos) != SPAWN_DISTANCE) {
          var bottom_square = new Vector2(x_pos, -y_pos);
          if (!square_too_close(bottom_square, player_locations) && spawnable.location_spawnable(bottom_square)) {
            result.Add(bottom_square);
          }
        }
      }
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
