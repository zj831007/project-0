//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly Project0.PadComponent padComponent = new Project0.PadComponent();

    public bool isPad {
        get { return HasComponent(GameComponentsLookup.Pad); }
        set {
            if (value != isPad) {
                if (value) {
                    AddComponent(GameComponentsLookup.Pad, padComponent);
                } else {
                    RemoveComponent(GameComponentsLookup.Pad);
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

    static Entitas.IMatcher<GameEntity> _matcherPad;

    public static Entitas.IMatcher<GameEntity> Pad {
        get {
            if (_matcherPad == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Pad);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherPad = matcher;
            }

            return _matcherPad;
        }
    }
}
