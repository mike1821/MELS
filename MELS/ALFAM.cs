using System;
/// <summary>
//! Class to implement the ALFAM model of ammonia emission from slurry (Atmospheric Environment 36 (2002) 3309–3319)
/*!
  A more elaborate class description.
*/
/// </summary>
public class ALFAM
{
    /// <summary>
    //! Parameters of model
    /// </summary>
    public double maxTime = 168.0;
    public double b_Nmx0 = -6.5757;
    public double b_sm1Nmx = 0.0971;
    public double b_atNmx = 0.0221;
    public double b_wsNmx = 0.0409;
    public double b_mt1Nmx = -0.156;
    public double b_mdmNmx = 0.1024;
    public double b_mtanNmx = -0.1888;
    public double b_ma0Nmx = 3.5691;
    public double b_ma1Nmx = 3.0198;
    public double b_ma2Nmx = 3.1592;
    public double b_ma3Nmx = 2.2702;
    public double b_ma4Nmx = 2.9582;
    public double b_mrNmx = -0.00433;
    public double b_mi0Nmx = 2.4291;
    public double b_met1Nmx = -0.6382;
    public double b_met2Nmx = -0.5485;
    public double b_Km0 = 0.037;
    public double b_sm1Km = 0.0974;
    public double b_atKm = -0.0409;
    public double b_wsKm = -0.0517;
    public double b_mt1Km = 1.3567;
    public double b_mdmKm = 0.1614;
    public double b_mtanKm = 0.1011;
    public double b_mrKm = 0.0175;
    public double b_met1Km = 0.3888;
    public double b_met2Km = 0.7024;
    public double applicRate = 0;
    public double exposureTime = 0;
    public double km = 0;
    public double Nmax = 0;
    public double TAN = 0;

    /// <summary>
    //! Default constructor (empty)
    // </summary>
    public ALFAM()
    {
    }
    //! A member taking 9 arguments 
    /*!
     \param soilWet an integer argument.
     \param aveAirTemp a double argument, air temperature in Celsius.
     \param aveWindspeed a double argument, wind speed in metres per second
     \param manureType an integer argument, 1 = cattle, 2 = pig
     \param initDM a double argument, initial dry matter content
     \param initTAN a double argument, initial TAN content
     \param appRate a double argument,  application rate in tonnes per ha
     \param appMeth an integer argument, application method (1 = broadcast, 2 = trailing hose, 3 = trailing shoe, 4 = open slot injection, 5 = closed slot injection)
     \param anExposureTime a double argument, duration of emission event in hours
   */

    public void initialise(int soilWet, //
                           double aveAirTemp, //air temperature in Celsius
                           double aveWindspeed, // wind speed in metres per second
                           int manureType, // 1 = cattle, 2 = pig
                           double initDM, // initial dry matter content 
                           double initTAN, // initial TAN content
                           double appRate, // application rate in tonnes per ha
                           int appMeth, // application method (1 = broadcast, 2 = trailing hose, 3 = trailing shoe, 4 = open slot injection, 5 = closed slot injection)
                           double anExposureTime)  // duration of emission event in hours
    {
        TAN = initTAN;
        applicRate = appRate;
        exposureTime = anExposureTime;

        switch (appMeth)
        {
            case 1:    // broadcast
                Nmax = Math.Exp(b_Nmx0 + b_sm1Nmx * soilWet + b_atNmx * aveAirTemp + b_wsNmx * aveWindspeed
                           + b_mt1Nmx * manureType + b_mdmNmx * initDM + b_mtanNmx * TAN + b_ma0Nmx + b_mrNmx * appRate
                           + b_mi0Nmx + b_met2Nmx);
                km = Math.Exp(b_Km0 + b_sm1Km * soilWet + b_atKm * aveAirTemp + b_wsKm * aveWindspeed + b_mt1Km * manureType
                         + b_mdmKm * initDM + b_mtanKm * TAN + b_mrKm * appRate + b_met2Km);

                break;

            case 2:    // trailing hose
                Nmax = Math.Exp(b_Nmx0 + b_sm1Nmx * soilWet + b_atNmx * aveAirTemp + b_wsNmx * aveWindspeed
                           + b_mt1Nmx * manureType + b_mdmNmx * initDM + b_mtanNmx * TAN + b_ma1Nmx + b_mrNmx * appRate
                           + b_mi0Nmx + b_met2Nmx);
                km = Math.Exp(b_Km0 + b_sm1Km * soilWet + b_atKm * aveAirTemp + b_wsKm * aveWindspeed + b_mt1Km * manureType
                         + b_mdmKm * initDM + b_mtanKm * TAN + b_mrKm * appRate + b_met2Km);

                break;

            case 3:    // trailing shoe
                Nmax = Math.Exp(b_Nmx0 + b_sm1Nmx * soilWet + b_atNmx * aveAirTemp + b_wsNmx * aveWindspeed
                           + b_mt1Nmx * manureType + b_mdmNmx * initDM + b_mtanNmx * TAN + b_ma2Nmx + b_mrNmx * appRate
                           + b_mi0Nmx + b_met2Nmx);
                km = Math.Exp(b_Km0 + b_sm1Km * soilWet + b_atKm * aveAirTemp + b_wsKm * aveWindspeed + b_mt1Km * manureType
                         + b_mdmKm * initDM + b_mtanKm * TAN + b_mrKm * appRate + b_met2Km);

                break;

            case 4:    // open slot
                Nmax = Math.Exp(b_Nmx0 + b_sm1Nmx * soilWet + b_atNmx * aveAirTemp + b_wsNmx * aveWindspeed
                           + b_mt1Nmx * manureType + b_mdmNmx * initDM + b_mtanNmx * TAN + b_ma3Nmx + b_mrNmx * appRate
                           + b_mi0Nmx + b_met2Nmx);
                km = Math.Exp(b_Km0 + b_sm1Km * soilWet + b_atKm * aveAirTemp + b_wsKm * aveWindspeed + b_mt1Km * manureType
                         + b_mdmKm * initDM + b_mtanKm * TAN + b_mrKm * appRate + b_met2Km);

                break;

            case 5:    // closed slot
                Nmax = Math.Exp(b_Nmx0 + b_sm1Nmx * soilWet + b_atNmx * aveAirTemp + b_wsNmx * aveWindspeed
                           + b_mt1Nmx * manureType + b_mdmNmx * initDM + b_mtanNmx * TAN + b_ma4Nmx + b_mrNmx * appRate
                           + b_mi0Nmx + b_met2Nmx);
                km = Math.Exp(b_Km0 + b_sm1Km * soilWet + b_atKm * aveAirTemp + b_wsKm * aveWindspeed + b_mt1Km * manureType
                         + b_mdmKm * initDM + b_mtanKm * TAN + b_mrKm * appRate + b_met2Km);

                break;
        }
    }

    /// <summary>
    //! A member,that Returns proportion of TAN volatilised.
    /*!

    \return proportion of TAN volatilised
    
  */

    /// </summary>

    public double ALFAM_volatilisation()
    {
      double ret_val = Nmax* exposureTime / (exposureTime+ km);
      return ret_val;
    }
    //! A member,that Get ALFARMApplicCode.
    /*!
      \param OpCode an integer argument.
      
      \return The code number or string
      
    */
    public int GetALFAMApplicCode(int OpCode)
    {
        switch (OpCode)
        {
            case 7:    // SpreadingLiquidManure
                return 1;
                break;

            case 8:    // ClosedSlotInjectingLiquidManure
                return 5;
                break;

            case 9:    // SpreadingSolidManure
                return 1;
                break;

            case 35:    // OpenSlotInjectingLiquidManure
                return 4;
                break;

            case 36:    // TrailingHoseSpreadingLiquidManure
                return 2;
                break;

            case 37:    // TrailingShoeSpreadingLiquidManure
                return 3;
                break;

            default:
                string theMessage="ALFAM: application method code not found";
                break;
        }

        return 0;
    }
}

