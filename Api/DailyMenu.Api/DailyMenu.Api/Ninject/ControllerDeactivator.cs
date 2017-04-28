using System;

namespace DailyMenu.Api.Ninject
{
    public class ControllerDeactivator : IDisposable
    {
        private readonly Action release;

        public ControllerDeactivator(Action release)
        {
            this.release = release;
        }

        public void Dispose()
        {
            this.release();
        }
    }
}