//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly Project0.TupleButtonComponent tupleButtonComponent = new Project0.TupleButtonComponent();

    public bool isTupleButton {
        get { return HasComponent(GameComponentsLookup.TupleButton); }
        set {
            if (value != isTupleButton) {
                if (value) {
                    AddComponent(GameComponentsLookup.TupleButton, tupleButtonComponent);
                } else {
                    RemoveComponent(GameComponentsLookup.TupleButton);
                }
            }
        }
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

    static Entitas.IMatcher<GameEntity> _matcherTupleButton;

    public static Entitas.IMatcher<GameEntity> TupleButton {
        get {
            if (_matcherTupleButton == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.TupleButton);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherTupleButton = matcher;
            }

            return _matcherTupleButton;
        }
    }
}