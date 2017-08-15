using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShootingPhase : Phase {

  private int _move_counter;
  private List<Templar> _shot_actors;
  private List<int> _shots_fired;

  private Templar _selected_unit;

  public ShootingPhase(Map map) : base(map) {
    _move_counter = 0;
    _shot_actors = new List<Templar>();
    _shots_fired = new List<int>();
    Debug.Log("Shooting Phase");
  }

  public override void click(Vector2 grid_location) {
    var target = map.get_actor_at(grid_location);
    if (target != null) {
      var templar = target.GetComponent<Templar>();
      if (templar != null) {
        _selected_unit = templar;
      } else {
        var alien = target.GetComponent<Alien>();
        if (alien != null && _selected_unit != null && can_shoot(_selected_unit)) {
          shoot(_selected_unit, alien);
        }
      }
    }
  }

  public override Phase next_phase() {
    return new MovementPhase(map);
  }

  private void shoot(Templar shooter, Alien target) {
    if (_shot_actors.Contains(shooter)) {
      _shots_fired[_shot_actors.IndexOf(shooter)] += 1;
    } else {
      _shot_actors.Add(shooter);
      _shots_fired.Add(1);
    }

    var damage = new Damage(shooter.damage, target.armour, shooter.accuracy).calculate();
    target.hurt(damage);
  }

  private bool can_shoot(Templar shooter) {
    return !_shot_actors.Contains(shooter) || _shots_fired[_shot_actors.IndexOf(shooter)] < shooter.shots;
  }

  private void move_aliens() {
    _move_counter++;
    foreach (var alien in map.aliens()) {
      // complicated alien movement stuff
    }
  }
}
