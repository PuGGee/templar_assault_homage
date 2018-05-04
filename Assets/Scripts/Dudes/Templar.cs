using UnityEngine;
using System.Collections;

public class Templar : Actor {

  public const int UP = 0;
  public const int DOWN = 2;
  public const int LEFT = 1;
  public const int RIGHT = 3;

  public SpriteRenderer scanner;

  public int vitality;
  public int armour;
  public int movement;

  public int shots;
  public int damage;
  public int accuracy;

  private int _health;
  private int _shots_fired;
  private int _moved;

  private int _direction;

  private Coroutine co;

  public int shots_fired {
    get {
      return _shots_fired;
    }
  }

  public int moved {
    get {
      return _moved;
    }
  }

  public int direction {
    get {
      return _direction;
    }
  }

  void Start() {
    _health = vitality;
    reset_turn();
  }

  public void reset_turn() {
    _moved = 0;
    _shots_fired = 0;
  }

  public void shoot() {
    _shots_fired++;
  }

  public void turn_to(int direction) {
    _direction = direction;
    scanner.enabled = true;
    transform.rotation = Quaternion.Euler(0, 0, _direction * 90);
    if (co != null) StopCoroutine(co);
    co = StartCoroutine(disable_scanner());
  }

  public bool within_arc(Vector2 current_grid_location, Vector2 grid_location) {
    var distance = grid_location - current_grid_location;
    if (_direction == UP) {
      return distance.y > 0 && Mathf.Abs(distance.x) <= Mathf.Abs(distance.y);
    } else if (_direction == DOWN) {
      return distance.y < 0 && Mathf.Abs(distance.x) <= Mathf.Abs(distance.y);
    } else if (_direction == LEFT) {
      return distance.x < 0 && Mathf.Abs(distance.y) <= Mathf.Abs(distance.x);
    } else {
      return distance.x < 0 && Mathf.Abs(distance.y) <= Mathf.Abs(distance.x);
    }
  }

  public override void move(Vector2 current_grid_location, Vector2 new_grid_location) {
    base.move(current_grid_location, new_grid_location);
    _moved++;
  }

  public void hurt(int amount) {
    _health -= amount;
    Debug.Log("reduced to " + _health + " health");
  }

  IEnumerator disable_scanner() {
    yield return new WaitForSeconds(1);
    scanner.enabled = false;
  }
}
