using EwkQxObd.Core.Model.Views;
using EwkQxObd.WebApi.Data;
using EwkQxObd.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO.Enumeration;

namespace EwkQxObd.WebApi.Controllers
{
    [Route("[Controller]")]
    public class SysnetinstController : Controller
    {
        private EwkIqxObdContext _context;

        public SysnetinstController(EwkIqxObdContext ctx)
        {
            _context = ctx;
        }
        public async Task<IActionResult> Index()
        {
            GrouppedVwSysnetinstorg groupped = new()
            {
                Systems = []
            };

            var qrySystem = await (
                from inst in _context.VwSysnetinstorg
                select inst.System
            ).Distinct().ToListAsync();

            foreach (var sys in qrySystem)
            {
                var systemToAdd = new GvsSystem
                {
                    SystemName = sys,
                    Networks = []
                };

                var qryInstruments = await (
                    from inst in _context.VwSysnetinstorg
                    where inst.System == sys
                    select inst
                ).ToListAsync();

                var qryNetwork = (
                    from inst in qryInstruments
                    select KeyValuePair.Create(inst.NetworkId, inst.NetworkName)
                ).ToList();

                foreach (var netw in qryNetwork)
                {
                    systemToAdd.Networks.Add(
                        new GvsNetwork
                        {
                            NetworkId = netw.Key,
                            NetworkName = netw.Value,
                            Instruments = qryInstruments
                        }
                    );
                }


                groupped.Systems.Add(systemToAdd);
            }

            return View(groupped);
        }

        [Route("[Action]/{SerialNumber}")]
        public async Task<IActionResult> GetByInstrumentSN([FromRoute] string SerialNumber)
        {
            var network = await (
                from a in _context.VwSysnetinstorg
                where a.SerialNumber == SerialNumber
                select a
            ).FirstOrDefaultAsync();

            if (network is null) {

                return NoContent();

            }


            var result = await (
                from a in _context.VwSysnetinstorg
                where a.NetworkId == network.NetworkId && a.System == network.System
                select a
            ).ToListAsync();

            return Ok(result);
        }
    }
}
