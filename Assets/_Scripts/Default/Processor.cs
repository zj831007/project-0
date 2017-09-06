using Entitas;
using System.Collections.Generic;

namespace Project0
{
#if (!ENTITAS_DISABLE_VISUAL_DEBUGGING && UNITY_EDITOR)
    public class Processor : Feature
    {
        Dictionary<string, int> _executeSystemInfoIndices = new Dictionary<string, int>();
        Dictionary<string, int> _initializeSystemInfoIndices = new Dictionary<string, int>();

        public Processor this[string name]
        {
            get
            {
                return (Processor)executeSystemInfos[_executeSystemInfoIndices[name]].system;
            }
        }
        public override Systems Add(ISystem system)
        {
            base.Add(system);
            if (system is IExecuteSystem)
            {
                _executeSystemInfoIndices[system.GetType().Name] = _executeSystemInfoIndices.Count;
            }
            if (system is Entitas.IInitializeSystem)
            {
                _initializeSystemInfoIndices[system.GetType().Name] = _initializeSystemInfoIndices.Count;
            }
            return this;
        }
        public Processor ActivateExecuteSystem(string name)
        {
            int index = _executeSystemInfoIndices[name];
            var info = executeSystemInfos[index];
            info.isActive = true;
            (info.system as IInitializeSystem)?.Initialize();
            return this;
        }
        public Processor DeactivateExecuteSystem(string name)
        {
            int index = _executeSystemInfoIndices[name];
            var info = executeSystemInfos[index];
            info.isActive = false;
            (info.system as ITearDownSystem)?.TearDown();
            return this;
        }
        public Processor ExecuteInitializeSystem(string name)
        {
            int index = _initializeSystemInfoIndices[name];
            var info = initializeSystemInfos[index];
            ((IInitializeSystem)info.system).Initialize();
            return this;
        }
        public Processor(string name) : base(name)
        {

        }
        public Processor()
        {

        }
    }

#else
        
    public class Processor : Feature
    {
        Dictionary<string, int> _executeSystemIndices = new Dictionary<string, int>();
        List<bool> _executeSystemActivities = new List<bool>();
        Dictionary<string, int> _initializeSystemIndices = new Dictionary<string, int>();

        public Processor this[string name]
        {
            get
            {
                return (Processor)_executeSystems[_executeSystemIndices[name]];
            }
        }
        public override void Execute()
        {
            for (int i = 0; i < _executeSystems.Count; i++)
            {
                if (_executeSystemActivities[i])
                {
                    _executeSystems[i].Execute();
                }
            }
        }
        public override Systems Add(ISystem system)
        {
            base.Add(system);
            if (system is IExecuteSystem)
            {
                _executeSystemIndices[system.GetType().Name] = _executeSystemIndices.Count;
                _executeSystemActivities.Add(true);
            }
            if (system is IInitializeSystem)
            {
                _initializeSystemIndices[system.GetType().Name] = _initializeSystemIndices.Count;
            }
            return this;
        }
        public Processor ActivateExecuteSystem(string name)
        {
            int index = _executeSystemIndices[name];
            var system = _executeSystems[index];
            _executeSystemActivities[index] = true;
            (system as IInitializeSystem)?.Initialize();
            return this;
        }
        public Processor DeactivateExecuteSystem(string name)
        {
            int index = _executeSystemIndices[name];
            var system = _executeSystems[index];
            _executeSystemActivities[index] = false;
            (system as ITearDownSystem)?.TearDown();
            return this;
        }
        public Processor ExecuteInitializeSystem(string name)
        {
            int index = _initializeSystemIndices[name];
            var system = _initializeSystems[index];
            system.Initialize();
            return this;
        }
        public Processor(string name) : base(name)
        {

        }
        public Processor()
        {

        }
    }
#endif

}
