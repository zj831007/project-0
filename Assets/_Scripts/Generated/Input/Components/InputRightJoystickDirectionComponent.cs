//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputContext {

    public InputEntity rightJoystickDirectionEntity { get { return GetGroup(InputMatcher.RightJoystickDirection).GetSingleEntity(); } }
    public Project0.RightJoystickDirectionComponent rightJoystickDirection { get { return rightJoystickDirectionEntity.rightJoystickDirection; } }
    public bool hasRightJoystickDirection { get { return rightJoystickDirectionEntity != null; } }

    public InputEntity SetRightJoystickDirection(UnityEngine.Vector3 newValue) {
        if (hasRightJoystickDirection) {
            throw new Entitas.EntitasException("Could not set RightJoystickDirection!\n" + this + " already has an entity with Project0.RightJoystickDirectionComponent!",
                "You should check if the context already has a rightJoystickDirectionEntity before setting it or use context.ReplaceRightJoystickDirection().");
        }
        var entity = CreateEntity();
        entity.AddRightJoystickDirection(newValue);
        return entity;
    }

    public void ReplaceRightJoystickDirection(UnityEngine.Vector3 newValue) {
        var entity = rightJoystickDirectionEntity;
        if (entity == null) {
            entity = SetRightJoystickDirection(newValue);
        } else {
            entity.ReplaceRightJoystickDirection(newValue);
        }
    }

    public void RemoveRightJoystickDirection() {
        rightJoystickDirectionEntity.Destroy();
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputEntity {

    public Project0.RightJoystickDirectionComponent rightJoystickDirection { get { return (Project0.RightJoystickDirectionComponent)GetComponent(InputComponentsLookup.RightJoystickDirection); } }
    public bool hasRightJoystickDirection { get { return HasComponent(InputComponentsLookup.RightJoystickDirection); } }

    public void AddRightJoystickDirection(UnityEngine.Vector3 newValue) {
        var index = InputComponentsLookup.RightJoystickDirection;
        var component = CreateComponent<Project0.RightJoystickDirectionComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceRightJoystickDirection(UnityEngine.Vector3 newValue) {
        var index = InputComponentsLookup.RightJoystickDirection;
        var component = CreateComponent<Project0.RightJoystickDirectionComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveRightJoystickDirection() {
        RemoveComponent(InputComponentsLookup.RightJoystickDirection);
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

    static Entitas.IMatcher<InputEntity> _matcherRightJoystickDirection;

    public static Entitas.IMatcher<InputEntity> RightJoystickDirection {
        get {
            if (_matcherRightJoystickDirection == null) {
                var matcher = (Entitas.Matcher<InputEntity>)Entitas.Matcher<InputEntity>.AllOf(InputComponentsLookup.RightJoystickDirection);
                matcher.componentNames = InputComponentsLookup.componentNames;
                _matcherRightJoystickDirection = matcher;
            }

            return _matcherRightJoystickDirection;
        }
    }
}