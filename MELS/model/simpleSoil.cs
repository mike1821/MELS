using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace simplesoilModel
{
    class simpleSoil
    {

        //! Cumulative amount of water input into the system		
        double waterIn; //Attribute data member
        //! Cumulative amount of water leaving the system		
        double waterOut; //Attribute data member
        //! Amount of water in soil (mm)
        double waterInSystem;
        //! Plant-available water of whole soil at field capacity (millimetres)		
        double soilFieldCapacity; //Attribute data member
        
        //! Pool containing snow						
        pool theSnow;
        //! Rooting pool
        pool theRooting;
        //! Pool of water beneath the rooting pool
        pool theSubZone;
        //! Plant canopy interception pool
        pool canopyInterception;
        //!Pool that describes the water that can be lost in the soil by evaporation
        pool theEvaporation;
        /*!Pool that describes the water available in the upper part of the rooting zone. Only used when precipitation
        or irrigation water falls onto the soil at a time when the soil moisture in the rooting zone is below the breakpoint
        for reduced transpiration.
        */
        double Sandfraction;
        double Clayfraction;
        double percentOrgMatter;
        pool upperRooting;
        pool belowPWP;

        //! Pointer to a list of instances containing the physical characteristics of each soil layer
        List<layerClass> theLayers; //1:M aggregation part data member
        double maxRootingDepth;
        double drainageConst;
        //relative water content below which transpiration is reduced below the potential
        double breakpoint = 0.5;
        public double getwaterIn() { return waterIn; }
        public double getwaterOut() { return waterOut; }
        public double getsoilFieldCapacity() { return soilFieldCapacity; }
        void setwaterIn(double awaterIn) { waterIn = awaterIn; }
        void setwaterOut(double awaterOut) { waterOut = awaterOut; }
        void setsoilFieldCapacity(double asoilFieldCapacity) { soilFieldCapacity = asoilFieldCapacity; }
        pool gettheSnow() { return theSnow; }
        pool gettheRooting() { return theRooting; }
        pool gettheSubZone() { return theSubZone; }
        public double getcanopyInterception()
        {
            if (canopyInterception != null)
                return canopyInterception.getvolume();
            else
                return 0;
        }
        pool gettheEvaporation() { return theEvaporation; }
        pool getupperRooting() { return upperRooting; }
        public double GetRootingWaterVolume() { return theRooting.getvolume(); }
        void settheSnow(pool atheSnow) { theSnow = atheSnow; }
        void settheRooting(pool atheRooting) { theRooting = atheRooting; }
        void settheSubZone(pool atheSubZone) { theSubZone = atheSubZone; }
        void settheEvaporation(pool atheEvaporation) { theEvaporation = atheEvaporation; }
        void setupperRooting(pool aupperRooting) { upperRooting = aupperRooting; }
        public List<layerClass> gettheLayers() { return theLayers; }
        private double drainage;
        public double Getdrainage() { return drainage; }
        private double snowEvap;
        public double GetsnowEvap() { return snowEvap; }
        //private double irrigationWater;
       // public double GetirrigationWater() { return irrigationWater; }
        private double evap;
        public double Getevap() { return evap; }
        private double transpire;
        public double Gettranspire() { return transpire; }

        public simpleSoil()
        {
        }
        public simpleSoil(simpleSoil copy)
        {
            waterIn = copy.waterIn;
            waterOut = copy.waterOut; ;
            waterInSystem = copy.waterInSystem; ;
            soilFieldCapacity = copy.soilFieldCapacity; ;
            theSnow = copy.theSnow; ;
            theRooting = copy.theRooting; ;
            theSubZone = copy.theSubZone; ;
            canopyInterception = copy.canopyInterception;
            theEvaporation = copy.theEvaporation;
            upperRooting = copy.upperRooting;
            belowPWP = copy.belowPWP;
            theLayers = new List<layerClass>();
            for (int i = 0; i < copy.theLayers.Count; i++)
            {
                layerClass aLayer = new layerClass(copy.theLayers[i]);
                theLayers.Add(aLayer);
            }
            maxRootingDepth = copy.maxRootingDepth;
            drainageConst = copy.drainageConst;
            breakpoint = copy.breakpoint;
        }

        public void reload_simpleSoil(simpleSoil original)
        {
            waterIn = original.waterIn;
            waterOut = original.waterOut; ;
            waterInSystem = original.waterInSystem; ;
            soilFieldCapacity = original.soilFieldCapacity; ;
            theSnow = original.theSnow; ;
            theRooting = original.theRooting; ;
            theSubZone = original.theSubZone; ;
            canopyInterception = original.canopyInterception;
            theEvaporation = original.theEvaporation;
            upperRooting = original.upperRooting;
            belowPWP = original.belowPWP;
            for (int i = 0; i < original.theLayers.Count; i++)
            {
                theLayers[i].reload_layer(original.theLayers[i]);
            }
            maxRootingDepth = original.maxRootingDepth;
            drainageConst = original.drainageConst;
            breakpoint = original.breakpoint;
        }

        //!Initialise an instance of this class
        /*! 
        \param thesoilData pointer to the instance of a class containing the soil parameters
        */
        public void Initialise2(string CropSequenceName, int zoneNR, int soiltypeNo, string cropName, double amaxRootingDepth, double rootingDepth, double totalLAI, 
            double [] layerOrgC)
        {
            theLayers = new List<layerClass>();
            layerClass alayerClass;
            double z_upper = 0.0;
            soilFieldCapacity = 0.0;
            maxRootingDepth = amaxRootingDepth;
            drainageConst = GlobalVars.Instance.theZoneData.thesoilData[soiltypeNo].thesoilWaterData.drainageConstant;
            Sandfraction = GlobalVars.Instance.theZoneData.thesoilData[soiltypeNo].SandFraction;
            Clayfraction = GlobalVars.Instance.theZoneData.thesoilData[soiltypeNo].ClayFraction;
            //Load the lowest soil layer
            int numSoilLayers =GlobalVars.Instance.theZoneData.thesoilData[soiltypeNo].thesoilWaterData.thesoilLayerData.Count;
            double soilDepth = GlobalVars.Instance.theZoneData.thesoilData[soiltypeNo].thesoilWaterData.thesoilLayerData[numSoilLayers-1].z_lower;
            for (int index = 0; index < numSoilLayers; index++)
            {
                double z_lower = GlobalVars.Instance.theZoneData.thesoilData[soiltypeNo].thesoilWaterData.thesoilLayerData[index].z_lower;

                //the field capacity is expressed as 100 * cubic metre/cubic metre
                alayerClass = new layerClass();
                if (index == 0)
                    z_upper = 0.0;
                else
                    z_upper = theLayers[index - 1].getz_lower();  //lower boundary of one layer is the upper boundary of the next
                double layerOM = 0;
                if (index <= 1)
                    layerOM = 100 * 2 * layerOrgC[0] / (10000 * 1000 * 0.5);  //100 for percent, 2 for OM = 2 * orgC, 10000 for ha, 1000 for kg to tonnes, 0.5 for 0.5 metre depth
                else
                    layerOM = 100 * 2 * layerOrgC[1] / (10000 * 1000 * (soilDepth - 0.5));
                double PWP = 100 * GetPermanentWilting(Sandfraction, Clayfraction, layerOM);//100 * theta_f
                double fieldCapacity = 100 * GetFieldCapacity(Sandfraction, Clayfraction, layerOM);//100 * theta_f
                alayerClass.Initialise(z_upper,z_lower,fieldCapacity, PWP);
                soilFieldCapacity += alayerClass.getfieldCapacity() * alayerClass.getthickness();
                theLayers.Add(alayerClass);
            }

            waterIn = 0.0;
            waterOut = 0.0;
            layerClass anewlayerClass;
            theSnow = new pool();
            theSnow.Initialise(0.0, 100000.0, 1.0);
            theSnow.setname("snow");
            anewlayerClass = theLayers[0];
            theEvaporation = new pool();
            theEvaporation.setname("evaporation");
            double poolDepth;
            anewlayerClass = theLayers[0];
            double evapDepth = 10.0 / (1000.0 * (anewlayerClass.getfieldCapacity()-anewlayerClass.getPWP()));   //evaporation pool has always a capacity of10 millimetres
            //depth of rooting pool is always greater or equal to the depth of evaporation pool
            if (evapDepth > 0.1)
                evapDepth = 0.1;
            double evaporationPoolCapacity = calcPoolVolume(0, evapDepth);
            theEvaporation.Initialise(evaporationPoolCapacity, evaporationPoolCapacity, 1.0);
            //initialise the rooting pool
            poolDepth = rootingDepth;
            if (poolDepth < evapDepth)
                poolDepth = evapDepth;
            //calculate the drainage constant for the rooting pool
            double pooldrainageConst = drainageConst + (1 - drainageConst) * (soilDepth - poolDepth) / soilDepth;
            double rootingPoolCapacity = calcPoolVolume(0, poolDepth);
            theRooting = new pool();
            theRooting.Initialise(rootingPoolCapacity, rootingPoolCapacity, pooldrainageConst);
            theRooting.setname("rooting");
            double availWater = 0.0;
            //calculate the thickness of the different layers
            double thickness;
            double waterBelowPWP = 0;
            for (int index = 0; index < theLayers.Count; index++)
            {
                anewlayerClass = gettheLayers()[index];
                thickness = anewlayerClass.getthickness();
                availWater += 1000.0 * thickness * anewlayerClass.getfieldCapacity();
                waterBelowPWP += 1000.0 * anewlayerClass.GetWaterBelowPWP();
            }
            availWater -= waterBelowPWP;
            belowPWP = new pool();
            belowPWP.setname("belowPWP");
            belowPWP.Initialise(waterBelowPWP, waterBelowPWP, 0);
            waterIn += availWater + waterBelowPWP;

            //note initial value of poolDepth is depth of rooting pool
            poolDepth = soilDepth - poolDepth;
            //calculate the drainage constant for the subroot zone
            pooldrainageConst = drainageConst + (1 - drainageConst) * poolDepth / soilDepth;
            theSubZone = new pool();
            double volumeInSubzone = availWater - theRooting.getvolume();
            theSubZone.Initialise(volumeInSubzone, volumeInSubzone, pooldrainageConst);
            theSubZone.setname("subzone");
            //set the upper rooting to zero
            upperRooting = new pool();
            upperRooting.Initialise(0.0, 0.0, 0.0);
            upperRooting.setname("upperRooting");
            //initialise the canopy interception pool.
            canopyInterception = new pool();
            canopyInterception.Initialise(0.0, 0.5 * totalLAI, 1.0);
            canopyInterception.setname("interception");
            updatePools(rootingDepth, totalLAI);
            checkBudget();
        }


        public double GetPlantAvailWaterInSoil()
        {
            double retVal = theRooting.getvolume() + theSubZone.getvolume();
            return retVal;
            }

        //! Execute the daily functions
        /*!
        \param potEvap potential evapotranspiration (millimetres per day)
        \param precipIn precipitation (millimetres per day)
        \param Tmean mean air temperature (Celsius)
        */
        public void dailyRoutine(double potEvap, double precipIn, double irrigationWater, double Tmean, double totalLAI, double rootingDepth, 
            ref double rel_droughtPlant, ref double relativeDroughtSoil)
        {
            if (precipIn > 0)
                Console.Write("");
            evap = 0.0;
            //update pool status, since rooting depth may have changed
            checkBudget();
            drainage = 0;
            if ((totalLAI == 0) && (canopyInterception.getvolume() > 0))//this can occur if a crop has just been harvested and there was water in the canopy
            {
                canopyInterception.setmaxVolume(0);
                drainage = canopyInterception.update(0, 0, 0, 0);
                theEvaporation.update(drainage, 0, 0, 0);
                theRooting.setvolume(theRooting.getvolume() + drainage);
                drainage = 0;
                upperRooting.setvolume(0);
                upperRooting.setmaxVolume(0);
            }
            //checkBudget();
            drainage += updatePools(rootingDepth, totalLAI);
            //checkBudget();
            waterIn += precipIn;
            //handle snow
            snowEvap = potEvap;  //initially set snow evaporation to potential
            drainage += theSnow.dailySnow(ref potEvap, precipIn, Tmean);
            snowEvap -= potEvap;   //potential evap may have been reduced, due to snow evap. Subtract new pot evap from old value, to get snow evap.
            waterOut += snowEvap;

            if (irrigationWater>0)
            {
                waterIn += irrigationWater;
                drainage += irrigationWater;
            }
            double croppotEvap;
            double croppotEvapGreen;
            double soilpotEvap;
            //set interception pool to zero if no plants present

            double capillary = 0.0;
            double soilEvap = 0;
            double availWater = drainage;
            double attenuationCoeff = 0.6; //read from Globvars
            double propToSoil = Math.Exp(-attenuationCoeff * totalLAI);
            croppotEvap = potEvap * (1 - propToSoil);  //potential evapotranspiration from crop
            soilpotEvap = potEvap * propToSoil;      //potential evaporation from soil
                                                        //water available for evaporation from canopy includes any drainage from snow melt or irrigation
            availWater = canopyInterception.getvolume() + drainage;
            double maxCanopyEvap = canopyInterception.getmaxVolume();
            if (maxCanopyEvap > croppotEvap)
                maxCanopyEvap = croppotEvap;
            double canopyDrainage = 0;
            if (availWater <= maxCanopyEvap)   //canopy storage + precip etc is lost as evaporation from the canopy
            {
                canopyInterception.update(drainage, 0.0, availWater, 0.0);
                croppotEvap -= availWater;
                evap += availWater;
                waterOut += availWater;
                canopyDrainage = 0.0;
            }
            else //the canopy storage will overflow
            {
                canopyDrainage = canopyInterception.update(drainage, 0.0, maxCanopyEvap, 0.0);
                waterOut += maxCanopyEvap;
                evap += maxCanopyEvap;
                croppotEvap -= maxCanopyEvap;
            }

            if (totalLAI > 0.0)   //calculate potential transpiration from green leaf
                croppotEvapGreen = croppotEvap;
            else
                croppotEvapGreen = 0.0;

            //deal with soil surface layer
            double surfaceAvailWater = theEvaporation.getvolume() + canopyDrainage;   //water available for evaporation
            double evapFraction = theEvaporation.getmaxVolume() / theRooting.getmaxVolume();
            double propEvapToSoil = 0;
            if ((croppotEvapGreen + soilpotEvap)>0)
                propEvapToSoil = 1 - evapFraction * croppotEvapGreen / (croppotEvapGreen + soilpotEvap);
            if (propEvapToSoil*surfaceAvailWater > soilpotEvap)  //if there is enough water in the surface to enable evaporation at the potential rate
            {
                soilEvap = soilpotEvap;
                //evap += soilpotEvap;
            }
            else  //insufficient water available in surface to enable evaporation at the potential rate
            {
                capillary = 0.15 * (soilpotEvap - propEvapToSoil * surfaceAvailWater);   //calculate the evaporation supported by capillary flow
                double maxWaterAvail = theRooting.getvolume() + theSubZone.getvolume();
                double maxCapacity = theRooting.getmaxVolume() + theSubZone.getmaxVolume();
                if ((maxWaterAvail / maxCapacity) < breakpoint)
                    capillary *= (1/breakpoint) * maxWaterAvail / maxCapacity;
                if (capillary > (theRooting.getvolume() + theSubZone.getvolume()))
                    capillary = theRooting.getvolume() + theSubZone.getvolume();
                if (surfaceAvailWater > 0.0)  //there is water in the rooting pool
                    soilEvap = capillary + propEvapToSoil * surfaceAvailWater;
                else
                    soilEvap = capillary;
            }
            if (capillary > 0.0)  //if there has been capillary flow
            {
                theEvaporation.update(canopyDrainage, capillary, soilEvap, 0.0);    //update the evaporation pool
                if (soilEvap <= (theRooting.getvolume() + canopyDrainage))
                {
                    drainage = theRooting.update(canopyDrainage, 0.0, soilEvap, 0.0);
                    drainage = theSubZone.update(drainage, 0.0, 0.0, 0.0);
                }
                else
                {
                    double fromRooting = theRooting.getvolume();    //capillary flow provided from the rooting zone
                    double fromSubZone = soilEvap - (fromRooting + canopyDrainage); //capillary flow provided from below the rooting zone
                    if (fromSubZone > theSubZone.getvolume())
                    {
                        soilEvap -= (fromSubZone - theSubZone.getvolume());
                        fromSubZone = theSubZone.getvolume();
                    }
                    double temp = fromRooting + canopyDrainage + fromSubZone - soilEvap;
                    if (temp < 0)//THIS SHOULD NOT HAPPEN. WHY DOES IT?
                        soilEvap += temp;
                    drainage = theRooting.update(canopyDrainage, fromSubZone, soilEvap, 0.0);  //update rooting zone
                    drainage = theSubZone.update(drainage, 0, fromSubZone, 0.0);  //update sub rooting zone
                }
            }
            else   //update all the soil pools
            {
                theEvaporation.update(canopyDrainage, 0.0, soilEvap, 0.0);
                drainage = theRooting.update(canopyDrainage, 0.0, soilEvap, 0.0);
                drainage = theSubZone.update(drainage, 0.0, 0.0, 0.0);
            }
            evap += soilEvap;
            waterOut += soilEvap;
            waterOut += drainage;

            //now calculate transpiration
            rel_droughtPlant = 1.0;
            transpire = 0.0;
            //if (croppotEvapGreen == 0)
              //  Console.WriteLine("");
            if (totalLAI > 0)
            {
                if (theRooting.getvolume() >= breakpoint * theRooting.getmaxVolume())    //if the water available in the rooting zone is sufficient to allow transpiration to proceed at the maximum rate
                {
                    //zero the upper rooting pool
                    upperRooting.setvolume(0.0);
                    upperRooting.setmaxVolume(0.0);
                    if (croppotEvapGreen > theRooting.getvolume())
                        transpire = theRooting.getvolume();
                    else
                        transpire = croppotEvapGreen;
                    rel_droughtPlant = 0;
                }
                else  //calculate contents of the upper rooting pool
                {
                    upperRooting.setvolume(upperRooting.getvolume() + canopyDrainage - soilEvap); //the upper rooting volume contains new rainfall or irrigation water
                    if (upperRooting.getvolume() < 0.0)
                        upperRooting.setvolume(0.0);
                    upperRooting.setmaxVolume(Math.Min(theRooting.getmaxVolume(), upperRooting.getvolume()));
                    if (upperRooting.getvolume() >= croppotEvapGreen)     //if there is sufficient water in the upper rooting pool to enable transpiration to proceed at the central rate
                    {
                        transpire = croppotEvapGreen;
                        upperRooting.setvolume(upperRooting.getvolume() - transpire);
                        if (transpire>0)
                            rel_droughtPlant = 0;
                    }
                    else  //transpiration is reduced
                    {
                        transpire = (theRooting.getvolume() / (breakpoint * theRooting.getmaxVolume())) * croppotEvapGreen;
                        if (transpire > theRooting.getvolume())
                            transpire = theRooting.getvolume();
                        upperRooting.setvolume(0);
                        rel_droughtPlant = 1 - transpire / croppotEvapGreen;
                    }
                    if (upperRooting.getvolume() < theEvaporation.getvolume())//evaporation pool must be less than the upper rooting pool
                        theEvaporation.setvolume(upperRooting.getvolume());
                }
            }
            theRooting.setvolume(theRooting.getvolume() - transpire);     //update rooting pool
            theRooting.settranspiration(transpire);

            waterOut += transpire;
            if (theEvaporation.getvolume() < 0.000000001)  //set evaporation pool to 0 (limited precision means that calculations can lead to very small negative values)
                theEvaporation.setvolume(0.0);
            double soilDroughtFactor= GetPlantAvailWaterInSoil() / GetMaxPlantAvailableWater();
            double temp_rel_droughtPlant = 1 - theRooting.getvolume() / theRooting.getmaxVolume();
            if (temp_rel_droughtPlant < rel_droughtPlant)
                rel_droughtPlant = temp_rel_droughtPlant;
            if (soilDroughtFactor <= 0.5) //note - drought factor can be more than 1 if the soil is above field capacity after heavy rain over a long period
                relativeDroughtSoil = 1 - 2 * soilDroughtFactor;
            else
                relativeDroughtSoil = 0;
            if ((rel_droughtPlant > 0) && (drainage > 0))
                Console.Write("");
            if (transpire < 0)
                Console.Write("");
            if (double.IsNaN(evap))
            {
                Console.WriteLine("evap" + evap.ToString());
                throw new ArithmeticException();
            }
            checkBudget();    //check for water budget closure
        }
        //!Calculate the capacity of a soil water pool
        /*! 
        \param startDepth depth below the soil surface of the upper boundary of the pool (millimetres)
        \param endDepth depth below the soil surface of the lower boundary of the pool (millimetres)
        */
        double calcPoolVolume(double startDepth, double endDepth)
        {
            double ret_val = 0.0;
            int num = gettheLayers().Count;
            double z_upper = 0.0;
            for (int index = 0; index < num; index++)
            {
                layerClass alayerClass = gettheLayers()[index];
                double z_lower = alayerClass.getz_lower();
                if ((startDepth <= z_upper) && (endDepth >= z_lower))  //whole layer is included
                    ret_val += 1000.0 * (z_lower - z_upper) * (alayerClass.getfieldCapacity()-alayerClass.getPWP());
                if ((endDepth > z_upper) && (endDepth < z_lower))  //upper part of layer included only
                    ret_val += 1000.0 * (endDepth - z_upper) * (alayerClass.getfieldCapacity()-alayerClass.getPWP());
                if ((startDepth > z_upper) && (startDepth < z_lower))  //lower part of layer included only
                    ret_val += 1000.0 * (z_lower - startDepth) * (alayerClass.getfieldCapacity()-alayerClass.getPWP());
                z_upper = alayerClass.getz_lower();
            }
            return ret_val;
        }
        //!Adjust the variables and parameters of the soil pool in response to a tillage operation
        /*! 
        \param depth depth to which the soil is tilled (millimetres)
        */
        void tillage(double depth)
        {
            GlobalVars.Instance.Error("Check the tillage function before using");
            //checkBudget();
            layerClass alayerClass = gettheLayers()[0];
            double z_lower = alayerClass.getz_lower();
            double depthRootingPool = theRooting.getmaxVolume() / (alayerClass.getfieldCapacity()-alayerClass.getPWP());  //depth of rooting pool may be > rooting depth (if == to evaporation pool)
            double newMoistVal;
            if (depth > z_lower)
                GlobalVars.Instance.Error("tillage depth is greater than the depth of the uppermost soil layer");
            if (depth > depthRootingPool)  //then tillage will affect all of the rooting pool and some of the sub zone pool
            {
                z_lower = gettheLayers()[gettheLayers().Count].getz_lower();
                double depthSubZonePool = z_lower - depthRootingPool;
                double oldRootZoneVol = theRooting.getvolume();
                double oldRootZoneMaxVol = theRooting.getmaxVolume();
                double oldSubZoneVol = theSubZone.getvolume();
                double oldSubZoneMaxVol = theSubZone.getmaxVolume();
                //new moisture value for the rooting pool is calculated as the weighted average of the rooting and sub rooting pools
                double tempVol = oldRootZoneVol + (depth - depthRootingPool) * oldSubZoneVol / depthSubZonePool;
                double tempMaxVol = oldRootZoneMaxVol + (depth - depthRootingPool) * oldSubZoneMaxVol / depthSubZonePool;
                newMoistVal = tempVol / tempMaxVol;
                theRooting.setmaxVolume(theEvaporation.getmaxVolume());
                double newRootingVol = newMoistVal * theRooting.getmaxVolume();
                theRooting.setvolume(newRootingVol);
                double newEvapVol = newMoistVal * theEvaporation.getmaxVolume();
                theEvaporation.setvolume(newEvapVol);
                //now calculate new moisture value for the sub zone
                theSubZone.setmaxVolume(oldRootZoneMaxVol + oldSubZoneVol - theRooting.getmaxVolume());
                theSubZone.setvolume(oldSubZoneVol + oldRootZoneVol - theRooting.getvolume());
            }
            else  //there will be no change in the rooting pool but a change in the evaporation pool only
            {
                newMoistVal = theRooting.getvolume() / theRooting.getmaxVolume();
                double newEvapVol = newMoistVal * theEvaporation.getmaxVolume();
                theEvaporation.setvolume(newEvapVol);
            }
            //   checkBudget();
        }

        //! Update the soil pools to account for changes in the crop rooting depth
        public double updatePools(double rootingDepth, double totalLAI)
        {
            double canopyExcess = 0; //if the total LAI falls (e.g. to zero, when a crop has been harvested), any canopy storage must be reallocated
            // update pools
            double newrootCapacity;
            layerClass alayerClass;
            double poolDepth;
            alayerClass = gettheLayers()[0];
            double evapDepth = 10.0 / (1000.0 * (alayerClass.getfieldCapacity() - alayerClass.getPWP()));
            if (evapDepth > 0.1)
                evapDepth = 0.1;
            double newEvaporationMaxVol = calcPoolVolume(0, evapDepth);
            theEvaporation.setmaxVolume(newEvaporationMaxVol);
            if (rootingDepth > maxRootingDepth) //rooting depth cannot exceed the maximum rooting depth of the soil
                rootingDepth = maxRootingDepth;
            if (rootingDepth <= evapDepth)
                newrootCapacity = theEvaporation.getmaxVolume();
            else
                newrootCapacity = calcPoolVolume(0, rootingDepth); //recalculate the capacity of the rooting pool
            canopyInterception.setmaxVolume(0.5 * totalLAI); //update the capacity of the canopy interception pool
            canopyExcess = canopyInterception.update(0, 0, 0, 0);

            double oldrootCapacity = theRooting.getmaxVolume();
            if (newrootCapacity != oldrootCapacity)
            {
                theRooting.setmaxVolume(newrootCapacity);
                double oldrootVolume = theRooting.getvolume();
                double oldSubZonecapacity = theSubZone.getmaxVolume();
                double oldSubZonevolume = theSubZone.getvolume();
                double newSubZoneVol = 0;
                double newRootingVolume = 0;
                if (newrootCapacity < oldrootCapacity)   //if the rooting depth is decreasing
                {
                    newRootingVolume = oldrootVolume * newrootCapacity / oldrootCapacity;
                    if (newRootingVolume < theEvaporation.getvolume())
                        newRootingVolume = theEvaporation.getvolume();
                    theRooting.setvolume(newRootingVolume);
                    newSubZoneVol = oldSubZonevolume + (oldrootVolume - theRooting.getvolume());
                }
                else
                {
                    if ((oldSubZonecapacity > 0) && (newrootCapacity != oldrootCapacity))//the rooting pool will inherit water from the sub zone pool
                    {
                        double waterFromSubzone = oldSubZonevolume * (newrootCapacity - oldrootCapacity) / oldSubZonecapacity;
                        newRootingVolume = oldrootVolume + waterFromSubzone;
                        theRooting.setvolume(newRootingVolume);
                        //   waterIn += waterFromSubzone;
                        newSubZoneVol = oldSubZonevolume - waterFromSubzone;
                    }
                }

                poolDepth = rootingDepth;
                if (poolDepth < evapDepth)
                    poolDepth = evapDepth;
                else
                    poolDepth = evapDepth;
                alayerClass = gettheLayers()[gettheLayers().Count - 1];
                double soilDepth = alayerClass.getz_lower();
                //recalculate the rooting pool drainage constant
                double pooldrainageConst = drainageConst + (1 - drainageConst)
                                                     * (soilDepth - poolDepth) / soilDepth;
                theRooting.setdrainageConst(pooldrainageConst);
                //note initial value of poolDepth is depth of rooting pool
                poolDepth = soilDepth - poolDepth;
                //recalculate the sub zone drainage constant
                pooldrainageConst = drainageConst + (1 - drainageConst)
                                                     * poolDepth / soilDepth;
                theSubZone.setdrainageConst(pooldrainageConst);

                //recalculate volume and capacity of the sub zone
                if (newSubZoneVol < 0)
                {
                    waterOut += newSubZoneVol;
                    newSubZoneVol = 0;
                }
                theSubZone.setvolume(newSubZoneVol);
                double fieldCap = 0.0;
                double thickness;
                for (int index = 0; index < gettheLayers().Count; index++)
                {
                    layerClass anewlayerClass = gettheLayers()[index];
                    thickness = anewlayerClass.getthickness();
                    fieldCap += 1000.0 * thickness * (anewlayerClass.getfieldCapacity() - anewlayerClass.getPWP());
                }
                theSubZone.setmaxVolume(fieldCap - theRooting.getmaxVolume());
            }
            if (theRooting.getmaxVolume() < theEvaporation.getmaxVolume())
                Console.Write("");
            return canopyExcess;
        }
        //! Reports an error if the soil water budget cannot be closed
        public void checkBudget()
        {
            waterInSystem = getwaterInSystem();
            double error = waterInSystem + waterOut - waterIn;

            //    Console.WriteLine("waterInSystem" + waterInSystem.ToString() + " waterOut " + waterOut.ToString() + " waterIn " + waterIn.ToString());
            //Console.WriteLine(" error " + error.ToString());
            if ((Math.Abs(error) > 0.0001) || (Math.Abs(theRooting.getvolume()) < 0) || (Math.Abs(theEvaporation.getvolume()) < 0) || 
                (Math.Abs(theSnow.getvolume()) < 0) || (Math.Abs(theSubZone.getvolume()) < 0))
            {
                if (Math.Abs(error) > 0.0001)
                    GlobalVars.Instance.Error("Water balance error " + error.ToString());
                if (Math.Abs(theRooting.getvolume()) < 0)
                    GlobalVars.Instance.Error("Water balance error in rooting zone" + error.ToString());
                if (Math.Abs(theEvaporation.getvolume()) < 0)
                    GlobalVars.Instance.Error("Water balance error in the evaporation zone" + error.ToString());
                if (Math.Abs(theSnow.getvolume()) < 0)
                    GlobalVars.Instance.Error("Water balance error in snow reservoir" + error.ToString());
                if (Math.Abs(theSubZone.getvolume()) < 0)
                    GlobalVars.Instance.Error("Water balance error in the sub-rooting zone" + error.ToString());
            }
        }

        //!Outputs the soil variables to a file
        /*! 
        \param afile pointer to the output file
        \param header set to true if only the variable names should be printed
        */
        public void Write()
        {
            GlobalVars.Instance.writeStartTab("simpleSoil");
            theSnow.outputDetails();
            canopyInterception.outputDetails();
            theEvaporation.outputDetails();
            theRooting.outputDetails();
            theSubZone.outputDetails();
            upperRooting.outputDetails();
            belowPWP.outputDetails();
            GlobalVars.Instance.writeEndTab();
        }

        public void WriteDebugHeader()
        {
            GlobalVars.Instance.theZoneData.WriteToDebug("day ");
            theSnow.OutputDebugHeader(1);
            canopyInterception.OutputDebugHeader(1);
            theEvaporation.OutputDebugHeader(1);
            theRooting.OutputDebugHeader(1);
            theSubZone.OutputDebugHeader(1);
            upperRooting.OutputDebugHeader(1);
            GlobalVars.Instance.theZoneData.WriteLineToDebug("");
            GlobalVars.Instance.theZoneData.WriteToDebug(" ");
            theSnow.OutputDebugHeader(2);
            canopyInterception.OutputDebugHeader(2);
            theEvaporation.OutputDebugHeader(2);
            theRooting.OutputDebugHeader(2);
            theSubZone.OutputDebugHeader(2);
            upperRooting.OutputDebugHeader(2);
            belowPWP.OutputDebugHeader(2);
            GlobalVars.Instance.theZoneData.WriteLineToDebug("");
        }
        public void WriteDebug(int day)
        {
            GlobalVars.Instance.theZoneData.WriteToDebug(day + " ");
            theSnow.OutputDebug();
            canopyInterception.OutputDebug();
            theEvaporation.OutputDebug();
            theRooting.OutputDebug();
            theSubZone.OutputDebug();
            upperRooting.OutputDebug();
            belowPWP.OutputDebug();
            GlobalVars.Instance.theZoneData.WriteLineToDebug("");
        }
        //!Returns the soil moisture deficit to a given depth (mm)
        /*! 
        \param depth depth below the soil surface for which the soil moisture deficit should be calculated
        */
        public double getSMD(double depth, double rootingDepth)
        {
            double ret_val = 0.0;
            layerClass alayerClass = gettheLayers()[0];
            double evapDepth = 10.0 / (1000.0 * alayerClass.getfieldCapacity());   //evaporation pool has always a capacity of10 millimetres
            if (evapDepth > 0.1)
                evapDepth = 0.1;
            if (depth <= evapDepth)   //retrieve the deficit in the evaporation pool
                ret_val = theEvaporation.getDeficit();
            else
            {
                if (rootingDepth > depth)   //only a rooting pool is involved
                    ret_val = theRooting.getDeficit() * depth/rootingDepth;
                else      //both the rooting and the sub zone pools are involved
                    ret_val = (rootingDepth * theRooting.getDeficit() + (depth - rootingDepth) * theSubZone.getDeficit()) / depth;
            }
            return ret_val;
        }

        //!Returns the field capacity to a given depth (mm)
        /*! 
        \param depth depth below the soil surface for which the field capacity should be calculated
        */
        public double GetMaxAvailWaterToRootingDepth(double depth, double rootingDepth)
        {
            double ret_val = 0.0;
            layerClass alayerClass = gettheLayers()[0];
            double evapDepth = 10.0 / (1000.0 * (alayerClass.getfieldCapacity()-alayerClass.getPWP()));   //evaporation pool has always a capacity of10 millimetres
            if (evapDepth > 0.1)
                evapDepth = 0.1;
            if (depth <= evapDepth)   //retrieve the deficit in the evaporation pool
                ret_val = theEvaporation.getmaxVolume();
            else
            {
                if (rootingDepth > depth)   //only a rooting pool is involved
                    ret_val = theRooting.getmaxVolume() * depth / rootingDepth;
                else      //both the rooting and the sub zone pools are involved
                    ret_val = (rootingDepth * theRooting.getmaxVolume() + (depth - rootingDepth) * theSubZone.getmaxVolume()) / depth;
            }
            return ret_val;
        }

        //! Return the depth of the soil
        double getSoilDepth()
        {
            layerClass alayerClass = gettheLayers()[(gettheLayers().Count)];
            return alayerClass.getz_lower();
        }
        public double getwaterInSystem() 
        {
            waterInSystem = 0.0;
            waterInSystem += theSnow.getvolume();
            waterInSystem += canopyInterception.getvolume();
            waterInSystem += theRooting.getvolume();
            waterInSystem += theSubZone.getvolume();
            waterInSystem += belowPWP.getvolume();

      //      Console.WriteLine("theRooting" + theRooting.getvolume().ToString() + "theSubZone" + theSubZone.getvolume().ToString() + "belowPWP" + belowPWP.getvolume().ToString() + "\t");
            return waterInSystem;
        }
        //returns the water content (mm/mm) at field capacity
        //sand and clay are proportions of sand and clay by weight, OM is organic matter percentage by weight
        public double GetFieldCapacity(double sand, double clay, double OM)
        {
            double retVal = 0;
            retVal = -0.251 * sand + 0.195 * clay + 0.011 * OM + 0.006 * sand * OM - 0.027 * clay * OM + 0.452 * sand * clay + 0.299;
            retVal += 1.283 * retVal * retVal - 0.374 * retVal - 0.015;
            return retVal;
        }
        //returns the water content (mm/mm) at permanent wilting point
        //sand and clay are proportions of sand and clay by weight, OM is organic matter percentage by weight
        public double GetPermanentWilting(double sand, double clay, double OM)
        {
            double retVal = 0;
            retVal = -0.024 * sand + 0.487 * clay + 0.006 * OM + 0.005 * sand * OM - 0.013 * clay * OM + 0.068 * sand * clay + 0.031;
            retVal += 0.14 * retVal - 0.02;
            return retVal;
        }

        public double GetMaxPlantAvailableWater()
        {
            double ret_val = 0.0;
            for (int index = 0; index < gettheLayers().Count; index++)
            {
                layerClass alayerClass = gettheLayers()[index];
                ret_val += 1000.0 * alayerClass.GetPlantAvailableWater();
            }
            return ret_val;

        }
        public void CalcSoilWaterProps(double totalSoilC)
        {
            soilFieldCapacity = 0;
            percentOrgMatter=100 * (totalSoilC/(10000 * 0.46))/(1000 * theLayers[theLayers.Count-1].getz_lower());
            double PWP = 100 * GetPermanentWilting(Sandfraction, Clayfraction, percentOrgMatter);//100 * theta_f
            double fieldCapacity = 100 * GetFieldCapacity(Sandfraction, Clayfraction, percentOrgMatter);//100 * theta_f
            for (int i = 0; i < theLayers.Count; i++)
            {
                layerClass alayerClass = theLayers[i];
                alayerClass.setfieldCapacity(fieldCapacity/100);
                alayerClass.setPWP(PWP/100);
                soilFieldCapacity += (alayerClass.getfieldCapacity() - alayerClass.getPWP()) * alayerClass.getthickness();
            }
        }
    }
}
