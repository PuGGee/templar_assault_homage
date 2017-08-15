using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovementPhase : Phase {

  private Templar _selected_unit;
  private Vector2 _selected_grid_location;
  private List<Templar> _moved_units;

  public MovementPhase(Map map) : base(map) {
    _moved_units = new List<Templar>();
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
        if (script.type == " " && valid_move(_selected_grid_location, grid_location, _selected_unit.movement) && !_moved_units.Contains(_selected_unit)) {
          _selected_unit.move(_selected_grid_location, grid_location);
          _moved_units.Add(_selected_unit);
          _selected_unit = null;
        }
      }
    }
  }

  public override Phase next_phase() {
    return new ShootingPhase(map);
  }

  protected bool valid_move(Vector2 current_grid_location, Vector2 new_grid_location, int movement) {
    var distance = current_grid_location - new_grid_location;
    return Mathf.Abs(distance.x) + Mathf.Abs(distance.y) <= movement;
  }
}
