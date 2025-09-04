using EwkQxObd.Core.Model;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace EwkQxObd.WebApi.Data
{
    public class ContractObjectDataSync
    {

       
        private async Task createOrFetchPerson(EwkIqxObdContext context, EqoContract Contract)
        {
            var empEmail = Contract.EmployeeResponsible!.EmailAddress;

            if (!string.IsNullOrEmpty(empEmail))
            {
                var employeResponsible = await context.EqoContactInfo.FirstOrDefaultAsync(
                    emp => emp.EmailAddress == empEmail
                );

                if (employeResponsible != default)
                {
                    Contract.EmployeeResponsible = default;
                    Contract.EmployeeResponsibleId = employeResponsible.Id;

                }
            }
            var customerEmail = Contract.CustomerContact!.EmailAddress;

            if (!string.IsNullOrEmpty(customerEmail))
            {
                var customerEmailFound = await context.EqoContactInfo.FirstOrDefaultAsync(
                    cust => cust.EmailAddress == customerEmail
                );

                if (customerEmailFound != default)
                {
                    Contract.CustomerContact = default;
                    Contract.CustomerContactId = customerEmailFound.Id;

                }
            }
        }


        public async Task<ContractObjectDataSyncResult> SyncSingle(
            EwkIqxObdContext context, 
            EqoContractObject contractObject)
        {
            
            var syncResult = new ContractObjectDataSyncResult();
            if (contractObject.ShipTo != default)
            {
                var shipTo = await context.EqoAccount.FirstOrDefaultAsync(acc => acc.PartnerId == contractObject.ShipTo.PartnerId);
                if (shipTo != default)
                {
                    contractObject.ShipTo = default;
                    contractObject.ShipToId = shipTo.Id;
                }
            }
            else
            {
                syncResult.MissingState |= ContractObjectDataMissing.ShipTo;
            }


            if (contractObject.Contract != default)
            {
                var contract = await context.EqoContract.FirstOrDefaultAsync(con => con.ContractNumber == contractObject.Contract.ContractNumber);

                if (contract != default)
                {
                    contractObject.ContractId = contract.Id;
                }

                await createOrFetchPerson(context, contractObject.Contract);
            }
            else
            {
                syncResult.MissingState |= ContractObjectDataMissing.Contract;
            }

            return syncResult;



        }
    }
}
