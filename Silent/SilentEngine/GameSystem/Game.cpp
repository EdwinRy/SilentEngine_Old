#include "Game.h"


Silent_Core::GameSystem::Game::Game()
{

	

	// initialize SDL
	SDL_Init(SDL_INIT_VIDEO);

	
	if (m_windowType == "Borderless") {
		m_borderType = SDL_WINDOW_BORDERLESS;
	}

	else if (m_windowType == "Fullscreen") {
		m_borderType = SDL_WINDOW_FULLSCREEN;
	}

	else if (m_windowType == "Resizable") {
		m_borderType = SDL_WINDOW_RESIZABLE;
	}

	else if (m_windowType == "Fullscreen Desktop") {
		m_borderType = SDL_WINDOW_FULLSCREEN_DESKTOP;
	}

	else if (m_windowType == "Hidden") {
		m_borderType = SDL_WINDOW_HIDDEN;
	}

	SDL_CreateWindow(
		m_title,
		m_windowPosX,
		m_windowPosY,
		m_windowWidth,
		m_windowHeight,
		SDL_WINDOW_OPENGL | m_borderType);

	SDL_GL_SetAttribute(SDL_GL_RED_SIZE, 8);
	SDL_GL_SetAttribute(SDL_GL_GREEN_SIZE, 8);
	SDL_GL_SetAttribute(SDL_GL_BLUE_SIZE, 8);
	SDL_GL_SetAttribute(SDL_GL_ALPHA_SIZE, 8);
	SDL_GL_SetAttribute(SDL_GL_DOUBLEBUFFER, 1);

	SDL_GL_CreateContext(m_screen);

	setUpOpenGL();

	SDL_GL_SetSwapInterval(1);


	OnLoad();

	while (m_running == true) {
		OnUpdate();
		OnRender();
	}

	OnClosing();
	//Terminate SDL here or in OnClosing

	OnClosed();
}

void Silent_Core::GameSystem::Game::setUpOpenGL()
{
	SDL_GL_SetAttribute(SDL_GL_CONTEXT_PROFILE_MASK, SDL_GL_CONTEXT_PROFILE_CORE);

	SDL_GL_SetAttribute(SDL_GL_CONTEXT_MAJOR_VERSION, 4);
	SDL_GL_SetAttribute(SDL_GL_CONTEXT_MINOR_VERSION, 2);

	SDL_GL_SetAttribute(SDL_GL_DOUBLEBUFFER, 1);

}

void Silent_Core::GameSystem::Game::OnLoad()
{
	glClearColor(0, 0, 0, 1);
	OnLoadGame();
}

void Silent_Core::GameSystem::Game::OnUpdate()
{
	OnUpdateGame();
}

void Silent_Core::GameSystem::Game::OnRender()
{
	
	OnRenderGame();
}

void Silent_Core::GameSystem::Game::OnClosing()
{
	
	OnClosingGame();
}

void Silent_Core::GameSystem::Game::OnClosed()
{
	
	OnClosedGame();
}