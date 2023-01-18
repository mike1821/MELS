using System.Collections.Generic;
using System.Xml;
using System;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;
/*! A class that named GlobalVals */
public class GlobalVars
{
    private static GlobalVars instance;
    //! A structure avgCFom.
    /*!
      with two variables .
    */
    public struct avgCFom
    {
        public string parants;
        public double amounts;

    };
    //! an instance
    public List<avgCFom> allAvgCFom = new List<avgCFom>();
    //! A normal member. Add variables to structure AvgCFom. Taking two arguments.
    /*!
      \param amount, a double value.
      \param parant, a string value.
    */
    public void addAvgCFom(double amount, string parant)
    {
        avgCFom tmp;
        tmp.parants = parant;
        tmp.amounts = amount;
        allAvgCFom.Add(tmp);
    }
    //! A structure avgNFom.
    /*!
      with two variables .
    */
    public struct avgNFom
    {
        public string parants;
        public double amounts;

    };
    public List<avgNFom> allAvgNFom = new List<avgNFom>();
    //! A normal member. Add variables to structure AvgNFom. Taking two arguments.
    /*!
      \param amount, a double value.
      \param parant, a string value.
    */
    public void addAvgNFom(double amount, string parant)
    {
        avgNFom tmp;
        tmp.parants = parant;
        tmp.amounts = amount;
        allAvgNFom.Add(tmp);
    }

    public bool header;
    public bool headerLivestock;
    public bool Ctoolheader;
    private Stopwatch sw;
    System.IO.StreamWriter SummaryExcel;
    //! A constructor without argument.
    /*!
      set variables as default value false .
    */
    private GlobalVars()
    {
        header = false;
        headerLivestock = false;
        Ctoolheader = false;
    }
    //! A GlobaleVars class instance.
    /*!
      return class instance .
    */
    public static GlobalVars Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GlobalVars();
                instance.sw = new Stopwatch();
                instance.sw.Start();
            }
            return instance;
        }
    }
    ///Returns the species group that should be associated with the StorageID (from Plantedirektorats list)
    private int maxSpeciesGroupTypes = 25;
    //! A normal member, Get spacies group. Taking one argument and returning an integer value.
    /*!
      \param StorageID, an integer value.
      \return an integer value.
    */
    public int getSpeciesGroup(int StorageID)
    {
        if (StorageID > maxSpeciesGroupTypes - 1)
        {
            string messageString = "Error - attempting to access a manure of species type that is not recognised";
            GlobalVars.instance.Error(messageString);
        }
        int[] SpeciesGroup = new int[maxSpeciesGroupTypes];
        SpeciesGroup[0] = 0;
        SpeciesGroup[1] = 1;
        SpeciesGroup[2] = 1;
        SpeciesGroup[3] = 1;
        SpeciesGroup[4] = 1;
        SpeciesGroup[5] = 1;
        SpeciesGroup[6] = 1;
        SpeciesGroup[7] = 2;
        SpeciesGroup[8] = 2;
        SpeciesGroup[9] = 0;
        SpeciesGroup[10] = 4;
        SpeciesGroup[11] = 5;
        SpeciesGroup[12] = 1;
        SpeciesGroup[13] = 1;
        SpeciesGroup[14] = 0;
        SpeciesGroup[15] = 0;
        SpeciesGroup[16] = 0;
        SpeciesGroup[17] = 0;
        SpeciesGroup[18] = 0;
        SpeciesGroup[19] = 0;
        SpeciesGroup[20] = 0;
        SpeciesGroup[21] = 0;
        SpeciesGroup[22] = 0;
        SpeciesGroup[23] = 5;
        SpeciesGroup[24] = 3;
        return SpeciesGroup[StorageID];
    }
    ///Returns the manure type that should be associated with the StorageID (from Plantedirektorats list)
    private int maxManureTypes = 25;
    //! A normal member, Get Manure StorageID. Taking one argument and returning an integer value.
    /*!
      \param StorageID, an integer value.
      \return an integer value.
    */
    public int getManureStorageID(int StorageID)
    {
        int[] ManureType = new int[maxManureTypes];
        if (StorageID > maxManureTypes - 1)
        {
            string messageString = "Error - attempting to access a manure type that is not recognised";
            GlobalVars.instance.Error(messageString);
        }
        ManureType[0] = 0;
        ManureType[1] = 2;
        ManureType[2] = 3;
        ManureType[3] = 0;
        ManureType[4] = 13;
        ManureType[5] = 7;
        ManureType[6] = 6;
        ManureType[7] = 1;
        ManureType[8] = 3;
        ManureType[9] = 0;
        ManureType[10] = 1;
        ManureType[11] = 3;
        ManureType[12] = 5;
        ManureType[13] = 12;
        ManureType[14] = 1;
        ManureType[15] = 1;
        ManureType[16] = 13;
        ManureType[17] = 1;
        ManureType[18] = 1;
        ManureType[19] = 13;
        ManureType[20] = 13;
        ManureType[21] = 10;
        ManureType[22] = 9;
        ManureType[23] = 1;
        ManureType[24] = 3;
        return ManureType[StorageID];
    }
    //! A structure grazedItem.
    /*!
      with 9 variables and one method Write.
    */
    public struct grazedItem
    {
        public double urineC;
        public double urineN;
        public double faecesC;
        public double faecesN;
        public double ruminantDMgrazed;
        public double fieldDMgrazed;
        public double fieldCH4C;
        public string name;
        public string parens;
        public void Write()
        {
            GlobalVars.Instance.writeStartTab("GrazedItem");
            GlobalVars.Instance.writeInformationToFiles("name", "Name", "-", name, parens);
            if (GlobalVars.Instance.getRunFullModel() == true)
            {
                GlobalVars.Instance.writeInformationToFiles("fieldDMgrazed", "FieldDMgrazed", "kg", fieldDMgrazed, parens);
                GlobalVars.Instance.writeInformationToFiles("ruminantDMgrazed", "ruminantDMgrazed", "kg", ruminantDMgrazed, parens);
            }
            else
            {
                GlobalVars.Instance.writeInformationToFiles("fieldDMgrazed", "FieldDMgrazed", "kg", fieldDMgrazed, parens);
                GlobalVars.Instance.writeInformationToFiles("ruminantDMgrazed", "ruminantDMgrazed", "kg", ruminantDMgrazed, parens);
            }


            GlobalVars.Instance.writeEndTab();
        }
    }
    public grazedItem[] grazedArray = new grazedItem[maxNumberFeedItems];
    public product[] allFeedAndProductsUsed = new product[maxNumberFeedItems];
    public product[] allFeedAndProductsProduced = new product[maxNumberFeedItems];
    public product[] allFeedAndProductsPotential = new product[maxNumberFeedItems];
    public product[] allFeedAndProductTradeBalance = new product[maxNumberFeedItems];
    public product[] allFeedAndProductFieldProduction = new product[maxNumberFeedItems];
    private product[] allUnutilisedGrazableFeed = new product[maxNumberFeedItems];

    //constants
    private double humification_const;
    private double alpha;
    private double rgas;
    private double CNhum;
    private double tor;
    private double Eapp;
    private double C_CO2 = (12 + 2 * 16) / 12;
    private double CO2EqCH4;
    private double CO2EqN2O;
    private double CO2EqsoilC;
    private double IndirectNH3N2OFactor;
    private double IndirectNO3N2OFactor;
    private double defaultBeddingCconc;
    private double defaultBeddingNconc;
    private double maxToleratedErrorYield;
    private double maxToleratedErrorGrazing;
    private int maximumIterations;
    private double EFNO3_IPCC;
    private double digestEnergyToME = 0.81;
    private int minimumTimePeriod;
    private int adaptationTimePeriod;
    private List<int> theInventorySystems;
    private int currentInventorySystem;
    private int currentEnergySystem;
    bool strictGrazing;
    public bool logFile;
    public bool logScreen;
    public int verbosity;

    public bool returnErrorMessage = false;

    public bool Writeoutputxlm;
    public bool Writeoutputxls;
    public bool Writectoolxlm;
    public bool Writectoolxls;
    public bool WriteDebug;
    public bool Writelivestock;
    public bool WritePlant;
    public bool WriteCrop;
    public bool WriteSummaryExcel;
    public int reuseCtoolData;
    bool lockSoilTypes = false;  //if true, the CTOOL pools for each crop sequence will be preserved but the areas must not change. If false, pools within a soil type will be merged and areas can change.            
    public System.IO.StreamWriter logFileStream;

    private int zoneNr;
    private string locationNr;
    //! A normal member, Get Humification_const. Returning a double value.
    /*!
      \return a double value.
    */
    public double getHumification_const() { return humification_const; }
    //! A normal member, Get alpha. Returning a double value.
    /*!
      \return a double value.
    */
    public double getalpha() { return alpha; }
    //! A normal member, Get rgas. Returning a double value.
    /*!
      \return a double value.
    */
    public double getrgas() { return rgas; }
    //! A normal member, Get CNhum. Returning a double value.
    /*!
      \return a double value.
    */
    public double getCNhum() { return CNhum; }
    //! A normal member, Get tor. Returning a double value.
    /*!
      \return a double value.
    */
    public double gettor() { return tor; }
    //! A normal member, Get Eapp. Returning a double value.
    /*!
      \return a double value.
    */
    public double getEapp() { return Eapp; }
    //! A normal member, Get CO2EqCH4. Returning a double value.
    /*!
      \return a double value.
    */
    public double GetCO2EqCH4() { return CO2EqCH4; }
    //! A normal member, Get CO2EqN2O. Returning a double value.
    /*!
      \return a double value.
    */
    public double GetCO2EqN2O() { return CO2EqN2O; }
    //! A normal member, Get CO2EqsoilC. Returning a double value.
    /*!
      \return a double value.
    */
    public double GetCO2EqsoilC() { return CO2EqsoilC; }
    //! A normal member, Get C_CO2. Returning a double value.
    /*!
      \return a double value.
    */
    public double GetC_CO2() { return C_CO2; }
    //! A normal member, Get IndirectNH3N2OFactor. Returning a double value.
    /*!
      \return a double value.
    */
    public double GetIndirectNH3N2OFactor() { return IndirectNH3N2OFactor; }
    //! A normal member, Get IndirectNO3N2OFactor. Returning a double value.
    /*!
      \return a double value.
    */
    public double GetIndirectNO3N2OFactor() { return IndirectNO3N2OFactor; }
    //! A normal member, Get minimumTimePeriod. Returning an integer value.
    /*!
      \return an integer value.
    */
    public int GetminimumTimePeriod() { return minimumTimePeriod; }
    //! A normal member, Get adaptationTimePeriod. Returning an integer value.
    /*!
      \return an integer value.
    */
    public int GetadaptationTimePeriod() { return adaptationTimePeriod; }
    //! A normal member, Get strictGrazing. Returning a boolean value.
    /*!
      \return a boolean value.
    */
    public bool GetstrictGrazing() { return strictGrazing; }
    //! A normal member, Get maximumIterations. Returning an integer value.
    /*!
      \return an integer value.
    */
    public int GetmaximumIterations() { return maximumIterations; }
    //! A normal member, Get digetsEnergyToME. Returning a double value.
    /*!
      \return a double value.
    */
    public double GetdigestEnergyToME() { return digestEnergyToME; }
    //! A normal member, Get Zone. Returning an integer value.
    /*!
      \return an integer value.
    */
    public int GetZone() { return zoneNr; }
    //! A normal member, Set Zone. Taking one argument.
    /*!
      \param zone, an integer argument.
    */
    public void SetZone(int zone) { zoneNr = zone; }

    //! A normal member, Get Location. Returning a string value (East/West).
    /*!
      \return an integer value.
    */
    public string GetLocation() { return locationNr; }
    //! A normal member, Set location. Taking one argument.
    /*!
      \param location East/West, an string argument.
    */
    public void SetLocation(string location) { locationNr = location; }    //! A normal member, Get defaultBeddingCconc. Returning a double value.
    /*!
      \return a double value.
    */
    public double getdefaultBeddingCconc() { return defaultBeddingCconc; }
    //! A normal member, Get defaultBeddingNconc. Returning a double value.
    /*!
      \return a double value.
    */
    public double getdefaultBeddingNconc() { return defaultBeddingNconc; }
    //! A normal member, Get currentInventorySystem. Returning an integer value.
    /*!
      \return an integer value.
    */
    public int getcurrentInventorySystem() { return currentInventorySystem; }
    //! A normal member, Get currentEnergySystem. Returning an integer value.
    /*!
      \return an integer value.
    */
    public int getcurrentEnergySystem() { return currentEnergySystem; }
    //! A normal member, Get EFNO3_IPCC. Returning a double value.
    /*!
      \return a double value.
    */
    public double getEFNO3_IPCC() { return EFNO3_IPCC; }
    //! A normal member, Get maxToleratedErrorYield. Returning a double value.
    /*!
      \return a double value.
    */
    public double getmaxToleratedErrorYield() { return maxToleratedErrorYield; }
    //! A normal member, Get maxToleratedErrorGrazing. Returning a double value.
    /*!
      \return a double value.
    */
    public double getmaxToleratedErrorGrazing() { return maxToleratedErrorGrazing; }
    //! A normal member, Get lock SoilTypes. Returning a boolean value.
    /*!
      \return a boolean value.
    */
    public bool GetlockSoilTypes() { return lockSoilTypes; }
    //! A normal member, Set currentInvertorySystem. Taking one argument.
    /*!
      \param aVal, an integer argument.
    */
    public void setcurrentInventorySystem(int aVal) { currentInventorySystem = aVal; }
    //! A normal member, Set currentEnergySystem. Taking one argument.
    /*!
      \param aVal, an integer argument.
    */
    public void setcurrentEnergySystem(int aVal) { currentEnergySystem = aVal; }
    //! A normal member, Set strictGrazing. Taking one argument.
    /*!
      \param aVal, a boolean argument.
    */
    public void SetstrictGrazing(bool aVal) { strictGrazing = aVal; }
    //! A normal member, Get rho. Returning a double value.
    /*!
      \return a double value.
    */
    public double Getrho() { return 3.1415926 * 2.0 / (365.0 * 24.0 * 3600.0); }
    private bool stopOnError;
    private bool pauseBeforeExit;
    //! A normal member, Set pauseBeforeExit. Taking one argument.
    /*!
      \param stop, a boolean argument.
    */
    public void setPauseBeforeExit(bool stop) { pauseBeforeExit = stop; }
    //! A normal member, Get pauseBeforeExit. Returning a boolean value.
    /*!
      \return a boolean value.
    */
    public bool getPauseBeforeExit() { return pauseBeforeExit; }
    //! A normal member, Set stopOnError. Taking one argument.
    /*!
      \param stop, a boolean argument.
    */
    public void setstopOnError(bool stop) { stopOnError = stop; }
    //! A normal member, Get stopOnError. Returning a boolean value.
    /*!
      \return a boolean value.
    */
    public bool getstopOnError() { return stopOnError; }

    static string parens;
    //! A normal member, Set ErrorMessageReturn. 
    /*!
      more details.
    */
    public void ResetErrorMessageReturn()
    { AnimalChange.model.errorMessageReturn = ""; }
    //! A normal member, ReSet. Taking one argument.
    /*!
      \param aParens, a string argument.
    */
    public void reset(string aParens)
    {
        instance = null;
        parens = aParens;
        FileInformation information = new FileInformation();
        information.reset();
    }
    //private int[] errorCodes = new int[100];
    private bool RunFullModel;//forces exit with error if energy requirements not met
    //! A normal member, Set Run FullModel. Taking one argument.
    /*!
      \param aVal, a boolean argument.
    */
    public void setRunFullModel(bool aVal) { RunFullModel = aVal; }
    //! A normal member, Get Run FullModel. Returning a boolean value.
    /*!
      \return a boolean value.
    */
    public bool getRunFullModel() { return RunFullModel; }
    //! A normal member, Get ECM. Taking three arguments and returing a double value.
    /*!
      \param litres, a double argument.
      \param percentFat, a double argument.
      \param percentProtein, a double argument.
      \return a double value
    */
    public double GetECM(double litres, double percentFat, double percentProtein)
    {
        double retVal = litres * (0.383 * percentFat + 0.242 * percentProtein + 0.7832) / 3.1138;
        return retVal;
    }
    //! A structure zoneSpecificData.
    /*!
      more details.
    */
    public struct zoneSpecificData
    {

        private string debugFileName;
        private System.IO.StreamWriter debugfile;

        //! An internal member in structure, Set debugFileName. Taking one argument.
        /*!
          \param aName, a string argument.
        */
        public void SetdebugFileName(string aName) { debugFileName = aName; }


        public double[] airTemp;
        private double[] droughtIndex;
        public double[] Precipitation;
        public double[] PotentialEvapoTrans;
        public int[] rainDays;
        private int numberRainyDaysPerYear;
        private double Ndeposition;

        //! An internal structure fertiliserData.
        /*!
         data read from parameters.xml, fertilisers or manure tag.
        */
        public struct fertiliserData
        {
            public int manureType;
            public int speciesGroup; //livestock type for this manure (not used for fertilisers)
            public double fertManNH3EmissionFactor; //NH3 emission factor for field-applied manure or fertiliser (read from EFNH3)
            public double EFNH3FieldTier2; //Tier 2 NH3 emission for fertiliser (read from EFNH3FieldTier2)
            public double fertManNH3EmissionFactorHousingRefTemperature;
            public string name;
        }
        //! An internal structure C_ToolData.
        /*!
         data read from C_Tool.
        */
        public struct C_ToolData
        {
            public double initialC;
            public double InitialFOMCtoN;
            public double InitialFOM;
            public double InitialCtoN;
            public double pHUMupperLayer;
            public double pHUMlowerLayer;

        }
        //! An internal structure soilLayerData.
        /*!
         data read from soilLayer.
        */
        public struct soilLayerData
        {
            public double z_lower;
            public double fieldCapacity;
        }
        //! An internal structure soilWaterData.
        /*!
         data read from soilWater.
        */
        public struct soilWaterData
        {
            public double drainageConstant;
            public List<soilLayerData> thesoilLayerData;
        }
        //! An internal structure soilData.
        /*!
         data read from soil.
        */
        public struct soilData
        {
            public double N2Factor;
            public string name;
            public double ClayFraction;
            public double SandFraction;
            public double maxSoilDepth;
            public double dampingDepth;
            public double thermalDiff;
            public double GetdampingDepth() { return dampingDepth; }
            public List<C_ToolData> theC_ToolData;
            public soilWaterData thesoilWaterData;

            public double CalcDampingDepth(double thermalDiff, double rho)
            {
                return Math.Sqrt(3600 * 24 * 2.0 * thermalDiff / rho);
            }
            public string Getname() { return name; }
            public double GetSoilDepth() { return maxSoilDepth; }
        }
        //! An internal structure manureAppData.
        /*!
         data read from manureApp.
        */
        public struct manureAppData
        {
            public double NH3EmissionReductionFactor;
            public string name;
        }

        public List<fertiliserData> theFertManData;
        public List<soilData> thesoilData;
        public List<manureAppData> themanureAppData;
        double urineNH3EmissionFactor;
        double manureN20EmissionFactor;
        double fertiliserN20EmissionFactor;
        double residueN2OEmissionFactor;
        double burntResidueN2OEmissionFactor;
        double burntResidueNH3EmissionFactor;
        double burntResidueNOxEmissionFactor;
        double burntResidueCOEmissionFactor;
        double burntResidueBlackCEmissionFactor;
        double soilN2OEmissionFactor;
        double manureN2Factor;
        double averageAirTemperature;
        int airtemperatureOffset;
        double airtemperatureAmplitude;
        int grazingMidpoint;
        double averageYearsToSimulate;
        public double geturineNH3EmissionFactor() { return urineNH3EmissionFactor; }
        public double getmanureN20EmissionFactor() { return manureN20EmissionFactor; }
        public double getfertiliserN20EmissionFactor() { return fertiliserN20EmissionFactor; }
        //        public double GetfertManNH3EmissionFactorHousingRefTemperature() { return fertManNH3EmissionFactorHousingRefTemperature; }
        public double getresidueN2OEmissionFactor() { return residueN2OEmissionFactor; }
        public double getsoilN2OEmissionFactor() { return soilN2OEmissionFactor; }
        public double GetburntResidueN2OEmissionFactor() { return burntResidueN2OEmissionFactor; }
        public double GetburntResidueNH3EmissionFactor() { return burntResidueNH3EmissionFactor; }
        public double GetburntResidueNOxEmissionFactor() { return burntResidueNOxEmissionFactor; }
        public double GetburntResidueCOEmissionFactor() { return burntResidueCOEmissionFactor; }
        public double GetburntResidueBlackCEmissionFactor() { return burntResidueBlackCEmissionFactor; }
        public double GetaverageAirTemperature() { return averageAirTemperature; }
        public int GetairtemperatureOffset() { return airtemperatureOffset; }
        public double GetairtemperatureAmplitude() { return airtemperatureAmplitude; }
        public int GetgrazingMidpoint() { return grazingMidpoint; }
        public void SetaverageYearsToSimulate(double aVal) { averageYearsToSimulate = aVal; }
        public double GetaverageYearsToSimulate() { return averageYearsToSimulate; }
        public void SetNdeposition(double aVal) { Ndeposition = aVal; }
        public double GetNdeposition() { return Ndeposition; }

        public void OpenDebugFile(string afilename)
        {
            SetdebugFileName(afilename);
            debugfile = new System.IO.StreamWriter(debugFileName);
        }
        public void CloseDebugFile()
        {
            try
            {
                debugfile.Close();
            }
            catch
            { }
        }

        public void WriteToDebug(string aString)
        {
            debugfile.Write(aString);
        }

        public void WriteLineToDebug(string aString)
        {
            debugfile.WriteLine(aString);
        }

        ///this is only used if the soil C model needs to spin up. The drought index is read from parameters.xml
        public double GetMeanDroughtIndex(int startDay, int startMonth, int startYear, int endDay, int endMonth, int endYear)
        {
            timeClass time = new timeClass();
            double MeanDroughtIndex = 0;
            int endCount = endMonth;
            if (endYear > startYear)
                endCount += 12;
            for (int i = (startMonth) + 1; i <= endCount - 1; i++)
            {
                int monthCount = i;
                if (i > 12)
                    monthCount -= 12;
                MeanDroughtIndex += droughtIndex[monthCount - 1] * time.GetDaysInMonth(monthCount);
            }
            double startMonthAmount = ((time.GetDaysInMonth(startMonth) - startDay) / time.GetDaysInMonth(startMonth)) * droughtIndex[startMonth - 1];
            MeanDroughtIndex += startMonthAmount;
            double endMonthAmount = ((time.GetDaysInMonth(endMonth) - endDay) / time.GetDaysInMonth(endMonth)) * droughtIndex[endMonth - 1];
            MeanDroughtIndex += endMonthAmount;
            int numbersOfDays = 0;
            for (int i = (startMonth) + 1; i <= endCount - 1; i++)
            {
                int monthCount = i;
                if (i > 12)
                    monthCount -= 12;
                numbersOfDays += time.GetDaysInMonth(monthCount);
            }
            numbersOfDays += (time.GetDaysInMonth(endMonth) - endDay);
            numbersOfDays += (time.GetDaysInMonth(startMonth) - startDay);

            return MeanDroughtIndex / numbersOfDays;
        }
        public double GetMeanTemperature(CropClass cropData)
        {
            double MeanTemperature = 0;
            int startDay = cropData.GetStartDay();
            int startMonth = cropData.GetStartMonth();
            int startYear = cropData.GetStartYear();
            int endDay = cropData.GetEndDay();
            int endMonth = cropData.GetEndMonth();
            int endYear = cropData.GetEndYear();
            MeanTemperature = GetMeanTemperature(startDay, startMonth, startYear, endDay, endMonth, endYear);
            return MeanTemperature;
        }

        public double GetMeanTemperature(int startDay, int startMonth, int startYear, int endDay, int endMonth, int endYear)
        {
            timeClass time = new timeClass();
            double MeanTemperature = 0;
            int endCount = endMonth;
            if (endYear > startYear)
                endCount += 12;
            for (int i = (startMonth) + 1; i <= endCount - 1; i++)
            {
                int monthCount = i;
                if (i > 12)
                    monthCount -= 12;
                MeanTemperature += airTemp[monthCount - 1] * time.GetDaysInMonth(monthCount);
            }
            double startMonthAmount = ((time.GetDaysInMonth(startMonth) - startDay) / time.GetDaysInMonth(startMonth)) * airTemp[startMonth - 1];
            MeanTemperature += startMonthAmount;
            double endMonthAmount = ((time.GetDaysInMonth(endMonth) - endDay) / time.GetDaysInMonth(endMonth)) * airTemp[endMonth - 1];
            MeanTemperature += endMonthAmount;
            int numbersOfDays = 0;
            for (int i = (startMonth) + 1; i <= endCount - 1; i++)
            {
                int monthCount = i;
                if (i > 12)
                    monthCount -= 12;
                numbersOfDays += time.GetDaysInMonth(monthCount);
            }
            numbersOfDays += (time.GetDaysInMonth(endMonth) - endDay);
            numbersOfDays += (time.GetDaysInMonth(startMonth) - startDay);

            return MeanTemperature / numbersOfDays;
        }

        public double GetTemperatureSum(double temperature, double baseTemp)
        {
            double Tsum = 0;
            if (temperature > baseTemp)
                Tsum = temperature - baseTemp;
            return Tsum;
        }

        public double GetPeriodTemperatureSum(int startDay, int startMonth, int startYear, int endDay, int endMonth, int endYear, double baseTemp)
        {
            timeClass time = new timeClass();
            double Tsum = 0;
            int endCount = endMonth;
            if (endYear > startYear)
                endCount += 12;
            for (int i = (startMonth) + 1; i <= endCount - 1; i++)
            {
                int monthCount = i;
                if (i > 12)
                    monthCount -= 12;
                Tsum += GetTemperatureSum(airTemp[monthCount - 1], baseTemp) * time.GetDaysInMonth(monthCount);
            }
            Tsum += GetTemperatureSum(airTemp[startMonth - 1], baseTemp) * (time.GetDaysInMonth(startMonth) - startDay + 1);
            Tsum += GetTemperatureSum(airTemp[endMonth - 1], baseTemp) * endDay;
            return Tsum;
        }

        //!Generate temperature model parameters from monthly temperature data
        public void CalcTemperatureParameters()
        {
            double minTemp = 300;
            double maxTemp = -300;
            int maxMonth = 0;
            averageAirTemperature = 0;
            for (int i = 1; i <= 12; i++)
            {
                averageAirTemperature += airTemp[i - 1];
                if (airTemp[i - 1] > maxTemp)
                {
                    maxTemp = airTemp[i - 1];
                    maxMonth = i;
                }
                if (airTemp[i - 1] < minTemp)
                    minTemp = airTemp[i - 1];
            }
            averageAirTemperature /= 12.0;
            airtemperatureAmplitude = (maxTemp - minTemp) / 2;
            if ((maxMonth > 3) && (maxMonth <= 9))
                airtemperatureOffset = 245;//Northern hemisphere
            else
                airtemperatureOffset = 65;//Southern hemisphere
        }

        public void readZoneSpecificData(int zone_nr, int currentFarmType)
        {
            FileInformation AEZParamFile = new FileInformation(GlobalVars.Instance.getParamFilePath());
            //get zone-specific constants
            string basePath = "AgroecologicalZone(" + zone_nr.ToString() + ")";
            AEZParamFile.setPath(basePath);
            airTemp = new double[12];
            droughtIndex = new double[12];
            Precipitation = new double[12];
            Precipitation = new double[12];
            PotentialEvapoTrans = new double[12];
            rainDays = new int[12];
            double cumulativePrecip = 0;
            //check if the agroecological zone data are present. Note that the numbering of agroecological zones starts at 1 not zero
            string ZoneName=AEZParamFile.getItemString("Name", basePath);

            if (ZoneName.CompareTo("nothing")==0)
            {
                string messageString = "Error - agroecological zone " + zone_nr.ToString() + " not present in parameters.xml";
                GlobalVars.instance.Error(messageString);
            }


            //getItemInt("Value", basePath + ".Identity(" + zone_nr + ")");
            numberRainyDaysPerYear = AEZParamFile.getItemInt("Value", basePath + ".NumberRaindays(-1)");
            AEZParamFile.setPath(basePath);
            bool monthlyData = AEZParamFile.getItemBool("MonthlyAirTemp");
            AEZParamFile.PathNames.Add("Month");
            int max = 0; int min = 99;
            AEZParamFile.getSectionNumber(ref min, ref max);
            if ((max - min + 1) != 12)
            {
                string messageString = "Error - number of months in parameters.xml is not 12";
                GlobalVars.instance.Error(messageString);
            }
            AEZParamFile.Identity.Add(-1);
            AEZParamFile.Identity.Add(-1);
            AEZParamFile.PathNames.Add("DroughtIndex");
            for (int i = min; i <= max; i++)
            {
                AEZParamFile.Identity[1] = i;
                droughtIndex[i - 1] = AEZParamFile.getItemDouble("Value", false);
            }
            if (monthlyData == true)
            {
                AEZParamFile.PathNames[2] = "AirTemperature";
                for (int i = min; i <= max; i++)
                {
                    AEZParamFile.Identity[1] = i;
                    airTemp[i - 1] = AEZParamFile.getItemDouble("Value");
                    averageAirTemperature += airTemp[i - 1];
                }
                averageAirTemperature /= 12.0;
                /*AEZParamFile.PathNames[2] = "DroughtIndex"; 
                for (int i = min; i <= max; i++)
                {
                    AEZParamFile.Identity[1] = i;
                    droughtIndex[i - 1] = AEZParamFile.getItemDouble("Value");
                }*/
                AEZParamFile.PathNames[2] = "Precipitation";
                for (int i = min; i <= max; i++)
                {
                    AEZParamFile.Identity[1] = i;
                    Precipitation[i - 1] = AEZParamFile.getItemDouble("Value");
                    cumulativePrecip += Precipitation[i - 1];
                }
                AEZParamFile.PathNames[2] = "PotentialEvapoTrans";
                for (int i = min; i <= max; i++)
                {
                    AEZParamFile.Identity[1] = i;
                    PotentialEvapoTrans[i - 1] = AEZParamFile.getItemDouble("Value");
                }
                int checkdays = 0;
                for (int i = 0; i < 12; i++)
                {
                    rainDays[i] = (int)Math.Round(numberRainyDaysPerYear * (Precipitation[i] / cumulativePrecip));
                    checkdays += rainDays[i];
                }
                CalcTemperatureParameters();
            }
            else
            {
                AEZParamFile.setPath(basePath + ".AverageAirTemperature(-1)");
                averageAirTemperature = AEZParamFile.getItemDouble("Value");
                AEZParamFile.PathNames[1] = "AirTemperatureMaxDay";

                int temp = AEZParamFile.getItemInt("Value");
                airtemperatureOffset = temp + 94;
                AEZParamFile.PathNames[1] = "AirTemperaturAmplitude";

                airtemperatureAmplitude = AEZParamFile.getItemDouble("Value");
            }
            AEZParamFile.setPath(basePath + ".GrazingMidpoint(-1)");
            grazingMidpoint = AEZParamFile.getItemInt("Value");
            AEZParamFile.setPath(basePath + ".UrineNH3EF(-1)");
            urineNH3EmissionFactor = AEZParamFile.getItemDouble("Value");
            AEZParamFile.setPath(basePath + ".Manure(-1).EFN2O(-1)");
            manureN20EmissionFactor = AEZParamFile.getItemDouble("Value");
            AEZParamFile.setPath(basePath + ".Manure(-1).N2Factor(-1)");
            manureN2Factor = AEZParamFile.getItemDouble("Value");
            string tempPath = basePath + ".CropResidues(-1).EFN2O(-1)";
            residueN2OEmissionFactor = AEZParamFile.getItemDouble("Value", tempPath);
            tempPath = basePath + ".CropResidues(-1).EFN2O_burning(-1)";
            burntResidueN2OEmissionFactor = AEZParamFile.getItemDouble("Value", tempPath);
            tempPath = basePath + ".CropResidues(-1).EFNOx_burning(-1)";
            burntResidueNOxEmissionFactor = AEZParamFile.getItemDouble("Value", tempPath);
            tempPath = basePath + ".CropResidues(-1).EFNH3_burning(-1)";
            burntResidueNH3EmissionFactor = AEZParamFile.getItemDouble("Value", tempPath);
            tempPath = basePath + ".CropResidues(-1).EFBlackC_burning(-1)";
            burntResidueBlackCEmissionFactor = AEZParamFile.getItemDouble("Value", tempPath);
            tempPath = basePath + ".CropResidues(-1).EFCO_burning(-1)";
            burntResidueCOEmissionFactor = AEZParamFile.getItemDouble("Value", tempPath);
            AEZParamFile.setPath(basePath + ".MineralisedSoilN(-1).EFN2O(-1)");
            soilN2OEmissionFactor = AEZParamFile.getItemDouble("Value");
            AEZParamFile.setPath("AgroecologicalZone(" + zone_nr.ToString() + ").ManureApplicationTechnique");
            int maxApp = 0, minApp = 99;
            AEZParamFile.getSectionNumber(ref minApp, ref maxApp);
            themanureAppData = new List<manureAppData>();
            AEZParamFile.Identity.Add(-1);
            for (int j = minApp; j <= maxApp; j++)
            {
                AEZParamFile.Identity[1] = j;
                manureAppData newappData = new manureAppData();
                string RecipientPath = "AgroecologicalZone(" + zone_nr.ToString() + ").ManureApplicationTechnique" + '(' + j.ToString() + ").Name";
                newappData.name = AEZParamFile.getItemString("Name", RecipientPath);
                RecipientPath = "AgroecologicalZone(" + zone_nr.ToString() + ").ManureApplicationTechnique" + '(' + j.ToString() + ").NH3ReductionFactor(-1)";
                newappData.NH3EmissionReductionFactor = AEZParamFile.getItemDouble("Value", RecipientPath);
                themanureAppData.Add(newappData);
            }
            AEZParamFile.setPath("AgroecologicalZone(" + zone_nr.ToString() + ").SoilType");
            int maxSoil = 0, minSoil = 99;
            AEZParamFile.getSectionNumber(ref minSoil, ref maxSoil);
            thesoilData = new List<soilData>();
            AEZParamFile.Identity.Add(-1);
            for (int j = minSoil; j <= maxSoil; j++)
            {
                AEZParamFile.setPath("AgroecologicalZone(" + zone_nr.ToString() + ").SoilType");
                if (AEZParamFile.doesIDExist(j))
                {

                    soilData newsoilData = new soilData();
                    string RecipientStub = "AgroecologicalZone(" + zone_nr.ToString() + ").SoilType" + '(' + j.ToString() + ").";
                    string RecipientPath = RecipientStub;
                    newsoilData.name = AEZParamFile.getItemString("Name", RecipientPath);
                    RecipientPath = RecipientStub + "N2Factor(-1)";
                    newsoilData.N2Factor = AEZParamFile.getItemDouble("Value", RecipientPath);
                    RecipientPath = RecipientStub + "SandFraction(-1)";
                    newsoilData.SandFraction = AEZParamFile.getItemDouble("Value", RecipientPath);
                    RecipientPath = RecipientStub + "ClayFraction(-1)";
                    newsoilData.ClayFraction = AEZParamFile.getItemDouble("Value", RecipientPath);


                    RecipientPath = RecipientStub + "ThermalDiffusivity(-1)";
                    newsoilData.thermalDiff = AEZParamFile.getItemDouble("Value", RecipientPath);
                    newsoilData.dampingDepth = newsoilData.CalcDampingDepth(newsoilData.thermalDiff, GlobalVars.Instance.Getrho());
                    RecipientStub = "AgroecologicalZone(" + zone_nr.ToString() + ").SoilType(" + j.ToString() + ").C-Tool";
                    AEZParamFile.setPath(RecipientStub);
                    int maxHistory = 0, minHistory = 99;
                    AEZParamFile.getSectionNumber(ref minHistory, ref maxHistory);
                    newsoilData.theC_ToolData = new List<C_ToolData>();
                    AEZParamFile.Identity.Add(-1);
                    for (int k = minHistory; k <= maxHistory; k++)
                    {
                        AEZParamFile.Identity[1] = k;
                        C_ToolData newC_ToolData = new C_ToolData();
                        RecipientStub = "AgroecologicalZone(" + zone_nr.ToString() + ").SoilType(" + j.ToString() + ").C-Tool" + '(' + k.ToString() + ").";
                        RecipientPath = RecipientStub + "InitialC(-1)";
                        newC_ToolData.initialC = AEZParamFile.getItemDouble("Value", RecipientPath);
                        RecipientPath = RecipientStub + "InitialFOMinput(-1)";
                        newC_ToolData.InitialFOM = AEZParamFile.getItemDouble("Value", RecipientPath);
                        RecipientPath = RecipientStub + "InitialFOMCtoN(-1)";
                        newC_ToolData.InitialFOMCtoN = AEZParamFile.getItemDouble("Value", RecipientPath);
                        RecipientPath = RecipientStub + "InitialCtoN(-1)";
                        newC_ToolData.InitialCtoN = AEZParamFile.getItemDouble("Value", RecipientPath);
                        RecipientPath = RecipientStub + "pHUMupperLayer(-1)";
                        newC_ToolData.pHUMupperLayer = AEZParamFile.getItemDouble("Value", RecipientPath);
                        RecipientPath = RecipientStub + "pHUMlowerLayer(-1)";
                        newC_ToolData.pHUMlowerLayer = AEZParamFile.getItemDouble("Value", RecipientPath);
                        newsoilData.theC_ToolData.Add(newC_ToolData);
                    }
                    RecipientStub = "AgroecologicalZone(" + zone_nr.ToString() + ").SoilType(" + j.ToString() + ").SoilWater(-1)";

                    AEZParamFile.setPath(RecipientStub);
                    newsoilData.thesoilWaterData = new soilWaterData();
                    AEZParamFile.Identity.Add(-1);
                    AEZParamFile.setPath(RecipientStub + ".drainageConst(-1)");
                    newsoilData.thesoilWaterData = new soilWaterData();
                    newsoilData.thesoilWaterData.thesoilLayerData = new List<soilLayerData>();

                    newsoilData.thesoilWaterData.drainageConstant = AEZParamFile.getItemDouble("Value");

                    AEZParamFile.setPath(RecipientStub + ".layerClass");
                    min = 99; max = 0;
                    AEZParamFile.getSectionNumber(ref min, ref max);
                    for (int index = min; index <= max; index++)
                    {
                        soilLayerData anewsoilLayer = new soilLayerData();
                        string temp = RecipientStub + ".layerClass(" + index.ToString() + ").z_lower(-1)";
                        anewsoilLayer.z_lower = AEZParamFile.getItemDouble("Value", temp);
                        //temp = RecipientStub + ".layerClass(" + index.ToString() + ").fieldCapacity(-1)";
                        // anewsoilLayer.fieldCapacity = AEZParamFile.getItemDouble("Value", temp);
                        newsoilData.thesoilWaterData.thesoilLayerData.Add(anewsoilLayer);
                    }
                    newsoilData.maxSoilDepth = newsoilData.thesoilWaterData.thesoilLayerData[newsoilData.thesoilWaterData.thesoilLayerData.Count - 1].z_lower;
                    thesoilData.Add(newsoilData);
                }
            }
            AEZParamFile.setPath("AgroecologicalZone(" + zone_nr.ToString() + ").Fertiliser(-1).EFN2O(-1)");
            fertiliserN20EmissionFactor = AEZParamFile.getItemDouble("Value");
            AEZParamFile.setPath("AgroecologicalZone(" + zone_nr.ToString() + ").Fertiliser(-1).FertiliserType");
            int maxFert = 0, minFert = 99;
            AEZParamFile.getSectionNumber(ref minFert, ref maxFert);
            theFertManData = new List<fertiliserData>();

            for (int j = minFert; j <= maxFert; j++)
            {
                AEZParamFile.setPath("AgroecologicalZone(" + zone_nr.ToString() + ").Fertiliser(-1).FertiliserType");
                if (AEZParamFile.doesIDExist(j))
                {
                    AEZParamFile.Identity.Add(j);
                    fertiliserData newfertData = new fertiliserData();
                    string RecipientPath = "AgroecologicalZone(" + zone_nr.ToString() + ").Fertiliser(-1).FertiliserType" + '(' + j.ToString() + ")";
                    newfertData.name = AEZParamFile.getItemString("Name", RecipientPath);
                    RecipientPath = "AgroecologicalZone(" + zone_nr.ToString() + ").Fertiliser(-1).FertiliserType" + '(' + j.ToString() + ").EFNH3(-1)";
                    newfertData.fertManNH3EmissionFactor = AEZParamFile.getItemDouble("Value", RecipientPath);
                    newfertData.fertManNH3EmissionFactorHousingRefTemperature = 0;
                    theFertManData.Add(newfertData);

                }
            }
            string tmpPath = "AgroecologicalZone(" + zone_nr.ToString() + ").Manure(-1).ManureType";
            AEZParamFile.setPath(tmpPath);
            int maxMan = 0, minMan = 99;
            AEZParamFile.getSectionNumber(ref minMan, ref maxMan);
            AEZParamFile.Identity.Add(-1);
            for (int j = minMan; j <= maxMan; j++)
            {
                tmpPath = "AgroecologicalZone(" + zone_nr.ToString() + ").Manure(-1).ManureType";
                AEZParamFile.setPath(tmpPath);
                if (AEZParamFile.doesIDExist(j))
                {
                    tmpPath = "AgroecologicalZone(" + zone_nr.ToString() + ").Manure(-1).ManureType(-1)";
                    AEZParamFile.setPath(tmpPath);
                    AEZParamFile.Identity[2] = j;
                    fertiliserData newfertData = new fertiliserData();
                    newfertData.manureType = AEZParamFile.getItemInt("StorageType");
                    newfertData.speciesGroup = AEZParamFile.getItemInt("SpeciesGroup");
                    newfertData.name = AEZParamFile.getItemString("Name");
                    string RecipientPath = "AgroecologicalZone(" + zone_nr.ToString() + ").Manure(-1).ManureType" + '(' + j.ToString() + ").EFNH3FieldRef(-1)";
                    newfertData.fertManNH3EmissionFactor = AEZParamFile.getItemDouble("Value", RecipientPath);
                    RecipientPath = "AgroecologicalZone(" + zone_nr.ToString() + ").Manure(-1).ManureType" + '(' + j.ToString() + ").EFNH3FieldRefTemperature(-1)";
                    newfertData.fertManNH3EmissionFactorHousingRefTemperature = AEZParamFile.getItemDouble("Value", RecipientPath);
                    RecipientPath = "AgroecologicalZone(" + zone_nr.ToString() + ").Manure(-1).ManureType" + '(' + j.ToString() + ").EFNH3FieldTier2(-1)";
                    newfertData.EFNH3FieldTier2 = AEZParamFile.getItemDouble("Value", RecipientPath);
                    theFertManData.Add(newfertData);
                }
            }
        }
    }
    public zoneSpecificData theZoneData;
    //! A normal member, Read global constants. 
    /*!
      more details.
    */
    public void readGlobalConstants()
    {
        FileInformation constants = new FileInformation(GlobalVars.Instance.getConstantFilePath());
        constants.setPath("constants(0)");
        constants.Identity.Add(-1);
        constants.PathNames.Add("humification_const");
        humification_const = constants.getItemDouble("Value");
        constants.PathNames[constants.PathNames.Count - 1] = "alpha";
        alpha = constants.getItemDouble("Value");

        constants.PathNames[constants.PathNames.Count - 1] = "rgas";
        rgas = constants.getItemDouble("Value");
        constants.PathNames[constants.PathNames.Count - 1] = "CNhum";
        CNhum = constants.getItemDouble("Value");
        constants.PathNames[constants.PathNames.Count - 1] = "tor";
        tor = constants.getItemDouble("Value");
        constants.PathNames[constants.PathNames.Count - 1] = "Eapp";
        Eapp = constants.getItemDouble("Value");
        constants.PathNames[constants.PathNames.Count - 1] = "CO2EqCH4";
        CO2EqCH4 = constants.getItemDouble("Value");
        constants.PathNames[constants.PathNames.Count - 1] = "CO2EqN2O";
        CO2EqN2O = constants.getItemDouble("Value");
        constants.PathNames[constants.PathNames.Count - 1] = "CO2EqsoilC";
        CO2EqsoilC = constants.getItemDouble("Value");
        constants.PathNames[constants.PathNames.Count - 1] = "IndirectNH3N2OFactor";
        IndirectNH3N2OFactor = constants.getItemDouble("Value");
        constants.PathNames[constants.PathNames.Count - 1] = "IndirectNO3N2OFactor";
        IndirectNO3N2OFactor = constants.getItemDouble("Value");
        constants.PathNames[constants.PathNames.Count - 1] = "defaultBeddingCconc";
        defaultBeddingCconc = constants.getItemDouble("Value");
        constants.PathNames[constants.PathNames.Count - 1] = "defaultBeddingNconc";
        defaultBeddingNconc = constants.getItemDouble("Value");
        constants.PathNames[constants.PathNames.Count - 1] = "ErrorToleranceYield";
        maxToleratedErrorYield = constants.getItemDouble("Value");
        constants.PathNames[constants.PathNames.Count - 1] = "ErrorToleranceGrazing";
        maxToleratedErrorGrazing = constants.getItemDouble("Value");

        constants.PathNames[constants.PathNames.Count - 1] = "maximumIterations";
        maximumIterations = constants.getItemInt("Value");
        constants.PathNames[constants.PathNames.Count - 1] = "EFNO3_IPCC";
        EFNO3_IPCC = constants.getItemDouble("Value");
        constants.PathNames[constants.PathNames.Count - 1] = "minimumTimePeriod";
        minimumTimePeriod = constants.getItemInt("Value");
        constants.PathNames[constants.PathNames.Count - 1] = "adaptationTimePeriod";
        adaptationTimePeriod = constants.getItemInt("Value");
        constants.PathNames[constants.PathNames.Count - 1] = "lockSoilTypes";
        lockSoilTypes = constants.getItemBool("Value");
        /*constants.PathNames[constants.PathNames.Count - 1] = "CurrentInventorySystem";
        currentInventorySystem = constants.getItemInt("Value");*/

        List<int> theInventorySystems = new List<int>();
        constants.setPath("constants(0).InventorySystem");
        int maxInvSysts = 0, minInvSysts = 99;
        constants.getSectionNumber(ref minInvSysts, ref maxInvSysts);
        constants.Identity.Add(-1);
        for (int i = minInvSysts; i <= maxInvSysts; i++)
        {
            constants.Identity[constants.Identity.Count - 1] = i;
            theInventorySystems.Add(constants.getItemInt("Value"));
            //theInventorySystems.Add(i);
        }
    }

    private string[] constantFilePath;
    //! A normal member, Set constant FilePath. Taking one argument.
    /*!
      \param path a string value for path.
    */
    public void setConstantFilePath(string[] path)
    {
        constantFilePath = path;
    }
    //! A normal member, Get constant FilePath. Returing one string value.
    /*!
      \return a string array.
    */
    public string[] getConstantFilePath()
    {
        return constantFilePath;
    }

    private string[] ParamFilePath;
    //! A normal member, Set param FilePath. Taking one argument.
    /*!
      \param path a string value for path.
    */
    public void setParamFilePath(string[] path)
    {
        ParamFilePath = path;
    }
    //! A normal member, Get param FilePath. Returing one string value.
    /*!
      \return a string array.
    */
    public string[] getParamFilePath()
    {
        return ParamFilePath;
    }
    private string[] farmFilePath;
    //! A normal member, Set farm FilePath. Taking one argument.
    /*!
      \param path a string value for path.
    */
    public void setFarmtFilePath(string[] path)
    {
        farmFilePath = path;
    }
    //! A normal member, Get farm FilePath. Returing one string value.
    /*!
      \return a string array.
    */
    public string[] getFarmFilePath()
    {
        return farmFilePath;
    }
    private string[] feeditemPath;
    //! A normal member, Set FeedItem FilePath. Taking one argument.
    /*!
      \param path a string value for path.
    */
    public void setFeeditemFilePath(string[] path)
    {
        feeditemPath = path;
    }
    //! A normal member, Get FeedItem FilePath. Returing one string value.
    /*!
      \return a string array.
    */
    public string[] getfeeditemFilePath()
    {
        return feeditemPath;
    }
    private string[] fertManPath;
    //! A normal member, Set fertMan FilePath. Taking one argument.
    /*!
      \param path a string value for path.
    */
    public void setfertManFilePath(string[] path)
    {
        fertManPath = path;
    }
    //! A normal member, Get fertMan FilePath. Returing one string value.
    /*!
      \return a string array.
    */
    public string[] getfertManFilePath()
    {
        return fertManPath;
    }
    private string writeHandOverData = "simplesoilModel.xml";
    //! A normal member, Set Write HandOver Data. Taking one argument.
    /*!
      \param path a string value for path.
    */
    public void setWriteHandOverData(string path)
    {
        writeHandOverData = path;
    }
    //! A normal member, Get Write HandOver Data. Returing one string value.
    /*!
      \return  a string value.
    */
    public string getWriteHandOverData() { return writeHandOverData; }
    private string ReadHandOverData = "simplesoilModel.xml";
    //! A normal member, Set Read HandOver Data. Taking one argument.
    /*!
      \param path a string value for path.
    */
    public void setReadHandOverData(string path)
    {
        ReadHandOverData = path;
    }
    //! A normal member, Get Read HandOver Data. Returing one string value.
    /*!
      \return  a string value.
    */
    public string getReadHandOverData() { return ReadHandOverData; }

    private string errorFileName = "error.xml";
    //! A normal member, Set error FileName Taking one argument.
    /*!
      \param path a string value for path.
    */
    public void seterrorFileName(string path)
    {
        errorFileName = path;
    }
    public string GeterrorFileName() { return errorFileName; }
    public const int totalNumberLivestockCategories = 1;
    public const int totalNumberHousingCategories = 1;
    public const int totalNumberSpeciesGroups = 1;
    public const int totalNumberStorageTypes = 1;
    public const double avgNumberOfDays = 365;
    public const double NtoCrudeProtein = 6.25;
    public const double absoluteTemp = 273.15;
    public const int maxNumberFeedItems = 2000;
    public int getmaxNumberFeedItems() { return maxNumberFeedItems; }
    public double GetavgNumberOfDays() { return avgNumberOfDays; }
    public List<housing> listOfHousing = new List<housing>();

    public List<manureStore> listOfManurestores = new List<manureStore>();
    //! A class named product.
    /*!
      more details.
    */
    public class product
    {
        public double Modelled_yield;
        public double Expected_yield;
        public double Potential_yield;
        public double waterLimited_yield;
        public double Grazed_yield;
        public string Harvested;
        public feedItem composition;
        public string Units;
        public bool burn;
        public double ResidueGrazingAmount;
        //! An constructor.
        /*!
          without argument.
        */
        public product()
        {
            Modelled_yield = 0;
            Expected_yield = 0;
            waterLimited_yield = 0;
            Grazed_yield = 0;
            Potential_yield = 0;
            Harvested = "";
            Units = "";
            burn = false;
            ResidueGrazingAmount = 0;
            composition = new feedItem();
        }
        //! An constructor with one argument.
        /*!
          \param aProduct, a product instance.
        */
        public product(product aProduct)
        {
            Modelled_yield = aProduct.Modelled_yield;
            Expected_yield = aProduct.Expected_yield;
            waterLimited_yield = aProduct.waterLimited_yield;
            Grazed_yield = aProduct.Grazed_yield;
            Potential_yield = aProduct.Potential_yield;
            Harvested = aProduct.Harvested;
            Units = aProduct.Units;
            burn = aProduct.burn;
            ResidueGrazingAmount = aProduct.ResidueGrazingAmount;
            composition = new feedItem(aProduct.composition);
        }
        //! A normal member inside product class, Set ExpectedYield. Taking one argument.
        /*!
          \param aVal a double argument.
        */
        public void SetExpectedYield(double aVal) { Expected_yield = aVal; }
        //! A normal member inside product class, Get ExpectedYield. Returing a double value.
        /*!
          \return a double value.
        */
        public double GetExpectedYield() { return Expected_yield; }
        //! A normal member inside product class, Set Modelled_yield. Taking one argument.
        /*!
          \param aVal a double argument.
        */
        public void SetModelled_yield(double aVal)
        {
            Modelled_yield = aVal;
        }
        //! A normal member inside product class, Set waterLimited_yield. Taking one argument.
        /*!
          \param aVal a double argument.
        */
        public void SetwaterLimited_yield(double aVal) { waterLimited_yield = aVal; }
        //! A normal member inside product class, Set Grazed_yield. Taking one argument.
        /*!
          \param aVal a double argument.
        */
        public void SetGrazed_yield(double aVal) { Grazed_yield = aVal; }
        //! A normal member inside product class, Get Modelled_yield. Returing one double value.
        /*!
          \return a double value.
        */
        public double GetModelled_yield() { return Modelled_yield; }
        //! A normal member inside product class, Get waterLimited_yield. Returing one double value.
        /*!
          \return a double value.
        */
        public double GetwaterLimited_yield() { return waterLimited_yield; }
        //! A normal member inside product class, Get Potential_yield. Returing one double value.
        /*!
          \return a double value.
        */
        public double GetPotential_yield() { return Potential_yield; }
        //! A normal member inside product class, Get Grazed_yield. Returing one double value.
        /*!
          \return a double value.
        */
        public double GetGrazed_yield() { return Grazed_yield; }
        //! A normal member inside product class, Get isBedding. Returing one boolean value.
        /*!
          \return a boolean value.
        */

        public bool GetisBedding() { return composition.GetbeddingMaterial(); }
        //! A normal member inside product class, Add Expected Yield. Taking one argument.
        /*!
          \param aVal a double argument.
        */
        public void AddExpectedYield(double aVal) { Expected_yield += aVal; }
        //! A normal member inside product class, Add ActualAmount. Taking one argument.
        /*!
          \param aVal a double argument.
        */
        public void AddActualAmount(double aVal) { composition.Setamount(composition.Getamount() + aVal); }
        //! A normal member inside product class, Write. Taking one argument.
        /*!
          \param theParens a string argument.
        */
        public void Write(string theParens)
        {
            GlobalVars.Instance.writeStartTab("product");
            parens = theParens + "_FeedCode" + composition.GetFeedCode().ToString();
            GlobalVars.Instance.writeInformationToFiles("Name", "Name", "-", composition.GetName(), parens);
            GlobalVars.Instance.writeInformationToFiles("Potential_yield", "Potential yield", "kgDM/ha", Potential_yield, parens);
            GlobalVars.Instance.writeInformationToFiles("waterLimited_yield", "Expected yield", "kgDM/ha", waterLimited_yield, parens);
            GlobalVars.Instance.writeInformationToFiles("Modelled_yield", "Modelled yield", "kgDM/ha", Modelled_yield, parens);
            GlobalVars.Instance.writeInformationToFiles("Expected_yield", "Expected yield", "kgDM/ha", Expected_yield, parens);
            GlobalVars.Instance.writeInformationToFiles("Grazed_yield", "Grazed yield", "kgDM/ha", Grazed_yield, parens);
            GlobalVars.Instance.writeInformationToFiles("Harvested", "Is harvested", "-", Harvested, parens);
            GlobalVars.Instance.writeInformationToFiles("usableForBedding", "Usable for bedding", "-", GetisBedding(), parens);

            if (composition != null)
                composition.Write(parens);

            GlobalVars.Instance.writeEndTab();

        }

        //! A normal member inside product class, Write PlantFile. Taking three arguments.
        /*!
          \param theParens, a string argument.
          \param i, an integer argument
          \param count, an integer argument
        */

        public void WritePlantFile(string theParens, int i, int count)
        {

            parens = theParens + "_FeedCode" + composition.GetFeedCode().ToString();
            if (GlobalVars.Instance.header == false)
            {
                GlobalVars.Instance.writePlantFile("Name", "Name", "kg/ha", composition.GetName(), parens, 0);
                GlobalVars.Instance.writePlantFile("Potential_yield", "Potential yield", "kgDM/ha", Potential_yield, parens, 0);
                GlobalVars.Instance.writePlantFile("waterLimited_yield", "Expected yield", "kgDM/ha", waterLimited_yield, parens, 0);
                GlobalVars.Instance.writePlantFile("Modelled_yield", "Modelled yield", "kgDM/ha", Modelled_yield, parens, 0);
                GlobalVars.Instance.writePlantFile("Expected_yield", "Expected yield", "kgDM/ha", Expected_yield, parens, 0);
                GlobalVars.Instance.writePlantFile("Grazed_yield", "Grazed yield", "kgDM/ha", Grazed_yield, parens, 0);
                GlobalVars.Instance.writePlantFile("Harvested", "Is harvested", "-", Harvested, parens, 0);

                GlobalVars.Instance.writePlantFile("usableForBedding", "Usable for bedding", "-", GetisBedding(), parens, 0);

            }
            else if (GlobalVars.Instance.header == true)
            {
                GlobalVars.Instance.writePlantFile("Name", "Name", "kg/ha", composition.GetName(), parens, 0);
                GlobalVars.Instance.writePlantFile("Potential_yield", "Potential yield", "kgDM/ha", Potential_yield, parens, 0);
                GlobalVars.Instance.writePlantFile("waterLimited_yield", "Expected yield", "kgDM/ha", waterLimited_yield, parens, 0);
                GlobalVars.Instance.writePlantFile("Modelled_yield", "Modelled yield", "kgDM/ha", Modelled_yield, parens, 0);
                GlobalVars.Instance.writePlantFile("Expected_yield", "Expected yield", "kgDM/ha", Expected_yield, parens, 0);
                GlobalVars.Instance.writePlantFile("Grazed_yield", "Grazed yield", "kgDM/ha", Grazed_yield, parens, 0);
                GlobalVars.Instance.writePlantFile("Harvested", "Is harvested", "-", Harvested, parens, 0);

                GlobalVars.Instance.writePlantFile("usableForBedding", "Usable for bedding", "-", GetisBedding(), parens, 0);
            }
        }
    }

    public feedItem thebeddingMaterial = new feedItem();
    //! A normal member, Get the bedding Material Returing one string value.
    /*!
      \return  a feedItem.
    */
    public feedItem GetthebeddingMaterial() { return thebeddingMaterial; }

    //need to calculate these values
    //! A normal member, Calculate bedding Material. Taking one argument.
    /*!
      \param rotationList a list argument that points to CropSequenceClass.
    */
    public void CalcbeddingMaterial(List<CropSequenceClass> rotationList)
    {
        thebeddingMaterial.Setfibre_conc(0.1); //guess
        double tmp1 = 0;
        double tmp2 = 0;
        double tmp3 = 0;
        if (rotationList != null)
        {
            for (int i = 0; i < rotationList.Count; i++)
            {

                CropSequenceClass arotation = rotationList[i];
                for (int j = 0; j < arotation.GettheCrops().Count; j++)
                {
                    CropClass acrop = arotation.GettheCrops()[j];
                    for (int k = 0; k < acrop.GettheProducts().Count; k++)
                    {
                        product aproduct = acrop.GettheProducts()[k];
                        if (aproduct.composition.GetbeddingMaterial())
                        {
                            tmp1 += acrop.getArea() * aproduct.GetPotential_yield() * (1 - aproduct.composition.GetC_conc()) * aproduct.composition.Getash_conc();
                            tmp3 += acrop.getArea() * aproduct.GetPotential_yield() * (1 - aproduct.composition.GetN_conc()) * aproduct.composition.Getash_conc();
                            tmp2 += acrop.getArea() * aproduct.GetPotential_yield();
                        }
                    }
                }
            }
        }
        if (tmp2 > 0.0 && tmp3 > 0.0)
        {
            thebeddingMaterial.SetC_conc(tmp1 / tmp2);
            thebeddingMaterial.SetN_conc(tmp3 / tmp2);
            thebeddingMaterial.Setname("mixed beddding");
        }
        if (thebeddingMaterial.GetC_conc() == 0) //no bedding found on farm or field model not called
        {
            thebeddingMaterial.SetC_conc(GlobalVars.Instance.getdefaultBeddingCconc());
            thebeddingMaterial.SetN_conc(GlobalVars.Instance.getdefaultBeddingNconc());
            thebeddingMaterial.Setname("default beddding");
        }
        thebeddingMaterial.setFeedCode(999);
    }
    //! A structure manurestoreRecord.
    /*!
      more details.
    */
    public struct manurestoreRecord
    {
        manureStore theStore;
        double propYearGrazing;
        public void SetpropYearGrazing(double aVal) { propYearGrazing = aVal; }
        public manure manureToStorage;
        public void SetmanureToStorage(manure amanureToStorage) { manureToStorage = amanureToStorage; }
        public double GetpropYearGrazing() { return propYearGrazing; }
        public manure GetmanureToStorage() { return manureToStorage; }
        public manureStore GettheStore() { return theStore; }
        public void SettheStore(manureStore aStore) { theStore = aStore; }
        public void Write()
        {
            GlobalVars.Instance.writeStartTab("manurestoreRecord");
            theStore.Write();
            manureToStorage.Write(theStore.Getname().ToString());
            GlobalVars.Instance.writeEndTab();
        }
    }

     //! A class named theManureExchangeClass.
    /*!
      \the theManureExchangeClass is used to keep track of the manure generated on the farm and the manure that must be imported.
    */
    public class theManureExchangeClass
    {
        private List<manure> manuresStored;
        private List<manure> manuresProduced;
        private List<manure> manuresImported;
        private List<manure> manuresUsed;
        public List<manure> GetmanuresImported() { return manuresImported; }
        public List<manure> GetmanuresExported() { return manuresStored; }
        //! A constructor for theManureExchangeClass.
        /*!
          more details.
        */
        public theManureExchangeClass()
        {
            manuresStored = new List<manure>();
            manuresProduced = new List<manure>();
            manuresImported = new List<manure>();
            manuresUsed = new List<manure>();
        }
        //! A normal member, Write. 
        /*!
          a method for class theManureExchangeClass.
        */
        public void Write()
        {
            for (int i = 0; i < manuresProduced.Count; i++)
                manuresUsed.Add(new manure(manuresProduced[i]));
            for (int i = 0; i < manuresImported.Count; i++)
            {
                bool gotit = false;
                for (int k = 0; k < manuresUsed.Count; k++)
                {
                    if ((manuresUsed[k].GetmanureType() == manuresImported[i].GetmanureType()) &&
                        (manuresUsed[k].GetspeciesGroup() == manuresImported[i].GetspeciesGroup()))
                    {
                        manuresUsed[k].AddManure(manuresImported[i]);
                        gotit = true;
                    }
                }
                if (gotit == false)
                    manuresUsed.Add(manuresImported[i]);
            }
            for (int i = 0; i < manuresStored.Count; i++)
            {
                for (int k = 0; k < manuresUsed.Count; k++)
                {
                    if ((manuresUsed[k].GetmanureType() == manuresStored[i].GetmanureType()) &&
                        (manuresUsed[k].GetspeciesGroup() == manuresStored[i].GetspeciesGroup()))
                    {
                        manure amanure = new manure(manuresStored[i]);
                        double amount = manuresStored[i].GetTotalN();
                        manuresUsed[k].TakeManure(ref amount, ref amanure);
                    }
                }
            }
            GlobalVars.Instance.writeStartTab("theManureExchangeClass");
            GlobalVars.Instance.writeStartTab("producedManure");

            for (int i = 0; i < manuresProduced.Count; i++)
            {
                manuresProduced[i].Write("Produced");
            }
            GlobalVars.Instance.writeEndTab();
            GlobalVars.Instance.writeStartTab("exportedManure");
            for (int i = 0; i < manuresStored.Count; i++)
            {
                if (manuresStored[i].GetTotalN() > 0)
                    manuresStored[i].Write("Exported");
            }
            GlobalVars.Instance.writeEndTab();
            GlobalVars.Instance.writeStartTab("importedManure");
            for (int i = 0; i < manuresImported.Count; i++)
            {
                manuresImported[i].Write("Imported");
            }
            GlobalVars.Instance.writeEndTab();
            GlobalVars.Instance.writeStartTab("usedManure");
            for (int i = 0; i < manuresUsed.Count; i++)
            {
                manuresUsed[i].Write("Used");
            }
            GlobalVars.Instance.writeEndTab();
            GlobalVars.Instance.writeEndTab();
        }
        
        //! A normal member, Add to Manure Exchange. Taking one argument. Adds manure to the list of manures available
        /*!
          a method for class theManureExchangeClass.
        \param aManure, a manure instance argument
        */
        public void AddToManureExchange(manure aManure)
        {
            bool gotit = false;
            for (int i = 0; i < manuresStored.Count; i++)
            {
                if (manuresStored[i].isSame(aManure)) //add this manure to an existing manure
                {
                    gotit = true;
                    manuresStored[i].AddManure(aManure);
                    manure newManure = new manure(aManure);
                    manuresProduced[i].AddManure(newManure);
                    continue;
                }
            }
            if (gotit == false)
            {
                manuresStored.Add(aManure);
                manure newManure = new manure(aManure);
                manuresProduced.Add(newManure);
            }
        }
        //! A normal member, Take Manure. Taking four arguments and returing a manure class instance value.
        /*!
          \param amountN, a double argument.
          \param lengthOfSequence, a double argument.
          \param manureType, an integer argument.
          \param speciesGroup, an integer argument.
          \return a manure instance value. 
        */
        public manure TakeManure(double amountN, double lengthOfSequence, int manureType, int speciesGroup)
        {
            bool gotit = false;
            double amountNFound = amountN / lengthOfSequence;
            double amountNNeeded = amountN / lengthOfSequence;
            amountN /= lengthOfSequence;
            manure aManure = new manure();
            aManure.SetmanureType(manureType);
            aManure.SetspeciesGroup(speciesGroup);
            int i = 0;
            while ((i < manuresStored.Count) && (gotit == false))
            {
                if (manuresStored[i].isSame(aManure))
                {
                    gotit = true;
                    manuresStored[i].TakeManure(ref amountNFound, ref aManure);
                    amountNNeeded = amountN - amountNFound;
                }
                else
                    i++;
            }

            ///if cannot find this manure or there is none left
            if ((gotit == false) || (amountNNeeded > 0))
            {
                i = 0;
                gotit = false;
                while ((i < manuresImported.Count) && (gotit == false))
                {
                    if (manuresImported[i].isSame(aManure))  //there is already some of this manure that will be imported
                    {
                        double proportion = 0;
                        if (manuresImported[i].GetTotalN() != 0)
                            proportion = amountNNeeded / manuresImported[i].GetTotalN();
                        aManure.SetTAN(manuresImported[i].GetTAN() * proportion);
                        aManure.SetlabileOrganicN(manuresImported[i].GetlabileOrganicN() * proportion);
                        aManure.SethumicN(manuresImported[i].GethumicN() * proportion);
                        aManure.SethumicC(manuresImported[i].GethumicC() * proportion);
                        aManure.SetnonDegC(manuresImported[i].GetnonDegC() * proportion);
                        aManure.SetdegC(manuresImported[i].GetdegC() * proportion);
                        manuresImported[i].AddManure(aManure);
                        //and for all the other components of manure...
                        gotit = true;
                    }
                    else
                        i++;
                }
                if (gotit != true)
                {
                    ///find a standard manure of this type

                    FileInformation file = new FileInformation(GlobalVars.Instance.getfertManFilePath());
                    file.setPath("AgroecologicalZone(" + GlobalVars.Instance.GetZone().ToString() + ").manure");
                    int min = 99; int max = 0;
                    file.getSectionNumber(ref min, ref max);
                    file.Identity.Add(-1);
                    int itemNr = 0;
                    i = min;
                    while ((i <= max) && (gotit == false))
                    {
                        file.Identity[1] = i;
                        int ManureType = file.getItemInt("ManureType");
                        int SpeciesGroupFile = file.getItemInt("SpeciesGroup");
                        if (ManureType == manureType)
                        {
                            if ((SpeciesGroupFile == speciesGroup) || (speciesGroup == 0))
                            {
                                itemNr = i;
                                gotit = true;
                            }
                        }
                        i++;
                    }
                    if (gotit == false)
                    {

                        string messageString = "problem finding manure to import: species group = " + speciesGroup.ToString()
                            + " and storage type = " + manureType.ToString();
                        GlobalVars.Instance.Error(messageString);
                    }

                    manure anotherManure = new manure("manure", itemNr, amountNNeeded, parens + "_" + i.ToString());
                    manuresImported.Add(anotherManure);
                    aManure.AddManure(anotherManure);
                    aManure.Setname(anotherManure.Getname());
                }
            }
            aManure.IncreaseManure(lengthOfSequence);
            return aManure;
        }

    }///end of manure exchange
    private XmlWriter writer;
    //   public XElement writerCtool;
    //public XmlWriter writerCtool;
    private System.IO.StreamWriter tabFile;
    private System.IO.StreamWriter DebugFile;
    private System.IO.StreamWriter PlantFile;
    private System.IO.StreamWriter livestockFile;
    private System.IO.StreamWriter CtoolFile;
    private System.IO.StreamWriter CropFile;
    private string plantfileName;
    private string CtoolfileName;
    private string DebugfileName;
    private string livestockfileName;
    private string cropfileName;
    string PlantHeaderName;
    string PlantHeaderUnits;
    string CtoolHeaderName;
    string CtoolHeaderUnits;
    string livestockHeaderName;
    string livestockHeaderUnits;
    string cropHeaderName;
    string cropHeaderUnits;
    //! A normal member, open Output XML. Taking one argument and return a XMLWriter.
    /*!
      \param outputName, a string argument.
      \return a XMLWriter instance.
    */
    public XmlWriter OpenOutputXML(string outputName)
    {
        if (Writeoutputxlm)
        {
            writer = XmlWriter.Create(outputName);
            writer.WriteStartDocument();
        }
        return writer;
    }
    //! A normal member, Close Output XML. 
    /*!
      a method for closing the output XML file.
    */
    public void CloseOutputXML()
    {
        try //closing the output XML file
        {
            if (Writeoutputxlm)
            {
                writer.WriteEndDocument();
                writer.Close();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message); //write message to screen, if this fails
            log(e.Message, 6);
            log("Cannot close output xml file", 6);
        }


    }
    //! A normal member, Write StartTab. Taking one argument. 
    /*!
      \param name, a string argument.
    */
    public void writeStartTab(string name)
    {
        if (Writeoutputxls)
            tabFile.Write(name + "\n");
        if (Writeoutputxlm)
            writer.WriteStartElement(name);

    }
    //! A normal member, Write EndTab. Taking one argument. 
    /*!
      a method for writing the end tab of file.
    */
    public void writeEndTab()
    {
        if (Writeoutputxlm)
            writer.WriteEndElement();
    }
    //! A normal member, Write Information to Files (for boolean values). Taking five arguments. 
    /*!
      \param name, a string argument.
      \param Description, a string argument.
      \param Units, a string argument.
      \param value, a boolean argument.
      \param parens, a string argument.
    */
    public void writeInformationToFiles(string name, string Description, string Units, bool value, string parens)
    {
        writeInformationToFiles(name, Description, Units, Convert.ToString(value), parens);
    }
    //! A normal member, Write Information to Files (for double values). Taking five arguments. 
    /*!
      \param name, a string argument.
      \param Description, a string argument.
      \param Units, a string argument.
      \param value, a double argument.
      \param parens, a string argument.
    */
    public void writeInformationToFiles(string name, string Description, string Units, double value, string parens)
    {
        writeInformationToFiles(name, Description, Units, Convert.ToString(Math.Round(value, 4)), parens);
    }
    //! A normal member, Write Information to Files (for integer values). Taking five arguments. 
    /*!
      \param name, a string argument.
      \param Description, a string argument.
      \param Units, a string argument.
      \param value, an integer argument.
      \param parens, a string argument.
    */
    public void writeInformationToFiles(string name, string Description, string Units, int value, string parens)
    {
        writeInformationToFiles(name, Description, Units, Convert.ToString(value), parens);
    }
    //! A normal member, Write Information to Files (for string values). Taking five arguments. 
    /*!
      \param name, a string argument.
      \param Description, a string argument.
      \param Units, a string argument.
      \param value, a string argument.
      \param parens, a string argument.
    */
    public void writeInformationToFiles(string name, string Description, string Units, string value, string parens)
    {
        if (Writeoutputxlm)
        {
            writer.WriteStartElement(name);
            writer.WriteStartElement("Description");
            writer.WriteValue(Description);
            writer.WriteEndElement();
            writer.WriteStartElement("Units");
            writer.WriteValue(Units);
            writer.WriteEndElement();
            writer.WriteStartElement("Name");
            writer.WriteValue(name);
            writer.WriteEndElement();
            writer.WriteStartElement("Value");
            writer.WriteValue(value);
            writer.WriteEndElement();
            writer.WriteStartElement("StringUI");
            writer.WriteValue(name + parens);
            writer.WriteEndElement();
            writer.WriteEndElement();
        }
        if (Writeoutputxls)
        {
            tabFile.Write("Description" + "\t");
            tabFile.Write(Description + "\t");
            tabFile.Write("Units" + "\t");
            tabFile.Write(Units + "\t");
            tabFile.Write("Name" + "\t");
            tabFile.Write(name + parens + "\t");
            tabFile.Write("Value" + "\t");
            tabFile.Write(value + "\n");
        }

    }
    //! A normal member, Write Plant Files (for boolean values). Taking six arguments. 
    /*!
      \param name, a string argument.
      \param Description, a string argument.
      \param Units, a string argument.
      \param value, a boolean argument.
      \param parens, a string argument.
      \param newlinePlant, an integer argument. 
    */
    public void writePlantFile(string name, string Description, string Units, bool value, string parens, int newlinePlant)
    {
        writePlantFile(name, Description, Units, Convert.ToString(value), parens, newlinePlant);
    }
    //! A normal member, Write Plant Files (for double values). Taking six arguments. 
    /*!
      \param name, a string argument.
      \param Description, a string argument.
      \param Units, a string argument.
      \param value, a double argument.
      \param parens, a string argument.
      \param newlinePlant, an integer argument. 
    */
    public void writePlantFile(string name, string Description, string Units, double value, string parens, int newlinePlant)
    {
        writePlantFile(name, Description, Units, Convert.ToString(Math.Round(value, 4)), parens, newlinePlant);
    }
    //! A normal member, Write Plant Files (for integer values). Taking six arguments. 
    /*!
      \param name, a string argument.
      \param Description, a string argument.
      \param Units, a string argument.
      \param value, an integer argument.
      \param parens, a string argument.
      \param newlinePlant, an integer argument. 
    */
    public void writePlantFile(string name, string Description, string Units, int value, string parens, int newlinePlant)
    {
        writePlantFile(name, Description, Units, Convert.ToString(value), parens, newlinePlant);
    }
    //! A normal member, Write Plant Files (for string values). Taking six arguments. 
    /*!
      \param name, a string argument.
      \param Description, a string argument.
      \param Units, a string argument.
      \param value, a string argument.
      \param parens, a string argument.
      \param newlinePlant, an integer argument. 
    */
    public void writePlantFile(string name, string Description, string Units, string value, string parens, int newlinePlant)
    {
        if (WritePlant)
        {
            if (header == false)
            {
                if (newlinePlant == 0)
                {

                    PlantHeaderName += name + "\t";
                    PlantHeaderUnits += Units + "\t"; ;
                }
                if (newlinePlant == 1)
                {
                    PlantHeaderName += name;
                    PlantHeaderUnits += Units;
                    if (PlantFile != null)
                    {
                        PlantFile.Write(PlantHeaderName + "\n");
                        PlantFile.Write(PlantHeaderUnits + "\n");
                    }
                }
            }
            else
            {
                if (newlinePlant == 0)
                {
                    if (PlantFile != null)
                        PlantFile.Write(value + "\t");
                }
                if (newlinePlant == 1)
                {
                    if (PlantFile != null)
                        PlantFile.Write(value + "\n");
                }
            }
        }
    }
    //! A normal member, Write Livestock Files (for boolean values). Taking six arguments. 
    /*!
      \param name, a string argument.
      \param Description, a string argument.
      \param Units, a string argument.
      \param value, a boolean argument.
      \param parens, a string argument.
      \param newlinePlant, an integer argument. 
    */
    public void writeLivestockFile(string name, string Description, string Units, bool value, string parens, int newlinePlant)
    {
        writeLivestockFile(name, Description, Units, Convert.ToString(value), parens, newlinePlant);
    }
    //! A normal member, Write Livestock Files (for double values). Taking six arguments. 
    /*!
      \param name, a string argument.
      \param Description, a string argument.
      \param Units, a string argument.
      \param value, a double argument.
      \param parens, a string argument.
      \param newlinePlant, an integer argument. 
    */
    public void writeLivestockFile(string name, string Description, string Units, double value, string parens, int newlinePlant)
    {
        writeLivestockFile(name, Description, Units, Convert.ToString(Math.Round(value, 4)), parens, newlinePlant);
    }
    //! A normal member, Write Livestock Files (for integer values). Taking six arguments. 
    /*!
      \param name, a string argument.
      \param Description, a string argument.
      \param Units, a string argument.
      \param value, an integer argument.
      \param parens, a string argument.
      \param newlinePlant, an integer argument. 
    */
    public void writeLivestockFile(string name, string Description, string Units, int value, string parens, int newlinePlant)
    {
        writeLivestockFile(name, Description, Units, Convert.ToString(value), parens, newlinePlant);
    }
    //! A normal member, Write Livestock Files (for string values). Taking six arguments. 
    /*!
      \param name, a string argument.
      \param Description, a string argument.
      \param Units, a string argument.
      \param value, a string argument.
      \param parens, a string argument.
      \param newlinePlant, an integer argument. 
    */
    public void writeLivestockFile(string name, string Description, string Units, string value, string parens, int newlinePlant)
    {
        if (Writelivestock)
        {
            if (headerLivestock == false)
            {
                if (newlinePlant == 0)
                {

                    livestockHeaderName += name + "\t";
                    livestockHeaderUnits += Units + "\t"; ;
                }
                if (newlinePlant == 1)
                {
                    livestockFile.Write(livestockHeaderName + name + "\n");
                    livestockFile.Write(livestockHeaderUnits + Units + "\n");
                }
            }
            else
            {
                if (newlinePlant == 0)
                {

                    livestockFile.Write(value + "\t");
                }
                if (newlinePlant == 1)
                {
                    livestockFile.Write(value + "\n");
                }
            }
        }
    }

    //! A normal member, Write CTool Files (for boolean values). Taking eight arguments. 
    /*!
      \param name, a string argument.
      \param Description, a string argument.
      \param Units, a string argument.
      \param value, a boolean argument.
      \param parens, a string argument.
      \param printValue, a boolean argument.
      \param printUnits, a boolean argument. 
      \param newline, an integer argument. 
    */
    public XElement writeCtoolFile(string name, string Description, string Units, bool value, string parens, bool printValues, bool printUnits, int newline)
    {
        return writeCtoolFile(name, Description, Units, Convert.ToString(value), parens, printValues, printUnits, newline);
    }
    //! A normal member, Write CTool Files (for double values). Taking eight arguments. 
    /*!
      \param name, a string argument.
      \param Description, a string argument.
      \param Units, a string argument.
      \param value, a double argument.
      \param parens, a string argument.
      \param printValue, a boolean argument.
      \param printUnits, a boolean argument. 
      \param newline, an integer argument. 
    */
    public XElement writeCtoolFile(string name, string Description, string Units, double value, string parens, bool printValues, bool printUnits, int newline)
    {
        return writeCtoolFile(name, Description, Units, Convert.ToString(Math.Round(value, 4)), parens, printValues, printUnits, newline);
    }
    //! A normal member, Write CTool Files (for integer values). Taking eight arguments. 
    /*!
      \param name, a string argument.
      \param Description, a string argument.
      \param Units, a string argument.
      \param value, an integer argument.
      \param parens, a string argument.
      \param printValue, a boolean argument.
      \param printUnits, a boolean argument. 
      \param newline, an integer argument. 
    */
    public XElement writeCtoolFile(string name, string Description, string Units, int value, string parens, bool printValues, bool printUnits, int newline)
    {
        return writeCtoolFile(name, Description, Units, Convert.ToString(value), parens, printValues, printUnits, newline);
    }

    //if newlinePlant = 0, the data is written with a trailing tab character, if newlinePlant = 1, data is written with a trailing carriage retturn (new line) character
    //! A normal member, Write CTool Files (for string values). Taking eight arguments. 
    /*!
      \param name, a string argument.
      \param Description, a string argument.
      \param Units, a string argument.
      \param value, a string argument.
      \param parens, a string argument.
      \param printValue, a boolean argument.
      \param printUnits, a boolean argument. 
      \param newline, an integer argument. 
    */
    public XElement writeCtoolFile(string name, string Description, string Units, string value, string parens, bool printValues, bool printUnits, int newline)
    {

        if (Ctoolheader == false)
        {
            if (!printUnits)
            {
                if (newline == 1)
                    CtoolFile.Write(name + "\n");
                else
                    CtoolFile.Write(name + "\t");
            }
            else
            {
                if (newline == 1)
                    CtoolFile.Write(Units + "\n");
                else
                    CtoolFile.Write(Units + "\t");
            }
            return null;
        }
        if (printValues)
        {
            if (newline == 0)
            {
                if (Writectoolxls)
                    CtoolFile.Write(value + "\t");
            }
            if (newline == 1)
            {
                if (Writectoolxls)
                    CtoolFile.Write(value + "\n");
            }
        }
        return writeInformationToCtoolFiles(name, Description, Units, value, parens);
    }
    //! A normal member, Write Information to CTool Files (for string values). Taking five arguments. 
    /*!
      \param name, a string argument.
      \param Description, a string argument.
      \param Units, a string argument.
      \param value, a string argument.
      \param parens, a string argument. 
      \return a XElement. 
    */
    public XElement writeInformationToCtoolFiles(string name, string Description, string Units, string value, string parens)
    {
        XElement node = new XElement(name);

        XElement DescriptionNode = new XElement("Description");
        DescriptionNode.Value = Description;
        node.Add(DescriptionNode);

        DescriptionNode = new XElement("Units");
        DescriptionNode.Value = Units;
        node.Add(DescriptionNode);

        DescriptionNode = new XElement("name");
        DescriptionNode.Value = name;
        node.Add(DescriptionNode);
        DescriptionNode = new XElement("value");
        DescriptionNode.Value = value;
        node.Add(DescriptionNode);

        DescriptionNode = new XElement("StringUI");
        DescriptionNode.Value = name + parens;
        node.Add(DescriptionNode);
        return node;

    }
    //! A normal member, Open output tab files. Taking two arguments. 
    /*!
      \param outputName, a string argument.
      \param output, a string argument.
    */
    public void OpenOutputTabFile(string outputName, string output)
    {
        string tabfileName = outputName + ".xls";
        if (Writeoutputxls)
            tabFile = new System.IO.StreamWriter(tabfileName);
        plantfileName = outputName + "plantfile.xls";
        if (File.Exists(plantfileName))
            File.Delete(plantfileName);
        livestockfileName = outputName + "livetockfile.xls";
        CtoolfileName = outputName + "CtoolFile.xls";
        DebugfileName = outputName + "Debug.xls";
        cropfileName = outputName + "Crop.xls";
    }
    static bool usedPlant = false;
    //! A normal member, Open Plant File. 
    /*!
      more details.
    */
    public void OpenPlantFile()
    {
        if (WritePlant)
        {
            if (usedPlant == false)
                PlantFile = new System.IO.StreamWriter(plantfileName);
            else
                PlantFile = File.AppendText(plantfileName);
            usedPlant = true;
        }
    }
    //! A normal member, Close Plant File. 
    /*!
      more details.
    */
    public void ClosePlantFile()
    {
        try  //closing the crop output file
        {
            if (WritePlant)
                PlantFile.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            log(e.Message, 6);
            log("Cannot close output plant file", 6);
        }
    }

    static bool usedCrop = false;
    private bool CropHeader = false;
    private string headerCrop;
    private string dataCrop;
    //! A normal member, Open Crop File. 
    /*!
      more details.
    */
    public void OpenCropFile()
    {
        if (WriteCrop)
        {
            if (usedCrop == false)
                CropFile = new System.IO.StreamWriter(cropfileName);
            else
                CropFile = File.AppendText(cropfileName);
            usedPlant = true;
        }
    }
    //! A normal member, Close Crop File. 
    /*!
      more details.
    */
    public void CloseCropFile()
    {
        try
        {
            if (WriteCrop)
                CropFile.Close();
        }
        catch
        {
        }
    }
    //! A normal member, Write Crop Files (for integer values). Taking five arguments. 
    /*!
      \param name, a string argument.
      \param Units, a string argument.
      \param value, an integer argument.
      \param printUnits, a boolean argument. 
      \param newline, a boolean argument. 
    */
    public void WriteCropFile(string name, string Units, int value, bool printUnits, bool newline)
    {
        WriteCropFile(name, Units, value.ToString(), printUnits, newline);
    }
    //! A normal member, Write Crop Files (for double values). Taking five arguments. 
    /*!
      \param name, a string argument.
      \param Units, a string argument.
      \param value, a double argument.
      \param printUnits, a boolean argument. 
      \param newline, a boolean argument. 
    */
    public void WriteCropFile(string name, string Units, double value, bool printUnits, bool newline)
    {
        WriteCropFile(name, Units, value.ToString(), printUnits, newline);
    }
    //! A normal member, Write Crop Files (for string values). Taking five arguments. 
    /*!
      \param name, a string argument.
      \param Units, a string argument.
      \param value, a string argument.
      \param printUnits, a boolean argument. 
      \param newline, a boolean argument. 
    */
    public void WriteCropFile(string name, string Units, string value, bool printUnits, bool newline)
    {
        if (WriteCrop)
        {
            if (CropHeader)
            {
                string headerCrop = name + " (" + Units + ")";
                if (newline)
                    CropFile.Write(headerCrop + "\n");
                else
                    CropFile.Write(headerCrop + "\t");
                CropHeader = false;
            }
            else
            {
                if (printUnits)
                    CropFile.Write(Units + "\t");
                if (newline)
                    CropFile.Write(value + "\n");
                else
                    CropFile.Write(value + "\t");
            }
        }
    }

    //! A normal member, Open Debug Files. 
    public void OpenDebugFile()
    {
        if (WriteDebug)
            DebugFile = new System.IO.StreamWriter(DebugfileName);

    }
    //! A normal member, Close Debug Files. 
    public void CloseDebugFile()
    {

        try  //closing the debug file
        {
            if (WriteDebug)
            {
                DebugFile.Write(headerDebug);
                DebugFile.Write(dataDebug);
                DebugFile.Close();
                if (headerDebug == null)
                    File.Delete(DebugfileName);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            log(e.Message, 6);
            log("Cannot close output debug file", 6);
        }
    }
    private bool DebugHeader = true;
    private string headerDebug;
    private string dataDebug;
    //! A normal member, Write Debug Files (for integer values). Taking three arguments. 
    /*!
      \param name, a string argument.
      \param value, an integer argument.
      \param seperater, a char argument. 
    */
    public void WriteDebugFile(string name, int value, char seperater)
    {
        WriteDebugFile(name, value.ToString(), seperater);
    }
    //! A normal member, Write Debug Files (for double values). Taking three arguments. 
    /*!
      \param name, a string argument.
      \param value, a double argument.
      \param seperater, a char argument. 
    */
    public void WriteDebugFile(string name, double value, char seperater)
    {
        WriteDebugFile(name, value.ToString(), seperater);
    }
    //! A normal member, Write Debug Files (for string values). Taking three arguments. 
    /*!
      \param name, a string argument.
      \param value, a string argument.
      \param seperater, a char argument. 
    */
    public void WriteDebugFile(string name, string value, char seperater)
    {
        if (DebugHeader == true)
        {
            headerDebug += name + seperater;
        }
        if (seperater == '\n')
            DebugHeader = false;
        dataDebug += value + seperater;
    }
    //! A normal member, Open CTool File. 
    
    public void OpenCtoolFile()
    {
        if (Writectoolxls)
            CtoolFile = new System.IO.StreamWriter(CtoolfileName);
        /*        if (usedCtoolFile == false)
                    CtoolFile = new System.IO.StreamWriter(CtoolfileName);
                else
                    CtoolFile = File.AppendText(CtoolfileName);
                usedCtoolFile = true;
              */

    }
    //! A normal member, Close CTool File. 
    public void CloseCtoolFile()
    {
        try  //closing the CTOOL output file
        {
            if (Writectoolxls)
                CtoolFile.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            log(e.Message, 6);
            log("Cannot close output Ctool file", 6);
        }
    }
    //! A normal member, Open Livestock File. 
    public void OpenLivestockFile()
    {
        headerLivestock = false;
        if (Writelivestock)
            livestockFile = new System.IO.StreamWriter(livestockfileName);
    }
    //! A normal member, Close Livestock File. 
    public void CloseLivestockFile()
    {
        try  //closing the livestock output file
        {
            if (Writelivestock)
                livestockFile.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            log(e.Message, 6);
            log("Cannot close output livestock file", 6);
        }
    }
    //! A normal member, Close Output Tab File. 
    public void CloseOutputTabFile()
    {
        try //try to close output Excel file
        {
            if (Writeoutputxls)
                tabFile.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            log(e.Message, 6);
            log("Cannot close output tab file", 6);
        }
    }
    //! A normal member, Get Tab File Writer. Returning a tabFile value.
    /*!
     \return a tabFile. 
   */
    public System.IO.StreamWriter GetTabFileWriter() { return tabFile; }
    //! A normol member. erroMsg contains the description of the error, stackTrace contains the details of the calls to the stack, if stopOnException is true, the program will exit
    //! use stopOnException = false when running more than one farm or scenario and you wish to continue to the next farm or scenario, when an error occurs
    /*!
     \param erroMsg, a string argument.
     \param stackTrace, a string argument.
     \param stopOnException, a boolean argument. 
   */
    public void Error(string erroMsg, string stackTrace = "", bool stopOnException = true)
    {
        if (stopOnException == true)
        {
            if (logFileStream != null)
                log(erroMsg + " " + stackTrace, -1);
            CloseOutputXML();
            ClosePlantFile();
            CloseLivestockFile();
            CloseDebugFile();
            closeSummaryExcel();
            if (!erroMsg.Contains("farm Fail"))
            {
                if (returnErrorMessage)
                {
                    AnimalChange.model.errorMessageReturn = " " + erroMsg + " " + stackTrace;
                    Console.WriteLine(erroMsg + " " + stackTrace);
                    sw.Stop();
                    Console.WriteLine("RunTime (hrs:mins:secs) " + sw.Elapsed);
                    if (GlobalVars.Instance.getPauseBeforeExit())
                        Console.Read();
                }
                else
                {
                    Console.WriteLine(GlobalVars.Instance.GeterrorFileName());
                    System.IO.StreamWriter files = new System.IO.StreamWriter(GlobalVars.Instance.GeterrorFileName());
                    files.WriteLine(erroMsg + " " + stackTrace);
                    files.Close();
                    Console.WriteLine(erroMsg + " " + stackTrace);
                    sw.Stop();
                    Console.WriteLine("RunTime (hrs:mins:secs) " + sw.Elapsed);
                    if (GlobalVars.Instance.getPauseBeforeExit())
                        Console.Read();
                }
            }
        }
        else
        {
            try
            {
                CloseOutputXML();
                CloseOutputTabFile();
                ClosePlantFile();
                CloseLivestockFile();
                CloseCtoolFile();
                CloseDebugFile();
                closeSummaryExcel();
                //logFileStream.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exit without trapping error ");
                Console.WriteLine(erroMsg + " " + stackTrace);
                Console.Read();
            }
        }
        if (logFileStream != null)
            logFileStream.Close();
        if (stopOnException == true)
            throw new System.ArgumentException("farm Fail", "farm Fail");
    }
    public theManureExchangeClass theManureExchange;
    //! A normal member, Initialise Excreta Exchange.
    public void initialiseExcretaExchange()
    {
        for (int i = 0; i < maxNumberFeedItems; i++)
        {
            grazedArray[i].ruminantDMgrazed = 0;  //kg
            grazedArray[i].fieldDMgrazed = 0;  //kg
            grazedArray[i].urineC = 0;  //kg
            grazedArray[i].urineN = 0;  //kg
            grazedArray[i].faecesC = 0;  //kg
            grazedArray[i].faecesN = 0;  //kg
            grazedArray[i].fieldCH4C = 0; //kg
        }
    }
    //! A normal member, Write Grazed Items.
    /*!
     Write the consumed and produced DM for all grazed products/feeditems. 
   */
    public void writeGrazedItems()/// writes the consumed and produced DM for all grazed products/feeditems
    {
        for (int i = 0; i < maxNumberFeedItems; i++)
        {
            if ((grazedArray[i].fieldDMgrazed > 0.0) || (grazedArray[i].ruminantDMgrazed > 0.0))
                grazedArray[i].Write();
        }
    }
    //! A normal member, Initialise Feed and Product lists.
   
    public void initialiseFeedAndProductLists()
    {
        for (int i = 0; i < maxNumberFeedItems; i++)
        {
            feedItem aproduct = new feedItem();
            allFeedAndProductsUsed[i] = new product();
            allFeedAndProductsUsed[i].composition = aproduct;
            allFeedAndProductsUsed[i].composition.setFeedCode(i);
            allFeedAndProductsUsed[i].composition.Setamount(0);
            aproduct = new feedItem();
            allFeedAndProductsProduced[i] = new product();
            allFeedAndProductsProduced[i].composition = aproduct;
            allFeedAndProductsProduced[i].composition.setFeedCode(i);
            allFeedAndProductsProduced[i].composition.Setamount(0);
            aproduct = new feedItem();
            allFeedAndProductsPotential[i] = new product();
            allFeedAndProductsPotential[i].composition = aproduct;
            allFeedAndProductsPotential[i].composition.setFeedCode(i);
            allFeedAndProductsPotential[i].composition.Setamount(0);
            aproduct = new feedItem();
            allFeedAndProductFieldProduction[i] = new product();
            allFeedAndProductFieldProduction[i].composition = aproduct;
            allFeedAndProductFieldProduction[i].composition.setFeedCode(i);
            allFeedAndProductFieldProduction[i].composition.Setamount(0);
            aproduct = new feedItem();
            allFeedAndProductTradeBalance[i] = new product();
            allFeedAndProductTradeBalance[i].composition = aproduct;
            allFeedAndProductTradeBalance[i].composition.setFeedCode(i);
            allFeedAndProductTradeBalance[i].composition.Setamount(0);
            aproduct = new feedItem();
            allUnutilisedGrazableFeed[i] = new product();
            allUnutilisedGrazableFeed[i].composition = aproduct;
            allUnutilisedGrazableFeed[i].composition.setFeedCode(i);
            allUnutilisedGrazableFeed[i].composition.Setamount(0);
        }
    }
    //! A normal member, Add Product Produced. Taking one argument.
    /*!
     \param anItem, a feedItem class instance. 
   */
    public void AddProductProduced(feedItem anItem)
    {
        allFeedAndProductsProduced[anItem.GetFeedCode()].composition.Setname(anItem.GetName());
        allFeedAndProductsProduced[anItem.GetFeedCode()].composition.AddFeedItem(anItem, false);
    }
    //! A normal member, Add Grazable Product Unutilised. Taking one argument.
    /*!
     \param anItem, a feedItem class instance. 
   */
    public void AddGrazableProductUnutilised(feedItem anItem)
    {
        allUnutilisedGrazableFeed[anItem.GetFeedCode()].composition.Setname(anItem.GetName());
        allUnutilisedGrazableFeed[anItem.GetFeedCode()].composition.AddFeedItem(anItem, false);
    }
    //! A normal member, Test all Feed and Products used. 
    /*!
      Print to screen the amount of N in products used. Only for debugging.
   */
   
    public void testallFeedAndProductsUsed()
    {
        for (int i = 0; i < maxNumberFeedItems; i++)
        {
            if (allFeedAndProductsUsed[i].composition.Getamount() > 0)
                Console.WriteLine(allFeedAndProductsUsed[i].composition.GetFeedCode() + " " + allFeedAndProductsUsed[i].composition.Getamount() * allFeedAndProductsUsed[i].composition.GetN_conc());
        }
    }
    //! A normal member, Calculate Trade Balance.
    public void CalculateTradeBalance()
    {
        for (int i = 0; i < maxNumberFeedItems; i++)
        {
            product feedItemUsed = allFeedAndProductsUsed[i];
            product feedItemProduced = allFeedAndProductsProduced[i];
            if ((feedItemUsed.composition.Getamount() > 0) || (feedItemProduced.composition.Getamount() > 0))
            {
                if (feedItemUsed.composition.Getamount() > 0)
                {
                    if (feedItemProduced.composition.Getamount() == 0)
                    {
                        if (feedItemUsed.composition.GetFeedCode() == 999)
                        {
                            allFeedAndProductTradeBalance[i].composition.AddFeedItem(allFeedAndProductsUsed[999].composition, true, true);
                            allFeedAndProductTradeBalance[i].composition.Setamount(0);
                        }
                        else
                            allFeedAndProductTradeBalance[i].composition.GetStandardFeedItem(i);
                    }
                }
                if (feedItemProduced.composition.Getamount() > 0)
                    allFeedAndProductTradeBalance[i].composition.AddFeedItem(feedItemProduced.composition, true);
                if (feedItemUsed.composition.Getamount() > 0)
                    allFeedAndProductTradeBalance[i].composition.SubtractFeedItem(feedItemUsed.composition, true);
            }
        }
    }
    //! A normal member, Get Plant Product Imports. Returing a product instance.
    /*!
     \return a class product instance.
  */
    public product GetPlantProductImports()
    {
        product aProduct = new product();
        aProduct.composition = new feedItem();
        for (int i = 0; i < maxNumberFeedItems; i++)
        {
            product productToAdd = new product();
            productToAdd.composition = new feedItem();
            if (allFeedAndProductTradeBalance[i].composition.Getamount() < 0)
            {
                productToAdd.composition.AddFeedItem(allFeedAndProductTradeBalance[i].composition, true);
                productToAdd.composition.Setamount(allFeedAndProductTradeBalance[i].composition.Getamount() * -1.0);
                aProduct.composition.AddFeedItem(productToAdd.composition, true);
                double N = productToAdd.composition.Getamount() * productToAdd.composition.GetN_conc();

                GlobalVars.Instance.log(productToAdd.composition.GetFeedCode().ToString() + " " + productToAdd.composition.Getamount().ToString("0.0") + " " + N.ToString("0.0"), 5);
            }
        }
        return aProduct;
    }
    //! A normal member, Get Bedding Imported. Returing a feedItem instance.
    /*!
     \return a class feedItem instance.
  */
    public feedItem GetBeddingImported()
    {
        feedItem beddingItem = new feedItem();
        for (int i = 0; i < maxNumberFeedItems; i++)
        {
            if ((allFeedAndProductTradeBalance[i].composition.GetbeddingMaterial()) && (allFeedAndProductTradeBalance[i].composition.Getamount() < 0))
                beddingItem.AddFeedItem(allFeedAndProductTradeBalance[i].composition, true, true);
        }
        beddingItem.Setamount(beddingItem.Getamount() * -1);
        return beddingItem;
    }
    //! A normal member, Get Plant Product Exports. Returing a product instance.
    /*!
     \return a class product instance.
  */

    public product GetPlantProductExports()
    {
        product aProduct = new product();
        aProduct.composition = new feedItem();
        for (int i = 0; i < maxNumberFeedItems; i++)
        {
            product productToAdd = new product();
            productToAdd.composition = new feedItem();
            if (allFeedAndProductTradeBalance[i].composition.Getamount() > 0)
            {
                productToAdd.composition.AddFeedItem(allFeedAndProductTradeBalance[i].composition, true);
                aProduct.composition.AddFeedItem(productToAdd.composition, true);
            }
        }
        return aProduct;
    }
    //! A normal member, Calculate all Feed and Products Potential. Taking one list argument.
    /*!
     \param list, a list argument that points to CropSequenceClass.
  */

    public void CalcAllFeedAndProductsPotential(List<CropSequenceClass> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            List<CropClass> crops = list[i].GettheCrops();
            int yearsInSequence = list[i].GetlengthOfSequence();
            for (int j = 0; j < crops.Count; j++)
            {
                List<product> products = crops[j].GettheProducts();
                for (int k = 0; k < products.Count; k++)
                {
                    int feedCode = products[k].composition.GetFeedCode();
                    if (products[k].Potential_yield > 0)
                    {
                        allFeedAndProductsPotential[feedCode].composition.Addamount(crops[j].getArea() * products[k].Potential_yield / yearsInSequence);
                        allFeedAndProductsPotential[feedCode].composition.Setname(products[k].composition.GetName());
                    }
                    if (products[k].Modelled_yield > 0)
                    {
                        allFeedAndProductFieldProduction[feedCode].composition.Addamount(crops[j].getArea() * products[k].Modelled_yield / yearsInSequence);
                        allFeedAndProductFieldProduction[feedCode].composition.Setname(products[k].composition.GetName());
                    }
                }

            }
        }
    }
    //! A normal member, Calculate all Feed and Products Potential. Taking two arguments.
    /*!
     \param importedRoughageDM, a double argument.
     \param exportedRoughageDM, a double argument.
  */
    public void GetRoughageExchange(ref double importedRoughageDM, ref double exportedRoughageDM)
    {
        importedRoughageDM = 0;
        exportedRoughageDM = 0;
        for (int i = 0; i < maxNumberFeedItems; i++)
        {
            int feedCode = allFeedAndProductTradeBalance[i].composition.GetFeedCode();
            double amount = allFeedAndProductTradeBalance[i].composition.Getamount();
            if ((feedCode >= 400) && (feedCode < 700))
            {
                if (amount > 0)
                    exportedRoughageDM += amount;
                if (amount < 0)
                    importedRoughageDM -= amount;
            }
        }
    }
    //! A normal member, Print Plant Products.
    
    public void PrintPlantProducts()
    {
        double totDM = 0;
        double totN = 0;
        double totC = 0;
        GlobalVars.Instance.log("Fdcode FdAndProdProduced FdAndProdUsed  FdAndProdTradeBalance", 5);
        for (int i = 0; i < maxNumberFeedItems; i++)
        {
            if ((allFeedAndProductsProduced[i].composition.Getamount() != 0) || (allFeedAndProductsUsed[i].composition.Getamount() != 0))
            {
                totDM += allFeedAndProductsProduced[i].composition.Getamount();
                totN += allFeedAndProductsProduced[i].composition.Getamount() * allFeedAndProductsProduced[i].composition.GetN_conc();
                totC += allFeedAndProductsProduced[i].composition.Getamount() * allFeedAndProductsProduced[i].composition.GetC_conc();

                GlobalVars.Instance.log(allFeedAndProductTradeBalance[i].composition.GetFeedCode().ToString() + " " +
                    allFeedAndProductsProduced[i].composition.Getamount().ToString("0.0") + " " +
                    allFeedAndProductsUsed[i].composition.Getamount().ToString("0.0") + " " +
                     allFeedAndProductTradeBalance[i].composition.Getamount().ToString("0.0"), 5);
            }
        }
        GlobalVars.Instance.log("Tot DM produced " + totDM.ToString("0.0"), 5);
        GlobalVars.Instance.log("Tot C produced " + totC.ToString("0.0"), 5);
        GlobalVars.Instance.log("Tot N produced " + totN.ToString("0.0"), 5);
    }
    //! A normal member, Check Grazing Data.
    /*!
     \return an integer value.
  */
    public int CheckGrazingData()
    {
        double diff = 0;
        for (int i = 0; i < maxNumberFeedItems; i++)
        {
            if ((grazedArray[i].fieldDMgrazed > 0) || (grazedArray[i].ruminantDMgrazed > 0.0))
            {
                double expectedDM = grazedArray[i].fieldDMgrazed;
                double actualDM = grazedArray[i].ruminantDMgrazed;
                if (expectedDM == 0.0)
                {
                    string messageString = "Error; grazed feed item not produced on farm.\n feed code = " + i.ToString();
                    GlobalVars.instance.Error(messageString);

                }
                else
                {
                    diff = (expectedDM - actualDM) / expectedDM;
                    double tolerance = GlobalVars.Instance.getmaxToleratedErrorGrazing();
                    if ((tolerance <= 1) && (Math.Abs(diff) > tolerance))
                    {
                        double errorPercent = 100 * diff;
                        string productName = grazedArray[i].name;
                        string messageString = "";
                        if (expectedDM>actualDM)
                            messageString= "Error; modelled production of grazed DM exceeds livestock requirement for grazed DM for " +
                            productName + " by more than the permitted margin.\n Percentage error = " + errorPercent.ToString("0.00") + "%";
                        else
                            messageString = "Error; Livestock requirement for grazed DM exceeds modelled production of grazed DM for " +
                            productName + " by more than the permitted margin.\n Percentage error = " + errorPercent.ToString("0.00") + "%";
                        GlobalVars.instance.Error(messageString);
                    }
                }
            }
        }
        return 0;
    }
    //! A normal member, Return Temperature. Taking six arguments and returning a double value.
    /*!
     \param avgTemperature, a double argument.
     \param dampingDepth, a double argument.
     \param day, an integer argument.
     \param depth, a double argument.
     \param amplitude, a double argument.
     \param offset, an integer argument.
     \return a double value.
  */
    public double Temperature(double avgTemperature, double dampingDepth, int day, double depth, double amplitude, int offset)
    {
        double retVal = 0;
        double rho = 3.1415926 * 2.0 / 365.0;
        if (dampingDepth == 0)
            retVal = avgTemperature + amplitude * Math.Sin(rho * (day + offset));
        else
            retVal = avgTemperature + amplitude * Math.Exp(-depth / dampingDepth) * Math.Sin(rho * (day + offset) - depth / dampingDepth);
        return retVal;
    }

    //! A normal member, Get Degree Days. Taking six arguments and returning a double value.
    /*!
     \param startDay, an integer argument.
     \param endDay, an integer argument.
     \param basetemperature, a double argument.
     \param averageTemperature, a double argument.
     \param amplitude, a double argument.
     \param offset, an integer argument.
     \return a double value.
  */
    public double GetDegreeDays(int startDay, int endDay, double basetemperature, double averageTemperature, double amplitude, int offset)
    {
        double retVal = 0;
        for (int i = startDay; i <= endDay; i++)
            retVal += Temperature(averageTemperature, 0.0
                , i, 0.0, amplitude, offset) - basetemperature;
        return retVal;
    }
    //! A normal member, Get Concentrate Exports. Returning a double value.
    /*!
     \return a double value.
  */
    public double GetConcentrateExports()
    {
        double ret_val = 0;
        product aProduct = new product();
        for (int i = 0; i < maxNumberFeedItems; i++)
        {
            if (allFeedAndProductTradeBalance[i].composition.Getamount() > 0)
            {
                if (allFeedAndProductTradeBalance[i].composition.isConcentrate()) ;
                ret_val += allFeedAndProductTradeBalance[i].composition.Getamount();
            }
        }
        return ret_val;
    }
    //! A normal member, Write. Taking one argument.
    /*!
     \param fullModelRun, a boolean argument.
  */
    public void Write(bool fullModelRun)
    {
        theManureExchange.Write();
        if (!fullModelRun)
        {
            GlobalVars.Instance.writeStartTab("PotentialBalance");
            for (int i = 0; i < maxNumberFeedItems; i++)
            {
                if ((allFeedAndProductsUsed[i].composition.Getamount() > 0) || (allFeedAndProductsPotential[i].composition.Getamount() > 0))
                {
                    GlobalVars.Instance.writeStartTab("FeedItem");
                    GlobalVars.Instance.writeInformationToFiles("name", "Name", "-", allFeedAndProductsUsed[i].composition.GetName(), parens);
                    GlobalVars.Instance.writeInformationToFiles("Used", "Used", "kg", allFeedAndProductsUsed[i].composition.Getamount(), parens);
                    GlobalVars.Instance.writeInformationToFiles("Potential", "Potential", "kg", allFeedAndProductsPotential[i].composition.Getamount(), parens);
                    GlobalVars.Instance.writeInformationToFiles("Expected", "Expected", "kg", allFeedAndProductsProduced[i].composition.Getamount(), parens);
                    GlobalVars.Instance.writeEndTab();
                }
            }
            GlobalVars.Instance.writeEndTab();
        }

        if (fullModelRun)
        {
            GlobalVars.Instance.writeStartTab("FeedAndProductsUsed");
            for (int i = 0; i < maxNumberFeedItems; i++)
            {
                if (allFeedAndProductsUsed[i].composition.Getamount() > 0)
                {
                    allFeedAndProductsUsed[i].composition.Write("Used");
                }
            }
            GlobalVars.Instance.writeEndTab();
            GlobalVars.Instance.writeStartTab("FeedAndProductsProduced");
            for (int i = 0; i < maxNumberFeedItems; i++)
            {
                if (allFeedAndProductsProduced[i].composition.Getamount() > 0)
                {
                    allFeedAndProductsProduced[i].composition.Write("Produced");
                }
            }
            GlobalVars.Instance.writeEndTab();
            GlobalVars.Instance.writeStartTab("FeedAndProductTradeBalance");
            for (int i = 0; i < maxNumberFeedItems; i++)
            {
                if ((allFeedAndProductsUsed[i].composition.Getamount() > 0) || (allFeedAndProductsProduced[i].composition.Getamount() > 0))
                {
                    allFeedAndProductTradeBalance[i].composition.Write("Balance");
                }
            }
            GlobalVars.Instance.writeEndTab();
            GlobalVars.Instance.writeStartTab("FeedAndProductsPotential");
            for (int i = 0; i < maxNumberFeedItems; i++)
            {
                if (allFeedAndProductsPotential[i].composition.Getamount() > 0)
                    allFeedAndProductsPotential[i].composition.Write("Potential");
            }
            GlobalVars.Instance.writeEndTab();
            GlobalVars.Instance.writeStartTab("FeedAndProductsField");
            for (int i = 0; i < maxNumberFeedItems; i++)
            {
                if (allFeedAndProductFieldProduction[i].composition.Getamount() > 0)
                    allFeedAndProductFieldProduction[i].composition.Write("Field");
            }
            GlobalVars.Instance.writeEndTab();

            GlobalVars.Instance.writeStartTab("allUnutilisedGrazableFeed");
            for (int i = 0; i < maxNumberFeedItems; i++)
            {
                if (allUnutilisedGrazableFeed[i].composition.Getamount() > 0)
                    allUnutilisedGrazableFeed[i].composition.Write("UnutilisedGrazableFeed");
            }
            GlobalVars.Instance.writeEndTab();
        }
    }
    //! A normal member, Log. Taking two arguments.
    /*!
     \param informatio, a string argument.
     \param level, an integer argument.
  */
    public void log(string informatio, int level)
    {
        if (level <= verbosity)
        {
            if (logScreen)
                Console.WriteLine(informatio);
            if (logFile)
            {
                try
                {
                    logFileStream.WriteLine(informatio);

                }
                catch
                {

                }

            }
        }

    }
    //! A normal member, Write Summary Excel. Taking three arguments.
    /*!
     \param information, a string argument.
     \param units, a string argument.
     \param amount, a double argument.
  */
    public void writeSummaryExcel(string information, string units, double amount)
    {

        writeSummaryExcel(information, units, Convert.ToString(amount));

    }
    //! A normal member, Write Summary Excel. Taking three arguments.
    /*!
     \param information, a string argument.
     \param units, a string argument.
     \param amount, a string argument.
  */
    public void writeSummaryExcel(string information, string units, string amount)
    {
        if (WriteSummaryExcel)
            SummaryExcel.WriteLine(information + '\t' + units + '\t' + amount);

    }
    //! A normal member, Open Summary Excel. Taking three arguments.
    /*!
     \param outputDir, a string argument.
     \param scenarioNr, a string argument.
     \param farmNr, a string argument.
  */
    public void openSummaryExcel(string outputDir, string scenarioNr, string farmNr)
    {
        if (WriteSummaryExcel)
            SummaryExcel = new System.IO.StreamWriter(outputDir + "SummaryExcel" + farmNr + "_" + scenarioNr + ".xls");
    }
    //! A normal member, Close Summary Excel. 
    public void closeSummaryExcel()
    {
        try
        {
            if (WriteSummaryExcel)
                SummaryExcel.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            log(e.Message, 6);
            log("Cannot close summary Excel file", 6);
        }
    }
}