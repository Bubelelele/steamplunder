using UnityEngine;

public static class PlayerInput {
    
    public static Vector2 Dir2 => new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    public static Vector3 Dir3 => new Vector3(Dir2.x, 0, Dir2.y);

}
