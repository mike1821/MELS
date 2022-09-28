using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using System.Diagnostics;
using System.Globalization;
/*! \namespace AnimalChange */
namespace AnimalChange
{
    class Program
    {
        //! A normal member, Main function. Taking one argument.
        /*!
          \param args, a string array argument.
        */
        ///args[0] and args[1] are farm number and scenario number respectively
        ///if args[2] is 1, the energy demand of livestock must be met (0 is used when reporting current energy balance of livestock to users)
        ///if args[3] is 1, the crop model requires the modelled production of grazed DM to match the expected production. If =0, grazed DM will be imported if there is a deficit
        ///if args[4] is -1, the model will spinup for the years in the spinupYearsBaseLine parameter then run a baseline scenario and generate a Ctool data transfer file
        ///if args[4] is a positive integer and the spinupYearsNonBaseLine parameter is zero, the model will read Ctool data from the Ctool data transfer file. If spinupYearsNonBaseLine is not zero, the model will spin up for spinupYearsNonBaseLine years then run the scenario
        /// So to just calculate the animal production, set the farm file in system.xml to "farm.xml" and the args to <farm number> <scenario number> "0" "0" "-1"
        
        static void Main(string[] args)
        {
            model mod = new model();
            mod.run(args);

        }

    }

}
