namespace BaseScripts
{
    public abstract class BaseController
    {
        public bool IsActive { get; private set; }

        /// <summary>
        /// Включаем контроллер
        /// </summary>
        public virtual void On()
        {
            IsActive = true;
        }

        /// <summary>
        /// Выключаем контроллер
        /// </summary>
        public virtual void Off()
        {
            IsActive = false;
        }

        public virtual void ControllerUpdate(float time) { }

        public virtual void ControllerLateUpdate(float time) { }
    }
}

