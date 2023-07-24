namespace EnemyScripts.Interfaces
{
    public interface ITriggerCheckable
    {
        bool IsAggro { get; set; }
        bool IsWithinAttackDistance { get; set; }
    
        void SetAggroStatus(bool isAggro);
        void SetAttackDistanceBool(bool isWithinAttackDistance);
    }
}
