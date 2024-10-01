using Application.Interfaces.Campaign;

namespace Application;

public class JobMethodsService
{
    public readonly IMarketingService _marketingService;

    public JobMethodsService(IMarketingService marketingService) { 
        _marketingService = marketingService;
    }


    public void MiMetodoRecurrente()
    {
        // Lógica del trabajo recurrente
        Console.WriteLine("Trabajo recurrente personalizado ejecutado!");
    }

    public void MiMetodoEstatico()
    {
        // Lógica del trabajo estático
        Console.WriteLine("Trabajo estático ejecutado!");
    }

    public async Task UpdateTemplateStatus()
    {
        await _marketingService.UpdateTemplateStatus();
    }

    public async Task SendingMessages()
    {
        await _marketingService.SendingMessages();
    }
}
