//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Project0.TouchPositionComponent touchPosition { get { return (Project0.TouchPositionComponent)GetComponent(GameComponentsLookup.TouchPosition); } }
    public bool hasTouchPosition { get { return HasComponent(GameComponentsLookup.TouchPosition); } }

    public void AddTouchPosition(UnityEngine.Vector3 newValue) {
        var index = GameComponentsLookup.TouchPosition;
        var component = CreateComponent<Project0.TouchPositionComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceTouchPosition(UnityEngine.Vector3 newValue) {
        var index = GameComponentsLookup.TouchPosition;
        var component = CreateComponent<Project0.TouchPositionComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveTouchPosition() {
        RemoveComponent(GameComponentsLookup.TouchPosition);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherTouchPosition;

    public static Entitas.IMatcher<GameEntity> TouchPosition {
        get {
            if (_matcherTouchPosition == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.TouchPosition);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherTouchPosition = matcher;
            }

            return _matcherTouchPosition;
        }
    }
}