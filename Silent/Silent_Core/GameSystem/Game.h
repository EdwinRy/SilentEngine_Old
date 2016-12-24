#pragma once
#include <GL\glew.h>
#include<iostream>
#include "Level.h"
#include"SDL.h"

namespace Silent {

	class Silent_Game {
	public:
		
		Silent_Game();

		enum DisplayBorder {
			Resizable = SDL_WINDOW_RESIZABLE,
			Fixed = SDL_WINDOW_SHOWN,
			Hidden = SDL_WINDOW_BORDERLESS,
			Fullscreen = SDL_WINDOW_FULLSCREEN_DESKTOP
		};

		enum GameAPI {
			OpenGL,
			Vulkan,
			OpenGLES,	
		};

		SDL_Window* GameWindow;

		DisplayBorder WindowBorder = DisplayBorder::Fixed;

		//The width of the game window
		int WindowWidth = 600;

		//The height of teh game window
		int WindowHeight = 400;

		int WindowPositionX = SDL_WINDOWPOS_CENTERED;
		int WindowPositionY = SDL_WINDOWPOS_CENTERED;

		bool UseFramerateCap = false;
		int FramerateCap = 60;

		bool GameInitializedProperly;

		bool GameRunning = true;

		char* WindowTitle = "Silent Game Engine";

		GameAPI UsedGameAPI = GameAPI::OpenGL;
		
		void MainLoop();

		virtual void OnLoadGame() = 0;
		virtual void OnUpdateGame() = 0;
		virtual void OnRenderGame() = 0;
		virtual void OnClosingGame() = 0;
		virtual void OnClosedGame() = 0;

	private:
		Silent_Level Levels[25];
		Silent_Level CurrentLevel;

		void OnLoad();
		void OnUpdate();
		void OnRender();
		void OnClosing();
		void OnClosed();
	public:
		void LoadLevel(Silent_Level& levelToLoad);
		void SetCurrentLevel(Silent_Level& newCurrentLevel);
	};

}