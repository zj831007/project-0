//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputEntity {

    public Project0.PreviousPosition previousPosition { get { return (Project0.PreviousPosition)GetComponent(InputComponentsLookup.PreviousPosition); } }
    public bool hasPreviousPosition { get { return HasComponent(InputComponentsLookup.PreviousPosition); } }

    public void AddPreviousPosition(UnityEngine.Vector3 newValue) {
        var index = InputComponentsLookup.PreviousPosition;
        var component = CreateComponent<Project0.PreviousPosition>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplacePreviousPosition(UnityEngine.Vector3 newValue) {
        var index = InputComponentsLookup.PreviousPosition;
        var component = CreateComponent<Project0.PreviousPosition>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemovePreviousPosition() {
        RemoveComponent(InputComponentsLookup.PreviousPosition);
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
public sealed partial class InputMatcher {

    static Entitas.IMatcher<InputEntity> _matcherPreviousPosition;

    public static Entitas.IMatcher<InputEntity> PreviousPosition {
        get {
            if (_matcherPreviousPosition == null) {
                var matcher = (Entitas.Matcher<InputEntity>)Entitas.Matcher<InputEntity>.AllOf(InputComponentsLookup.PreviousPosition);
                matcher.componentNames = InputComponentsLookup.componentNames;
                _matcherPreviousPosition = matcher;
            }

            return _matcherPreviousPosition;
        }
    }
}