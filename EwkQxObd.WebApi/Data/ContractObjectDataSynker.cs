using EwkQxObd.Core.Model;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace EwkQxObd.WebApi.Data
{
    public class ContractObjectDataSynker
    {
        private readonly EwkIqxObdContext _context;
        private readonly EqoContractObject _contractObj;
        private readonly EqoContract? _contract;

        public ContractObjectDataSynker(
            EwkIqxObdContext Context,
            EqoContractObject ContractObj)
        {
            _context = Context;
            _contractObj = ContractObj;
            _contract = ContractObj.Contract;
        }

        private async Task CreateOrFetchPerson( 
            Func<EqoContract, EqoContactInfo?> EntitySelector,
            Func<EqoContract, long?> IdSelector)
        {
            if (_contract == default)
            {
                return;
            }
            var cifPassed = EntitySelector.Invoke(_contract);
            if (cifPassed == null)
            {
                return;
            }

            var cifPassedEmail = cifPassed.EmailAddress;

            if (!string.IsNullOrEmpty(cifPassedEmail))
            {
                var cifFound = await _context.EqoContactInfo.FirstOrDefaultAsync(
                    emp => emp.EmailAddress == cifPassedEmail
                );

                if (cifFound != null)
                {
                    cifPassed = null;
                    var id = IdSelector.Invoke(_contract);

                    id = cifFound.Id;
                }
            }
        }

        private async Task CreateOrFetchPersonForBothProps()
        {
            //Preprocess logic. If the contact has id, fill the id
            //The contact get from the js calls.
            // remove obj, keep outter id
            // if the contact has id = 0,
            // create new, or do not sync if no email

            if (_contract == null)
            {
                return;
            }
            await CreateOrFetchPerson( 
                c => c.EmployeeResponsible,
                c => c.EmployeeResponsibleId);


            await CreateOrFetchPerson( 
                c => c.CustomerContact,
                c => c.CustomerContactId);

            if (_contract.EmployeeResponsible != null && _contract.EmployeeResponsible.Id > 0)
            {
                    _contract.EmployeeResponsibleId = _contract.EmployeeResponsible.Id;
                    _contract.EmployeeResponsible = null;
            }
            if (_contract.EmployeeResponsible != null && string.IsNullOrEmpty(_contract.EmployeeResponsible.FullName))
            {
                _contract.EmployeeResponsibleId = null;
                _contract.EmployeeResponsible = null;
            }

            if (_contract.CustomerContact != null && _contract.CustomerContact.Id > 0)
            {
                _contract.CustomerContactId = _contract.CustomerContact.Id;
                _contract.CustomerContact = null;
            }
            if (_contract.CustomerContact != null && string.IsNullOrEmpty(_contract.CustomerContact.FullName))
            {
                _contract.CustomerContactId = null;
                _contract.CustomerContact = null;
            }
        }


        public async Task<ContractObjectDataSyncResult> SyncSingleCobj()
        {
            
            var syncResult = new ContractObjectDataSyncResult();
            if (_contractObj.ShipTo != default)
            {
                var shipTo = await _context.EqoAccount.FirstOrDefaultAsync(acc => acc.PartnerId == _contractObj.ShipTo.PartnerId);
                if (shipTo != default)
                {
                    _contractObj.ShipTo = default;
                    _contractObj.ShipToId = shipTo.Id;
                }
            }
            else
            {
                syncResult.MissingState |= ContractObjectDataMissing.ShipTo;
            }


            if (_contractObj.Contract != default)
            {
                //if the original contract is not default,
                // make null and sync only id
                var contract = await _context.EqoContract.FirstOrDefaultAsync(con => con.ContractNumber == _contractObj.Contract.ContractNumber);

                if (contract != default)
                {
                    _contractObj.Contract = default;
                    _contractObj.ContractId = contract.Id;
                }
                else
                {
                    await CreateOrFetchPersonForBothProps();
                }
            }
            else
            {
                syncResult.MissingState |= ContractObjectDataMissing.Contract;
            }

            return syncResult;



        }
    }
}
