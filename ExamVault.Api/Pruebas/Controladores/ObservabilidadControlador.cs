#if DEBUG
using Microsoft.AspNetCore.Mvc;
using Sentry;

namespace ExamVault.Api.Pruebas.Controladores
{
    [ApiController]
    [Route("api/observabilidad")]
    public class ObservabilidadControlador : ControllerBase
    {
        [HttpGet("error-tecnico")]
        public IActionResult GenerarErrorTecnico()
        {
            throw new InvalidOperationException("Prueba de integración EXAMVAULT: Sentry capturando error técnico.");
        }
    }
}
#endif