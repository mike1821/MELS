using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Xml.XPath;
/*! A class that named feedItem */
public class feedItem
{
    double amount;
    string name;
    int feedCode;
    int ID;
    string path;
    double energy_conc;
    double ash_conc;
    double C_conc;
    double N_conc;
    double NDF_conc;
    double fibre_conc;
    double fat_conc;
    double nitrate_conc;
    double DMdigestibility;
    bool isGrazed;
    bool fedAtPasture;
    bool beddingMaterial;
    double StoreProcessFactor;
    //! A normal member, Set feedCode. Taking one argument.
    /*!
      \param aFeedCode an integer value for feedCode.
    */
    public void setFeedCode(int aFeedCode) { feedCode = aFeedCode; }
    //! A normal member, Get feedCode. Returning an integer value.
    /*!
      \return an integer value for feedCode.
    */
    public int GetFeedCode(){return feedCode;}
    //! A normal member, Set amount. Taking one argument.
    /*!
      \param anamount a double value for amount.
    */
    public void Setamount(double anamount) { amount = anamount; }
    //! A normal member, Set C_conc. Taking one argument.
    /*!
      \param aVal a double value for C_conc.
    */
    public void SetC_conc(double aVal) { C_conc = aVal; }
    //! A normal member, Set N_conc. Taking one argument.
    /*!
      \param aVal a double value for N_conc.
    */
    public void SetN_conc(double aVal) { N_conc = aVal; }
    //! A normal member, Set fibre_conc. Taking one argument.
    /*!
      \param aVal a double value for fibre_conc.
    */
    public void Setfibre_conc(double aVal) { fibre_conc = aVal; }
    //! A normal member, Set NDF_conc. Taking one argument.
    /*!
    \param aVal a double value for NDF_conc.
  */
    public void SetNDF_conc(double aVal) { NDF_conc = aVal; }
    //! A normal member, Set DMdigestibility. Taking one argument.
    /*!
    \param aVal a double value for DMdigestibility.
   */
    public void SetDMdigestibility(double aVal) { DMdigestibility = aVal; }
    //! A normal member, Set ash_conc. Taking one argument.
    /*!
    \param aVal a double value for ash_conc.
  */
    public void Setash_conc(double aVal) { ash_conc = aVal; }
    //! A normal member, Set nitrate_conc. Taking one argument.
    /*!
    \param aVal a double value for nitrate_conc.
  */
    public void Setnitrate_conc(double aVal) { nitrate_conc = aVal; }
    //! A normal member, Set energy_conc. Taking one argument.
    /*!
    \param aVal a double value for energy_conc.
  */
    public void Setenergy_conc(double aVal) { energy_conc = aVal; }
    //! A normal member, Set name. Taking one argument.
    /*!
      \param aString a string value for name.
    */
    public void Setname(string aString) { name = aString; }
    //! A normal member, Set isGrazed. Taking one argument.
    /*!
      \param aVal a bool value for isGrazed.
    */
    public void SetisGrazed(bool aVal) { isGrazed = aVal; }
    //! A normal member, Set fedAtPasture. Taking one argument.
    /*!
      \param aVal a bool value for fedAtPasture.
    */
    public void SetfedAtPasture(bool aVal) { fedAtPasture = aVal; }
    //! A normal member, Get amount. Returning a double value.
    /*!
      \return a double value for amount.
    */
    public double Getamount() { return amount; }
    //! A normal member, Get name. Returning a string value.
    /*!
      \return a string value for name.
    */
    public string GetName() { return name; }
    //! A normal member, Get energy_conc. Returning a double value.
    /*!
      \return a double value for energy_conc.
    */
    public double Getenergy_conc() { return energy_conc; }
    //! A normal member, Get ash_conc. Returning a double value.
    /*!
      \return a double value for ash_conc.
    */
    public double Getash_conc() { return ash_conc; }
    //! A normal member, Get C_conc. Returning a double value.
    /*!
      \return a double value for C_conc.
    */
    public double GetC_conc() { return C_conc; }
    //! A normal member, Get N_conc. Returning a double value.
    /*!
      \return a double value for N_conc.
    */
    public double GetN_conc() { return N_conc; }
    //! A normal member, Get Nitrate_conc. Returning a double value.
    /*!
      \return a double value for Nitrate_conc.
    */
    public double GetNitrate_conc() { return nitrate_conc; }
    //! A normal member, Get NDF_conc. Returning a double value.
    /*!
      \return a double value for NDF_conc.
    */
    public double GetNDF_conc() { return NDF_conc; }
    //! A normal member, Get fibre_conc. Returning a double value.
    /*!
      \return a double value for fibre_conc.
    */
    public double Getfibre_conc() { return fibre_conc; }
    //! A normal member, Get fat_conc. Returning a double value.
    /*!
      \return a double value for fat_conc.
    */
    public double Getfat_conc() { return fat_conc; }
    //! A normal member, Get DMdigestibility. Returning a double value.
    /*!
      \return a double value for DMdigestibility.
    */
    public double GetDMdigestibility() { return DMdigestibility; }
    //! A normal member, Get StoreProcessFactor. Returning a double value.
    /*!
      \return a double value for StoreProcessFactor.
    */
    public double GetStoreProcessFactor() { return StoreProcessFactor; }
    //! A normal member, Get isGrazed. Returning a bool value.
    /*!
      \return a bool value for isGrazed.
    */
    public bool GetisGrazed() { return isGrazed; }
    //! A normal member, Get fedAtPasture. Returning a bool value.
    /*!
      \return a bool value for fedAtPasture.
    */
    public bool GetfedAtPasture() { return fedAtPasture; }
    //! A normal member, Get beddingMaterial. Returning a bool value.
    /*!
      \return a bool value for beddingMaterial.
    */
    public bool GetbeddingMaterial() { return beddingMaterial; }
    //! A normal member, Add amount. Taking one argument.
    /*!
      \param aVal a double value for amount.
    */
    public void Addamount(double aVal){amount += aVal;}
    private string Unit;
    string parens;
    //! A constructor without argument.
    /*!
      \more details.
    */
    public feedItem()
    {
        name = "None";
        path = "None";
        amount=0;
        feedCode=0;
        energy_conc=0;
        ash_conc=0;
        C_conc=0;
        N_conc=0;
        NDF_conc=0;
        fibre_conc=0;
        fat_conc=0;
        nitrate_conc = 0;
        DMdigestibility=0;
        isGrazed=false;
        beddingMaterial=false;
        StoreProcessFactor=0;
        Unit = "kg/day";
    }
    //! A constructor with four arguments.
    /*!
      \param feeditemPath, a string argument that points to a path.
      \param id, an integer argument
      \param getamount, a bool argument
      \param aParens, a string argument
    */
    public feedItem(string feeditemPath, int id, bool getamount, string aParens)
    {
        parens = aParens;
        ID = id;
        path = feeditemPath + '(' + id + ')';
        FileInformation feedFile = new FileInformation(GlobalVars.Instance.getFarmFilePath());
        feedFile.setPath(path);
        if (getamount == true)
        {
            amount = feedFile.getItemDouble("Amount");
            Unit = feedFile.getItemString("Unit");
        }
        name = feedFile.getItemString("Name");
        feedCode = feedFile.getItemInt("FeedCode");
        if ((feedCode > 1000) && (feedCode < 2000))
        {
            feedCode -= 1000;
        }
        string aString = feedFile.getItemString("Grazed");
        if (aString == "true")
            isGrazed = true;
        else
            isGrazed = false;

        //need a more elegant method of detecting incorporation
        bool checkIncorp = name.Contains("incorporated");
        if (name.Contains("incorporated"))
            feedCode-=2000;
        GetStandardFeedItem(feedCode);
        if (checkIncorp)//get standard feeditem has renamed the product, so add incorporated again, so it will be recognised later
            name += " incorporated";
    }

    //! A constructor with one argument.
    /*!
      \param afeedItem, a class instance that points to class feedItem.
    */
    public feedItem(feedItem afeedItem)
        {
            energy_conc = afeedItem.energy_conc ;
            ash_conc = afeedItem.ash_conc;
            C_conc = afeedItem.C_conc;
            N_conc = afeedItem.N_conc;
            NDF_conc = afeedItem.NDF_conc;
            fibre_conc = afeedItem.fibre_conc;
            fat_conc = afeedItem.fat_conc;
            DMdigestibility = afeedItem.DMdigestibility;
            amount = afeedItem.amount;
            name = afeedItem.name;
            isGrazed = afeedItem.isGrazed;
            feedCode = afeedItem.feedCode;
            StoreProcessFactor = afeedItem.StoreProcessFactor;
            fedAtPasture = afeedItem.fedAtPasture;
        }
    //! A normal member, Get StandardFeedItem. Taking one argument.
    /*!
      \param targetFeedCode, an integer value.
    */
    public void GetStandardFeedItem(int targetFeedCode)
    {
        DateTime start = DateTime.Now;
       
        FileInformation file=new FileInformation(GlobalVars.Instance.getfeeditemFilePath());
        file.setPath("feedItem");
//        file.setPath("AgroecologicalZone(" + GlobalVars.Instance.GetZone() + ").feedItem");
        int min = 99; int max = 0;
        file.getSectionNumber(ref min, ref max);

        bool found = false;
        for(int i=min;i<=max;i++)
        {
//            file.setPath("AgroecologicalZone(" + GlobalVars.Instance.GetZone() + ").feedItem");
            file.setPath("feedItem");
            if (file.doesIDExist(i))
            {
                string coreString = "feedItem(" + i.ToString() + ")";
                file.setPath(coreString);
                int StandardFeedCode = file.getItemInt("FeedCode");
                if (StandardFeedCode == targetFeedCode)
                {
                    found = true;
                    feedCode = targetFeedCode;
                    name = file.getItemString("Name");
                    file.setPath(coreString+".Fibre_concentration(-1)");
                    fibre_conc = file.getItemDouble("Value");
                    file.setPath(coreString+".NDF_concentration(-1)");
                    NDF_conc = file.getItemDouble("Value");
                    if (NDF_conc==-1)
                    {
                        string messageString = ("could not find NDF for feeditem ");
                        messageString += feedCode.ToString() + " name = " + name + "\n";
                        GlobalVars.Instance.Error(messageString);
                    }
                    file.setPath(coreString+".CrudeProtein_concentration(-1)");
                    double CrudeProtein_concentration = file.getItemDouble("Value");
                    SetN_conc(CrudeProtein_concentration / GlobalVars.NtoCrudeProtein);
                    file.setPath(coreString+".Fat_concentration(-1)");
                    fat_conc = file.getItemDouble("Value");
       
                    file.setPath(coreString+".Energy_concentration(-1)");
                    energy_conc = file.getItemDouble("Value"); 
                    energy_conc *= GlobalVars.Instance.GetdigestEnergyToME();

                    file.setPath(coreString+".Ash_concentration(-1)");
                    ash_conc = file.getItemDouble("Value");
                    SetC_conc((1.0 - Getash_conc()) * 0.46);
                    if (energy_conc == 0)
                        SetC_conc(0);
                    file.setPath(coreString+".Nitrate_concentration(-1)");
                    nitrate_conc = file.getItemDouble("Value");
     
                    file.setPath(coreString+".DMDigestibility(-1)");
                    DMdigestibility = file.getItemDouble("Value");
     
                    file.setPath(coreString+".Bedding_material(-1)");
                    beddingMaterial =file.getItemBool("Value");

                    file.setPath(coreString+".processStorageLoss(-1)");
                    StoreProcessFactor = file.getItemDouble("Value");
    /*                fibre_conc = (0.264 * 100*fibre_conc - 3.75)/100;
                    if (fibre_conc < 0)
                        fibre_conc = 0;*/
                    break;
                }
            }
        }
    
        if (found == false)
        {
            string messageString=("could not find feeditem ");
            messageString+=feedCode.ToString() + " name = " + name +  "\n";
            GlobalVars.Instance.Error(messageString);
        }
 
        TimeSpan timeItTook = DateTime.Now - start;
    //! A normal member, Add FeedItem. Taking three arguments and returing one integer value.
    /*!
      \param afeedItem, an instance that points to class feedItem.
      \param pooling, a bool value
      \param isBedding, a bool value with default value false
      \return an integer value
    */
}
    public int AddFeedItem(feedItem afeedItem, bool pooling, bool isBedding=false)
    {
        if ((afeedItem.GetFeedCode() != feedCode) && (pooling != true))
        {
              string messageString=("Error; attempt to combine two different feed items");
               GlobalVars.Instance.Error(messageString);
        }
        double donorAmount = afeedItem.Getamount();
        name = afeedItem.GetName();
        feedCode = afeedItem.GetFeedCode();
        if (donorAmount != 0)
        {
            energy_conc = (energy_conc * amount + donorAmount * afeedItem.Getenergy_conc()) / (amount + donorAmount);
            ash_conc = (ash_conc * amount + donorAmount * afeedItem.Getash_conc()) / (amount + donorAmount);
            C_conc = (C_conc * amount + donorAmount * afeedItem.GetC_conc()) / (amount + donorAmount);
            N_conc = (N_conc * amount + donorAmount * afeedItem.GetN_conc()) / (amount + donorAmount);
            NDF_conc = (NDF_conc * amount + donorAmount * afeedItem.GetNDF_conc()) / (amount + donorAmount);
            fibre_conc = (fibre_conc * amount + donorAmount * afeedItem.Getfibre_conc()) / (amount + donorAmount);
            fat_conc = (fat_conc * amount + donorAmount * afeedItem.Getfat_conc()) / (amount + donorAmount);
            DMdigestibility = (DMdigestibility * amount + donorAmount * afeedItem.GetDMdigestibility()) / (amount + donorAmount);
            amount += donorAmount;
            if (isBedding)
                beddingMaterial = true;
        }
        return 0;
    }
    //Jonas - this is dangerous code. The feedItem concentrations could go negative. I have no immediate solution
    //! A normal member, Substract FeedItem. Taking two arguments and returing one integer value.
    /*!
      \param afeedItem, an instance that points to class feedItem.
      \param pooling, a bool value
      \return an integer value
    */
    public int SubtractFeedItem(feedItem afeedItem, bool pooling)
    {
        if ((afeedItem.GetFeedCode() != feedCode) && (pooling != true))
        {
            string messageString=("Error; attempt to subtract two different feed items");
            GlobalVars.Instance.Error(messageString);
        }
        double donorAmount = afeedItem.Getamount();
        amount -= donorAmount;
        return 0;
    }
    //! A normal member, initialize.
    /*!
      more details.
    */

    public void initialize()
    {
        amount = 0;
        isGrazed = false;
        beddingMaterial = false;
    }
    //! A normal member, Write. Taking one argument.
    /*!
      \param theParens, a string value.
    */
    public void Write(string theParens)
    {
        parens = theParens + "feedCode" + feedCode.ToString();
        GlobalVars.Instance.writeStartTab("FeedItem");
        GlobalVars.Instance.writeInformationToFiles("name", "Name", "-", name,parens);
        GlobalVars.Instance.writeInformationToFiles("amount", "Amount", "kg DM", amount, parens);
        GlobalVars.Instance.writeInformationToFiles("feedCode", "Feed code", "-", feedCode, parens);
        GlobalVars.Instance.writeInformationToFiles("ash_conc", "Ash concentration", "kg/kg DM", ash_conc, parens);
        GlobalVars.Instance.writeInformationToFiles("C_conc", "C concentration", "kg/kg DM", C_conc, parens);
        GlobalVars.Instance.writeInformationToFiles("N_conc", "N concentration", "kg/kg DM", N_conc, parens);
        GlobalVars.Instance.writeInformationToFiles("NDF_conc", "NDF concentration", "kg/kg DM", NDF_conc, parens);
        GlobalVars.Instance.writeInformationToFiles("fibre_conc", "Fibre concentration", "kg/kg DM", fibre_conc, parens);
        GlobalVars.Instance.writeInformationToFiles("fat_conc", "Fat concentration", "kg/kg DM", fat_conc, parens);
        GlobalVars.Instance.writeInformationToFiles("energy_conc", "Energy concentration", "ME/kg DM", energy_conc, parens);
        GlobalVars.Instance.writeInformationToFiles("DMdigestibility", "DM digestibility", "kg/kg DM", DMdigestibility, parens);
        GlobalVars.Instance.writeInformationToFiles("isGrazed", "Fed at pasture", "-", isGrazed, parens);
        GlobalVars.Instance.writeInformationToFiles("beddingMaterial", "Can be used for bedding", "-", beddingMaterial, parens);
        GlobalVars.Instance.writeEndTab();

        
    }
    //! A normal member, Adjust amount. Taking one argument.
    /*!
      \param factor, a double value.
    */
    public void AdjustAmount(double factor)
    {
        amount *= factor;
    }
    //! A normal member, Get Bo. Returning one double value.
    /*!
      \return a double value.
    */
    public double GetBo()
    {
        double NDF = (144.5 - 1.54 * DMdigestibility * 100)/10; //NDF as percentage of DM
        double CelAndHemi = NDF - fibre_conc * 100;
        if (CelAndHemi < 0)
            CelAndHemi = 0;
        double Bo = 19.05 * N_conc * 6.25 * 100 + 27.73 * fat_conc * 100 + 1.75 * CelAndHemi;
        Bo /= 1000;
        return Bo;
    }
    //! A normal member, Get Bo. Returning one bool value.
    /*!
      \return a bool value.
    */
    public bool isConcentrate()
    {
        bool ret_val = false;
        if ((GetFeedCode() < 348) || ((GetFeedCode() >= 908) && GetFeedCode() <= 944))
            ret_val = true;
        return ret_val;
    }
}

