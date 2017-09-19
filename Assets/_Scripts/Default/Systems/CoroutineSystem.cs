using Entitas;

public class CoroutineSystem : IExecuteSystem
{
    IGroup<GameEntity> _inputCoroutinesGroup;
    IGroup<GameEntity> _gameCoroutinesGroup;

    public CoroutineSystem(Contexts contexts)
    {
        _inputCoroutinesGroup = contexts.game.GetGroup(GameMatcher.Coroutine);
        _gameCoroutinesGroup = contexts.game.GetGroup(GameMatcher.Coroutine);
    }

    void IExecuteSystem.Execute()
    {
        foreach (var e in _inputCoroutinesGroup.GetEntities())
        {
            var coroutine = e.coroutine.value;
            if (!coroutine.MoveNext())
            {
                e.RemoveCoroutine();
            }
        }
        foreach (var e in _gameCoroutinesGroup.GetEntities())
        {
            var coroutine = e.coroutine.value;
            if (!coroutine.MoveNext())
            {
                e.RemoveCoroutine();
            }
        }
    }
}
