using System.Drawing;
using System.Windows.Forms;

namespace CAS.UI.UIControls
{
    ///<summary>
    ///</summary>
    internal partial class WaitingControl : PictureBox
    {
        private int currentImage = 0;

        ///<summary>
        ///</summary>
        public WaitingControl()
        {
            InitializeComponent();
            SizeMode = PictureBoxSizeMode.Zoom;
        }

        /// <summary>
        /// ���������� �������� ����� ������ �����������
        /// </summary>
        public int Delay
        {
            get
            {
                return timer.Interval;
            }
            set
            {
                timer.Interval = value;
            }
        }

        /// <summary>
        /// ������������� �����������
        /// </summary>
        public void Start()
        {
            currentImage = 0;
            timer.Start();
        }

        /// <summary>
        /// �������������
        /// </summary>
        public void Pause()
        {
            timer.Stop();
        }

        /// <summary>
        /// ����������
        /// </summary>
        public void Continue()
        {
            timer.Start();
        }

        /// <summary>
        /// ����������
        /// </summary>
        public void Stop()
        {
            timer.Stop();
            Image = null;
        }

        private void timer_Tick(object sender, System.EventArgs e)
        {
            Image = NextImage;
        }

        private Image NextImage
        {
            get
            {
                currentImage = (currentImage + 1)%imageList.Images.Count;
                return imageList.Images[currentImage];
            }
        }
    }
}