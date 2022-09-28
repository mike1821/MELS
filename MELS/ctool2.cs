using System;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Generic;
/*! A class that named ctool2 */
/*!
  A more elaborate class description.
*/
public class ctool2
{
    
   /// Parameters to output
     double Th_diff = 0; 
    /// Parameters to output
    double FOMdecompositionrate = 0; 
/// Parameters to output
    double HUMdecompositionrate = 0; 
    double Clayfraction = 0; /// Parameters to output
    double tF = 0;
     double ROMificationfraction = 0;
     double ROMdecompositionrate = 0;
     double CtoNHUM = 10.0;
     double CtoNROM = 10.0;
    

    //! Other variables to output
    int numberOfLayers = 0;

     
    /// fomc = fresh organic matter C, kg/ha
    public double[] fomc; 
/// humc = humic organic matter C, kg/ha
    public double[] humc;   
/// romc = resistant organic matter C, kg/ha
    public double[] romc;   
    public double[] biocharc;
    /// FOMn = N in fresh organic matter, kg N/ha
    public double FOMn = 0;

    double maxSoilDepth = 0;
    double timeStep = 1/365.25;
    double fCO2 = 0;
    double totalCO2Emission = 0;
    double CInput = 0;
    double FOMNInput = 0;
    double HUMNInput = 0;
    double Nlost = 0;
    double offset = 0;
    double amplitude = 0;
    double dampingDepth = 0;
    string parens;
    private bool pauseBeforeExit = false;

    double FOMcInput = 0;
    double FOMcCO2 = 0;
    double FOMcToHUM = 0;
    double HUMcInput = 0;
    double HUMcCO2 = 0;
    double HUMcToROM = 0;
    double ROMcInput = 0;
    double ROMcCO2 = 0;
    double BiocharcCO2 = 0;
    double BiocharCInput = 0;
    double BiocharcInput = 0;
    double BiocharCCO2 = 0;

    //! A destructor.
    /*!
      A more elaborate description of the destructor.
    */
    ~ctool2()
    {
        
    }
    //! A constructor with one argument.
    /*!
      \param aParens, a string argument that points to parens.
    */
    public ctool2(string aParens)
    {
        parens = aParens;
    }
    //! A normal member, Get Clayfraction. Returning a double value.
    /*!
      \return a double value for Clayfraction.
    */
    public double GetClayfraction() { return Clayfraction; }
    //! A normal member, Get FOMn. Returning a double value.
    /*!
      \return a double value for FOMn.
    */
    public double GetFOMn() { return FOMn; }
    //! A normal member, Set Clayfraction. Taking one argument.
    /*!
      \param aVal, one bool value that points to pauseBeforeExit.
    */
    public void SetpauseBeforeExit(bool aVal) { pauseBeforeExit = aVal; }
    //! A normal member, Get CN. Taking one argument and Returning a double value.
    /*!
     \param cn, one double value.
      \return a double value for minimum CN.
    */
    double CN(double cn)
    {

        return Math.Min(56.2 * Math.Pow(cn, -1.69), 1);
    }
    //! A normal member, Get FOM. Returning a double value.
    /*!
      \return a double value for FOM.
    */
    public double GetFOM()
    {
        double retVal = 0;
        for (int i = 0; i < numberOfLayers; i++)
            retVal += fomc[i];
        return retVal;
    }
    //! A normal member, Get HUM. Returning a double value.
    /*!
      \return a double value for HUM.
    */
    public double GetHUM()
    {
        double retVal = 0;
        for (int i = 0; i < numberOfLayers; i++)
            retVal += humc[i];
        return retVal;
    }
    //! A normal member, Get ROM. Returning a double value.
    /*!
      \return a double value for ROM.
    */
    public double GetROM()
    {
        double retVal = 0;
        for (int i = 0; i < numberOfLayers; i++)
            retVal += romc[i];
        return retVal;
    }
    //! A normal member, Get BiChar. Returning a double value.
    /*!
      \return a double value for BiChar.
    */
    public double GetBichar()
    {
        double retVal = 0;
        for (int i = 0; i < numberOfLayers; i++)
            retVal += biocharc[i];
        return retVal;
    }
    //! A normal member, Get OrgC. Returning a double value.
    /*!
      \return a double value for OrgC.
    */
    public double GetOrgC(int layer)
    {
        double retVal = 0;
        retVal = fomc[layer] + humc[layer] + romc[layer] + biocharc[layer];
        return retVal;
    }
    //! A member taking 13 arguments 
    /*!
     \param soilTypeNo an integer argument,
     \param ClayFraction a double argument, 
     \param offsetIn a double argument, number of days to move. 
     \param amplitudeIn a double argument, array [month, layer] of fresh organic matter input (kg/ha).
     \param maxSoilDepthIn a double argument, array [month, layer] of humic organic matter input (kg/ha).
     \param dampingDepthIn a double argument, array [month] of N input in fresh organic matter (kg/ha).
     \param initialC a double argument, array [month] of depth of cultivation (m) (not used yet).
     \param parameterFileName a string argument, N leached from the soil in organic matter (kg)
     \param errorFileName a string argument,
     \param InitialCtoN a double argument,
     \param pHUMupperLayer a double argument,
     \param pHUMLowwerLayer a double argument, 
     \param residualMineralN      
     */
    public void Initialisation(int soilTypeNo, double ClayFraction, double offsetIn, double amplitudeIn, double maxSoilDepthIn, double dampingDepthIn,
        double initialC, string[] parameterFileName, string errorFileName, double InitialCtoN, double pHUMupperLayer, double pHUMLowerLayer,
        ref double residualMineralN)
    {
        amplitude = amplitudeIn;
        maxSoilDepth = maxSoilDepthIn;
        dampingDepth = dampingDepthIn;
        Th_diff = 0.35E-6;
        residualMineralN = 0;
   
        FileInformation ctoolInfo = new FileInformation(parameterFileName);
        ctoolInfo.setPath("constants(0).C-Tool(-1).timeStep(-1)");
        timeStep = ctoolInfo.getItemDouble("Value"); //one day pr year

        ctoolInfo.PathNames[ctoolInfo.PathNames.Count - 1] = "NumOfLayers";
        numberOfLayers = ctoolInfo.getItemInt("Value");

        ctoolInfo.PathNames[ctoolInfo.PathNames.Count - 1] = "FOMdecompositionrate";
        FOMdecompositionrate = ctoolInfo.getItemDouble("Value");
        ctoolInfo.PathNames[ctoolInfo.PathNames.Count - 1] = "HUMdecompositionrate";
        HUMdecompositionrate = ctoolInfo.getItemDouble("Value");
        this.Clayfraction = ClayFraction;
        ctoolInfo.PathNames[ctoolInfo.PathNames.Count - 1] = "transportCoefficient";
        tF = ctoolInfo.getItemDouble("Value");
        ctoolInfo.PathNames[ctoolInfo.PathNames.Count - 1] = "ROMdecompositionrate";
        ROMdecompositionrate = ctoolInfo.getItemDouble("Value");
        ctoolInfo.PathNames[ctoolInfo.PathNames.Count - 1] = "fCO2";
        fCO2 = ctoolInfo.getItemDouble("Value");
        ctoolInfo.PathNames[ctoolInfo.PathNames.Count - 1] = "ROMificationfraction";
        ROMificationfraction = ctoolInfo.getItemDouble("Value");
        fCO2 = 0.628;
        ROMificationfraction = 0.012; 

        fomc = new double[numberOfLayers];
        humc = new double[numberOfLayers];
        romc = new double[numberOfLayers];
        biocharc = new double[numberOfLayers];

        double CNfactor = CN(InitialCtoN);
        double fractionCtopsoil = 0.47;

        CtoNHUM = GlobalVars.Instance.getCNhum();
        CtoNROM = GlobalVars.Instance.getCNhum();

        int NonBaselinespinupYears=0;
        if (GlobalVars.Instance.reuseCtoolData != -1)
        {
            FileInformation file = new FileInformation(GlobalVars.Instance.getConstantFilePath());
            file.setPath("constants(0).spinupYearsNonBaseLine(-1)");
            NonBaselinespinupYears = file.getItemInt("Value");
        }
        if ((GlobalVars.Instance.reuseCtoolData != -1)&&(NonBaselinespinupYears==0))
        {
            Console.WriteLine("handover Data from "+ GlobalVars.Instance.getReadHandOverData());
            string[] lines=null;
            try
            {
                lines = System.IO.File.ReadAllLines(GlobalVars.Instance.getReadHandOverData());
            }
            catch
            {
                GlobalVars.Instance.Error("could not find CTool handover data " + GlobalVars.Instance.getReadHandOverData());
            }
            bool gotit=false;
            for (int j = 0; j < lines.Length; j++)
            {
                string[] data = lines[j].Split('\t');
                if (soilTypeNo == Convert.ToDouble(data[0]))
                {
                    fomc[0] = Convert.ToDouble(data[1]);
                    fomc[1] = Convert.ToDouble(data[2]);
                    humc[0] = Convert.ToDouble(data[3]);
                    humc[1] = Convert.ToDouble(data[4]);
                    romc[0] = Convert.ToDouble(data[5]);
                    romc[1] = Convert.ToDouble(data[6]);
                    biocharc[0] = Convert.ToDouble(data[7]);
                    biocharc[1] = Convert.ToDouble(data[8]);
                    FOMn = Convert.ToDouble(data[9]);
                    residualMineralN = Convert.ToDouble(data[10]);
                    gotit=true;
                }
            }
            if (!gotit)
                GlobalVars.Instance.Error("could not find soil carbon data for soil type " + soilTypeNo.ToString());
            // file.WriteLine(fomc[0].ToString() + '\t' + fomc[1].ToString() + '\t' + humc[0].ToString() + '\t' + humc[1].ToString() + '\t' + humc[0].ToString() + '\t' + humc[1].ToString() + '\t' + FOMn);
            //file.Close();
        }
        else
        {
            humc[0] = initialC * pHUMupperLayer * CNfactor * fractionCtopsoil;
            romc[0] = initialC * fractionCtopsoil - humc[0];
            humc[1] = initialC * pHUMLowerLayer * CNfactor * (1 - fractionCtopsoil);
            romc[1] = initialC * (1 - fractionCtopsoil) - humc[1];
            fomc[0] = initialC * 0.05;
            FOMn = fomc[0]/10.0;
            biocharc[0] = 0.0;
            biocharc[1] = 0.0;
        }
        //CInput = humc[0] + humc[1] + romc[0] + romc[1];
        CInput = initialC;
        HUMcInput = humc[0] + humc[1];
        ROMcInput = romc[0] + romc[1];
        BiocharcInput = biocharc[0] + biocharc[1];
    }
    //! A constructor with one argument.
    /*!
      \param C_ToolToCopy.
    */

    public ctool2(ctool2 C_ToolToCopy)
    {
        Th_diff = C_ToolToCopy.Th_diff;
        FOMdecompositionrate = C_ToolToCopy.FOMdecompositionrate;
        HUMdecompositionrate = C_ToolToCopy.HUMdecompositionrate;
        Clayfraction = C_ToolToCopy.Clayfraction;
        tF = C_ToolToCopy.tF;
        ROMificationfraction = C_ToolToCopy.ROMificationfraction;
        ROMdecompositionrate = C_ToolToCopy.ROMdecompositionrate;
        
        maxSoilDepth = C_ToolToCopy.maxSoilDepth;
        fCO2 = C_ToolToCopy.fCO2;
        CInput = C_ToolToCopy.CInput;
        FOMn = C_ToolToCopy.FOMn;
        CtoNHUM = C_ToolToCopy.CtoNHUM;
        CtoNROM = C_ToolToCopy.CtoNROM;
        FOMNInput = C_ToolToCopy.FOMNInput;
        HUMNInput = C_ToolToCopy.HUMNInput;
        Nlost = C_ToolToCopy.Nlost;
        offset = C_ToolToCopy.offset;
        amplitude = C_ToolToCopy.amplitude;
        dampingDepth = C_ToolToCopy.dampingDepth;

        numberOfLayers = C_ToolToCopy.numberOfLayers;
        fomc = new double[numberOfLayers];
        humc = new double[numberOfLayers];
        romc = new double[numberOfLayers];
        biocharc= new double[numberOfLayers];
        for (int j = 0; j < numberOfLayers; j++)
        {
            fomc[j] = C_ToolToCopy.fomc[j];
            romc[j] = C_ToolToCopy.romc[j];
            humc[j] = C_ToolToCopy.humc[j];
            biocharc[j] = C_ToolToCopy.biocharc[j];
        }

    }
    //! A normal member, CopyCTool. Taking one argument.
    /*!
     \param C_ToolToCopy, one ctool2 instance.
    */
    public void CopyCTool(ctool2 C_ToolToCopy)
    {
        Th_diff = C_ToolToCopy.Th_diff;
        FOMdecompositionrate = C_ToolToCopy.FOMdecompositionrate;
        HUMdecompositionrate = C_ToolToCopy.HUMdecompositionrate;
        Clayfraction = C_ToolToCopy.Clayfraction;
        tF = C_ToolToCopy.tF;
        ROMificationfraction = C_ToolToCopy.ROMificationfraction;
        ROMdecompositionrate = C_ToolToCopy.ROMdecompositionrate;
        
        maxSoilDepth = C_ToolToCopy.maxSoilDepth;
        fCO2 = C_ToolToCopy.fCO2;
        CInput = C_ToolToCopy.CInput;
        FOMn = C_ToolToCopy.FOMn;
        CtoNHUM = C_ToolToCopy.CtoNHUM;
        CtoNROM = C_ToolToCopy.CtoNROM;
        FOMNInput = C_ToolToCopy.FOMNInput;
        HUMNInput = C_ToolToCopy.HUMNInput;
        Nlost = C_ToolToCopy.Nlost;
        offset = C_ToolToCopy.offset;
        amplitude = C_ToolToCopy.amplitude;
        dampingDepth = C_ToolToCopy.dampingDepth;

        numberOfLayers = C_ToolToCopy.numberOfLayers;
        fomc = new double[numberOfLayers];
        humc = new double[numberOfLayers];
        romc = new double[numberOfLayers];
        biocharc = new double[numberOfLayers];
        for (int j = 0; j < numberOfLayers; j++)
        {
            fomc[j] = C_ToolToCopy.fomc[j];
            romc[j] = C_ToolToCopy.romc[j];
            humc[j] = C_ToolToCopy.humc[j];
            biocharc[j] = C_ToolToCopy.biocharc[j];
        }

    }
    //! A normal member, reloadC_Tool. Taking one argument.
    /*!
     \param original, one ctool2 instance.
    */
    public void reloadC_Tool(ctool2 original)
    {
        for (int j = 0; j < numberOfLayers; j++)
        {
            fomc[j] = original.fomc[j];
            romc[j] = original.romc[j];
            humc[j] = original.humc[j];
            biocharc[j] = original.biocharc[j];
        }
        FOMn = original.FOMn;
    }
    //! A normal member, Get mumOfLayers. Returning an integer value.
    /*!
      \return an integer value for mumOfLayers.
    */
    public int GetnumOfLayers() { return numberOfLayers; }

    //! A normal member, Get CStored. Returning a double value.
    /*!
      \return a double value for CStored.
    */
    public double GetCStored()
    {
        double Cstored = 0;
        for (int j = 0; j < numberOfLayers; j++)
            Cstored += fomc[j] + humc[j] + romc[j] + biocharc[j];
        return Cstored;
    }
    //! A normal member, Get FOMCStored. Returning a double value.
    /*!
      \return a double value for FOMCStored.
    */
    public double GetFOMCStored()
    {
        double FOMCstored = 0;
        for (int j = 0; j < numberOfLayers; j++)
            FOMCstored += fomc[j];
        return FOMCstored;
    }
    //! A normal member, Get HUMCStored. Returning a double value.
    /*!
      \return a double value for HUMCStored.
    */
    public double GetHUMCStored()
    {
        double HUMCstored = 0;
        for (int j = 0; j < numberOfLayers; j++)
            HUMCstored += humc[j];
        return HUMCstored;
    }
    //! A normal member, Get ROMCStored. Returning a double value.
    /*!
      \return a double value for ROMCStored.
    */
    public double GetROMCStored()
    {
        double ROMCstored = 0;
        for (int j = 0; j < numberOfLayers; j++)
            ROMCstored += romc[j];
        return ROMCstored;
    }
    //! A normal member, Get BiocharCStored. Returning a double value.
    /*!
      \return a double value for BiocharCStored.
    */
    public double GetBiocharCStored()
    {
        double BiocharCstored = 0;
        for (int j = 0; j < numberOfLayers; j++)
            BiocharCstored += biocharc[j];
        return BiocharCstored;
    }
    //! A normal member, Get NStored. Returning a double value.
    /*!
      \return a double value for NCStored.
    */
    public double GetNStored()
    {
        double Nstored = FOMn;
        for (int j = 0; j < numberOfLayers; j++)
            Nstored += (romc[j]/CtoNROM) + (humc[j]/CtoNHUM);
        return Nstored;
    }
    //! A normal member, Get HUMn. Returning a double value.
    /*!
      \return a double value for HUMn.
    */
    public double GetHUMn() { return GetHUMCStored() / CtoNHUM; }
    //! A normal member, Get ROMn. Returning a double value.
    /*!
      \return a double value for ROMn.
    */
    public double GetROMn() { return GetROMCStored() / CtoNROM; }
    //! A normal member, Get CDetails.
    /*!
      more details.
    */
    public void GetCDetails()
    {
        for (int j = 0; j < numberOfLayers; j++)
            GlobalVars.Instance.log(j.ToString() + " " + fomc[j].ToString() + " " + humc[j].ToString() + " " +romc[j].ToString() + " " + biocharc[j].ToString(), 5);
    }

    //! A normal member, Check CBalance.
    /*!
      more details.
    */
    public void CheckCBalance()
    {
        double Cstored = GetCStored();
        double CBalance = CInput - (Cstored + totalCO2Emission);
        double diff = CBalance / CInput;
        if (Math.Abs(diff) > 0.05)
        {
            double errorPercent = 100 * diff;
           /* System.IO.StreamWriter file = new System.IO.StreamWriter(GlobalVars.Instance.GeterrorFileName());
            file.WriteLine("Error; C balance in C-Tool");
            file.Write("Percentage error = " + errorPercent.ToString("0.00") + "%");
            file.Close();*/
            string messageString=("Error; C balance in C-Tool\n");
            messageString+=("Percentage error = " + errorPercent.ToString("0.00") + "%");
            GlobalVars.Instance.Error(messageString);
        }

    }
    //! A member taking 17 arguments 
    /*!
     \param writeOutput a bool argument,
     \param julianDay an integer argument,
     \param startDay a long argument,first day of period
     \param endDay a long argument, last day of period
     \param FOM_Cin a double array argument, array [month, layer] of fresh organic matter input (kg/ha)
     \param HUM_Cin a double array argument, array [month, layer] of humic organic matter input (kg/ha)
     \param Biochar_Cin a double array argument, array [month, layer] of biochar matter input (kg/ha)
     \param FOMnIn a double array argument, array [month] of N input in fresh organic matter (kg/ha)
     \param cultivation a double array argument, array [month] of depth of cultivation (m) (not used yet)
     \param meanTemperature a double array argument, mean air temperature for the agroecological zone (Celcius)
     \param droughtIndex a double argument
     \param Cchange, change in carbon in the soil over the period (kg) 
     \param CO2Emission
     \param Clearched
     \param Nmin, mineralisation of soil N over the period (kg) (negative if N is immobilised)
     \param Nleached N leached from the soil in organic matter (kg)
     \param CropSeqID an integer argument
     */
    public XElement Dynamics(bool writeOutput, int julianDay, long startDay, long endDay, double[,] FOM_Cin, double[,] HUM_Cin, double[,] Biochar_Cin, double[] FOMnIn, 
        double[] cultivation, double[] meanTemperature, double droughtIndex, ref double Cchange, ref double CO2Emission,
        ref double Cleached, ref double Nmin, ref double Nleached,  int CropSeqID)
    {
        Cchange = 0;
        Nmin = 0;
        FOMNInput = 0;
        double FOMCInput = 0;
        double HUMCInput = 0;
        double FOMnmineralised = 0;
        double CStart = GetCStored();
        double NStart = GetNStored();
        double startHUM = GetHUMCStored();
        double startROM = GetROMCStored();
        double FOMCO2 = 0;
        double HUMCO2 = 0;
        double ROMCO2 = 0;
        double BiocharCO2 = 0;
        CO2Emission = 0;
        Nleached = 0;
        Cleached = 0;
        long iterations = endDay - startDay+1;
        double balance = 0;
        
        XElement ctoolData = new XElement("ctool");
        if ((GlobalVars.Instance.Ctoolheader == false)&&(writeOutput))
        {
            int times = 3;
            bool printUnits = false;
            bool printValues = false;
            for (int j = 0; j < times; j++)
            {
                if (j == 1)
                    printUnits = true;
                else
                    printUnits = false;
                if (j == 2)
                {
                    printValues = true;
                    GlobalVars.Instance.Ctoolheader = true;
                }
                else
                    printValues = false;
                GlobalVars.Instance.writeCtoolFile("CropSeqID", "CropSeqID", "CropSeqID", CropSeqID, parens, printValues, printUnits, 0);
                GlobalVars.Instance.writeCtoolFile("startDay", "startDay", "day", startDay.ToString(), parens, printValues, printUnits, 0);
                GlobalVars.Instance.writeCtoolFile("endDay", "endDay", "day", endDay.ToString(), parens, printValues, printUnits, 0);

                GlobalVars.Instance.writeCtoolFile("FOMCStoredStart", "Initial C FOM", "MgC/ha", GetFOMCStored().ToString(), parens, printValues, printUnits, 0);
                GlobalVars.Instance.writeCtoolFile("HUMCStoredStart", "Initial C HUM", "MgC/ha", GetHUMCStored().ToString(), parens, printValues, printUnits,0);
                GlobalVars.Instance.writeCtoolFile("ROMCStoredStart", "Initial C ROM", "MgC/ha", GetROMCStored().ToString(), parens, printValues, printUnits,0);
                GlobalVars.Instance.writeCtoolFile("BiocharCStoredStart", "Initial C ROM", "MgC/ha", GetBiocharCStored().ToString(), parens, printValues, printUnits,0);
                GlobalVars.Instance.writeCtoolFile("FOMnStoredStart", "Initial N FOM", "MgN/ha", GetFOMn().ToString(), parens, printValues, printUnits,0);
                GlobalVars.Instance.writeCtoolFile("HUMnStoredStart", "Initial N HUM", "MgN/ha", GetHUMn().ToString(), parens, printValues, printUnits,0);
                GlobalVars.Instance.writeCtoolFile("ROMnStoredStart", "Initial N ROM", "MgN/ha", GetROMn().ToString(), parens, printValues, printUnits,0);

                GlobalVars.Instance.writeCtoolFile("FOMCInput", "FOM_C_input", "MgC/ha/period", FOMCInput.ToString(), parens, printValues, printUnits,0);
                GlobalVars.Instance.writeCtoolFile("HUMCInput", "HUM_C_input", "MgC/ha/period", HUMCInput.ToString(), parens, printValues, printUnits,0);
                GlobalVars.Instance.writeCtoolFile("BiocharCInput", "Biochar_C_input", "MgC/ha/period", BiocharCInput.ToString(), parens, printValues, printUnits, 0);
                GlobalVars.Instance.writeCtoolFile("CO2Emission", "CO2_C_emission", "MgC/ha/period", CO2Emission.ToString(), parens, printValues, printUnits,0);
                GlobalVars.Instance.writeCtoolFile("balance", "balance", "MgC/ha/period", balance.ToString(), parens, printValues, printUnits,0);


                GlobalVars.Instance.writeCtoolFile("FOMCStoredEnd", "Final_C_FOM", "MgC/ha", GetFOMCStored().ToString(), parens, printValues, printUnits,0);
                GlobalVars.Instance.writeCtoolFile("HUMCStoredEnd", "Final_C_HUM", "MgC/ha", GetHUMCStored().ToString(), parens, printValues, printUnits,0);
                GlobalVars.Instance.writeCtoolFile("ROMCStoredEnd", "Final_C_ROM", "MgC/ha", GetROMCStored().ToString(), parens, printValues, printUnits,0);
                GlobalVars.Instance.writeCtoolFile("BiocharCStoredEnd", "Final_C_ROM", "MgC/ha", GetBiocharCStored().ToString(), parens, printValues, printUnits,0);
                GlobalVars.Instance.writeCtoolFile("SoilOrganicCarbon", "Final_C_Total", "MgC/ha", GetCStored().ToString(), parens, printValues, printUnits,0);
                GlobalVars.Instance.writeCtoolFile("FOMnStoredEnd", "Final_N_FOM", "MgN/ha", GetFOMn().ToString(), parens, printValues, printUnits,0);
                GlobalVars.Instance.writeCtoolFile("HUMnStoredEnd", "Final_N_HUM", "MgN/ha", GetHUMn().ToString(), parens, printValues, printUnits,0);
                GlobalVars.Instance.writeCtoolFile("ROMnStoredEnd", "Final_N_ROM", "MgN/ha", GetROMn().ToString(), parens, printValues, printUnits,0);
                GlobalVars.Instance.writeCtoolFile("Total_soil_N", "Total_soilN_Total", "MgN/ha", GetFOMn() + GetHUMn() + GetROMn(), parens, printValues, printUnits,0);

                GlobalVars.Instance.writeCtoolFile("FOMNInput", "FOMNin", "MgN/ha/period", FOMNInput.ToString(), parens, printValues, printUnits,0);
                GlobalVars.Instance.writeCtoolFile("HUMNInput", "HUMNin", "MgN/ha/period", 0.ToString(), parens, printValues, printUnits,0);
                GlobalVars.Instance.writeCtoolFile("Nmin", "Nmin", "MgN/ha/period", Nmin.ToString(), parens, printValues, printUnits,0);

                GlobalVars.Instance.writeCtoolFile("Org_N_leached", "Org_N_leached", "MgN/ha/period", Nleached.ToString(), parens, printValues, printUnits,0);
                GlobalVars.Instance.writeCtoolFile("NStart", "NStart", "MgN/ha", NStart.ToString(), parens, printValues, printUnits,0);
                GlobalVars.Instance.writeCtoolFile("Nend", "Nend", "MgN/ha", 0.ToString(), parens, printValues, printUnits,0);
                GlobalVars.Instance.writeCtoolFile("FOMnmineralised", "FOMnmineralised", "MgN/ha/period", FOMnmineralised, parens, printValues, printUnits,1);
            }
        }
        double min = 999999;
        double max = 0;
        for (int j = 0; j < 12; j++)
        {
            if (meanTemperature[j] < min)
                min = meanTemperature[j];

            if (meanTemperature[j] > max)
                max = meanTemperature[j];
        }
        amplitude = (max - min) / 2;
        if (writeOutput)
        {
            ctoolData.Add(GlobalVars.Instance.writeCtoolFile("CropSeqID", "CropSeqID", "CropSeqID", CropSeqID, parens, true, false, 0));
            ctoolData.Add(GlobalVars.Instance.writeCtoolFile("startDay", "startDay", "day", startDay.ToString(), parens, true, false, 0));
            ctoolData.Add(GlobalVars.Instance.writeCtoolFile("endDay", "endDay", "day", endDay.ToString(), parens, true, false, 0));

            ctoolData.Add(GlobalVars.Instance.writeCtoolFile("GetFOMCStored", "Initial C FOM", "MgC/ha", GetFOMCStored().ToString(), parens, true, false, 0));
            ctoolData.Add(GlobalVars.Instance.writeCtoolFile("GetHUMCStored", "Initial C HUM", "MgC/ha", GetHUMCStored().ToString(), parens, true, false, 0));
            ctoolData.Add(GlobalVars.Instance.writeCtoolFile("GetROMCStored", "Initial C ROM", "MgC/ha", GetROMCStored().ToString(), parens, true, false, 0));
            ctoolData.Add(GlobalVars.Instance.writeCtoolFile("GetBiocharCStored", "Initial C ROM", "MgC/ha", GetBiocharCStored().ToString(), parens, true, false, 0));
            ctoolData.Add(GlobalVars.Instance.writeCtoolFile("GetFOMn", "Initial N FOM", "MgN/ha", GetFOMn().ToString(), parens, true, false, 0));
            ctoolData.Add(GlobalVars.Instance.writeCtoolFile("GetHUMn", "Initial N HUM", "MgN/ha", GetHUMn().ToString(), parens, true, false, 0));
            ctoolData.Add(GlobalVars.Instance.writeCtoolFile("GetROMn", "Initial N ROM", "MgN/ha", GetROMn().ToString(), parens, true, false, 0));
        }

        double cumFOMnmineralised = 0;
        double cumhumificationAmount = 0;
        double totFOMCO2 = 0;

        //double totTransportOut = 0;
        for (int i = 0; i < iterations; i++)
        {
			if (julianDay >= 365)
                julianDay = 1;
            double JulianAsDouble=(double)julianDay;
            int month = (int) Math.Floor(JulianAsDouble / 30.4166)+1;
            /*double tmp = (i) / 12;
            int year = (int)Math.Floor(tmp) + 1;*/
            double juliandayCtool = month * 30.4166;
            double FOMtransportIn=0;
            double FOMtransportOut=0;
            double HUMtransportIn=0;
            double HUMtransportOut = 0;
            double ROMtransportIn = 0;
            double ROMtransportOut = 0;
            double BiochartransportOut = 0;
            double BiochartransportIn = 0;
            double startFOM = GetFOMCStored();
            double newFOM = 0;
            double cumFOMCO2 = 0;
            double newHUM = 0;
            double newROM = 0;
            double FOMmineralised = 0;
            for (int j = 0; j < numberOfLayers; j++)
            {
                FOMCInput += FOM_Cin[i, j];
                FOMcInput += FOM_Cin[i, j];
                HUMCInput += HUM_Cin[i, j];
                HUMcInput += HUM_Cin[i, j];
                BiocharCInput += Biochar_Cin[i, j];
                BiocharcInput += Biochar_Cin[i, j];
                if (HUMCInput > 0)
                    Console.Write("");
                CInput += FOMCInput + HUMCInput + BiocharCInput;
                layerDynamics(julianDay, j, meanTemperature[month-1], droughtIndex, FOMtransportIn, 
                    ref FOMtransportOut, ref FOMCO2, HUMtransportIn, ref HUMtransportOut, ref HUMCO2, 
                    ROMtransportIn, ref ROMtransportOut, ref ROMCO2,
                    BiochartransportIn, ref BiochartransportOut, ref BiocharCO2,
                    ref newHUM, ref newROM);



                CO2Emission += FOMCO2 + HUMCO2 + ROMCO2 + BiocharCO2;
                FOMcCO2 += FOMCO2;
                HUMcCO2 += HUMCO2;
                ROMcCO2 += ROMCO2;
                BiocharcCO2 += BiocharCO2;
                FOMcToHUM += newHUM;
                HUMcToROM += newROM;
                cumFOMCO2 += FOMCO2;
                cumhumificationAmount += newHUM;
                FOMmineralised += FOMCO2 + newHUM;
                FOMtransportIn = FOMtransportOut;
                HUMtransportIn = HUMtransportOut;
                ROMtransportIn = ROMtransportOut;
                BiochartransportIn = BiochartransportOut;
                fomc[j] += FOM_Cin[i, j];
                humc[j] += HUM_Cin[i, j];
                biocharc[j] += Biochar_Cin[i, j];
                newFOM += FOM_Cin[i, j];
            }
            totFOMCO2 += cumFOMCO2;
            //last value of C transport out equates to C leaving the soil
            double FOMntransportOut = (FOMtransportOut * FOMn) / GetFOMCStored();
           // totTransportOut += FOMntransportOut + ROMtransportOut + HUMtransportOut;
//            Nleached += FOMntransportOut + HUMtransportOut / CtoNHUM + ROMtransportOut / CtoNROM;
            Nleached = 0;
            if (startFOM > 0)
                FOMnmineralised = FOMmineralised * FOMn / startFOM;
            else
                FOMnmineralised = 0;
            cumFOMnmineralised += FOMnmineralised;
            FOMNInput += FOMnIn[i];
            double test = FOMCInput / FOMNInput;
            FOMn += FOMnIn[i] - FOMnmineralised ;
            test= GetFOMCStored() / FOMn;
            julianDay++;
        }
        double CEnd = GetCStored();
        Cchange = CEnd - CStart;
        double Nend = GetNStored();
        double theHUMNInput = HUMCInput / CtoNHUM;  //Jonas
        Nmin = NStart + FOMNInput + theHUMNInput - Nend - Nleached;

        balance = FOMcInput - (FOMcCO2  + GetFOMCStored() + FOMcToHUM);
        balance = HUMcInput + FOMcToHUM - (HUMcCO2 + GetHUMCStored() + HUMcToROM);
        balance = ROMcInput + HUMcToROM - ROMcCO2 - GetROMCStored();
        balance = BiocharcInput - BiocharcCO2 - GetBiocharCStored();
        balance = CStart + FOMCInput + HUMCInput + BiocharcInput - (CO2Emission + CEnd);
        double HUMNInput = HUMCInput / CtoNHUM;
       //CheckCBalance();
        if (writeOutput)
        {
            ctoolData.Add(GlobalVars.Instance.writeCtoolFile("FOMCInput", "FOM_C_input", "", FOMCInput.ToString(), parens, true, false, 0));
            ctoolData.Add(GlobalVars.Instance.writeCtoolFile("HUMCInput", "HUM_C_input", "", HUMCInput.ToString(), parens, true, false, 0));
            ctoolData.Add(GlobalVars.Instance.writeCtoolFile("BiocharCInput", "Biochar_C_input", "", BiocharCInput.ToString(), parens, true, false, 0));
            ctoolData.Add(GlobalVars.Instance.writeCtoolFile("CO2Emission", "CO2_C_emission", "", CO2Emission.ToString(), parens, true, false, 0));
            ctoolData.Add(GlobalVars.Instance.writeCtoolFile("balance", "balance", "", balance.ToString(), parens, true, false, 0));


            ctoolData.Add(GlobalVars.Instance.writeCtoolFile("GetFOMCStored", "Final_C_FOM", "", GetFOMCStored().ToString(), parens, true, false, 0));
            ctoolData.Add(GlobalVars.Instance.writeCtoolFile("GetHUMCStored", "Final_C_HUM", "", GetHUMCStored().ToString(), parens, true, false, 0));
            ctoolData.Add(GlobalVars.Instance.writeCtoolFile("GetROMCStored", "Final_C_ROM", "", GetROMCStored().ToString(), parens, true, false, 0));
            ctoolData.Add(GlobalVars.Instance.writeCtoolFile("GetBiocharCStored", "Final_C_Biochar", "", GetBiocharCStored().ToString(), parens, true, false, 0));
            ctoolData.Add(GlobalVars.Instance.writeCtoolFile("GetCStored", "Final_C_Total", "", GetCStored().ToString(), parens, true, false, 0));

            double finalN = GetFOMn() + GetHUMn() + GetROMn();

            ctoolData.Add(GlobalVars.Instance.writeCtoolFile("GetFOMn", "Final_N_FOM", "", GetFOMn().ToString(), parens, true, false, 0));
            ctoolData.Add(GlobalVars.Instance.writeCtoolFile("GetHUMn", "Final_N_HUM", "", GetHUMn().ToString(), parens, true, false, 0));
            ctoolData.Add(GlobalVars.Instance.writeCtoolFile("GetROMn", "Final_N_ROM", "", GetROMn().ToString(), parens, true, false, 0));
            ctoolData.Add(GlobalVars.Instance.writeCtoolFile("Total_soil_N","Total_soilN_Total","", finalN.ToString(), parens, true, false, 0));

            ctoolData.Add(GlobalVars.Instance.writeCtoolFile("FOMNInput", "FOMNin", "", FOMNInput.ToString(), parens, true, false, 0));
            ctoolData.Add(GlobalVars.Instance.writeCtoolFile("HUMNInput", "HUMNin", "", HUMNInput.ToString(), parens, true, false, 0));
            ctoolData.Add(GlobalVars.Instance.writeCtoolFile("Nmin", "Nmin", "", Nmin.ToString(), parens, true, false, 0));

            ctoolData.Add(GlobalVars.Instance.writeCtoolFile("Org_N_leached", "Org_N_leached", "", Nleached.ToString(), parens, true, false, 0));
            ctoolData.Add(GlobalVars.Instance.writeCtoolFile("NStart", "NStart", "", NStart.ToString(), parens, true, false, 0));
            ctoolData.Add(GlobalVars.Instance.writeCtoolFile("Nend", "Nend", "", Nend.ToString(), parens, true, false, 0));

            ctoolData.Add(GlobalVars.Instance.writeCtoolFile("FOMnmineralised", "FOMnmineralised", "", cumFOMnmineralised, parens, true, false, 1));
                
        }
        return ctoolData;
    }
    //! A normal member, Calculate DampingDepth. Taking two arguments and Returning a double value.
    /*!
      \param k a double argument
      \param rho a double argument
      \return a double value for DampingDepth.
    */
    double CalcDampingDepth(double k, double rho)
    {
        return Math.Sqrt(2.0 * k / rho);
    }
    //! A normal member, Calculate Temperature. Taking five arguments and Returning a double value.
    /*!
      \param avgTemperature a double argument
      \param day a double argument
      \param depth a double argument
      \param amplitude a double argument
      \param offset a double argument
      \return a double value for Temperature.
    */
    public double Temperature(double avgTemperature, double day, double depth, double amplitude, double offset)
    {
        double rho = 3.1415926 * 2.0 / (365.0 * 24.0 * 3600.0);
        double Th_diff = 0.35E-6;
        double dampingDepth = CalcDampingDepth(Th_diff, rho);
        double retVal = avgTemperature + amplitude * Math.Exp(-depth / dampingDepth) * Math.Sin(rho * (day + offset) * 24.0 * 3600.0 - depth / dampingDepth);
        return retVal;
    }
    //! A normal member taking 15 arguments 
    /*!
     \param julianDay an integer argument,
     \param layerNo an integer argument,
     \param temperature a double argument
     \param droughtCoefficient a double argument
     \param FOMtransportIn a double argument
     \param FOMtransportOut 
     \param FOMCO2
     \param HUMtransportIn a double argument
     \param HUMtransportOut
     \param HUMCO2
     \param BiochartransportIn a double argument 
     \param BiochartransportOut
     \param BiocharCO2
     \param newHUM
     \param newROM
     */
    public void layerDynamics(int JulianDay, int layerNo, double temperature, double droughtCoefficient, double FOMtransportIn, 
        ref double FOMtransportOut, ref double FOMCO2, double HUMtransportIn, ref double HUMtransportOut, ref double HUMCO2,
        double ROMtransportIn, ref double ROMtransportOut, ref double ROMCO2, 
        double BiochartransportIn, ref double BiochartransportOut, ref double BiocharCO2, 
        ref double newHUM, ref double newROM)
    {
        double CO2=0;
        double fomcStart = fomc[layerNo];
        double humcStart = humc[layerNo];
        double romcStart = romc[layerNo];
        double biocharcStart = biocharc[layerNo];
        bool zeroRatesForDebugging = false; //use true to help when debugging
        //tF = 0;
        double depthInLayer = (100.0) / numberOfLayers * layerNo + (100.0) / numberOfLayers/2;
  
        double temp =Temperature(temperature, JulianDay, depthInLayer, amplitude, offset);
        double tempCofficent = temperatureCoefficent(temp);
        double temporaryCoefficient = tempCofficent *(1 - droughtCoefficient);
        if (zeroRatesForDebugging)
            FOMdecompositionrate = 0;
        //do FOM
        double FomAfterDecom = rk4decay(fomc[layerNo], timeStep, FOMdecompositionrate, temporaryCoefficient);
        double remainingDegradedFOM=fomc[layerNo]-FomAfterDecom;
        FOMtransportOut = remainingDegradedFOM * tF;
        remainingDegradedFOM -= FOMtransportOut;
        
        //Jonas - this following calculation could be moved to Initialisation, since it need only be calculated once
        double Rfraction = R(Clayfraction);
        double humification = 1 / (Rfraction + 1);
        newHUM = remainingDegradedFOM * humification;
        FOMCO2 = remainingDegradedFOM * (1 - humification);
        CO2+=FOMCO2;
        double test = (fomc[layerNo] - FomAfterDecom) - (FOMtransportOut + newHUM + FOMCO2);
        fomc[layerNo] = FomAfterDecom + FOMtransportIn;
        if (layerNo == (numberOfLayers-1))
            fomc[layerNo] += FOMtransportOut;
        if (zeroRatesForDebugging)
            HUMdecompositionrate = 0;
        //do HUM
        double HumAfterDecom = rk4decay(humc[layerNo], timeStep, HUMdecompositionrate, temporaryCoefficient);
       
        double degradedHUM = humc[layerNo] - HumAfterDecom;
        newROM = ROMificationfraction* degradedHUM;
        HUMCO2 = fCO2 * degradedHUM;
        HUMtransportOut = degradedHUM * (1 - fCO2 - ROMificationfraction);
        CO2+=HUMCO2;
        double test2 = (humc[layerNo] - HumAfterDecom) - (HUMCO2 + HUMtransportOut + newROM);
        humc[layerNo] = HumAfterDecom + newHUM + HUMtransportIn;
        if (layerNo == (numberOfLayers-1))
            humc[layerNo] += HUMtransportOut;
        if (zeroRatesForDebugging)
            ROMdecompositionrate = 0;
        romc[layerNo] += newROM;
        //do ROM
        double RomAfterDecom = rk4decay(romc[layerNo], timeStep, ROMdecompositionrate, temporaryCoefficient);
      
        double degradedROM = romc[layerNo] - RomAfterDecom;

        ROMCO2 = fCO2 * degradedROM;
        ROMtransportOut = degradedROM * (1 - fCO2);
        romc[layerNo] = RomAfterDecom + ROMtransportIn;

        //use ROM decomposition rate for biochar
        double BiocharAfterDecom = rk4decay(biocharc[layerNo], timeStep, ROMdecompositionrate, temporaryCoefficient);

        double degradedBiochar = biocharc[layerNo] - BiocharAfterDecom;
        BiocharCO2 = fCO2 * degradedBiochar;
        BiochartransportOut = degradedBiochar * (1 - fCO2);
        biocharc[layerNo] = BiocharAfterDecom + BiochartransportIn;

        double balance1 = fomcStart + FOMtransportIn - (fomc[layerNo] + FOMCO2 + FOMtransportOut + newHUM);
        double balance2 = humcStart + HUMtransportIn + newHUM - (humc[layerNo] + HUMCO2 + HUMtransportOut + newROM);
        double balance3 = romcStart + ROMtransportIn + newROM - (romc[layerNo] + ROMCO2 + ROMtransportOut);
        double balance4 = biocharcStart + BiochartransportIn - (biocharc[layerNo] + BiocharCO2 + BiochartransportOut);
        if (layerNo == (numberOfLayers - 1))
        {
            romc[layerNo] += ROMtransportOut;
            biocharc[layerNo] += BiochartransportOut;
        }
    }
    //! A private member, Calculate R. Taking one argument and Returning a double value.
    /*!
      \param Clayfraction a double argument
      \return a double value for R.
    */
    private double R(double Clayfraction)
    {
        return 1.67 * (1.85 + 1.6 * Math.Exp(-7.86 * Clayfraction));
    }
    //! A private member, Calculate temperatureCoefficent. Taking one argument and Returning a double value.
    /*!
      \param temperature a double argument
      \return a double value for temperatureCoefficent.
    */
    private double temperatureCoefficent(double temperature)
    {
	    return 7.24*Math.Exp(-3.432+0.168*temperature*(1-0.5*temperature/36.9)); 
    }
    //! A private member, Calculate func. Taking two arguments and Returning a double value.
    /*!
      \param amount a double argument
      \param coeff a double argument
      \return a double value for func.
    */
    private double func (double amount,double coeff)
    {
        return amount*-coeff;
    }
    //! A private member, Calculate rk4decay. Taking four arguments and Returning a double value.
    /*!
      \param u0 a double argument
      \param dt a double argument
      \param k a double argument
      \param temporaryCoefficient a double argument
      \return a double value for rk4decay.
    */
    private double  rk4decay ( double u0,double dt,double k, double temporaryCoefficient)
    {
        double coeff = k * temporaryCoefficient;
        double f1 = func(u0, coeff);
        double f2 = func(u0 + dt * f1 / 2,coeff);
        double f3 = func(u0 + dt * f2 / 2, coeff);
        double f4 = func(u0 + dt * f3, coeff);
        double retVal = u0 + dt * ( f1 + 2.0 * f2 + 2.0 * f3 + f4 ) / 6.0;
        return retVal;
    }
    //! A public member, Calculate CtoNFactor. Taking one argument and Returning a double value.
    /*!
      \param CtoNratio a double argument
      \return a double value for CtoNFactor.
    */
    public double GetCtoNFactor(double CtoNratio)
    {
        double retVal = Math.Min(56.2 * Math.Pow(CtoNratio,- 1.69), 1.0);
        return retVal;
    }
    //! A public member, Write. 
    /*!
      more details.
    */
    public void Write()
    {

        GlobalVars.Instance.writeStartTab("Ctool2");
        GlobalVars.Instance.writeInformationToFiles("timeStep", "Timestep", "Day", timeStep, parens);
        GlobalVars.Instance.writeInformationToFiles("numberOfLayers", "Number of soil layers", "-", numberOfLayers, parens);
        GlobalVars.Instance.writeInformationToFiles("FOMdecompositionrate", "FOM decomposition rate", "per day", FOMdecompositionrate, parens);
        GlobalVars.Instance.writeInformationToFiles("HUMdecompositionrate", "HUM decomposition rate", "per day", HUMdecompositionrate, parens);
        GlobalVars.Instance.writeInformationToFiles("ROMdecompositionrate", "ROM decomposition rate", "per day", ROMdecompositionrate, parens);
        GlobalVars.Instance.writeInformationToFiles("ROMificationfraction", "ROMification rate", "per day", ROMificationfraction, parens);
        GlobalVars.Instance.writeInformationToFiles("fCO2", "fCO2", "-", fCO2, parens);
        GlobalVars.Instance.writeInformationToFiles("GetFOMCStored", "GetFOMCStored", "-", GetFOMCStored(), parens);
        GlobalVars.Instance.writeInformationToFiles("GetHUMCStored", "GetHUMCStored", "-", GetHUMCStored(), parens);
        GlobalVars.Instance.writeInformationToFiles("GetROMCStored", "GetROMCStored", "-", GetROMCStored(), parens);
        GlobalVars.Instance.writeInformationToFiles("GetBiocharCStored", "GetBiocharCStored", "-", GetBiocharCStored(), parens);
        GlobalVars.Instance.writeInformationToFiles("GetFOMn", "GetFOMn", "-", GetFOMn(), parens);
        GlobalVars.Instance.writeInformationToFiles("GetHUMn", "GetHUMn", "-", GetHUMn(), parens);
        GlobalVars.Instance.writeInformationToFiles("GetROMn", "GetROMn", "-", GetROMn(), parens);
        GlobalVars.Instance.writeEndTab();
       
    }
}