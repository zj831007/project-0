using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entitas;
using UnityEngine;

namespace Project0
{
    //#if (!ENTITAS_DISABLE_VISUAL_DEBUGGING && UNITY_EDITOR)
    public class Processor : Feature
    {
        Dictionary<string, int> _executeSystemInfoIndices = new Dictionary<string, int>();

        public ThirdPersonCameraProcessor this[string name]
        {
            get { return (ThirdPersonCameraProcessor)executeSystemInfos[_executeSystemInfoIndices[name]].system; }
        }
        public override Systems Add(ISystem system)
        {
            base.Add(system);
            if (system is IExecuteSystem)
            {
                _executeSystemInfoIndices[system.GetType().Name] = _executeSystemInfoIndices.Count;
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
        public Processor(string name) : base(name)
        {

        }
        public Processor()
        {

        }
    }
    //#else
    //    public class Processor : Feature {
    //        Dictionary<string, ISystem> _systems;

    //        public ISystem this[string name]
    //        {
    //            get { return _systems[name]; }
    //        }
    //        public Processor(string name) : base(name)
    //        {
    //        }
    //        public Processor() {
    //        }
    //    }
    //#endif

}
