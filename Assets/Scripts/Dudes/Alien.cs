using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Alien : Actor {

  public int vitality;
  public int armour;
  public int movement;

  public int damage;

  private int _health;

  public SpriteRenderer sprite_renderer;

  void Start() {
    _health = vitality;
  }

  public void hide() {
    sprite_renderer.enabled = false;
  }

  public void show() {
    sprite_renderer.enabled = true;
  }

  public void attack(Vector2 current_location) {
    foreach (var templar in adjacent_templars(current_location)) {
      var damage_amount = new Damage(random_damage(), templar.armour, 100).calculate();
      templar.hurt(damage_amount);
    }
  }

  public void hurt(Vector2 current_location, int amount) {
    _health -= amount;
    Debug.Log("reduced to " + _health + " health");
    if (_health <= 0) {
      die(current_location);
    }
  }

  private void die(Vector2 current_location) {
      map.remove_actor_at(current_location);
      Destroy(gameObject);
  }

  private int random_damage() {
    if (Random.value < 0.2) {
      return damage * 4;
    }
    return damage;
  }

  private List<Templar> adjacent_templars(Vector2 current_location) {
    var result = new List<Templar>();
    var templar = templar_at(current_location.x, current_location.y + 1);
    if (templar != null) {
      result.Add(templar);
    }
    templar = templar_at(current_location.x, current_location.y - 1);
    if (templar != null) {
      result.Add(templar);
    }
    templar = templar_at(current_location.x + 1, current_location.y);
    if (templar != null) {
      result.Add(templar);
    }
    templar = templar_at(current_location.x - 1, current_location.y);
    if (templar != null) {
      result.Add(templar);
    }
    return result;
  }

  private Templar templar_at(float x, float y) {
    var actor = map.get_actor_at(new Vector2(x, y));
    if (actor != null) {
      return actor.GetComponent<Templar>();
    }
    return null;
  }
}
