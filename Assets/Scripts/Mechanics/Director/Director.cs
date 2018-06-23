using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Director {

  private ISpawnable spawnable;

  private const int SPAWN_DISTANCE = 4;

  public Director(ISpawnable spawnable) {
    this.spawnable = spawnable;
  }

  public void spawn_aliens(List<Vector2> player_locations) {
    var list = spawnable_locations(player_locations);
    list.Sort(delegate(Vector2 vec1, Vector2 vec2) {
      return distance_to_player(vec1, player_locations).CompareTo(distance_to_player(vec2, player_locations));
    });
    // var location = list[(int)(Random.value * Mathf.Min((float)list.Count, 5f))];
    int number = (int)(Random.value * 3) + 1;
    for (int i = 0; i < number; i++) {
      var location = list[(int)(Random.value * list.Count)];
      if (Random.value < 0.2f) {
        spawn_mob(location, 3);
      } else {
        spawn_alien(location);
      }
    }
  }

  private List<Vector2> spawnable_locations(List<Vector2> player_locations) {
    return spawnable.spawn_locations();
    // var result = new List<Vector2>();
    // foreach (var location in spawnable.spawn_locations()) {
    //   if (!square_too_close(location, player_locations)) result.Add(location);
    // }
    // return result;
  }

  private bool square_too_close(Vector2 square, List<Vector2> player_locations) {
    return distance_to_player(square, player_locations) < SPAWN_DISTANCE;
  }
  
  private int distance_to_player(Vector2 square, List<Vector2>player_locations) {
    float result = 9999;
    foreach (var player_location in player_locations) {
      var dist = Mathf.Abs(square.x - player_location.x) + Mathf.Abs(square.y - player_location.y);
      if (dist < result) result = dist;
    }
    return (int)result;
  }
  
  private void spawn_alien(Vector2 spawn_location) {
    if (spawnable.location_spawnable(spawn_location)) spawnable.spawn(spawn_location);
  }
  
  private void spawn_mob(Vector2 spawn_location, int size) {
    spawn_alien(spawn_location);
    int spawn_count = 1;
    foreach (var location in surrounding_squares(spawn_location)) {
      if (spawnable.location_spawnable(location)) {
        spawn_alien(location);
        spawn_count++;
        if (spawn_count >= size) return;
      }
      if (spawnable.location_pathable(location)) {
        foreach (var child_location in surrounding_squares(location)) {
          if (spawnable.location_spawnable(child_location)) {
            spawn_alien(child_location);
            spawn_count++;
            if (spawn_count >= size) return;
          }
        }
      }
    }
  }
  
  private IEnumerable<Vector2> surrounding_squares(Vector2 location) {
    // return yield new Vector2(location.x - 1, location.y - 1);
    yield return new Vector2(location.x, location.y - 1);
    // return yield new Vector2(location.x + 1, location.y - 1);
    yield return new Vector2(location.x + 1, location.y);
    // return yield new Vector2(location.x + 1, location.y + 1);
    yield return new Vector2(location.x, location.y + 1);
    // return yield new Vector2(location.x - 1, location.y + 1);
    yield return new Vector2(location.x - 1, location.y);
  }
}
