using UnityEngine;
using System.Collections;

public class MovementPhase : Phase {

  private Templar _selected_unit;
  private Vector2 _selected_grid_location;

  public MovementPhase(Map map) : base(map) {}

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
        if (script.type == " ") {
          _selected_unit.move(_selected_grid_location, grid_location);
          _selected_unit = null;
        }
      }
    }
  }
}
