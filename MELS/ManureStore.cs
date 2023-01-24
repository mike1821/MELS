using System;
using System.Collections.Generic;
using System.Xml;
//! A class that named manureStore. 
public class manureStore
{
    string path;
    ///inputs

    ///parameters
    string name; //! Name of the manure storage
    int ManureStorageID;
    int speciesGroup;
    double b1;
    double lnArr;
    double StorageRefTemp;
    double meanTemp;
    double MCF; //needs to be AEZ specific
    //MELS-2023
    double AWMS;
    double EFStoreNH3;
    double EFStoreN20;
    double Lambda;
    double propGasCapture;

    ///other variables
    string parens;
    int identity;
    manure theManure;
    housing theHousing;
    livestock theLiveStock;

    double tstore=0;
    double CdegradationRate;
    double Cinput=0;
    double CCH4ST=0;
    double CCO2ST = 0;
    double Cdegradation = 0;
    double NTanInstore;
    double NlabileOrgInstore = 0;
    double NhumicInstore = 0;
    double NDegOrgOut;
    double ohmOrg;
    double ohmTAN;
    double NRunOffLabileOrg;
    double CRunOffOrg;
    double newNHUM;
    double NRunoffHum;
    double NlabileOrgOutStore;
    double NhumicOutstore = 0;
    double NTanOutstore;
    double NTANLost;
    double NrunoffTan;
    double totalNstoreNH3;
    double totalNstoreN2;
    double totalNstoreN20;
    double Ninput = 0;
    double Nout = 0;
    double NLost = 0;
    double Nbalance = 0;
    double biogasCH4C = 0;
    double biogasCO2C = 0;
    double supplementaryN = 0;
    double supplementaryC = 0;
    double feedStuff = 0;
    public List<feedItem> supplementaryFeedstock;
    //! A normal member. Get CCH4ST. Returning one value.
    /*!
     \return a double value.
    */
    public double GetCCH4ST() { return CCH4ST; }
    //! A normal member. Get CCO2ST. Returning one value.
    /*!
     \return a double value.
    */
    public double GetCCO2ST() { return CCO2ST; }
    //! A normal member. Get totalN store NH3. Returning one value.
    /*!
     \return a double value.
    */
    public double GettotalNstoreNH3() { return totalNstoreNH3; }
    //! A normal member. Get totalN store N2. Returning one value.
    /*!
     \return a double value.
    */
    public double GettotalNstoreN2() { return totalNstoreN2; }
    //! A normal member. Get totalN store N2O. Returning one value.
    /*!
     \return a double value.
    */
    public double GettotalNstoreN20() { return totalNstoreN20; }
    //! A normal member. Get run off N. Returning one value.
    /*!
     \return a double value.
    */
    public double GetrunoffN() { return NRunoffHum + NRunOffLabileOrg + NrunoffTan; }
    //! A normal member. Get run off C. Returning one value.
    /*!
     \return a double value.
    */
    public double GetrunoffC() { return CRunOffOrg; }
    //! A normal member. Get biogas CH4C. Returning one value.
    /*!
     \return a double value.
    */
    public double GetbiogasCH4C() { return biogasCH4C; }
    //! A normal member. Get biogas CO2C. Returning one value.
    /*!
     \return a double value.
    */
    public double GetbiogasCO2C() { return biogasCO2C; }
    //! A normal member. Get supplementary C. Returning one value.
    /*!
     \return a double value.
    */
    public double GetsupplementaryC() { return supplementaryC; }
    //! A normal member. Get supplementary N. Returning one value.
    /*!
     \return a double value.
    */
    public double GetsupplementaryN() { return supplementaryN; }
    //! A normal member. Get ManureC. Returning one value.
    /*!
     \return a double value.
    */
    public double GetManureC()
    {
        double retVal = 0;
        retVal = theManure.GetdegC() + theManure.GethumicC() + theManure.GetnonDegC();
        return retVal;
    }
    //! A normal member. Get ManureN. Returning one value.
    /*!
     \return a double value.
    */
    public double GetManureN()
    {
        double retVal = 0;
        retVal = theManure.GetTAN()+ theManure.GetlabileOrganicN() + theManure.GethumicN();
        return retVal;
    }
    //! A constructor. 
    /*!
     without argument.
    */
    private manureStore()
    {

    }
    //! A constructor with four arguments.
    /*!
     \param aPath, one string argument.
     \param id, one integer argument.
     \param zoneNr, one integer argument.
     \param aParens, one string argument.
    */
    public manureStore(string aPath, int id, int zoneNr, string aParens)
    {
        supplementaryFeedstock = new List<feedItem>();
        string parens = aParens;
        FileInformation manureStoreFile = new FileInformation(GlobalVars.Instance.getFarmFilePath());
        identity = id;
        path=aPath+'('+id.ToString()+')';

        manureStoreFile.setPath(path);
        name=manureStoreFile.getItemString("NameOfStorage");
        ManureStorageID = manureStoreFile.getItemInt("StorageType");
        speciesGroup = manureStoreFile.getItemInt("SpeciesGroup");
        getParameters(zoneNr);
    }
    //! A constructor with four arguments.
    /*!
     \param manureStorageType, one integer argument.
     \param livestockSpeciesGroup, one integer argument.
     \param zoneNr, one integer argument.
     \param aParens, one string argument.
    */
    public manureStore(int manureStorageType, int livestockSpeciesGroup, int zoneNr, string aParens)
    {
        supplementaryFeedstock = new List<feedItem>();
        ManureStorageID = manureStorageType;
        speciesGroup = livestockSpeciesGroup;
        getParameters(zoneNr);
        parens = aParens;
    }
    //! A normal member. Get Parameters. Taking one argument.
    /*!
     \param zoneNr, one integer argument.
    */
    public void getParameters(int zoneNr)
    {
        FileInformation manureParamFile = new FileInformation(GlobalVars.Instance.getParamFilePath());
        manureParamFile.setPath("AgroecologicalZone("+zoneNr.ToString()+").ManureStorage");
        int maxManure = 0, minManure = 99;
        manureParamFile.getSectionNumber(ref minManure, ref maxManure);

        bool found = false;
        int num=0;
        //GlobalVars.Instance.log("ind " + " Req " + " test" + " sg ");
        for (int i = minManure; i <= maxManure; i++)
        {
            if (manureParamFile.doesIDExist(i))
            {
                manureParamFile.Identity.Add(i);
                int tmpStorageType = manureParamFile.getItemInt("StorageType");
                int tmpSpeciesGroup = manureParamFile.getItemInt("SpeciesGroup");
                name = manureParamFile.getItemString("Name");
                //  GlobalVars.Instance.log(i.ToString()+ " " + storageType.ToString()+ " "+ tmpStorageType.ToString()+ " "+ speciesGroup.ToString()+ " "+ tmpSpeciesGroup.ToString());
                if (ManureStorageID == tmpStorageType & speciesGroup == tmpSpeciesGroup)
                {
                    found = true;
                    num = i;
                    break;
                }
                manureParamFile.Identity.RemoveAt(manureParamFile.Identity.Count - 1);
            }
        }
        if (found == false)
        {
            string messageString = ("could not match StorageType and SpeciesGroup at ManureStore. Was trying to find StorageType " + ManureStorageID.ToString() + " and speciesGroup " + speciesGroup.ToString());
          GlobalVars.Instance.Error(messageString);
        }
        string RecipientPath = "AgroecologicalZone("+zoneNr.ToString()+").ManureStorage" + '(' + num.ToString() + ").StoresSolid(-1)";
        bool StoresSolid;
        string tempString = manureParamFile.getItemString("Value",RecipientPath);
        if (tempString == "true")
            StoresSolid = true;
        else
            StoresSolid = false;
        manureParamFile.PathNames[manureParamFile.PathNames.Count - 1] = "b1";
        b1 = manureParamFile.getItemDouble("Value");
        manureParamFile.PathNames[manureParamFile.PathNames.Count - 1] = "lnArr";
        lnArr = manureParamFile.getItemDouble("Value");
        manureParamFile.PathNames[manureParamFile.PathNames.Count - 1] = "ohmOrg";
        ohmOrg = manureParamFile.getItemDouble("Value");
        manureParamFile.PathNames[manureParamFile.PathNames.Count - 1] = "ohmTAN";
        ohmTAN = manureParamFile.getItemDouble("Value");
        manureParamFile.PathNames[manureParamFile.PathNames.Count - 1] = "MCF";
        MCF = manureParamFile.getItemDouble("Value");
        //MELS-2023
        if(GlobalVars.Instance.GetLocation() == "East"){
            manureParamFile.PathNames[manureParamFile.PathNames.Count - 1] = "AWMSEast";
        }
        else{
            manureParamFile.PathNames[manureParamFile.PathNames.Count - 1] = "AWMSWest";
        }
        AWMS = manureParamFile.getItemDouble("Value");
        Console.WriteLine("Detected " + GlobalVars.Instance.GetLocation() + ", Setting AWMS = " + AWMS );
        switch (GlobalVars.Instance.getcurrentInventorySystem())
        {
            case 1:
            case 2:
                manureParamFile.PathNames[manureParamFile.PathNames.Count - 1] = "EFNH3storageIPCC";
                EFStoreNH3 = manureParamFile.getItemDouble("Value");
                manureParamFile.PathNames[manureParamFile.PathNames.Count - 1] = "EFN2OstorageIPCC";
                EFStoreN20 = manureParamFile.getItemDouble("Value");
                break;
            case 3:
                manureParamFile.PathNames[manureParamFile.PathNames.Count - 1] = "meanTemp";
                meanTemp = manureParamFile.getItemDouble("Value");
                manureParamFile.PathNames[manureParamFile.PathNames.Count - 1] = "EFNH3storageRef";
                EFStoreNH3 = manureParamFile.getItemDouble("Value");
                manureParamFile.PathNames[manureParamFile.PathNames.Count - 1] = "EFN2OstorageRef";
                EFStoreN20 = manureParamFile.getItemDouble("Value");
                manureParamFile.PathNames[manureParamFile.PathNames.Count - 1] = "StorageRefTemp";
                StorageRefTemp = manureParamFile.getItemDouble("Value");
                break;
        }
        manureParamFile.PathNames[manureParamFile.PathNames.Count - 1] = "PropGasCapture";
        propGasCapture = manureParamFile.getItemDouble("Value");
        manureParamFile.PathNames[manureParamFile.PathNames.Count - 1] = "lambda_m";
        Lambda = manureParamFile.getItemDouble("Value");
        string aPath = "AgroecologicalZone(" + zoneNr.ToString() + ").ManureStorage(" + num.ToString() + ").SupplementaryFeedstocks(-1).Feedstock";
        manureParamFile.setPath(aPath);
        int minsuppFeed = 99, maxsuppFeed = 0;
        manureParamFile.getSectionNumber(ref minsuppFeed, ref maxsuppFeed);
        for (int k = minsuppFeed; k <= maxsuppFeed; k++)
        {
            manureParamFile.Identity.Add(k);
            feedItem aFeedstock = new feedItem();
            aFeedstock.Setamount(manureParamFile.getItemDouble("Amount"));
            aFeedstock.GetStandardFeedItem(manureParamFile.getItemInt("FeedCode"));
            supplementaryFeedstock.Add(aFeedstock);
            manureParamFile.Identity.RemoveAt(manureParamFile.Identity.Count - 1);
        }

        theManure = new manure();
        theManure.SetisSolid(StoresSolid);
        //indicate the type of manure
        theManure.SetspeciesGroup(speciesGroup);
        FileInformation file = new FileInformation(GlobalVars.Instance.getfertManFilePath());
        file.setPath("AgroecologicalZone("+GlobalVars.Instance.GetZone().ToString()+").manure");
        int min = 99; int max = 0;
        file.getSectionNumber(ref min, ref max);
  
        //int itemNr = 0;
        bool gotit = false;
        int j = min;

        while ((j <= max) && (gotit == false))
        {
            if(file.doesIDExist(j))
            {
        
                file.Identity.Add(j);
                int StoredTypeFile = file.getItemInt("ManureType");
                int SpeciesGroupFile = file.getItemInt("SpeciesGroup");
                string manureName = file.getItemString("Name");

                if (StoredTypeFile == ManureStorageID && SpeciesGroupFile == speciesGroup)
                {
                    //itemNr = j;
                    theManure.SetmanureType(ManureStorageID);
                    theManure.Setname(manureName);
                    gotit = true;
                }
                j++;
                file.Identity.RemoveAt(file.Identity.Count-1);
            }
        }
        if (gotit == false)
        {
            string messageString = "Error - manure type not found for manure storage " + name + " ManureStorageID = " 
                + ManureStorageID.ToString() + " Species group = " + speciesGroup.ToString();
            GlobalVars.Instance.Error(messageString);
        }
       // theManure.SetmanureType(itemNr);
    }
    //! A normal member. Set Name. Taking one argument.
    /*!
     \param aname, one string argument.
    */
    public void Setname(string aname) { name = aname; }
    //! A normal member. Set Identity. Taking one argument.
    /*!
     \param aValue, one integer argument.
    */
    public void Setidentity(int aValue) { identity = aValue; }
    //! A normal member. Set speciesGroup. Taking one argument.
    /*!
     \param aValue, one integer argument.
    */
    public void SetspeciesGroup(int aValue) { speciesGroup = aValue; }
    //! A normal member. Set Manure StorageID. Taking one argument.
    /*!
     \param aValue, one integer argument.
    */
    public void SetManureStorageID(int aValue) { ManureStorageID = aValue; }
    //! A normal member. Set the Housing. Taking one argument.
    /*!
     \param ahouse, one integer argument.
    */
    public void SettheHousing(housing ahouse){theHousing=ahouse;}
    //! A normal member. Get Name. Returning one value.
    /*!
     \return a string value.
    */
    public string Getname() { return name; }
    //! A normal member. Get Identity. Returning one value.
    /*!
     \return an integer value.
    */
    public int Getidentity() { return identity; }
    //! A normal member. Get Manure StorageID. Returning one value.
    /*!
     \return an integer value.
    */
    public int GetManureStorageID() { return ManureStorageID; }
    //! A normal member. Get speiciesGroup. Returning one value.
    /*!
     \return an integer value.
    */
    public int GetspeciesGroup() { return speciesGroup; }
    //! A normal member. Get the Housing. Returning one value.
    /*!
     \return a housing class instance value.
    */
    public housing GettheHousing() { return theHousing; }
    //! A normal member. Get StoresSolid. Returning one value.
    /*!
     \return a boolean value.
    */
    public bool GetStoresSolid() { return theManure.GetisSolid(); }
    //! A normal member. Add Manure. Taking two arguments and Returning one value.
    /*!
     \param amanure, one manure class instance argument.
     \param proportionOfYearGrazing, one double argument.
    */
    public void Addmanure(manure amanure, double proportionOfYearGrazing)
    {
        theManure.AddManure(amanure);
        Cinput += amanure.GetdegC();
        Cinput += amanure.GetnonDegC();
        tstore += ((1 - proportionOfYearGrazing) * (theManure.GetdegC() + theManure.GetnonDegC()) / (2 * (theManure.GetdegC() + theManure.GetnonDegC())));
    }
    //! A normal member. Update Manure Exchange.
   
    public void UpdateManureExchange()
    {
        manure manureToManureExchange = new manure(theManure);
        //manureToManureExchange.IncreaseManure(GlobalVars.Instance.theZoneData.GetaverageYearsToSimulate());
        GlobalVars.Instance.theManureExchange.AddToManureExchange(manureToManureExchange);
    }
    //! A normal member. Do Manurestore.
    public void DoManurestore()
    {
        supplementaryC = 0;
        supplementaryN = 0;
        double temp = theManure.GetnonDegC() + theManure.GetdegC() +theManure.GetTAN() + theManure.GetorganicN();
        if (temp > 0.0)
        {
            double Bo = theManure.GetBo();
            if (supplementaryFeedstock.Count > 0)
            {
                double degSupplC = 0;
                double nondegSupplC = 0;
                double supplN = 0;
                double manureDM = Cinput / 0.42; //hack!
                double cumBo = 0;
                double cumSupplDM=0;
                for (int i = 0; i < supplementaryFeedstock.Count; i++)
                {
                    feedItem aFeedstock = supplementaryFeedstock[i];
                    double amountThisFeedstock = manureDM * aFeedstock.Getamount(); //kg DM
                    if (amountThisFeedstock > 0)
                    {
                        cumSupplDM += amountThisFeedstock;
                        cumBo += amountThisFeedstock * aFeedstock.GetBo();
                        nondegSupplC += amountThisFeedstock * aFeedstock.GetC_conc() * aFeedstock.Getfibre_conc();
                        degSupplC += amountThisFeedstock * aFeedstock.GetC_conc() * (1 - aFeedstock.Getfibre_conc());
                        supplementaryC += nondegSupplC + degSupplC;
                        supplN = amountThisFeedstock * aFeedstock.GetN_conc();
                        supplementaryN += supplN;
                        aFeedstock.Setamount(amountThisFeedstock); //convert from amount per unit manure DM mass to total amount
                        GlobalVars.Instance.allFeedAndProductsUsed[aFeedstock.GetFeedCode()].composition.AddFeedItem(aFeedstock, false);
                    }
                }
                if (cumSupplDM > 0)
                {
                    double aveSupplBo = cumBo / cumSupplDM;
                    Bo = (manureDM * Bo + cumSupplDM * aveSupplBo) / (manureDM + cumSupplDM);
                }
                theManure.SetBo(Bo);
                theManure.SetdegC(theManure.GetdegC() + degSupplC);
                theManure.SetnonDegC(theManure.GetnonDegC() + nondegSupplC);
                theManure.SetlabileOrganicN(theManure.GetlabileOrganicN() + supplementaryN);
            }
            DoCarbon();
            DoNitrogen();
            CheckManureStoreNBalance();
            UpdateManureExchange();
            temp = theManure.GetnonDegC() + theManure.GetdegC();
        }
    }
    //! A normal member. Do Carbon.
    public void DoCarbon()
    {
        Cinput = GetManureC();
        double tor = GlobalVars.Instance.gettor();
        double rgas = GlobalVars.Instance.getrgas();
        double aveTemperature = GlobalVars.Instance.theZoneData.GetaverageAirTemperature();
        switch (GlobalVars.Instance.getcurrentInventorySystem())
        {
            case 1:
            case 2:
                CRunOffOrg = Cinput * ohmOrg; //assume runoff occurs immediately, before degradation
                theManure.SetdegC(theManure.GetdegC() * (1 - ohmOrg));
                theManure.SetnonDegC(theManure.GetnonDegC() * (1 - ohmOrg));
                if ((theManure.GetmanureType() != 5) && (theManure.GetmanureType() < 11))
                {
                    bool isCovered = false;
                    switch (theManure.GetmanureType())
                    {
                        case 2: isCovered = true;
                            break;
                        case 4: isCovered = true;
                            break;
                        case 9: isCovered = true;
                            break;
                        case 10: isCovered = true;
                            break;
                    }
                    if (GlobalVars.Instance.getcurrentInventorySystem() == 1)
                    {
                        if (theManure.GetisSolid())
                        {
                            if (aveTemperature < 14.5)
                                MCF = 0.02;
                            if ((aveTemperature >= 14.5) && (aveTemperature < 25.5))
                                MCF = 0.04;
                            if (aveTemperature >= 25.5)
                                MCF = 0.05;
                        }
                        else
                        {
                            if (isCovered)
                                MCF = Math.Exp(0.0896159864767708 * aveTemperature - 3.1458426322101);
                            else
                                MCF = Math.Exp(0.088371620269402 * aveTemperature - 2.64281541545576);
                        }
                    }
                }
                double Bo = theManure.GetBo();
                CCH4ST = (1 / GlobalVars.Instance.getalpha()) * (theManure.GetdegC() + theManure.GetnonDegC() + theManure.GethumicC()) * (Bo * 0.67 * MCF);//1.46
                // double fT=Math.Exp(((-1.22*100000)/rgas)*((1/(meanTemp + GlobalVars.absoluteTemp))-(1/(15.0 + GlobalVars.absoluteTemp))));
                // double km = 0.39;

                // In order to use MCF from ManureStorage "InventorySystem" parameter must be 0 in farm file. 
                // We also need to adjust Cdegradation below by checking fertMan.xml file (selected fertiliser)
                // MELS-2023
                // CCH4ST = MCF * VS * Bo * 0.67 * 12 / 16;
                // double VS = (theManure.GetdegC() + theManure.GetnonDegC() + theManure.GethumicC()) / GlobalVars.Instance.getalpha();
                double VS = theLiveStock.CalculateFeedStuff();
                CCH4ST = (VS)*(Bo*0.67*MCF*AWMS); //use calculated value from new function
                CCO2ST = (CCH4ST * (1 - tor)) / tor;

                double biogasC = CCH4ST + CCO2ST;
                Cdegradation = biogasC / (1 - GlobalVars.Instance.getHumification_const());
                if (Cdegradation > theManure.GetdegC())
                {
                    if (Cdegradation > (theManure.GetdegC() + theManure.GetnonDegC()))
                    {
                        string message2 = "C degradation greater than sum of degradable and non-degradable C in store " + name;
                        GlobalVars.Instance.Error(message2);
                    }
                    else
                    {
                        double nonDegCdegraded = Cdegradation - theManure.GetdegC();
                        theManure.SetdegC(0.0);
                        theManure.SetnonDegC(theManure.GetnonDegC() - nonDegCdegraded);
                    }
                }
                else
                    theManure.SetdegC(theManure.GetdegC() - Cdegradation);
                theManure.SethumicC(theManure.GethumicC() + Cdegradation * GlobalVars.Instance.getHumification_const());
                break;
            case 3:
                string message1 = "Un-upgraded code in manure storage " + name;
                GlobalVars.Instance.Error(message1);
                break;
        }
        biogasCH4C = propGasCapture * CCH4ST;
        CCH4ST -= biogasCH4C;
        biogasCO2C = propGasCapture * CCO2ST;
        CCO2ST -= biogasCO2C;
        CheckManureStoreCBalance();
    }
    //! A normal member. Do Nitrogen.
    public void DoNitrogen()
    {
        NTanInstore = theManure.GetTAN();
        NlabileOrgInstore = theManure.GetlabileOrganicN();
        NhumicInstore = theManure.GethumicN();
        double totalOrgNdegradation = 0; //only used if GlobalVars.Instance.getcurrentInventorySystem() = 1
        double newTAN = 0;
        switch (GlobalVars.Instance.getcurrentInventorySystem())
        {
            case 1:
            case 2:
                NRunOffLabileOrg = NlabileOrgInstore * ohmOrg; //assume runoff occurs immediately, before degradation
                theManure.SetlabileOrganicN(theManure.GetlabileOrganicN() - NRunOffLabileOrg);
                NRunoffHum = theManure.GethumicN() * ohmOrg;
                theManure.SethumicN(theManure.GethumicN() - NRunoffHum);  //adjust humic N for loss in runoff
                totalOrgNdegradation = (Cdegradation / (Cinput-CRunOffOrg)) * NlabileOrgInstore;
                newNHUM = (theManure.GethumicC() / GlobalVars.Instance.getCNhum()) - theManure.GethumicN();
                if (totalOrgNdegradation < newNHUM)  //there will be immobilisation of TAN in humic N
                {
                    if ((theManure.GetTAN() + totalOrgNdegradation) < newNHUM)  //there is insufficient mineral N to create the humic N
                    {
                        string message2 = "Insufficient mineral N to create new humic N in " + name;
                        GlobalVars.Instance.Error(message2);
                    }
                    else
                    {
                        double immobilisedTAN = newNHUM - totalOrgNdegradation;  //immobilise some TAN
                        theManure.SetTAN(theManure.GetTAN() - immobilisedTAN);
                    }
                }
                NlabileOrgOutStore = NlabileOrgInstore - (NRunOffLabileOrg + totalOrgNdegradation);
                NhumicOutstore = NhumicInstore - NRunoffHum + newNHUM;
                newTAN = totalOrgNdegradation - newNHUM;
                break;
            case 3:
                string message1 = "Out of date code in Manurestore.cs for " + name;
                GlobalVars.Instance.Error(message1);
                //if (CdegradationRate > 0)
                //{
                //    NDegOrgOut = NlabileOrgInstore * Math.Exp(-(CdegradationRate + ohmOrg) * tstore * GlobalVars.avgNumberOfDays);
                //    NRunOffOrg = (ohmOrg / (ohmOrg + CdegradationRate)) * NlabileOrgInstore * (1 - Math.Pow(Math.E, -(ohmOrg + CdegradationRate * tstore * GlobalVars.avgNumberOfDays))); //1.59
                //    //disable until can get workng properly
                //    /*    NHUM = (GlobalVars.Instance.getHumification_const() / GlobalVars.Instance.getCNhum()) * CdegradationRate
                //            * (Math.Pow(Math.E, -(ohmOrg + CdegradationRate) * tstore) - Math.Pow(Math.E, -ohmOrg * tstore * GlobalVars.avgNumberOfDays));
                //        NRunoffHum = (CdegradationRate / (ohmOrg + CdegradationRate));*/
                //}
                //else
                //{
                //    NDegOrgOut = NlabileOrgInstore * Math.Exp(-ohmOrg * tstore * GlobalVars.avgNumberOfDays);
                //    NRunOffOrg = NlabileOrgInstore - NDegOrgOut;
                //    NRunoffHum = 0;
                //}
                //NorgOutStore = NDegOrgOut + NHUM;
                break;
        }

        switch (GlobalVars.Instance.getcurrentInventorySystem())
        {
            case 1:
                totalNstoreN20 = EFStoreN20 * (NTanInstore + NlabileOrgInstore); //1.64 - not quite..
                totalNstoreN2 = Lambda * totalNstoreN20;//1.66
                totalNstoreNH3 = EFStoreNH3 * (NTanInstore + newTAN);//1.65 - not quite..
                NrunoffTan = NTanInstore * ohmTAN;
                NTanOutstore = NTanInstore + totalOrgNdegradation - (totalNstoreN20 + totalNstoreN2 + totalNstoreNH3 + NrunoffTan + newNHUM);
                break;
            case 2:
                totalNstoreN20 = EFStoreN20 * theManure.GetTotalN(); //1.64 - not quite..
                totalNstoreN2 = Lambda * totalNstoreN20;//1.66
                totalNstoreNH3 = EFStoreNH3 * theManure.GetTotalN();//1.65 - not quite..
                NrunoffTan = NTanInstore * ohmTAN;
                NTanOutstore = NTanInstore + totalOrgNdegradation - (totalNstoreN20 + totalNstoreN2 + totalNstoreNH3 + NrunoffTan + newNHUM);
                break;
            case 3:
                double EFStoreN2 = EFStoreN20 * Lambda;
                double CN = theManure.GetdegC() / NlabileOrgInstore;
                double StorageRefTemp = 0;
                double EFNH3ref = 0;
                double KHø = 1 - 1.69 + 1447.7 / (meanTemp + GlobalVars.absoluteTemp);
                double KHref = 1 - 1.69 + 1447.7 / (StorageRefTemp + GlobalVars.absoluteTemp);
                EFStoreNH3 = KHref / KHø * EFNH3ref; //1.67
                double EFsum = EFStoreNH3 + EFStoreN20 + EFStoreN2;

                NTanOutstore = ((CdegradationRate * (1 / CN - GlobalVars.Instance.getHumification_const() / GlobalVars.Instance.getCNhum())
                    * theManure.GetOrgDegC()) / ((EFsum + ohmTAN) - (ohmOrg + CdegradationRate / CN)))
                    * Math.Pow(Math.E, -(ohmOrg + CdegradationRate / CN) * tstore);//1.63
                //NTanOutstore += NTanInstore - (theManure.GetdegC() * (1 / CN - tau / GlobalVars.Instance.getCNhum()) * theManure.GetOrgDegC()) / ((EFsum + ohmTAN) - (ohmOrg + theManure.GetdegC() / CN)) * Math.Pow(Math.E, -(EFsum + ohmOrg) * tstore);//1.63
                NTANLost = NlabileOrgInstore + NTanInstore - (NTanInstore + NrunoffTan + NTanOutstore);//1.68
                NrunoffTan = ohmTAN / (ohmTAN + EFStoreNH3 + EFStoreN20 + EFStoreN2) * NTANLost;
                totalNstoreNH3 = NTANLost * EFStoreNH3 / (ohmTAN + EFStoreN2 + EFStoreN20 + EFStoreNH3);
                totalNstoreN2 = NTANLost * EFStoreN2 / (ohmTAN + EFStoreN2 + EFStoreN20 + EFStoreNH3);
                totalNstoreN20 = NTANLost * EFStoreN20 / (ohmTAN + EFStoreN2 + EFStoreN20 + EFStoreNH3);
                break;
        }
        theManure.SethumicN(NhumicOutstore);
        theManure.SetlabileOrganicN(NlabileOrgOutStore);
        theManure.SetTAN(NTanOutstore);
    }
    //! A normal member. Do Write.
    public void Write()
    {
        if (GlobalVars.Instance.getRunFullModel())
            theHousing.Write();
        GlobalVars.Instance.writeStartTab("ManureStore");
        GlobalVars.Instance.writeInformationToFiles("name", "Name", "-", name, parens);
        GlobalVars.Instance.writeInformationToFiles("identity", "ID", "-", identity, parens);
        GlobalVars.Instance.writeInformationToFiles("Cinput", "C input", "kg", Cinput, parens);
        GlobalVars.Instance.writeInformationToFiles("CCH4ST", "CH4-C emitted", "kg", CCH4ST, parens);
        GlobalVars.Instance.writeInformationToFiles("CCO2ST", "CO2-C emitted", "kg", CCO2ST, parens);

        GlobalVars.Instance.writeInformationToFiles("Ninput", "N input", "kg", Ninput, parens);
        GlobalVars.Instance.writeInformationToFiles("NTanInstore", "TAN input to storage", "kg", NTanInstore, parens);
        GlobalVars.Instance.writeInformationToFiles("totalNstoreNH3", "NH3-N emitted", "kg", totalNstoreNH3, parens);
        GlobalVars.Instance.writeInformationToFiles("totalNstoreN2", "N2-N emitted", "kg", totalNstoreN2, parens);
        GlobalVars.Instance.writeInformationToFiles("totalNstoreN20", "N2O-N emitted", "kg", totalNstoreN20, parens);
        GlobalVars.Instance.writeInformationToFiles("NTANLost", "Total TAN lost", "kg", NTANLost, parens);
        GlobalVars.Instance.writeInformationToFiles("NDegOrgOut", "Degradable N ex storage", "kg", NDegOrgOut, parens);
        GlobalVars.Instance.writeInformationToFiles("newNHUM", "new Humic N created in manure storage", "kg", newNHUM, parens);
        GlobalVars.Instance.writeInformationToFiles("NlabileOrgOutStore", "Labile organic N ex storage", "kg", NlabileOrgOutStore, parens);
        GlobalVars.Instance.writeInformationToFiles("NhumicOutstore", "Humic organic N ex storage", "kg", NhumicOutstore, parens);
        GlobalVars.Instance.writeInformationToFiles("NRunoffHum", "Humic N in runoff", "kg", NRunoffHum, parens);
        GlobalVars.Instance.writeInformationToFiles("NrunoffTan", "TAN in runoff", "kg", NrunoffTan, parens);
        GlobalVars.Instance.writeInformationToFiles("NRunOffLabileOrg", "Labile organic N in runoff", "kg", NRunOffLabileOrg, parens);
        GlobalVars.Instance.writeInformationToFiles("NLost", "Total N lost", "kg", NLost, parens);
        //GlobalVars.Instance.writeInformationToFiles("Nout", "Total N ", "kg", Nout);
        //GlobalVars.Instance.writeInformationToFiles("Nbalance", "??", "??", Nbalance);
        
        theManure.Write("");
      //  if (writeEndTab)
            GlobalVars.Instance.writeEndTab();
    }
    //! A normal member. Check ManureStoreC Balance. Returning one value.
    /*!
     \return a boolean value.
    */
    public bool CheckManureStoreCBalance()
    {
        bool retVal = false;
        double Cout = GetManureC() + biogasCO2C + biogasCH4C;
        double CLost = CCH4ST + CCO2ST + CRunOffOrg;
        double Cbalance = Cinput - (Cout + CLost);
        double diff = Cbalance / Cinput;
        double tolerance = GlobalVars.Instance.getmaxToleratedErrorYield();
        if (Math.Abs(diff) > tolerance)
        {
                double errorPercent = 100 * diff;
                string message1 = "Error; Manure storage C balance error for " + name + " is more than the permitted margin\n";
                string message2 =message1+ "Percentage error = " + errorPercent.ToString("0.00") + "%";
                GlobalVars.Instance.Error(message2);
             
        }
        return retVal;
    }
    //! A normal member. Check ManureStoreN Balance. Returning one value.
    /*!
     \return a boolean value.
    */
    public bool CheckManureStoreNBalance()
    {
        bool retVal = false;
        Ninput = NTanInstore + NlabileOrgInstore + NhumicInstore;
        Nout = GetManureN();
        NLost = NRunOffLabileOrg + NRunoffHum + NrunoffTan+ totalNstoreN2+totalNstoreN20+totalNstoreNH3;
        Nbalance = Ninput - (Nout + NLost);
        double diff = Nbalance / Ninput;
        double tolerance = GlobalVars.Instance.getmaxToleratedErrorYield();
        if (Math.Abs(diff) > tolerance)
        {
            Write();
            GlobalVars.Instance.CloseOutputXML();

                double errorPercent = 100 * diff;
       
                string messageString= ("Error; Manure storage N balance error for " + name + " is more than the permitted margin\n");
                messageString = messageString+("Percentage error = " + errorPercent.ToString("0.00") + "%");
        
                GlobalVars.Instance.Error(messageString);
          
        }
        return retVal;
    }
}