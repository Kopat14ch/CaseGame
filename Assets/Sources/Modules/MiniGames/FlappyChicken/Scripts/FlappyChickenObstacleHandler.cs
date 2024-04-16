namespace Sources.Modules.MiniGames.FlappyChicken.Scripts
{
    public class FlappyChickenObstacleHandler
    {
        private readonly FlappyChickenObstacleRoot[] _obstacles;

        public FlappyChickenObstacleHandler(FlappyChickenObstacleRoots obstacles)
        {
            _obstacles = obstacles.Obstacles;
        }
    }
}