using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MarketingMainPageForm
{
    internal class AnimationPlayer
    {
        string defoltName = "lein_animation\\lein_for_banner0";
        static string RunningPath = AppDomain.CurrentDomain.BaseDirectory;
        public string resorsesPath = string.Format("{0}Resources\\",
            Path.GetFullPath(Path.Combine(RunningPath, @"..\..\")));


        public async void PlayAnimation(PictureBox pictureBox)
        {
            int frameNumber = 0;
            string frameName = "";
            while (pictureBox != null)
            {

                frameName = frameNumber.ToString();
                if (frameNumber < 10)
                {
                    frameName = "00" + frameName;
                }
                else if (frameNumber < 100)
                {
                    frameName = "0" + frameName;
                }
                Image image = Image.FromFile(resorsesPath + defoltName + frameName + ".jpg");
                pictureBox.Image = image;
                frameNumber++;
                if (frameNumber == 181)
                {
                    frameNumber = 0;
                }
                Debug.WriteLine(frameName);
                await Task.Delay(100);

            }
        }


    }



}
