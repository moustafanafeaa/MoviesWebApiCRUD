namespace movies.MiddleWares
{
    public class RateLimitingMiddleware
    {

        private static int _counter = 0;
        private static DateTime _LastRequestDate = DateTime.Now;
        private readonly RequestDelegate next;

        public RateLimitingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            _counter++;
            if(DateTime.Now.Subtract(_LastRequestDate).Seconds > 10) 
            {
                _counter = 1;
                _LastRequestDate= DateTime.Now;
                await next(context);
            }
            else
            {
                if (_counter > 5)
                {
                    _LastRequestDate = DateTime.Now;
                    await context.Response.WriteAsync("rate limit exceeded");
                }
                else
                {
                    _LastRequestDate = DateTime.Now;
                    await next(context);
                    
                }
            }
        }



    }
}
