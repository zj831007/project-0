//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly Project0.CameraComponent cameraComponent = new Project0.CameraComponent();

    public bool isCamera {
        get { return HasComponent(GameComponentsLookup.Camera); }
        set {
            if (value != isCamera) {
                if (value) {
                    AddComponent(GameComponentsLookup.Camera, cameraComponent);
                } else {
                    RemoveComponent(GameComponentsLookup.Camera);
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

    static Entitas.IMatcher<GameEntity> _matcherCamera;

    public static Entitas.IMatcher<GameEntity> Camera {
        get {
            if (_matcherCamera == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Camera);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCamera = matcher;
            }

            return _matcherCamera;
        }
    }
}
