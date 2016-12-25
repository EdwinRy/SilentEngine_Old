#include "Game.h"

Silent::Silent_Game::Silent_Game()
{
	if (!SDL_Init(SDL_INIT_VIDEO)) {
		this->GameInitializedProperly = false;
	}
}

void Silent::Silent_Game::MainLoop()
{
	this->GameWindow = SDL_CreateWindow(
		WindowTitle,
		WindowPositionX, 
		WindowPositionY,
		WindowWidth,
		WindowHeight,
		WindowBorder && SDL_WINDOW_OPENGL);

	if (GameWindow == nullptr) {
		this->GameInitializedProperly = false;
		this->GameRunning = false;
		SDL_Quit();
	}

	SDL_GL_SetAttribute(SDL_GL_RED_SIZE, 8);
	SDL_GL_SetAttribute(SDL_GL_GREEN_SIZE, 8);
	SDL_GL_SetAttribute(SDL_GL_BLUE_SIZE, 8);
	SDL_GL_SetAttribute(SDL_GL_ALPHA_SIZE, 8);
	SDL_GL_SetAttribute(SDL_GL_DOUBLEBUFFER, 1);
	SDL_GL_SetAttribute(SDL_GL_MULTISAMPLESAMPLES, 1);
	SDL_GL_SetAttribute(SDL_GL_CONTEXT_MINOR_VERSION, 2);
	SDL_GL_CreateContext(GameWindow);

	glewExperimental = GL_TRUE;

	if (glewInit() != GL_TRUE) {
		this->GameInitializedProperly = false;
		this->GameRunning = false;
	}

	OnLoad();
	while (GameRunning) {
		OnUpdate();
		OnRender();
	}

	OnClosing();
	OnClosed();
}

void Silent::Silent_Game::OnLoad()
{
	OnLoadGame();
}

void Silent::Silent_Game::OnUpdate()
{
	
	OnUpdateGame();
	SDL_UpdateWindowSurface(GameWindow);
}

void Silent::Silent_Game::OnRender()
{
	OnRenderGame();
}

void Silent::Silent_Game::OnClosing()
{
	OnClosingGame();
}

void Silent::Silent_Game::OnClosed()
{
	OnClosedGame();
}

void Silent::Silent_Game::LoadLevel(Silent_Level & levelToLoad)
{
}

void Silent::Silent_Game::SetCurrentLevel(Silent_Level & newCurrentLevel)
{
}
