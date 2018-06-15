using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface ISpawnable {

  List<Vector2> spawn_locations();

  void spawn(Vector2 grid_location);
}
