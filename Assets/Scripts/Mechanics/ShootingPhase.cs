using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShootingPhase : Phase {

  private int _move_counter;

  private Templar _selected_unit;
  private Vector2 _selected_square;

  public ShootingPhase(Map map) : base(map) {
    _move_counter = 0;
    Debug.Log("Shooting Phase");
    move_aliens();
  }

  public override void click(Vector2 grid_location) {
    var target = map.get_actor_at(grid_location);
    if (target != null) {
      var templar = target.GetComponent<Templar>();
      if (templar != null) {
        _selected_unit = templar;
        _selected_square = grid_location;
      } else {
        var alien = target.GetComponent<Alien>();
        if (alien != null && _selected_unit != null && can_shoot(_selected_unit) && _selected_unit.within_arc(_selected_square, grid_location)) {
          shoot(_selected_unit, alien);
        } else {
          Debug.Log("Invalid target");
          Debug.Log("Direction: " + _selected_unit.direction);
        }
      }
    }
  }

  public override Phase next_phase() {
    return new MovementPhase(map);
  }

  private void shoot(Templar shooter, Alien target) {
    shooter.shoot();

    var damage = new Damage(shooter.damage, target.armour, shooter.accuracy).calculate();
    target.hurt(damage);
  }

  private bool can_shoot(Templar shooter) {
    return shooter.shots_fired < shooter.shots;
  }

  private void move_aliens() {
    // _move_counter++;
    // List<Path> paths = new List<Path>();
    // foreach (var alien in map.aliens()) {
    //   var position = map.actor_location(alien.transform);

    // }
  }
}
