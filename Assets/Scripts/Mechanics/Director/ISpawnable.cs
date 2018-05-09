using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface ISpawnable {

  bool location_spawnable(Vector2 grid_location);

  void spawn(Vector2 grid_location);

  void test(List<Vector2> possible_spawn_locations);
}
