using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DacpacContract
{
    [ServiceContract]
    public interface IDacpacService
    {
        [OperationContract]
        byte[] GetDacpac(string id, string version);

        [OperationContract]
        void SetDacpac(string id, string version, byte[] dacpac);
    }
}
