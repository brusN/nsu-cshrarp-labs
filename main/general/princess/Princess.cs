using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace lab2;

public class Princess : BackgroundService
{
    private readonly ILogger<Princess> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IHostApplicationLifetime _hostApplicationLifetime;
    private Friend? _friend;
    private Hall? _hall;
    private ContenderGenerator? _contenderGenerator;
    private IPrincessStrategy? _strategy;

    public Princess(IServiceProvider serviceProvider, IHostApplicationLifetime hostApplicationLifetime,
        ILogger<Princess> logger)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _hostApplicationLifetime = hostApplicationLifetime;
    }

    private Contender? ChooseGroom()
    {
        return _strategy.ChooseGroom(_hall, _friend);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // DI
        var scope = _serviceProvider.CreateScope();
        _hall = scope.ServiceProvider.GetRequiredService<Hall>();
        _friend = scope.ServiceProvider.GetRequiredService<Friend>();
        _friend.PrincessRejectedContenders = _hall.RejectedContenders;
        _contenderGenerator = scope.ServiceProvider.GetRequiredService<ContenderGenerator>();
        _strategy = scope.ServiceProvider.GetRequiredService<ClassicalPrincessStrategy>();

        var contenders = _contenderGenerator.GenerateContenders();
        _hall.Contenders = new List<Contender>(contenders);
        var groom = ChooseGroom();
        var happinessLvl = HappinessCalculator.CalcPrincessHappinessLvl(contenders, groom);
        _logger.LogInformation("Happiness lvl: {0}", happinessLvl);
        
        _hostApplicationLifetime.StopApplication();
        return Task.CompletedTask;
    }
}