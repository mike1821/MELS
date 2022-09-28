using System;
using System.Collections.Generic;
using System.Xml;
/*! A class that named housing. */
public class housing
{
    ///inputs
    int HousingType;
    string Name;
    ///parameters

    double feedWasteFactor;
    double HousingRefTemp;
    double EFNH3ref;
    double EFNH3housingTier2;
    double beddingFactor;
    double nightTimeProp;

    ///other variables
    double beddingDM;
    double Cinput;
    double CO2C;
    double CtoStorage;
    double FibreCToStorage;
    double propTimeThisHouse;
    double meanTemp;
    double beddingC;
    string parens;
    livestock theLivestock;
    double NTanInstore;
    double proportionDegradable; 
    double proportionNondegradable;
    double proportionTAN;
    double feedWasteC=0;
    double NWasteFeed = 0;
    double Nbedding = 0;
    double NtanInhouse = 0;
    double NorgInHouse = 0;
    double faecalN = 0;
    double NNH3housing = 0;
    double NfedInHousing = 0;
    double Ninput = 0;
    double Nout = 0;
    double NLost = 0;
    double Nbalance = 0;
    feedItem wasteFeed = null;

    double[] TANtoThisStore = new double[2];
    double[] labileOrganicNtoThisStore = new double[2];
    //! A normal member, Set Proportion Degradable. Taking one argument.
    /*!
     \param aValue, a double argument.
    */
    public void SetproportionDegradable(double aValue) { proportionDegradable = aValue; }
    //! A normal member, Set Proportion Nondegradable. Taking one argument.
    /*!
     \param aValue, a double argument.
    */
    public void SetproportionNondegradable(double aValue) { proportionNondegradable = aValue; }
    //! A normal member, Set Proportion TAN. Taking one argument.
    /*!
     \param aValue, a double argument.
    */
    public void SetproportionTAN(double aValue) { proportionTAN = aValue; }
    //! A normal member, Get Proportion Degradable. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetproportionDegradable() { return proportionDegradable; }
    //! A normal member, Get Proportion Nondegradable. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetproportionNondegradable() { return proportionNondegradable; }
    //! A normal member, Get Proportion TAN. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetproportionTAN() { return proportionTAN; }
    //! A normal member, Get NightTime Proportion. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetnightTimeProp() { return nightTimeProp; }
    //! A normal member, Set Name. Taking one argument.
    /*!
     \param aName, a string argument.
    */
    public void SetName(string aName) { Name = aName; }
    //! A normal member, Get PropTime ThisHouse. Returning one double value.
    /*!
     \return a double value.
    */
    public double getPropTimeThisHouse() { return propTimeThisHouse; }
    //! A normal member, Get Name. Returning one string value.
    /*!
     \return a string value.
    */
    public string GetName() { return Name; }
    //! A normal member, Get beddingDM. Returning one double value.
    /*!
     \return a double value.
    */
    public double getbeddingDM(){return beddingDM;}
    //! A normal member, Get UrineC. Returning one double value.
    /*!
     \return a double value.
    */
    public double getUrineC() { return theLivestock.GeturineC(); }
    //! A normal member, Get FacesC. Returning one double value.
    /*!
     \return a double value.
    */
    public double getFaecesC() { return theLivestock.GetfaecalC(); }
    //! A normal member, Get UrineN. Returning one double value.
    /*!
     \return a double value.
    */
    public double getUrineN() { return NtanInhouse; }
    //! A normal member, Get FacesN. Returning one double value.
    /*!
     \return a double value.
    */
    public double getFaecesN() { return faecalN; }
    //! A normal member, Get BeddingC. Returning one double value.
    /*!
     \return a double value.
    */
    public double getBeddingC() { return beddingC; }
    //! A normal member, Get BeddingN. Returning one double value.
    /*!
     \return a double value.
    */
    public double getBeddingN() { return Nbedding; }
    //! A normal member, Get FeedWasteC. Returning one double value.
    /*!
     \return a double value.
    */
    public double getFeedWasteC() { return feedWasteC; }
    //! A normal member, Get FeedWasteN. Returning one double value.
    /*!
     \return a double value.
    */
    public double getFeedWasteN() { return NWasteFeed; }
    //! A normal member, Get CO2C. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetCO2C() { return CO2C; }
    //! A normal member, Get NNH3housing. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetNNH3housing() { return NNH3housing; }
    //! A normal member, Get NfedInHousing. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetNfedInHousing() { return NfedInHousing; }
    //! A normal member, Get NinputInExcreta. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetNinputInExcreta() {return (NtanInhouse + faecalN);}
    //! A normal member, Get CinputInExcreta. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetCinputInExcreta() { return getUrineC() + getFaecesC(); }
    //! A normal member, Get Manure Carbon. Returning one double value.
    /*!
     \return a double value.
    */
    public double getManureCarbon()
    {
        double returnVal = 0;
        for (int i = 0; i < manurestoreDetails.Count; i++)
        {
            returnVal += manurestoreDetails[i].manureToStorage.GetTotalC();
        }
        return returnVal;
    }
    //! A normal member, Get Manure Nitrogen. Returning one double value.
    /*!
     \return a double value.
    */
    public double getManureNitrogen()
    {
        double returnVal = 0;
        for (int i = 0; i < manurestoreDetails.Count; i++)
        {
            returnVal += manurestoreDetails[i].manureToStorage.GetTotalN();
        }
        return returnVal;
    }
    List<feedItem> feedInHouse;
    //! A normal member, Get CinFeed FedInHouse. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetCinFeedFedInHouse()
    {
        double ret_val = 0;
        for (int i = 0; i < feedInHouse.Count; i++)
        {
            ret_val += feedInHouse[i].Getamount() * feedInHouse[i].GetC_conc();
        }
        return ret_val;
    }
    //! A normal member, Get NinFeed FedInHouse. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetNinFeedFedInHouse()
    {
        double ret_val = 0;
        for (int i = 0; i < feedInHouse.Count; i++)
        {
            ret_val += feedInHouse[i].Getamount() * feedInHouse[i].GetN_conc();
        }
        return ret_val;
    }
    List<GlobalVars.manurestoreRecord> manurestoreDetails = new List<GlobalVars.manurestoreRecord>();
    //! A normal member, Get Manurestore Details. Returning one list value.
    /*!
     \return a list value that points to GlobalVars.manurestoreRecord.
    */
    public List<GlobalVars.manurestoreRecord> GetmanurestoreDetails() { return manurestoreDetails; }
    //! A normal member, Get the livestock. Returning one livestock value.
    /*!
     \return a value that points to livestock.
    */
    public livestock gettheLivestock() { return theLivestock; }
    //    public double getTimeOnPasture(){return propDMgrazed;}
    //! A constructor
    /*!
     without argument.
    */
    private housing()
    {
    }
    //! A constructor with five arguments.
    /*!
     \param aHousingType, an integer argument.
     \param aLivestock, an class livestock instance.
     \param houseIndex, an integer argument.
     \param zoneNr, an integer argument.
     \param aparens, a string argument.
    */
    public housing(int aHousingType, livestock aLivestock, int houseIndex, int zoneNr, string aParens)
    {
        parens = aParens;
        theLivestock = aLivestock;
        //propDMgrazed = aLivestock.GetpropDMgrazed();
        HousingType = aHousingType;
        feedInHouse = new List<feedItem>();
        FileInformation paramFile = new FileInformation(GlobalVars.Instance.getParamFilePath());

        if (HousingType != 0)
        {
            string basePath = "AgroecologicalZone(" + zoneNr.ToString() + ").Housing";
            paramFile.setPath(basePath);
            int minHouse = 99, maxHouse = 0;
            paramFile.getSectionNumber(ref minHouse, ref maxHouse);
            int tmpHousingType = -1;

            bool found = false;
            int num = 0;
            for (int i = minHouse; i <= maxHouse; i++)
            {
                if (paramFile.doesIDExist(i))
                {
                    paramFile.Identity.Add(i);
                    tmpHousingType = paramFile.getItemInt("HousingType");
                    if (tmpHousingType == HousingType)
                    {
                        found = true;
                        num = i;
                        basePath += "(" + num.ToString() + ")";
                        break;
                    }
                    paramFile.Identity.RemoveAt(paramFile.Identity.Count - 1);
                }
            }
            if (found == false)
            {

                string messageString = aLivestock.Getname() + " could not link housing and manure storage";
                GlobalVars.Instance.Error(messageString);
            }
            Name = paramFile.getItemString("Name");
            paramFile.PathNames.Add("feedWasteFactor");
            paramFile.Identity.Add(-1);
            feedWasteFactor = paramFile.getItemDouble("Value");
            paramFile.PathNames[paramFile.PathNames.Count - 1] = "beddingFactor";
            beddingFactor = paramFile.getItemDouble("Value");
            paramFile.PathNames[paramFile.PathNames.Count - 1] = "HousingRefTemp";
            HousingRefTemp = paramFile.getItemDouble("Value");
            nightTimeProp = -1;
            paramFile.PathNames[paramFile.PathNames.Count - 1] = "nightTimeProp";
            nightTimeProp = paramFile.getItemDouble("Value",false);
            if (nightTimeProp > 0.0)
                theLivestock.SetExcretalDistributionHousing(nightTimeProp);
            switch (GlobalVars.Instance.getcurrentInventorySystem())
            {
                case 1:
                    EFNH3housingTier2 = paramFile.getItemDouble("Value", basePath + ".EFNH3housingTier2(-1)");
                    break;
                case 2:
                    break;
                case 3:
                    paramFile.PathNames[paramFile.PathNames.Count - 1] = "EFNH3housingRef";
                    EFNH3ref = paramFile.getItemDouble("Value");
                    paramFile.PathNames[paramFile.PathNames.Count - 1] = "meanTemp";
                    meanTemp = paramFile.getItemDouble("Value");
                    break;
            }
            paramFile.PathNames[paramFile.PathNames.Count - 1] = "ProportionDegradable";
            proportionDegradable = paramFile.getItemDouble("Value");
            paramFile.PathNames[paramFile.PathNames.Count - 1] = "ProportionNondegradable";
            proportionNondegradable = paramFile.getItemDouble("Value");
            paramFile.PathNames[paramFile.PathNames.Count - 1] = "ProportionTAN";
            proportionTAN = paramFile.getItemDouble("Value");
            GlobalVars.manurestoreRecord amanurestoreRecord = new GlobalVars.manurestoreRecord();
            int numManureStores = theLivestock.GethousingDetails()[houseIndex].GetManureRecipient().Count;
            for (int j = 0; j < numManureStores; j++)
            {

                manureStore aStore = new manureStore(theLivestock.GethousingDetails()[houseIndex].GetManureRecipient()[j].GetStorageType(),
                    theLivestock.GetspeciesGroup(), zoneNr, parens + "_manureStore" + (j + 1).ToString());
                amanurestoreRecord.SettheStore(aStore);
                manure amanureToStore = new manure();
                amanureToStore.Setname(aStore.Getname());
                amanurestoreRecord.SetmanureToStorage(amanureToStore);
                manurestoreDetails.Add(amanurestoreRecord);

            }
        }
        for (int i = 0; i < aLivestock.GetfeedRation().Count; i++)
        {
            feedItem aFeedItem = aLivestock.GetfeedRation()[i];
            if (!aFeedItem.GetisGrazed())
                feedInHouse.Add(aFeedItem);
            //else feedInHouse.Add(null);   // not sure why this was put here. It causes trouble later, so I have removed it.
        }
    }
    //! A normal member, Set the Livestock. Taking one argument.
    /*!
     \param someLivestock, a class livestock instance.
    */
    public void SettheLivestock(livestock someLivestock)
    {
        theLivestock=someLivestock;
    }
    //! A normal member, Get C to Storage. Returning one double value.
    /*!
     \return a double value.
    */
    public double GetCtoStorage() { return CtoStorage; }
    //! A normal member, Do housing.
    /*!
     \more details.
    */
    public void DoHousing()
    {
        propTimeThisHouse = 0;
        for (int i = 0; i < theLivestock.GethousingDetails().Count; i++)
        {
            if (theLivestock.GethousingDetails()[i].GetHousingType() == HousingType)
                propTimeThisHouse = theLivestock.GethousingDetails()[i].GetpropTime();
        }
        beddingDM = propTimeThisHouse * (1 - theLivestock.GetpropExcretaField()) * theLivestock.GetAvgNumberOfAnimal()
            * GlobalVars.avgNumberOfDays * beddingFactor;//1.28  
        //GlobalVars.Instance.GetthebeddingMaterial().Setamount(beddingDM);
        feedItem bedding = new feedItem(GlobalVars.Instance.GetthebeddingMaterial());
        bedding.Setamount(beddingDM);
        GlobalVars.Instance.allFeedAndProductsUsed[bedding.GetFeedCode()].composition.AddFeedItem(bedding, false, true);

        double amount = 0;
        for (int i = 0; i < theLivestock.GetfeedRation().Count; i++)
        {
            
            if (!theLivestock.GetfeedRation()[i].GetisGrazed())
            {
                feedInHouse[i].Setamount(propTimeThisHouse * theLivestock.GetfeedRation()[i].Getamount());
                feedInHouse[i].SetC_conc(theLivestock.GetfeedRation()[i].GetC_conc());
                feedInHouse[i].SetN_conc(theLivestock.GetfeedRation()[i].GetN_conc());
                amount += feedInHouse[i].Getamount();
            }
        }

        int daysOnPasture = (int) Math.Round(theLivestock.GetpropDMgrazed() * 365);
        if (GlobalVars.Instance.getcurrentInventorySystem() == 3)
        {
            if (daysOnPasture > 0)
//                if ((daysOnPasture > 0) && (theLivestock.GethousedDuringGrazing() == false))
                    meanTemp = GetMeanTemperature(daysOnPasture);
            else
                meanTemp = GetMeanTemperature(0);
        }
        //DWaste = propTimeThisHouse *  amount * theLivestock.GetAvgNumberOfAnimal() * GlobalVars.avgNumberOfDays;
        DoCarbon();
        DoNitrogen();
        for (int i = 0; i < manurestoreDetails.Count; i++)
        {
            GlobalVars.manurestoreRecord amanurestoreRecord = manurestoreDetails[i];
            amanurestoreRecord.GettheStore().Addmanure(amanurestoreRecord.GetmanureToStorage(), amanurestoreRecord.GetpropYearGrazing());
        }
    }

    //! A normal member, Get Mean Temperature. Taking one argument and returning one double value.
    /*!
     \param daysOnPasture, one integer argument.
     \return a double value.
    */
    private double GetMeanTemperature(int daysOnPasture)
    {
        int startDay=1;
        int endDay=365;
        double retVal = 0;
        if (daysOnPasture == 0)
            retVal = GlobalVars.Instance.GetDegreeDays(startDay, endDay, 0.0, GlobalVars.Instance.theZoneData.GetaverageAirTemperature(),
                GlobalVars.Instance.theZoneData.GetairtemperatureAmplitude(), GlobalVars.Instance.theZoneData.GetairtemperatureOffset());
        else
        {
            int midpoint = GlobalVars.Instance.theZoneData.GetgrazingMidpoint();
            //Jonas - this division will loose data
            int halfPoint = midpoint - daysOnPasture / 2;
            if (halfPoint < 0)
            {
                startDay = midpoint + daysOnPasture / 2;
                endDay = 365 - startDay;
                retVal = GlobalVars.Instance.GetDegreeDays(startDay, endDay, 0.0, GlobalVars.Instance.theZoneData.GetaverageAirTemperature(),
                    GlobalVars.Instance.theZoneData.GetairtemperatureAmplitude(), GlobalVars.Instance.theZoneData.GetairtemperatureOffset());
            }
            else
            {
                startDay = 1;
                endDay = midpoint - daysOnPasture / 2;
                retVal = GlobalVars.Instance.GetDegreeDays(startDay, endDay, 0.0, GlobalVars.Instance.theZoneData.GetaverageAirTemperature(),
                    GlobalVars.Instance.theZoneData.GetairtemperatureAmplitude(), GlobalVars.Instance.theZoneData.GetairtemperatureOffset());
                startDay = midpoint + daysOnPasture / 2;
                endDay = 365;
                retVal += GlobalVars.Instance.GetDegreeDays(startDay, endDay, 0.0, GlobalVars.Instance.theZoneData.GetaverageAirTemperature(),
                    GlobalVars.Instance.theZoneData.GetairtemperatureAmplitude(), GlobalVars.Instance.theZoneData.GetairtemperatureOffset());
            }
        }
        retVal /= (365 - daysOnPasture);

        return retVal;
    }
    //! A normal member, Get Register FeedWaste.
    
    void registerFeedWaste()
    {
        wasteFeed = new feedItem();
        for (int i = 0; i < GlobalVars.Instance.getmaxNumberFeedItems(); i++)
        {
            for (int j = 0; j < feedInHouse.Count; j++)
                if(feedInHouse[j]!=null)
                if (feedInHouse[j].GetFeedCode() == i)
                {
                    feedItem afeedItem = new feedItem();
                    afeedItem.setFeedCode(i);
                    afeedItem.AddFeedItem(feedInHouse[j], false);
                    double amountConsumedPerYear=theLivestock.GetAvgNumberOfAnimal() * GlobalVars.Instance.GetavgNumberOfDays() 
                        * feedInHouse[j].Getamount();
                    double wastedAmountPerYear =feedWasteFactor * amountConsumedPerYear/(1-feedWasteFactor) ;
                    afeedItem.Setamount(wastedAmountPerYear);
                    GlobalVars.Instance.allFeedAndProductsUsed[i].composition.AddFeedItem(afeedItem, false);
                    wasteFeed.AddFeedItem(afeedItem,true,false);
                    break;
                }
        }
    }
    //! A normal member, Do Carbon.
    void DoCarbon()
    {
        if (theLivestock.GetpropExcretaField() < 1.0)
        {
            double amount = 0;
            double C_conc = 0;
            double fibre_conc = 0;
            for (int i = 0; i < feedInHouse.Count; i++)
            {
                if (feedInHouse[i] != null)
                {
                    amount += feedInHouse[i].Getamount();
                    C_conc += feedInHouse[i].GetC_conc() * feedInHouse[i].Getamount();
                    fibre_conc += feedInHouse[i].Getfibre_conc() * feedInHouse[i].Getamount();
                }
            }
            C_conc /= amount;
            fibre_conc /= amount;
            beddingC = beddingDM * GlobalVars.Instance.GetthebeddingMaterial().GetC_conc();
            //feedWasteFactor = 0;
            registerFeedWaste();
            feedWasteC = wasteFeed.Getamount() * wasteFeed.GetC_conc();
            double urineC = theLivestock.GeturineC() * theLivestock.GetAvgNumberOfAnimal();
            double faecalC = theLivestock.GetfaecalC() * theLivestock.GetAvgNumberOfAnimal();
            Cinput = propTimeThisHouse * (1 - theLivestock.GetpropExcretaField()) * (faecalC + urineC) + beddingC + feedWasteC;
            CO2C = propTimeThisHouse * (1 - theLivestock.GetpropExcretaField()) * urineC;
            CtoStorage = Cinput - CO2C;
            //        FibreCToStorage = (1 - theLivestock.GetpropExcretaField()) * theLivestock.GetCintake() * (theLivestock.GetFibre() / (theLivestock.GetDMintake()))
            //          + beddingC * GlobalVars.Instance.GetthebeddingMaterial().Getfibre_conc();
            FibreCToStorage = ((1 - theLivestock.GetpropExcretaField()) * theLivestock.GetCintake() * theLivestock.GetAvgNumberOfAnimal() + feedWasteC) * fibre_conc
                + beddingC * GlobalVars.Instance.GetthebeddingMaterial().Getfibre_conc();
            double nonDegC;
            double DegC;
            double beddingBo = GlobalVars.Instance.GetthebeddingMaterial().GetBo();
            double feedWasteBo = wasteFeed.GetBo();
            double excretaBo = theLivestock.GetBo();
            double manureBo = (beddingC * beddingBo + feedWasteC * feedWasteBo + excretaBo * faecalC) / (beddingC + feedWasteC + faecalC);
            GlobalVars.manurestoreRecord amanurestoreRecord;
            switch (manurestoreDetails.Count)
            {
                case 0:
                    string messageString=("Error - No manure storage destinations");
                    GlobalVars.Instance.Error(messageString);
                    break;
                case 1:
                    //proportionNondegradable is ignored - only one store
                    amanurestoreRecord = manurestoreDetails[0];
                    nonDegC = FibreCToStorage;
                    DegC = CtoStorage - FibreCToStorage;
                    amanurestoreRecord.GetmanureToStorage().SetdegC(DegC);
                    amanurestoreRecord.GetmanureToStorage().SetnonDegC(nonDegC);
                    amanurestoreRecord.GetmanureToStorage().SetBo(manureBo);
                    amanurestoreRecord.SetpropYearGrazing(theLivestock.GetpropDMgrazed());
                    break;
                case 2:
                    bool gotSolid = false;
                    bool gotLiquid = false;
                    for (int i = 0; i < manurestoreDetails.Count; i++)
                    {
                        amanurestoreRecord = manurestoreDetails[i];
                        if ((proportionNondegradable == 0) && (proportionDegradable == 0))
                        {
                            messageString = ("Error - proportionNondegradable & proportionDegradable are both zero, in housing " + Name);
                            GlobalVars.Instance.Error(messageString);
                            break;
                        }
                        if (amanurestoreRecord.GettheStore().GetStoresSolid())
                        {
                            nonDegC = proportionNondegradable * FibreCToStorage;
                            DegC = proportionDegradable * (CtoStorage - FibreCToStorage);
                            if (gotSolid)
                            {
                                messageString = ("Error - two manure storage destinations receive solid manure");
                                GlobalVars.Instance.Error(messageString);
                                break;
                            }
                            else
                                gotSolid = true;
                        }
                        else
                        {
                            nonDegC = (1 - proportionNondegradable) * FibreCToStorage;
                            DegC = (1 - proportionDegradable) * (CtoStorage - FibreCToStorage);
                            if (gotLiquid)
                            {
                                messageString = ("Error - two manure storage destinations receive liquid manure");
                                GlobalVars.Instance.Error(messageString);
                                break;
                            }
                            else
                                gotLiquid = true;
                        }
                        if (DegC < 0)
                        {
                            messageString = "Error - degradable carbon is less than zero for " + Name;
                            GlobalVars.Instance.Error(messageString);
                        }
                        amanurestoreRecord.GetmanureToStorage().SetdegC(DegC);
                        amanurestoreRecord.GetmanureToStorage().SetnonDegC(nonDegC);
                        amanurestoreRecord.GetmanureToStorage().SetBo(manureBo);
                        amanurestoreRecord.SetpropYearGrazing(theLivestock.GetpropDMgrazed());
                        //send C to manure store
                    }
                    break;
                default:
                    messageString=("Error - too manure storage destinations");
                    GlobalVars.Instance.Error(messageString);
                    break;
            }
        }
    }
    //! A normal member, Do Nitrogen.
    public void DoNitrogen()
    {
        if (theLivestock.GetpropExcretaField() < 1.0)
        {
            NtanInhouse = propTimeThisHouse * (1 - theLivestock.GetpropExcretaField()) * theLivestock.GeturineN() * theLivestock.GetAvgNumberOfAnimal(); //1.37
            double amount = 0;
            double N_conc = 0;
            TANtoThisStore[0] = 0;
            labileOrganicNtoThisStore[0] = 0;
            TANtoThisStore[1] = 0;
            labileOrganicNtoThisStore[1] = 0;

            for (int i = 0; i < feedInHouse.Count; i++)
            {
                if (feedInHouse[i] != null)
                {
                    amount += feedInHouse[i].Getamount();
                    N_conc += feedInHouse[i].GetN_conc() * feedInHouse[i].Getamount();
                }
            }
            N_conc /= amount;
            NfedInHousing = N_conc * amount * GlobalVars.avgNumberOfDays * theLivestock.GetAvgNumberOfAnimal();
            NWasteFeed = wasteFeed.Getamount() * wasteFeed.GetN_conc();
            Nbedding = beddingDM * GlobalVars.Instance.GetthebeddingMaterial().GetN_conc();
            faecalN = propTimeThisHouse * (1 - theLivestock.GetpropExcretaField()) * theLivestock.GetfaecalN() * theLivestock.GetAvgNumberOfAnimal();
            NorgInHouse = Nbedding + NWasteFeed + faecalN;
            Ninput = NorgInHouse + NtanInhouse;

            switch (GlobalVars.Instance.getcurrentInventorySystem())
            {
                case 1://UNECE method           
                    NNH3housing = EFNH3housingTier2* NtanInhouse;
                    NTanInstore = NtanInhouse - NNH3housing;
                    break;
                case 2://IPCC method, NH3 emissions from housing are included in manure storage emissions 
                    NNH3housing = 0.0;
                    NTanInstore = NtanInhouse;
                    break;
                case 3:
                    double KHtheta = Math.Pow(10, -1.69 + 1447.7 / (meanTemp + GlobalVars.absoluteTemp));
                    double KHref = Math.Pow(10, -1.69 + 1447.7 / (HousingRefTemp + GlobalVars.absoluteTemp));
                    double EFNH3theta = (KHref / KHtheta) * EFNH3ref;
                    NNH3housing = propTimeThisHouse * (1 - theLivestock.GetpropExcretaField()) * EFNH3theta * NtanInhouse;
                    NTanInstore = NtanInhouse - NNH3housing;
                    break;
            }
            GlobalVars.manurestoreRecord amanurestoreRecord;
            switch (manurestoreDetails.Count)
            {
                case 1:
                    amanurestoreRecord = manurestoreDetails[0];
                    TANtoThisStore[0] = NTanInstore;
                    labileOrganicNtoThisStore[0] = NorgInHouse;
                    amanurestoreRecord.GetmanureToStorage().SetTAN(TANtoThisStore[0]);
                    amanurestoreRecord.GetmanureToStorage().SetlabileOrganicN(labileOrganicNtoThisStore[0]);
                    break;
                case 2:
                    for (int i = 0; i < manurestoreDetails.Count; i++)
                    {
                        amanurestoreRecord = manurestoreDetails[i];
                        if (amanurestoreRecord.GettheStore().GetStoresSolid())
                        {
                            TANtoThisStore[i] = proportionTAN * NTanInstore;
                            labileOrganicNtoThisStore[i] = proportionDegradable * NorgInHouse;
                        }
                        else
                        {
                            TANtoThisStore[i] = (1 - proportionTAN) * NTanInstore;
                            labileOrganicNtoThisStore[i] = (1 - proportionDegradable) * NorgInHouse;
                        }
                        amanurestoreRecord.GetmanureToStorage().SetTAN(TANtoThisStore[i]);
                        amanurestoreRecord.GetmanureToStorage().SetlabileOrganicN(labileOrganicNtoThisStore[i]);
                    }
                    break;
                default:
                    string messageString=("Error - too many manure storage destinations");
                    
                    GlobalVars.Instance.Error(messageString);
                    break;
            }
        }
    }
    //! A normal member, Write.
    public void Write()
    {
        if (GlobalVars.Instance.getRunFullModel())
            theLivestock.Write();
        GlobalVars.Instance.writeStartTab("Housing");
        for (int i = 0; i < feedInHouse.Count; i++)
        {

            if (feedInHouse[i] != null)
            feedInHouse[i].Write(parens+"_");
        }

        GlobalVars.Instance.writeInformationToFiles("Name", "Name", "-", Name, parens);
        GlobalVars.Instance.writeInformationToFiles("HousingType", "Type of housing", "-", HousingType, parens);

        GlobalVars.Instance.writeInformationToFiles("Cinput", "Total C input to housing", "kg", Cinput, parens);
        GlobalVars.Instance.writeInformationToFiles("CO2C", "CO2-C emitted from housing", "kg", CO2C, parens);
        GlobalVars.Instance.writeInformationToFiles("CtoStorage", "C sent to manure storage", "kg", CtoStorage, parens);
        //GlobalVars.Instance.writeInformationToFiles("HousingRefTemp", "Reference temperature for housing NH3 emissions", "Celsius", HousingRefTemp);

        GlobalVars.Instance.writeInformationToFiles("Ninput", "Total N entering the housing", "kg", Ninput, parens);
        GlobalVars.Instance.writeInformationToFiles("NtanInhouse", "TAN entering the housing", "kg", NtanInhouse, parens);
        GlobalVars.Instance.writeInformationToFiles("NorgInHouse", "Organic N entering the housing", "kg", NorgInHouse, parens);
        GlobalVars.Instance.writeInformationToFiles("faecalNInHouseing", "Faecal N deposited", "kg", faecalN, parens);
        GlobalVars.Instance.writeInformationToFiles("Nbedding", "N added in bedding", "kg", Nbedding, parens);
        GlobalVars.Instance.writeInformationToFiles("NWasteFeed", "N added in waste feed", "kg", NWasteFeed, parens);
        GlobalVars.Instance.writeInformationToFiles("NNH3housing", "NH3-N emitted from housing", "kg", NNH3housing, parens);

/*        for (int i = 0; i < manurestoreDetails.Count; i++)
        {
            manurestoreDetails[i].Write();
        }*/
        GlobalVars.Instance.writeInformationToFiles("NLost", "Total N lost", "kg", NLost, parens);
        GlobalVars.Instance.writeInformationToFiles("Nout", "N leaving housing", "kg", Nout, parens);
        GlobalVars.Instance.writeInformationToFiles("Nbalance", "N budget check", "kg", Nbalance, parens);
        if (!GlobalVars.Instance.getRunFullModel())
            GlobalVars.Instance.writeEndTab();
    }
    //! A normal member, Check Housing C Balance.
    /*!
    \return a boolean value.
   */
    public bool CheckHousingCBalance()
    {
        bool retVal = false;
        double Cout = getManureCarbon();
        double CLost = CO2C;
        double Cbalance = Cinput - (Cout + CLost);
        double diff = Cbalance / Cinput;
        double tolerance = GlobalVars.Instance.getmaxToleratedErrorYield();
        if (Math.Abs(diff) > tolerance)
        {
            double errorPercent = 100 * diff;
            string messageString=("Error; Housing C balance error is more than the permitted margin\n");
            messageString+=("Percentage error = " + errorPercent.ToString("0.00") + "%");
            GlobalVars.Instance.Error(messageString);
        }
        return retVal;
    }
    //! A normal member, Check Housing N Balance.
    /*!
    \return a boolean value.
   */
    public bool CheckHousingNBalance()
    {
        bool retVal = false;
        Nout = getManureNitrogen();
        NLost = NNH3housing;
        Nbalance = Ninput - (Nout + NLost);
        double diff = Nbalance / Ninput;
        double tolerance = GlobalVars.Instance.getmaxToleratedErrorYield();
        if (Math.Abs(diff) > tolerance)
        {
                double errorPercent = 100 * diff;
                string messageString =("Error; Housing N balance error is more than the permitted margin\n");
                messageString+=("Percentage error = " + errorPercent.ToString("0.00") + "%");
                GlobalVars.Instance.Error(messageString);
        }
        return retVal;
    }
  
}
