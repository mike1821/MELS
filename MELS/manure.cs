using System;
using System.Xml;
/*! A class that named manure */
public class manure
{
    string path;
    int id;
    double DM;
    double nonDegDM;
    double degDM;
    double nonDegC;
    double orgDegC;
    double degC;
    double humicC;
    double labileOrganicN;
    double TAN;
    double humicN;
    double biocharC;
    double Bo;
    int manureType;
    int speciesGroup;
    string name;
    bool isSolid;
    string parens;
    //! A normal member, Set degC. Taking one argument.
    /*!
     \param aValue, one double argument.
    */
    public void SetdegC(double aValue) { degC = aValue; }
    //! A normal member, Set nonDegC. Taking one argument.
    /*!
     \param aValue, one double argument.
    */
    public void SetnonDegC(double aValue) { nonDegC = aValue; }
    //! A normal member, Set thumicC. Taking one argument.
    /*!
     \param aValue, one double argument.
    */
    public void SethumicC(double aValue) { humicC = aValue; }
    //! A normal member, Set TAN. Taking one argument.
    /*!
     \param aValue, one double argument.
    */
    public void SetTAN(double aValue) { TAN = aValue; }
    //! A normal member, Set labile OrganicN. Taking one argument.
    /*!
     \param aValue, one double argument.
    */
    public void SetlabileOrganicN(double aValue) { labileOrganicN = aValue; }
    //! A normal member, Set DM. Taking one argument.
    /*!
     \param aVal, one double argument.
    */
    public void SetDM(double aVal) { DM = aVal; }
    //! A normal member, Set speciesGroup. Taking one argument.
    /*!
     \param aValue, one integer argument.
    */
    public void SetspeciesGroup(int aValue) { speciesGroup = aValue; }
    //! A normal member, Set manureType. Taking one argument.
    /*!
     \param aValue, one integer argument.
    */
    public void SetmanureType(int aValue) { manureType = aValue; }
    //! A normal member, Set isSolid. Taking one argument.
    /*!
     \param aValue, one boolean argument.
    */
    public void SetisSolid(bool aValue) { isSolid = aValue; }
    //! A normal member, Set thumicN. Taking one argument.
    /*!
     \param aVal, one double argument.
    */
    public void SethumicN(double aVal) { humicN = aVal; }
    //! A normal member, Set Bo. Taking one argument.
    /*!
     \param aVal, one double argument.
    */
    public void SetBo(double aVal) { Bo = aVal; }
    //! A normal member, Set Name. Taking one argument.
    /*!
     \param aname, one string argument.
    */
    public void Setname(string aname) { name = aname; }
    //! A normal member, Get DM. returning one value.
    /*!
     \return a double value.
    */
    public double GetDM() { return DM; }
    //! A normal member, Get nonDegDM. returning one value.
    /*!
     \return a double value.
    */
    public double GetnonDegDM() { return nonDegDM; }
    //! A normal member, Get degDM. returning one value.
    /*!
     \return a double value.
    */
    public double GetdegDM() { return degDM; }
    //! A normal member, Get nonDegC. returning one value.
    /*!
     \return a double value.
    */
    public double GetnonDegC() { return nonDegC; }
    //! A normal member, Get thumicC. returning one value.
    /*!
     \return a double value.
    */
    public double GethumicC() { return humicC; }
    public double GetBiocharC() { return biocharC; }
    //! A normal member, Get degC. returning one value.
    /*!
     \return a double value.
    */
    public double GetdegC() { return degC; }
    //! A normal member, Get Orog DegC returning one value.
    /*!
     \return a double value.
    */
    public double GetOrgDegC() { return orgDegC; }
    //! A normal member, Get TAN. returning one value.
    /*!
     \return a double value.
    */
    public double GetTAN() { return TAN; }
    //! A normal member, Get organicN. returning one value.
    /*!
     \return a double value.
    */
    public double GetorganicN() { return labileOrganicN + humicN; }
    //! A normal member, Get manureType. returning one value.
    /*!
     \return an integer value.
    */
    public int GetmanureType() { return manureType; }
    //! A normal member, Get speciesGroup. returning one value.
    /*!
     \return an integer value.
    */
    public int GetspeciesGroup() { return speciesGroup; }
    //! A normal member, Get isSolid. returning one value.
    /*!
     \return a boolean value.
    */
    public bool GetisSolid() { return isSolid; }
    //! A normal member, Get Name. returning one value.
    /*!
     \return a string value.
    */
    public string Getname() { return name; }
    //! A normal member, Get thumicN. returning one value.
    /*!
     \return a double value.
    */
    public double GethumicN() { return humicN; }
    //! A normal member, Get labile OrganicN. returning one value.
    /*!
     \return a double value.
    */
    public double GetlabileOrganicN() { return labileOrganicN; }
    //! A normal member, Get TotalN. returning one value.
    /*!
     \return a double value.
    */
    public double GetTotalN() { return TAN + labileOrganicN + humicN; }
    //! A normal member, Get Bo. returning one value.
    /*!
     \return a double value.
    */
    public double GetBo() { return Bo; }
    //! A normal member, isSame. Taking one argument and returning one value.
    /*!
     \param aManure, one manure class instance.
     \return a boolean value.
    */
    public bool isSame(manure aManure)
    {
        if (manureType == aManure.manureType)
        {
            if ((speciesGroup == aManure.speciesGroup) || (aManure.speciesGroup == 0))
                return true;
            else
                return false;
        }
        else
        {
            if ((speciesGroup == aManure.speciesGroup) || (aManure.speciesGroup == 0))
            {
                if (manureType == 1 && aManure.manureType == 2)
                    return true;
                else if (manureType == 2 && aManure.manureType == 1)
                    return true;
                else if (manureType == 3 && aManure.manureType == 4)
                    return true;
                else if (manureType == 4 && aManure.manureType == 3)
                    return true;
                else if (manureType == 6 && aManure.manureType == 9)
                    return true;
                else if (manureType == 9 && aManure.manureType == 6)
                    return true;
                else if (manureType == 7 && aManure.manureType == 10)
                    return true;
                else if (manureType == 10 && aManure.manureType == 7)
                    return true;
                else if (manureType == 8 && aManure.manureType == 12)
                    return true;
                else if (manureType == 12 && aManure.manureType == 8)
                    return true;
                else if (manureType == 13 && aManure.manureType == 14)
                    return true;
                else if (manureType == 14 && aManure.manureType == 13)
                    return true;
                else
                    return false;
            }
            return false;
        }
           
    }
    //! A constructor.
    /*!
     without argument.
    */
    public manure()
    {
        DM =0;
        nonDegDM =0;
        degDM = 0;
        nonDegC = 0;
        degC = 0;
        humicC = 0;
        biocharC = 0;
        labileOrganicN = 0;
        TAN = 0;
        humicN = 0;
        manureType = 0;
        speciesGroup = 0;
        Bo = 0;
        name = "";
    }
    //! A constructor with one argument.
    /*!
     \param manureToCopy, one manure class instance.
    */
    public manure(manure manureToCopy)
    {
        DM = manureToCopy.DM;
        nonDegDM = manureToCopy.nonDegDM;
        degDM = manureToCopy.degDM;
        nonDegC = manureToCopy.nonDegC;
        degC = manureToCopy.degC;
        humicC = manureToCopy.humicC;
        biocharC = manureToCopy.biocharC;
        labileOrganicN = manureToCopy.labileOrganicN;
        TAN = manureToCopy.TAN;
        humicN = manureToCopy.humicN;
        Bo = manureToCopy.Bo;
        manureType = manureToCopy.manureType;
        speciesGroup = manureToCopy.speciesGroup;
        name = manureToCopy.name;
    }

    //! A constructor with four arguments. create new instance of manure, with amount determined by N required
    /*!
     \param aPath, one string argument.
     \param aID, one integer argument.
     \param amountN, one double argument.
     \param aParens, one string argument.
    */
    public manure(string aPath, int aID, double amountN, string aParens)
    {
        id=aID;
        path="AgroecologicalZone("+GlobalVars.Instance.GetZone().ToString()+")."+aPath+'('+id.ToString()+')';
        parens = aParens;
        FileInformation manureFile = new FileInformation(GlobalVars.Instance.getfertManFilePath());
        manureFile.setPath(path);
        name = manureFile.getItemString("Name");
        manureType = manureFile.getItemInt("ManureType");
        speciesGroup = manureFile.getItemInt("SpeciesGroup");
        path += ".TANconcentration(-1)";
        manureFile.setPath(path);
        double tempTAN = manureFile.getItemDouble("Value");
        manureFile.PathNames[2] = "organicNconcentration";
        double temporganicN = manureFile.getItemDouble("Value");
        double proportionTAN = tempTAN / (tempTAN + temporganicN);
        TAN = proportionTAN * amountN;
        labileOrganicN = (1 - proportionTAN) * amountN;
        double amount = amountN/(tempTAN+ temporganicN);
        manureFile.PathNames[2] = "degCconcentration";
        double degCconc=manureFile.getItemDouble("Value");
        degC = amount * degCconc;
        manureFile.PathNames[2] = "nonDegCconcentration";
        double nonDegCconc = manureFile.getItemDouble("Value");
        nonDegC = amount * nonDegCconc;
        manureFile.PathNames[2] = "humicCconcentration";
        double humicCconc = manureFile.getItemDouble("Value");
        manureFile.PathNames[2] = "biocharConcentration";
        double biocharCconc = manureFile.getItemDouble("Value",false);
        biocharC = 0.0;
        if (humicCconc>nonDegCconc)
        {
            string messageString = "Error; manure humic C is greater than total non-degradable C\n";
            messageString += "Manure name = " + name + "\n";
            GlobalVars.Instance.Error(messageString);
        }
        humicC = amount * humicCconc;
        if (humicC > 0)
        {
            humicN = humicC / GlobalVars.Instance.getCNhum();
            labileOrganicN -= humicN;
            if (labileOrganicN < 0)
            {
                string messageString = "Error; manure humic N is greater than total organic N\n";
                messageString += "Manure name = " + name + "\n";
                GlobalVars.Instance.Error(messageString);
            }
        }
        else
            humicN = 0;
        if (biocharCconc > 0.0)
        {
            biocharC = amount * biocharCconc;
        }
        manureFile.PathNames[2] = "Bo";
        Bo = manureFile.getItemDouble("Value",false);
    }
    //! A normal member. Get TotalC.
    /*!
     \return a double value.
    */
    public double GetTotalC()
    {
        return degC + nonDegC + humicC;
    }
    //! A normal member. 
    /*!
     Initialise method.
    */
    public void Initialise()
    {
        DM = 0;
        nonDegC = 0;
        nonDegDM = 0;
        degC = 0;
        humicC = 0;
        Bo = 0;
    }
    //! A normal member. Add Manure. Taking one argument.
    /*!
     \param aManure, one manure class instance argument.
    */
    public void AddManure(manure aManure)
    {
        double totalC = nonDegC + degC;
        double oldBo = Bo * totalC;
        double donorC = aManure.degC + aManure.nonDegC;
        double addedBo = aManure.Bo * donorC;
        Bo = (oldBo + addedBo) / (totalC + donorC);
        DM += aManure.DM;
        nonDegDM += aManure.nonDegDM;
        degDM += aManure.degDM;
        nonDegC += aManure.nonDegC;
        degC += aManure.degC;
        humicC += aManure.humicC;
        biocharC += aManure.biocharC;
        labileOrganicN += aManure.labileOrganicN;
        TAN += aManure.TAN;
        humicN += aManure.humicN;
    }
    //! A normal member. Increase Manure. Taking one argument.
    /*!
     \param factor, one double argument.
    */
    public void IncreaseManure(double factor)
    {
        DM*= factor;
        nonDegDM *= factor;
        degDM *= factor;
        nonDegC *= factor;
        degC *= factor;
        humicC *= factor;
        labileOrganicN *= factor;
        TAN *= factor;
        humicN *= factor;
    }
    //! A normal member. Divide Manure. Taking one argument.
    /*!
     \param proportion, one double argument.
    */
    public void DivideManure(double proportion)
    {
        DM *=proportion;
        nonDegDM *= proportion;
        degDM *= proportion;
        nonDegC *= proportion;
        degC *= proportion;
        humicC *= proportion;
        labileOrganicN *= proportion;
        TAN *= proportion;
        humicN *= proportion;
    }
    //! A normal member. Take Manure. Taking two arguments.
    /*!
     \param amountN, one double argument.
     \param aManure, one manure class instance argument.
    */
    public void TakeManure(ref double amountN, ref manure aManure)
    {
        double totalN = GetTotalN();
        double proportion;
        if (amountN <= totalN)
            proportion = amountN / totalN;
        else
        {
            proportion=1.0;
            amountN = totalN;
        }
        aManure.DM=proportion*DM;
        DM -= aManure.DM;
            
        aManure.nonDegDM = proportion * nonDegDM;
        nonDegDM-=aManure.nonDegDM;
        aManure.degDM = proportion * degDM;
        degDM-=aManure.degDM;
        aManure.nonDegC = proportion * nonDegC;
        nonDegC-=aManure.nonDegC;
        aManure.degC = proportion * degC;
        degC-=aManure.degC;
        aManure.humicC = proportion * humicC;
        humicC-=aManure.humicC;
            
        aManure.labileOrganicN = proportion * labileOrganicN;
        labileOrganicN -= aManure.labileOrganicN;
        aManure.TAN = proportion * TAN;
        TAN -= aManure.TAN;
        aManure.humicN = proportion * humicN;
        humicN -= aManure.humicN;
        aManure.Setname(Getname());
    }
    //! A normal member. Write. Taking one argument.
    /*!
     \param addedInfo, one string argument.
    */
    public void Write(string addedInfo)
    {
        parens = "_" + addedInfo + "_" + name;
        GlobalVars.Instance.writeStartTab("manure");
        GlobalVars.Instance.writeInformationToFiles("name", "Name", "-", name, parens);
        GlobalVars.Instance.writeInformationToFiles("speciesGroup", "Species number", "-", speciesGroup, parens);
        GlobalVars.Instance.writeInformationToFiles("typeStored", "Storage type", "-", manureType, parens);
        GlobalVars.Instance.writeInformationToFiles("DM", "Dry matter", "kg", DM, parens);
        GlobalVars.Instance.writeInformationToFiles("nonDegDM", "Non-degradable DM", "kg", nonDegDM, parens);
        GlobalVars.Instance.writeInformationToFiles("degDM", "Degradable DM", "kg", degDM, parens);
        GlobalVars.Instance.writeInformationToFiles("nonDegC", "Non-degradable C", "kg", nonDegC, parens);
        GlobalVars.Instance.writeInformationToFiles("degC", "Degradable C", "kg", degC, parens);
        GlobalVars.Instance.writeInformationToFiles("humicC", "Humic C", "kg", humicC, parens);
        GlobalVars.Instance.writeInformationToFiles("TAN", "TAN", "kg", TAN, parens);
        GlobalVars.Instance.writeInformationToFiles("labileOrganicN", "Organic N", "kg", labileOrganicN, parens);
        GlobalVars.Instance.writeInformationToFiles("humicN", "humic N", "kg", humicN, parens);
        GlobalVars.Instance.writeEndTab();        
    }
}
