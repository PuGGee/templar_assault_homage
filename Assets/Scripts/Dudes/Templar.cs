using UnityEngine;
using System.Collections;

public class Templar : Actor {

  public const int UP = 0;
  public const int DOWN = 2;
  public const int LEFT = 1;
  public const int RIGHT = 3;

  public SpriteRenderer scanner;
  public SpriteRenderer bullets;
  public RectTransform health_sprite;
  public TextMesh health_text;

  public int vitality;
  public int armour;
  public int movement;

  public int shots;
  public int damage;
  public int accuracy;
  public int range;

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
    bullets.enabled = true;
  }

  public void shoot() {
    _shots_fired++;
    if (!can_shoot()) {
      bullets.enabled = false;
    }
  }

  public bool can_shoot() {
    return _moved > 0 && _shots_fired < shots || _moved == 0 && _shots_fired < 2 * shots;
  }

  public void turn_to(int direction) {
    _direction = direction;
    scanner.enabled = true;
    transform.rotation = Quaternion.Euler(0, 0, _direction * 90);
    health_sprite.rotation = Quaternion.Euler(0, 0, 0);
    if (co != null) StopCoroutine(co);
    co = StartCoroutine(disable_scanner());
  }

  public bool within_arc(Vector2 current_grid_location, Vector2 grid_location) {
    var los = new LineOfSight(current_grid_location, grid_location, map);
    if (los.blocked()) return false;
    
    var dist = Mathf.Abs(current_grid_location.x - grid_location.x) + Mathf.Abs(current_grid_location.y - grid_location.y);
    if (dist > range) return false;
    var distance = grid_location - current_grid_location;
    if (_direction == UP) {
      return distance.y > 0 && Mathf.Abs(distance.x) <= Mathf.Abs(distance.y);
    } else if (_direction == DOWN) {
      return distance.y < 0 && Mathf.Abs(distance.x) <= Mathf.Abs(distance.y);
    } else if (_direction == LEFT) {
      return distance.x < 0 && Mathf.Abs(distance.y) <= Mathf.Abs(distance.x);
    } else {
      return distance.x > 0 && Mathf.Abs(distance.y) <= Mathf.Abs(distance.x);
    }
  }

  public override void move(Vector2 current_grid_location, Vector2 new_grid_location) {
    base.move(current_grid_location, new_grid_location);
    _moved++;
  }

  public void hurt(int amount) {
    _health -= amount;
    health_text.text = _health.ToString();
    if (_health <= 0) die();
  }

  IEnumerator disable_scanner() {
    yield return new WaitForSeconds(1);
    scanner.enabled = false;
  }
}
