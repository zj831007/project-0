//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Project0.InputTypeComponent inputType { get { return (Project0.InputTypeComponent)GetComponent(GameComponentsLookup.InputType); } }
    public bool hasInputType { get { return HasComponent(GameComponentsLookup.InputType); } }

    public void AddInputType(Project0.InputType newValue) {
        var index = GameComponentsLookup.InputType;
        var component = CreateComponent<Project0.InputTypeComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceInputType(Project0.InputType newValue) {
        var index = GameComponentsLookup.InputType;
        var component = CreateComponent<Project0.InputTypeComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveInputType() {
        RemoveComponent(GameComponentsLookup.InputType);
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

    static Entitas.IMatcher<GameEntity> _matcherInputType;

    public static Entitas.IMatcher<GameEntity> InputType {
        get {
            if (_matcherInputType == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.InputType);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherInputType = matcher;
            }

            return _matcherInputType;
        }
    }
}
