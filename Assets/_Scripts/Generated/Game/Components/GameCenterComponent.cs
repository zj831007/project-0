//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Project0.CenterComponent center { get { return (Project0.CenterComponent)GetComponent(GameComponentsLookup.Center); } }
    public bool hasCenter { get { return HasComponent(GameComponentsLookup.Center); } }

    public void AddCenter(UnityEngine.Vector3 newValue) {
        var index = GameComponentsLookup.Center;
        var component = CreateComponent<Project0.CenterComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceCenter(UnityEngine.Vector3 newValue) {
        var index = GameComponentsLookup.Center;
        var component = CreateComponent<Project0.CenterComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveCenter() {
        RemoveComponent(GameComponentsLookup.Center);
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

    static Entitas.IMatcher<GameEntity> _matcherCenter;

    public static Entitas.IMatcher<GameEntity> Center {
        get {
            if (_matcherCenter == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Center);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCenter = matcher;
            }

            return _matcherCenter;
        }
    }
}
