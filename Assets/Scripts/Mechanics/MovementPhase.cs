using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovementPhase : Phase {

  private Templar _selected_unit;
  private Vector2 _selected_grid_location;

  public MovementPhase(Map map) : base(map) {
    Debug.Log("Movement Phase");
  }

  public override void click(Vector2 grid_location) {
    var target = map.get_actor_at(grid_location);
    var tile = map.get_tile_at(grid_location);
    if (target != null) {
      var templar = target.GetComponent<Templar>();
      if (templar != null) {
        _selected_unit = templar;
        _selected_grid_location = grid_location;
      }
    } else {
      if (_selected_unit != null) {
        var script = tile.GetComponent<Tile>();
        if (script.type == " " && valid_move(_selected_grid_location, grid_location, _selected_unit.movement) && _selected_unit.moved < 1) {
          _selected_unit.move(_selected_grid_location, grid_location);
        }
      }
    }
  }

  public override void keypress(string key) {
    if (key == "w") {
      _selected_unit.turn_to(Templar.UP);
    } else if (key == "s") {
      _selected_unit.turn_to(Templar.DOWN);
    } else if (key == "a") {
      _selected_unit.turn_to(Templar.LEFT);
    } else if (key == "d") {
      _selected_unit.turn_to(Templar.RIGHT);
    }
  }

  public override Phase next_phase() {
    return new ShootingPhase(map);
  }

  public override bool can_progress() {
    return true;
  }

  protected bool valid_move(Vector2 current_grid_location, Vector2 new_grid_location, int movement) {
    var path_finder = new PathFinder(
      map,
      current_grid_location,
      new List<Vector2> { new_grid_location }
    );
    var path = path_finder.find_path();
    return path.Count - 1 <= movement;
  }
}
