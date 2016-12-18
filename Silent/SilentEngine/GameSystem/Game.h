#pragma once
#include<iostream>
#include "Level.h"
#include<glew.h>
#include"SDL.h"

namespace Silent {

	class silent_Game {
	public:

		silent_Game();

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

	private:
		silent_Level Levels[25];
		silent_Level CurrentLevel;
		
		virtual void OnLoadGame() = 0;
		virtual void OnUpdateGame() = 0;
		virtual void OnRenderGame() = 0;
		virtual void OnClosingGame() = 0;
		virtual void OnClosedGame() = 0;

		void OnLoad();
		void OnUpdate();
		void OnRender();
		void OnClosing();
		void OnClosed();
	};

}