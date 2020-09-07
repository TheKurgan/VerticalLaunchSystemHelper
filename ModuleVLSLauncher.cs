using System.Collections.Generic;
using UnityEngine;

namespace VLSLauncher
{
    public class ModuleVLSLauncher : PartModule
    {
        private bool setup = false;

        public override void OnStart(StartState state)
        {
            if (HighLogic.LoadedSceneIsFlight)
            {
                base.part.force_activate();
            }
            base.OnStart(state);
        }

        public void Update()
        {
            if (HighLogic.LoadedSceneIsFlight && !setup)
            {
                setup = true;
                Setup();
            }
        }

        private void Setup()
        {
            if (base.part.children.Count >= 0)
            {
                List<Part>.Enumerator enumerator = base.part.children.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    if (!((Object)enumerator.Current == (Object)null))
                    {
                        enumerator.Current.AddModule("ModuleVLSMissile", true);
                    }
                }
                enumerator.Dispose();
            }
        }
    }
}
