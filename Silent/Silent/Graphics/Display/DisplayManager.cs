using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Silent.Graphics;


namespace Silent.Graphics
{
    
    public class DisplayManager
    {

        Display display;

        DisplayBorder   m_border                    = DisplayBorder.Fixed;
        private int     m_width                     = 600;
        private int     m_height                    = 400;
        private int     m_updateFrameRate           = 60;
        private int     m_renderFrameRate           = 60;
        private string  m_title                     = "Silent Game Engine";
        private bool    m_usePresetUpdateFrequency  = false;
        private bool    m_usePresetRenderFrequency  = false;

        public Display CreateDisplay()
        {

            return new Display(
                border                      : m_border,
                width                       : m_width,
                height                      : m_height,
                updateFrameRate             : m_updateFrameRate,
                renderFrameRate             : m_renderFrameRate,
                title                       : m_title,
                usePresetUpdateFrequency    : m_usePresetUpdateFrequency,
                usePresetRenderFrequency    : m_usePresetRenderFrequency);
        }

        public enum DisplayBorder
        {
            Resizable = 0,
            Fixed = 1,
            Hidden = 2
        }

        //GETTERS

        //Return width of the display
        public int getDisplayWidth()
        {
            return m_width;
        }

        //Return height of the display
        public int getDisplayHeight()
        {
            return m_width;
        }

        //Return update frequency of the display 
        public int getDisplayUpdateFrequency()
        {
            return m_updateFrameRate;
        }

        //Return render frequency of the display
        public int getDisplayRenderFrequency()
        {
            return m_renderFrameRate;
        }

        //Return title of the display
        public string getDisplayTitle()
        {
            return m_title;
        }

        //Return border type of the display
        public DisplayBorder getBorder()
        {
            return m_border;
        }



        //SETTERS

        //Set width of the display
        public void setDisplayWidth(int width)
        {
            m_width = width;
        }

        //Set height of the display
        public void setDisplayHeight(int height)
        {
            m_height = height;
        }

        //Set update frequency of the display
        public void setDisplayUpdateFrequency(int updateFrequency)
        {
            m_updateFrameRate = updateFrequency;
        }

        //Set render frequency of the display
        public void setDisplayRenderFrequency(int renderFrequency)
        {
            m_renderFrameRate = renderFrequency;
        }

        //Set title of the display
        public void setDisplayTitle(string title)
        {
            m_title = title;
        }

        //Set border type of the display
        public void setBorder(DisplayBorder border)
        {
            m_border = border;
        }

    }
}
