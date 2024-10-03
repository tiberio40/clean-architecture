using Core.DTOs;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IWeatherForecastService
    {
        public IEnumerable<WeatherForecast> Get();

        public IEnumerable<MarketingStatusEntity> Prueba();
    }
}
