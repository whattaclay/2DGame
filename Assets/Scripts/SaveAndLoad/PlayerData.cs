using Character;

namespace SaveAndLoad
{
    [System.Serializable]
    public class PlayerData
    {
        public float Health;
        public float[] Position;

        public PlayerData(CharacterController2D player)
        {
            Health = player.health.CurrentHealth;
            Position = new float[3];
            Position[0] = player.transform.position.x;
            Position[1] = player.transform.position.y;
            Position[2] = player.transform.position.z;
        }
    }
}