using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdisyonProg.Core.Helper
{
    public class Globalislemler
    {
        public void TryCatchKullan(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                string ExStr = Newtonsoft.Json.JsonConvert.SerializeObject(ex);
                var logger = NLog.LogManager.GetCurrentClassLogger();
                logger.Log(LogLevel.Error, ExStr);
            }
        }

        public static bool NullControl(Form form)
        {
            bool flag = true;

            foreach (Control item in form.Controls)
            {
                if (item is TextBox)
                {
                    if (item.Text == "" || item.Text == null)
                    {
                        flag = false;
                        break;
                    }
                }
            }
            return flag;
        }
    }
}
