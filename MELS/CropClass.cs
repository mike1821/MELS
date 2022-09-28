using System;
using System.Collections.Generic;
using System.Xml;
/*! A class that named CropClass */
public class CropClass
{
    
    public  List<manure> theManureApplied;
    public string Parens;
    //! An struct. Contains all the information on each application of manure or fertiliser
    /*! More detailed struct description. */
    public struct fertRecord //Contains all the information on each application of manure or fertiliser
    {
        public string Name; /*!< Name of the manure or fertiliser. */
        public string Parens; /*!< Parens string. */
        public double Namount; /*!< Total amount of N applied. */
        public double Camount; /*!< total amount of C applied. */
        public int Month_applied; /*!<Month of the year when the application is made. */
        public int dayOfApplication; /*!<Day during the duration of the crop that the application is made. */
        public timeClass applicdate; /*!<calendar date when the application is made. */
        public string Applic_techniqueS; /*!<Name of the manure application technique. */
        public int Applic_techniqueI;  /*!<Identification number of the manure application technique. */
        public int ManureStorageID; /*!< Identification number of the manure storage from which the manure originates. */
        public int speciesGroup;  /*!< Species of livestock producing the manure. */
        public string Units;  /*!< Units used to. */

        
        //! A normal member, Check to see if fertiliser or manure is applied outside the crop period.Taking three arguments and returning an bool value.
        /*!
          \param cropInformation, a class argument that points to FileInformation.
          \param startTime, a class argument that points to timeClass.
          \param endTime, a class argument that points to timeClass.
          \return a bool value
          
        */
       
        public bool ReadFertManApplication(FileInformation cropInformation, timeClass startTime, timeClass endTime)
        {
            bool aVal = false;
            Name = cropInformation.getItemString("Name");
            Units = cropInformation.getItemString("Unit");
            Namount = cropInformation.getItemDouble("Value");
            int applicationYear = startTime.GetYear();
            int month_applied = cropInformation.getItemInt("Month_applied");
            //only have month of application, so need to set a sensible day in month
            if (month_applied < startTime.GetMonth())
                applicationYear++;
            if (month_applied == startTime.GetMonth())
                SetapplicDate(startTime.GetDay(), month_applied, applicationYear); //earliest possible day
            else if (month_applied == endTime.GetMonth())
            {
                int applicationDay = Math.Max(15, endTime.GetDay());
                SetapplicDate(applicationDay, month_applied, applicationYear); //last possible day
            }
            else
                SetapplicDate(15, month_applied, applicationYear);//some day in the middle of the month
            if ((GetDate().getLongTime() < startTime.getLongTime()) || (GetDate().getLongTime() > endTime.getLongTime()))
                aVal = false;
            else
                aVal = true;
            return aVal;
        }
        //! A normal member, to write the Parens.Taking one argument.
        /*!
          \param theParens, a string argument that points to the Parens.
         
        */
        public void Write(string theParens)
        {
            Parens = theParens;
            GlobalVars.Instance.writeStartTab("fertRecord");

            GlobalVars.Instance.writeInformationToFiles("Namount", "Total N applied", "kg/ha", Namount, Parens);
            GlobalVars.Instance.writeInformationToFiles("Applic_techniqueS", "Application technique", "-", Applic_techniqueS, Parens);
            GlobalVars.Instance.writeInformationToFiles("Applic_techniqueI", "Application technique ID", "-", Applic_techniqueI, Parens);
            GlobalVars.Instance.writeInformationToFiles("ManureType", "Manure category", "-", ManureStorageID, Parens);
            GlobalVars.Instance.writeInformationToFiles("speciesGroup", "Species category", "-", speciesGroup, Parens);
            GlobalVars.Instance.writeInformationToFiles("Month_applied", "Month applied", "month", Month_applied, Parens);
            GlobalVars.Instance.writeInformationToFiles("dayOfApplication", "Day applied", "Day in month", dayOfApplication, Parens);

            GlobalVars.Instance.writeEndTab();

        }
        //! A normal member, to write the Xls.
        /*!
         
        */
        public void WriteXls()
        {
            GlobalVars.Instance.WriteCropFile("Name", "-", Name, false, false);
            GlobalVars.Instance.WriteCropFile("Day applied", "Day", applicdate.GetDay(), true, false);
            GlobalVars.Instance.WriteCropFile("Month_applied", "Month", applicdate.GetMonth(), true, false);
            GlobalVars.Instance.WriteCropFile("N applied", "kgN/ha", Namount, true, false);
            GlobalVars.Instance.WriteCropFile("Applic_technique", "-", Applic_techniqueS, false, false);
        }

        //! A normal member, Copy constructor.Taking one argument and returning an bool value.
        /*!
          \param theCropToCopied, a constructor argument that points to fertRecord.
          
        */    
        public fertRecord(fertRecord theCropToBeCopied)
        {
            Name = theCropToBeCopied.Name;
            Parens = theCropToBeCopied.Parens;
            Namount = theCropToBeCopied.Namount;
            Camount = theCropToBeCopied.Camount;
            Month_applied = theCropToBeCopied.Month_applied;
            dayOfApplication = theCropToBeCopied.dayOfApplication;
            if (theCropToBeCopied.applicdate != null)
                applicdate = new timeClass(theCropToBeCopied.applicdate);
            else
                applicdate = null;
            Applic_techniqueS = theCropToBeCopied.Applic_techniqueS;
            Applic_techniqueI = theCropToBeCopied.Applic_techniqueI;
            ManureStorageID = theCropToBeCopied.ManureStorageID;
            speciesGroup = theCropToBeCopied.speciesGroup;
            Units = theCropToBeCopied.Units;
        }
        //! A normal member, Set Parens. Taking one argument.
        /*!
          \param aParen, a string argument that points to Parens.    
        */
        public void setParens(string aParen) { Parens = aParen; }
        //! A normal member, Get N amount.Returning a double value.
        /*!
          \return a double value forNamount.        
        */
        public double getNamount() { return Namount; }
        //! A normal member, Get C amount.Returning a double value.
        /*!
          \return a double value for Camount.     
        */
        public double getCamount() { return Camount; }
        //! A normal member, Get Name.Returning a string value.
        /*!
          \return a string value for Name.       
        */
        public string getName() { return Name; }
        //! A normal member, Get speiciesGroup.Returning an integer value.
        /*!
          \return an integer value for speciesGroup.       
        */
        public int getspeciesGroup() { return speciesGroup; }
        //! A normal member, Get ManureType.Returning an integer value.
        /*!
          \return an integer value for ManureStroageID.       
        */
        public int getManureType() { return ManureStorageID; }
        //! A normal member, Get Month_applied.Returning an integer value.
        /*!
          \return an integer value for Month_applied.       
        */
        public int GetMonth_applied() { return Month_applied; }
        //! A normal member, Set dayOfApplication. Taking one argument.
        /*!
          \param aDay, an integer argument that points to dayOfApplication.    
        */
        public void SetdayOfApplication(int aDay) { dayOfApplication = aDay; }
        //! A normal member, Get dayOfApplication.Returning an integer value.
        /*!
          \return an integer value for dayOfApplication.       
        */
        public int GetdayOfApplication() { return dayOfApplication; }
        //! A normal member, Set Namount. Taking one argument.
        /*!
          \param aVal, a double argument that points to Namount.    
        */
        public void SetNamount(double aVal) { Namount = aVal; }
        //! A normal member, Set applicDate. Taking three integer arguments.
        /*!
          \param day, an integer argument that points to day.
          \param month, an integer argument that points to month. 
          \param year, an integer argument that points to year. 
        */
        public void SetapplicDate(int day, int month, int year) 
        {
            applicdate.setDate(day, month, year);
        }
        //! A normal member, Get Date.Returning a timeClass value.
        /*!
          \return a timeClass value for Date.       
        */
        public timeClass GetDate() { return applicdate; }
        //! A normal member, Get RelativeDay. Taking one argument and Returning a long value.
        /*!
         * \param startDay, a long argument that points to timeClass startDate.
          \return a long value for startDay.       
        */
        public long GetRelativeDay(long startDay)//timeClass startDate) 
        {
            long retVal = applicdate.getLongTime() - startDay;// startDate.getLongTime();
      
            return retVal;
        }
    }
    //! A normal member, Copy CrosClass constructor.Taking one argument and returning an bool value.
    /*!
      \param theCropToBeCopied, a class argument that points to CropClass.
      \return a bool value.
    */
    
    public CropClass(CropClass theCropToBeCopied)
    {
        NCrop = theCropToBeCopied.NCrop;
        name = theCropToBeCopied.name;
        identity = theCropToBeCopied.identity;
        cropSequenceNo = theCropToBeCopied.cropSequenceNo;
        area = theCropToBeCopied.area;
        theStartDate = new timeClass(theCropToBeCopied.theStartDate);
       
        theEndDate = new timeClass(theCropToBeCopied.theEndDate);
        if (theCropToBeCopied.residueToNext!=null)
            residueToNext = new GlobalVars.product(theCropToBeCopied.residueToNext);
        isIrrigated = theCropToBeCopied.isIrrigated;
        fertiliserApplied=new List<fertRecord>();
        for (int i = 0; i < theCropToBeCopied.fertiliserApplied.Count; i++)
            fertiliserApplied.Add(new fertRecord( theCropToBeCopied.fertiliserApplied[i]));
        manureApplied=new List<fertRecord>();
        for (int i = 0; i < theCropToBeCopied.manureApplied.Count; i++)
            manureApplied.Add(new fertRecord(theCropToBeCopied.manureApplied[i]));
        propAboveGroundResidues[0] = theCropToBeCopied.propAboveGroundResidues[0];
        propAboveGroundResidues[1] = theCropToBeCopied.propAboveGroundResidues[1];
        propBelowGroundResidues = theCropToBeCopied.propBelowGroundResidues;
        CconcBelowGroundResidues = theCropToBeCopied.CconcBelowGroundResidues;
        CtoNBelowGroundResidues = theCropToBeCopied.CtoNBelowGroundResidues;
        NDepositionRate = theCropToBeCopied.NDepositionRate;
        urineNH3EmissionFactor = theCropToBeCopied.urineNH3EmissionFactor;
        manureN2OEmissionFactor = theCropToBeCopied.manureN2OEmissionFactor;
        fertiliserN2OEmissionFactor = theCropToBeCopied.fertiliserN2OEmissionFactor;
        soilN2OEmissionFactor = theCropToBeCopied.soilN2OEmissionFactor;
        soilN2Factor = theCropToBeCopied.soilN2Factor;
        harvestMethod = theCropToBeCopied.harvestMethod;
        MaximumRootingDepth = theCropToBeCopied.MaximumRootingDepth;
        NfixationFactor = theCropToBeCopied.NfixationFactor;
        duration = theCropToBeCopied.duration;
        permanent = theCropToBeCopied.permanent;

        theProducts = new List<GlobalVars.product>();
        for (int i = 0; i < theCropToBeCopied.theProducts.Count; i++)
        {
            GlobalVars.product aProduct=new  GlobalVars.product(theCropToBeCopied.theProducts[i]);
            theProducts.Add(aProduct);
        }
        CFixed = theCropToBeCopied.CFixed;
        Nfixed = theCropToBeCopied.Nfixed;
        nAtm = theCropToBeCopied.nAtm;
        manureNH3emission = theCropToBeCopied.manureNH3emission;
        fertiliserNH3emission = theCropToBeCopied.fertiliserNH3emission;
        urineNH3emission = theCropToBeCopied.urineNH3emission;
        surfaceResidueC = theCropToBeCopied.surfaceResidueC;
        subsurfaceResidueC = theCropToBeCopied.subsurfaceResidueC;
        surfaceResidueN = theCropToBeCopied.surfaceResidueN;
        subsurfaceResidueN = theCropToBeCopied.subsurfaceResidueN;
        manureFOMCsurface = new double[theCropToBeCopied.manureFOMCsurface.Length];
        for (int i = 0; i < theCropToBeCopied.manureFOMCsurface.Length; i++)
        {
            manureFOMCsurface[i] = theCropToBeCopied.manureFOMCsurface[i];
        }
        manureHUMCsurface = new double[theCropToBeCopied.manureHUMCsurface.Length];
        for (int i = 0; i < theCropToBeCopied.manureHUMCsurface.Length; i++)
        {
            manureHUMCsurface[i] = theCropToBeCopied.manureHUMCsurface[i];
        }
        manureBiocharCsurface = new double[theCropToBeCopied.manureBiocharCsurface.Length];
        for (int i = 0; i < theCropToBeCopied.manureBiocharCsurface.Length; i++)
        {
            manureBiocharCsurface[i] = theCropToBeCopied.manureBiocharCsurface[i];
        }
        manureFOMCsubsurface = new double[theCropToBeCopied.manureFOMCsubsurface.Length];
        for (int i = 0; i < theCropToBeCopied.manureFOMCsubsurface.Length; i++)
        {
            manureFOMCsubsurface[i] = theCropToBeCopied.manureFOMCsubsurface[i];
        }
        manureHUMCsubsurface = new double[theCropToBeCopied.manureHUMCsubsurface.Length];
        for (int i = 0; i < theCropToBeCopied.manureHUMCsubsurface.Length; i++)
        {
            manureHUMCsubsurface[i] = theCropToBeCopied.manureHUMCsubsurface[i];
        }
        manureBiocharCsubsurface = new double[theCropToBeCopied.manureBiocharCsubsurface.Length];
        for (int i = 0; i < theCropToBeCopied.manureBiocharCsubsurface.Length; i++)
        {
            manureBiocharCsubsurface[i] = theCropToBeCopied.manureBiocharCsubsurface[i];
        }
        manureFOMNsurface = new double[theCropToBeCopied.manureFOMNsurface.Length];
        for (int i = 0; i < theCropToBeCopied.manureFOMNsurface.Length; i++)
        {
            manureFOMNsurface[i] = theCropToBeCopied.manureFOMNsurface[i];
        }
        manureHUMNsurface = new double[theCropToBeCopied.manureHUMNsurface.Length];
        for (int i = 0; i < theCropToBeCopied.manureHUMNsurface.Length; i++)
        {
            manureHUMNsurface[i] = theCropToBeCopied.manureHUMNsurface[i];
        }
        manureTAN = new double[theCropToBeCopied.manureTAN.Length];
        for (int i = 0; i < theCropToBeCopied.manureTAN.Length; i++)
        {
            manureTAN[i] = theCropToBeCopied.manureTAN[i];
        }
        fertiliserN = new double[theCropToBeCopied.fertiliserN.Length];
        for (int i = 0; i < theCropToBeCopied.fertiliserN.Length; i++)
            fertiliserN[i] = theCropToBeCopied.fertiliserN[i];
        nitrificationInhibitor = new double[theCropToBeCopied.nitrificationInhibitor.Length];
        for (int i = 0; i < theCropToBeCopied.nitrificationInhibitor.Length; i++)
            nitrificationInhibitor[i] = theCropToBeCopied.nitrificationInhibitor[i];
        droughtFactorPlant = new double[theCropToBeCopied.droughtFactorPlant.Length];
        for (int i = 0; i < theCropToBeCopied.droughtFactorPlant.Length; i++)
            droughtFactorPlant[i] = theCropToBeCopied.droughtFactorPlant[i];
        droughtFactorSoil = new double[theCropToBeCopied.droughtFactorSoil.Length];
        for (int i = 0; i < theCropToBeCopied.droughtFactorSoil.Length; i++)
            droughtFactorSoil[i] = theCropToBeCopied.droughtFactorSoil[i];
        dailyNitrateLeaching = new double[theCropToBeCopied.dailyNitrateLeaching.Length];
        for (int i = 0; i < theCropToBeCopied.dailyNitrateLeaching.Length; i++)
            dailyNitrateLeaching[i] = theCropToBeCopied.dailyNitrateLeaching[i];
        LAI = new double[theCropToBeCopied.LAI.Length];
        for (int i = 0; i < theCropToBeCopied.LAI.Length; i++)
            LAI[i] = theCropToBeCopied.LAI[i];
        drainage = new double[theCropToBeCopied.drainage.Length];
        for (int i = 0; i < theCropToBeCopied.drainage.Length; i++)
            drainage[i] = theCropToBeCopied.drainage[i];
        transpire = new double[theCropToBeCopied.transpire.Length];
        for (int i = 0; i < theCropToBeCopied.transpire.Length; i++)
            transpire[i] = theCropToBeCopied.transpire[i];
        dailyCanopyStorage = new double[theCropToBeCopied.dailyCanopyStorage.Length];
        for (int i = 0; i < theCropToBeCopied.dailyCanopyStorage.Length; i++)
            dailyCanopyStorage[i] = theCropToBeCopied.dailyCanopyStorage[i];
        evaporation = new double[theCropToBeCopied.evaporation.Length];
        for (int i = 0; i < theCropToBeCopied.evaporation.Length; i++)
            evaporation[i] = theCropToBeCopied.evaporation[i];
        irrigationWater = new double[theCropToBeCopied.irrigationWater.Length];
        for (int i = 0; i < theCropToBeCopied.irrigationWater.Length; i++)
            irrigationWater[i] = theCropToBeCopied.irrigationWater[i];
        soilWater = new double[theCropToBeCopied.soilWater.Length];
        for (int i = 0; i < theCropToBeCopied.soilWater.Length; i++)
            soilWater[i] = theCropToBeCopied.soilWater[i];
        plantavailableWater = new double[theCropToBeCopied.plantavailableWater.Length];
        for (int i = 0; i < theCropToBeCopied.plantavailableWater.Length; i++)
            plantavailableWater[i] = theCropToBeCopied.plantavailableWater[i];

  
        fertiliserC = theCropToBeCopied.fertiliserC;
        urineC = theCropToBeCopied.urineC;
        faecalC = theCropToBeCopied.faecalC;
        harvestedC = theCropToBeCopied.harvestedC;
        harvestedDM = theCropToBeCopied.harvestedDM;
        storageProcessingCLoss = theCropToBeCopied.storageProcessingCLoss;
        storageProcessingNLoss = theCropToBeCopied.storageProcessingNLoss;
        mineralNavailable = theCropToBeCopied.mineralNavailable;
        residueN = theCropToBeCopied.residueN;
        excretaNInput = theCropToBeCopied.excretaNInput;
        excretaCInput = theCropToBeCopied.excretaCInput;
        fertiliserNinput = theCropToBeCopied.fertiliserNinput;
        harvestedN = theCropToBeCopied.harvestedN;
        totalManureNApplied = theCropToBeCopied.totalManureNApplied;
        N2ONemission = theCropToBeCopied.N2ONemission;
        N2Nemission = theCropToBeCopied.N2Nemission;
        cropNuptake = theCropToBeCopied.cropNuptake;
        manureFOMn = theCropToBeCopied.manureFOMn;
        manureHUMn = theCropToBeCopied.manureHUMn;
        urineNasFertilizer = theCropToBeCopied.urineNasFertilizer;
        faecalN = theCropToBeCopied.faecalN;
        OrganicNLeached = theCropToBeCopied.OrganicNLeached;
        soilNMineralisation = theCropToBeCopied.soilNMineralisation;
        mineralNFromLastCrop = theCropToBeCopied.mineralNFromLastCrop;
        mineralNToNextCrop = theCropToBeCopied.mineralNToNextCrop;
        fertiliserN2OEmission = theCropToBeCopied.fertiliserN2OEmission;
        manureN2OEmission = theCropToBeCopied.manureN2OEmission;
        cropResidueN2O = theCropToBeCopied.cropResidueN2O ;
        soilN2OEmission = theCropToBeCopied.soilN2OEmission;
        proportionLeached = theCropToBeCopied.proportionLeached;
        mineralNreserve = theCropToBeCopied.mineralNreserve;
        nitrateLeaching = theCropToBeCopied.nitrateLeaching;
        cropSequenceName = theCropToBeCopied.cropSequenceName;
        droughtSusceptability = theCropToBeCopied.droughtSusceptability;
        totalTsum = theCropToBeCopied.totalTsum;
        maxLAI= theCropToBeCopied.maxLAI;
        irrigationThreshold = theCropToBeCopied.irrigationThreshold;
        irrigationMinimum = theCropToBeCopied.irrigationMinimum;
    }
    public double NCrop;
    string name;
    int identity;
    //! Function inputs
    /*!
      Details
     */
    double area;
    public timeClass theStartDate;
    timeClass theEndDate;
    bool isIrrigated;

    string cropSequenceName;
    int cropSequenceNo;
    public List<fertRecord> fertiliserApplied;
    public List<fertRecord> manureApplied;

    //! Function parameters
    /*!
      Details
     */
    double[] propAboveGroundResidues = new double[2];
    double propBelowGroundResidues;
    double CconcBelowGroundResidues;
    double CtoNBelowGroundResidues;
    double NDepositionRate;
    double urineNH3EmissionFactor;
    double manureN2OEmissionFactor;
    double fertiliserN2OEmissionFactor;
    double soilN2OEmissionFactor;
    double soilN2Factor;
    string harvestMethod;
    double MaximumRootingDepth;
    double NfixationFactor;
    double baseTemperature;
    long duration = 0;
    bool permanent = false;
    double droughtSusceptability = 1.0;
    bool zeroGasEmissionsDebugging = false;
    bool zeroLeachingDebugging = false;
    double irrigationThreshold = 0.5;
    double irrigationMinimum = 5.0;
    double[] potentialEvapoTrans = new double[366];
    double[] precipitation = new double[366];
    double[] temperature = new double[366];
    double[] Tsum = new double[366];
    double[] potEvapoTrans;
    double[] evaporation;
    double[] drainage;
    double[] transpire;
    double[] soilWater;
    double[] plantavailableWater;
    double[] LAI;
    double[] irrigationWater;
    double[] droughtFactorPlant;
    double[] droughtFactorSoil;
    double[] dailyNitrateLeaching;
    double[] dailyCanopyStorage;
    List<GlobalVars.product> theProducts = new List<GlobalVars.product>();

    //other variables

    //! Function ariables to output 
    /*!
      Details
     */
    double CFixed = 0;//!carbon fixed
    double surfaceResidueC;
    double subsurfaceResidueC;
    double urineC = 0;
    double faecalC = 0;
    double grazedC = 0;
    double grazingCH4C = 0;
    double harvestedC = 0;
    double harvestedDM = 0;
    double storageProcessingCLoss = 0;
    double storageProcessingNLoss = 0;
    double storageProcessingDMLoss = 0;

    double NyieldMax = 0;
    double maxCropNuptake = 0;
    double modelledCropNuptake = 0;
    double NavailFact = 0;
    double Nfixed = 0;
    double nAtm = 0;
    double manureNH3emission = 0;
    double fertiliserNH3emission = 0;
    double urineNH3emission = 0;
    double surfaceResidueN;
    double subsurfaceResidueN;
    double surfaceResidueDM = 0;
    double fertiliserC = 0;
    double mineralNavailable = 0;
    double residueN = 0;
    double excretaNInput = 0;
    double excretaCInput = 0;
    double fertiliserNinput = 0;
    double harvestedN = 0;
    double grazedN = 0;
    double totalManureNApplied = 0;
    double N2ONemission = 0;
    double N2Nemission = 0;
    double cropNuptake = 0;
    double manureFOMn = 0;
    double manureHUMn = 0;
    double urineNasFertilizer = 0;
    double faecalN = 0;
    double burntResidueC = 0;
    double burntResidueN = 0;
    double burningN2ON = 0;
    double burningNH3N = 0;
    double burningNOxN = 0;
    double burningCOC = 0;
    double burningCO2C = 0;
    double burningBlackC = 0;
    double burningOtherN = 0;
    double OrganicNLeached = 0;
    double soilNMineralisation = 0;
    double mineralNFromLastCrop = 0;
    double mineralNToNextCrop = 0;
    double residueCfromLastCrop = 0;
    double residueNfromLastCrop = 0;
    double residueCtoNextCrop = 0;
    double residueNtoNextCrop = 0;
    double fertiliserN2OEmission = 0;
    double manureN2OEmission;
    double cropResidueN2O = 0;
    double soilN2OEmission = 0;
    double proportionLeached = 0;
    double mineralNreserve = 0;
    double nitrateLeaching = 0;
    double lengthOfSequence = 0;
    double previousPhaseCropNuptake = 0;
    double unutilisedGrazableDM = 0;
    double unutilisedGrazableC = 0;
    double unutilisedGrazableN = 0;
    double totalTsum = 0;
    double maxLAI = 5.0;

    //! Function other variables not for output
    /*!
      Details
     */
    public double [] manureFOMCsurface;
    double [] manureHUMCsurface;
    double[] manureBiocharCsurface;
    double[] manureFOMCsubsurface;
    double[] manureHUMCsubsurface;
    double[] manureBiocharCsubsurface;
    double[] manureFOMNsurface;
    double[] manureHUMNsurface;
    double[] manureTAN;
    double[] fertiliserN;
    //! nitrification inhibitor - 1 at time of application
    double[] nitrificationInhibitor;

    GlobalVars.product residueFromPrevious;
    GlobalVars.product residueToNext;
    //! A normal member, Get evaporation. Taking one integer argument and returning a double value.
    /*!
      \param index, an integer argument that points to index.
      \return a double value for evaporation.
    */
    public double Getevaporation(int index) { return evaporation[index]; }
    //! A normal member, Get transpire. Taking one integer argument and returning a double value.
    /*!
      \param index, an integer argument that points to index.
      \return a double value for evaporation.
    */
    public double Gettranspire(int index) { return transpire[index]; }
    //! A normal member, Get IrrigationWater. Taking one integer argument and returning a double value.
    /*!
      \param index, an integer argument that points to index.
      \return a double value for irrigationWater.
    */
    public double GetIrrigationWater(int index) { return irrigationWater[index]; }
    //! A normal member, Get irrigationThreshold. Returning a double value.
    /*!
      \return a double value for irrigationThreshold.
    */
    public double GetirrigationThreshold() { return irrigationThreshold; }
    //! A normal member, Get irrigationMinimum. Returning a double value.
    /*!
      \return a double value for irrigationMinimum.
    */
    public double GetirrigationMinimum() { return irrigationMinimum; }
    //! A normal member, Set soilWater. Taking two arguments.
    /*!
      \param index, an integer argument that points to index.
      \param val, a double value for val, soilWater.
    */
    public void SetsoilWater(int index, double val) { soilWater[index] = val; }
    //! A normal member, Get soilWater. Taking one integer argument and returning a double value.
    /*!
      \param index, an integer argument that points to index.
      \return a double value for soilWater.
    */
    public double GetsoilWater(int index) { return soilWater[index]; }
    //! A normal member, Get plantavailableWater. Taking one integer argument and returning a double value.
    /*!
      \param index, an integer argument that points to index.
      \return a double value for plantavailableWater.
    */
    public double GetplantavailableWater(int index) { return plantavailableWater[index]; }
    //! A normal member, Set plantavailableWater. Taking two arguments.
    /*!
      \param index, an integer argument that points to index.
      \param val, a double value for plantavailableWater.
    */
    public void SetplantavailableWater(int index, double val) { plantavailableWater[index]=val; }
    //! A normal member, Set droughtFactorPlant. Taking two arguments.
    /*!
      \param index, an integer argument that points to index.
      \param val, a double value for droughtFactorPlant.
    */
    public void SetdroughtFactorPlant(int index, double val) { droughtFactorPlant[index] = val; }
    //! A normal member, Get droughtFactorPlant. Taking one argument and returing a double value.
    /*!
      \param index, an integer argument that points to index.
      \return, a double value for droughtFactorPlant.
    */
    public double GetdroughtFactorPlant(int index) { return droughtFactorPlant[index]; }
    //! A normal member, Get droughtFactorSoil. Taking one argument and returing a double value.
    /*!
      \param index, an integer argument that points to index.
      \return, a double value for droughtFactorSoil.
    */
    public double GetdroughtFactorSoil(int index) { return droughtFactorSoil[index]; }
    //! A normal member, Set droughtFactorSoil. Taking two arguments.
    /*!
      \param index, an integer argument that points to index.
      \param val, a double value for droughtFactorSoil.
    */
    public void SetdroughtFactorSoil(int index, double val) { droughtFactorSoil[index] = val; }
    //! A normal member, Get dailyNitrateLeaching. Taking one argument and returing a double value.
    /*!
      \param index, an integer argument that points to index.
      \return, a double value for dailyNitrateLeaching.
    */
    public double GetdailyNitrateLeaching(int index) { return dailyNitrateLeaching[index]; }
    //! A normal member, Get dailyCanopyStorage. Taking one argument and returing a double value.
    /*!
      \param index, an integer argument that points to index.
      \return, a double value for dailyCanopyStorage.
    */
    public double getdailyCanopyStorage(int index) { return dailyCanopyStorage[index]; }
    //! A normal member, Get drainage. Taking one argument and returing a double value.
    /*!
      \param index, an integer argument that points to index.
      \return, a double value for drainage.
    */
    public double Getdrainage(int index) { return drainage[index]; }
    //! A normal member, Set name. Taking one argument.
    /*!
      \param aname, a string argument that points to name.
    */
    public void Setname(string aname) { name = aname; }
    //! A normal member, Set identity. Taking one argument.
    /*!
      \param aValue, an integer argument that points to identity.
    */
    public void Setidentity(int aValue) { identity = aValue; }
    //! A normal member, Set area. Taking one argument.
    /*!
      \param aVal, a double argument that points to area.
    */
    public void setArea(double aVal) { area = aVal; }
    //! A normal member, Set soilN2Factor. Taking one argument.
    /*!
      \param aVal, a double argument that points to soilN2Factor.
    */
    public void setsoilN2Factor(double aVal) { soilN2Factor = aVal; }
    //! A normal member, Set duration. Taking one argument.
    /*!
      \param aVal, a long argument that points to duration.
    */
    public void Setduration(long aVal) { duration = aVal; }
    //! A normal member, Set cropSequenceName. Taking one argument.
    /*!
      \param aVal, a string argument that points to cropSequenceName.
    */
    public void SetcropSequenceName(string aVal) { cropSequenceName = aVal; }
    //! A normal member, Set cropSequenceNo. Taking one argument.
    /*!
      \param aVal, an integer argument that points to cropSequenceNo.
    */
    public void SetcropSequenceNo(int aVal) { cropSequenceNo = aVal; }
    //! A normal member, Set previous Phase CropNuptake. Taking one argument.
    /*!
      \param aVal, an double argument.
    */
    public void SetpreviousPhaseCropNuptake(double aVal) { previousPhaseCropNuptake = aVal; }
    //! A normal member, Set nitrification In hibitor. Taking one argument.
    /*!
      \param aVal, a double argument.
    */
    public void SetnitrificationInhibitor(double aVal) { nitrificationInhibitor[0] = aVal; }
    //! A normal member, Get MineralN from lsstCrop. Returning one value.
    /*!
      \return a double value.
    */
    public double getMineralNFromLastCrop(){return mineralNFromLastCrop;}
    //! A normal member, Get Crop Residue N2O. Returning one value.
    /*!
      \return a double value.
    */
    public double getCropResidueN2O() { return cropResidueN2O; }
    //! A normal member, Get NFix. Returning one value.
    /*!
      \return a double value.
    */
    public double getNFix() { return Nfixed; }
    //! A normal member, Get nAtm. Returning one value.
    /*!
      \return a double value.
    */
    public double getnAtm() { return nAtm; }
    //! A normal member, Get name. Returning one value.
    /*!
      \return a string value.
    */
    public string Getname() { return name; }
    //! A normal member, Get Surface Residue DM. Returning one value.
    /*!
      \return a double value.
    */
    public double getSurfaceResidueDM(){return surfaceResidueDM;}
    //! A normal member, Get identity. Returning one value.
    /*!
      \return an integer value.
    */
    public int Getidentity() { return identity; }
    //! A normal member, Get Area. Returning one value.
    /*!
      \return a double value.
    */
    public double getArea() { return area; }
    //! A normal member, Set length of Sequence. Taking one argument.
    /*!
      \param aVal, an double argument.
    */
    public void SetlengthOfSequence(double aVal) { lengthOfSequence = aVal; }
    //! A normal member, Get Prop belowGround Residues. Returning one value.
    /*!
      \return a double value.
    */
    public double GetpropBelowGroundResidues() { return propBelowGroundResidues; }
    //! A normal member, Get Conc BelowGround Residues. Returning one value.
    /*!
      \return a double value.
    */
    public double GetCconcBelowGroundResidues() { return CconcBelowGroundResidues; }
    //! A normal member, Get Storage Processing Closs. Returning one value.
    /*!
      \return a double value.
    */
    public double GetstorageProcessingCLoss() { return storageProcessingCLoss; }
    //! A normal member, Get Storage Processing NLoss. Returning one value.
    /*!
      \return a double value.
    */
    public double GetstorageProcessingNLoss() { return storageProcessingNLoss; }
    //! A normal member, Get StorageProcessing DMLoss. Returning one value.
    /*!
      \return a double value.
    */
    public double GetstorageProcessingDMLoss() { return storageProcessingDMLoss; }
    //! A normal member, Get HavestedC. Returning one value.
    /*!
      \return a double value.
    */
    public double GetharvestedC() { return harvestedC; }
    //! A normal member, Get HavestedDM. Returning one value.
    /*!
      \return a double value.
    */
    public double GetharvestedDM() { return harvestedDM; }
    //! A normal member, Get GrazedC. Returning one value.
    /*!
      \return a double value.
    */
    public double GetgrazedC() { return grazedC; }
    //! A normal member, Get Surface Residue C. Returning one value.
    /*!
      \return a double value.
    */
    public double GetsurfaceResidueC() { return surfaceResidueC; }
    //! A normal member, Get Subsurface Residue C. Returning one value.
    /*!
      \return a double value.
    */
    public double GetsubsurfaceResidueC() { return subsurfaceResidueC; }
    //! A normal member, Get Surface Residue N. Returning one value.
    /*!
      \return a double value.
    */
    public double GetsurfaceResidueN() { return surfaceResidueN; }
    //! A normal member, Get Subsurface Residue N. Returning one value.
    /*!
      \return a double value.
    */
    public double GetsubsurfaceResidueN() { return subsurfaceResidueN; }
    //! A normal member, Get Urine NH3 Emission. Returning one value.
    /*!
      \return a double value.
    */
    public double GeturineNH3emission() { return urineNH3emission; }
    //! A normal member, Get Fertiliser C. Returning one value.
    /*!
      \return a double value.
    */
    public double GetFertiliserC() { return fertiliserC; }
    //! A normal member, Get Nitrification in Hibitor. Returning one value.
    /*!
      \return a double value.
    */
    public double GetnitrificationInhibitor()  { return nitrificationInhibitor[duration-1]; }
    //! A normal member, Get Residue C from LastCrop. Returning one value.
    /*!
      \return a double value.
    */
    public double GetResidueCfromLastCrop() { return residueCfromLastCrop; }
    //! A normal member, Get Residue N from LastCrop. Returning one value.
    /*!
      \return a double value.
    */
    public double GetResidueNfromLastCrop() { return residueNfromLastCrop; }
    //! A normal member, Get Residue C to NextCrop. Returning one value.
    /*!
      \return a double value.
    */
    public double GetResidueCtoNextCrop() { return residueCtoNextCrop; }
    //! A normal member, Get Residue N to NextCrop. Returning one value.
    /*!
      \return a double value.
    */
    public double GetResidueNtoNextCrop() { return residueNtoNextCrop; }
    //! A normal member, Get Unutilised Grazable C. Returning one value.
    /*!
      \return a double value.
    */
    public double GetUnutilisedGrazableC() { return unutilisedGrazableC; }
    //! A normal member, Get Unutilised Grazable DM. Returning one value.
    /*!
      \return a double value.
    */
    public double GetUnutilisedGrazableDM() { return unutilisedGrazableDM; }
    //! A normal member, Get Prop aboveGround Residues. Taking one arugment and  Returning one value.
    /*!
     \param index, an integer argument.
      \return a double value.
    */
    public double GetpropAboveGroundResidues(int index) { return propAboveGroundResidues[index]; }
    //! A normal member, Get Maxprop AboveGround Residues. Returning one value.
    /*!
      \return a double value.
    */
    public double GetMaxpropAboveGroundResidues() 
    {
        double retVal = propAboveGroundResidues[0];
        if (propAboveGroundResidues[1] > retVal)
            retVal = propAboveGroundResidues[1];

        return retVal; 
    }
    //! A normal member, Get Utilised DM. Returning one value.
    /*!
      \return a double value.
    */
    public double GetUtilisedDM()
    {
        double retVal = 0;
        for (int i = 0; i < theProducts.Count; i++)
        {
            if (theProducts[i].composition.GetisGrazed())
                retVal+=theProducts[i].GetGrazed_yield();
            else if (theProducts[i].Harvested=="Harvested")
                retVal += theProducts[i].GetModelled_yield();
        }
        return retVal;
    }
    //! A normal member, Get Manure C. Returning one value.
    /*!
      \return a double value.
    */
    public double GetManureC() 
    {
        double retVal = 0;
        int numDays = manureHUMCsurface.Length;
        for (int i = 0; i < numDays; i++)
            retVal += manureHUMCsurface[i] + manureFOMCsurface[i] + manureHUMCsubsurface[i] + manureFOMCsubsurface[i];
        return retVal;
    }
    //! A normal member, Get Urine N. Returning one value.
    /*!
      \return a double value.
    */
    public double GetUrineN() { return urineNasFertilizer; }
    //! A normal member, Get Urine C. Returning one value.
    /*!
      \return a double value.
    */
    public double GetUrineC() { return urineC; }
    //! A normal member, Get Grazing CH4C. Returning one value.
    /*!
      \return a double value.
    */
    public double GetgrazingCH4C() { return grazingCH4C; }
    //! A normal member, Get Faecal C. Returning one value.
    /*!
      \return a double value.
    */
    public double GetfaecalC() { return faecalC; }
    //! A normal member, Get Faecal N. Returning one value.
    /*!
      \return a double value.
    */
    public double GetfaecalN() { return faecalN; }
    //public double GetresidueN() { return residueN; }
    //! A normal member, Get Total Manure N Applied. Returning one value.
    /*!
      \return a double value.
    */
    public double GettotalManureNApplied() { return totalManureNApplied; }
    //! A normal member, Get Excreta N Input. Returning one value.
    /*!
      \return a double value.
    */
    public double GetexcretaNInput() { return excretaNInput; }
    //! A normal member, Get Excreta C Input. Returning one value.
    /*!
      \return a double value.
    */
    public double GetexcretaCInput() { return excretaCInput; }
    //! A normal member, Get Manure NH3N Emission. Returning one value.
    /*!
      \return a double value.
    */
    public double GetmanureNH3Nemission() { return manureNH3emission; }
    //! A normal member, Get N2ON Emission. Returning one value.
    /*!
      \return a double value.
    */
    public double GetN2ONemission() { return N2ONemission; }
    //! A normal member, Get N2N Emission. Returning one value.
    /*!
      \return a double value.
    */
    public double GetN2Nemission() { return N2Nemission; }
    //! A normal member, Get Fertiliser NH3N Emission. Returning one value.
    /*!
      \return a double value.
    */
    public double GetfertiliserNH3Nemission() { return fertiliserNH3emission; }
    //! A normal member, Get Mineral N available. Returning one value.
    /*!
      \return a double value.
    */
    public double GetmineralNavailable() { return mineralNavailable; }
    //! A normal member, Get Fertiliser N2ON Emission. Returning one value.
    /*!
      \return a double value.
    */
    public double GetfertiliserN2ONEmission() { return fertiliserN2OEmission; }
    //! A normal member, Get Manure N2ON Emission. Returning one value.
    /*!
      \return a double value.
    */
    public double GetmanureN2ONEmission() { return manureN2OEmission; }
    //! A normal member, Get Crop Residue N2ON. Returning one value.
    /*!
      \return a double value.
    */
    public double GetcropResidueN2ON() { return cropResidueN2O; }
    //! A normal member, Get Soil N2ON Emission. Returning one value.
    /*!
      \return a double value.
    */
    public double GetsoilN2ONEmission() { return soilN2OEmission; }
    //! A normal member, Get burning N2ON. Returning one value.
    /*!
      \return a double value.
    */
    public double GetburningN2ON() { return burningN2ON; }
    //! A normal member, Get burning NH3N. Returning one value.
    /*!
      \return a double value.
    */
    public double GetburningNH3N() { return burningNH3N; }
    //! A normal member, Get burning NOxN. Returning one value.
    /*!
      \return a double value.
    */
    public double GetburningNOxN() { return burningNOxN; }
    //! A normal member, Get burning Other N. Returning one value.
    /*!
      \return a double value.
    */
    public double GetburningOtherN() { return burningOtherN; }
    //! A normal member, Get burning COC. Returning one value.
    /*!
      \return a double value.
    */
    public double GetburningCOC() { return burningCOC; }
    //! A normal member, Get burning CO2C. Returning one value.
    /*!
      \return a double value.
    */
    public double GetburningCO2C() { return burningCO2C; }
    //! A normal member, Get burning Black C. Returning one value.
    /*!
      \return a double value.
    */
    public double GetburningBlackC() { return burningBlackC; }
    //! A normal member, Get burnt Residue C. Returning one value.
    /*!
      \return a double value.
    */
    public double GetburntResidueC() { return burntResidueC; }
    //! A normal member, Get burnt Residue N. Returning one value.
    /*!
      \return a double value.
    */
    public double GetburntResidueN() { return burntResidueN; }
    //! A normal member, Get Harvested N. Returning one value.
    /*!
      \return a double value.
    */
    public double GetharvestedN() { return harvestedN; }
    //! A normal member, Get Mineral N to NextCrop. Returning one value.
    /*!
      \return a double value.
    */
    public double GetmineralNToNextCrop() { return mineralNToNextCrop; }
    //public bool GetContinues() { return continues; }
    //! A normal member, Get Crop N uptake. Returning one value.
    /*!
      \return a double value.
    */
    public double GetCropNuptake() { return cropNuptake; }
    //! A normal member, Get the Products. Returning one value.
    /*!
      \return a list value that points to GlobalVar.product.
    */
    public List<GlobalVars.product> GettheProducts() { return theProducts; }
    //! A normal member, Get Fertiliser Applied. Returning one value.
    /*!
      \return a list value that points to fertRecord.
    */
    public List<fertRecord> GetfertiliserApplied() { return fertiliserApplied; }
    //! A normal member, Get Manure Applied. Returning one value.
    /*!
      \return a list value that points to fertRecord.
    */
    public List<fertRecord> GetmanureApplied() { return manureApplied; }
    //! A normal member, Get Start Day. Returning one value.
    /*!
      \return an integer value.
    */
    public int GetStartDay() { return theStartDate.GetDay(); }
    //! A normal member, Get End Day. Returning one value.
    /*!
      \return an integer value.
    */
    public int GetEndDay() { return theEndDate.GetDay(); }
    //! A normal member, Get Start Month. Returning one value.
    /*!
      \return an integer value.
    */
    public int GetStartMonth() { return theStartDate.GetMonth(); }
    //! A normal member, Get End Month. Returning one value.
    /*!
      \return an integer value.
    */
    public int GetEndMonth() { return theEndDate.GetMonth(); }
    //! A normal member, Get End Year. Returning one value.
    /*!
      \return an integer value.
    */
    public int GetEndYear() { return theEndDate.GetYear(); }
    //! A normal member, Get Start Year. Returning one value.
    /*!
      \return an integer value.
    */
    public int GetStartYear() { return theStartDate.GetYear(); }
    //! A normal member, Set Start Year. Taking one arugment.
    /*!
      \param aVal, an integer argument.
    */
    public void SetStartYear(int aVal) { theStartDate.SetYear(aVal); }
    //! A normal member, Set End Year. Taking one arugment.
    /*!
      \param aVal, an integer argument.
    */
    public void SetEndYear(int aVal) { theEndDate.SetYear(aVal); }
    //! A normal member, Get Permanent. Returning one value.
    /*!
      \return a boolean value.
    */
    public bool Getpermanent() {return permanent;}
    //! A normal member, Get Maximum Rooting Depth. Returning one value.
    /*!
      \return a double value.
    */
    public double GetMaximumRootingDepth() { return MaximumRootingDepth; }
    //! A normal member, Set Maximum Rooting Depth. Taking one argument.
    /*!
      \param aVal, a double argument.
    */
    public void SetMaximumRootingDepth(double aVal) { MaximumRootingDepth = aVal; }
    //! A normal member, Get Manure FOM C surface. Taking one arugment and  Returning one value.
    /*!
     \param day, an integer argument.
      \return a double value.
    */
    public double GetmanureFOMCsurface(int day) { return manureFOMCsurface[day]; }
    //! A normal member, Get Manure FOM N surface. Taking one arugment and  Returning one value.
    /*!
     \param day, an integer argument.
      \return a double value.
    */
    public double GetmanureFOMNsurface(int day) { return manureFOMNsurface[day]; }
    //! A normal member, Get Manure HUM C surface. Taking one arugment and  Returning one value.
    /*!
     \param day, an integer argument.
      \return a double value.
    */
    public double GetmanureHUMCsurface(int day) { return manureHUMCsurface[day]; }
    //! A normal member, Get Manure Biochar C surface. Taking one arugment and  Returning one value.
    /*!
     \param day, an integer argument.
      \return a double value.
    */
    public double GetmanureBiocharCsurface(int day) { return manureBiocharCsurface[day]; }
    //! A normal member, Get Duration. Returning one value.
    /*!
      \return a long value.
    */
    public long getDuration() { return duration; }
    //! A normal member, Get Organic N Leached. Returning one value.
    /*!
      \return a double value.
    */
    public double GetOrganicNLeached() {return OrganicNLeached; }
    //! A normal member, Get Soil N Mineralisation. Returning one value.
    /*!
      \return a double value.
    */
    public double GetSoilNMineralisation() { return soilNMineralisation; }
    //! A normal member, Get Proportion Leached. Returning one value.
    /*!
      \return a double value.
    */
    public double GetproportionLeached() { return proportionLeached; }
    //! A normal member, Get Mineral N Reserve. Returning one value.
    /*!
      \return a double value.
    */
    public double GetMineralNreserve() { return mineralNreserve; }
    //! A normal member, Set Nitrate Leaching. Taking one argument.
    /*!
      \param aVal, a double argument.
    */
    public void SetnitrateLeaching(double aVal){nitrateLeaching=aVal;}
    //! A normal member, Get Nitrate Leaching. Returning one value.
    /*!
      \return a double value.
    */
    public double GetnitrateLeaching() { return nitrateLeaching; }
    //! A normal member, Get Total Manure HUM N surface. Returning one value.
    /*!
      \return a double value.
    */
    public double GetTotalmanureHUMNsurface()
    {
        double retVal = 0;
        for (int i = 0; i < manureHUMNsurface.Length; i++)
            retVal += manureHUMNsurface[i];
        return retVal;
    }
    //! A normal member, Get Manure HUM N surface. Taking one argument and Returning one value.
    /*!
      \param j, an integer argument.
      \return a double value.
    */
    public double GetmanureHUMNsurface(int j)
    {
        double retVal = manureHUMNsurface[j];
        return retVal;
    }
    //! A normal member, Get Residue to Next. Returning one value.
    /*!
      \return an instance value that points to GlobalVar.product.
    */
    public GlobalVars.product GetresidueToNext() { return residueToNext; }
    //! A normal member, Get Total Manure FOM N surface. Returning one value.
    /*!
      \return a double value.
    */
    public double GetTotalmanureFOMNsurface()
    {
        double retVal = 0;
        for (int i = 0; i < manureFOMNsurface.Length; i++)
            retVal += manureFOMNsurface[i];
        return retVal;
    }
    //! A normal member, Get Fertiliser N Applied. Returning one value.
    /*!
      \return a double value.
    */
    public double GetFertiliserNapplied()
    {
        double retVal = 0;
        for (int i = 0; i < fertiliserApplied.Count; i++)
            retVal += fertiliserApplied[i].getNamount();
        return retVal;
    }
    //! A normal member, Get Manure N Applied. Returning one value.
    /*!
      \return a double value.
    */
    public double GetManureNapplied()
    {
        double retVal = 0;
        for (int i = 0; i < manureApplied.Count; i++)
            retVal += manureApplied[i].getNamount();
        return retVal;
    }
    //! A normal member, Get Residue N Input. Returning one value.
    /*!
      \return a double value.
    */
    public double GetresidueNinput()
    {
        double NinputInResidues = surfaceResidueN + subsurfaceResidueN;
        return NinputInResidues;
    }
    //! A normal member, Set Pot Evapp Trans. Taking two arugments.
    /*!
     \param index, an integer argument.
      \param val, a double argument.
    */
    public void SetpotEvapoTrans(int index, double val)
    {
        potEvapoTrans[index] = val;
    }
    //! A normal member, Set Evaporation. Taking two arugments.
    /*!
     \param index, an integer argument.
      \param val, a double argument.
    */
    public void Setevaporation(int index, double val)
    {
        evaporation[index] = val;
    }
    //! A normal member, Set Drainage. Taking two arugments.
    /*!
     \param index, an integer argument.
      \param val, a double argument.
    */
    public void Setdrainage(int index, double val)
    {
        drainage[index] = val;
    }
    //! A normal member, Set Canopy Storage. Taking two arugments.
    /*!
     \param index, an integer argument.
      \param val, a double argument.
    */
    public void SetcanopyStorage(int index, double val)
    {
        dailyCanopyStorage[index] = val;
    }
    //! A normal member, Set Transpire. Taking two arugments.
    /*!
     \param index, an integer argument.
      \param val, a double argument.
    */
    public void Settranspire(int index, double val)
    {
        transpire[index] = val;
    }
    //! A normal member, Set Organic N Leached. Taking one arugment.
    /*!
      \param aVal, a double argument.
    */
    public void SetOrganicNLeached(double aVal) { OrganicNLeached = aVal; }
    //! A normal member, Set Soil N Mineralisation. Taking one arugment.
    /*!
      \param aVal, a double argument.
    */
    public void SetsoilNMineralisation(double aVal) { soilNMineralisation = aVal; }
    //! A normal member, Set Soil N2O Emission Factor. Taking one arugment.
    /*!
      \param aVal, a double argument.
    */
    public void setsoilN2OEmissionFactor(double aVal) { soilN2OEmissionFactor = aVal; }
    //! A constructor.
    /*!
      without argument.
    */
    private CropClass(){}
    //! A normal member, Get Start Long Time. Returning one value.
    /*!
      \return a long value.
    */
    public long getStartLongTime() { return theStartDate.getLongTime(); }
    //! A normal member, Get End Long Time. Returning one value.
    /*!
      \return a long value.
    */
    public long getEndLongTime(){return theEndDate.getLongTime();}
    //! A normal member, Get C Fixed. Returning one value.
    /*!
      \return a double value.
    */
    public double getCFixed(){return CFixed;}
    //! A normal member, Get N Crop. Returning one value.
    /*!
      \return a double value.
    */
    public double getNCrop(){return NCrop;}
    //! A constructor with four arguments.
    /*!
      \param lastCropMonth, an integer arguemnt.
      \param lastCropYear, an integer argument.
      \param nextCropMonth, an integer argument.
      \param nextCropYear, an integer arugment.
    */
    public CropClass(int lastCropMonth, int lastCropYear, int nextCropMonth, int nextCropYear)
    {
        fertiliserApplied = new List<fertRecord>();
        manureApplied = new List<fertRecord>();
        theManureApplied = new List<manure>(); ;
        theStartDate = new timeClass();
        theStartDate.setDate(15, lastCropMonth, lastCropYear);
        theEndDate = new timeClass();
        theEndDate.setDate(15, nextCropMonth, nextCropYear);
        duration = theEndDate.getLongTime() - theStartDate.getLongTime() + 1;
        manureFOMCsurface = new double[duration];
        manureHUMCsurface = new double[duration];
        manureBiocharCsurface = new double[duration];
        manureFOMCsubsurface = new double[duration];
        manureHUMCsubsurface = new double[duration];
        manureBiocharCsubsurface = new double[duration];
        manureFOMNsurface = new double[duration];
        manureHUMNsurface = new double[duration];
        manureTAN = new double[duration];
        drainage = new double[duration];
        evaporation = new double[duration];
        potEvapoTrans = new double[duration];
        transpire = new double[duration];
        LAI = new double[duration];
        droughtFactorPlant = new double[duration];
        droughtFactorSoil = new double[duration];
        dailyNitrateLeaching = new double[duration];
        dailyCanopyStorage = new double[duration];
        fertiliserN = new double[duration];
        nitrificationInhibitor = new double[duration];
        soilWater = new double[duration];
        plantavailableWater = new double[duration];
        irrigationWater = new double[duration];
    }
    //! A constructor with five arguments.
    /*!
      \param path, a strign arguemnt.
      \param index, an integer argument.
      \param zoneNr, an integer argument.
      \param theCropSeqName, a string arugment.
      \param theArea, a double argument.
    */
    public CropClass(string path, int index, int zoneNr, string theCropSeqName, double theArea)
    {
        fertiliserApplied = new List<fertRecord>();
        manureApplied = new List<fertRecord>();
        theStartDate = new timeClass();
        theEndDate = new timeClass();

        area = theArea;
        cropSequenceName = theCropSeqName;
        FileInformation cropInformation = new FileInformation(GlobalVars.Instance.getFarmFilePath());
        cropInformation.setPath(path+"("+index+")");
        name = cropInformation.getItemString("NameOfCrop");
        int Start_day = cropInformation.getItemInt("Start_day");
        int Start_month = cropInformation.getItemInt("Start_month");
        int Start_year = cropInformation.getItemInt("Start_year");
        if (!theStartDate.setDate(Start_day, Start_month, Start_year))
        {
            GlobalVars.Instance.Error("Incorrect date for start date for " + name);
        }
        int End_day = cropInformation.getItemInt("End_day");
        int End_month = cropInformation.getItemInt("End_month");
        int End_year = cropInformation.getItemInt("End_year");
        if (!theEndDate.setDate(End_day, End_month, End_year))
        {
            GlobalVars.Instance.Error("Incorrect date for end date for " + name);
        }
        duration = theEndDate.getLongTime() - theStartDate.getLongTime() + 1;
        if (duration <= 0)
        {
            string outputString = "negative duration in crop sequence name " + theCropSeqName + " crop name " + name;
            GlobalVars.Instance.Error(outputString);
        }
        drainage = new double[duration];
        evaporation = new double[duration];
        potEvapoTrans = new double[duration];
        transpire = new double[duration];
        LAI = new double[duration];
        soilWater = new double[duration];
        plantavailableWater = new double[duration];
        droughtFactorPlant = new double[duration];
        droughtFactorSoil = new double[duration];
        dailyNitrateLeaching = new double[duration];
        dailyCanopyStorage = new double[duration];
        irrigationWater = new double[duration];
        fertiliserN = new double[duration];
        nitrificationInhibitor = new double[duration];
        manureFOMCsurface = new double[duration];
        manureHUMCsurface = new double[duration];
        manureBiocharCsurface = new double[duration];
        manureFOMCsubsurface = new double[duration];
        manureHUMCsubsurface = new double[duration];
        manureBiocharCsubsurface = new double[duration];
        manureFOMNsurface = new double[duration];
        manureHUMNsurface = new double[duration];
        manureTAN = new double[duration];
        string tempString = cropInformation.getItemString("Irrigation");
        if (tempString == "false")
            isIrrigated = false;
        else
            isIrrigated = true;
        if (name != "Bare soil")
            GetCropInformation(path, index, zoneNr);
        int length = name.IndexOf("Permanent");
        if (length != -1)
        {
            permanent = true;
            droughtSusceptability = 0.5;
        }
        int minFertiliserApplied = 99, maxFertiliserApplied = 0;
        cropInformation.PathNames.Add("Fertilizer_applied");
        cropInformation.getSectionNumber(ref minFertiliserApplied, ref maxFertiliserApplied);
        bool tooEarly = false;
        bool tooLate = false;
        for (int i = minFertiliserApplied; i <= maxFertiliserApplied; i++)
        {
            if (cropInformation.doesIDExist(i))
            {
                fertRecord newFertRecord = new fertRecord();
                newFertRecord.applicdate = new timeClass();
                cropInformation.Identity.Add(i);
                newFertRecord.Name = cropInformation.getItemString("Name");
                newFertRecord.Units = cropInformation.getItemString("Unit");
                if (newFertRecord.Units.CompareTo("kg N/ha") == 0)
                    newFertRecord.Namount = cropInformation.getItemDouble("Value");
                if (newFertRecord.Units.CompareTo("t/ha") == 0)
                    newFertRecord.Camount = cropInformation.getItemDouble("Value");

                newFertRecord.Month_applied = cropInformation.getItemInt("Month_applied");
                if (newFertRecord.ReadFertManApplication(cropInformation, theStartDate, theEndDate) == false)
                {

                    string messageString = ("Error - fertiliser applied outside the period of this crop\n");
                    messageString = messageString + ("Crop sequence name = " + cropSequenceName + "\n");
                    messageString = messageString + ("Crop sequence number = " + cropSequenceNo + "\n");
                    messageString = messageString + ("Crop name = " + name);
                    GlobalVars.Instance.Error(messageString);
                }
                cropInformation.PathNames.Add("Applic_technique_Fertilizer");
                cropInformation.Identity.Add(-1);
                newFertRecord.Applic_techniqueS = cropInformation.getItemString("NameS");
                newFertRecord.Applic_techniqueI = cropInformation.getItemInt("NameI");
                fertiliserApplied.Add(newFertRecord);
                cropInformation.PathNames.RemoveAt(cropInformation.PathNames.Count - 1);
                cropInformation.Identity.RemoveAt(cropInformation.Identity.Count - 1);
                cropInformation.Identity.RemoveAt(cropInformation.Identity.Count - 1);
            }
        }

        int minManure_applied = 99, maxManure_applied = 0;
        cropInformation.PathNames[cropInformation.PathNames.Count - 1] = "Manure_applied";
        cropInformation.getSectionNumber(ref minManure_applied, ref maxManure_applied);
        //Check to see if manure is applied outside crop period. Why does this code not use ReadFertManApplication? NJH
        for (int i = minManure_applied; i <= maxManure_applied; i++)
        {
            if (cropInformation.doesIDExist(i))
            {
                fertRecord newFertRecord = new fertRecord();
                newFertRecord.applicdate = new timeClass();
                cropInformation.Identity.Add(i);
                newFertRecord.Name = cropInformation.getItemString("Name");
                newFertRecord.Units = cropInformation.getItemString("Unit");
                newFertRecord.Namount = cropInformation.getItemDouble("Value");
                newFertRecord.Month_applied = cropInformation.getItemInt("Month_applied");
                //only have month of application, so need to set a sensible day in month
                if (newFertRecord.Month_applied == Start_month)
                    newFertRecord.dayOfApplication = Start_day; //earliest possible day
                else if (newFertRecord.Month_applied == End_month)
                    newFertRecord.dayOfApplication = End_day;//last possible day
                else
                    newFertRecord.dayOfApplication = 15;//some day in the middle of the month
                bool inPeriod = true;
                if (GetEndYear() > GetStartYear()) //crop period straddles 1 Jan
                {
                    if ((newFertRecord.GetMonth_applied() < GetStartMonth()) && (newFertRecord.GetMonth_applied() > GetEndMonth()))
                    {
                        if (newFertRecord.GetMonth_applied() < GetStartMonth())
                            tooEarly = true;
                        if (newFertRecord.GetMonth_applied() > GetEndMonth())
                            tooLate = true;
                            inPeriod = false;
                    }
                    else

                        if ((newFertRecord.GetMonth_applied() >= GetStartMonth()) && (newFertRecord.GetMonth_applied() <= 12))
                        newFertRecord.SetapplicDate(newFertRecord.dayOfApplication, newFertRecord.Month_applied, GetStartYear());
                    else
                        newFertRecord.SetapplicDate(newFertRecord.dayOfApplication, newFertRecord.Month_applied, GetEndYear());
                }
                else //crop period is within one calendar year
                {
                    if ((newFertRecord.GetMonth_applied() < GetStartMonth()) || (newFertRecord.GetMonth_applied() > GetEndMonth()))
                    {
                        if (newFertRecord.GetMonth_applied() < GetStartMonth())
                            tooEarly = true;
                        if (newFertRecord.GetMonth_applied() > GetEndMonth())
                            tooLate = true;
                        inPeriod = false;
                    }
                    else
                        newFertRecord.SetapplicDate(newFertRecord.dayOfApplication, newFertRecord.Month_applied, GetStartYear());
                }
                if (inPeriod == false)
                {

                    string messageString="";
                    if (tooEarly)
                        messageString = ("Error - manure applied before this crop starts\n");
                    if (tooLate)
                        messageString = ("Error - manure applied after this crop ends\n");

                    messageString = messageString + ("Crop sequence name = " + cropSequenceName + "\n");
                    messageString = messageString + ("Crop sequence number = " + cropSequenceNo + "\n");
                    messageString = messageString + ("Crop name = " + name);
                    GlobalVars.Instance.Error(messageString);
                }

                newFertRecord.ManureStorageID = GlobalVars.Instance.getManureStorageID(cropInformation.getItemInt("StorageType"));
                newFertRecord.speciesGroup = cropInformation.getItemInt("SpeciesGroup");
                cropInformation.PathNames.Add("Applic_technique_Manure");
                cropInformation.Identity.Add(-1);
                newFertRecord.Applic_techniqueS = cropInformation.getItemString("NameS");
                newFertRecord.Applic_techniqueI = cropInformation.getItemInt("NameI");
                manureApplied.Add(newFertRecord);
                cropInformation.PathNames.RemoveAt(cropInformation.PathNames.Count - 1);
                cropInformation.Identity.RemoveAt(cropInformation.Identity.Count - 1);
                cropInformation.Identity.RemoveAt(cropInformation.Identity.Count - 1);
            }
        }
        totalTsum = GlobalVars.Instance.theZoneData.GetPeriodTemperatureSum(Start_day, Start_month, Start_year, End_day, End_month, End_year, baseTemperature);
        if (totalTsum <= 0)
        {
            string messageString = ("Error - total Tsum is zero or less\n");
            messageString = messageString + ("Crop sequence name = " + cropSequenceName + "\n");
            messageString = messageString + ("Crop sequence number = " + cropSequenceNo + "\n");
            messageString = messageString + ("Crop name = " + name);
            GlobalVars.Instance.Error(messageString);
        }
        getParameters(index, zoneNr, path);
    }

    public void UpdateParens(string aParent, int ID)
    {
        identity = ID;

        Parens = aParent;
        for (int i = 0; i < fertiliserApplied.Count; i++)
        {
            fertiliserApplied[i].setParens( Parens + "_" + i.ToString());
        }
        for (int i = 0; i < manureApplied.Count; i++)
        {
            manureApplied[i].setParens(Parens + "_" + i.ToString());
        }

    }

    public void GetBareSoilResidues(string path, int index, int zoneNr)
    {
        FileInformation cropInformation = new FileInformation(GlobalVars.Instance.getFarmFilePath());
        cropInformation.setPath(path + "(" + index + ").Product(-1)");
        cropInformation.Identity.RemoveAt(cropInformation.Identity.Count - 1);
        int minProduct = 99, maxProduct = 0;
        cropInformation.getSectionNumber(ref minProduct, ref maxProduct);
        double isGrazedProduction = 0;
        for (int i = minProduct; i <= maxProduct; i++)
        {
            if (cropInformation.doesIDExist(i) == true)
            {
                cropInformation.Identity.Add(i);
                string cropPath = path + "(" + index + ")" + ".Product";
                GlobalVars.product anExample = new GlobalVars.product();
                feedItem aComposition = new feedItem();
                anExample.composition = aComposition;
                anExample.Harvested = cropInformation.getItemString("Harvested");
                string temp = path + "(" + index + ")" + ".Product" + "(" + i.ToString() + ").Expected_yield(-1)";
                anExample.Expected_yield = cropInformation.getItemDouble("Value", temp);
                if (anExample.composition.GetisGrazed() == true)
                {
                    anExample.Grazed_yield = anExample.Expected_yield;
                    isGrazedProduction += anExample.Expected_yield;
                }
                if (anExample.Expected_yield > 0)
                    theProducts.Add(anExample);
                else
                {
                    string messageString = ("Error - grazed yield of grazed residue crop must be greater than zero \n");
                    messageString = messageString + ("Crop sequence name = " + cropSequenceName + "\n");
                    messageString = messageString + ("Crop sequence number = " + cropSequenceNo + "\n");
                    messageString = messageString + ("Crop name = " + name);
                    GlobalVars.Instance.Error(messageString);
                }
                cropInformation.PathNames.RemoveAt(cropInformation.PathNames.Count - 1);
                cropInformation.Identity.RemoveAt(cropInformation.Identity.Count - 1);
                cropInformation.Identity.RemoveAt(cropInformation.Identity.Count - 1);
            }
        }
    }
    public void AddProductsWithResidue(List<GlobalVars.product> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            theProducts.Add(list[i]);
        }
    }
    public void GetCropInformation(string path, int index, int zoneNr)
    {
        GlobalVars.Instance.log("Entering GetCropInformation " + " crop " + name.ToString(), 6);

        FileInformation cropInformation = new FileInformation(GlobalVars.Instance.getFarmFilePath());
        cropInformation.setPath(path + "(" + index + ")");
        cropInformation.Identity.Add(-1);
        cropInformation.PathNames.Add("HarvestMethod");
        harvestMethod = cropInformation.getItemString("Value");
        cropInformation.Identity.RemoveAt(cropInformation.Identity.Count - 1);
        cropInformation.PathNames[cropInformation.PathNames.Count - 1] = "Product";
        int minProduct = 99, maxProduct = 0;
        cropInformation.getSectionNumber(ref minProduct, ref maxProduct);
        double totProduction = 0;
        double isGrazedProduction = 0;
        for (int i = minProduct; i <= maxProduct; i++)
        {
            if(cropInformation.doesIDExist(i)==true)
            {
                cropInformation.Identity.Add(i);
                string cropPath = path + "(" + index + ")" + ".Product";
                GlobalVars.product anExample = new GlobalVars.product();
                feedItem aComposition = new feedItem(cropPath, i, false,Parens+"_"+i.ToString());
                anExample.composition = aComposition;
                anExample.Harvested = cropInformation.getItemString("Harvested");

                /*bool checkIncorp = anExample.composition.GetName().Contains("Incorporated");
                if (checkIncorp)
                    anExample.Harvested = "Incorporated";
                */
        string temp = path + "(" + index + ")" + ".Product" + "(" + i.ToString() + ").Potential_yield(-1)";
                anExample.Potential_yield = cropInformation.getItemDouble("Value", temp);
                if (anExample.Potential_yield<=0)
                {
                    string messageString = ("Error - potential production of a crop product cannot be zero or less than zero\n");
                    messageString += ("Crop source = " + path + "\n");
                    messageString += ("Crop name = " + name);
                    GlobalVars.Instance.Error(messageString);
                }
                temp = path + "(" + index + ")" + ".Product" + "(" + i.ToString() + ").Expected_yield(-1)";
                anExample.Expected_yield = cropInformation.getItemDouble("Value", temp);
                totProduction += anExample.Potential_yield;
                if (anExample.composition.GetisGrazed() == true || anExample.Harvested.Contains("Residue"))
                {
                    anExample.Grazed_yield = anExample.Expected_yield;
                    isGrazedProduction += anExample.Expected_yield;
                }
                else
                    if (!anExample.Harvested.Contains("Incorporated"))
                        anExample.composition.AdjustAmount(1+anExample.composition.GetStoreProcessFactor());
                if (anExample.Potential_yield >0)
                    theProducts.Add(anExample);
                cropInformation.PathNames.RemoveAt(cropInformation.PathNames.Count - 1);
                cropInformation.Identity.RemoveAt(cropInformation.Identity.Count - 1);
                cropInformation.Identity.RemoveAt(cropInformation.Identity.Count - 1);
            }
        }

        if ((totProduction== 0)&&(name!="Bare soil"))
        {

            string messageString=("Error - total potential production of a crop cannot be zero\n");
            messageString+=("Crop source = " + path+"\n");
            messageString += ("Crop name = " + name);
            GlobalVars.Instance.Error(messageString);
        }
    }
    //! A normal member, Get Parameters. Taking three arugments.
    /*!
     \param cropIdentityNo, an integer argument.
      \param zoneNR, an integer argument.
      \param cropPath, a string argument.
    */
    public void getParameters(int cropIdentityNo, int zoneNR, string cropPath)
    {
        identity = cropIdentityNo;
        FileInformation cropParamFile = new FileInformation(GlobalVars.Instance.getParamFilePath());  //prob here
        string basePath = "AgroecologicalZone(" + zoneNR + ")";
        string tmpPath;
        NDepositionRate = GlobalVars.Instance.theZoneData.GetNdeposition();
        tmpPath = basePath + ".UrineNH3EF(-1)";
        cropParamFile.setPath(tmpPath);
        if (zeroGasEmissionsDebugging)
            urineNH3EmissionFactor = 0;
        else
            urineNH3EmissionFactor = cropParamFile.getItemDouble("Value");

        cropParamFile.Identity.RemoveAt(cropParamFile.Identity.Count - 1);
        if (name != "Bare soil")
        {

            cropParamFile.PathNames[cropParamFile.PathNames.Count - 1] = "Crop";
            int min = 99, max = 0;
            cropParamFile.getSectionNumber(ref min, ref max);
            bool gotit = false;
            string aname ="None";
            for (int j = min; j <= max; j++)
            {
                if (cropParamFile.doesIDExist(j))
                {
                    tmpPath = basePath + ".Crop" + "(" + j.ToString() + ")";
                    cropParamFile.setPath(tmpPath);
                    aname = cropParamFile.getItemString("Name");
         
                    if (aname == name)
                    {
                        gotit = true;
                        break;
                    }
                    cropParamFile.Identity.RemoveAt(cropParamFile.Identity.Count - 1);
                }
            }
            if (gotit == false)
            {
              
                string message1=("Error - could not find crop in parameter file\n");
                message1 += message1 + ("Crop source = " + cropPath + "(" + cropIdentityNo.ToString() + ")\n");
                message1+=("Crop name = " + name);
                GlobalVars.Instance.Error(message1);
            }
            
            cropParamFile.setPath(tmpPath + ".MaxLAI(-1)");
            double value = cropParamFile.getItemDouble("Value", false);
            if (value!=0)
                maxLAI = value;
          
            cropParamFile.setPath(tmpPath + ".NavailMax(-1)");
            cropParamFile.setPath(tmpPath + ".NfixationFactor(-1)");
            NfixationFactor = cropParamFile.getItemDouble("Value");
            
            cropParamFile.setPath(tmpPath + ".PropBelowGroundResidues(-1)");
            propBelowGroundResidues = cropParamFile.getItemDouble("Value");
            cropParamFile.setPath(tmpPath + ".BelowGroundCconc(-1)");
            CconcBelowGroundResidues = cropParamFile.getItemDouble("Value");
            cropParamFile.setPath(tmpPath + ".BelowGroundCtoN(-1)");
            CtoNBelowGroundResidues = cropParamFile.getItemDouble("Value");
            //at this point, tmpPath = "AgroecologicalZone(1).Crop(20)"
            // PathNames are “AgroecologicalZone” and “Crop", Identity = “1”, “20” and “-1”
            cropParamFile.setPath(tmpPath + ".MaximumRootingDepth(-1)");
            // PathNames are “AgroecologicalZone”, “Crop" and “MaximumRootingDepth”, Identity = “1”, “20” and “-1”
            MaximumRootingDepth = cropParamFile.getItemDouble("Value");
            cropParamFile.setPath(tmpPath + ".Irrigation(-1).irrigationThreshold(-1)");
            irrigationThreshold= cropParamFile.getItemDouble("Value", false);
            // PathNames are “AgroecologicalZone”, “Crop", “Irrigation” and “irrigationThreshold”, Identity = “1”, “20”, “-1” and “-1”
            cropParamFile.setPath(tmpPath + ".Irrigation(-1).irrigationMinimum(-1)");
            irrigationMinimum = cropParamFile.getItemDouble("Value", false);

            cropParamFile.Identity.RemoveAt(cropParamFile.Identity.Count - 1);
            cropParamFile.Identity.RemoveAt(cropParamFile.Identity.Count - 1);
            // PathNames are “AgroecologicalZone”, “Crop", “Irrigation” and “irrigationThreshold”, Identity = “1” and “20”
            cropParamFile.PathNames.RemoveAt(cropParamFile.PathNames.Count - 1);
            // PathNames are “AgroecologicalZone”, “Crop" and “Irrigation”, Identity = “1” and “20”
            cropParamFile.PathNames[cropParamFile.PathNames.Count - 1] = "HarvestMethod";
            // PathNames are “AgroecologicalZone”, “Crop" and “HarvestMethod”, Identity = “1” and “20”
            max = 0;
            min = 999;
            cropParamFile.getSectionNumber(ref min, ref max);
            cropParamFile.Identity.Add(-1);
            // PathNames are “AgroecologicalZone”, “Crop" and “HarvestMethod”, Identity = “1”, “20” and “-1”
            for (int i = min; i <= max; i++)
            {
                //    if (cropParamFile.doesIDExist(i))
                {
                    cropParamFile.Identity[cropParamFile.PathNames.Count - 1] = i;
                    string harvestMethodName = cropParamFile.getItemString("Name");
                    if (harvestMethodName == harvestMethod)
                    {
                        cropParamFile.PathNames.Add("PropAboveGroundResidues");
                        cropParamFile.Identity.Add(-1);
                        if (harvestMethodName == "Grazing")
                            propAboveGroundResidues[1] = cropParamFile.getItemDouble("Value");
                        else
                            propAboveGroundResidues[0] = cropParamFile.getItemDouble("Value");
                        break;
                    }
                }
            }
        }
    }
    //! A normal member, Calculate Water limited yield. Taking one arugment.
    /*!
     \param droughtIndex, a double argument.
    */
    public void Calcwaterlimited_yield(double droughtIndex)
    {
        if (Getname() != "Bare soil")
        {
            for (int k = 0; k < theProducts.Count; k++)
            {
                if (isIrrigated)
                {
                    NyieldMax += theProducts[k].composition.GetN_conc() * theProducts[k].Potential_yield;
                    theProducts[k].SetwaterLimited_yield(theProducts[k].Potential_yield);
                }
                else
                {
                    NyieldMax += theProducts[k].composition.GetN_conc() * theProducts[k].Potential_yield * (1 - droughtSusceptability * droughtIndex);
                    theProducts[k].SetwaterLimited_yield(theProducts[k].Potential_yield * (1 - droughtSusceptability * droughtIndex));
                }
                theProducts[k].SetExpectedYield(theProducts[k].GetwaterLimited_yield());
            }
            maxCropNuptake = CalculateCropNUptake();
        }
    }

    //! A normal member, Calculate Crop N Uptake. Returning one value.
    /*!
     \return a double value.
    */
    public double CalculateCropNUptake()
    {
        double uptakeN = 0;
        double NinProduct = 0;
        double NinSurfaceResidues = 0;
        double NinSubsurfaceResidues = 0;
        for (int i = 0; i < theProducts.Count; i++)
        {
            if (theProducts[i].composition.GetisGrazed()) //grazed crop
            {
                NinProduct += theProducts[i].GetExpectedYield() * theProducts[i].composition.GetN_conc();
                NinSurfaceResidues += theProducts[i].GetExpectedYield() * theProducts[i].composition.GetN_conc() * propAboveGroundResidues[1];
            }
            else
            {
                //ungrazed part of crop
                NinProduct += theProducts[i].GetExpectedYield() * theProducts[i].composition.GetN_conc();
                NinSurfaceResidues += theProducts[i].GetExpectedYield() * theProducts[i].composition.GetN_conc() * propAboveGroundResidues[0];
            }
            double adjustment = 1.0;// (1 - 0.5) / (theProducts[i].GetExpectedYield() / theProducts[i].GetPotential_yield());
            NinSubsurfaceResidues += theProducts[i].GetExpectedYield() * (GetCconcBelowGroundResidues() * GetpropBelowGroundResidues() * adjustment) / CtoNBelowGroundResidues;
        }
        uptakeN += NinProduct + NinSurfaceResidues + NinSubsurfaceResidues;
        return uptakeN;
    }
    //! A normal member, Calculate Max Crop N Uptake. Returning one value.
    /*!
     \return a double value.
    */
    public double CalculateMaxCropNUptake()
    {
        double uptakeN = 0;
        double NinProduct = 0;
        double NinSurfaceResidues = 0;
        double NinSubsurfaceResidues = 0;
        for (int i = 0; i < theProducts.Count; i++)
        {
            if (theProducts[i].composition.GetisGrazed()) //grazed crop
            {
                NinProduct += theProducts[i].GetPotential_yield() * theProducts[i].composition.GetN_conc();
                NinSurfaceResidues += theProducts[i].GetPotential_yield() * theProducts[i].composition.GetN_conc() * propAboveGroundResidues[1];
                NinSubsurfaceResidues += theProducts[i].GetPotential_yield() * (GetCconcBelowGroundResidues() * GetpropBelowGroundResidues()) / CtoNBelowGroundResidues;
            }
            else
            {
                //ungrazed part of crop
                NinProduct += theProducts[i].GetPotential_yield() * theProducts[i].composition.GetN_conc();
                NinSurfaceResidues += theProducts[i].GetPotential_yield() * theProducts[i].composition.GetN_conc() * propAboveGroundResidues[0];
                NinSubsurfaceResidues += theProducts[i].GetPotential_yield() * (GetCconcBelowGroundResidues() * GetpropBelowGroundResidues()) / CtoNBelowGroundResidues;
            }
        }
        uptakeN += NinProduct + NinSurfaceResidues + NinSubsurfaceResidues;
        return uptakeN;
    }
    //! A normal member, Calculate Harvested Yields.
    /*!
     without return value.
    */
    public void CalculateHarvestedYields()
    {
        if (residueFromPrevious == null)
        {
            harvestedC = 0;
            harvestedN = 0;
            harvestedDM = 0;
            grazedC = 0;
            grazedN = 0;
        }
        for (int i = 0; i < theProducts.Count; i++)
        {
            if (theProducts[i].composition.GetisGrazed()) //grazed crop
            {
                if (GlobalVars.Instance.GetstrictGrazing())
                {
                    double grazedProductC = theProducts[i].Grazed_yield * theProducts[i].composition.GetC_conc();
                    harvestedDM = theProducts[i].Grazed_yield;
                    harvestedC += grazedProductC;
                    grazedC += grazedProductC;
                    harvestedN += theProducts[i].Grazed_yield * theProducts[i].composition.GetN_conc();
                    grazedN += theProducts[i].Grazed_yield * theProducts[i].composition.GetN_conc();
                }
                else
                {
                    if (theProducts[i].GetExpectedYield() > theProducts[i].GetGrazed_yield() && theProducts[i].GetGrazed_yield()!=0)
                    {
                        //theProducts[i].SetExpectedYield(theProducts[i].GetGrazed_yield());
                        harvestedDM += theProducts[i].GetGrazed_yield();
                        harvestedC += theProducts[i].GetGrazed_yield() * theProducts[i].composition.GetC_conc();
                        harvestedN += theProducts[i].GetGrazed_yield() * theProducts[i].composition.GetN_conc();
                        grazedN += theProducts[i].GetGrazed_yield() * theProducts[i].composition.GetN_conc();
                    }
                    else
                    {
                        harvestedDM += theProducts[i].GetExpectedYield();
                        harvestedC += theProducts[i].GetExpectedYield() * theProducts[i].composition.GetC_conc();
                        harvestedN += theProducts[i].GetExpectedYield() * theProducts[i].composition.GetN_conc();
                    }
                }
            }
            else
            {
                if ((theProducts[i].Harvested == "Harvested") || (theProducts[i].Harvested == "Burnt stubble"))
                {
                    harvestedDM += theProducts[i].GetExpectedYield();
                    harvestedC += theProducts[i].GetExpectedYield() * theProducts[i].composition.GetC_conc();
                    harvestedN += theProducts[i].GetExpectedYield() * theProducts[i].composition.GetN_conc();
                }
            }
        }
    }
    //! A normal member, Calculate C Fixed.
    /*!
     without return value.
    */
    public void CalculateCFixed()
    {
        CFixed = 0;
        if (Getname() != "Bare soil")
        {
            for (int i = 0; i < theProducts.Count; i++)
            {
                double CFixedThisCrop = 0;
                double CaboveGroundResidues = 0;
                double CbelowGroundResidues = 0;
                double CHarvestable = 0;
                if (theProducts[i].composition.GetisGrazed())  //
                    CaboveGroundResidues = theProducts[i].GetExpectedYield() * theProducts[i].composition.GetC_conc() * propAboveGroundResidues[1];
                else
                    CaboveGroundResidues = theProducts[i].GetExpectedYield() * theProducts[i].composition.GetC_conc() * propAboveGroundResidues[0];
                CFixedThisCrop += CaboveGroundResidues;
                double adjustment = 1.0;// (1 - 0.5) / (theProducts[i].GetExpectedYield() / theProducts[i].GetPotential_yield());
                CbelowGroundResidues = theProducts[i].GetExpectedYield() * propBelowGroundResidues * GetCconcBelowGroundResidues() * adjustment;
                CFixedThisCrop += CbelowGroundResidues;
                CHarvestable = theProducts[i].GetExpectedYield() * theProducts[i].composition.GetC_conc();
                CFixedThisCrop += CHarvestable;
                CFixed += CFixedThisCrop;
            }
        }
    }
    //! A normal member, Calculate Crop Residues.
    /*!
     without return value.
    */
    public void CalculateCropResidues()
    {
        surfaceResidueC = 0;
        subsurfaceResidueC = 0;
        surfaceResidueN = 0;
        subsurfaceResidueN = 0;
        surfaceResidueDM = 0;
        unutilisedGrazableC = 0;
        unutilisedGrazableN = 0;
        unutilisedGrazableDM = 0;
        if (Getname() != "Bare soil")
        {
            for (int i = 0; i < theProducts.Count; i++)
            {
                if (theProducts[i].composition.GetisGrazed()) //grazed crop
                {
                    surfaceResidueC += theProducts[i].GetExpectedYield() * theProducts[i].composition.GetC_conc() * propAboveGroundResidues[1];
                    surfaceResidueN += theProducts[i].GetExpectedYield() * theProducts[i].composition.GetN_conc() * propAboveGroundResidues[1];
                    surfaceResidueDM += theProducts[i].GetExpectedYield() * propAboveGroundResidues[1];
                    if (theProducts[i].Expected_yield >= theProducts[i].Grazed_yield)//yield in excess of grazed is added to surface residues
                    {
                        unutilisedGrazableC += (theProducts[i].Expected_yield - theProducts[i].Grazed_yield) * theProducts[i].composition.GetC_conc();
                        surfaceResidueC += (theProducts[i].Expected_yield - theProducts[i].Grazed_yield) * theProducts[i].composition.GetC_conc();
                        unutilisedGrazableN += (theProducts[i].Expected_yield - theProducts[i].Grazed_yield) * theProducts[i].composition.GetN_conc();
                        surfaceResidueN += (theProducts[i].Expected_yield - theProducts[i].Grazed_yield) * theProducts[i].composition.GetN_conc();
                        unutilisedGrazableDM += (theProducts[i].Expected_yield - theProducts[i].Grazed_yield);
                        surfaceResidueDM += (theProducts[i].Expected_yield - theProducts[i].Grazed_yield);
                    }
                }
                else //ungrazed crop
                {
                    if ((theProducts[i].Harvested == "Harvested") || (theProducts[i].Harvested == "Burnt stubble"))
                    {
                        surfaceResidueC += theProducts[i].GetExpectedYield() * theProducts[i].composition.GetC_conc() * propAboveGroundResidues[0];
                        surfaceResidueN += theProducts[i].GetExpectedYield() * theProducts[i].composition.GetN_conc() * propAboveGroundResidues[0];
                        surfaceResidueDM += theProducts[i].GetExpectedYield() * propAboveGroundResidues[0];
                    }
                    else if (theProducts[i].Harvested.Contains("Residue"))
                    {
                        residueToNext = new GlobalVars.product(theProducts[i]);
                        //add surface residues to amount to carry over to bare soil
                        residueToNext.SetModelled_yield(theProducts[i].GetExpectedYield() * (1 + propAboveGroundResidues[0]));
                        residueToNext.SetExpectedYield(theProducts[i].GetExpectedYield() * (1 + propAboveGroundResidues[0]));
                    }
                    else
                    {
                        surfaceResidueC += theProducts[i].GetExpectedYield() * theProducts[i].composition.GetC_conc() * (propAboveGroundResidues[0] + 1);
                        surfaceResidueN += theProducts[i].GetExpectedYield() * theProducts[i].composition.GetN_conc() * (propAboveGroundResidues[0] + 1);
                        surfaceResidueDM += theProducts[i].GetExpectedYield() * (propAboveGroundResidues[0] + 1);
                    }
                }
                double adjustment = 1.0;// (1 - 0.5) / (theProducts[i].GetExpectedYield() / theProducts[i].GetPotential_yield());
                subsurfaceResidueC += theProducts[i].GetExpectedYield() * (GetCconcBelowGroundResidues() * GetpropBelowGroundResidues() * adjustment);
                subsurfaceResidueN += theProducts[i].GetExpectedYield() * GetCconcBelowGroundResidues() * GetpropBelowGroundResidues()*adjustment / CtoNBelowGroundResidues;
            }
        }
    }

    //! A normal member, Calculate Crop Residue Burning.
    /*!
     without return value.
    */
    public void CalculateCropResidueBurning()
    {
        double DMburnt = 0;
        for (int i = 0; i < theProducts.Count; i++)
        {
            if ((theProducts[i].Harvested == "Burnt") || (theProducts[i].Harvested == "Burnt stubble"))
            {
                DMburnt = surfaceResidueDM;
                burntResidueC = surfaceResidueC;
                burntResidueN = surfaceResidueN;
                surfaceResidueN = 0.0;
                surfaceResidueC = 0.0;
            }
        }
        if (!zeroGasEmissionsDebugging)
        {
            burningBlackC = DMburnt * GlobalVars.Instance.theZoneData.GetburntResidueBlackCEmissionFactor();
            burningCOC = DMburnt * GlobalVars.Instance.theZoneData.GetburntResidueCOEmissionFactor();
            burningCO2C = burntResidueC - (burningBlackC + burningCOC);

            burningN2ON = DMburnt * GlobalVars.Instance.theZoneData.GetburntResidueN2OEmissionFactor();
            burningNH3N = DMburnt * GlobalVars.Instance.theZoneData.GetburntResidueNH3EmissionFactor();
            burningNOxN = DMburnt * GlobalVars.Instance.theZoneData.GetburntResidueNOxEmissionFactor();
        }
        burningOtherN = burntResidueN - (burningN2ON + burningNH3N + burningNOxN);
    }
    //! A normal member, Calculate Excreta N Input.
    /*!
     without return value.
    */
    public void CalculateExcretaNInput()
    {
        excretaNInput = 0;
        urineNasFertilizer = 0;
        faecalN = 0;
        int feedCode;
        urineNH3emission = 0;
        for (int i = 0; i < theProducts.Count; i++)
        {
            feedCode = theProducts[i].composition.GetFeedCode();
            if (theProducts[i].composition.GetisGrazed())
            {
                for (int j = 0; j < GlobalVars.Instance.getmaxNumberFeedItems(); j++)
                {
                    if (theProducts[i].composition.GetFeedCode() == j)
                    {
                        double grazedDM=GlobalVars.Instance.grazedArray[j].fieldDMgrazed;
                        if (grazedDM > 0)
                        {
                            double proportion = theProducts[i].Grazed_yield / grazedDM;
                            urineNasFertilizer += proportion * GlobalVars.Instance.grazedArray[j].urineN;
                            urineNH3emission += urineNH3EmissionFactor * urineNasFertilizer;
                            faecalN += proportion * GlobalVars.Instance.grazedArray[j].faecesN;
                        }
                    }
                }
            }
        }
        if (residueFromPrevious!=null)
        {
            feedCode = residueFromPrevious.composition.GetFeedCode();
            for (int j = 0; j < GlobalVars.Instance.getmaxNumberFeedItems(); j++)
            {
                if (residueFromPrevious.composition.GetFeedCode() == j)
                {
                    double grazedDM = GlobalVars.Instance.grazedArray[j].fieldDMgrazed;
                    if (grazedDM > 0)
                    {
                        double proportion = residueFromPrevious.Grazed_yield / grazedDM;
                        urineNasFertilizer += proportion * GlobalVars.Instance.grazedArray[j].urineN;
                        urineNH3emission += urineNH3EmissionFactor * urineNasFertilizer;
                        faecalN += proportion * GlobalVars.Instance.grazedArray[j].faecesN;
                    }
                }
            }
        }
        excretaNInput = urineNasFertilizer + faecalN;
    }
    //! A normal member, Calculate Excreta C Input.
    /*!
     without return value.
    */
    public void CalculateExcretaCInput()
    {
        excretaCInput = 0;
        urineC = 0;
        faecalC = 0;
        grazingCH4C = 0;
        int feedCode;
        for (int i = 0; i < theProducts.Count; i++)
        {
            feedCode = theProducts[i].composition.GetFeedCode();
            if (theProducts[i].composition.GetisGrazed())
            {
                for (int j = 0; j < GlobalVars.Instance.getmaxNumberFeedItems(); j++)
                {
                    if (theProducts[i].composition.GetFeedCode() == j)
                    {
                        if (GlobalVars.Instance.grazedArray[j].fieldDMgrazed > 0)
                        {
                            double proportion = theProducts[i].Grazed_yield / GlobalVars.Instance.grazedArray[j].fieldDMgrazed;
                            urineC += proportion * GlobalVars.Instance.grazedArray[j].urineC;
                            faecalC += proportion * GlobalVars.Instance.grazedArray[j].faecesC;
                            grazingCH4C += proportion * GlobalVars.Instance.grazedArray[j].fieldCH4C;
                        }
                    }
                }
            }
        }
        if (residueFromPrevious != null)
        {
            feedCode = residueFromPrevious.composition.GetFeedCode();
            for (int j = 0; j < GlobalVars.Instance.getmaxNumberFeedItems(); j++)
            {
                if (residueFromPrevious.composition.GetFeedCode() == j)
                {
                    double grazedDM = GlobalVars.Instance.grazedArray[j].fieldDMgrazed;
                    if (grazedDM > 0)
                    {
                        double proportion = residueFromPrevious.Grazed_yield / GlobalVars.Instance.grazedArray[j].fieldDMgrazed;
                        urineC += proportion * GlobalVars.Instance.grazedArray[j].urineC;
                        faecalC += proportion * GlobalVars.Instance.grazedArray[j].faecesC;
                        grazingCH4C += proportion * GlobalVars.Instance.grazedArray[j].fieldCH4C;
                    }
                }
            }
        }
        excretaCInput = urineC + faecalC;
    }
    //! A normal member, Calculate Manure Input Limited.
    /*!
     without return value.
    */
    public void CalculateManureInputLimited()
    {
        for (int i = 0; i < manureApplied.Count; i++)
        {
            double amountTotalN = manureApplied[i].getNamount();
            int ManureType = manureApplied[i].getManureType();
            int speciesGroup = manureApplied[i].getspeciesGroup();            
             GlobalVars.Instance.theManureExchange.TakeManure(amountTotalN, lengthOfSequence, ManureType, speciesGroup);
              
        }
    }
    //! A normal member, Calculate Manure Input. Taking one argument.
    /*!
     \param lockit, one boolean argument.
    */
    public void CalculateManureInput(bool lockit)
    {
        manureNH3emission = 0;
        //need to modify this to allow manure OM to be placed at different depths in the soil
        for (int i = 0; i < duration; i++)
        {
            manureFOMCsurface[i] = 0;
            manureHUMCsurface[i] = 0;
            manureBiocharCsurface[i] = 0;
            manureFOMCsubsurface[i] = 0;
            manureHUMCsubsurface[i] = 0;
            manureBiocharCsubsurface[i] = 0;
            manureFOMNsurface[i] = 0;
            manureHUMNsurface[i] = 0;
            manureTAN[i] = 0;
        }
        double NH3EmissionFactor=0;
        double totManureCapplied = 0;
        manure aManure;// = new manure() ;

        for (int i = 0; i < manureApplied.Count; i++)
        {
            double amountTotalN = manureApplied[i].getNamount() * area;
            totalManureNApplied += manureApplied[i].getNamount();
            int ManureType = manureApplied[i].getManureType();
            int speciesGroup = manureApplied[i].getspeciesGroup();
            string applicType = manureApplied[i].Applic_techniqueS;
            int applicationMonth = manureApplied[i].GetMonth_applied();

            if (lockit == false)
            {
                aManure = GlobalVars.Instance.theManureExchange.TakeManure(amountTotalN, lengthOfSequence, ManureType, speciesGroup);
                aManure.DivideManure(1 / area);
                manure anextraManure = new manure(aManure);
                if (theManureApplied == null)
                    theManureApplied = new List<manure>();
                theManureApplied.Add(anextraManure);
            }
            else
                aManure = theManureApplied[i];
            //parameters for ALFAM. Only used for liquid manures
            double airTemperature = GlobalVars.Instance.theZoneData.airTemp[applicationMonth];
            double manureDM = 5.0;
            double EFNH3TotalN = 0;//emission factor as proportion of total N
            double EFNH3TAN = 0;//emission factor as proportion of TAN
            int maxManure = 0;
            maxManure = GlobalVars.Instance.theZoneData.theFertManData.Count;
            bool gotit = false;
            for (int j = 0; j < maxManure; j++)
            {
                int tmpType = GlobalVars.Instance.theZoneData.theFertManData[j].manureType;  //type of manure storage
                int tmpSpecies = GlobalVars.Instance.theZoneData.theFertManData[j].speciesGroup;
                bool isManureTypeSame;
                if (ManureType == 1 && tmpType == 2)
                        isManureTypeSame= true;
                else if (ManureType == 2 && tmpType == 1)
                    isManureTypeSame = true;
                else if (ManureType == 3 && tmpType == 4)
                    isManureTypeSame = true;
                else if (ManureType == 4 && tmpType == 3)
                    isManureTypeSame = true;
                else if (ManureType == 6 && tmpType == 9)
                    isManureTypeSame = true;
                else if (ManureType == 9 && tmpType == 6)
                    isManureTypeSame = true;
                else if (ManureType == 7 && tmpType == 10)
                    isManureTypeSame = true;
                else if (ManureType == 10 && tmpType == 7)
                    isManureTypeSame = true;
                else if (ManureType == 8 && tmpType == 12)
                    isManureTypeSame = true;
                else if (ManureType == 12 && tmpType == 8)
                    isManureTypeSame = true;
                else if (ManureType == 13 && tmpType == 14)
                    isManureTypeSame = true;
                else if (ManureType == 14 && tmpType == 13)
                    isManureTypeSame = true;
                    else
                    isManureTypeSame = false;

                if (tmpType == ManureType||isManureTypeSame) //have found the correct manure type
                {
                    if ((tmpSpecies == speciesGroup) || (tmpType == 5))
                    {
                        switch (GlobalVars.Instance.getcurrentInventorySystem())
                        {
                            case 1://UNECE with modifications
                                ALFAM emissionEvent = new ALFAM();
                                switch (ManureType)
                                {
                                    case 1:
                                        emissionEvent.initialise(0, airTemperature, 2.0, speciesGroup, manureDM, 2.2, 25.0, 1, 148.0);
                                        EFNH3TAN = emissionEvent.ALFAM_volatilisation();
                                        break;
                                    case 2:
                                        emissionEvent.initialise(0, airTemperature, 2.0, speciesGroup, manureDM, 2.2, 25.0, 1, 148.0);
                                        EFNH3TAN = emissionEvent.ALFAM_volatilisation();
                                        break;
                                    case 3:
                                        EFNH3TotalN = GlobalVars.Instance.theZoneData.theFertManData[j].EFNH3FieldTier2;//Tier 2 EF is proportion of total N
                                        break;
                                    case 4:
                                        EFNH3TotalN = GlobalVars.Instance.theZoneData.theFertManData[j].EFNH3FieldTier2;//Tier 2 EF is proportion of total N
                                        break;
                                    case 5:
                                        emissionEvent.initialise(0, airTemperature, 2.0, speciesGroup, manureDM, 2.2, 25.0, 1, 148.0);
                                        EFNH3TAN = emissionEvent.ALFAM_volatilisation();
                                        break;
                                    case 6:
                                        emissionEvent.initialise(0, airTemperature, 2.0, speciesGroup, manureDM, 2.2, 25.0, 1, 148.0);
                                        EFNH3TAN = emissionEvent.ALFAM_volatilisation();
                                        break;
                                    case 7:
                                        EFNH3TotalN = GlobalVars.Instance.theZoneData.theFertManData[j].EFNH3FieldTier2;//Tier 2 EF is proportion of total N
                                        break;
                                    case 8:
                                        EFNH3TotalN = GlobalVars.Instance.theZoneData.theFertManData[j].EFNH3FieldTier2;//Tier 2 EF is proportion of total N
                                        break;
                                    case 9:
                                        emissionEvent.initialise(0, airTemperature, 2.0, speciesGroup, manureDM, 2.2, 25.0, 1, 148.0);
                                        EFNH3TAN = emissionEvent.ALFAM_volatilisation();
                                        break;
                                    case 10:
                                        EFNH3TotalN = GlobalVars.Instance.theZoneData.theFertManData[j].EFNH3FieldTier2;//Tier 2 EF is proportion of total N
                                        break;
                                    case 12:
                                        EFNH3TotalN = GlobalVars.Instance.theZoneData.theFertManData[j].EFNH3FieldTier2;//Tier 2 EF is proportion of total N
                                        break;
                                    case 13:
                                        EFNH3TotalN = GlobalVars.Instance.theZoneData.theFertManData[j].EFNH3FieldTier2;//Tier 2 EF is proportion of total N
                                        break;
                                    case 14:
                                        EFNH3TotalN = GlobalVars.Instance.theZoneData.theFertManData[j].EFNH3FieldTier2;//Tier 2 EF is proportion of total N
                                        break;
                                    default:
                                        break;

                                }
                                if (EFNH3TotalN > 0) //then need to convert EF based on total N to an EF based on TAN
                                {
                                    //Revised NH3 emission
                                    double temp = aManure.GetTotalN() / aManure.GetTAN();
                                    NH3EmissionFactor = EFNH3TotalN * temp;
                                    //end revised NH3 emission
                                }
                                else
                                    NH3EmissionFactor = EFNH3TAN;
                                gotit = true;
                                break;
                            case 2: //IPCC
                                EFNH3TotalN = GlobalVars.Instance.theZoneData.theFertManData[j].EFNH3FieldTier2;//Tier 2 EF is proportion of total N            NH3EmissionFactor = EFNH3TotalN;
                                NH3EmissionFactor = EFNH3TotalN;
                                gotit = true;
                                break;
                            case 3:
                                double refEFNH3 = GlobalVars.Instance.theZoneData.theFertManData[j].fertManNH3EmissionFactor;
                                double HousingRefTemp = GlobalVars.Instance.theZoneData.theFertManData[j].fertManNH3EmissionFactorHousingRefTemperature;
                                double actualTemp = GlobalVars.Instance.Temperature(GlobalVars.Instance.theZoneData.GetaverageAirTemperature(),
                                    0.0, manureApplied[i].GetdayOfApplication(), 0.0, GlobalVars.Instance.theZoneData.GetairtemperatureAmplitude(), GlobalVars.Instance.theZoneData.GetairtemperatureOffset());
                                double KHtheta = Math.Pow(10, -1.69 + 1447.7 / (actualTemp + GlobalVars.absoluteTemp));
                                double KHref = Math.Pow(10, -1.69 + 1447.7 / (HousingRefTemp + GlobalVars.absoluteTemp));
                                NH3EmissionFactor = (KHref / KHtheta) * refEFNH3;
                                gotit = true;
                                break;
                        }//end switch of inventory system
                    }
                }
            }
            if (!gotit)
            {
                string messageString = ("Error - unable to find ammonia emission factor for a field-applied manure\n");
                messageString += " Manure name " + aManure.Getname() + " ManureType = " + ManureType + " SpeciesGroup = " + speciesGroup + " \n";
                messageString += " Crop sequence name " + cropSequenceName + " \n";
                messageString += " Crop start year " + GetStartYear().ToString();
                GlobalVars.Instance.Error(messageString);
            }
            if (zeroGasEmissionsDebugging)
                NH3EmissionFactor = 0;

            double NH3ReductionFactor = 0;
            int maxApps = GlobalVars.Instance.theZoneData.themanureAppData.Count;
            gotit = false;
            for (int j = 0; j < maxApps; j++)
            {
                string tmpName = GlobalVars.Instance.theZoneData.themanureAppData[j].name;
                if (tmpName == applicType)
                {
                    NH3ReductionFactor = GlobalVars.Instance.theZoneData.themanureAppData[j].NH3EmissionReductionFactor;
                    gotit = true;
                    break;
                }
            }
            if (!gotit)
            {
                string messageString = ("Error - unable to find ammonia emission reduction factor for a manure or fertiliser application method\n");
                messageString += " Application method name " + applicType + " \n";
                messageString += " Crop sequence name " + cropSequenceName + " \n";
                messageString += " Crop start year " + GetStartYear().ToString();
                GlobalVars.Instance.Error(messageString);
            }
            double tmpNH3emission = 0.0;
            switch (GlobalVars.Instance.getcurrentInventorySystem())
            {
                case 1:
                    tmpNH3emission = NH3EmissionFactor * (1 - NH3ReductionFactor) * aManure.GetTAN();
                    break;
                case 2://IPCC EF, based on total N
                    tmpNH3emission = NH3EmissionFactor * (1 - NH3ReductionFactor) * aManure.GetTotalN();
                    break;
                case 3:
                    tmpNH3emission = NH3EmissionFactor * (1 - NH3ReductionFactor) * aManure.GetTAN();
                    break;
            }
            manureNH3emission += tmpNH3emission;
            int dayNo= (int) manureApplied[i].GetRelativeDay(getStartLongTime());
            if (tmpNH3emission <= aManure.GetTAN())
                aManure.SetTAN(aManure.GetTAN() - tmpNH3emission);
            else //this should rarely happen and only if using Tier 2 for solid manures
            {
                tmpNH3emission -= aManure.GetTAN();
                aManure.SetTAN(0.0);
                aManure.SetlabileOrganicN(aManure.GetlabileOrganicN() - tmpNH3emission);
            }
            manureTAN[dayNo] += aManure.GetTAN();
            manureFOMCsurface[dayNo] += aManure.GetdegC() + aManure.GetnonDegC();
            manureHUMCsurface[dayNo] += aManure.GethumicC();
            manureBiocharCsurface[dayNo] += aManure.GetBiocharC();
            manureFOMNsurface[dayNo] += aManure.GetlabileOrganicN();
            manureHUMNsurface[dayNo] += aManure.GethumicN();
            totManureCapplied += aManure.GetdegC() + aManure.GetnonDegC() + aManure.GethumicC();
            GlobalVars.Instance.log(manureFOMNsurface[dayNo].ToString(), 5);
        }
    }

    //public double GetLiqManureNH3Emission(
    //! A normal member, Calculate Fertiliser Input.
    /*!
     without return value.
    */
    public void CalculateFertiliserInput()
    {
        double fertiliserNin = 0;
        fertiliserNH3emission = 0;
        fertiliserNinput = 0;
        FileInformation cropInformation = new FileInformation(GlobalVars.Instance.getfertManFilePath());
        for (int i = 0; i < fertiliserApplied.Count; i++)
        {

            double Napplied = 0;
            if (fertiliserApplied[i].getName() != "Nitrification inhibitor")
                Napplied=fertiliserApplied[i].getNamount();

            fertiliserNinput += Napplied;
            string fertilizerName = fertiliserApplied[i].getName();
            cropInformation.setPath("AgroecologicalZone("+GlobalVars.Instance.GetZone().ToString()+").fertiliser");
            int max = 0;
            int min = 99;
            cropInformation.getSectionNumber(ref min, ref max);
            cropInformation.Identity.Add(min);
            bool found = false;
            for (int j = min; j <= max; j++)
            {
                cropInformation.Identity[1] = j;
                string fertManName = cropInformation.getItemString("Name");
                if (fertManName.CompareTo(fertilizerName) == 0)
                {
                    found = true;
                    break;
                }
            }
            if (found == false)
                GlobalVars.Instance.Error("Fertiliser not found in FertMan file for " + fertilizerName);
            cropInformation.PathNames.Add("Cconcentration");
            cropInformation.Identity.Add(-1);
            double Cconc = cropInformation.getItemDouble("Value");
            cropInformation.PathNames[2] = "Nconcentration";
            double Nconc = cropInformation.getItemDouble("Value");
            double amount = 0;
            if (Nconc > 0)
            {
                amount = Napplied / Nconc;
                fertiliserNin += Napplied;
                fertiliserC += amount * Cconc;
            }
            else
            {
                amount = fertiliserApplied[i].getCamount();
                fertiliserC += amount * 1000.0 * Cconc;

            }
            double NH3EmissionFactor = 0;

            int maxFert = 0;
            maxFert = GlobalVars.Instance.theZoneData.theFertManData.Count;
            found=false;
            for (int j = 0; j < maxFert; j++)
            {
                string tmpName = GlobalVars.Instance.theZoneData.theFertManData[j].name;             
                if (tmpName == fertilizerName)
                {
                    NH3EmissionFactor = GlobalVars.Instance.theZoneData.theFertManData[j].fertManNH3EmissionFactor;
                    found = true;
                    break;
                }
            }
            if (zeroGasEmissionsDebugging)
                NH3EmissionFactor = 0;

            if (found == false)
                GlobalVars.Instance.Error("NH3 not found in parameter file for " + fertilizerName);
            double tmpNH3emission = NH3EmissionFactor * Napplied;
            fertiliserNH3emission += tmpNH3emission;
            int applicDay = (int)(fertiliserApplied[i].GetDate().getLongTime() - theStartDate.getLongTime());
            if ((applicDay < 0) || (applicDay > duration))
            {
                string messageString = ("Error - Fertiliser applied outside crop period\n");
                messageString += " Crop sequence name " + cropSequenceName + " \n";
                messageString += " Crop number " + identity + " \n";
                messageString += " Crop start year " + GetStartYear().ToString();
                GlobalVars.Instance.Error(messageString);
            }
            //Napplied now refers to N entering soil
            fertiliserN[applicDay]+= Napplied - tmpNH3emission; //this is what enters the soil
            if (fertilizerName == "Nitrification inhibitor")
            {
                if (Napplied > 1.0)
                {
                    string messageString = ("Error - nitrification inhibitor efficiency must be <=1.0\n");
                    messageString += " Crop sequence name " + cropSequenceName + " \n";
                    messageString += " Crop number " + identity + " \n";
                    messageString += " Crop start year " + GetStartYear().ToString();
                    GlobalVars.Instance.Error(messageString);
                }
                else
                    nitrificationInhibitor[applicDay] = fertiliserApplied[i].getNamount();
            }

        }        
    }
    //! A normal member, Calculate Nitrification Inhibitor. Returning a double value.
    /*!
     \return a double value.
    */
    public double CalculateNitrificationInhibitor()
    {
        double retVal = 0;
        for (int i = 0; i < duration; i++)
        {
            if (i > 0)
            {
                if (nitrificationInhibitor[i] < nitrificationInhibitor[i - 1])  //no new inhibitor applied
                {
                    double temp = temperature[i];
                    double degRate = Math.Log(2) / (168 * Math.Exp(-0.084 * temp));//from F.M. Kelliher a,b,*, T.J. Clough b, H. Clark c, G. Rys d, J.R. Sedcole b
                    nitrificationInhibitor[i] = nitrificationInhibitor[i - 1] * Math.Exp(-degRate);
                    if (nitrificationInhibitor[i] < 0.000001)
                        nitrificationInhibitor[i] = 0;
                }
            }
            retVal += nitrificationInhibitor[i];
        }
        retVal /= duration;
        return retVal;
    }
    //! A normal member, Do Crop Inputs. Taking one argument.
    /*!
     \param lockit, a boolean argument.
    */
    public void DoCropInputs(bool lockit)
    {
        if (!lockit)
        {
            CalculateExcretaCInput();
            CalculateExcretaNInput();
            CalculateManureInput(lockit);
        }
        if (Getname() != "Bare soil")
        {
            CalculateCFixed();
            CalculateCropResidues();
            CalculateCropResidueBurning();
        }
        CalculateHarvestedYields();
    }
    //! A normal member, Handle Bare Soil Residues. Taking one argument.
    /*!
     \param someResidueFromPrevious, one instance argutment that points to GlobalVar.product.
    */
    public void HandleBareSoilResidues(GlobalVars.product someResidueFromPrevious)
    {
        if ((Getname() == "Bare soil") && (someResidueFromPrevious!=null))
        {
            double DMburnt = 0;
            double grazedDM = 0;
            residueFromPrevious = new GlobalVars.product(someResidueFromPrevious);
            //This is to keep 
            //residueFromPrevious.SetExpectedYield(residueFromPrevious.GetModelled_yield();
            //retrieve the DM in the residue from the previous crop
            double remainingResidueDM = residueFromPrevious.GetModelled_yield();
            if (remainingResidueDM > 0)
            {
                if (residueFromPrevious.GetGrazed_yield() > 0)
                {
                    residueFromPrevious.composition.SetisGrazed(true);
                    grazedDM = residueFromPrevious.GetGrazed_yield();
                    harvestedC = residueFromPrevious.GetGrazed_yield() * residueFromPrevious.composition.GetC_conc();
                    grazedC = harvestedC;
                    harvestedN += residueFromPrevious.GetGrazed_yield() * residueFromPrevious.composition.GetN_conc();
                    grazedN += harvestedN;
                    harvestedDM += grazedDM;
                    GlobalVars.product aProduct = new GlobalVars.product(residueFromPrevious);
                    //convert to yield from yield per ha
                    aProduct.composition.Setamount((aProduct.GetModelled_yield() - aProduct.GetGrazed_yield())*area);
                    GlobalVars.Instance.AddGrazableProductUnutilised(aProduct.composition);
                }
                remainingResidueDM -= grazedDM;
                unutilisedGrazableDM += remainingResidueDM;
                unutilisedGrazableC += remainingResidueDM * someResidueFromPrevious.composition.GetC_conc();
                unutilisedGrazableN += remainingResidueDM * someResidueFromPrevious.composition.GetN_conc();

                if (residueFromPrevious.Harvested.Contains("burnt")) //burn the DM remaining after grazing of residues
                {
                    DMburnt = remainingResidueDM;
                    if (DMburnt >= 0)
                    {
                        burntResidueC = DMburnt * residueFromPrevious.composition.GetC_conc();
                        burntResidueN = DMburnt * residueFromPrevious.composition.GetN_conc();
                        surfaceResidueN = 0.0;
                        surfaceResidueC = 0.0;
                    }
                }
                else if (residueFromPrevious.Harvested.Contains("harvested")) //
                {
                    harvestedC += remainingResidueDM * residueFromPrevious.composition.GetC_conc();
                    harvestedN += remainingResidueDM * residueFromPrevious.composition.GetN_conc();
                   // theProducts[1].SetExpectedYield(remainingResidueDM);//otherwise the residues will be harvested with zero mass
                    string messageString = ("Error - attempt to harvest crop residue on bare soil - function not implemented\n");
                    messageString += " Crop sequence name " + cropSequenceName + " \n";
                    messageString += " Crop number " + identity + " \n";
                    messageString += " Crop start year " + GetStartYear().ToString();
                    GlobalVars.Instance.Error(messageString);
                }
                else
                //return the residue to the soil surface
                {
                    surfaceResidueC = remainingResidueDM * residueFromPrevious.composition.GetC_conc();
                    surfaceResidueN = remainingResidueDM * residueFromPrevious.composition.GetN_conc();
                }
                /*                    else if (theProducts[2].Harvested == "Incorporated")
                                    {
                                    }*/

            }
            residueFromPrevious.composition.Setamount(grazedDM);//so this is logged as produced
            burningBlackC = DMburnt * GlobalVars.Instance.theZoneData.GetburntResidueBlackCEmissionFactor();
            burningCOC = DMburnt * GlobalVars.Instance.theZoneData.GetburntResidueCOEmissionFactor();
            burningCO2C = burntResidueC - (burningBlackC + burningCOC);

            burningN2ON = DMburnt * GlobalVars.Instance.theZoneData.GetburntResidueN2OEmissionFactor();
            burningNH3N = DMburnt * GlobalVars.Instance.theZoneData.GetburntResidueNH3EmissionFactor();
            burningNOxN = DMburnt * GlobalVars.Instance.theZoneData.GetburntResidueNOxEmissionFactor();
            burningOtherN = burntResidueN - (burningN2ON + burningNH3N + burningNOxN);
           // theProducts.Clear();
        }

    }
    //! A normal member, Get Manure OrgN. Returning a double value.
    /*!
     \return a double value.
    */
    public double GetmanureOrgN()
    {
        double retVal = 0;
        for (int i = 0; i < manureFOMNsurface.Length; i++)
            retVal += manureFOMNsurface[i];
        for (int i = 0; i < manureHUMNsurface.Length; i++)
            retVal += manureHUMNsurface[i];
        return retVal;
    }
    //! A normal member, Get Manure TAN. Returning a double value.
    /*!
     \return a double value.
    */
    public double GetmanureTAN()
    {
        double retVal = 0;
        for (int i = 0; i < manureTAN.Length; i++)
            retVal += manureTAN[i];
        return retVal;
    }
    //! A normal member, Get N fixation. Taking one argument and  Returning a double value.
    /*!
     \param deficit, a double argument.
     \return a double value.
    */
    public double GetNfixation(double deficit)
    {
        double retVal = 0;
        if ((deficit>0)&&(NfixationFactor>=0))
            retVal = deficit * NfixationFactor;
        return retVal;
    }

    //Jonas - this function is messy. It is called CalcAvailableN but also has carbon in it too. Needs to be tidied
    //! A normal member, Calculate Available N. Taking three arguments.
    /*!
     \param surplusMineralN, one double argument.
     \param thesoilNMineralisation, one double argument.
     \param relGrowth, oen double argument.
    */
    public void CalcAvailableN(ref double surplusMineralN, double thesoilNMineralisation, ref double relGrowth)
    {
        mineralNFromLastCrop = surplusMineralN;
        soilNMineralisation = thesoilNMineralisation;
        double manureOrgN = GetmanureOrgN();
        double totmanureTAN = GetmanureTAN();
        double soilNSupply = mineralNFromLastCrop + soilNMineralisation;
        
        nAtm = (NDepositionRate/365) * (getEndLongTime() - getStartLongTime() + 1);
        //note tha N2O emissions are currently calculated after removal of NH3 emissions and without humic N
        double evenNSupply = urineNasFertilizer + soilNMineralisation + nAtm - urineNH3emission;
        relGrowth = 0;
        CalculateLeachingAndUptake(mineralNFromLastCrop, evenNSupply, ref relGrowth, ref surplusMineralN);//calculate leaching and modelled crop uptake

        GlobalVars.Instance.log("Crop " + name + " rel growth " + relGrowth.ToString("0.00") + " Nfix " + Nfixed.ToString("0.00") + " soil min " + soilNMineralisation.ToString("0.00")
            + " fromlast " + mineralNFromLastCrop.ToString("0.00") + " surplus " +surplusMineralN.ToString("0.00") + " leaching " + nitrateLeaching.ToString("0.00")
            + " avail " + mineralNavailable.ToString("0.00"), 5);
        //GlobalVars.Instance.log("Manure NH3N " + manureNH3emission.ToString());
    }
    //! A normal member, Get Grazed FeedItems.
    /*!
     without a return.
    */
    public void getGrazedFeedItems()
    {
        for (int i = 0; i < theProducts.Count; i++)
        {
            if ((theProducts[i].composition.GetisGrazed())||(theProducts[i].Harvested.Contains("Residue")))
            {
                int feedCode = theProducts[i].composition.GetFeedCode();
                GlobalVars.Instance.grazedArray[feedCode].fieldDMgrazed += theProducts[i].Grazed_yield * area/lengthOfSequence;
                GlobalVars.Instance.grazedArray[feedCode].name = theProducts[i].composition.GetName();
            }
        }
    }
    //! A normal member, Get DM Yield. Returning a double value.
    /*!
     \return a double value.
    */
    public double GetDMYield()
    {
        double DMYield = 0;
        for (int i = 0; i < theProducts.Count; i++)
        {
            DMYield += theProducts[i].GetModelled_yield();
        }
        return DMYield;
    }
    //! A normal member, expect. Returning a boolean value.
    /*!
     \return a boolean value.
    */
    public bool expect()
    {
        int numberOfMatching = 0;
        for (int i = 0; i < theProducts.Count; i++)
        {
            if (theProducts[i].GetModelled_yield() > 0)
            {
                double diff = theProducts[i].GetModelled_yield() - theProducts[i].GetExpectedYield();
                double relative_diff = System.Math.Abs(diff / theProducts[i].GetModelled_yield());
                double threshold = 0.1 * GlobalVars.Instance.getmaxToleratedErrorYield();
                if (relative_diff < threshold)
                    numberOfMatching++;
            }
            else
            {
                theProducts[i].SetExpectedYield(0);
                theProducts[i].SetModelled_yield(0);
                numberOfMatching++;
            }
        }
        if (numberOfMatching == theProducts.Count)
        {
            if (mineralNavailable < 0.0)
            {
                string messageString = ("Error - insufficient mineral N available to satisfy immobilisation in soil\n");
                messageString += " Crop sequence name " + cropSequenceName + " \n";
                messageString += " Crop number " + identity + " \n";
                messageString += " Crop start year " + GetStartYear().ToString();
                GlobalVars.Instance.Error(messageString);
                return false;
            }
            else
                return true;
        }
        else
            return false;
    }
    //! A normal member, Calculate Available N. Taking three arguments and Returning a boolean value.
    /*!
     \param surplusMineralN, one double argument.
     \param relGrowth, oen double argument.
     \param final, one boolean argument.
     \return a boolean value.
    */
    public bool CalcModelledYield(ref double surplusMineralN, double relGrowth, bool final)
    {
        bool retVal = false;
        if (theProducts.Count > 4)
        {
           string messageString=("Error - too many products in crop");
           GlobalVars.Instance.Error(messageString);
        }

        if (Getname() != "Bare soil")
        {
            GlobalVars.Instance.log("relGrowth " + relGrowth.ToString(), 5);

            for (int i = 0; i < theProducts.Count; i++)
            {
                if (theProducts[i].GetGrazed_yield() > theProducts[i].GetPotential_yield())
                {
                    string messageString = ("Error - grazed yield " + theProducts[i].GetGrazed_yield().ToString() +
                            " required is greater than the potential yield " + theProducts[i].GetwaterLimited_yield().ToString());
                    GlobalVars.Instance.Error(messageString);
                }

                theProducts[i].SetModelled_yield(theProducts[i].GetPotential_yield() * relGrowth);
                GlobalVars.Instance.log("expected yield " + theProducts[i].Modelled_yield.ToString(), 5);
            }
            if (expect())
            {
                for (int i = 0; i < theProducts.Count; i++)
                {
                    bool residueGrazed = false;
                    if (theProducts[i].Harvested.Contains("Residue"))
                        residueGrazed = true;
                    if (((theProducts[i].composition.GetisGrazed())|| (residueGrazed)) && (GlobalVars.Instance.GetstrictGrazing()))
                    {
                        double diff_grazed = theProducts[i].GetModelled_yield() - theProducts[i].Grazed_yield;
                        FileInformation constantFile = new FileInformation(GlobalVars.Instance.getConstantFilePath());
                        constantFile.setPath("constants(0).absoluteGrazedDMtolerance(-1)");
                        double absoluteGrazedDMtolerance = constantFile.getItemDouble("Value");
                        if (Math.Abs(diff_grazed) > absoluteGrazedDMtolerance)
                        {
                            double rel_diff_grazed = 0;
                            if (theProducts[i].Grazed_yield > 0)
                                rel_diff_grazed = diff_grazed / theProducts[i].Grazed_yield;
                            double tolerance = GlobalVars.Instance.getmaxToleratedErrorGrazing();
                            if ((rel_diff_grazed < 0.0) && (Math.Abs(rel_diff_grazed) > tolerance))
                            {
                                WritePlantFile(0, 0, 0);
                                string messageString = ("Error - modelled production lower than required production for grazed feed item \n");
                                messageString += " Modelled yield " + theProducts[i].GetModelled_yield().ToString() + " Required yield " + theProducts[i].Grazed_yield.ToString();
                                messageString += " Crop sequence name " + cropSequenceName + " \n";
                                messageString += "Crop number " + identity + " \n";
                                messageString += "Crop product = " + theProducts[i].composition.GetName() + " \n";
                                messageString += "Crop start year " + GetStartYear().ToString();
                                GlobalVars.Instance.Error(messageString);
                            }
                        }
                    }
                    theProducts[i].SetExpectedYield(theProducts[i].GetModelled_yield());
                }
                cropNuptake = CalculateCropNUptake();
                mineralNToNextCrop = surplusMineralN;
                //DoCropInputs(true);
                retVal = true;
            }
            else
            {
                for (int i = 0; i < theProducts.Count; i++)
                {
                    double newExpectedYield = 0;
                    double diff = theProducts[i].GetModelled_yield() - theProducts[i].GetExpectedYield();
                    if (diff < 0)
                        newExpectedYield = theProducts[i].GetExpectedYield() + diff / 2;
                    else
                        newExpectedYield = theProducts[i].GetExpectedYield() - diff / 2;
                    if (newExpectedYield <= 0.0)
                    {
                        newExpectedYield = 0.0001;
                        retVal = true;
                    }
                    theProducts[i].SetExpectedYield(newExpectedYield);
                }
            }
        }
        else  //bare soil
        {
            mineralNToNextCrop = surplusMineralN;
            retVal = true;
        }
    return retVal;
}
    //! A normal member, Check Yields. Taking one argument and Returning an integer value.
    /*!
     \param RotationName, one string argument.
     \return an integer value.
    */
    public int CheckYields(string RotationName)
    {
        if (residueFromPrevious != null)
        {
            residueFromPrevious.composition.Setamount(residueFromPrevious.composition.Getamount() * area / lengthOfSequence);
//            double temp = residueFromPrevious.composition.Getamount() * theProducts[i].composition.GetN_conc();
            GlobalVars.Instance.AddProductProduced(residueFromPrevious.composition);
            //reset value to per ha basis (for debugging)
            residueFromPrevious.composition.Setamount(residueFromPrevious.composition.Getamount() / area);
        }
        else
        {
            for (int i = 0; i < theProducts.Count; i++)
            {
                if (theProducts[i].GetModelled_yield() == 0)
                {
                    string messageString = ("Error - modelled yield is zero\n");
                    messageString += ("Rotation name = " + RotationName + "\n");
                    messageString += ("Crop product = " + theProducts[i].composition.GetName());
                    GlobalVars.Instance.Error(messageString);
                }
                else
                {
                    double expected = theProducts[i].Expected_yield;
                    double modelled = theProducts[i].GetModelled_yield();
                    if (Double.IsNaN(modelled)) //this should never happen..
                    {
                        string messageString = "Error; modelled yield has not been calculated\n";
                        messageString += "Rotation name = " + RotationName + "\n";
                        messageString += "Crop product = " + theProducts[i].composition.GetName() + "\n";
                        messageString += "Crop start year " + GetStartYear().ToString();
                        GlobalVars.Instance.Error(messageString);
                    }

                    double diff = (modelled - expected) / modelled;
                    double tolerance = GlobalVars.Instance.getmaxToleratedErrorYield();
                    if (Math.Abs(diff) > tolerance)
                    {
                        double errorPercent = 100 * diff;
                        string messageString;
                        if (diff > 0)
                            messageString = "Error; modelled yield exceeds expected yield by more than the permitted margin of "
                            + tolerance.ToString() + "\n";
                        else
                            messageString = "Error; expected yield exceeds modelled yield by more than the permitted margin"
                            + tolerance.ToString() +"\n";
                        if (errorPercent < 0)
                            errorPercent *= -1.0;
                        messageString += "Rotation name = " + RotationName + "\n";
                        messageString += "Crop product = " + theProducts[i].composition.GetName() + "\n";
                        messageString += "Crop start year " + GetStartYear().ToString() + "\n";
                        messageString += "Percentage error = " + errorPercent.ToString("0.00") + "%" + "\n";
                        messageString += "Expected yield= " + expected.ToString() + " Modelled yield= " + modelled.ToString() + "\n";
                        Write();

                        GlobalVars.Instance.Error(messageString);

                    }
                    else
                    {
                        //accept the modelled yield as valid and add to allFeedAndProductsProduced
                        double productProcessingLossFactor = theProducts[i].composition.GetStoreProcessFactor();
                        if ((theProducts[i].Harvested == "Harvested") || ((theProducts[i].composition.GetisGrazed()))
                            || (theProducts[i].Harvested == "Burnt stubble"))
                        //                        || (theProducts[i].Harvested == "Residue") || (theProducts[i].Harvested == "Stubbleburning"))
                        {
                            if (!theProducts[i].composition.GetisGrazed())
                            {
                                theProducts[i].composition.Setamount(theProducts[i].GetExpectedYield());
                                double originalC = theProducts[i].composition.Getamount() * theProducts[i].composition.GetC_conc();
                                double tempCLoss = productProcessingLossFactor * originalC;
                                storageProcessingCLoss += tempCLoss;
                                double originalN = theProducts[i].composition.Getamount() * theProducts[i].composition.GetN_conc();
                                double tempNLoss = productProcessingLossFactor * originalN;
                                storageProcessingNLoss += tempNLoss;
                                theProducts[i].composition.AdjustAmount(1 - productProcessingLossFactor);
                                theProducts[i].composition.SetC_conc((originalC - tempCLoss) / theProducts[i].composition.Getamount());
                                double temp2 = theProducts[i].composition.GetC_conc() * theProducts[i].composition.Getamount();
                            }
                            else
                                theProducts[i].composition.Setamount(theProducts[i].GetGrazed_yield());
                            //multiply by crop area to obtain yield from yield per ha
                            theProducts[i].composition.Setamount(theProducts[i].composition.Getamount() * area / lengthOfSequence);
                            double temp = theProducts[i].composition.Getamount() * theProducts[i].composition.GetN_conc();
                            GlobalVars.Instance.AddProductProduced(theProducts[i].composition);
                            if (theProducts[i].GetGrazed_yield() > 0)
                            {
                                GlobalVars.product aProduct = new GlobalVars.product(theProducts[i]);
                                aProduct.composition.Setamount(aProduct.GetModelled_yield() - aProduct.GetGrazed_yield());
                                GlobalVars.Instance.AddGrazableProductUnutilised(aProduct.composition);
                            }
                                
                            //reset yield value back to per ha basis (for debugging)
                            theProducts[i].composition.Setamount(theProducts[i].composition.Getamount() / area);
                        }
                    }
                }
            }
        }
        return 0;
    }
    //! A normal member, Adjust Dates. Taking one argument.
    /*!
     \param firstYear, one integer argument.
    */
    public void AdjustDates(int firstYear)
    {
        //Console.WriteLine(" init start yr " + GetStartYear().ToString() + " init end " + GetEndYear().ToString());
        SetEndYear(GetEndYear() - firstYear + 1);
        SetStartYear(GetStartYear()-firstYear+1);
        //Console.WriteLine(" fin start yr " + GetStartYear().ToString() + " fin end " + GetEndYear().ToString());

        for (int i = 0; i < fertiliserApplied.Count; i++)
        {
            int monthOfApplication;
            int yearOfApplication;
            if (fertiliserApplied[i].GetMonth_applied() < GetStartMonth())
            {
                monthOfApplication = fertiliserApplied[i].GetMonth_applied() + 12 - GetStartMonth();
                yearOfApplication = GetEndYear();
            }
            else
            {
                monthOfApplication = fertiliserApplied[i].GetMonth_applied() - GetStartMonth();
                yearOfApplication = GetStartYear();
            }
            fertiliserApplied[i].SetdayOfApplication((int)Math.Round(monthOfApplication * 30.416 + 15));
            if (fertiliserApplied[i].GetdayOfApplication() > duration)
                fertiliserApplied[i].SetdayOfApplication((int)duration);
            fertiliserApplied[i].SetapplicDate(fertiliserApplied[i].GetdayOfApplication(), fertiliserApplied[i].GetMonth_applied(), yearOfApplication);
        }
        for (int i = 0; i < manureApplied.Count; i++)
        {
            int monthOfApplication;
            int yearOfApplication;
            if (manureApplied[i].GetMonth_applied() < GetStartMonth())
            {
                monthOfApplication = manureApplied[i].GetMonth_applied() + 12 - GetStartMonth();
                yearOfApplication = GetEndYear();
            }
            else
            {
                monthOfApplication = manureApplied[i].GetMonth_applied() - GetStartMonth();
                yearOfApplication = GetStartYear();
            }
            manureApplied[i].SetdayOfApplication((int)Math.Round(monthOfApplication * 30.416 + 15));
            if (manureApplied[i].GetdayOfApplication() > duration)
                manureApplied[i].SetdayOfApplication((int)duration);
            manureApplied[i].SetapplicDate(manureApplied[i].GetdayOfApplication(), manureApplied[i].GetMonth_applied(), yearOfApplication);
        }
    }
    //! A normal member, Check Crop C Balance. Taking twi arguments and returning a boolean value.
    /*!
     \param rotationName, one string argument.
     \param cropNo, one integer argument.
     \return a boolean value.
    */
    public bool CheckCropCBalance(string rotationName, int cropNo)
    {
        bool retVal = false;
        double manureCinput = GetManureC();
        double Cinp = CFixed;
        if (residueFromPrevious != null)
        {
            residueCfromLastCrop = residueFromPrevious.GetModelled_yield() * residueFromPrevious.composition.GetC_conc();
            Cinp += residueCfromLastCrop;
        }
        if (residueToNext != null)
            residueCtoNextCrop = residueToNext.GetModelled_yield() * residueToNext.composition.GetC_conc();
        double Cout = surfaceResidueC + subsurfaceResidueC + harvestedC + burntResidueC + residueCtoNextCrop;
        double Cbalance = Cinp - Cout;
        if (Cinp != 0) //cropped or bare soil with crop residues
        {
            double diff = Cbalance / Cinp;
            double tolerance = GlobalVars.Instance.getmaxToleratedErrorYield();
            if (Math.Abs(diff) > tolerance)
            {
                double errorPercent = 100 * diff;
                string messageString=("Error; Crop C balance error is more than the permitted margin of "
                        + tolerance.ToString() +"\n");
                messageString+=("Crop name " + name+"\n");
                messageString+=("Percentage error = " + errorPercent.ToString("0.00") + "%");
                GlobalVars.Instance.Error(messageString);
            }
            else
                return true;
        }
        else
            retVal = true;
        return retVal;
    }
    //! A normal member, Check Crop N Balance. Taking two arguments.
    /*!
     \param rotationName, one string argument.
     \param cropNo, one integer argument.
    */
    public void CheckCropNBalance(string rotationName, int cropNo)
    {
        double Ninp = 0;
        if (Getname() != "Bare soil")
            Ninp = CalculateCropNUptake();
        else
        {
            if (residueFromPrevious != null)
            {
                residueNfromLastCrop = residueFromPrevious.GetModelled_yield() * residueFromPrevious.composition.GetN_conc();
                Ninp += residueNfromLastCrop;
            }
        }
        if (residueToNext!=null)
            residueNtoNextCrop = residueToNext.GetModelled_yield() * residueToNext.composition.GetN_conc();
        double Nout = surfaceResidueN + subsurfaceResidueN + harvestedN + burntResidueN + residueNtoNextCrop;
        double Nbalance = Ninp - Nout;
       // GlobalVars.Instance.log("crop " + name + " Ninp " + Ninp.ToString() + " Nsurface " + surfaceResidueN.ToString() + " Nsubsurf " + subsurfaceResidueN.ToString() +
         //   " harvestedN" + harvestedN.ToString());
        if (Ninp != 0) //not bare soil
        {
            double diff = Nbalance / Ninp;
            double tolerance = GlobalVars.Instance.getmaxToleratedErrorYield();
            if (Math.Abs(diff) > tolerance)
            {               
                    double errorPercent = 100 * diff;                 
                    string messageString=("Error; Crop N balance error is more than the permitted margin of "
                        + tolerance.ToString() +"\n");
                    messageString+=("Crop name " + name+"\n");
                    messageString+=("Percentage error = " + errorPercent.ToString("0.00") + "%");
                    GlobalVars.Instance.Error(messageString);
           }
        }
    }
    //! A normal member, Calculate Duration. Returning one double value.
    /*!
     \return a double value.
    */
    public double  CalcDuration()
    {
        return duration = theEndDate.getLongTime() - theStartDate.getLongTime() + 1;
    }
    //! A normal member, Write.
    /*!
     without returning value.
    */
    public void Write()
    {
        GlobalVars.Instance.writeStartTab("CropClass");
        
            GlobalVars.Instance.writeInformationToFiles("Identity", "Identity", "-", identity, Parens);
            GlobalVars.Instance.writeInformationToFiles("name", "Crop name", "-", name, Parens);
            GlobalVars.Instance.writeInformationToFiles("area", "Area", "ha", area, Parens);
            GlobalVars.Instance.writeStartTab("theStartDate");
            theStartDate.Write();
            GlobalVars.Instance.writeEndTab();
            GlobalVars.Instance.writeStartTab("theEndDate");
            theEndDate.Write();
            GlobalVars.Instance.writeEndTab();
            GlobalVars.Instance.writeInformationToFiles("isIrrigated", "Is irrigated", "-", isIrrigated, Parens);
            GlobalVars.Instance.writeInformationToFiles("fertiliserN2OEmission", "N2O emission from fertiliser", "kgN/ha", fertiliserN2OEmission, Parens);
            GlobalVars.Instance.writeInformationToFiles("cropResidueN2O", "N2O emission from crop residues", "kgN/ha", cropResidueN2O, Parens);
            GlobalVars.Instance.writeInformationToFiles("soilN2OEmission", "N2O emission from mineralised N", "kgN/ha", soilN2OEmission, Parens);
            GlobalVars.Instance.writeInformationToFiles("unutilisedGrazableDM", "Unutilised grazable DM", "kg DM/ha", unutilisedGrazableDM, Parens);

            //        GlobalVars.Instance.writeInformationToFiles("NyieldMax", "??", "??", NyieldMax);
            //potential and water limited yield

            GlobalVars.Instance.writeInformationToFiles("CFixed", "C fixed", "kgC/ha", CFixed, Parens);
            GlobalVars.Instance.writeInformationToFiles("residueCfromLastCrop", "C in residues from previous crop", "kgC/ha", residueCfromLastCrop, Parens);
            GlobalVars.Instance.writeInformationToFiles("surfaceResidueC", "C in surface residues", "kgC/ha", surfaceResidueC, Parens);
            GlobalVars.Instance.writeInformationToFiles("subsurfaceResidueC", "C in subsurface residues", "kgC/ha", subsurfaceResidueC, Parens);

            GlobalVars.Instance.writeInformationToFiles("urineCCropClass", "C in urine", "kgC/ha", urineC, Parens);
            double amount=0;
            for(int i=0;i<manureFOMCsurface.Length;i++)
            {
                amount+=manureFOMCsurface[i];
            }
            GlobalVars.Instance.writeInformationToFiles("manureFOMCsurface", "manureFOMCsurface", "kgC/ha", amount, Parens);
            GlobalVars.Instance.writeInformationToFiles("faecalCCropClass", "C in faeces", "kgC/ha", faecalC, Parens);
            GlobalVars.Instance.writeInformationToFiles("storageProcessingCLoss", "C lost during processing or storage", "kgC/ha", storageProcessingCLoss, Parens);
            GlobalVars.Instance.writeInformationToFiles("fertiliserC", "Emission of CO2 from fertiliser", "kgC/ha", fertiliserC, Parens);
            GlobalVars.Instance.writeInformationToFiles("harvestedC", "Harvested C", "kgC/ha", harvestedC, Parens);
            GlobalVars.Instance.writeInformationToFiles("harvestedDM", "Harvested DM", "kg DM/ha", harvestedDM, Parens);
            GlobalVars.Instance.writeInformationToFiles("burntResidueC", "C in burned crop residues", "kg/ha", burntResidueC, Parens);
            GlobalVars.Instance.writeInformationToFiles("unutilisedGrazableC", "C in unutilised grazable DM", "kg/ha", unutilisedGrazableC, Parens);
            GlobalVars.Instance.writeInformationToFiles("residueCtoNextCrop", "C in residues passed to next crop", "kgC/ha", residueCtoNextCrop, Parens);

            GlobalVars.Instance.writeInformationToFiles("NyieldMax", "Maximum N yield", "kgN/ha", NyieldMax, Parens);
            GlobalVars.Instance.writeInformationToFiles("NavailFact", "N availability factor", "-", NavailFact, Parens);
            GlobalVars.Instance.writeInformationToFiles("maxCropNuptake", "Maximum crop N uptake", "kgN/ha", maxCropNuptake, Parens);
            GlobalVars.Instance.writeInformationToFiles("cropNuptake", "Crop N uptake", "kgN/ha", cropNuptake, Parens);
            GlobalVars.Instance.writeInformationToFiles("Nfixed", "N fixed", "kgN/ha", Nfixed, Parens);
            GlobalVars.Instance.writeInformationToFiles("nAtm", "N from atmospheric deposition", "kgN/ha", nAtm, Parens);
            GlobalVars.Instance.writeInformationToFiles("fertiliserNinput", "Input of N in fertiliser", "kgN/ha", fertiliserNinput, Parens);
            GlobalVars.Instance.writeInformationToFiles("excretaNInput", "Input of N in excreta", "kgN/ha", excretaNInput, Parens);
            GlobalVars.Instance.writeInformationToFiles("totalManureNApplied", "Total N applied in manure", "kgN/ha", totalManureNApplied, Parens);
            GlobalVars.Instance.writeInformationToFiles("mineralNFromLastCrop", "N2O emission from mineralised N", "kgN/ha", mineralNFromLastCrop, Parens);
            GlobalVars.Instance.writeInformationToFiles("residueNfromLastCrop", "N in residues from previous crop", "kgN/ha", residueNfromLastCrop, Parens);
            GlobalVars.Instance.writeInformationToFiles("manureNH3emission", "NH3-N from manure application", "kgN/ha", manureNH3emission, Parens);
            GlobalVars.Instance.writeInformationToFiles("surfaceResidueN", "N in surface residues", "kg/ha", surfaceResidueN, Parens);
            GlobalVars.Instance.writeInformationToFiles("subsurfaceResidueN", "N in subsurface residues", "kgN/ha", subsurfaceResidueN, Parens);

            GlobalVars.Instance.writeInformationToFiles("surfaceResidueDM", "Surface residue dry matter", "kg/ha", surfaceResidueDM, Parens);
            GlobalVars.Instance.writeInformationToFiles("fertiliserNH3emission", "NH3-N from fertiliser application", "kgN/ha", fertiliserNH3emission, Parens);
            GlobalVars.Instance.writeInformationToFiles("urineNH3emission", "NH3-N from urine deposition", "kgN/ha", urineNH3emission, Parens);
            GlobalVars.Instance.writeInformationToFiles("harvestedN", "N harvested (N yield)", "kgN/ha", harvestedN, Parens);
            GlobalVars.Instance.writeInformationToFiles("grazedN", "N grazed", "kgN/ha", grazedN, Parens);
            GlobalVars.Instance.writeInformationToFiles("storageProcessingNLoss", "N2 emission during product processing/storage", "kgN/ha", storageProcessingNLoss, Parens);
            GlobalVars.Instance.writeInformationToFiles("residueNtoNextCrop", "N in residues passed to next crop", "kgN/ha", residueNtoNextCrop, Parens);
            GlobalVars.Instance.writeInformationToFiles("N2Nemission", "N2 emission", "kgN/ha", N2Nemission, Parens);
            GlobalVars.Instance.writeInformationToFiles("urineNCropClass", "Urine N", "kgN/ha", urineNasFertilizer, Parens);
            GlobalVars.Instance.writeInformationToFiles("faecalNCropClass", "Faecal N", "kgN/ha", faecalN, Parens);
            GlobalVars.Instance.writeInformationToFiles("burningN2ON", "N2O emission from burned crop residues", "kgN/ha", burningN2ON, Parens);
            GlobalVars.Instance.writeInformationToFiles("burningNH3N", "NH3 emission from burned crop residues", "kgN/ha", burningNH3N, Parens);
            GlobalVars.Instance.writeInformationToFiles("burningNOxN", "NOx emission from burned crop residues", "kgN/ha", burningNOxN, Parens);
            GlobalVars.Instance.writeInformationToFiles("burningOtherN", "N2 emission from burned crop residues", "kgN/ha", burningOtherN, Parens);
            GlobalVars.Instance.writeInformationToFiles("OrganicNLeached", "Leached organic N", "kgN/ha", OrganicNLeached, Parens);
            GlobalVars.Instance.writeInformationToFiles("mineralNToNextCrop", "Mineral N to next crop", "kgN/ha", mineralNToNextCrop, Parens);
            GlobalVars.Instance.writeInformationToFiles("fertiliserN2OEmission", "N2O emission from fertiliser N", "kgN/ha", fertiliserN2OEmission, Parens);
            GlobalVars.Instance.writeInformationToFiles("manureN2OEmission", "N2O emission from manure N", "kgN/ha", manureN2OEmission, Parens);
            GlobalVars.Instance.writeInformationToFiles("cropResidueN2O", "N2O emission from crop residue N", "kgN/ha", cropResidueN2O, Parens);
            GlobalVars.Instance.writeInformationToFiles("soilN2OEmission", "N2O emission from mineralised N", "kgN/ha", soilN2OEmission, Parens);
            GlobalVars.Instance.writeInformationToFiles("N2ONemission", "N2O emission", "kgN/ha", N2ONemission, Parens);
            GlobalVars.Instance.writeInformationToFiles("soilNMineralisation", "Soil mineralised N", "kgN/ha", soilNMineralisation, Parens);
            GlobalVars.Instance.writeInformationToFiles("mineralNavailable", "Mineral N available", "kgN/ha", mineralNavailable, Parens);
            GlobalVars.Instance.writeInformationToFiles("proportionLeached", "Proportion of other N leached", "kgN/ha", proportionLeached, Parens);
            GlobalVars.Instance.writeInformationToFiles("mineralNreserve", "Mineral N applied near end of crop perios", "kgN/ha", mineralNreserve, Parens);
            GlobalVars.Instance.writeInformationToFiles("nitrateLeaching", "Nitrate N leaching", "kgN/ha", nitrateLeaching, Parens);
            GlobalVars.Instance.writeInformationToFiles("unutilisedGrazableN", "N in unutilised grazable DM", "kgN/ha", unutilisedGrazableN, Parens);

            GlobalVars.Instance.writeInformationToFiles("cumulativepotEvapoTrans", "cumulativepotEvapoTrans", "mm", GetcumulativepotEvapoTrans(), Parens);
            GlobalVars.Instance.writeInformationToFiles("cumulativePrecipitation", "cumulativePrecipitation", "mm", GetcumulativePrecipitation(), Parens);
            GlobalVars.Instance.writeInformationToFiles("cumulativeIrrigation", "cumulativeIrrigation", "mm", GetcumulativeIrrigation(), Parens);
            GlobalVars.Instance.writeInformationToFiles("cumulativeEvaporation", "cumulativeEvaporation", "mm", GetcumulativeEvaporation(), Parens);
            GlobalVars.Instance.writeInformationToFiles("cumulativeTranspiration", "cumulativeTranspiration", "mm", GetcumulativeTranspiration(), Parens);
            GlobalVars.Instance.writeInformationToFiles("cumulativeDrainage", "cumulativeDrainage", "kg/ha", GetcumulativeDrainage(), Parens);
            GlobalVars.Instance.writeInformationToFiles("AverageDroughtIndex", "AverageDroughtIndex", "kg/ha", GetAverageDroughtIndexPlant(), Parens);

        
        for (int i = 0; i < theProducts.Count; i++)
        {
            theProducts[i].Write(Parens + "_theProducts" + i.ToString());
        }
        for (int i = 0; i < fertiliserApplied.Count; i++)
        {
            fertiliserApplied[i].Write(Parens + "_fertiliserApplied" + i.ToString());
        }
        for (int i = 0; i < manureApplied.Count; i++)
        {
            manureApplied[i].Write(Parens + "_manureApplied" + i.ToString());
        }

        GlobalVars.Instance.writeEndTab();

    }
    //! A normal member, Write Xls.
    /*!
     without returning value.
    */
    public void WriteXls()
    {
        GlobalVars.Instance.WriteCropFile("name", "New crop", name, true, false);
        GlobalVars.Instance.WriteCropFile("day", "Start_day", theStartDate.GetDay(), true, false);
        GlobalVars.Instance.WriteCropFile("month", "Start_month", theStartDate.GetMonth(), true, false);
        GlobalVars.Instance.WriteCropFile("year", "Start_year", theStartDate.GetYear(), true, false);
        GlobalVars.Instance.WriteCropFile("day", "End_day", theEndDate.GetDay(), true, false);
        GlobalVars.Instance.WriteCropFile("month", "End_month", theEndDate.GetMonth(), true, false);
        GlobalVars.Instance.WriteCropFile("year", "End_year", theEndDate.GetYear(), true, false);
        for (int i = 0; i < manureApplied.Count; i++)
            manureApplied[i].WriteXls();
        for (int i = 0; i < fertiliserApplied.Count; i++)
            fertiliserApplied[i].WriteXls();
        for (int i = 0; i < theProducts.Count; i++)
        {
            GlobalVars.Instance.WriteCropFile("product", "product name", theProducts[i].composition.GetName(), true, false);
            GlobalVars.Instance.WriteCropFile("fate", "fate of product", theProducts[i].Harvested, true, false);
        }
    }
    //! A normal member, Write Plant File. Taking three arguments.
    /*!
     \param deltaSoilN, one double argument.
     \param CdeltaSoil, one double argument.
     \param soilCO2_CEmission, one double argument.
    */
    public void WritePlantFile(double deltaSoilN, double CdeltaSoil, double soilCO2_CEmission)
    {
        int times = 1;
        if (GlobalVars.Instance.header == false)
            times = 2;
        for (int j = 0; j < times; j++)
        {
            GlobalVars.Instance.writePlantFile("Identity", "Identity", "-", identity, Parens, 0);
            GlobalVars.Instance.writePlantFile("name", "Crop name", "-", name, Parens, 0);
            GlobalVars.Instance.writePlantFile("area", "Area", "ha", area, Parens, 0);
            GlobalVars.Instance.writePlantFile("isIrrigated", "Is irrigated", "-", isIrrigated, Parens, 0);
            GlobalVars.Instance.writePlantFile("unutilisedGrazableDM", "Unutilised grazable DM", "kg/ha", unutilisedGrazableDM, Parens, 0);

            //        GlobalVars.Instance.writeInformationToFiles("NyieldMax", "??", "??", NyieldMax);
            //potential and water limited yield

            GlobalVars.Instance.writePlantFile("CFixed", "C fixed", "kgC/ha", CFixed, Parens, 0);
            GlobalVars.Instance.writePlantFile("surfaceResidueC", "C in surface residues", "kgC/ha", surfaceResidueC, Parens, 0);
            GlobalVars.Instance.writePlantFile("subsurfaceResidueC", "C in subsurface residues", "kgC/ha", subsurfaceResidueC, Parens, 0);
           
            GlobalVars.Instance.writePlantFile("urineCCropClass", "C in urine", "kgC/ha", urineC, Parens, 0);
            double amount = 0;
            for (int i = 0; i < manureFOMCsurface.Length; i++)
            {
                amount += manureFOMCsurface[i];
            }
            GlobalVars.Instance.writePlantFile("manureFOMCsurface", "manureFOMCsurface", "kgC/ha", amount, Parens,0);
            GlobalVars.Instance.writePlantFile("faecalCCropClass", "C in faeces", "kgC/ha", faecalC, Parens, 0);
            GlobalVars.Instance.writePlantFile("storageProcessingCLoss", "C lost during processing or storage", "kgC/ha", storageProcessingCLoss, Parens, 0);
            GlobalVars.Instance.writePlantFile("fertiliserC", "Emission of CO2 from fertiliser", "kgC/ha", fertiliserC, Parens, 0);
            GlobalVars.Instance.writePlantFile("harvestedC", "Harvested C", "kgC/ha", harvestedC, Parens, 0);
            GlobalVars.Instance.writePlantFile("harvestedDM", "Harvested DM", "kgDM/ha", harvestedDM, Parens, 0);
            GlobalVars.Instance.writePlantFile("burntResidueC", "C in burned crop residues", "kgC/ha", burntResidueC, Parens, 0);
            GlobalVars.Instance.writePlantFile("residueCtoNextCrop", "C in residues passed to next crop", "kgC/ha", residueCtoNextCrop, Parens, 0);
            
            GlobalVars.Instance.writePlantFile("unutilisedGrazableC", "C in unutilised grazable DM", "kg/ha", unutilisedGrazableC, Parens, 0);

            GlobalVars.Instance.writePlantFile("NyieldMax", "Maximum N yield", "kgN/ha", NyieldMax, Parens, 0);
            GlobalVars.Instance.writePlantFile("NavailFact", "N availability factor", "-", NavailFact, Parens, 0);
            GlobalVars.Instance.writePlantFile("maxCropNuptake", "Maximum crop N uptake", "kgN/ha", maxCropNuptake, Parens, 0);
            GlobalVars.Instance.writePlantFile("cropNuptake", "Crop N uptake", "kgN/ha", cropNuptake, Parens, 0);
            GlobalVars.Instance.writePlantFile("mineralNavailable", "Mineral N available", "kgN/ha", mineralNavailable, Parens, 0);
            GlobalVars.Instance.writePlantFile("soilNMineralisation", "Soil mineralised N", "kgN/ha", soilNMineralisation, Parens, 0);
            GlobalVars.Instance.writePlantFile("Nfixed", "N fixed", "kgN/ha", Nfixed, Parens, 0);
            GlobalVars.Instance.writePlantFile("nAtm", "N from atmospheric deposition", "kgN/ha", nAtm, Parens, 0);
            GlobalVars.Instance.writePlantFile("fertiliserNinput", "Input of N in fertiliser", "kgN/ha", fertiliserNinput, Parens, 0);
            GlobalVars.Instance.writePlantFile("totalManureNApplied", "Total N applied in manure", "kgN/ha", totalManureNApplied, Parens, 0);
            GlobalVars.Instance.writePlantFile("urineNCropClass", "Urine N", "kgN/ha", urineNasFertilizer, Parens, 0);
            GlobalVars.Instance.writePlantFile("faecalNCropClass", "Faecal N", "kgN/ha", faecalN, Parens, 0);
            GlobalVars.Instance.writePlantFile("mineralNFromLastCrop", "N2O emission from mineralised N", "kgN/ha", mineralNFromLastCrop, Parens, 0);
            GlobalVars.Instance.writePlantFile("surfaceResidueN", "N in surface residues", "kgN/ha", surfaceResidueN, Parens, 0);
            GlobalVars.Instance.writePlantFile("subsurfaceResidueN", "N in subsurface residues", "kgN/ha", subsurfaceResidueN, Parens, 0);
            GlobalVars.Instance.writePlantFile("excretaNInput", "Input of N in excreta", "kgN/ha", excretaNInput, Parens, 0);
            GlobalVars.Instance.writePlantFile("manureNH3emission", "NH3-N from manure application", "kgN/ha", manureNH3emission, Parens, 0);
            GlobalVars.Instance.writePlantFile("fertiliserNH3emission", "NH3-N from fertiliser application", "kgN/ha", fertiliserNH3emission, Parens, 0);
            GlobalVars.Instance.writePlantFile("urineNH3emission", "NH3-N from urine deposition", "kgN/ha", urineNH3emission, Parens, 0);
            GlobalVars.Instance.writePlantFile("harvestedN", "N harvested (N yield)", "kgN/ha", harvestedN, Parens, 0);
            GlobalVars.Instance.writePlantFile("storageProcessingNLoss", "N2 emission during product processing/storage", "kgN/ha", storageProcessingNLoss, Parens, 0);
            GlobalVars.Instance.writePlantFile("manureN2OEmission", "N2O emission from manure N", "kgN/ha", manureN2OEmission, Parens, 0);
            GlobalVars.Instance.writePlantFile("soilN2OEmission", "N2O emission from mineralised N", "kgN/ha", soilN2OEmission, Parens, 0);
            GlobalVars.Instance.writePlantFile("fertiliserN2OEmission", "N2O emission from fertiliser", "kgN/ha", fertiliserN2OEmission, Parens, 0);
            GlobalVars.Instance.writePlantFile("cropResidueN2O", "N2O emission from crop residues", "kgN/ha", cropResidueN2O, Parens, 0);
            GlobalVars.Instance.writePlantFile("burningN2ON", "N2O emission from burned crop residues", "kgN/ha", burningN2ON, Parens, 0);
            GlobalVars.Instance.writePlantFile("N2Nemission", "N2 emission", "kgN/ha", N2Nemission, Parens, 0);
            GlobalVars.Instance.writePlantFile("burningNH3N", "NH3 emission from burned crop residues", "kgN/ha", burningNH3N, Parens, 0);
            GlobalVars.Instance.writePlantFile("burningNOxN", "NOx emission from burned crop residues", "kgN/ha", burningNOxN, Parens, 0);
            GlobalVars.Instance.writePlantFile("burningOtherN", "N2 emission from burned crop residues", "kgN/ha", burningOtherN, Parens, 0);
            GlobalVars.Instance.writePlantFile("N2ONemission", "N2O emission", "kgN/ha", N2ONemission, Parens, 0);
            GlobalVars.Instance.writePlantFile("nitrateLeaching", "Nitrate N leaching", "kgN/ha", nitrateLeaching, Parens, 0);
            GlobalVars.Instance.writePlantFile("mineralNToNextCrop", "Mineral N to next crop", "kgN/ha", mineralNToNextCrop, Parens, 0);
            GlobalVars.Instance.writePlantFile("Precipitation", "Cum precipitation", "mm", GetcumulativePrecipitation(), Parens, 0);
            GlobalVars.Instance.writePlantFile("Irrigation", "Irrigation", "mm", GetcumulativeIrrigation(), Parens, 0);
            GlobalVars.Instance.writePlantFile("Drainage", "Drainage", "mm", GetcumulativeDrainage(), Parens, 0);
            GlobalVars.Instance.writePlantFile("surfaceResidueDM", "Surface residue dry matter", "kg/ha", surfaceResidueDM, Parens, 0);
            GlobalVars.Instance.writePlantFile("startday ", "startday", "day", theStartDate.GetDay(), Parens, 0);
            GlobalVars.Instance.writePlantFile("startmonth ", "startmonth", "month", theStartDate.GetMonth(), Parens, 0);
            GlobalVars.Instance.writePlantFile("startyear ", "startyear", "year", theStartDate.GetYear(), Parens, 0);

            GlobalVars.Instance.writePlantFile("endday ", "endday", "day", theEndDate.GetDay(), Parens, 0);
            GlobalVars.Instance.writePlantFile("endmonth ", "endmonth", "month", theEndDate.GetMonth(), Parens, 0);
            GlobalVars.Instance.writePlantFile("endyear ", "endyear", "year", theEndDate.GetYear(), Parens, 0);
            //This section of code writes the storage and processing losses to the plant file. However, this is only for information as the losses are accounted for elsewhere
            double temp = 0;
            for (int I = 0; I < theProducts.Count; I++)
            {
 //               if (theProducts[I].composition.GetStoreProcessFactor() > 0)
   //                 Console.WriteLine();
                temp += theProducts[I].Modelled_yield * theProducts[I].composition.GetStoreProcessFactor();
            }
            GlobalVars.Instance.writePlantFile("storageProcessingDMLoss", "DM lost during processing/storage", "kg DM/ha", temp, Parens, 0);
            temp = 0;
            for (int I = 0; I < theProducts.Count; I++)
            {
                temp += theProducts[I].Modelled_yield * theProducts[I].composition.GetStoreProcessFactor() * theProducts[I].composition.GetC_conc();
            }
            temp = 0;
            GlobalVars.Instance.writePlantFile("storageProcessingCLoss", "C lost during processing/storage", "kg C/ha", temp, Parens, 0);
            for (int I = 0; I < theProducts.Count; I++)
            {
                temp += theProducts[I].Modelled_yield * theProducts[I].composition.GetStoreProcessFactor() * theProducts[I].composition.GetN_conc();
            }
            GlobalVars.Instance.writePlantFile("storageProcessingNLoss", "N lost during processing/storage", "kg N/ha", temp, Parens, 0);
            GlobalVars.Instance.writePlantFile("unutilisedGrazableN", "N in unutilised grazable DM", "kgN/ha", unutilisedGrazableN, Parens, 0);

            if(GlobalVars.Instance.header==false)
                for (int i = 0; i < 2; i++)
                {
                    GlobalVars.product tmp = new GlobalVars.product();
                    tmp.WritePlantFile(Parens + "_theProducts" + i.ToString(), i, 2);
                }
            if (GlobalVars.Instance.header == true)
            {
               
                for (int i = 0; i < theProducts.Count; i++)
                {
                    theProducts[i].WritePlantFile(Parens + "_theProducts" + i.ToString(), i, theProducts.Count);
                }
                if(theProducts.Count==0)
                {
                    for (int i = 0; i <( 16); i++)
                        GlobalVars.Instance.writePlantFile("0", "0", "0", "0", Parens, 0);
                }
                if (theProducts.Count == 1)
                {
                    for (int i = 0; i < (8); i++)
                        GlobalVars.Instance.writePlantFile("0", "0", "0", "0", Parens, 0);
                }
            }
            GlobalVars.Instance.writePlantFile("CropSeq", "CropSeq", "-", cropSequenceNo, Parens, 0);
            GlobalVars.Instance.writePlantFile("duration", "duration", "days", duration, Parens, 0);
            GlobalVars.Instance.writePlantFile("residueCfromLastCrop", "C in residues from previous crop", "kgC/ha", residueCfromLastCrop, Parens, 0);
            GlobalVars.Instance.writePlantFile("soilCO2_CEmission", "Soil CO2-C emission", "kgC/ha", soilCO2_CEmission, Parens, 0);
            GlobalVars.Instance.writePlantFile("CdeltaSoil", "Change in soil C", "kgC/ha", CdeltaSoil, Parens, 0);
            GlobalVars.Instance.writePlantFile("deltaSoilN", "Change in soil N", "kgN/ha", deltaSoilN, Parens, 0);
            GlobalVars.Instance.writePlantFile("residueNfromLastCrop", "N in residues from previous crop", "kgN/ha", residueNfromLastCrop, Parens, 0);
            GlobalVars.Instance.writePlantFile("grazedN", "N grazed", "kgN/ha", grazedN, Parens, 0);
            GlobalVars.Instance.writePlantFile("residueNtoNextCrop", "N in residues passed to next crop", "kgN/ha", residueNtoNextCrop, Parens, 1);
            GlobalVars.Instance.header = true;
            
        }
    }

    //! A normal member, Get LAI. Taking one argument and Returning one double value.
    /*!
     \param index, one integer argument.
     \return a double value.
    */
    public double GetLAI(int index) { return LAI[index]; }
    //! A normal member, Get Potential Evapo Trans. Taking one argument and Returning one double value.
    /*!
     \param day, one integer argument.
     \return a double value.
    */
    public double GetpotentialEvapoTrans(int day) { return potentialEvapoTrans[day]; }
    //! A normal member, Get Precipitation. Taking one argument and Returning one double value.
    /*!
     \param day, one integer argument.
     \return a double value.
    */
    public double Getprecipitation(int day) { return precipitation[day]; }
    //! A normal member, Get Temperature. Taking one argument and Returning one double value.
    /*!
     \param day, one integer argument.
     \return a double value.
    */
    public double Gettemperature(int day) { return temperature[day]; }
    //! A normal member, Get isIrrigated. Returning one boolean value.
    /*!
     \return a boolean value.
    */
    public bool GetisIrrigated() { return isIrrigated; }
    //! A normal member, Get the Start Date. Returning one timeClass value.
    /*!
     \return a timeClass value.
    */
    public timeClass GettheStartDate() { return theStartDate; }
    //! A normal member, Get the End Date. Returning one timeClass value.
    /*!
     \return a timeClass value.
    */
    public timeClass GettheEndDate() { return theEndDate; }
    //! A normal member, Set Irrigation. Taking two arguments.
    /*!
     \param index, one integer argument.
     \param val, one double argument.
    */

    public void Setirrigation(int index, double val)
    {
        irrigationWater[index] = val;
    }
    //! A normal member, Calculate LAI. Taking one argument and Returning one double value.
    /*!
     \param dayNo, one integer argument.
     \return a double value.
    */
    public double CalculateLAI(int dayNo)
    {
        if (permanent)
            LAI[dayNo] = maxLAI;
        else
        {
            double maxDM = 0;
            double currentDM = 0;
            double timeIntoCrop = (double)dayNo / (double)duration;
            for (int i = 0; i < theProducts.Count; i++)
            {
                maxDM += theProducts[i].GetPotential_yield();
                //a more sensible way to simulate LAI
                //currentDM = theProducts[i].GetExpectedYield() * (Tsum[i] / totalTsum);
                currentDM += theProducts[i].GetExpectedYield() * timeIntoCrop;
            }
            if (maxDM == 0)
                LAI[dayNo] = 0;
            else
                LAI[dayNo] = maxLAI * currentDM / maxDM;
        }
        return LAI[dayNo];
    }
    //! A normal member, Calculate Rooting Depth. Taking one argument and Returning one double value.
    /*!
     \param dayNo, one double argument.
     \return a double value.
    */
    public double CalculateRootingDepth(double dayNo)
    {
        double rootingDepth = 0;
        if (permanent)
            rootingDepth = MaximumRootingDepth;
        else
        {
            double maxDM = 0;
            double currentDM = 0;
            double timeIntoCrop = dayNo / duration;
            for (int i = 0; i < theProducts.Count; i++)
            {
                maxDM += theProducts[i].GetPotential_yield();
                currentDM += theProducts[i].GetExpectedYield() * timeIntoCrop;
            }
            if (maxDM == 0)
                rootingDepth = 0;
            else
                rootingDepth = MaximumRootingDepth * currentDM / maxDM;
        }
        return rootingDepth;
    }
    //! A normal member, Calculate Drought Factors Soil. Returning one double value.
    /*!
     \return a double value.
    */
    public double CalculatedroughtFactorSoil()
    {
        double cumDroughtIndex = 0;
        for (int i = 0; i < duration; i++)
        {
            cumDroughtIndex += droughtFactorSoil[i];
        }
        double cropdroughtIndex = cumDroughtIndex / (double)duration;
        return cropdroughtIndex;
    }
    //! A normal member, Get Cumulative Precipitation. Returning one double value.
    /*!
     \return a double value.
    */

    public double GetcumulativePrecipitation()
    {
        double cumPrecip = 0;
        for (int i = 0; i < (int)duration; i++)
            cumPrecip += precipitation[i];
        return cumPrecip;
    }
    //! A normal member, Get Cumulative Drainage. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetcumulativeDrainage()
    {
        double cum = 0;
        for (int i = 0; i < (int)duration; i++)
            cum += drainage[i];
        return cum;
    }
    //! A normal member, Get Cumulative Irrigation. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetcumulativeIrrigation()
    {
        double cum = 0;
        for (int i = 0; i < (int)duration; i++)
            cum += irrigationWater[i];
        return cum;
    }
    //! A normal member, Get Cumulativepot Evapo Trans. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetcumulativepotEvapoTrans()
    {
        double cum = 0;
        for (int i = 0; i < (int)duration; i++)
            cum += potentialEvapoTrans[i];
        return cum;
    }
    //! A normal member, Get Cumulative Evaporation. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetcumulativeEvaporation()
    {
        double cum = 0;
        for (int i = 0; i < (int)duration; i++)
            cum += evaporation[i];
        return cum;
    }
    //! A normal member, Get Cumulative Transpiration. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetcumulativeTranspiration()
    {
        double cum = 0;
        for (int i = 0; i < (int)duration; i++)
            cum += transpire[i];
        return cum;
    }
    //! A normal member, Get Average Drought Index Plant. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetAverageDroughtIndexPlant()
    {
        double averageDroughtIndex = 0;
        for (int i = 0; i < duration; i++)
            averageDroughtIndex += droughtFactorPlant[i];
        averageDroughtIndex /= duration;
        return averageDroughtIndex;
    }
    //! A normal member, Get Average Drought Index Soil. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetAverageDroughtIndexSoil()
    {
        double averageDroughtIndex = 0;
        for (int i = 0; i < duration; i++)
            averageDroughtIndex += droughtFactorSoil[i];
        averageDroughtIndex /= duration;
        return averageDroughtIndex;
    }
    //! A normal member, Calculate Climate. 
    /*!
     without a returning value.
    */
    public void CalculateClimate()
    {
        double[] temppotentialEvapoTrans = new double[366];
        double[] tempprecipitation = new double[366];
        double[] temptemperature = new double[366];
        double[] tempTsum = new double[366];
        for (int i = 0; i < 366; i++)
        {
            temppotentialEvapoTrans[i] = 0;
            tempprecipitation[i] = 0;
            temptemperature[i] = 0;
            tempTsum[i] = 0;
        }
        int rainfreeDays = 1;
        double cumPrecip = 0;
        double dailyPrecip = 0;
        timeClass realTime = new timeClass(theStartDate);
        realTime.setDate(1, 1, realTime.GetYear());
        int daycount = 0;
        int maxRainfreeDays = 31;
        int currentMonth = 0;
        //calculate climate, using calendar year as starting point
        //Jonas - I think some of this code could be moved to GlobalVars
        for (int i = 0; i < 366; i++)
        {
            //if (realTime.GetMonth() == 10)
              //  Console.WriteLine("");
            if (realTime.GetMonth() > currentMonth)
            {
                double temp = 0;
                if (GlobalVars.Instance.theZoneData.rainDays[realTime.GetMonth() - 1] > 0)
                {
                    temp = realTime.GetDaysInMonth(realTime.GetMonth()) / GlobalVars.Instance.theZoneData.rainDays[realTime.GetMonth() - 1];
                    maxRainfreeDays = (int)Math.Round(temp);
                }
                else
                    maxRainfreeDays = 0;
                double precip = GlobalVars.Instance.theZoneData.Precipitation[realTime.GetMonth() - 1];
                double daysInMonth = (double)realTime.GetDaysInMonth(realTime.GetMonth());
                dailyPrecip = precip / daysInMonth;
                if (realTime.GetMonth() == 1)
                    rainfreeDays = (int)Math.Round(temp / 2);
                currentMonth = realTime.GetMonth();
            }
            temppotentialEvapoTrans[daycount] = GlobalVars.Instance.theZoneData.PotentialEvapoTrans[realTime.GetMonth() - 1];
            if (potentialEvapoTrans[daycount] > 20)
            {
                string messageString = ("Error; potential evapotranspiration for month \n");
                messageString += (daycount.ToString() + " is unrealistically high at " + potentialEvapoTrans[daycount].ToString());
                GlobalVars.Instance.Error(messageString);
            }
            cumPrecip += dailyPrecip;
            if (rainfreeDays >= maxRainfreeDays)
            {
                tempprecipitation[daycount] = cumPrecip;
                cumPrecip = 0;
                rainfreeDays = 1;
            }
            else
                rainfreeDays++;
            //            Console.WriteLine(" k " + k + " precip " + precipitation[k].ToString("F3"));
            temptemperature[daycount] = GlobalVars.Instance.theZoneData.airTemp[realTime.GetMonth() - 1];
            tempTsum[daycount] = GlobalVars.Instance.theZoneData.GetTemperatureSum(temptemperature[daycount], baseTemperature);
            //Console.WriteLine(name + " month " + realTime.GetMonth().ToString() + " day " + daycount.ToString() + " ppt " + tempprecipitation[daycount].ToString("0.00")
//                + " potevap " + temppotentialEvapoTrans[daycount].ToString("0.00"));
              //  + " dailyPrecip " + dailyPrecip.ToString("0.00"));
            daycount++;
            realTime.incrementOneDay();
        }

        //map climate based on calendar year, onto climate based on crop period
        timeClass clockit = new timeClass(theStartDate);
        int k = 0;
        k = clockit.getJulianDay()-1;

        for (int i = 0; i < 366; i++)
        {
            if (i < k)
                daycount = i + 366 - k;
            else
                daycount = i - k;
            potentialEvapoTrans[daycount] = temppotentialEvapoTrans[i];
            precipitation[daycount] = tempprecipitation[i];
            temperature[daycount] = temptemperature[i];
            Tsum[daycount]=tempTsum[i];
        }
        double temp2 = GetcumulativepotEvapoTrans();
        double temp1 = GetcumulativePrecipitation();
    }
    //! A normal member, Calculate Leaching and Uptake. Taking three arguments.
    /*!
     \param mineralNFromLastCrop, one double argument.
     \param evenNsupply, one double argument.
     \param relGrowth, one double argument.
    */
    public void CalculateLeachingAndUptake(double mineralNFromLastCrop, double evenNsupply, ref double relGrowth, 
        ref double mineralNsurplus)
    {
        double averagNitrificationInhibition=CalculateNitrificationInhibitor();
        double maxCropNuptake = CalculateMaxCropNUptake();
        if (zeroGasEmissionsDebugging)
        {
            soilN2OEmissionFactor = 0;
            manureN2OEmissionFactor = 0;
            fertiliserN2OEmissionFactor = 0;
        }
        else
        {
            soilN2OEmissionFactor = (1-averagNitrificationInhibition) * GlobalVars.Instance.theZoneData.getsoilN2OEmissionFactor(); ;
            manureN2OEmissionFactor = (1 - averagNitrificationInhibition) * GlobalVars.Instance.theZoneData.getmanureN20EmissionFactor();
            fertiliserN2OEmissionFactor = (1 - averagNitrificationInhibition) * GlobalVars.Instance.theZoneData.getfertiliserN20EmissionFactor();
            if (manureN2OEmissionFactor < 0.0)
                Console.WriteLine();
        }

        modelledCropNuptake = 0;
        nitrateLeaching = 0;
        manureN2OEmission = 0;
        fertiliserN2OEmission = 0;
        Nfixed = 0;

        soilN2OEmission = soilN2OEmissionFactor * evenNsupply;
        N2Nemission = soilN2OEmission * soilN2Factor;
        double Ninp = mineralNFromLastCrop + evenNsupply;
        evenNsupply -= soilN2OEmission + N2Nemission;
        double Nout = 0;
        double cumdrainage = 0;
        evenNsupply /= duration; //added evenly over duration

        mineralNavailable= mineralNFromLastCrop;
        double N2ON = 0;
        double N2N = 0;
        for (int i = 0; i < duration; i++)
        {
            double NleachingToday = 0;
            double fixationToday = 0;
            if ((!zeroLeachingDebugging)&&(mineralNavailable>0))
                NleachingToday=(1 - nitrificationInhibitor[i]) * mineralNavailable * drainage[i] / soilWater[i];
            cumdrainage += drainage[i];
            double NuptakeToday = 0;
            if (mineralNavailable>0)
                NuptakeToday=mineralNavailable * (1 - droughtFactorPlant[i]);
            if (NuptakeToday < 0)
                Console.Write("");
            double maxDailyCropNuptake = 0;
            if ((Tsum[i] > 0)&&(maxCropNuptake>0))
                maxDailyCropNuptake = (Tsum[i] / totalTsum) * maxCropNuptake;
            if (NuptakeToday > maxDailyCropNuptake)
                NuptakeToday = maxDailyCropNuptake;
          /*  Console.WriteLine("i " + i.ToString() + " drain " + drainage[i].ToString("0.00") + " leach " + 
                NleachingToday.ToString("0.00") + " min " + mineralNavailable.ToString("0.00") + " Nup " + NuptakeToday.ToString("0.00"));*/
            if (mineralNavailable > 0)
            {
                if ((NleachingToday + NuptakeToday) > mineralNavailable)
                {
                    NleachingToday *= mineralNavailable / (NleachingToday + NuptakeToday);
                    NuptakeToday *= mineralNavailable / (NleachingToday + NuptakeToday);
                }
            }
            dailyNitrateLeaching[i] = NleachingToday;
            nitrateLeaching += NleachingToday;
            modelledCropNuptake += NuptakeToday;
            if (NuptakeToday < 0)
                Console.WriteLine();
            if (((maxDailyCropNuptake - NuptakeToday) > 0) && (NfixationFactor > 0))
            {
                fixationToday = GetNfixation(maxDailyCropNuptake - NuptakeToday) * (1 - droughtFactorPlant[i]);
                Ninp += fixationToday;
            }
            modelledCropNuptake += fixationToday;
            Nfixed += fixationToday;
            Nout += NuptakeToday + NleachingToday + fixationToday;
            mineralNavailable += evenNsupply - (NleachingToday + NuptakeToday);
            if (fertiliserN[i] > 0)
            {
                Ninp += fertiliserN[i];
                N2ON = fertiliserN2OEmissionFactor * fertiliserN[i];
                fertiliserN2OEmission += N2ON;
                N2N = N2ON * soilN2Factor;
                N2Nemission += N2N;
                mineralNavailable += fertiliserN[i] - (N2ON + N2N);
            }
            if (manureTAN[i] > 0)
            {
                Ninp += manureTAN[i];
                N2ON = manureN2OEmissionFactor * manureTAN[i];
                manureN2OEmission += N2ON;
                N2N = N2ON * soilN2Factor;
                N2Nemission += N2N;
                mineralNavailable += manureTAN[i] - (N2ON + N2N);
            }
            if (N2ON < 0.0)
                Console.WriteLine();
        }
        if (modelledCropNuptake > maxCropNuptake)
        {
            mineralNavailable += modelledCropNuptake - maxCropNuptake;
            modelledCropNuptake = maxCropNuptake;
        }
        if (maxCropNuptake > 0) //not bare soil
            relGrowth = modelledCropNuptake / maxCropNuptake;
        else
            relGrowth = 0;
//        if (nitrateLeaching > 0)
  //          Console.Write("drain " + cumdrainage.ToString());
        GlobalVars.Instance.log(" Cum potevap " + GetcumulativepotEvapoTrans().ToString("F6") + " Cum ppt " + GetcumulativePrecipitation().ToString("F6")
            + " evap " + GetcumulativeEvaporation().ToString("F6") + " ave drought " + GetAverageDroughtIndexPlant() + " cum drain " + GetcumulativeDrainage().ToString("F6"),5);
        mineralNsurplus = mineralNavailable;
        N2ONemission = fertiliserN2OEmission + manureN2OEmission + soilN2OEmission;
        Nout += N2ONemission + N2Nemission;
        double balance = Ninp - (Nout + mineralNavailable);
    }
    //! A normal member, Get Relative N Uptake. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetRelativeNuptake()
    {
        double ret_val = 0;
        double maxNuptake = CalculateMaxCropNUptake();
        if (modelledCropNuptake > 0)
            ret_val = maxNuptake / modelledCropNuptake;
        else
            ret_val = 0;
        return ret_val;
    }

}
