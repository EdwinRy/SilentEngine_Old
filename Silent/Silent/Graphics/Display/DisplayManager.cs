﻿using OpenTK;
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

        private int     m_width                     = 600;
        private int     m_height                    = 400;
        private int     m_updateFrameRate           = 60;
        private int     m_renderFrameRate           = 60;
        private string  m_title                     = "Silent Game Engine";
        private bool    m_resizable                 = false;
        private bool    m_hiddenBorder              = false;
        private bool    m_usePresetUpdateFrequency  = false;
        private bool    m_usePresetRenderFrequency  = false;

        public DisplayManager()
        {

        }

        public Display CreateDisplay()
        {

            return new Display(
                width                       : m_width,
                height                      : m_height,
                updateFrameRate             : m_updateFrameRate,
                renderFrameRate             : m_renderFrameRate,
                title                       : m_title,
                resizable                   : m_resizable,
                usePresetUpdateFrequency    :m_usePresetUpdateFrequency,
                usePresetRenderFrequency    :m_usePresetRenderFrequency);
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

        //Get resizability of the display
        public bool getDisplayResizable()
        {
            return m_resizable;
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

        //Set resizability of the display
        public void setDisplayResizable(bool resizable)
        {
            m_resizable = resizable;
        }

    }
}
