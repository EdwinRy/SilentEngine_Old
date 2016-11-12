#pragma once

#include <GL\glew.h>
#include <SDL.h>
#include <SDL_opengl.h>

namespace Silent_Core {
	namespace GameSystem {
		public ref class GL_Game
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
			GL_Game();

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


		};
	}
}