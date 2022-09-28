using System;
using System.Xml;
/*! A class that named timeClass. */
public class timeClass
{
    private int day;
    private int month;
    private int year;
    string parens;
    private int[] tabDaysPerMonth;
    //! A constructor.
    /*!
        without argument.
     */

    public timeClass()
	{
        tabDaysPerMonth = new int[12];

        tabDaysPerMonth[0] = 31;
        tabDaysPerMonth[1] = 28;
        tabDaysPerMonth[2] = 31;
        tabDaysPerMonth[3] = 30;
        tabDaysPerMonth[4] = 31;
        tabDaysPerMonth[5] = 30;
        tabDaysPerMonth[6] = 31;
        tabDaysPerMonth[7] = 31;
        tabDaysPerMonth[8] = 30;
        tabDaysPerMonth[9] = 31;
        tabDaysPerMonth[10] = 30;
        tabDaysPerMonth[11] = 31;
	}
    //! A constructor with one argument.
    /*!
      \param previousclass, one instance that points to timeClass class.
     */
    public timeClass(timeClass previousclass)
    {
        day = previousclass.day;
        month = previousclass.month;
        year = previousclass.year;
        tabDaysPerMonth = new int [12];
        for (int i = 0; i < 12; i++)
            tabDaysPerMonth[i] = previousclass.tabDaysPerMonth[i];
    }
    //! A normal member, Set Date. Taking three arguments and returning a boolean value.
    /*!
      \param aday, an integer argument.
      \param amonth, an integer argument.
      \param ayear, an integer argument.
      \return a boolean value
    */
    public bool setDate(int aday, int amonth, int ayear)
    {
        bool isOk = true;
        if ((aday <= 0) || (aday > 31))
            isOk = false;
        else
            day = aday;
        if ((amonth <= 0) || (amonth > 12))
            isOk = false;
        else
            month = amonth;
        year= ayear;
        return isOk;
    }
    //! A normal member, Set Day. Taking one argument.
    /*!
      \param aday, an integer argument.
    */
    public void SetDay(int aDay) { day = aDay; }
    //! A normal member, Set Month. Taking one argument.
    /*!
      \param aMonth, an integer argument.
    */
    public void SetMonth(int aMonth) { month = aMonth; }
    //! A normal member, Get Long Time. Returning a long value.
    /*!
      \return a long value.
    */
    public long getLongTime()
    {
        long longTime = 365*(year-1);  // no leap years here!
        for (int i = 0; i < month-1; i++)
        {
            longTime += tabDaysPerMonth[i];
        }
        longTime += day;
        return longTime;
    }
    //! A normal member, Get JulianDay. Returning an integer value.
    /*!
      \return an integer value.
    */
    public int getJulianDay()
    {
        int JulianDay = 0;
        for (int i = 0; i < month - 1; i++)
        {
            JulianDay += tabDaysPerMonth[i];
        }
        JulianDay += day;
        return JulianDay;
    }
    //! A normal member, Get Year. Returning an integer value.
    /*!
      \return an integer value.
    */
    public int GetYear() { return year; }
    //! A normal member, Get Running Month. Returning an integer value.
    /*!
      \return an integer value.
    */
    public int GetRunningMonth()
    {
        int retVal = year * 12 + month;
        if (day > 15)
            retVal += 1;
        return retVal;
    }
    //! A normal member, Get Day. Returning an integer value.
    /*!
      \return an integer value.
    */
    public int GetDay() { return day; }
    //! A normal member, Get Month. Returning an integer value.
    /*!
      \return an integer value.
    */
    public int GetMonth() { return month; }
    //! A normal member, Set Year. Taking one argument.
    /*!
      \param aVal, an integer argument.
    */
    public void SetYear(int aVal) { 
        year = aVal; 
    }
    //! A normal member, increment oneDay. 
    /*!
      more details.
    */
    public void incrementOneDay()
    {
        if (day + 1 > tabDaysPerMonth[month-1])
        {
            day = 1;
            month++;
        }
        else
            day++;
        if (month > 12)
        {
            year++;
            month = 1;
        }
    }
    //! A normal member, Get Days in Month. Taking one argument and returning an integer value.
    /*!
      \param amonth, an integer argument.
      \return an integer value.
    */
    public int GetDaysInMonth(int amonth)
    {
        return tabDaysPerMonth[amonth-1];
    }
    //! A normal member, Write.
    /*!
      more details.
    */
    public void Write()
    {
        GlobalVars.Instance.writeInformationToFiles("day", "Day", "-", day, parens);
        GlobalVars.Instance.writeInformationToFiles("month", "Month", "-", month, parens);
        GlobalVars.Instance.writeInformationToFiles("year", "Year", "-", year, parens);
    }
    //! A normal member, Override string. Returning one string value.
    /*!
      \return a string. 
    */
    public override string  ToString()
    {
        return "day " + day.ToString() + " month " + month.ToString() + " year " + year.ToString(); 
    }
}
