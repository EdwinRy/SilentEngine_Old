#include "Game.h"

void Silent::Silent_Game::MainGameLoop()
{

	if (SDL_Init(SDL_INIT_VIDEO) < 0) {
		std::cout << "Couldn't initialize SDL!" << std::endl;
	}

	SDL_GL_SetAttribute(SDL_GL_RED_SIZE, 8);
	SDL_GL_SetAttribute(SDL_GL_GREEN_SIZE, 8);
	SDL_GL_SetAttribute(SDL_GL_BLUE_SIZE, 8);
	SDL_GL_SetAttribute(SDL_GL_ALPHA_SIZE, 8);
	SDL_GL_SetAttribute(SDL_GL_CONTEXT_MINOR_VERSION, 2);
	SDL_GL_SetAttribute(SDL_GL_CONTEXT_MAJOR_VERSION, 4);

	if (DoubleBuffering) {
		SDL_GL_SetAttribute(SDL_GL_DOUBLEBUFFER, 1);
	}
	if (Vsync) {
		SDL_GL_SetSwapInterval(1);
	}

	this->GameWindow = SDL_CreateWindow(
		WindowTitle,
		WindowPositionX,
		WindowPositionY,
		WindowWidth,
		WindowHeight,
		WindowType | SDL_WINDOW_OPENGL);

	glContext = SDL_GL_CreateContext(GameWindow);

	glewExperimental = GL_TRUE;

	if (glewInit() < 0) {
		std::cout << "Failed to initialize glew" << std::endl;
	}

	GameRunning = true;

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
}

void Silent::Silent_Game::OnRender()
{
	OnRenderGame();
	SDL_GL_SwapWindow(GameWindow);
}

void Silent::Silent_Game::OnClosing()
{
	OnClosingGame();
}

void Silent::Silent_Game::OnClosed()
{
	OnClosedGame();
}
