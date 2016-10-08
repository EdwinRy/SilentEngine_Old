using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Silent.Graphics
{
    public class Display : GameWindow
    {

        private int     m_width                     = 600;
        private int     m_height                    = 400;
        private int     m_updateFrameRate           = 60;
        private int     m_renderFrameRate           = 60;
        private string  m_title                     = "Silent Game Engine";
        private bool    m_resizable                 = false;
        private bool    m_hiddenBorder              = false;
        private bool    m_usePresetUpdateFrequency  = false;
        private bool    m_usePresetRenderFrequency  = false;

        public Display(
            int     width                       = 600, 
            int     height                      = 400,             
            int     updateFrameRate             = 60,
            int     renderFrameRate             = 60,
            string  title                       = "Silent Game Engine",
            bool    resizable                   = false,
            bool    hiddenBorder                = false,
            bool    usePresetUpdateFrequency    = false,
            bool    usePresetRenderFrequency    = false
            )
        {
            this.m_width            = width;
            this.m_height           = height;
            this.m_title            = title;
            this.m_resizable        = resizable;
            this.m_hiddenBorder     = hiddenBorder;
            this.m_updateFrameRate  = updateFrameRate;
            this.m_renderFrameRate  = renderFrameRate;

        }

        public void EnterMainLoop()
        {

            this.Width  = m_width;
            this.Height = m_height;
            this.Title  = m_title;
            


            if (m_usePresetUpdateFrequency)
            {

                if (m_usePresetRenderFrequency)
                {
                    this.Run(m_updateFrameRate, m_renderFrameRate);
                }

                else
                {
                    this.Run(m_updateFrameRate);
                }
            }
            else
                this.Run();

            if (!m_usePresetUpdateFrequency && m_usePresetRenderFrequency)
            {
                Console.WriteLine("Update frequency required in order to change render frequency");
                this.Run();
            }

        }

        public bool getResizable()
        {
            return m_resizable;
        }  


    }
}
