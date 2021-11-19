using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMICommon2;

namespace AMISystemManagement
{
    public class AMISystemManagementService : IAgregatorAndManagementJob
    {
         AddToSQL SQLWorker = new AddToSQL();

        public bool Posalji(AMIAgregatorStruct struktura)
        {
            try
            {
                SQLWorker.AddNewStructToSQL(struktura);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            
        }
    }
}
