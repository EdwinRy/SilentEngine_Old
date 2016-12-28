#include "../GameSystem/Game.h"

#undef main

class SampleGame : public Silent::Silent_Game {

public:
	void OnLoadGame() {
		std::cout << "load" << std::endl;
	}

};

int main() {

	Silent::Silent_Game* sampleGame = new SampleGame();

	sampleGame->WindowPositionX = SDL_WINDOWPOS_CENTERED;
	sampleGame->WindowPositionY = SDL_WINDOWPOS_CENTERED;

	sampleGame->MainGameLoop();

	return 0;
}

