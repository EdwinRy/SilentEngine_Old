#pragma once
#include <GL\glew.h>
#include <SDL.h>
#include <SDL_opengl.h>


namespace Silent_Core {
	namespace GameSystem {
		public class Game
		{
		private:
			//SDL screen (window)
			SDL_Window* m_screen;

			//OpenGL context
			SDL_GLContext m_glContext;

			char* m_title;
			char* m_windowType;
			int m_borderType;

			int m_windowWidth;
			int m_windowHeight;
			int m_windowPosX;
			int m_windowPosY;

			bool m_running = true;

		public:
			Game();

			virtual void OnLoadGame() = 0;
			virtual void OnUpdateGame() = 0;
			virtual void OnRenderGame() = 0;
			virtual void OnClosingGame() = 0;
			virtual void OnClosedGame() = 0;
			
		private:
			void setUpOpenGL();

			void OnLoad();
			void OnUpdate();
			void OnRender();
			void OnClosing();
			void OnClosed();

		//Getters and Setters
		public:

			//Set the title of the window
			void setWindowTitle(char* title) { m_title = title; }

			//Set the width of the window
			void setWindowWidth(int width) { m_windowWidth = width; }

			//Set the hight of the window
			void setWindowHeight(int height) { m_windowHeight = height; }

			//Set the X coordinate of the window
			void setWindowPosX(int windowPosX) { m_windowPosX = windowPosX; }

			//Set the y coordinate of the window
			void setWindowPosY(int windowPosY) { m_windowPosY = windowPosY; }

			char* getWindowTitle() { return m_title; }

			int getWindowWidth() { return m_windowWidth; }

			int getWindowHeight() { return m_windowHeight; }

			int getWindowPosX() { return m_windowPosX; }

			int getWindowPosY() { return m_windowPosY; }
		};
	}
}