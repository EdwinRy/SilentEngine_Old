using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using static Silent.Graphics.DisplayManager;

namespace Silent.Graphics
{
    public class Display : GameWindow
    {
        //Pre-set Display values with defaults
        DisplayBorder   m_border                    = DisplayBorder.Fixed;
        private int     m_width                     = 600;
        private int     m_height                    = 400;
        private int     m_updateFrameRate           = 60;
        private int     m_renderFrameRate           = 60;
        private string  m_title                     = "Silent Game Engine";
        private bool    m_usePresetUpdateFrequency  = false;
        private bool    m_usePresetRenderFrequency  = false;


        //The constructor takes in same values at the members with pre-set values
        public Display(
            DisplayBorder   border                      = DisplayBorder.Fixed,
            int             width                       = 600,
            int             height                      = 400,
            int             updateFrameRate             = 60,
            int             renderFrameRate             = 60,
            string          title                       = "Silent Game Engine",
            bool            usePresetUpdateFrequency    = false,
            bool            usePresetRenderFrequency    = false
            )
        {

            //Assign members to the parameter values of the constructor
            this.m_border           = border;
            this.m_width            = width;
            this.m_height           = height;
            this.m_title            = title;
            this.m_updateFrameRate  = updateFrameRate;
            this.m_renderFrameRate  = renderFrameRate;

        }

        public void EnterMainLoop()
        {

            if (m_border == DisplayBorder.Resizable)
                this.WindowBorder = WindowBorder.Resizable;

            if (m_border == DisplayBorder.Fixed)
                this.WindowBorder = WindowBorder.Fixed;

            if (m_border == DisplayBorder.Hidden)
                this.WindowBorder = WindowBorder.Hidden;

            this.Width      = m_width;
            this.Height     = m_height;
            this.Title      = m_title;



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

    }

}
