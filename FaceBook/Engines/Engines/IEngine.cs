using OpenQA.Selenium.Remote;

namespace Engines.Engines
{
    public interface IEngine<TModel, TResult>
    {
        TResult Execute(RemoteWebDriver driver, TModel model);
    }
}
