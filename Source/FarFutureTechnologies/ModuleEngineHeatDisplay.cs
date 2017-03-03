﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace FarFutureTechnologies
{
    public class ModuleEngineHeatDisplay: PartModule
    {

        [KSPField(isPersistant = false, guiActive = true, guiName = "Heat Production")]
        public string HeatProductionStatus = "N/A";

        private ModuleEnginesFX[] engines;

        public override string GetInfo()
        {
          string msg = "";
          engines = part.GetComponents<ModuleEnginesFX>();
          foreach (ModuleEnginesFX engine in engines)
          {
            msg += String.Format("<b>{0}</b>\n", engine.engineID);
            msg+= String.Format("Heat production (full throttle): {0:F1} MW\n\n", engine.heatProduction*800.0*0.025*0.4975*part.mass/1000.0);
          }
          return msg;
        }

        public void Start()
        {
          if (HighLogic.LoadedSceneIsFlight)
          {
              engines = part.GetComponents<ModuleEnginesFX>();
          }
        }

        protected void FixedUpdate()
        {
            if (HighLogic.LoadedSceneIsFlight)
            {
              HeatProductionStatus = "No engines active";
              foreach (ModuleEnginesFX engine in engines)
              {
                if (engine.EngineIgnited)
                    HeatProductionStatus = String.Format("{0:F1} MW", engine.heatProduction * 800.0 * 0.025 * 0.4975 * part.mass / 1000.0 * engine.GetCurrentThrust() / engine.GetMaxThrust());
              }
            }
        }

    }
}