//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly Project0.TouchingComponent touchingComponent = new Project0.TouchingComponent();

    public bool isTouching {
        get { return HasComponent(GameComponentsLookup.Touching); }
        set {
            if (value != isTouching) {
                if (value) {
                    AddComponent(GameComponentsLookup.Touching, touchingComponent);
                } else {
                    RemoveComponent(GameComponentsLookup.Touching);
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

    static Entitas.IMatcher<GameEntity> _matcherTouching;

    public static Entitas.IMatcher<GameEntity> Touching {
        get {
            if (_matcherTouching == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Touching);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherTouching = matcher;
            }

            return _matcherTouching;
        }
    }
}