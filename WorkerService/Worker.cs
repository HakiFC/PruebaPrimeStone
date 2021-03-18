using Common.Data;
using Common.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerService
{
    public class Worker : IHostedService, IDisposable
    {
        private int executionCount = 0;
        private readonly ILogger<Worker> _logger;
        private Timer _timer;
        private readonly IServiceScopeFactory scopeFactory;

        public Worker(ILogger<Worker> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            this.scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Ejecutándose.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(10));

            using (var scope = scopeFactory.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<PruebaDBContext>();

                DoWork registro = new DoWork();
                registro.EstaBorrado = false;
                registro.Evento = "Start";
                registro.Fecha = DateTime.Now;

                _context.DoWork.AddAsync(registro);
                _context.SaveChangesAsync();
            }

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            var count = Interlocked.Increment(ref executionCount);

            _logger.LogInformation(
                "Guardado: {Count}", count);

            using (var scope = scopeFactory.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<PruebaDBContext>();
                DoWork registro = new DoWork();
                registro.EstaBorrado = false;
                registro.Evento = "Ejecutándose {Count}";
                registro.Fecha = DateTime.Now;

                _context.DoWork.AddAsync(registro);
                _context.SaveChangesAsync();
            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Deteniendo.");

            _timer?.Change(Timeout.Infinite, 0);

            using (var scope = scopeFactory.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<PruebaDBContext>();
                DoWork registro = new DoWork();
                registro.EstaBorrado = false;
                registro.Evento = "Stop";
                registro.Fecha = DateTime.Now;

                _context.DoWork.AddAsync(registro);
                _context.SaveChangesAsync();
            }

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
