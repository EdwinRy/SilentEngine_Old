#include "Game.h"
#include "SDL.h"

Silent::silent_Game::silent_Game()
{
	if (!SDL_Init(SDL_INIT_VIDEO)) {
		this->GameInitializedProperly = false;
	}
}

void Silent::silent_Game::MainLoop()
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

void Silent::silent_Game::OnLoad()
{
	OnLoadGame();
}

void Silent::silent_Game::OnUpdate()
{
	OnUpdateGame();
}

void Silent::silent_Game::OnRender()
{
	OnRenderGame();
}

void Silent::silent_Game::OnClosing()
{
	OnClosingGame();
}

void Silent::silent_Game::OnClosed()
{
	OnClosedGame();
}
