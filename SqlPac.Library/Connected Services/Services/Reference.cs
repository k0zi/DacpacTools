﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SqlPac.Library.Services {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Services.IDacpacService")]
    public interface IDacpacService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDacpacService/GetDacpac", ReplyAction="http://tempuri.org/IDacpacService/GetDacpacResponse")]
        byte[] GetDacpac(string id, string version);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDacpacService/GetDacpac", ReplyAction="http://tempuri.org/IDacpacService/GetDacpacResponse")]
        System.Threading.Tasks.Task<byte[]> GetDacpacAsync(string id, string version);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDacpacService/SetDacpac", ReplyAction="http://tempuri.org/IDacpacService/SetDacpacResponse")]
        void SetDacpac(string id, string version, byte[] dacpac);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDacpacService/SetDacpac", ReplyAction="http://tempuri.org/IDacpacService/SetDacpacResponse")]
        System.Threading.Tasks.Task SetDacpacAsync(string id, string version, byte[] dacpac);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IDacpacServiceChannel : SqlPac.Library.Services.IDacpacService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DacpacServiceClient : System.ServiceModel.ClientBase<SqlPac.Library.Services.IDacpacService>, SqlPac.Library.Services.IDacpacService {
        
        public DacpacServiceClient() {
        }
        
        public DacpacServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public DacpacServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DacpacServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DacpacServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public byte[] GetDacpac(string id, string version) {
            return base.Channel.GetDacpac(id, version);
        }
        
        public System.Threading.Tasks.Task<byte[]> GetDacpacAsync(string id, string version) {
            return base.Channel.GetDacpacAsync(id, version);
        }
        
        public void SetDacpac(string id, string version, byte[] dacpac) {
            base.Channel.SetDacpac(id, version, dacpac);
        }
        
        public System.Threading.Tasks.Task SetDacpacAsync(string id, string version, byte[] dacpac) {
            return base.Channel.SetDacpacAsync(id, version, dacpac);
        }
    }
}
