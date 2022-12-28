using lab2.friend;
using lab2.hall;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace lab2;

public class PrincessSimulatorService : BackgroundService
{
    private const int CountAttempts = 100;
    private readonly ILogger<PrincessSimulator> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IHostApplicationLifetime _hostApplicationLifetime;
    private readonly LaunchServiceConfig _config;

    private IHall? _hall;
    private IFriend? _friend;
    private IPrincessStrategy? _strategy;
    private IContenderGenerator? _generator;
    private IPrincessSimulator? _princessSimulator;

    private DataContext _context;

    private int _totalHappinessLvl;

    public PrincessSimulatorService(IServiceProvider serviceProvider, ILogger<PrincessSimulator> logger,
        IHostApplicationLifetime hostApplicationLifetime, LaunchServiceConfig config, DataContext context)
    {
        _logger = logger;
        _hostApplicationLifetime = hostApplicationLifetime;
        _serviceProvider = serviceProvider;
        _config = config;
        _context = context;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Princess emulator started");
        var scope = _serviceProvider.CreateScope();
        _princessSimulator = scope.ServiceProvider.GetRequiredService<PrincessSimulator>();

        if (_config.IsDefaultConfig())
        {
            _logger.LogInformation("No passed attempt names. Generating 100 attempts and saving in DB...");
            _generator = scope.ServiceProvider.GetRequiredService<ContenderGenerator>();
            _princessSimulator.GenerateAttempts(CountAttempts, _generator, _context);
        }
        else
        {
            _logger.LogInformation("Launched simulation for passed attempts");
            int totalHappinessLvl = 0;
            int skippedAttempts = 0;
            foreach (var attemptName in _config.AttemptNames)
            {
                _hall = scope.ServiceProvider.GetRequiredService<Hall>();
                _friend = scope.ServiceProvider.GetRequiredService<Friend>();
                _friend.PrincessRejectedContenders = _hall.RejectedContenders;
                _strategy = scope.ServiceProvider.GetRequiredService<ClassicalPrincessStrategy>();
                int happinessLvl = _princessSimulator.LaunchSavedAttempt(attemptName, _hall, _friend, _strategy, _context);
                if (happinessLvl != -1)
                {
                    totalHappinessLvl += happinessLvl;
                    _logger.LogInformation("Happiness lvl for attempt {0} is {1}", attemptName, happinessLvl);
                }
                else
                {
                    _logger.LogWarning("Attempt {0} skipped, because not found in DB", attemptName);

                    skippedAttempts += 1;
                }
            }
            _logger.LogInformation("Average happiness lvl is {0}", (float) totalHappinessLvl / (_config.AttemptNames.Count - skippedAttempts));
        }
        
        _hostApplicationLifetime.StopApplication();
        return Task.CompletedTask;
    }
}