using System;

namespace Global.Managers
{
    public class DataManager : BaseManager
    {
        public override Type ManagerType => typeof(DataManager);

        protected override bool OnInit()
        {
            return true;
        }
    }
}