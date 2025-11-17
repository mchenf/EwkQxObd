using EwkQxObd.Core.Model.Views;
using EwkQxObd.WebApi.Data;
using EwkQxObd.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO.Enumeration;

namespace EwkQxObd.WebApi.Controllers
{
    [Route("[Controller]")]
    public class SyngioController : Controller
    {
        private EwkIqxObdContext _context;

        public SyngioController(EwkIqxObdContext ctx)
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
                from inst in _context.Syngio
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
                    from inst in _context.Syngio
                    where inst.System == sys
                    select inst
                ).ToListAsync();

                var qryNetwork = (
                    from inst in qryInstruments
                    select KeyValuePair.Create(inst.NetworkId, inst.NetworkName)
                ).Distinct().ToList();

                foreach (var netw in qryNetwork)
                {

                    string v = "Unassigned";
                    if (netw.Key is not null)
                    {
                        v = netw.Value;
                    }
                    systemToAdd.Networks.Add(
                        new GvsNetwork
                        {
                            NetworkId = netw.Key,
                            NetworkName = v,
                            Instruments = [.. (from inst in qryInstruments
                                           where inst.NetworkId == netw.Key
                                           select inst)]
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
                from a in _context.Syngio
                where a.SerialNumber == SerialNumber
                select a
            ).FirstOrDefaultAsync();

            if (network is null) {

                return NoContent();

            }


            var result = await (
                from a in _context.Syngio
                where a.NetworkId == network.NetworkId && a.System == network.System
                select a
            ).ToListAsync();

            return Ok(result);
        }

        [Route("[Action]/{id}")]
        public async Task<IActionResult> Details([FromRoute] long id)
        {
            var inst = await (
                from i in _context.Syngio
                where i.Id == id
                select i
            ).FirstOrDefaultAsync();

            if (inst is null)
            {

                return NoContent();

            }

            return View(inst);
        }


        [Route("[Action]")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchPost(Syngio search)
        {
            var inst = await (
                from i in _context.Syngio
                where i.SerialNumber == search.SerialNumber
                select i
            ).FirstOrDefaultAsync();


            return View(nameof(Search), inst);
        }

        [Route("[Action]")]
        [HttpGet]
        public IActionResult Search(Syngio search)
        {
            return View(search);
        }

        [Route("[Action]")]
        [HttpGet]
        public IActionResult Upload()
        {
            return View(new NetworkCsvUploadViewModel { FromWebCall = true, SelectedSystem = "Pacific" });
        }

    }
}
