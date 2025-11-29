namespace Runtime.MVC.Model
{
    public class EnemyModel
    {
        public float Speed { get; }
        public float Accuracy { get; }

        public EnemyModel(EnemySettingsSO settings)
        {
            Speed = settings.DefaultSettings.Speed;
            Accuracy = settings.DefaultSettings.Accuracy;
        }
    }
}