using MilitaryElite.Enums;
namespace MilitaryElite.Interfaces
{
    public interface IMission
    {
        string CodeName { get; }

        State States { get; }

        public void CompleteMission();
    }
}