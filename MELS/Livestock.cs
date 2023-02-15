using System;
using System.Collections.Generic;
using System.Xml;
//! A class named livestock.

public class livestock
{
    //! A normal structure named ManureRecipient.
    
    public struct ManureRecipient
    {
        int ManureStorageID;
        string ManureStorageName;
        string parens;
        public void setParens(string aParens){parens=aParens;}
        public void setManureStorageID(int aType)
        {
            ManureStorageID = aType;
        }
        public int GetStorageType() { return ManureStorageID; }
        public void setManureStorageName(string aName) { ManureStorageName = aName; }
        public string getManureStorageName() { return ManureStorageName; }
        public void WriteXML()
        {
            GlobalVars.Instance.writeStartTab("ManureRecipient");
            GlobalVars.Instance.writeInformationToFiles("StorageType", "Type of manure store", "-", ManureStorageID, parens);
            GlobalVars.Instance.writeEndTab();
        }
        public void WriteXls()
        {
            GlobalVars.Instance.writeLivestockFile("StorageType", "Type of manure store", "-", ManureStorageID, "livestock", 0);
            GlobalVars.Instance.writeLivestockFile("ManureStorageName", "ManureStorageName", "-", ManureStorageName, "livestock", 0);
        }
    }
    //! A normal structure named housingRecord.
    public struct housingRecord
    {
        int HousingType;
        double propTime;
        string NameOfHousing;
        string parens;
        public List<ManureRecipient> Recipient;
        public void SetNameOfHousing(string aName) { NameOfHousing = aName; }
        public void SetHousingType(int aVal) { HousingType = aVal; }
        public void SetpropTime(double aVal) { propTime = aVal; }
        public int GetHousingType() { return HousingType; }
        public string GetHousingName() { return NameOfHousing; }
        public double GetpropTime() { return propTime; }
        public List<ManureRecipient> GetManureRecipient() { return Recipient; }
        public void setParens(string aParens) { 
            parens = aParens; 
        }
        public void WriteXML()
        {
            GlobalVars.Instance.writeStartTab("housingRecord");
            GlobalVars.Instance.writeInformationToFiles("HousingType", "Type of housing", "-", HousingType, parens);
            GlobalVars.Instance.writeInformationToFiles("propTime", "Proportion of time spent in house", "-", propTime, parens);
            for (int i = 0; i < Recipient.Count; i++)
                Recipient[i].WriteXML();
            GlobalVars.Instance.writeEndTab();
        }
        public void WriteXls()
        {
            GlobalVars.Instance.writeLivestockFile("NameOfHousing", "NameOfHousing", "-", NameOfHousing, "livestock", 0);
            GlobalVars.Instance.writeLivestockFile("propTime", "Proportion of time spent in house", "-", propTime, "livestock", 0);
            for (int i = 0; i < Recipient.Count; i++)
                Recipient[i].WriteXls();
        }
    }
    //! A normal structure named item.
    public struct item
    {
        int feedCode;
        double amount;
        public void SetfeedCode(int aVal) { feedCode = aVal; }
        public void Setamount(double aVal) { amount = aVal; }
        public int GetfeedCode() { return feedCode; }
        public double Getamount() { return amount; }
    }

    string path;

    ///characteristics of livestock
    bool isRuminant;
    bool isDairy; // true if this is a milk-producing animal
    bool inputProduction; //true if the production (meat or milk) is an input
    double mu_base;   //energy intake level below which there is no reduction in energy utilisation
    double mu_b; //
    ///input data
    double avgNumberOfAnimal;

    double avgProductionMilk;
    double avgProductionMeat;
    double avgProductionECM;
    double efficiencyProteinMilk;
    List<housingRecord> housingDetails;
    ///parameters
    int identity;
    private string parens;
    int speciesGroup;
    int LivestockType; //finding parameter for this type
    double liveweight;
    double startWeight;
    double endWeight;
    double numberWeaners;
    double endWeightWeaners;
    double duration;
    double urineProp;
    string name;
    double growthNconc;
    double growthCconc;
    double milkNconc;
    double milkCconc;
    double milkFat;
    double age;
    double maintenanceEnergyCoeff;
    double growthEnergyDemandCoeff;
    double milkAdjustmentCoeff;
    double mortalityCoefficient;
    double entericTier2MCF;
    double Bo;
    double nitrateEfficiency;
    double propExcretaField =-1.0;
    ///other variables
    double energyIntake;
    double energyDemand;
    double energyUseForMaintenance;
    double energyUseForGrowth;
    double energyUseForMilk;
    double energyUseForGrazing;
    double energyFromRemobilisation;
    double maintenanceEnergyDeficit;
    double growthEnergyDeficit;
    double concentrateEnergy;
    double maxNuseEfficiency;

    double DMintake;
    double DMintake_IPCC2019;
    double DMgrazed;
    double FE;
    double concentrateDM;
    double Nintake;
    double diet_ash;
    double diet_fibre;
    double diet_fat;
    double diet_NDF;
    double digestibilityDiet;
    double diet_nitrate;//kg/kg
    double Cintake;
    double energyLevel;
    double milkN;
    double milkC;
    double growthN;
    double growthC;
    double mortalitiesN;
    double mortalitiesC;
    double urineC;
    double urineN;
    double faecalC;
    double CCH4GR;
    double faecalN;
    double NexcretionToPasture;
    double CexcretionToPasture;
    double CH4C;
    double CO2C;
    //! Intake of N in grazed feed
    double grazedN = 0;
    //! Intake of C in grazed feed
    double grazedC = 0;
    //! Intake of DM in grazed feed
    double grazedDM = 0;
    //!Intake of N in feed fed at pasture
    double pastureFedN = 0;
    //!Intake of C in feed fed at pasture
    double pastureFedC = 0;
    double mexp =0; ///Mass of manure from species group sg 
    double cman =0;/// Mass of manure from species group sg
    double nman = 0;
    double vsg=0;///Annual production of manure from species group sg and store type s
    bool proteinLimited;
    double VS = 0;
    public double GetVS() { return VS;}
    //! A normal member, Get isDairy. Returning one boolean value.
    /*!
     \return a boolean value.
    */
    public bool GetisDairy() { return isDairy; }
    //! A normal member, Get CH4C. Returning one double value.
    /*!
     \return a double value.
    */
    public double getCH4C() { return CH4C; }
    //! A normal member, Get CO2C. Returning one double value.
    /*!
     \return a double value.
    */
    public double getCO2C() { return CO2C; }
    //! A normal member, Get CmanExp. Returning one double value.
    /*!
     \return a double value.
    */
    public double getCmanExp() { return mexp * cman * vsg; }
    //! A normal member, Get NmanExp. Returning one double value.
    /*!
     \return a double value.
    */
    public double getNmanExp() { return mexp * nman * vsg; }
    public double propDMgrazed;
    //! A normal member, Set avgProductionMilk. Taking one argument.
    /*!
     \param anavgProductionMilk, one double argument.
    */
    public void SetavgProductionMilk(double anavgProductionMilk) { avgProductionMilk = anavgProductionMilk; }
    //! A normal member, Set avgProductionMeat. Taking one argument.
    /*!
     \param anavgProductionMeat, one double argument.
    */
    public void SetavgProductionMeat(double anavgProductionMeat) { avgProductionMeat = anavgProductionMeat; }
    //! A normal member, Set Name. Taking one argument.
    /*!
     \param aname, one string argument.
    */
    public void Setname(string aname) { name = aname; }
    //! A normal member, Set identity. Taking one argument.
    /*!
     \param aValue, one integer argument.
    */
    public void Setidentity(int aValue) { identity = aValue; }
    //! A normal member, Set speiciesGroup. Taking one argument.
    /*!
     \param aValue, one integer argument.
    */
    public void SetspeciesGroup(int aValue) { speciesGroup = aValue; }
    //! A normal member, Set propExcretaField. Taking one argument.
    /*!
     \param aValue, one double argument.
    */
    public void SetpropExcretaField(double aValue) { propExcretaField = aValue; }
    //! A normal member, Get MilkC. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetMilkC() { return milkC; }
    //! A normal member, Get GrowthC. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetGrowthC() { return growthC; }
    //! A normal member, Get MilkC. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetMilkN() { return milkN; }
    //! A normal member, Get GrowthN. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetGrowthN() { return growthN; }
    //! A normal member, Get MortalitiesN. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetMortalitiesN() { return mortalitiesN; }
    //! A normal member, Get MortalitiesC. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetMortalitiesC() { return mortalitiesC; }
    //! A normal member, Get mortality Coefficient. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetmortalityCoefficient() {return mortalityCoefficient;}
    //! A normal member, Get avgProductionMilk. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetavgProductionMilk() { return avgProductionMilk; }
    //! A normal member, Get avgProductionMeat. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetavgProductionMeat() { return avgProductionMeat; }
    //! A normal member, Get grazedN. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetgrazedN() { return grazedN; }
    //! A normal member, Get pastureFedN. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetpastureFedN() { return pastureFedN; }
    //! A normal member, Get grazedC. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetgrazedC() { return grazedC; }
    //! A normal member, Get pastureFedC. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetpastureFedC() { return pastureFedC; }
    //! A normal member, Get propExcretaField. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetpropExcretaField() { return propExcretaField; }
    //! A normal member, Get grazedDM. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetgrazedDM() { return grazedDM; }
    //! A normal member, Get Nexcretion to Pasture. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetNexcretionToPasture() { return NexcretionToPasture; }
    //! A normal member, Get Cexcretion to Pasture. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetCexcretionToPasture() { return CexcretionToPasture; }
    //! A normal member, Set isRuminant. Taking one argument.
    /*!
     \param aVal, one boolean argument.
    */
    public void SetisRuminant(bool aVal) { isRuminant = aVal; }
    List<feedItem> feedRation;
    //! A normal member, Set feedRation. Taking one argument.
    /*!
     \param afeedRation, one list argument that points to class feedItem.
    */
    public void SetfeedRation(List<feedItem> afeedRation) { feedRation = afeedRation; }

    //public void SethousingDetail(int houseID, double proportion)
    //{
    //    housingRecord ahousingRecord = new housingRecord();
    //    ahousingRecord.SetHousingType(houseID);
    //    ahousingRecord.SetpropTime(proportion);
    //    housingDetails.Add(ahousingRecord);
    //}
    //! A normal member, Get isRuminant. Returning one boolean value.
    /*!
     \return a boolean value.
    */
    public bool GetisRuminant() { return isRuminant; }
    //! A normal member, Get liveweight. Returning one double value.
    /*!
     \return a double value.
    */
    public double Getliveweight() { return liveweight; }
    //! A normal member, Get DMintake. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetDMintake() { return DMintake; }
    //! A normal member, Get DMintake based on IPCC2019. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetDMintakeIPCC2019(int livestockType) { 
        //MELS-2023

        double dailyenergyIntake = energyIntake/GlobalVars.avgNumberOfDays;
        switch (livestockType)
        {
            case 1:
            case 4:
            case 21:
                DMintake_IPCC2019 = (0.0185*liveweight) + (0.305*milkFat);
            break;
            
            case 15:
                DMintake_IPCC2019 = 3.184 + (0.01536*liveweight*0.96);
            break;
            case 3:
            case 6:
                DMintake_IPCC2019 = 3.184 + (0.0143*liveweight*0.96);
                
            break;
        }

        return DMintake_IPCC2019; 
    }

    public double GetDMintakeIPCC2019Growing() { 
        //MELS-2023
        double dailyenergyIntake = energyIntake/GlobalVars.avgNumberOfDays;
        return liveweight*0.75*( ((0.0582*dailyenergyIntake - 0.00266*(dailyenergyIntake*dailyenergyIntake) - 0.0869)) / (0.239*dailyenergyIntake) ); 
    }

    public double GetDMintakeIPCC2019Calves() { 
        //MELS-2023

        double dailyenergyIntake = energyIntake/GlobalVars.avgNumberOfDays;
        return liveweight*0.75*( ((0.0582*dailyenergyIntake - 0.00266*(dailyenergyIntake*dailyenergyIntake) - 0.1128)) / (0.239*dailyenergyIntake) );
    }
    //! A normal member, Get Digestibility. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetDigestibility() { return digestibilityDiet; }
    //! A normal member, Get Diet ash. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetDietAsh() { return diet_ash; }
    //! A normal member, Get DMgrazed. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetDMgrazed() { return DMgrazed; }
    //! A normal member, Get Cintake. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetCintake() { return Cintake; }
    //! A normal member, Get Nintake. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetNintake() { return Nintake; }
    //! A normal member, Get urineC. Returning one double value.
    /*!
     \return a double value.
    */
    public double GeturineC() { return urineC; }
    //! A normal member, Get faecalC. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetfaecalC() { return faecalC; }
    //! A normal member, Get Fibre. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetFibre() { return diet_fibre; }
    //! A normal member, Get urineN. Returning one double value.
    /*!
     \return a double value.
    */
    public double GeturineN() { return urineN; }
    //! A normal member, Get faecalN. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetfaecalN() { return faecalN; }
    //! A normal member, Get ExcretedN. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetExcretedN() { return faecalN + urineN; }
    //! A normal member, Get AvgNumberOfAnimal. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetAvgNumberOfAnimal(){return avgNumberOfAnimal;}
    //! A normal member, Get propDMgrazed. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetpropDMgrazed() { return propDMgrazed; }
    //! A normal member, Get feedRation. Returning one list value.
    /*!
     \return a list value that points to class feedItem.
    */
    public List<feedItem> GetfeedRation() { return feedRation; }
    //! A normal member, Get Bo. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetBo() { return Bo; }
    //! A normal member, Get Name. Returning one string value.
    /*!
     \return a string value.
    */
    public string Getname() { return name; }
    //! A normal member, Get identity. Returning one integer value.
    /*!
     \return a integer value.
    */
    public int Getidentity() { return identity; }
    //! A normal member, Get speiciesGroup. Returning one integer value.
    /*!
     \return a integer value.
    */
    public int GetLiveStockIdentity() { return LivestockType; }
    public int GetspeciesGroup() { return speciesGroup; }
    //! A normal member, Get housing Details. Returning one list value.
    /*!
     \return a list value that points to class housingRecord.
    */
    public List<housingRecord> GethousingDetails() { return housingDetails; }
    //! A constructor.
    /*!
     without argument.
    */
    public livestock()
    {
    }
    //! A constructor with five arguments.
    /*!
     \param aPath, a string argument.
     \param id, an integer argument.
     \param zoneNr, an integer argument.
     \param AnimalNr, an integer argument.
     \param aparens, a string argument.
    */
    public livestock(string aPath, int id, int zoneNr, int AnimalNr, string aParens)
    {
        parens = aParens;
        FileInformation livestockFile =new FileInformation(GlobalVars.Instance.getFarmFilePath());
        identity = id;
        path = aPath+"("+id.ToString()+")";
        livestockFile.setPath(path);
        feedRation = new List<feedItem>();
        urineProp = 0;
        DMintake =0;
        DMgrazed = 0;
        energyDemand = 0;
        energyIntake = 0;
        diet_ash = 0;
        diet_nitrate = 0;
        digestibilityDiet = 0;
        propDMgrazed = 0;
        proteinLimited = false;
        name = livestockFile.getItemString("NameOfAnimals");
        avgNumberOfAnimal = livestockFile.getItemDouble("NumberOfAnimals");
        housingDetails = new List<housingRecord>();
        if (avgNumberOfAnimal > 0)
        {
            LivestockType = livestockFile.getItemInt("LivestockType");
            speciesGroup = livestockFile.getItemInt("Species_group");
            if ((speciesGroup == 2)&&(LivestockType==1))
            {
                numberWeaners = livestockFile.getItemDouble("ProductionLevel");
                endWeightWeaners = livestockFile.getItemDouble("ProductionLevel2");
            }

            FileInformation paramFile = new FileInformation(GlobalVars.Instance.getParamFilePath());

            //read livestock parameters from constants.xml
            string basePath = "AgroecologicalZone(" + zoneNr.ToString() + ").Livestock";
            int min = 99, max = 0;
            paramFile.setPath(basePath);
            paramFile.getSectionNumber(ref min, ref max);
            bool gotit = false;
            int livestockID = 0;
            for (int i = min; i <= max; i++)
            {
                if (paramFile.doesIDExist(i))
                {
                    string testPath = basePath + "(" + i.ToString() + ").LivestockType(0)";
                    int testLivestockType = paramFile.getItemInt("Value", testPath);
                    testPath = basePath + "(" + i.ToString() + ").SpeciesGroup(0)";
                    int testspeciesGroup = paramFile.getItemInt("Value", testPath);
                    if ((testLivestockType == LivestockType) && (testspeciesGroup == speciesGroup))
                    {
                        livestockID = i;
                        gotit = true;
                        break;
                    }
                    paramFile.setPath(basePath);
                }
            }
            if (gotit == false)
            {
                string messageString = ("Livestock " + name + " Species " + speciesGroup.ToString() + ", Livestocktype  " + LivestockType.ToString() + " not found in parameters.xml");
                GlobalVars.Instance.Error(messageString);
            }
            basePath = "AgroecologicalZone(" + zoneNr.ToString() + ").Livestock(" + Convert.ToInt32(livestockID) + ")";
            //paramFile.setPath(basePath + ".SpeciesGroup(0)");
            //speciesGroup = paramFile.getItemInt("Value");
            paramFile.setPath(basePath + ".efficiencyProteinMilk(0)");
            efficiencyProteinMilk = paramFile.getItemDouble("Value");
            
            paramFile.setPath(basePath + ".isRuminant(0)");
            isRuminant = paramFile.getItemBool("Value");
            paramFile.setPath(basePath + ".isDairy(0)");
            isDairy = paramFile.getItemBool("Value");
            paramFile.setPath(basePath + ".growthNconc(0)");
            growthNconc = paramFile.getItemDouble("Value"); 
            paramFile.setPath(basePath + ".growthCconc(0)");
            growthCconc = paramFile.getItemDouble("Value");
            paramFile.setPath(basePath + ".urineProp(0)");
            urineProp = paramFile.getItemDouble("Value");
            paramFile.setPath(basePath + ".maintenanceEnergyCoeff(0)");
            maintenanceEnergyCoeff = paramFile.getItemDouble("Value");
            paramFile.setPath(basePath + ".growthEnergyDemandCoeff(0)");
            growthEnergyDemandCoeff = paramFile.getItemDouble("Value");
            if (isDairy)
            {
                paramFile.setPath(basePath + ".milkAdjustmentCoeff(0)");
                milkAdjustmentCoeff = paramFile.getItemDouble("Value");
                paramFile.setPath(basePath + ".milkFat(0)");
                milkFat = paramFile.getItemDouble("Value");
            }
            paramFile.setPath(basePath + ".Liveweight(0)");
            liveweight = paramFile.getItemDouble("Value");
            paramFile.setPath(basePath + ".Age(0)");
            age = paramFile.getItemDouble("Value");
            paramFile.setPath(basePath + ".Mortality(0)");
            mortalityCoefficient = paramFile.getItemDouble("Value");
            entericTier2MCF = paramFile.getItemDouble("Value", basePath + ".entericTier2MCF(-1)");
            Bo = paramFile.getItemDouble("Value", basePath + ".Bo(-1)");
            if (isRuminant)
            {
                paramFile.setPath(basePath + ".mu_b(0)");
                mu_b = paramFile.getItemDouble("Value");
                paramFile.setPath(basePath + ".mu_base(0)");
                mu_base = paramFile.getItemDouble("Value");
                paramFile.setPath(basePath + ".milkNconc(0)");
                milkNconc = paramFile.getItemDouble("Value");
                paramFile.setPath(basePath + ".milkCconc(0)");
                milkCconc = paramFile.getItemDouble("Value");
                paramFile.setPath(basePath + ".nitrateEfficiency(0)");
                nitrateEfficiency = paramFile.getItemDouble("Value");
                //nitrateEfficiency
            }
            //back to reading user input
            if (isDairy)
            {
                //avgProductionMilk = livestockFile.getItemDouble("avgProductionMilk");
                paramFile.setPath(basePath + ".weightGainDairy(0)");
                avgProductionMeat = paramFile.getItemDouble("Value");
                avgProductionMeat /= GlobalVars.avgNumberOfDays;
            }
            else
            {
                if (speciesGroup == 1)
                    avgProductionMeat = livestockFile.getItemDouble("avgProductionMeat");
                else
                if (speciesGroup == 2)
                {
                    paramFile.setPath(basePath + ".ProductionCycle(0).Startweight(0)");
                    startWeight = paramFile.getItemDouble("Value");
                    paramFile.setPath(basePath + ".ProductionCycle(0).Endweight(0)");
                    endWeight = paramFile.getItemDouble("Value");
                    paramFile.setPath(basePath + ".ProductionCycle(0).Duration(0)");
                    duration = paramFile.getItemDouble("Value");
                    paramFile.setPath(basePath + ".ProductionCycle(0).MaxLiveweightGain(0)");
                    double MaxLiveweightGain = paramFile.getItemDouble("Value");

                    avgProductionMeat = (endWeight - startWeight) / duration;
                    if (avgProductionMeat > MaxLiveweightGain)
                    {
                        string messageString = ("Growth rate of " + name + " is greater than the maximum permitted");
                        GlobalVars.Instance.Error(messageString);
                    }
                    if (LivestockType == 1)
                        avgProductionMeat += endWeightWeaners * numberWeaners / duration;
                    paramFile.setPath(basePath + ".ProductionCycle(0).MaxNuseEfficiency(0)");
                    maxNuseEfficiency = paramFile.getItemDouble("Value");

                }
            }

            
            string housingPath = path + ".Housing";
            min = 99;
            max = 0;
            livestockFile.setPath(housingPath);
            livestockFile.getSectionNumber(ref min, ref max);
            if (max > 0)
            {
                double testPropTime = 0;
                for (int i = min; i <= max; i++)
                {
                    if (livestockFile.doesIDExist(i))
                    {
                        housingRecord newHouse = new housingRecord();
                        newHouse.setParens(parens + "_housingRecord" + i.ToString());
                        livestockFile.Identity.Add(i);
                        newHouse.SetHousingType(livestockFile.getItemInt("HousingType"));
                        newHouse.SetNameOfHousing(livestockFile.getItemString("NameOfHousing"));
                        if (newHouse.GetHousingName() != "None")
                        {
                            newHouse.SetpropTime(livestockFile.getItemDouble("PropTime"));
                            testPropTime += newHouse.GetpropTime();
                            int maxManureRecipient = 0, minManureRecipient = 99;
                            newHouse.Recipient = new List<ManureRecipient>();
                            string RecipientPath = housingPath + '(' + i.ToString() + ").ManureRecipient";
                            livestockFile.setPath(RecipientPath);
                            livestockFile.getSectionNumber(ref minManureRecipient, ref maxManureRecipient);
                            for (int j = minManureRecipient; j <= maxManureRecipient; j++)
                            {
                                if (livestockFile.doesIDExist(j))
                                {
                                    ManureRecipient newRecipient = new ManureRecipient();
                                    newRecipient.setParens(parens + "_ManureRecipientI" + i.ToString() + "_ManureRecipientJ" + j.ToString());
                                    livestockFile.Identity.Add(j);
                                    int type = livestockFile.getItemInt("StorageType");
                                    newRecipient.setManureStorageID(type);
                                    string manurestoreName = livestockFile.getItemString("StorageName");
                                    newRecipient.setManureStorageName(manurestoreName);
                                    newHouse.Recipient.Add(newRecipient);
                                    livestockFile.Identity.RemoveAt(livestockFile.Identity.Count - 1);
                                }
                            }
                            housingDetails.Add(newHouse);
                            livestockFile.setPath(housingPath);
                        }
                        else
                        {
                            testPropTime = 1.0;
                            livestockFile.Identity.RemoveAt(livestockFile.Identity.Count - 1);
                        }
                    }
                }
                if (testPropTime != 1.0)
                {
                    string messageString = ("Sum of proportions of time in different housing does not equal 1.0 ");
                    GlobalVars.Instance.Error(messageString);
                }
            }
            ///read livestock input variables from input xml file
            string feeditemPath = path + ".itemFed";
            min = 99;
            max = 0;
            livestockFile.setPath(feeditemPath);
            livestockFile.getSectionNumber(ref min, ref max);
            for (int i = min; i <= max; i++)
            {
                if (livestockFile.doesIDExist(i))
                {
                    ///find the feed code for the first feed item

                    feedItem newFeedItem = new feedItem(feeditemPath, i, true,parens+"_"+i.ToString());
                    //if there is no housing or corralling, all feed is fed at pasture
                    if ((housingDetails.Count == 0) && (newFeedItem.GetisGrazed() == false))
                        newFeedItem.SetfedAtPasture(true);
                    feedRation.Add(newFeedItem);
                }
            }
        } ///end if average number of animals >0
    }

    //! a normal member. Calculate daily main tenance energy.
    /*!
     \return a double value.
    */
    double dailymaintenanceEnergy() //MJ per animal
    {
        double maintenanceEnergy = 0;
        switch (GlobalVars.Instance.getcurrentEnergySystem())
        {
            case 1:
            case 2:
                if (speciesGroup == 1)
                {
                    double efficiencyMaintenance = 0.02 * energyIntake / DMintake + 0.5;
                    double dailyEnergyIntake = energyIntake / GlobalVars.avgNumberOfDays;
                    if (age < 6.0)
                        maintenanceEnergy = maintenanceEnergyCoeff * (0.26 * Math.Pow(liveweight, 0.75) * Math.Exp(-0.03 * age)) / efficiencyMaintenance
                            + 0.09 * (energyIntake / GlobalVars.avgNumberOfDays);//SRC (1990) eq 1.21, minus EGRAZE and ECOLD
                    else
                        maintenanceEnergy = maintenanceEnergyCoeff * (0.26 * Math.Pow(liveweight, 0.75) * Math.Exp(-0.03 * 6.0)) / efficiencyMaintenance
                            + 0.09 * (energyIntake / GlobalVars.avgNumberOfDays);//SRC (1990) eq 1.21, minus EGRAZE and ECOLD
                }
                if (speciesGroup == 2)
                {
                    maintenanceEnergy = 0.44 * Math.Pow(liveweight, 0.75);
                }
                if (speciesGroup == 3)
                {
                    double efficiencyMaintenance = 0.02 * energyIntake / DMintake + 0.5;
                    double dailyEnergyIntake = energyIntake / GlobalVars.avgNumberOfDays;
                    if (age < 6.0)
                        maintenanceEnergy = maintenanceEnergyCoeff * (0.26 * Math.Pow(liveweight, 0.75) * Math.Exp(-0.03 * age)) / efficiencyMaintenance
                            + 0.09 * (energyIntake / GlobalVars.avgNumberOfDays);//SRC (1990) eq 1.21, minus EGRAZE and ECOLD
                    else
                        maintenanceEnergy = maintenanceEnergyCoeff * (0.26 * Math.Pow(liveweight, 0.75) * Math.Exp(-0.03 * 6.0)) / efficiencyMaintenance
                            + 0.09 * (energyIntake / GlobalVars.avgNumberOfDays);//SRC (1990) eq 1.21, minus EGRAZE and ECOLD
                }
                break;
      
            default: 
                    string messageString=("Energy system for " + name + " not found");
                    GlobalVars.Instance.Error(messageString);
                    break;

        }
        return maintenanceEnergy;
    }
    //! a normal member, calculate daily Growth Energy
    /*!
     \return a double value.
    */
    double dailyGrowthEnergyPerkg() //MJ per kg
    {
        double growthEnergyPerkg = 0;
        switch (GlobalVars.Instance.getcurrentEnergySystem())
        {
            case 1:
            case 2:
                if (speciesGroup == 1)
                {
                    double efficiencyGrowth = 0.042 * energyIntake / DMintake + 0.006;//SCA 1990 1.38,
                    growthEnergyPerkg = growthEnergyDemandCoeff / efficiencyGrowth;
                }
                if (speciesGroup == 2)
                    growthEnergyPerkg = 24.0; // 47.0;
                break;

            default:
                string messageString=("Energy system for livestock not found");
                GlobalVars.Instance.Error(messageString);
                break;

        }
        return growthEnergyPerkg;
    }
    //! a normal member, calculate daily Milk Energy
    /*!
     \return a double value.
    */
    double dailyMilkEnergyPerkg()//MJ per kg
    {
        double milkEnergyPerkg = 0;
        double milkEnergyContentPerkg=0;
        switch (speciesGroup)
        {
            case 1: milkEnergyContentPerkg = GlobalVars.Instance.GetECM(1, milkFat/10, milkNconc * 6.38 * 100) * 3.054;//Australian standards state 3.054 MJ/kg ECM
                break;
            case 2: break;
            case 3: milkEnergyContentPerkg = 0.0328 * milkFat + 0.0025 * 42 /*assume 6 weeks for day of lactation*/ + 2.203;
                break;
        }
        switch (GlobalVars.Instance.getcurrentEnergySystem())
        {
            case 1:
            case 2: double efficiencyMilk = (0.02 * energyIntake / DMintake + 0.4);//SCA 1990 1.48
                milkEnergyPerkg = milkAdjustmentCoeff * milkEnergyContentPerkg / efficiencyMilk; // milkAdjustmentCoeff is  Multiplier to increase (>1) or decrease (<1) the energy requirement per unit of milk produced
                break;
            default: 
                string messageString=("Energy system for livestock not found");
                GlobalVars.Instance.Error(messageString);
                break;
        }
        return milkEnergyPerkg;
    }
    //! a normal member, calculate daily Energy Remobilisation.
    /*!
     \return a double value.
    */
    double dailyEnergyRemobilisation(double weightLoss)//MJ ME/day
    {
        double energyRemobilisation = weightLoss * growthEnergyDemandCoeff * 0.84;
        return energyRemobilisation;
    }
    //! a normal member, calculate daily weitht loss.
    /*!
     \return a double value.
    */

    double dailyWeightLoss(double remobilisedEnergy)//MJ ME/day
    {
        avgProductionMeat = remobilisedEnergy / (growthEnergyDemandCoeff * 0.84);
        return avgProductionMeat;
    }
    //! a normal member, calculate daily Energy for Grazing.
    /*!
     \return a double value.
    */
    double dailyEnergyForGrazing() //MJ ME/day
    {
        double retVal = 0;
        return retVal;
    }
    //! a normal member, calculate Energy Level.
    /*!
     more details.
    */
    void calcEnergyLevel()
    {
        energyLevel=energyIntake/(dailymaintenanceEnergy() * GlobalVars.avgNumberOfDays);
    }
    //! a normal member, calculate endogenous Faecal Protein.
    /*!
     \return a double value.
    */
    double dailyEndogenousFaecalProtein()//g per animal/day
    {
        double endogenousFaecalProtein = 0;
        endogenousFaecalProtein = 15.2 * DMintake / GlobalVars.avgNumberOfDays;
        return endogenousFaecalProtein;
    }
    //! a normal member, calculate daily Faecal Protein.
    /*!
     \return a double value.
    */
    double dailyFaecalProtein()//g per animal per day - RedNex equation
    {
        double dailyDMI=DMintake/GlobalVars.avgNumberOfDays;
        double dailyNintake=1000*Nintake/GlobalVars.avgNumberOfDays;
        double faecalProtein = 0;
        if (dailyDMI < 5)
        {
            double faecalProtAtFiveKgDMI = 6.25 * (6.3 * 5 + 0.17 * 5.0 * (dailyNintake/dailyDMI) - 31.0);
            faecalProtein = (dailyDMI / 5.0) * faecalProtAtFiveKgDMI;
        }
        else
            faecalProtein = 6.25 * (6.3 * dailyDMI + 0.17 * dailyNintake - 31.0);

        faecalProtein = 6.25 * (0.04 * dailyNintake + (dailyDMI * dailyDMI * 1.8 / 6.25) + dailyDMI * 20.0 / 6.25);
        return faecalProtein;
    }
    //! a normal member, calculate daily Endogenous Urinary Protein.
    /*!
     \return a double value.
    */
    double dailyEndogenousUrinaryProtein()//g per animal/day
    {
        double endogenousUrinaryProtein = 0;
        switch (speciesGroup)
        {
            case 1:
                endogenousUrinaryProtein = 16.1 * Math.Log(liveweight) - 42.2;
                break;
            case 3:
                endogenousUrinaryProtein = 0.147 * liveweight + 3.375;
                break;
            default:
                string messageString1 = ("Protein system for livestock not found");
                GlobalVars.Instance.Error(messageString1);
                break;
        }
        return endogenousUrinaryProtein;
    }
    //! a normal member, calculate daily maintenance Protein.
    /*!
     \return a double value.
    */
    double dailymaintenanceProtein() //g per animal/day
    {
        double maintenanceProtein = 0;
        double endogenousUrinaryProtein = 0;
        double endogenousFaecalProtein = 0;
        switch (GlobalVars.Instance.getcurrentEnergySystem())
        {
            case 1:
            case 2:
                double efficiencyMaintenance = 0.7; //from Australian feeding standards
                endogenousUrinaryProtein= dailyEndogenousUrinaryProtein();
                endogenousFaecalProtein = dailyEndogenousFaecalProtein();
                maintenanceProtein = (endogenousUrinaryProtein + endogenousFaecalProtein) / efficiencyMaintenance;
                break;

            default:
                string messageString2 = ("Protein system for livestock not found");
                GlobalVars.Instance.Error(messageString2);
                break;

        }
        return maintenanceProtein;
    }
    //! a normal member, calculate daily Milk Protein Perkg.
    /*!
     \return a double value.
    */
    double dailyMilkProteinPerkg(double dailyProteinAvailableForProduction)//g per kg
    {
        double milkProteinPerkg = 0;
        double milkProteinContentPerkg = 0;
        milkProteinContentPerkg = 1000.0 * milkNconc * 6.38; 
        switch (GlobalVars.Instance.getcurrentEnergySystem())
        {
            case 1:
            case 2: double dailyME = energyIntake / GlobalVars.avgNumberOfDays;
                    double Nmet = dailyProteinAvailableForProduction / 6.25;
                    
                    milkProteinPerkg = milkProteinContentPerkg / efficiencyProteinMilk;
                break;
            default:
                string messageString = ("Protein system for livestock not found");
                GlobalVars.Instance.Error(messageString);
                break;
        }

        return milkProteinPerkg;
    }
    //! a normal member, calculate daily Grouth Protein.
    /*!
     \return a double value.
    */
    double dailyGrowthProteinPerkg() //g per kg
    {
        double growthProteinPerkg = 0;
        switch (GlobalVars.Instance.getcurrentEnergySystem())
        {
            case 1:
            case 2:
                if (speciesGroup == 1)
                {
                    double efficiencyGrowth = 0.7;
                    growthProteinPerkg = 1000.0 * growthNconc * 6.25 / efficiencyGrowth;
                }
                if (speciesGroup == 2) //need to make efficiency dependent on amino acids in diet
                {
                    double efficiencyGrowth = 0.7;
                    growthProteinPerkg = 1000.0 * growthNconc * 6.25 / efficiencyGrowth;
                }
                break;

            default:
                string messageString = ("Protein system for livestock not found");
                GlobalVars.Instance.Error(messageString);
                break;

        }
        return growthProteinPerkg;
    }
    //! a normal member, calculate daily Protein Remobilisation.
    /*!
     \return a double value.
    */
    double dailyProteinRemobilisation(double weightLoss)//MJ ME/day
    {
        double proteinRemobilisation = weightLoss * growthNconc * 6.25;
        return proteinRemobilisation;
    }
    //! a normal member, Get maintenance Energy.
    /*!
     \return a double value.
    */
    public double GetmaintenanceEnergy()//MJ ME per year
    {
        double maintenanceEnergy = dailymaintenanceEnergy() * GlobalVars.avgNumberOfDays;
        return maintenanceEnergy;
    }
    //! a normal member, Get Grouwth Energy.
    /*!
     \return a double value.
    */
    public double GetGrowthEnergy()//MJ ME per year
    {
        double growthEnergy = avgProductionMeat * dailyGrowthEnergyPerkg() * GlobalVars.avgNumberOfDays;
        return growthEnergy;
    }
    //! a normal member, Get Milk Energy.
    /*!
     \return a double value.
    */
    public double GetMilkEnergy()//MJ ME per year
    {
        double milkEnergy = 0;
        if (isDairy)
            milkEnergy= avgProductionMilk * dailyMilkEnergyPerkg() * GlobalVars.avgNumberOfDays;
        return milkEnergy;
    }
    //! a normal member, Get Maintenance Protein.
    /*!
     \return a double value.
    */
    public double GetmaintenanceProtein()//kg protein per year
    {
        double maintenanceProtein = dailymaintenanceProtein() * GlobalVars.avgNumberOfDays/1000.0;
        return maintenanceProtein;
    }
    //! a normal member, Get Grouwth Protein.
    /*!
     \return a double value.
    */
    public double GetGrowthProtein()//kg protein per year
    {
        double growthProtein = avgProductionMeat * dailyGrowthProteinPerkg() * GlobalVars.avgNumberOfDays / 1000.0;
        return growthProtein;
    }
    //! a normal member, Calculate Energy Demand.
    /*!
     more details.
    */
    public void CalcEnergyDemand()//MJ per year
    {
        calcEnergyLevel();
        energyDemand = GetmaintenanceEnergy() + GetGrowthEnergy() + GetMilkEnergy();
        energyIntake *= Getmu(energyLevel);
    }
    //! a normal member, Calculate Maximum Production.
    /*!
     \return a double value.
    */
    public bool CalcMaximumProduction()///calculate daily production permitted by energy available
    {
        bool retVal = true;
        double testDMI = 0;
        energyUseForMaintenance=0;
        energyUseForGrowth=0;
        energyUseForMilk=0;
        energyFromRemobilisation=0;
        maintenanceEnergyDeficit = 0;
       
        growthEnergyDeficit = 0;
        double proteinFromRemobilisation = 0;
        double initialEnergy = liveweight * growthEnergyDemandCoeff;
        calcEnergyLevel();
//        double energyAvail = energyIntake * Getmu(energyLevel);//energyIntake is in MJ per animal per year
        double MEAvail = 0.81* energyIntake;///energyIntake is in MJ per animal per year
        double proteinSupply = Nintake * 6.25;
        double dailyMEintake= MEAvail/ 365;
        double protEnergyRatio =(proteinSupply*1000/365)/dailyMEintake;
        double faecalProtein = dailyFaecalProtein() * GlobalVars.avgNumberOfDays/1000.0; //kg per year
        faecalN = faecalProtein / 6.25;
        energyUseForMaintenance = dailymaintenanceEnergy() * GlobalVars.avgNumberOfDays;
        MEAvail -= energyUseForMaintenance;
        energyFromRemobilisation = 0;
        if (avgProductionMeat < 0.0)  ///if parameter file says that weight loss is expected (e.g. in lactating dairy cows in early lactation)
        {
            energyFromRemobilisation = dailyEnergyRemobilisation(avgProductionMeat) * GlobalVars.avgNumberOfDays;
            proteinFromRemobilisation = dailyProteinRemobilisation(avgProductionMeat) * GlobalVars.avgNumberOfDays;
            liveweight-=avgProductionMeat;
        }
        MEAvail += energyFromRemobilisation;
        proteinSupply += proteinFromRemobilisation;
        double proteinAvailableForProduction = proteinSupply - faecalProtein;
        if ((MEAvail < 0)||(proteinAvailableForProduction<0))//feeding below maintenance for either energy or protein
        {
            double weightlLoss=0;
            if (avgProductionMeat>0.0)//wanted growth but not enough energy or protein
                avgProductionMeat=0;
            if (MEAvail < 0)//remoblise energy
            {
                double remobilisationForMaintenance = 0;
                remobilisationForMaintenance=Math.Abs(MEAvail);
                energyFromRemobilisation+=remobilisationForMaintenance;
                weightlLoss=dailyWeightLoss(remobilisationForMaintenance /GlobalVars.avgNumberOfDays);
                double associatedProteinRemob=dailyProteinRemobilisation(weightlLoss) * GlobalVars.avgNumberOfDays;
                proteinAvailableForProduction+=associatedProteinRemob;
                proteinFromRemobilisation += associatedProteinRemob;
                avgProductionMeat = -weightlLoss;
                liveweight -= weightlLoss;
                MEAvail = 0.0;
                if (liveweight < 0)
                {
                    if (GlobalVars.Instance.getRunFullModel())
                    {
                        string messageString = name + " - liveweight has fallen below zero!";
                        GlobalVars.Instance.Error(messageString);
                        retVal = false;
                    }
                    retVal = false;
                }
            }
            if (proteinAvailableForProduction<0.0) //need to remobilise protein
            {
                weightlLoss=Math.Abs(proteinAvailableForProduction)/(dailyGrowthProteinPerkg() * GlobalVars.avgNumberOfDays);
                avgProductionMeat = -weightlLoss;
                liveweight += avgProductionMeat;

                proteinLimited = true;
                if (liveweight < 0)
                {
                    if (GlobalVars.Instance.getRunFullModel())
                    {
                        string messageString = name + " - liveweight has fallen below zero!";
                        GlobalVars.Instance.Error(messageString);
                        retVal = false;
                    }
                    retVal = false;
                }
                else
                    proteinAvailableForProduction=0;
            }
        }
        if (isDairy)
        {
            energyUseForGrowth = 0;
            if (avgProductionMeat > 0)//these animals are growing
            {
                energyUseForGrowth = avgProductionMeat * dailyGrowthEnergyPerkg() * GlobalVars.avgNumberOfDays;
                double proteinRequiredForGrowth=GetGrowthProtein();
                if ((MEAvail < energyUseForGrowth)||(proteinAvailableForProduction < proteinRequiredForGrowth))  //need to reduce growth
                {
                    if (MEAvail < energyUseForGrowth)//reduce growth to match energy available
                    {
                        growthEnergyDeficit = -1 * (energyUseForGrowth - MEAvail);
                        avgProductionMeat = MEAvail / (dailyGrowthEnergyPerkg() * GlobalVars.avgNumberOfDays);
                        energyUseForMilk = 0;
                        avgProductionMilk = 0;
                        MEAvail = 0;
                        proteinRequiredForGrowth=avgProductionMeat * dailyGrowthProteinPerkg() * GlobalVars.avgNumberOfDays;
                    }
                    if (proteinAvailableForProduction < proteinRequiredForGrowth)//reduce growth to match protein available
                    {
                        avgProductionMeat=proteinAvailableForProduction/(dailyGrowthProteinPerkg() * GlobalVars.avgNumberOfDays);//reduce growth to match available protein
                        proteinAvailableForProduction=0;
                        avgProductionMilk = 0;
                        proteinLimited = true;
                    }                    
                }
                else //enough energy and protein for milk production
                {
                    energyUseForMilk = MEAvail - energyUseForGrowth;
                    proteinAvailableForProduction -= proteinRequiredForGrowth; //enough protein to satisfy growth demand of dairy animals
                }
            }
            else //growth is zero or less
            {
                energyUseForMilk = MEAvail;
            }
            double thedailyMilkEnergyPerkg = dailyMilkEnergyPerkg();
            double energyLimitedMilk= energyUseForMilk / (thedailyMilkEnergyPerkg * GlobalVars.avgNumberOfDays);
            double dailyproteinAvailableForProduction = 1000 * proteinAvailableForProduction / GlobalVars.avgNumberOfDays;
            double proteinLimitedMilk = dailyproteinAvailableForProduction / dailyMilkProteinPerkg(dailyproteinAvailableForProduction) ;
            if (energyLimitedMilk < proteinLimitedMilk)
            {
                avgProductionMilk = energyLimitedMilk;
            }
            else
            {
                avgProductionMilk = proteinLimitedMilk;
                proteinLimited = true;
            }
            double milkDMcontent = 0.14;//approx value from feed tables
//            milk.Setenergy_conc(thedailyMilkEnergyPerkg / milkDMcontent);
  //          milk.Setamount(avgProductionMilk * milkDMcontent * avgNumberOfAnimal * GlobalVars.avgNumberOfDays);
    //        GlobalVars.Instance.AddProductProduced(milk);
            if (avgProductionMilk > 0.0)
            {
                double percentMilkProtein = (avgProductionMilk * milkNconc * 6.23 * 100.0) / avgProductionMilk;
                avgProductionECM = GlobalVars.Instance.GetECM(avgProductionMilk, (milkFat / 10.0), percentMilkProtein);
            }
            else
                avgProductionECM = 0;
            retVal = true;
        }
        else //these are meat animals
        {
            energyUseForMilk = 0;
         
            energyUseForGrowth = MEAvail - maintenanceEnergyDeficit;
            double energyLimitedGrowth= MEAvail / (dailyGrowthEnergyPerkg() * GlobalVars.avgNumberOfDays);
            double proteinLimitedGrowth=1000 * proteinAvailableForProduction / (dailyGrowthProteinPerkg() * GlobalVars.avgNumberOfDays);
            if (avgProductionMeat >= 0)
            {
                if (energyLimitedGrowth < proteinLimitedGrowth)
                {
                    avgProductionMeat = energyLimitedGrowth;
                }
                else
                {
                    avgProductionMeat = proteinLimitedGrowth;
                    proteinLimited = true;
                }
            }
            retVal = true;
        }
        return retVal;
    }
    //! a normal member, Get mu. Taking one argument and returning a double value.
    /*!
     \param energyLevel, a double argument
     \return a double value.
    */
    double Getmu(double energyLevel)
    {
        double mu = 1;
        if (energyLevel > mu_base)
            mu = 1 - mu_b * (energyLevel - mu_base);
        return mu;
    }
    //! a normal member, intake.
    /*!
     more details.
    */
    public void intake()
    {
        concentrateEnergy = 0;
        for (int k = 0; k < feedRation.Count; k++)
        {
            feedItem anItem = feedRation[k];
            double amount = anItem.Getamount();
            DMintake += GlobalVars.avgNumberOfDays * amount;
            energyIntake += GlobalVars.avgNumberOfDays * amount * anItem.Getenergy_conc();
            diet_ash += GlobalVars.avgNumberOfDays * amount * anItem.Getash_conc();
            Nintake += GlobalVars.avgNumberOfDays * amount * anItem.GetN_conc();
            Cintake += GlobalVars.avgNumberOfDays * amount * anItem.GetC_conc();
            diet_fat += GlobalVars.avgNumberOfDays * amount * anItem.Getfat_conc();
            diet_fibre += GlobalVars.avgNumberOfDays * amount * anItem.Getfibre_conc();
            diet_NDF += GlobalVars.avgNumberOfDays * amount * anItem.GetNDF_conc();
            diet_nitrate += GlobalVars.avgNumberOfDays * amount * anItem.GetNitrate_conc();
            digestibilityDiet += amount * anItem.GetDMdigestibility();
        }
        if (feedRation.Count==0)
        {
            string message1 = "Error; no feed provided for " + name;
            GlobalVars.Instance.Error(message1);
        }
        digestibilityDiet /= (DMintake/ GlobalVars.avgNumberOfDays);
        for (int j = 0; j < feedRation.Count; j++)
        {
            if ((feedRation[j].GetisGrazed())||(feedRation[j].GetfedAtPasture()))
                DMgrazed += feedRation[j].Getamount() * GlobalVars.avgNumberOfDays;
        }
        propDMgrazed = DMgrazed / GetDMintake();
        //if the proportion of excreta deposited to the fields has not been set using the nightTimeProp variable of housing, assume it is the same as the DM intake
        if (propExcretaField < 0.0)
            propExcretaField = propDMgrazed;
        concentrateDM = GetConcentrateDM() * GlobalVars.avgNumberOfDays;
        concentrateEnergy = GetConcentrateEnergy() * GlobalVars.avgNumberOfDays;
        FE = (0.75 * 1000 * (energyIntake / DMintake) - 1883) / 7720;
    }
    //! a normal member, Get enteric Methane.
    /*!
     \return a double value.
    */
    public double entericMethane()
    {
        double numDays = GlobalVars.avgNumberOfDays;
        double methane = 0; //initially in grams
        switch (GlobalVars.Instance.getcurrentInventorySystem())
        {
            case 1:
                //double dailyFibre = diet_fibre / numDays;
                //double dailyCP = Nintake * GlobalVars.NtoCrudeProtein / numDays;
                //double dailyNDF = diet_NDF / numDays;
                //double dailyFat = diet_fat / numDays;
                //double tot = dailyCP + dailyFat + dailyFibre + dailyNDF;
                //double test = DMintake / numDays;

                //methane = 63 + 79 * dailyFibre + 10 * dailyNDF
                //      + 26 * dailyCP - 212 * dailyFat;
                //methane /= 1000.0;
                //methane *= numDays;
                //break;
            case 2:
                double grossEnergyIntake = 18.4 * DMintake;
                double diet_NDF_prop = diet_NDF / DMintake;
                if (isDairy)
                {
                    if ((digestibilityDiet >= 0.7) && (diet_NDF_prop <= 0.35))
                        entericTier2MCF = 0.057;
                    if ((digestibilityDiet >= 0.7) && (diet_NDF_prop > 0.35))
                        entericTier2MCF = 0.06;
                    if ((digestibilityDiet < 0.7) && (digestibilityDiet >= 0.63) && (diet_NDF_prop > 0.35))
                        entericTier2MCF = 0.063;
                    if ((digestibilityDiet < 0.63) && (diet_NDF_prop > 0.35))
                        entericTier2MCF = 0.065;
                }
                else
                {
                    if (digestibilityDiet <= 0.62)
                        entericTier2MCF = 0.07;
                    if ((digestibilityDiet < 0.71) && (digestibilityDiet >= 0.62))
                        entericTier2MCF = 0.063;
                    if (digestibilityDiet >= 0.72)
                        entericTier2MCF = 0.057;
                    if (digestibilityDiet >= 0.7)
                        entericTier2MCF = 0.04;
                    if (digestibilityDiet > 0.75)
                        entericTier2MCF = 0.03;
                }

                methane = grossEnergyIntake * entericTier2MCF / 55.65;//1.13
                break;
        }
        double methane_reduction = 0;
        if (diet_nitrate > 0)
        {
            double mol_nitrate = diet_nitrate / 62; //mols of nitrate
            double mol_methane = methane / 16; //mols of methane
            methane_reduction = nitrateEfficiency * mol_nitrate / mol_methane;
            if (methane_reduction > 1)
                methane_reduction = 1.0;
        }
        methane *= (1-methane_reduction);
        return methane;
    }
    //! a normal member, Do Carbon.
    /*!
     more details.
    */
    public void DoCarbon()
    {
        milkC = GlobalVars.avgNumberOfDays * avgProductionMilk * milkCconc;
        double totalGrowthC = GlobalVars.avgNumberOfDays * avgProductionMeat * growthCconc;
        mortalitiesC = mortalityCoefficient / 2 * totalGrowthC;
        growthC =  totalGrowthC - mortalitiesC;
        double ashConc =diet_ash/DMintake;
        if (isRuminant)
            faecalC = Cintake * (1 - Getmu(energyLevel) * digestibilityDiet)/(1-ashConc);
        else
            faecalC = Cintake * (1 - digestibilityDiet) / (1 - ashConc);
        urineC = urineProp * Cintake;
        CH4C = entericMethane() * 12 / 16;
        CO2C=Cintake - (milkC + growthC + mortalitiesC + faecalC + urineC + CH4C);

        CexcretionToPasture = propExcretaField * (faecalC + urineC);
        //methane emission during grazing
        CCH4GR = 0.0;
        if (CexcretionToPasture > 0)
        {
            double aveTemperature = GlobalVars.Instance.theZoneData.GetaverageAirTemperature();
            double grazingVS = CexcretionToPasture / GlobalVars.Instance.getalpha(); ;
            double MCF = 0;
            if (aveTemperature < 14.5)
                MCF = 0.01;
            if ((aveTemperature >= 14.5) && (aveTemperature < 25.5))
                MCF = 0.015;
            if (aveTemperature >= 25.5)
                MCF = 0.02;
            CCH4GR = MCF * grazingVS * Bo * 0.67 * 12 / 16;
        }
    }
    //! a normal member, Do Nitrogen.
    /*!
     more details.
    */
    public void DoNitrogen()
    {
        milkN = GlobalVars.avgNumberOfDays * avgProductionMilk * milkNconc;
        double totalGrowthN = 0;
        if (avgProductionMeat>=0)
            totalGrowthN= GlobalVars.avgNumberOfDays * avgProductionMeat * growthNconc;
        else
            totalGrowthN = avgProductionMeat * GlobalVars.avgNumberOfDays * dailyGrowthProteinPerkg()/6.25;
        mortalitiesN = mortalityCoefficient / 2 * totalGrowthN;
        growthN = totalGrowthN - mortalitiesN;
        double ashConc = diet_ash / DMintake;
        urineN = Nintake - (milkN + growthN + mortalitiesN + faecalN);
        if (urineN < -1E-10)
        {
            Write();
            string message1 = "Error; urine N for " + name + " (population "+ avgNumberOfAnimal + ") has gone negative";
            GlobalVars.Instance.Error(message1);
        }
        
        NexcretionToPasture = propExcretaField * (faecalN + urineN);
    }
    //! a normal member, Do Ruminant.
    /*!
     more details.
    */
    public void DoRuminant()
    {
        intake();
        CalcMaximumProduction();
        CalcEnergyDemand();
        DoCarbon();
        DoNitrogen();
        GetExcretaDeposition();
        CalculateVS();
    }
    //! a normal member, Get Excreta Deposition.
    /*!
     more details.
    */
    public void GetExcretaDeposition()
    {
        if ((propDMgrazed == 0.0)&&(propExcretaField!=0.0))
        {
            string messageString = name + " - attempt to enforce manure partitioning when there is no grazing";
            GlobalVars.Instance.Error(messageString);
        }
        double[] DM = new double[GlobalVars.Instance.getmaxNumberFeedItems()];
        for (int i = 0; i < GlobalVars.Instance.getmaxNumberFeedItems(); i++)
            DM[i] = 0;

        double sum = 0;
        for (int j = 0; j < feedRation.Count; j++)
        {
            if (feedRation[j].GetisGrazed())
            {
                int feedCode = feedRation[j].GetFeedCode();
                double temp = avgNumberOfAnimal * GlobalVars.Instance.GetavgNumberOfDays() * feedRation[j].Getamount();
                grazedN += feedRation[j].Getamount() * feedRation[j].GetN_conc() * GlobalVars.Instance.GetavgNumberOfDays();
                grazedC += feedRation[j].Getamount() * feedRation[j].GetC_conc() * GlobalVars.Instance.GetavgNumberOfDays();
                grazedDM += feedRation[j].Getamount() * GlobalVars.Instance.GetavgNumberOfDays();
                DM[feedCode] += temp;
                GlobalVars.Instance.grazedArray[feedCode].ruminantDMgrazed += temp;
                sum += temp;
            }
            if (feedRation[j].GetfedAtPasture())
            {
                int feedCode = feedRation[j].GetFeedCode();
                double temp = avgNumberOfAnimal * GlobalVars.Instance.GetavgNumberOfDays() * feedRation[j].Getamount();
                pastureFedN += feedRation[j].Getamount() * feedRation[j].GetN_conc() * GlobalVars.Instance.GetavgNumberOfDays();
                pastureFedC += feedRation[j].Getamount() * feedRation[j].GetC_conc() * GlobalVars.Instance.GetavgNumberOfDays();
            }
        }
        double excretaN = 0;
        for (int i = 0; i < GlobalVars.Instance.getmaxNumberFeedItems(); i++)
        {
            if (DM[i]>0)
            {
                double theUrineN = propExcretaField * avgNumberOfAnimal * urineN * DM[i] / sum;
                double theUrineC = propExcretaField * avgNumberOfAnimal * urineC * DM[i] / sum;
                double theFaecalN = propExcretaField * avgNumberOfAnimal * faecalN * DM[i] / sum;
                double theFaecalC = propExcretaField * avgNumberOfAnimal * faecalC * DM[i] / sum;
                double theCH4C = avgNumberOfAnimal * CCH4GR * DM[i] / sum;
                GlobalVars.Instance.grazedArray[i].urineC += theUrineC; 
                GlobalVars.Instance.grazedArray[i].urineN += theUrineN;
                GlobalVars.Instance.grazedArray[i].faecesC += theFaecalC;
                GlobalVars.Instance.grazedArray[i].faecesN += theFaecalN;
                GlobalVars.Instance.grazedArray[i].fieldCH4C += theCH4C;
                excretaN += theFaecalN + theUrineN;

            }
        }
        if ((excretaN==0) && (housingDetails.Count == 0))
        {
            string message1 = "Error; animals are fed at pasture only but no pasture is consumed. Livestock name = " + name;
            GlobalVars.Instance.Error(message1);
        }
    }
    //! a normal member, Get all FeedItem Used.
    /*!
     more details.
    */
    public void GetAllFeedItemsUsed()
    {
        for (int i = 0; i < GlobalVars.Instance.getmaxNumberFeedItems(); i++)
        {
            for (int j = 0; j < feedRation.Count; j++)
                 if (feedRation[j].GetFeedCode() == i) 
                 {
                    feedItem afeedItem = new feedItem(feedRation[j]);
                    afeedItem.setFeedCode(i);
                    afeedItem.AddFeedItem(feedRation[j], false);
                    afeedItem.Setamount(avgNumberOfAnimal * GlobalVars.Instance.GetavgNumberOfDays() * feedRation[j].Getamount());
/*                    if ((afeedItem.GetisGrazed())&&(GlobalVars.Instance.GetstrictGrazing()))
                   {
                        afeedItem.Setname(afeedItem.GetName() + ", grazed");
                        afeedItem.setFeedCode(afeedItem.GetFeedCode() + 1000);
                        GlobalVars.Instance.allFeedAndProductsUsed[i+1000].composition.AddFeedItem(afeedItem, false);
                    }
                    else*/
                        GlobalVars.Instance.allFeedAndProductsUsed[i].composition.AddFeedItem(afeedItem, false);
                    //break;
                }
        }
    }
    //! a normal member, Check Livestock C Balance. Returing a boolean value.
    /*!
     \return a boolean value.
    */
    public bool CheckLivestockCBalance()
    {
        bool retVal = false;
        double Cout = urineC + growthC + faecalC + milkC + mortalitiesC;
        double CLost = CH4C + CO2C;
        double Cbalance = Cintake - (Cout + CLost);
        double diff = Cbalance / Cintake;
        double tolerance = GlobalVars.Instance.getmaxToleratedErrorYield();
        if (Math.Abs(diff) > tolerance)
        {
           
                double errorPercent = 100 * diff;
               
                string messageString=("Error; Livestock C balance error is more than the permitted margin of "
                    + tolerance.ToString() +"\n");
                messageString+=("Percentage error = " + errorPercent.ToString("0.00") + "%");
                GlobalVars.Instance.Error(messageString);
        }
        return retVal;
    }
    //! a normal member, Check Livestock N Balance. Returing a boolean value.
    /*!
     \return a boolean value.
    */
    public bool CheckLivestockNBalances()
    {
        bool retVal = false;
        double Nout = urineN + growthN + faecalN + milkN + mortalitiesN;
        double Nbalance = Nintake - Nout;
        double diff = Nbalance / Nintake;
        double tolerance = GlobalVars.Instance.getmaxToleratedErrorYield();
        if (Math.Abs(diff) > tolerance)
        {
                double errorPercent = 100 * diff;
                string messageString = ("Error; Livestock N balance error is more than the permitted margin of "
                    + tolerance.ToString() + "\n");
                messageString += ("Percentage error = " + errorPercent.ToString("0.00") + "%");
                GlobalVars.Instance.Error(messageString);  
        }
        return retVal;
    }
    //! a normal member, Write.
    /*!
     more details.
    */
    public void Write()
    {
        double numofDaysInYear = GlobalVars.avgNumberOfDays;
        GlobalVars.Instance.writeStartTab("LiveStock");
        GlobalVars.Instance.writeInformationToFiles("nameLiveStock", "Name", "-", name, parens);
        GlobalVars.Instance.writeInformationToFiles("speciesGroup", "Species identifier", "-", speciesGroup, parens);
        GlobalVars.Instance.writeInformationToFiles("LivestockType", "Livestock type", "", LivestockType, parens);
        GlobalVars.Instance.writeInformationToFiles("liveweight", "Liveweight", "kg", liveweight, parens);
        GlobalVars.Instance.writeInformationToFiles("isRuminant", "Is a ruminant", "-", isRuminant, parens);
        GlobalVars.Instance.writeInformationToFiles("avgNumberOfAnimal", "Annual average number of animals", "-", avgNumberOfAnimal, parens);

        GlobalVars.Instance.writeInformationToFiles("DMintake", "Intake of DM", "kg/day", DMintake / numofDaysInYear, parens);
        GlobalVars.Instance.writeInformationToFiles("energyIntake", "Intake of energy", "MJ/day", energyIntake / numofDaysInYear, parens);
        GlobalVars.Instance.writeInformationToFiles("energyUseForGrowth", "Energy used for growth", "MJ/day", energyUseForGrowth / numofDaysInYear, parens);
        GlobalVars.Instance.writeInformationToFiles("energyUseForMilk", "Energy used for milk production", "MJ/day", energyUseForMilk / numofDaysInYear, parens);
        GlobalVars.Instance.writeInformationToFiles("energyFromRemobilisation", "Energy supplied by remobilisation", "MJ/day", energyFromRemobilisation / numofDaysInYear, parens);
        GlobalVars.Instance.writeInformationToFiles("energyUseForMaintenance", "Energy used for maintenance", "MJ/day", energyUseForMaintenance / numofDaysInYear, parens);
        GlobalVars.Instance.writeInformationToFiles("maintenanceEnergyDeficit", "Maintenance energy deficit", "MJ/day", maintenanceEnergyDeficit / numofDaysInYear, parens);
        GlobalVars.Instance.writeInformationToFiles("growthEnergyDeficit", "Growth energy deficit", "MJ/day", growthEnergyDeficit / numofDaysInYear, parens);
        //GlobalVars.Instance.writeInformationToFiles("milkEnergyDeficit", "Deficit in energy required for milk production", "MJ", milkEnergyDeficit);

        GlobalVars.Instance.writeInformationToFiles("diet_ash", "Ash in diet", "kg", diet_ash, parens);
        GlobalVars.Instance.writeInformationToFiles("diet_fibre", "Fibre in diet", "kg", diet_fibre, parens);
        GlobalVars.Instance.writeInformationToFiles("diet_fat", "Fat in diet", "kg", diet_fat, parens);
        GlobalVars.Instance.writeInformationToFiles("diet_NDF", "NDF  in diet", "kg", diet_NDF, parens);
        GlobalVars.Instance.writeInformationToFiles("digestibilityDiet", "Diet DM digestibility", "kg/kg", digestibilityDiet, parens);

        GlobalVars.Instance.writeInformationToFiles("Cintake", "Intake of C", "kg", Cintake, parens);
        GlobalVars.Instance.writeInformationToFiles("milkC", "C in milk", "kg", milkC, parens);
        GlobalVars.Instance.writeInformationToFiles("growthC", "C in growth", "kg", growthC, parens);
        GlobalVars.Instance.writeInformationToFiles("urineCLiveStock", "C in urine", "kg", urineC, parens);
        GlobalVars.Instance.writeInformationToFiles("faecalCLiveStock", "C in faeces", "kg", faecalC, parens);
        GlobalVars.Instance.writeInformationToFiles("CH4C", "CH4-C emitted", "kg", CH4C, parens);
        GlobalVars.Instance.writeInformationToFiles("CO2C", "CO2-C emitted", "kg", CO2C, parens);
        //GlobalVars.Instance.writeInformationToFiles("energyLevel", "??", "??", energyLevel);
        GlobalVars.Instance.writeInformationToFiles("Nintake", "Intake of N", "kg", Nintake, parens);
        GlobalVars.Instance.writeInformationToFiles("milkN", "N in milk", "kg", milkN, parens);
        GlobalVars.Instance.writeInformationToFiles("growthN", "N in growth", "kg", growthN, parens);
        GlobalVars.Instance.writeInformationToFiles("mortalitiesN", "N in mortalities", "kg", mortalitiesN, parens);
        GlobalVars.Instance.writeInformationToFiles("urineN", "N in urine", "kg", urineN, parens);
        GlobalVars.Instance.writeInformationToFiles("faecalN", "N in faeces", "kg", faecalN, parens);

        GlobalVars.Instance.writeInformationToFiles("avgDailyProductionMilk", "Average daily milk production", "kg/day", avgProductionMilk, parens);
        double temp = avgProductionMilk * GlobalVars.avgNumberOfDays;
        GlobalVars.Instance.writeInformationToFiles("avgProductionMilk", "Average yearly milk production", "kg", temp, parens);
        if (avgProductionMilk > 0.0)
        {
            double percentMilkProtein = (milkN * 6.23 * 100.0) / (avgProductionMilk * GlobalVars.avgNumberOfDays);
            avgProductionECM = GlobalVars.Instance.GetECM(avgProductionMilk, (milkFat / 10.0), percentMilkProtein);
        }
        else
            avgProductionECM = 0;
        GlobalVars.Instance.writeInformationToFiles("avgProductionECM", "Average energy-corrected milk production", "kg/day", avgProductionECM * 365.0, parens);
        GlobalVars.Instance.writeInformationToFiles("avgDailyProductionECM", "Average daily energy-corrected milk production", "kg/day", avgProductionECM, parens);
        GlobalVars.Instance.writeInformationToFiles("avgProductionMeat", "Average weight change", "g/day", avgProductionMeat * 1000.0, parens);
        //GlobalVars.Instance.writeInformationToFiles("housedDuringGrazing", "??", "??", housedDuringGrazing);
        for (int i = 0; i < housingDetails.Count; i++)
            housingDetails[i].WriteXML();
        if (!GlobalVars.Instance.getRunFullModel())
            GlobalVars.Instance.writeEndTab();
    }

    //! A normal member. Do Defined Production. 
    /*!
     Calculate N and C flows, using defined start and end weights, plus duration of production cycle. Suitable for all animal types.
    */
    public void DoDefinedProduction()
    {
        DoCarbon();
        faecalN = Nintake * (1 - digestibilityDiet);  //in kg/yr, assumes protein digestib = DM digestib
        DoNitrogen();
        double NUE = growthN / Nintake;
        if (NUE > maxNuseEfficiency)
        {
            string messageString = ("N use efficiency of " + name + " is " + NUE + " which is greater than the maximum permitted");
            GlobalVars.Instance.Error(messageString);
        }
    }

     //! a normal member, Do Pig Energy.
    /*!
     the following functions are from the US pig model.
    */
    public void DoPigEnergy()
    {
        double dailyenergyIntake = energyIntake/GlobalVars.avgNumberOfDays;
        double dailydiet_ash = diet_ash/GlobalVars.avgNumberOfDays;
        double dailydiet_fat = diet_fat/GlobalVars.avgNumberOfDays;
    }
    //! a normal member, Do Pig Energy. Return pig maintenance energy requirement (MJ/day)
    /*!
     \return a double value.
    */
    public double pigMaintenanceEnergy()
    {
        double retVal = 0.44 * Math.Pow(liveweight, 0.75);
        return retVal;
    }

    //! a normal member. return change in weight in kg. energyAvailable is in MJ 
    /*!
     \return a double value.
    */
    public double GetPigGrowth(double energyAvailable)
    {
        double retVal = energyAvailable / 44.35;
        return retVal;
    }
    //! a normal member. Do pig Lactation Energy. Taking four arguments and returning a double value.
    /*!
     \param numPiglets, a double argument.
     \param birthWt, a double argument.
     \param weanedWt, a double argument.
     \param duration, a double argument.
     \return a double value.
    */
    public double pigLactationEnergy(double numPiglets, double birthWt, double weanedWt, double duration)
    {
        double retVal = 0;
        double growthRate = (weanedWt - birthWt) / duration;
        retVal = 4.184 * numPiglets * (6.83 * ((weanedWt - birthWt) / duration) - 125);
        return retVal;
    }
    //! a normal member. Do pig
    /*!
     end of US pig model.
    */
    public void DoPig()
    {
        intake();
        DoGrowingPigs();
        CalculateVS();
    }
    //! a normal member. Do Growing pigs
    /*!
     more details.
    */
    public void DoGrowingPigs()
    {
        inputProduction = true;
        double FE = (0.75 * 1000 * (energyIntake / DMintake) - 1883) / 7720;
        double FEIntake = DMintake * FE;
        if (inputProduction)
        {
            DoDefinedProduction();
        }
        else
        {
            avgProductionMeat = FEIntake / (2.82 * GlobalVars.avgNumberOfDays);
            CalcMaximumPigProduction();
            if ((startWeight > 0) && (duration > 0))
                endWeight = startWeight + duration * avgProductionMeat;
        }
        double FEperKgPigProduced = (duration * FEIntake / GlobalVars.avgNumberOfDays) / (endWeight - startWeight);
        double ProteinperFE = 6.25 * Nintake / FEIntake;
        double NperPigProduced = duration * (faecalN + urineN) / GlobalVars.avgNumberOfDays;
    }
    //! a normal member. Calculate Maximum Pig production. Returning a boolean value.
    /*!
     \return a boolean value.
    */

    public bool CalcMaximumPigProduction()///calculate daily production permitted by energy available
    {
        bool retVal = true;
        energyUseForMaintenance = 0;
        energyUseForGrowth = 0;
        energyUseForMilk = 0;
        energyFromRemobilisation = 0;
        maintenanceEnergyDeficit = 0;
        
        growthEnergyDeficit = 0;
        double proteinFromRemobilisation = 0;
        double energyAvail = energyIntake;//energyIntake is in MJ per animal per year
        double proteinSupply = Nintake * 6.25;
        faecalN =  Nintake * (1 - digestibilityDiet);  //in kg/yr, assumes protein digestib = DM digestib
        double faecalProtein = faecalN * 6.25;
        energyUseForMaintenance = dailymaintenanceEnergy() * GlobalVars.avgNumberOfDays;
        energyAvail -= energyUseForMaintenance;
        energyFromRemobilisation = 0;
        if (avgProductionMeat < 0.0)
        {
            energyFromRemobilisation = dailyEnergyRemobilisation(avgProductionMeat) * GlobalVars.avgNumberOfDays;
            proteinFromRemobilisation = dailyProteinRemobilisation(avgProductionMeat) * GlobalVars.avgNumberOfDays;
            liveweight -= avgProductionMeat;
        }
        energyAvail += energyFromRemobilisation;
        proteinSupply += proteinFromRemobilisation;
        double proteinAvailableForProduction = proteinSupply - faecalProtein;
        if ((energyAvail < 0) || (proteinAvailableForProduction < 0))//feeding below maintenance for either energy or protein
        {
            double weightlLoss = 0;
            if (avgProductionMeat > 0.0)//wanted growth but not enough energy or protein
                avgProductionMeat = 0;
            if (energyAvail < 0)//remoblise energy
            {
                double remobilisationForMaintenance = 0;
                remobilisationForMaintenance = Math.Abs(energyAvail);
                energyFromRemobilisation += remobilisationForMaintenance;
                weightlLoss = dailyWeightLoss(remobilisationForMaintenance / GlobalVars.avgNumberOfDays);
                double associatedProteinRemob = dailyProteinRemobilisation(weightlLoss) * GlobalVars.avgNumberOfDays;
                proteinAvailableForProduction += associatedProteinRemob;
                proteinFromRemobilisation += associatedProteinRemob;
                avgProductionMeat = -weightlLoss;
                liveweight -= weightlLoss;
                energyAvail = 0.0;
                if (liveweight < 0)
                {
                    if (GlobalVars.Instance.getRunFullModel())
                    {
                        string messageString = name + " - liveweight has fallen below zero!";
                        GlobalVars.Instance.Error(messageString);
                        retVal = false;
                    }
                    retVal = false;
                }
            }
            if (proteinAvailableForProduction < 0.0) //need to remobilise protein
            {
                weightlLoss = Math.Abs(proteinAvailableForProduction) / (dailyGrowthProteinPerkg() * GlobalVars.avgNumberOfDays);
                liveweight -= avgProductionMeat;
                if (liveweight < 0)
                {
                    if (GlobalVars.Instance.getRunFullModel())
                    {
                        string messageString = name + " - liveweight has fallen below zero!";
                        GlobalVars.Instance.Error(messageString);
                        retVal = false;
                    }
                    retVal = false;
                }
                else
                    proteinAvailableForProduction = 0;
            }
        }
        if (isDairy)
        {
            energyUseForGrowth = 0;
            if (avgProductionMeat > 0)//these animals are growing
            {
                energyUseForGrowth = avgProductionMeat * dailyGrowthEnergyPerkg() * GlobalVars.avgNumberOfDays;
                double proteinRequiredForGrowth = GetGrowthProtein();
                if ((energyAvail < energyUseForGrowth) || (proteinAvailableForProduction < proteinRequiredForGrowth))  //need to reduce growth
                {
                    if (energyAvail < energyUseForGrowth)//reduce growth to match energy available
                    {
                        growthEnergyDeficit = -1 * (energyUseForGrowth - energyAvail);
                        avgProductionMeat = energyAvail / (dailyGrowthEnergyPerkg() * GlobalVars.avgNumberOfDays);
                        energyUseForMilk = 0;
                        avgProductionMilk = 0;
                        energyAvail = 0;
                        proteinRequiredForGrowth = avgProductionMeat * dailyGrowthProteinPerkg() * GlobalVars.avgNumberOfDays;
                    }
                    if (proteinAvailableForProduction < proteinRequiredForGrowth)//reduce growth to match protein available
                    {
                        avgProductionMeat = proteinAvailableForProduction / (dailyGrowthProteinPerkg() * GlobalVars.avgNumberOfDays);//reduce growth to match available protein
                        proteinAvailableForProduction = 0;
                        avgProductionMilk = 0;
                    }
                }
                else //enough energy and protein for milk production
                {
                    energyUseForMilk = energyAvail - energyUseForGrowth;
                    proteinAvailableForProduction -= proteinRequiredForGrowth; //enough protein to satisfy growth demand of dairy animals
                }
            }
            else //growth is zero or less
                energyUseForMilk = energyAvail;
            double energyLimitedMilk = energyUseForMilk / (dailyMilkEnergyPerkg() * GlobalVars.avgNumberOfDays);
            double dailyproteinAvailableForProduction = 1000 * proteinAvailableForProduction / GlobalVars.avgNumberOfDays;
            double proteinLimitedMilk = dailyproteinAvailableForProduction / dailyMilkProteinPerkg(dailyproteinAvailableForProduction);
            if (energyLimitedMilk < proteinLimitedMilk)
                avgProductionMilk = energyLimitedMilk;
            else
                avgProductionMilk = proteinLimitedMilk;
            retVal = true;
        }
        else //these are meat animals
        {
            energyUseForMilk = 0;
            energyUseForGrowth = energyAvail - maintenanceEnergyDeficit;
            double energyLimitedGrowth = energyAvail / (dailyGrowthEnergyPerkg() * GlobalVars.avgNumberOfDays);
            double proteinLimitedGrowth = 1000 * proteinAvailableForProduction / (dailyGrowthProteinPerkg() * GlobalVars.avgNumberOfDays);
            if (avgProductionMeat >= 0)
            {
                if (energyLimitedGrowth < proteinLimitedGrowth)
                    avgProductionMeat = energyLimitedGrowth;
                else
                    avgProductionMeat = proteinLimitedGrowth;
            }
            retVal = true;
        }
        return retVal;
    }
    //! a normal member. Get Concentrate DM. Returning a double value.
    /*!
     \return a double value.
    */
    public double GetConcentrateDM()
    {
        double retVal = 0;
        for (int i = 0; i < feedRation.Count; i++)
        {
            feedItem afeedItem = feedRation[i];
            if (afeedItem.isConcentrate())
                retVal += afeedItem.Getamount();
        }
        return retVal;
    }
    //! a normal member. Get Concentrate Energy. Returning a double value.
    /*!
     \return a double value.
    */
    public double GetConcentrateEnergy()
    {
        double retVal = 0;
        for (int i = 0; i < feedRation.Count; i++)
        {
            feedItem afeedItem = feedRation[i];
            if (afeedItem.isConcentrate())                
                retVal += afeedItem.Getamount() * afeedItem.Getenergy_conc();
        }
        return retVal;
    }
    //! a normal member. Set Excretal Distribution Housing. Taking one double argument.
    /*!
     \param PropExcretalDepositionHousing, a double argument.
    */
    public void  SetExcretalDistributionHousing(double PropExcretalDepositionHousing)
    {
        if (housingDetails.Count != 1)
        {
            string messageString = name + " - attempt to enforce manure partitioning to more than one animal house";
            GlobalVars.Instance.Error(messageString);
        }
        propExcretaField = (1 - PropExcretalDepositionHousing);
    }

        //! A normal member. Check Daily Feed Stuff. Returning one value.
    /*!
        \return a boolean value.
    */
    //MELS-2023
    public void CalculateVS()
    {
        double numDays = GlobalVars.avgNumberOfDays;

        double grossEnergyIntake = 18*GetDMintake()/numDays;
        double dietDigestibility = GetDigestibility()/numDays;
        double ash_conc = GetDietAsh()/DMintake;

        //MELS-2023      
        VS = grossEnergyIntake*(1-dietDigestibility) + 0.4*(grossEnergyIntake*((1-ash_conc)/18.45)); 
    }
    //! a normal member. Write Livestock File.
    /*!
     more details.
    */
    public void WriteLivestockFile()
    {
        int times = 1;
        if (GlobalVars.Instance.headerLivestock == false)
            times = 2;
        for (int j = 0; j < times; j++)
        {
            GlobalVars.Instance.writeLivestockFile("name", "name", "-", name, "livestock", 0);
            GlobalVars.Instance.writeLivestockFile("avgNumberOfAnimal", "avgNumberOfAnimal", "-", avgNumberOfAnimal, "livestock", 0);

            GlobalVars.Instance.writeLivestockFile("diet_ash", "diet_ash", "kg/day", diet_ash / GlobalVars.avgNumberOfDays, "livestock", 0);
            GlobalVars.Instance.writeLivestockFile("diet_fibre", "diet_fibre", "kg/day", diet_fibre / GlobalVars.avgNumberOfDays, "livestock", 0);
            GlobalVars.Instance.writeLivestockFile("diet_fat", "diet_fat", "kg/day", diet_fat / GlobalVars.avgNumberOfDays, "livestock", 0);
            GlobalVars.Instance.writeLivestockFile("diet_NDF", "diet_NDF", "kg/day", diet_NDF / GlobalVars.avgNumberOfDays, "livestock", 0);
            GlobalVars.Instance.writeLivestockFile("diet_nitrate", "diet_nitrate", "kg/day", diet_nitrate / GlobalVars.avgNumberOfDays, "livestock", 0);
            GlobalVars.Instance.writeLivestockFile("digestibilityDiet", "digestibilityDiet", "-", digestibilityDiet, "livestock", 0);
            GlobalVars.Instance.writeLivestockFile("energyIntake", "energyIntake", "MJ ME/day", energyIntake / GlobalVars.avgNumberOfDays, "livestock", 0);
            GlobalVars.Instance.writeLivestockFile("energyUseForMaintenance", "energyUseForMaintenance", "MJ ME/day", energyUseForMaintenance / GlobalVars.avgNumberOfDays, "livestock", 0);
            GlobalVars.Instance.writeLivestockFile("energyUseForGrowth", "energyUseForGrowth", "MJ ME/day", energyUseForGrowth / GlobalVars.avgNumberOfDays, "livestock", 0);
            GlobalVars.Instance.writeLivestockFile("energyUseForMilk", "energyUseForMilk", "MJ ME/day", energyUseForMilk / GlobalVars.avgNumberOfDays, "livestock", 0);
            GlobalVars.Instance.writeLivestockFile("energyUseForGrazing", "energyUseForGrazing", "MJ ME/day", energyUseForGrazing / GlobalVars.avgNumberOfDays, "livestock", 0);
            GlobalVars.Instance.writeLivestockFile("energyFromRemobilisation", "energyFromRemobilisation", "MJ ME/day", energyFromRemobilisation / GlobalVars.avgNumberOfDays, "livestock", 0);
            GlobalVars.Instance.writeLivestockFile("maintenanceEnergyDeficit", "maintenanceEnergyDeficit", "MJ ME/day", maintenanceEnergyDeficit / GlobalVars.avgNumberOfDays, "livestock", 0);
            GlobalVars.Instance.writeLivestockFile("growthEnergyDeficit", "growthEnergyDeficit", "MJ ME/day", growthEnergyDeficit / GlobalVars.avgNumberOfDays, "livestock", 0);
            GlobalVars.Instance.writeLivestockFile("energyLevel", "energyLevel", "-", energyLevel, "livestock", 0);
            GlobalVars.Instance.writeLivestockFile("DMintake", "DMintake", "kg/day", DMintake / GlobalVars.avgNumberOfDays, "livestock", 0);
            GlobalVars.Instance.writeLivestockFile("concentrateDM", "concentrateDM", "kg/yr", concentrateDM, "livestock", 0);
            GlobalVars.Instance.writeLivestockFile("grazedDM", "grazedDM", "kg/day", grazedDM / GlobalVars.avgNumberOfDays, "livestock", 0);
            if (proteinLimited)
                GlobalVars.Instance.writeLivestockFile("proteinLimited", "proteinLimited", "-", 1, "livestock", 0);
            else
                GlobalVars.Instance.writeLivestockFile("proteinLimited", "proteinLimited", "-", 0, "livestock", 0);

            GlobalVars.Instance.writeLivestockFile("Cintake", "Cintake", "kg/yr", Cintake, "livestock", 0);
            GlobalVars.Instance.writeLivestockFile("milkC", "milkC", "kg/yr", milkC, "livestock", 0);
            GlobalVars.Instance.writeLivestockFile("growthC", "growthC", "kg/yr", growthC, "livestock", 0);
            GlobalVars.Instance.writeLivestockFile("mortalitiesC", "mortalitiesC", "kg/yr", mortalitiesC, "livestock", 0);
            GlobalVars.Instance.writeLivestockFile("urineC", "urineC", "kg/yr", urineC, "livestock", 0);
            GlobalVars.Instance.writeLivestockFile("faecalC", "faecalC", "kg/yr", faecalC, "livestock", 0);
            GlobalVars.Instance.writeLivestockFile("CexcretionToPasture", "CexcretionToPasture", "kg/yr", CexcretionToPasture, "livestock", 0);
            GlobalVars.Instance.writeLivestockFile("LiveCH4C", "LiveCH4C", "kg/yr", CH4C, "livestock", 0);
            GlobalVars.Instance.writeLivestockFile("LiveCO2C", "LiveCO2C", "kg/yr", CO2C, "livestock", 0);

            GlobalVars.Instance.writeLivestockFile("Nintake", "Nintake", "kg/yr", Nintake, "livestock", 0);
            GlobalVars.Instance.writeLivestockFile("grazedN", "grazedN", "kg/yr", grazedN, "livestock", 0);
            GlobalVars.Instance.writeLivestockFile("milkN", "milkN", "kg/yr", milkN, "livestock", 0);
            GlobalVars.Instance.writeLivestockFile("growthN", "growthN", "kg/yr", growthN, "livestock", 0);
            GlobalVars.Instance.writeLivestockFile("mortalitiesN", "mortalitiesN", "kg/yr", mortalitiesN, "livestock", 0);
            GlobalVars.Instance.writeLivestockFile("urineN", "urineN", "kg/yr", urineN, "livestock", 0);
            GlobalVars.Instance.writeLivestockFile("faecalN", "faecalN", "kg/yr", faecalN, "livestock", 0);
            GlobalVars.Instance.writeLivestockFile("NexcretionToPasture", "NexcretionToPasture", "kg/yr", NexcretionToPasture, "livestock", 0);

            GlobalVars.Instance.writeLivestockFile("avgProductionMeat", "avgProductionMeat", "g/day", avgProductionMeat*1000, "livestock", 0);
            GlobalVars.Instance.writeLivestockFile("avgProductionMilk", "avgProductionMilk", "kg/day", avgProductionMilk, "livestock", 0);
            GlobalVars.Instance.writeLivestockFile("avgProductionECM", "avgProductionECM", "kg/day", avgProductionECM, "livestock", 0);
            for (int i = 0; i < housingDetails.Count; i++)
                housingDetails[i].WriteXls();
            GlobalVars.Instance.writeLivestockFile("NULL", "NULL", "-", "-", "livestock", 1);
            GlobalVars.Instance.headerLivestock = true;
        }
    }
}
