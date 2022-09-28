#define WIDE_AREA
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.IO;

public class VMP3
{
    private static VMP3 instance;
    private string VMP3filename;
  
    public void closeVMP3files() { farmfile.Close(); fieldfile.Close(); }
    //System.IO.StreamWriter VMP3File;
    System.IO.StreamWriter fieldfile;
    System.IO.StreamWriter farmfile;

    public static VMP3 Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new VMP3();
            }
            return instance;
        }
    }

    public void openVMP3(string outputDir, string filename1, string filename2)
    {
        farmfile = new System.IO.StreamWriter(outputDir + filename1 + ".txt");
        fieldfile = new System.IO.StreamWriter(outputDir + filename2 + ".txt");
    }
    public void WriteFarm(string aString)
    {
        farmfile.Write(aString);
    }
    public void WriteLineFarm(string aString)
    {
        farmfile.WriteLine(aString);
    }
    public void WriteField(string aString)
    {
        fieldfile.Write(aString);
    }
    public void WriteLineField(string aString)
    {
        fieldfile.WriteLine(aString);
    }
    public void WriteFarmHeader()
    {
        farmfile.Write("VMP3ID" + "\t" + "farmType" + "\t" + "farmArea" + "\t" + "entericCH4CO2Eq" + "\t" + "manureCH4CO2Eq" + "\t" + "manureN2OCO2Eq" +
            "\t" + "housingNH3CO2Eq" + "\t" + "manurestoreNH3CO2Eq" + "\t" + "fieldCH4CO2Eq" + "\t" + "fieldfertNH3CO2Eq" + "\t" + "fieldmanureNH3CO2Eq" + "\t" + "leachedNCO2Eq" + "\t" + "totalGHGCO2Eq" + 
            "\t" +"housingNH3Nemission" + "\t" + "manureNH3Nemission" + "\t" + "fieldNitrateLeachedN" + "\t" +
             "fertNapplied" + "\t" + "manNapplied" + "\t" + "manNexStore");
        farmfile.WriteLine("");
    }
    public void WriteFieldHeader()
    {
        fieldfile.Write("VMP3ID" + "\t" + "IMK_ID" + "\t" + "area"+ "\t" + "areaProportion" + "\t" +
            "OtherGHGemissionsCO2eq" + "\t" + "N2OCO2eq" + "\t" + "fieldN2OCO2Eq" + "\t" + "fertiliserN2OEmissionCO2eq" + 
            "\t" + "manureN2OEmissionCO2eq" + "\t" + "fieldcropresidueCO2Eq" + "\t" + "fertNH3emissionCO2eq" + "\t" + "fieldmanureNH3CO2Eq" + 
            "\t" + "leachedNCO2Eq" + "\t" + "leachedN"+ "\t"  + "NH3Nemission"
            + "\t" + "crop" + "\t" + "fertN" + "\t" +"ManType" + "\t" + "ManN");
        fieldfile.WriteLine("");
    }
}
