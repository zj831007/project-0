//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Project0.AnimatorComponent animator { get { return (Project0.AnimatorComponent)GetComponent(GameComponentsLookup.Animator); } }
    public bool hasAnimator { get { return HasComponent(GameComponentsLookup.Animator); } }

    public void AddAnimator(UnityEngine.Animator newValue) {
        var index = GameComponentsLookup.Animator;
        var component = CreateComponent<Project0.AnimatorComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceAnimator(UnityEngine.Animator newValue) {
        var index = GameComponentsLookup.Animator;
        var component = CreateComponent<Project0.AnimatorComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveAnimator() {
        RemoveComponent(GameComponentsLookup.Animator);
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

    static Entitas.IMatcher<GameEntity> _matcherAnimator;

    public static Entitas.IMatcher<GameEntity> Animator {
        get {
            if (_matcherAnimator == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Animator);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherAnimator = matcher;
            }

            return _matcherAnimator;
        }
    }
}
