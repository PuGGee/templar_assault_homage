using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fog {

  private int width;
  private int height;

  private bool[,] _fog_matrix;

  private const int FOG_DISTANCE = 6;

  public Fog(int width, int height) {
    this.width = width;
    this.height = height;
  }

  public bool foggy(Vector2 location) {
    return _fog_matrix[(int)location.x, (int)location.y];
  }

  public void generate_fog(List<Vector2> player_locations) {
    _fog_matrix = new bool[width, height];
    for (int x = 0; x < width; x++) {
      for (int y = 0; y < height; y++) {
        _fog_matrix[x, y] = calculate_fog(new Vector2((float)x, (float)y), player_locations);
      }
    }
  }

  private bool calculate_fog(Vector2 location, List<Vector2> player_locations) {
    bool result = true;
    foreach (var player_location in player_locations) {
      var distance = Mathf.Abs(player_location.x - location.x) + Mathf.Abs(player_location.y - location.y);
      if (distance <= FOG_DISTANCE) result = false;
    }
    return result;
  }
}
