using Microsoft.Extensions.DependencyInjection;

namespace WeekNumber.ViewModels
{
    /// <summary>
    /// Wird in App.xaml als Ressource genutzt
    /// </summary>
    internal class ViewModelLocator
    {
        #region MainViews

        public TrayViewModel TrayViewModel => App.ServiceProvider.GetRequiredService<TrayViewModel>();

        #endregion MainViews
    }
}