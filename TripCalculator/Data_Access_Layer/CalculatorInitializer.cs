using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TripCalculator.Data_Access_Layer
{
    public class CalculatorInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<CalculatorContext>
    {
    }
}