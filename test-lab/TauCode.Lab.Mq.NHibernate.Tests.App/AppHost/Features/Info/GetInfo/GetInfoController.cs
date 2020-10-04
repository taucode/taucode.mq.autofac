using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TauCode.Lab.Mq.NHibernate.Tests.App.AppHost.Features.Info.GetInfo
{
    [ApiController]
    public class GetInfoController : ControllerBase
    {
        private readonly IAppStartup _appStartup;
        public GetInfoController(IAppStartup appStartup)
        {
            _appStartup = appStartup;
        }

        [HttpGet]
        [Route("api/info")]
        public Task<IActionResult> GetInfo()
        {
            var info = new
            {
                _appStartup.ConnectionString,
                _appStartup.TempDbFilePath,
            };

            IActionResult result = this.Ok(info);
            return Task.FromResult(result);
        }
    }
}
