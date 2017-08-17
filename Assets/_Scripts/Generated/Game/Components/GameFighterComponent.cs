//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly Project0.FighterComponent fighterComponent = new Project0.FighterComponent();

    public bool isFighter {
        get { return HasComponent(GameComponentsLookup.Fighter); }
        set {
            if (value != isFighter) {
                if (value) {
                    AddComponent(GameComponentsLookup.Fighter, fighterComponent);
                } else {
                    RemoveComponent(GameComponentsLookup.Fighter);
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

    static Entitas.IMatcher<GameEntity> _matcherFighter;

    public static Entitas.IMatcher<GameEntity> Fighter {
        get {
            if (_matcherFighter == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Fighter);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherFighter = matcher;
            }

            return _matcherFighter;
        }
    }
}
