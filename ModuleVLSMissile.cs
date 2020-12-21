using System.Collections;
using UnityEngine;
using BDArmory.Modules;

namespace VLSLauncher
{
    public class ModuleVLSMissile : PartModule
    {
        private float mass = 0f;

        private float clearanceRadius = 0.01f;

        private float clearanceLength = 0.01f;

        private float maxOffBoresight = 270f;

        private float maxTurnRateDPS = 0f;

        private float maxTorque = 0f;

        private float delay = 0.6f;

        private bool settingsSaved = false;

        public override void OnStart(StartState state)
        {
            if (HighLogic.LoadedSceneIsFlight)
            {
                base.part.force_activate();
            }
            base.OnStart(state);
        }

        public void FixedUpdate()
        {
            if (HighLogic.LoadedSceneIsFlight && FlightGlobals.ready)
            {
                if (!settingsSaved)
                {
                    settingsSaved = true;
                    GetSettings();
                }
                else if (base.vessel.Parts.Count == 1)
                {
                    StartCoroutine(ResetSettings());
                }
            }
        }

        private void GetSettings()
        {
            MissileLauncher val = base.part.FindModuleImplementing<MissileLauncher>();
            if ((Object)val != (Object)null)
            {
                mass = base.part.mass;
                maxOffBoresight = val.maxOffBoresight;
                maxTurnRateDPS = val.maxTurnRateDPS;
                maxTorque = val.maxTorque;
                clearanceRadius = val.clearanceRadius;
                clearanceLength = val.clearanceLength;

                //Debug.Log("[VLS Missile] SAVING ... maxOffBoresight = " + maxOffBoresight);
                //Debug.Log("[VLS Missile] SAVING ... maxTurnRateDPS = " + maxTurnRateDPS);
                //Debug.Log("[VLS Missile] SAVING ... maxTorque = " + maxTorque);
                //Debug.Log("[VLS Missile] SAVING ... clearanceRadius = " + clearanceRadius);
                //Debug.Log("[VLS Missile] SAVING ... clearanceLength = " + clearanceLength);

                //Debug.Log("[VLS Missile] ... ADJUSTING SETTINGS FOR LAUNCH ...");

                base.part.mass = mass / 100f;
                val.maxOffBoresight = 270f;
                val.maxTurnRateDPS = 0f;
                val.maxTorque = 0f;
                val.clearanceRadius = 0.01f;
                val.clearanceLength = 0.01f;

                //Debug.Log("[VLS Missile] CHANGING ... mass = " + part.mass);
                //Debug.Log("[VLS Missile] CHANGING ... maxOffBoresight = " + val.maxOffBoresight);
                //Debug.Log("[VLS Missile] CHANGING ... maxTurnRateDPS = " + val.maxTurnRateDPS);
                //Debug.Log("[VLS Missile] CHANGING ... maxTorque = " + val.maxTorque);
                //Debug.Log("[VLS Missile] CHANGING ... clearanceRadius = " + val.clearanceRadius);
                //Debug.Log("[VLS Missile] CHANGING ... clearanceLength = " + val.clearanceLength);

            }
        }

        private IEnumerator ResetSettings()
        {
            MissileLauncher missile = base.part.FindModuleImplementing<MissileLauncher>();
            yield return (object)new WaitForSeconds(delay);

            //Debug.Log("[VLS Missile] ... RESET TO SAVED ...");

            base.part.mass = mass;
            missile.maxOffBoresight = maxOffBoresight;
            missile.maxTurnRateDPS = maxTurnRateDPS;
            missile.maxTorque = maxTorque;
            missile.clearanceRadius = clearanceRadius;
            missile.clearanceLength = clearanceLength;

            //Debug.Log("[VLS Missile] RESETING TO SAVED ... mass = " + mass);
            //Debug.Log("[VLS Missile] RESETING TO SAVED ... maxTurnRateDPS = " + maxTurnRateDPS);
            //Debug.Log("[VLS Missile] RESETING TO SAVED ... maxOffBoresight = " + maxOffBoresight);
            //Debug.Log("[VLS Missile] RESETING TO SAVED ... maxTorque = " + maxTorque);
            //Debug.Log("[VLS Missile] RESETING TO SAVED ... clearanceRadius = " + clearanceRadius);
            //Debug.Log("[VLS Missile] RESETING TO SAVED ... clearanceLength = " + clearanceLength);

            yield return (object)new WaitForFixedUpdate();
            Object.Destroy(this);
        }
    }
}
