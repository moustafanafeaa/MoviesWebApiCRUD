using System.Diagnostics;

namespace movies.MiddleWares
{
    public class ProfilingMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ProfilingMiddleWare> _logger;

        //RequestDelegate => ref to next middleware (to call next one)
        //Ilogger => to write more info to me about code and depuging
        public ProfilingMiddleWare(RequestDelegate next,ILogger<ProfilingMiddleWare> logger)
        {
            _next = next;
            _logger = logger;
        }


        //must be method Invoke in code
        //HttpContext => all info about req and res (importantttt)
        //Task => procces run in one place in proccessor (not in main thrid) =>> wait or not i decide

        public async Task Invoke(HttpContext context)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Start();
            await _next.Invoke(context);
            stopwatch.Stop();
            _logger.LogInformation($"request took {stopwatch.ElapsedMilliseconds}ms");
        }
    }
}
