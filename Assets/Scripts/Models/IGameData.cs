namespace Models
{
    //each game data class should inherit this interface, so that the corresponding data will be read by NewtonJson
    public interface IGameData
    {
        string GetKey();
    }
}