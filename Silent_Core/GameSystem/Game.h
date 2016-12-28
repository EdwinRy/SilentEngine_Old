#pragma once
#include <stdio.h>
#include <iostream>
#include <GL\glew.h>
#include <SDL.h>


namespace Silent {
		class Silent_Game {
		public:

			Silent_Game() {

			}

			enum DisplayType {
				Resizable = SDL_WINDOW_RESIZABLE,
				Hidden = SDL_WINDOW_BORDERLESS,
				Fullscreen = SDL_WINDOW_FULLSCREEN
			};

			enum GameAPI {
				OpenGL,
				Vulkan
			};

			SDL_Window* GameWindow;

			DisplayType WindowType = Resizable;

			GameAPI UsingAPI = OpenGL;

			int WindowWidth = 600;

			int WindowHeight = 400;

			int WindowPositionX = 0;
			int WindowPositionY = 0;

			bool DoubleBuffering = false;

			bool Vsync = false;

			char* WindowTitle = "Silent Game Engine";

			virtual void OnPreloadGame() {};
			virtual void OnLoadGame() {};
			virtual void OnUpdateGame() {};
			virtual void OnRenderGame() {};
			virtual void OnClosingGame() {};
			virtual void OnClosedGame() {};

			void MainGameLoop();

		private:

			SDL_GLContext glContext;

			bool GameRunning = false;
			bool FirstLevelLoaded = false;

			void OnLoad();
			void OnUpdate();
			void OnRender();
			void OnClosing();
			void OnClosed();

		};
}